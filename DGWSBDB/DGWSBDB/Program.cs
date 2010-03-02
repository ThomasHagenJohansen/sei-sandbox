using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using DGWSBDB.BDBChildMeasurementReport;
using Microsoft.Web.Services3.Design;
using SDSD.SealApi;
using SDSD.SealApi.Assertion;

namespace DGWSBDB
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Tieto.HCW.Framework.Net.Policy.SetPolicy();
				Tieto.HCW.Framework.Net.Policy.ValidationFailed += Policy_ValidationFailed;

				String path = Path.Combine(Directory.GetCurrentDirectory(), "TestMOCES1.pfx");
				X509Certificate2 moces = new X509Certificate2(path, "Test1234");

				CreateChildMeasurementReportType report = new CreateChildMeasurementReportType();
				report.ChildMeasurement = new ChildMeasurementType();
				report.ChildMeasurement.MeasurementDate = DateTime.Parse("2009-03-18");
				report.ChildMeasurement.PersonHeight = 1.11M;
				report.ChildMeasurement.PersonWeight = 22.2M;
				report.ChildMeasurement.PersonCivilRegistrationIdentifier = "xxxxxxxxxx";
				report.ChildMeasurement.InstitutionIdentifier = "031011";

				BDBChildMeasurementReport.BDBChildMeasurementReport service = new BDBChildMeasurementReport.BDBChildMeasurementReport();
				service.SetPolicy(new DGWSPolicy(moces));
//				service.Url = @"http://localhost:2245/BDBChildMeasurementReport.asmx";
//				service.Url = @"https://testei.sst.dk/DGWS/BDBChildMeasurementReport.asmx";
//				service.Url = @"https://seidgws.sst.dk/DGWS/BDBChildMeasurementReport.asmx";
				String s = service.CreateChildMeasurementReport(report);
				System.Diagnostics.Debug.WriteLine(s);
			}
			catch(SoapHeaderException ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			catch(SoapException ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
		}

		private static void Policy_ValidationFailed(object sender, Tieto.HCW.Framework.Net.Policy.ValidationFailedEventArgs vfea)
		{
			MessageBox.Show(vfea.Message);
			vfea.AcceptCertificate = true;
		}
	}

	public class DGWSPolicy : Policy
	{
		private X509Certificate2 cert;

		public DGWSPolicy(X509Certificate2 moces)
		{
			cert = moces;

			Assertions.Add(new RequireActionHeaderAssertion());		// WSE policy

			DGWSAssertion dgwsAss = new DGWSAssertion();
			dgwsAss.GetIDCard = GetCard;
			Assertions.Add(dgwsAss);

			Assertions.Add(new AddressingConverterAssertion());

			MessageSignAssertion msgAss = new MessageSignAssertion();
			msgAss.certificate = moces;
			msgAss.acceptedcartificates = new[] { "*" };
//			msgAss.acceptedcartificates = new[] { "CVR:25767535-UID:1100080130597 + CN=TDC TOTALLØSNINGER A/S - TDC Test" };
			Assertions.Add(msgAss);
		}

		private DGWSCard10Type GetCard(String version)
		{
			DGWSCard10Type card = GetIDCard();
			card.Sign(cert);

			return card;
		}

		private DGWSCard10Type GetIDCard()
		{
			DGWSCard11Type card = new DGWSCard11Type();

			// Issuer
			card.Issuer                  = "SEI Client";

			// Subject
			card.NameID                  = "1111111118";
			card.NameIDFormat            = FormatIds.cprnumber;

			// Conditions
			card.CardLifeTime            = CardLifeTimeType.Hours8;

			// Authentication (STS)

			// Attributes
			card.IDCardType              = CardType.user;
			card.AuthenticationLevel     = 3;
			card.CivilRegistrationNumber = "1111111118";
			card.GivenName               = "Bent";
			card.SurName                 = "Hansen";
			card.EmailAddress            = "bent@hansen.dk";
			card.Role                    = "SEI User";
			card.Occupation              = "?";
			card.AuthorizationCode       = "19901";

			// System log
			card.ITSystemName            = "SEI Client";
			card.OrganisationID          = "12070918";
			card.OrganisationIDFormat    = FormatIds.cvrnumber;
			card.OrganisationName        = "SST";

			return card;
		}
	}
}
