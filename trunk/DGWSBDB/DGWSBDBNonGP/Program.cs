using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Services.Protocols;
using System.Xml;
using DgwsWse.HeaderTypes;
using Medcom.DgwsWse;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Addressing;
using Microsoft.Web.Services3.Design;
using DGWSAssertion = Medcom.DgwsWse.DGWSAssertion;
using MessageSignAssertion = Medcom.DgwsWse.MessageSignAssertion;

using DGWSBDBNonGP.BDBNonGPChildReport;


namespace DGWSBDB
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				String path = Path.Combine(Directory.GetCurrentDirectory(), "TestMOCES1.pfx");
				X509Certificate2 moces = new X509Certificate2(path, "Underskriv3");

				path = Path.Combine(Directory.GetCurrentDirectory(), "TestVOCES1.pfx");
				X509Certificate2 voces = new X509Certificate2(path, "Test1234");

				BDBNonGPChildReport service = new BDBNonGPChildReport();
//				service.SetPolicy(new DGWSPolicy(moces, voces));
				service.SetPolicy(new DGWSPolicy(moces, moces));


				/*CREATE TEST*/
				CreateChildMeasurementReportType report = new CreateChildMeasurementReportType();
				report.ChildMeasurement = new ChildMeasurementType();
				report.ChildMeasurement.MeasurementDate = DateTime.Parse("2007-12-27");
				report.ChildMeasurement.PersonHeight = 0.46M;
				report.ChildMeasurement.PersonWeight = 8.2M;
				report.ChildMeasurement.PersonCivilRegistrationIdentifier = "1901075766";
				report.ChildMeasurement.InstitutionIdentifier = "031011";
				String s = service.CreateChildMeasurementReport(report);
				System.Diagnostics.Debug.WriteLine(s);


				/*MODIFY TEST*/
/*				ModifyChildMeasurementReportType report = new ModifyChildMeasurementReportType();
				report.ChildMeasurement = new ChildMeasurementType();
				report.ChildMeasurement.MeasurementDate = DateTime.Parse("2005-12-30");
				report.ChildMeasurement.PersonHeight = 1.46M;
				report.ChildMeasurement.PersonWeight = 7.23M;
				report.ChildMeasurement.PersonCivilRegistrationIdentifier = "131281-4435";
				report.ChildMeasurement.InstitutionIdentifier = "031011";
				report.UniversallyUniqueIdentifier = "b3325bc3-c933-4de1-aa44-5d5c14cf5dbe";//"aaa25bc3-c933-4de1-aa44-5d5c14cf5dbe";//
				String s = service.ModifyChildMeasurementReport(report);
				System.Diagnostics.Debug.WriteLine(s);
*/

				/*DELETE TEST*/
				//DeleteChildMeasurementReportType report = new DeleteChildMeasurementReportType();
				//report.UniversallyUniqueIdentifier = "b3325bc3-c933-4de1-aa44-5d5c14cf5dbe";
				//bool b = service.DeleteChildMeasurementReport(report);


				/*BREASTFEEDING TEST*/
				//ExclusivelyBreastFeedingPeriodEndReportType amningReport = new ExclusivelyBreastFeedingPeriodEndReportType();
				//amningReport.ExclusivelyBreastFeedingPeriodEnd = DateTime.Parse("2001-06-20");
				//amningReport.PersonCivilRegistrationIdentifier = "1312814435";
				//bool b = service.SetExclusivelyBreastFeedingPeriodEndReport(amningReport);


				/*PASSIVESMOKING TEST*/
				//ExposedToPassiveSmokingReportType passiveSmoking = new ExposedToPassiveSmokingReportType();
				//passiveSmoking.PersonCivilRegistrationIdentifier = "1312814435";
				//passiveSmoking.ExposedToPassiveSmoking = ExposedToPassiveSmokingType.Unknown;
				//bool b = service.SetExposedToPassiveSmokingReport(passiveSmoking);



				
			}
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
	}

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
			dgwsAss.Card = GetIDCard();
			Assertions.Add(dgwsAss);

			Assertions.Add(new AddressingConverterAssertion());

			MessageSignAssertion msgAss = new MessageSignAssertion();
			msgAss.certificate = moces;
			//msgAss.acceptedcartificates = new[] { "CVR:25767535-UID:1100080130597 + CN=TDC TOTALLØSNINGER A/S - TDC Test" };msgAss.acceptedcartificates = new[] { "CVR:25767535-UID:1100080130597 + CN=TDC TOTALLØSNINGER A/S - TDC Test" };
			msgAss.acceptedcartificates = new[] { "*" };
			Assertions.Add(msgAss);
		}

		private IDCardType GetIDCard()
		{
			IDCardType card = new IDCardType();
			AuthenticityType auth = new AuthenticityType();
			IDCardDataType data = new IDCardDataType();

			card.Authenticity = auth;
			card.IDCardData = data;
//			card.leveTid = new TimeSpan(24, 0, 0);

			data.IDCardType = "IdCardSignature";
			data.IDCardVersion = "1.1";

			// Issuer
			card.Issuer = "SEI Client";

			// Subject
			auth.NameID = "1111111118";
			//			card.NameIDFormat            = FormatIds.cprnumber;
			auth.MakeCertificate = new CertificateMaker(GetCertificate);

			// Conditions
			//			card.CardLifeTime            = CardLifeTimeType.Hours8;

			// Authentication (STS)

			// Attributes
			data.IDCardType = "user";
			data.AuthenticationLevel = 3;
			data.UserCivilRegistrationNumber = "2207712801";
//			data.UserCivilRegistrationNumber = "1111111118";
			data.UserGivenName = "Bent";
			data.UserSurName = "Hansen";
			data.UserEmailAddress = "bent@hansen.dk";
			data.UserRole = "SEI User";
			data.UserOccupation = "?";

			// System log
			data.ITSystemName = "SEI Client";
			data.CareProviderID = "12070918";
			data.CareProviderIDFormat = "medcom:cvrnumber";
			data.CareProviderName = "SST";

			//card.IDCardData.IDCardID = "urn:uuid:4C63D7C9-96BA-4022-BA43-ED862E10E2E3";

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
			return SoapFilterResult.Continue;
		}
	}
}
