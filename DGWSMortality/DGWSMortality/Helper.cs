using System;
using System.Collections.Generic;
using System.Text;
using DGWSMortality.MortalityRegistrationService;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Security.Cryptography;

namespace DGWSMortality
{
	public static class Helper
	{
		public static MortalityReasonType CreateTestDocument_Part2()
		{
			MortalityReasonType mortalityReason   = new MortalityReasonType();
			//mortalityReason.SchemaID              = "1D817F0A-0EC6-42ad-90B5-BAA6ADFFE858";
			mortalityReason.SchemaID              = Guid.Empty.ToString();
			mortalityReason.PersonIdentifier      = new PersonIdentifierType();
			//mortalityReason.PersonIdentifier.id   = "";
			mortalityReason.PersonIdentifier.Item = "0703614116";


			mortalityReason.Part1And2          = new Part1And2Type();
			mortalityReason.Part1And2.Items    = new Object[2];
			//mortalityReason.Part1And2.Items[0] = new Part1Type();
			//Part1Type part1                    = (Part1Type)mortalityReason.Part1And2.Items[0];
			mortalityReason.Part1And2.Items[1] = new Part2Type();
			Part2Type part2                    = (Part2Type)mortalityReason.Part1And2.Items[1];
			part2.Created                      = DateTime.Now;
			
			part2.PrimaryCauseOfDeath = new CauseOfDeathType[1];
			part2.PrimaryCauseOfDeath[0] = new CauseOfDeathType();
			part2.PrimaryCauseOfDeath[0].CauseOfDeath        = "K420";
			part2.PrimaryCauseOfDeath[0].DurationBeforeDeath = "P0Y0M10DT15H";
			part2.PrimaryCauseOfDeath[0].Version             = 1;
			//part2.PrimaryCauseOfDeath[2] = new CauseOfDeathType();
			//part2.PrimaryCauseOfDeath[2].CauseOfDeath = "TEST1234";


			part2.SecondaryCauseOfDeath = new CauseOfDeathType[1];
			part2.SecondaryCauseOfDeath[0] = new CauseOfDeathType();
			part2.SecondaryCauseOfDeath[0].CauseOfDeath        = "K421";
			part2.SecondaryCauseOfDeath[0].DurationBeforeDeath = "P0Y0M10DT14H";
			part2.SecondaryCauseOfDeath[0].Version             = 1;

			part2.ProductnameATCCode = new ATCElementType[3];
			part2.ProductnameATCCode[0] = new ATCElementType();
			part2.ProductnameATCCode[0].ATCCode = "A10AB01";
			part2.ProductnameATCCode[0].ATCText = "Actrapid Novolet";
			//part2.ProductnameATCCode[1] = new ATCElementType();
			//part2.ProductnameATCCode[1].ATCCode = "V04CL";
			//part2.ProductnameATCCode[1].ATCText = "Alk (231) Secale Cereale";
			part2.ProductnameATCCode[2] = new ATCElementType();
			part2.ProductnameATCCode[2].ATCCode = "V04CL";
			part2.ProductnameATCCode[2].ATCText = "Alk 561 Svinebørster";

			part2.DeathNonNatural = new DeathNonNaturalType();
			part2.DeathNonNatural.PlaceOfEvent = PlaceOfEventType.Amusementandparkarea;

			part2.Autopsy = new AutopsyType();
			part2.Autopsy.Item = NoAutopsyTextReasonType.ProhibitedAutopsy;
			

			



			//part1.Created = DateTime.Parse("2009-01-24");
			//part1.HealthInsuranceNumber = "1";
			//part1.BornDead = false;
			//part1.BornDeadData = null;

			//part1.Item = new TimeOfDeathType();
			//TimeOfDeathType tod = (TimeOfDeathType)part1.Item;
			//tod.DateTimeOrDate = new DateTimeOrDateType();
			//tod.DateTimeOrDate.ItemElementName = ItemChoiceType.Date;
			//tod.DateTimeOrDate.Item = DateTime.Parse("2009-01-22");
			//tod.DeathSite = new DeathSiteType();
			//DeathSiteType deathSite = (DeathSiteType)tod.DeathSite;
			//deathSite.Item = new HospitalIdentifierType();
			//HospitalIdentifierType hospID = (HospitalIdentifierType)deathSite.Item;
			//hospID.HospitalClasifikation = "9999";//"Udenlandsk Sygehus";
			//hospID.HospitalDepartment = "999";//"Færøsk sygehusafdeling";

			//part1.SignOfDeath = SignOfDeathType.Rigor;			
			//part1.InquestDate = DateTime.Parse("2009-01-23");
			//part1.PoliceContact = false;
			//part1.Implants = ImplantsType.No;
			//part1.AlternativeDoctor = null;
			//part1.PoliceStationName = String.Empty;

			return mortalityReason;
		}

