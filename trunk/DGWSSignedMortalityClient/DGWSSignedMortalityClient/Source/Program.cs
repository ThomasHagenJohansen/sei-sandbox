using System;
using System.Xml;
using ConsoleApplication5.sei_frontend;

namespace DGWSSignedMortalityClient
{
	partial class Program
	{
		static String OutFileName = "Signed.xml";

		static void Main(string[] args)
		{
			new Program().Run();
		}

		public void Run()
		{
			int type = 3;
			XmlDocument doc = null;
			SignedMortalityReasonType smrt = null;
			if(type==0)
			{
				smrt = CreateInstancePart1();
				doc = SignParts(smrt, true, false, GetCertificate());
			}
			else if(type==1)
			{
				smrt = CreateInstancePart2();
				doc = SignParts(smrt, false, true, GetCertificate());
			}
			else if (type == 2)
			{
				smrt = CreateInstancePart1AndPart2ForSameCertificate();
				doc = SignParts(smrt, true, true, GetCertificate());
			}
			else if (type == 3)
			{
				smrt = CreateInstancePart1AndPart2ForDifferentCertificates();
				doc = SignParts(smrt, true, false, GetCertificate());
				smrt = (SignedMortalityReasonType)Deserialize(doc.OuterXml, smrt.GetType());
				doc = SignParts(smrt, false, true, GetCertificate2());
			}
			doc.Save(OutFileName);

			// Standard verifier
			bool bOK = VerifyDetachedSignature(OutFileName);

			smrt = (SignedMortalityReasonType)Deserialize(doc.OuterXml, smrt.GetType());

			try
			{
				MortalityRegistrationService service = new MortalityRegistrationService();
				service.SetPolicy(new DGWSPolicy(GetCertificate()));
				bool b = service.Report(smrt);
				System.Diagnostics.Debug.WriteLine(b);
			}
			catch (Exception e)
			{
				Console.Out.Write(e.Message);
			}
		}
	}
}
