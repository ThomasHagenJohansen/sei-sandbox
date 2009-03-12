using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using System.Xml;
using DGWSSignedMortalityClient.sei_frontend;

namespace DGWSSignedMortalityClient
{
	partial class Program
	{
		// Sign an XML file and save the signature in a new file.
		XmlDocument SignParts(SignedMortalityReasonType smrt, Boolean bPart1, Boolean bPart2, X509Certificate2 certificate)
		{
			String xmlSource = Serialize(smrt, smrt.GetType());

			// Create a SignedXml object.
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xmlSource);
			SignedXml signedXml = new SignedXml(doc);

			// Add parts to be signed
			if (bPart1) AddSignatureReference(signedXml, doc, "Part1");
			if (bPart2) AddSignatureReference(signedXml, doc, "Part2");

			// Get Certificate and signing Key
			signedXml.SigningKey = certificate.PrivateKey;

			// Add public key and certificate
			KeyInfo keyInfo = new KeyInfo();
			keyInfo.AddClause(new RSAKeyValue((RSA)certificate.PublicKey.Key));
			keyInfo.AddClause(new KeyInfoX509Data(certificate));
			signedXml.KeyInfo = keyInfo;

			// Compute the signature.
			signedXml.ComputeSignature();

			// Get the XML representation of the signature and save it to an XmlElement object.
			XmlElement xmlSignature = signedXml.GetXml();
			doc.PreserveWhitespace = true;
			XmlNode n = doc.ImportNode(xmlSignature, true);
			String res = n.OuterXml;

			String tagName = "";
			if (bPart1 && bPart2)
				tagName = "AllSignature";
			else if (bPart1)
				tagName = "Part1Signature";
			else if (bPart2)
				tagName = "Part2Signature";

			XmlNode root = doc.DocumentElement;
			XmlNode node = root[tagName];
			if (node.HasChildNodes == false)
				node.AppendChild(n);
			else
				node.ReplaceChild(n, node.FirstChild);
			return (doc);
		}

		void AddSignatureReference(SignedXml signedXml, XmlDocument doc, String partName)
		{
			DataObject dataObject = new DataObject();
			XmlElement elPart = (XmlElement)doc.GetElementsByTagName(partName)[0];
			dataObject.Data = doc.GetElementsByTagName(partName);
			elPart.SetAttribute("id", partName);

			// Create a reference to be signed.
			Reference reference = new Reference();
			reference.Uri = "#" + partName;
			reference.AddTransform(new XmlDsigC14NTransform()); // Da det er xml vi signerer, ønsker vi Canonicalisation.
			signedXml.AddReference(reference);
		}

		// Verify the signature of an XML file and return the result.
		bool VerifyDetachedSignature(string filenameSignedXml)
		{
			XmlDocument docSigned = new XmlDocument();
			docSigned.PreserveWhitespace = true;
			docSigned.Load(filenameSignedXml);

			XmlElement elPart1 = (XmlElement)docSigned.GetElementsByTagName("Part1")[0];

			// Load the signature node.
			SignedXml signedXml = new SignedXml(docSigned); // load original document
			signedXml.LoadXml((XmlElement)docSigned.GetElementsByTagName("Signature")[0]);

			// Check the signature and return the result.
			bool bRes = signedXml.CheckSignature();
			return bRes;
		}
	}
}