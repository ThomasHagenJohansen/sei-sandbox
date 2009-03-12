using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace DGWSSignedMortalityClient
{
	partial class Program
	{
		private X509Certificate2 GetSKSMOCESCertificate()
		{
			if (sksMOCES == null)
			{
				String path = Path.Combine(Directory.GetCurrentDirectory(), "SKSMedarbejder.p12");
				sksMOCES    = new X509Certificate2(path, "Test1234");
			}

			return sksMOCES;
		}
		private X509Certificate2 sksMOCES = null;

		private X509Certificate2 GetSKSVOCESCertificate()
		{
			if (sksVOCES == null)
			{
				String path = Path.Combine(Directory.GetCurrentDirectory(), "SKSVirksomhed.p12");
				sksVOCES    = new X509Certificate2(path, "Test1234");
			}

			return sksVOCES;
		}
		private X509Certificate2 sksVOCES = null;

		private X509Certificate2 GetMOCESCertificate()
		{
			if (moces == null)
			{
				String path = Path.Combine(Directory.GetCurrentDirectory(), "TestMOCES1.p12");
				moces = new X509Certificate2(path, "Test1234");
			}

			return moces;
		}
		private X509Certificate2 moces = null;
	}
}
