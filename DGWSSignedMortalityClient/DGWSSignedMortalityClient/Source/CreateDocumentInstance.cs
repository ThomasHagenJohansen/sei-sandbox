using System;
using DGWSSignedMortalityClient.sei_frontend;

namespace DGWSSignedMortalityClient
{
	partial class Program
	{
		private SignedMortalityReasonType CreateInstancePart1()
		{
			SignedMortalityReasonType reason = new SignedMortalityReasonType();

			reason.MortalityReason = new MortalityReasonType();
			reason.MortalityReason.SchemaID = Guid.NewGuid().ToString();
			reason.MortalityReason.PersonIdentifier = new PersonIdentifierType();
			reason.MortalityReason.PersonIdentifier.Item = "1312814435";

			HospitalDoctorType doc = new HospitalDoctorType();
			doc.Part1 = CreatePart1();
			doc.Part1.DoctorFunction = DoctorFunctionType.HospitalDoctor;

			reason.Part1Signature = new SignedMortalityReasonTypePart1Signature();
			reason.MortalityReason.CertifyingDoctor = new CertifyingDoctorType();
			reason.MortalityReason.CertifyingDoctor.Item = doc;

			return reason;
		}

		private SignedMortalityReasonType CreateInstancePart2()
		{
			SignedMortalityReasonType reason = new SignedMortalityReasonType();

			reason.MortalityReason = new MortalityReasonType();
			reason.MortalityReason.SchemaID = Guid.NewGuid().ToString();
			reason.MortalityReason.PersonIdentifier = new PersonIdentifierType();
			reason.MortalityReason.PersonIdentifier.Item = "1312814435";

			PersonalDoctorType doc = new PersonalDoctorType();
            doc.Part2 = CreatePart2();
			doc.Part2.DoctorFunction = DoctorFunctionType.PersonalGP;

			reason.Part2Signature = new SignedMortalityReasonTypePart2Signature();
			reason.MortalityReason.CertifyingDoctor = new CertifyingDoctorType();
			reason.MortalityReason.CertifyingDoctor.Item = doc;

			return reason;
		}

		private SignedMortalityReasonType CreateInstancePart1AndPart2ForSameCertificate()
		{
			SignedMortalityReasonType reason = new SignedMortalityReasonType();

			reason.MortalityReason = new MortalityReasonType();
			reason.MortalityReason.SchemaID = Guid.NewGuid().ToString();
			reason.MortalityReason.PersonIdentifier = new PersonIdentifierType();
			reason.MortalityReason.PersonIdentifier.Item = "1312814435";

			PersonalDoctorType doc = new PersonalDoctorType();
			doc.Part1 = CreatePart1();
			doc.Part2 = CreatePart2();
			doc.Part1.DoctorFunction = DoctorFunctionType.PersonalGP;
			doc.Part2.DoctorFunction = DoctorFunctionType.PersonalGP;

			reason.AllSignature = new SignedMortalityReasonTypeAllSignature();
			reason.MortalityReason.CertifyingDoctor = new CertifyingDoctorType();
			reason.MortalityReason.CertifyingDoctor.Item = doc;

			return reason;
		}

		private SignedMortalityReasonType CreateInstancePart1AndPart2ForDifferentCertificates()
		{
			SignedMortalityReasonType reason = new SignedMortalityReasonType();

			reason.MortalityReason = new MortalityReasonType();
			reason.MortalityReason.SchemaID = Guid.NewGuid().ToString();
			reason.MortalityReason.PersonIdentifier = new PersonIdentifierType();
			reason.MortalityReason.PersonIdentifier.Item = "1312814435";

			PersonalDoctorType doc = new PersonalDoctorType();
			doc.Part1 = CreatePart1();
			doc.Part2 = CreatePart2();
			doc.Part1.DoctorFunction = DoctorFunctionType.PersonalGP;
			doc.Part2.DoctorFunction = DoctorFunctionType.PersonalGP;

			reason.Part1Signature = new SignedMortalityReasonTypePart1Signature();
			reason.Part2Signature = new SignedMortalityReasonTypePart2Signature();
			reason.MortalityReason.CertifyingDoctor = new CertifyingDoctorType();
			reason.MortalityReason.CertifyingDoctor.Item = doc;

			return reason;
		}

		private Part1Type CreatePart1()
		{
			Part1Type part1 = new Part1Type();
			part1.Created = DateTime.Now;
			part1.HealthInsuranceNumber = "1";
			part1.BornDead = false;
			part1.InquestDate = DateTime.Now;
			part1.Implants = ImplantsType.No;
			part1.PoliceContact = false;
			part1.PoliceStationName = "Station1";
			part1.SignOfDeath = SignOfDeathType.Rigor;

			TimeOfDeathType death = new TimeOfDeathType();
			death.DateTimeOrDate = new DateTimeOrDateType();
			death.DateTimeOrDate.Item = DateTime.Now;

			death.DeathSite = new DeathSiteType();
			death.DeathSite.Item = ResidenceType.Home;

			part1.Item = death;

			return part1;
		}

		private Part2Type CreatePart2()
		{
			Part2Type part2 = new Part2Type();
			part2.Created = DateTime.Now;
			part2.WayOfDeath = WayOfDeathType.natural;
			part2.PrimaryCauseOfDeath = new CauseOfDeathType[] { new CauseOfDeathType() };
			part2.PrimaryCauseOfDeath[0].CauseOfDeath = "A000";
			part2.PrimaryCauseOfDeath[0].Version = 0;
			part2.PrimaryCauseOfDeath[0].DurationBeforeDeath = "P0Y0M0DT2H0M0S";
			part2.SecondaryCauseOfDeath = new CauseOfDeathType[0];
			part2.Autopsy = new AutopsyType();
			part2.Autopsy.Item = NoAutopsyTextReasonType.NoAutopsy;

			return part2;
		}
	}
}
