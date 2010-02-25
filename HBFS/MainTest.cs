using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using dk.hob.Data.Transformation;
using dk.hob.IO.HBFS;

namespace HBFS
{
	/// <summary>
	/// Summary description for MainTest.
	/// </summary>
	public class MainTest
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			TwoWayTransformer trans = new TwoWayTransformer(new CompressionTransformer(), new DecompressionTransformer());
//			Volume vol = new Volume("D:\\TestVol.bin", trans, 512, 128);
			Volume vol = new Volume("D:\\TestVol.bin", trans);
//			Volume vol = new Volume("D:\\TestVol.bin");
			vol.OpenVolume(FileAccess.ReadWrite);

			try
			{
//				FileData fd = vol.OpenFile("≈≈≈≈≈≈≈≈hhhh ja!", FileMode.Create);
//				FileData fd = vol.OpenFile("≈≈≈≈≈≈≈≈hhhh ja!", FileMode.Open);
//				FileData fd = vol.OpenFile("Tom_fil", FileMode.Create);
//				FileData fd = vol.OpenFile("Svin_≈_Filen", FileMode.Create);
//				FileData fd = vol.OpenFile("Svin_≈_Filen", FileMode.Open);
//				FileData fd = vol.OpenFile("Streamet fil (hihi)", FileMode.Create);
//				FileData fd = vol.OpenFile("Streamet fil (hihi)", FileMode.Open);
//				FileData fd = vol.OpenFile("Append fil", FileMode.Append);
//				FileData fd = vol.OpenFile("Append fil", FileMode.Open);


//				using (FileStream s = new FileStream("d:\\stuff\\floppyimage152.zip", FileMode.Open))
/*				using (FileStream s = new FileStream("d:\\utilities\\windiff\\windiff.exe", FileMode.Open))
				{
					fd.WriteData(s);
				}
*/

/*				using (FileStream s = new FileStream("d:\\outfile", FileMode.Create))
				{
					fd.ReadData(s);
				}
*/
//				fd.WriteUInt32(0x88558855);
//				fd.WriteString("En lille hilsen");
//				fd.WriteString("til det ganske land i ¯-rÂdet");

//				System.Diagnostics.Debug.WriteLine(fd.ReadString());
//				System.Diagnostics.Debug.WriteLine(fd.ReadString());

/*				for (int i = 0; i < 600; i++)
					fd.WriteInt32(i);
*/
/*				for (int i = 0; i < 550; i++)
					fd.ReadInt32();

				fd.WriteInt32(0x12345678);
*/
/*				while (!fd.EOF)
				{
					System.Diagnostics.Debug.WriteLine(fd.ReadInt32());
				}
*/
/*				fd.WriteMagic(0x12345678);
				fd.WriteMagic(0xAABBCCDD);
				fd.WriteMagic(0xFFFFFFDA);
*/

/*				UInt32 magic = fd.ReadMagic();
				magic = fd.ReadMagic();
				magic = fd.ReadMagic();

				magic = fd.ReadMagic();
				magic = fd.ReadMagic();
				magic = fd.ReadMagic();
*/
//				vol.CloseFile(fd);

/*				for (int i = 0; i < 1010; i++)
				{
					FileData fd = vol.OpenFile("A" + i, FileMode.CreateNew);
					fd.WriteInt32(42);
					vol.CloseFile(fd);
				}
*/
				for (int i = 1; i < 1010; i += 2)
				{
					vol.DeleteFile("A" + i);
				}

//				vol.RenameFile("Svin_≈_Filen2", "Masser af tal");
//				vol.DeleteFile("Masser af tal");

/*				FileMetaData[] fileInfo = vol.GetFiles("");
				foreach (FileMetaData info in fileInfo)
				{
					System.Diagnostics.Debug.WriteLine("Filename: " + info.Name);
					System.Diagnostics.Debug.WriteLine("Created: " + info.Created);
					System.Diagnostics.Debug.WriteLine("Modified: " + info.LastModified);
					System.Diagnostics.Debug.WriteLine("Length: " + info.FileLength);
					System.Diagnostics.Debug.WriteLine("Encoded length: " + info.EncodedFileLength);
					System.Diagnostics.Debug.WriteLine("-----------------------------------");
				}
*/			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			finally
			{
				vol.CloseVolume();
			}
		}
	}
}
