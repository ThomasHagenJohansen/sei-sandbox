using System;

namespace Trans
{
	public class MapTest
	{
		[STAThread]
		public static void Main()
		{
			CommonCancerSchema ds=new CommonCancerSchema();

			Trans.CommonCancerSchema.CommonCancerRow parent=ds.CommonCancer.NewCommonCancerRow();
			parent.uiSkemaId          =Guid.NewGuid();
			parent.txCprNr            ="2711732503";
			parent.dtIncidensdato     =DateTime.Now;
			parent.txDiagnosekode     ="DC851";

			ds.CommonCancer.AddCommonCancerRow( parent );
			//ds.Udbredelse.AddUdbredelseRow( "AnnArbor", "AZCC99", parent );
			ds.TNM.AddTNMRow( "T1", "N1", "M1", parent );

			/*
			parent.txUdbredelsesformat="TNM";
			*/

			ds.txMakroGrundlag.AddtxMakroGrundlagRow( "AZCK0", parent );
			ds.txMakroGrundlag.AddtxMakroGrundlagRow( "AZCK1", parent );
			ds.txMikroGrundlag.AddtxMikroGrundlagRow( "AZCL2", parent );

			ds.AcceptChanges();
			ds.WriteXml( @"c:\hmm.xml" );
		}
	}
}
