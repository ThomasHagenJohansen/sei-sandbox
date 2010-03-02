using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using System.Xml;

using DGWSBDBNonGP.BDBNonGPChildReport;
using Microsoft.Web.Services3;
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
//				Tieto.HCW.Framework.Net.Policy.SetPolicy();
//				Tieto.HCW.Framework.Net.Policy.ValidationFailed += Policy_ValidationFailed;

				String path = Path.Combine(Directory.GetCurrentDirectory(), "TestMOCES1.pfx");
				X509Certificate2 moces = new X509Certificate2(path, "Underskriv2");

//				path = Path.Combine(Directory.GetCurrentDirectory(), "TestVOCES1.pfx");
//				X509Certificate2 voces = new X509Certificate2(path, "Test1234");

				BDBNonGPChildReport service = new BDBNonGPChildReport();
//				service.SetPolicy(new DGWSPolicy(moces, voces));
				service.SetPolicy(new DGWSPolicy(moces, moces));


				/*CREATE TEST*/
				CreateChildMeasurementReportType report = new CreateChildMeasurementReportType();
				report.ChildMeasurement = new ChildMeasurementType();
				report.ChildMeasurement.MeasurementDate = DateTime.Parse("2009-03-18");
				report.ChildMeasurement.PersonHeight = 1.11M;
				report.ChildMeasurement.PersonWeight = 22.2M;
				report.ChildMeasurement.PersonCivilRegistrationIdentifier = "1901075766";
//				report.ChildMeasurement.InstitutionIdentifier = "157";
//				report.ChildMeasurement.InstitutionIdentifier = "101168";
//				report.ChildMeasurement.InstitutionIdentifier = "031011";
				report.ChildMeasurement.InstitutionIdentifier = "12";
				String s = service.CreateChildMeasurementReport(report);
				System.Diagnostics.Debug.WriteLine(s);


				/*MODIFY TEST*/
/*				ModifyChildMeasurementReportType report = new ModifyChildMeasurementReportType();
				report.ChildMeasurement = new ChildMeasurementType();
				report.ChildMeasurement.MeasurementDate = DateTime.Parse("2007-12-30");
				report.ChildMeasurement.PersonHeight = 1.46M;
				report.ChildMeasurement.PersonWeight = 7.3M;
				report.ChildMeasurement.PersonCivilRegistrationIdentifier = "1901075766";
				report.ChildMeasurement.InstitutionIdentifier = "031011";
				report.UniversallyUniqueIdentifier = "bd174000-4aa8-4dd6-8816-09b39cd368cf";
				String s = service.ModifyChildMeasurementReport(report);
				System.Diagnostics.Debug.WriteLine(s);
*/

				/*DELETE TEST*/
/*				DeleteChildMeasurementReportType report = new DeleteChildMeasurementReportType();
				report.UniversallyUniqueIdentifier = "bd174000-4aa8-4dd6-8816-09b39cd368cf";
				bool b = service.DeleteChildMeasurementReport(report);
*/

				/*BREASTFEEDING TEST*/
/*				ExclusivelyBreastFeedingPeriodEndReportType amningReport = new ExclusivelyBreastFeedingPeriodEndReportType();
				amningReport.ExclusivelyBreastFeedingPeriodEnd = DateTime.Parse("2001-06-20");
				amningReport.PersonCivilRegistrationIdentifier = "1312814435";
				bool b = service.SetExclusivelyBreastFeedingPeriodEndReport(amningReport);
*/

				/*PASSIVESMOKING TEST*/
/*				ExposedToPassiveSmokingReportType passiveSmoking = new ExposedToPassiveSmokingReportType();
				passiveSmoking.PersonCivilRegistrationIdentifier = "1312814435";
				passiveSmoking.ExposedToPassiveSmoking = ExposedToPassiveSmokingType.Unknown;
				bool b = service.SetExposedToPassiveSmokingReport(passiveSmoking);
*/			}
			catch (SoapHeaderException ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			catch (SoapException ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
		}

/*		private static void Policy_ValidationFailed(object sender, Tieto.HCW.Framework.Net.Policy.ValidationFailedEventArgs vfea)
		{
			MessageBox.Show(vfea.Message);
			vfea.AcceptCertificate = true;
		}
*/	}

	public class DGWSPolicy : Policy
	{
		private readonly X509Certificate2 moces;
		private readonly X509Certificate2 voces;

		public DGWSPolicy(X509Certificate2 moces, X509Certificate2 voces)
		{
			this.moces = moces;
			this.voces = voces;

			Assertions.Add(new RequireActionHeaderAssertion());		// WSE policy

			Assertions.Add(new MessageIDChanger());

			DGWSAssertion dgwsAss = new DGWSAssertion();
			dgwsAss.GetIDCard = GetIDCard;
			Assertions.Add(dgwsAss);

			Assertions.Add(new AddressingConverterAssertion());

			MessageSignAssertion msgAss = new MessageSignAssertion();
			msgAss.certificate = moces;
			//msgAss.acceptedcartificates = new[] { "CVR:25767535-UID:1100080130597 + CN=TDC TOTALLØSNINGER A/S - TDC Test" };msgAss.acceptedcartificates = new[] { "CVR:25767535-UID:1100080130597 + CN=TDC TOTALLØSNINGER A/S - TDC Test" };
			msgAss.acceptedcartificates = new[] { "*" };
			Assertions.Add(msgAss);
		}

		private DGWSCard10Type GetIDCard(String version)
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

			// System log
			card.ITSystemName            = "SEI Client";
			card.OrganisationID          = "12070918";
			card.OrganisationIDFormat    = FormatIds.cvrnumber;
			card.OrganisationName        = "SST";

			return card;
		}

		private X509Certificate2 GetCertificate()
		{
			return voces;
		}
	}

	public class MessageIDChanger : PolicyAssertion
	{
		public override SoapFilter CreateClientInputFilter(FilterCreationContext context)
		{
			return new MessageIDChangerInFilter();
		}

		public override SoapFilter CreateClientOutputFilter(FilterCreationContext context)
		{
			return new MessageIDChangerOutFilter();
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

	public class MessageIDChangerInFilter : SoapFilter
	{
		public override SoapFilterResult ProcessMessage(SoapEnvelope envelope)
		{
			return SoapFilterResult.Continue;
		}
	}

	public class MessageIDChangerOutFilter : SoapFilter
	{
		public override SoapFilterResult ProcessMessage(SoapEnvelope envelope)
		{
/*			XmlNodeList n = envelope.Header.GetElementsByTagName("MessageID", "http://schemas.xmlsoap.org/ws/2004/08/addressing");
            n[0].InnerText = "urn:uuid:bb7961f8-019a-4c6e-aa84-bb7f46ba6bbd";

			envelope.Context.Addressing = new AddressingHeaders(envelope);
*/
//			envelope.Body.InnerXml = envelope.Body.InnerXml.Replace("0.46", "0,46");
			return SoapFilterResult.Continue;
		}
	}
}
