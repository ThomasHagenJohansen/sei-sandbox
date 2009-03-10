using System;
using ConsoleApplication5.sei_frontend;

namespace DGWSSignedMortalityClient
{
	partial class Program
	{
		SignedMortalityReasonType CreateInstancePart1()
		{
			SignedMortalityReasonType reason = new SignedMortalityReasonType();

			reason.MortalityReason = new MortalityReasonType();
			reason.MortalityReason.SchemaID = Guid.NewGuid().ToString();
			reason.MortalityReason.PersonIdentifier = new PersonIdentifierType();
			reason.MortalityReason.PersonIdentifier.Item = "1312814435";

			HospitalDoctorType doc = new HospitalDoctorType();
			Part1Type part1 = new Part1Type();
			//part1.id = "Part1";
			part1.Created = DateTime.Now;
			part1.HealthInsuranceNumber = "1";
			part1.BornDead = false;
			part1.InquestDate = DateTime.Now;
			part1.Implants = ImplantsType.No;
			part1.PoliceContact = false;
			part1.PoliceStationName = "Station1";
			part1.SignOfDeath = SignOfDeathType.Rigor;
			doc.Part1 = part1;
			reason.Part1Signature = new SignedMortalityReasonTypePart1Signature();
			reason.MortalityReason.CertifyingDoctor = new CertifyingDoctorType();
			reason.MortalityReason.CertifyingDoctor.Item = doc;
			return (reason);
		}

		SignedMortalityReasonType CreateInstancePart2()
		{
			SignedMortalityReasonType reason = new SignedMortalityReasonType();

			reason.MortalityReason = new MortalityReasonType();
			reason.MortalityReason.SchemaID = Guid.NewGuid().ToString();
			reason.MortalityReason.PersonIdentifier = new PersonIdentifierType();
			reason.MortalityReason.PersonIdentifier.Item = "1312814435";
			PersonalDoctorType doc = new PersonalDoctorType();

			Part2Type part2 = new Part2Type();
			part2.Created = DateTime.Now;
			doc.Part2 = part2;
			reason.Part2Signature = new SignedMortalityReasonTypePart2Signature();
			reason.MortalityReason.CertifyingDoctor = new CertifyingDoctorType();
			reason.MortalityReason.CertifyingDoctor.Item = doc;
			return (reason);
		}

		SignedMortalityReasonType CreateInstancePart1AndPart2ForSameCertificate()
		{
			SignedMortalityReasonType reason = new SignedMortalityReasonType();

			reason.MortalityReason = new MortalityReasonType();
			reason.MortalityReason.SchemaID = Guid.NewGuid().ToString();
			reason.MortalityReason.PersonIdentifier = new PersonIdentifierType();
			reason.MortalityReason.PersonIdentifier.Item = "1312814435";
			PersonalDoctorType doc = new PersonalDoctorType();

			Part1Type part1 = new Part1Type();
			Part2Type part2 = new Part2Type();
			part1.Created = DateTime.Now;
			part2.Created = DateTime.Now;
			doc.Part1 = part1;
			doc.Part2 = part2;
			reason.AllSignature = new SignedMortalityReasonTypeAllSignature();
			reason.MortalityReason.CertifyingDoctor = new CertifyingDoctorType();
			reason.MortalityReason.CertifyingDoctor.Item = doc;
			return (reason);
		}

		SignedMortalityReasonType CreateInstancePart1AndPart2ForDifferentCertificates()
		{
			SignedMortalityReasonType reason = new SignedMortalityReasonType();

			reason.MortalityReason = new MortalityReasonType();
			reason.MortalityReason.SchemaID = Guid.NewGuid().ToString();
			reason.MortalityReason.PersonIdentifier = new PersonIdentifierType();
			reason.MortalityReason.PersonIdentifier.Item = "1312814435";
			PersonalDoctorType doc = new PersonalDoctorType();

			Part1Type part1 = new Part1Type();
			Part2Type part2 = new Part2Type();
			part1.Created = DateTime.Now;
			part2.Created = DateTime.Now;
			doc.Part1 = part1;
			doc.Part2 = part2;
			reason.Part1Signature = new SignedMortalityReasonTypePart1Signature();
			reason.Part2Signature = new SignedMortalityReasonTypePart2Signature();
			reason.MortalityReason.CertifyingDoctor = new CertifyingDoctorType();
			reason.MortalityReason.CertifyingDoctor.Item = doc;
			return (reason);
		}
	}
}