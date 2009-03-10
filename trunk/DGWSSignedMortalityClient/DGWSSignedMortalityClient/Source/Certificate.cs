using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace DGWSSignedMortalityClient
{
	partial class Program
	{
		X509Certificate2 GetCertificate()
		{
			String path = Path.Combine(Directory.GetCurrentDirectory(), "TestMOCES1.pfx");
			X509Certificate2 moces = new X509Certificate2(path, "Test1234");
			return (moces);
		}

		X509Certificate2 GetCertificate2()
		{
			String path = Path.Combine(Directory.GetCurrentDirectory(), "TestMOCES1.pfx");
			X509Certificate2 moces = new X509Certificate2(path, "Test1234");
			return (moces);
		}
	}
}