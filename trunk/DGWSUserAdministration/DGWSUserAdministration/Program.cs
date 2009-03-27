using System;
using System.IO;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services.Protocols;
using DgwsWse.HeaderTypes;
using Medcom.DgwsWse;
using Microsoft.Web.Services3.Design;
using DGWSAssertion=Medcom.DgwsWse.DGWSAssertion;
using MessageSignAssertion=Medcom.DgwsWse.MessageSignAssertion;
using System.Xml.Serialization;
using DGWSUserAdministration.UserAdministration;


namespace DGWSUserAdministration
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				String path = Path.Combine(Directory.GetCurrentDirectory(), "TestMOCES1.pfx");
				X509Certificate2 moces = new X509Certificate2(path, "Test1234");

				UserAuthorisationType userAuthorisation = new UserAuthorisationType();
				userAuthorisation.SEIUser = new UserAuthorisationTypeSEIUser();
				userAuthorisation.SEIUser.FullName = "Bent Hansen";
				userAuthorisation.SEIUser.X509Certificate = moces.RawData;
				userAuthorisation.SEIUser.PersonCivilRegistrationIdentifier = "0000000000";
				userAuthorisation.SEIUser.Item = "bent@hansen.dk";
				userAuthorisation.SEIUser.ItemElementName = ItemChoiceType.Email;

				serialize(userAuthorisation);

				UserAdministration.UserAdministration service = new UserAdministration.UserAdministration();
				service.SetPolicy(new DGWSPolicy(moces));

				bool b = service.AuthoriseUser(userAuthorisation);

				System.Diagnostics.Debug.WriteLine(b);
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

		private static void serialize(UserAuthorisationType type)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(UserAuthorisationType));

			StringBuilder sb = new StringBuilder();

			using (StringWriter writer = new StringWriter(sb))
			{
				serializer.Serialize(writer, type);
			}
			String str = sb.ToString();
		}



		public class DGWSPolicy : Policy
		{
			private readonly X509Certificate2 moces;

			public DGWSPolicy(X509Certificate2 moces)
			{
				this.moces = moces;

				Assertions.Add(new RequireActionHeaderAssertion());		// WSE policy

				DGWSAssertion dgwsAss = new DGWSAssertion();
				dgwsAss.Card = GetIDCard();
				Assertions.Add(dgwsAss);

				Assertions.Add(new AddressingConverterAssertion());

				MessageSignAssertion msgAss = new MessageSignAssertion();
				msgAss.certificate = moces;
				//msgAss.acceptedcartificates = new[] { "CVR:25767535-UID:1100080130597 + CN=TDC TOTALLØSNINGER A/S - TDC Test" };
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
				data.UserCivilRegistrationNumber = "1111111118";
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

				return card;
			}

			private X509Certificate2 GetCertificate()
			{
				return moces;
			}
		}
	}
}