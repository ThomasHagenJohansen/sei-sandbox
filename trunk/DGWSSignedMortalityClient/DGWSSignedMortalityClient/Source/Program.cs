using System;
using System.Xml;
using DGWSSignedMortalityClient.sei_frontend;
using DGWSSignedMortalityClient.STS;

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
			int type = 2;
			XmlDocument doc = null;
			SignedMortalityReasonType smrt = null;
			bool useSTS = true;		// url: http://pan.certifikat.dk/sts/

/*			if(type==0)
			{
				smrt = CreateInstancePart1();
				doc = SignParts(smrt, true, false, GetMOCESCertificate());
			}
			else if(type==1)
			{
				smrt = CreateInstancePart2();
				doc = SignParts(smrt, false, true, GetMOCESCertificate());
			}
			else if (type == 2)
			{
				smrt = CreateInstancePart1AndPart2ForSameCertificate();
				doc = SignParts(smrt, true, true, GetMOCESCertificate());
			}
			else if (type == 3)
			{
				smrt = CreateInstancePart1AndPart2ForDifferentCertificates();
				doc = SignParts(smrt, true, false, GetSKSMOCESCertificate());
				smrt = (SignedMortalityReasonType)Deserialize(doc.OuterXml, smrt.GetType());
				doc = SignParts(smrt, false, true, GetMOCESCertificate());
			}
			//doc.Save(OutFileName);

			// Standard verifier
			//bool bOK = VerifyDetachedSignature(OutFileName);

			smrt = (SignedMortalityReasonType)Deserialize(doc.OuterXml, smrt.GetType());
*/
			try
			{
				if (useSTS)
				{
					AxisStsFacadeService sts = new AxisStsFacadeService();
					sts.SetPolicy(new DGWSPolicy(GetSKSMOCESCertificate(), GetSKSVOCESCertificate(), true));
					Object o = sts.issueIdCard(null);
					int g = 5;
				}

				MortalityRegistrationService service = new MortalityRegistrationService();
				service.SetPolicy(new DGWSPolicy(GetMOCESCertificate(), GetSKSVOCESCertificate(), false));
				bool b = service.Report(smrt);
//				bool b = service.RemoveReport("{B460D543-4627-4FEE-A310-367151256F32}");
				System.Diagnostics.Debug.WriteLine(b);
			}
			catch (Exception e)
			{
				Console.Out.Write(e.ToString());
				System.Diagnostics.Debug.WriteLine(e.ToString());
			}
		}
	}
}
