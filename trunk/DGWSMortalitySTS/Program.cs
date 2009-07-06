using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;
using DGWSMortalitySTS.Mortality;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Design;
using SDSD.SealApi;
using SDSD.SealApi.Assertion;

namespace DGWSMortalitySTS
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				bool useSTS = true;

//				X509Certificate2 MOCESCert = GetMOCESCertificate();
//				X509Certificate2 VOCESCert = GetVOCESCertificate();
				X509Certificate2 MOCESCert = GetSTSMOCESCertificate();
				X509Certificate2 VOCESCert = GetSTSVOCESCertificate();

				DGWSCard10Type card;

				if (useSTS)
				{
					DGWSCard101Type idCard = GetIDCardVersion101();
					idCard.Sign(MOCESCert);
//					DGWSCard101Type idCard = GetSystemIDCardVersion101();
//					idCard.Sign(VOCESCert);

					XElement x = IDP.CallIdp(idCard, "SEI", "http://pan.certifikat.dk/sts/services/SecurityTokenService");

					card = new DGWSCard101Type(x);
				}
				else
				{
//					card = GetIDCardVersion101();
//					card = GetIDCardVersion11();
					card = GetSystemIDCardVersion101();
//					card.Sign(MOCESCert);
					card.Sign(VOCESCert);
				}

				MortalityRegistrationService service = new MortalityRegistrationService();
				service.SetPolicy(new DGWSPolicy(card, VOCESCert));
//				service.SetPolicy(new DGWSPolicy(card, MOCESCert));
				MortalityReasonType mort = Helper.CreateTestDocument_Part1And2();
//				MortalityReasonType mort = Helper.CreateTestDocument_Part1();
				String s = service.Report(mort);
				System.Diagnostics.Debug.WriteLine(s);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
		}

		private static X509Certificate2 GetMOCESCertificate()
		{
			String path = Path.Combine(Directory.GetCurrentDirectory(), "Certificates\\TestMOCES1.p12");
			return new X509Certificate2(path, "Test1234");
		}

		private static X509Certificate2 GetVOCESCertificate()
		{
			String path = Path.Combine(Directory.GetCurrentDirectory(), "Certificates\\TestVOCES1.p12");
			return new X509Certificate2(path, "Test1234");
		}

		private static X509Certificate2 GetSTSMOCESCertificate()
		{
			String path = Path.Combine(Directory.GetCurrentDirectory(), "Certificates\\STSMedarbejder.p12");
			return new X509Certificate2(path, "Test1234");
		}

		private static X509Certificate2 GetSTSVOCESCertificate()
		{
			String path = Path.Combine(Directory.GetCurrentDirectory(), "Certificates\\STSVirksomhed.p12");
			return new X509Certificate2(path, "Test1234");
		}

		private static DGWSCard101Type GetIDCardVersion101()
		{
			DGWSCard101Type card = new DGWSCard101Type();

			card.Issuer = "SEI Client";

			card.NameID = "2207712801";
			card.NameIDFormat = FormatIds.cprnumber;

			card.CardLifeTime = CardLifeTimeType.Hours24;

			card.IDCardType = CardType.user;
			card.AuthenticationLevel = 4;
			card.CivilRegistrationNumber = "2207712801";
			card.GivenName = "Thomas";
			card.SurName = "Neumann";
			card.EmailAddress = "thomas.neumann@tieto.com";
			card.Role = "SEI User";
			card.Occupation = "?";
//			card.AuthorizationCode = "03M5P";	// Sygeplejske
			card.AuthorizationCode = "06CLX";	// Læge

			card.ITSystemName = "SEI Client";
			card.OrganisationID = "12070918";
			card.OrganisationIDFormat = FormatIds.cvrnumber;
			card.OrganisationName = "SST";

//			card.AuthenticatingAuthority = "http://sosi.dk";

			return card;
		}

		private static DGWSCard11Type GetIDCardVersion11()
		{
			DGWSCard11Type card = new DGWSCard11Type();

			card.Issuer = "SEI Client";

			card.NameID = "2207712801";
			card.NameIDFormat = FormatIds.cprnumber;

			card.CardLifeTime = CardLifeTimeType.Hours24;

			card.IDCardType = CardType.user;
			card.AuthenticationLevel = 3;
			card.CivilRegistrationNumber = "2207712801";
			card.GivenName = "Thomas";
			card.SurName = "Neumann";
			card.EmailAddress = "thomas.neumann@tieto.com";
			card.Role = "SEI User";
			card.Occupation = "?";
			card.AuthorizationCode = "19901";

			card.ITSystemName = "SEI Client";
			card.OrganisationID = "12070918";
			card.OrganisationIDFormat = FormatIds.cvrnumber;
			card.OrganisationName = "SST";

//			card.AuthenticatingAuthority = "http://sosi.dk";

			return card;
		}

		private static DGWSCard101Type GetSystemIDCardVersion101()
		{
			DGWSCard101Type card = new DGWSCard101Type();

			card.Issuer = "SEI Client";

			card.NameID = "12070918";
			card.NameIDFormat = FormatIds.cvrnumber;

			card.CardLifeTime = CardLifeTimeType.Hours24;

			card.IDCardType = CardType.system;
			card.AuthenticationLevel = 3;

			card.ITSystemName = "SEI Client";
			card.OrganisationID = "12070918";
			card.OrganisationIDFormat = FormatIds.cvrnumber;
			card.OrganisationName = "SST";

//			card.AuthenticatingAuthority = "http://sosi.dk";

			return card;
		}
	}

	public class DGWSPolicy : Policy
	{
		private readonly DGWSCard10Type card;

		public DGWSPolicy(DGWSCard10Type card, X509Certificate2 voces)
		{
			this.card  = card;

			Assertions.Add(new RequireActionHeaderAssertion());		// WSE policy

			DGWSAssertion dgwsAss = new DGWSAssertion();
			dgwsAss.GetIDCard = GetIDCard;
			Assertions.Add(dgwsAss);

			Assertions.Add(new AddIDsAssertion());

			Assertions.Add(new AddressingConverterAssertion());

			MessageSignAssertion msgAss = new MessageSignAssertion();
			msgAss.certificate = voces;
			msgAss.acceptedcartificates = new[] { "*" };
			Assertions.Add(msgAss);
		}

		private DGWSCard10Type GetIDCard(String version)
		{
			return card;
		}
	}

	public class AddIDsAssertion : PolicyAssertion
	{
		public override SoapFilter CreateClientInputFilter(FilterCreationContext context)
		{
			return new AddIDsInFilter();
		}

		public override SoapFilter CreateClientOutputFilter(FilterCreationContext context)
		{
			return new AddIDsOutFilter();
		}

		public override SoapFilter CreateServiceInputFilter(FilterCreationContext context)
		{
			return null;
		}

		public override SoapFilter CreateServiceOutputFilter(FilterCreationContext context)
		{
			return null;
		}
	}

	public class AddIDsInFilter : SoapFilter
	{
		public override SoapFilterResult ProcessMessage(SoapEnvelope envelope)
		{
			return SoapFilterResult.Continue;
		}
	}

	public class AddIDsOutFilter : SoapFilter
	{
		public override SoapFilterResult ProcessMessage(SoapEnvelope envelope)
		{
			try
			{
				var env = XElement.Load(envelope.GetDocumentReader());
				RequestResponse.SetIds(env);

				using (var rd = env.CreateReader())
				{
					envelope.Load(rd);
				}
			}
			catch
			{
			}

			return SoapFilterResult.Continue;
		}
	}
}