		public static MortalityReasonType CreateTestDocument_Part1And2()
		{
			MortalityReasonType mortalityReason   = new MortalityReasonType();
			//mortalityReason.SchemaID              = "1D817F0A-0EC6-42ad-90B5-BAA6ADFFE858";
			mortalityReason.SchemaID              = Guid.Empty.ToString();
			mortalityReason.PersonIdentifier      = new PersonIdentifierType();
			mortalityReason.PersonIdentifier.id   = "cprid";
			mortalityReason.PersonIdentifier.Item = "0703614116";

			mortalityReason.Part1And2       = new Part1And2Type();
			mortalityReason.Part1And2.Items = new Object[2];

			mortalityReason.Part1And2.Items[0] = new Part1Type();
			Part1Type part1                    = (Part1Type)mortalityReason.Part1And2.Items[0];
			part1.Created                      = DateTime.Now;			

			part1.AlternativeDoctor                                        = new AlternativeDoctorType();
			part1.AlternativeDoctor.DoctorAddress                          = new AddressPostalType();
			part1.AlternativeDoctor.DoctorAddress.StreetName               = "Test_Street";
			part1.AlternativeDoctor.DoctorAddress.StreetBuildingIdentifier = "45A";
			part1.AlternativeDoctor.DoctorAddress.PostCodeIdentifier       = "1660";
			part1.AlternativeDoctor.DoctorAddress.DistrictName             = "Test_District";
			part1.AlternativeDoctor.PersonNameStructure                    = new PersonNameStructureType();
			part1.AlternativeDoctor.PersonNameStructure.PersonGivenName    = "Test_Fornavn";
			part1.AlternativeDoctor.PersonNameStructure.PersonMiddleName   = "Test_Mellemnavn";
			part1.AlternativeDoctor.PersonNameStructure.PersonSurnameName  = "Test_Efternavn";

			part1.BornDead     = false;
			part1.BornDeadData = null;//Test om script fanger den her. (hvis den skal fanges!?) - update: Script fanger fejl når BornDead = true.

			part1.HealthInsuranceNumber = "1";
			part1.Implants              = ImplantsType.Unknown;
			part1.InquestDate           = DateTime.Now.AddDays(-1);

			part1.Item                                     = new TimeOfDeathType();
			TimeOfDeathType timeOfDeathType                = (TimeOfDeathType)part1.Item;
			timeOfDeathType.DateTimeOrDate                 = new DateTimeOrDateType();
			timeOfDeathType.DateTimeOrDate.ItemElementName = ItemChoiceType.DateTime;
			timeOfDeathType.DateTimeOrDate.Item            = DateTime.Now.AddDays(-2);
			timeOfDeathType.DeathSite      = new DeathSiteType();
			timeOfDeathType.DeathSite.Item = new ResidenceType();
			timeOfDeathType.DeathSite.Item = ResidenceType.NurseryHome;

			//part1.Page2WillFollow   = false;
			part1.PoliceContact     = true;
			part1.PoliceStationName = "";
			part1.SignOfDeath       = SignOfDeathType.MaceratioAndCadaverositasAndLivoresAndRigor;

			mortalityReason.Part1And2.Items[1] = new Part2Type();
			Part2Type part2                    = (Part2Type)mortalityReason.Part1And2.Items[1];
			part2.Created                      = DateTime.Now;			

			part2.PrimaryCauseOfDeath                        = new CauseOfDeathType[3];
			part2.PrimaryCauseOfDeath[0]                     = new CauseOfDeathType();
			part2.PrimaryCauseOfDeath[0].CauseOfDeath        = "K420";
			//part2.PrimaryCauseOfDeath[0].DurationBeforeDeath = "P0Y0M0DT15H0";
			//part2.PrimaryCauseOfDeath[0].DurationBeforeDeath = "P0Y0M0DT13H";
			part2.PrimaryCauseOfDeath[0].DurationBeforeDeath = "PT13H";
			part2.PrimaryCauseOfDeath[0].Version             = 1;
			part2.PrimaryCauseOfDeath[2]                     = new CauseOfDeathType();
			part2.PrimaryCauseOfDeath[2].CauseOfDeath        = "K421";
			//part2.PrimaryCauseOfDeath[2].DurationBeforeDeath = "P0Y0M5DT15H";
			part2.PrimaryCauseOfDeath[2].DurationBeforeDeath = "P0Y0M0DT15H";
			part2.PrimaryCauseOfDeath[2].Version             = 1;

			part2.SecondaryCauseOfDeath                        = new CauseOfDeathType[1];
			part2.SecondaryCauseOfDeath[0]                     = new CauseOfDeathType();
			part2.SecondaryCauseOfDeath[0].CauseOfDeath        = "K429";
			//part2.SecondaryCauseOfDeath[0].DurationBeforeDeath = "P0Y0M0DT14H";
			part2.SecondaryCauseOfDeath[0].DurationBeforeDeath = "PT14H";
			part2.SecondaryCauseOfDeath[0].Version             = 1;

			part2.ProductnameATCCode = new ATCElementType[3];
			part2.ProductnameATCCode[0] = new ATCElementType();
			part2.ProductnameATCCode[0].ATCCode = "A10AB01";
			part2.ProductnameATCCode[0].ATCText = "Actrapid Novolet";
			part2.ProductnameATCCode[1] = new ATCElementType();
			part2.ProductnameATCCode[1].ATCCode = "V04CL";
			part2.ProductnameATCCode[1].ATCText = "Alk (231) Secale Cereale";
			//part2.ProductnameATCCode[2] = new ATCElementType();
			//part2.ProductnameATCCode[2].ATCCode = "V04CL";
			//part2.ProductnameATCCode[2].ATCText = "Alk 561 Svinebørster";

			part2.DeathNonNatural              = new DeathNonNaturalType();
			part2.DeathNonNatural.PlaceOfEvent = PlaceOfEventType.Amusementandparkarea;

			part2.Autopsy = new AutopsyType();
			part2.Autopsy.Item = NoAutopsyTextReasonType.ProhibitedAutopsy;
			
			return mortalityReason;
		}
	}
}