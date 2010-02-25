using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Security.Cryptography.Xml;
using Microsoft.Web.Services.Security.X509;
using Microsoft.Web.Services.Security;

//using System;
//using System.Security.Cryptography;
//using System.Security.Cryptography.Xml;
//using System.Security.Cryptography.X509Certificates;
using System.Text;
//using System.Xml;

namespace SignXML
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonSign;
		private System.Windows.Forms.TextBox textInputFile;
		private System.Windows.Forms.TextBox textOutputFile;
		private System.Windows.Forms.Button buttonInputFile;
		private System.Windows.Forms.Button buttonOutputFile;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private dk.hob.OCESCertificates.CertificateList certList;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textDataID;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textSerial;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.certList = new dk.hob.OCESCertificates.CertificateList();
			this.textInputFile = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonInputFile = new System.Windows.Forms.Button();
			this.buttonOutputFile = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textOutputFile = new System.Windows.Forms.TextBox();
			this.buttonSign = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.label3 = new System.Windows.Forms.Label();
			this.textDataID = new System.Windows.Forms.TextBox();
			this.textSerial = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// certList
			// 
			this.certList.Location = new System.Drawing.Point(8, 8);
			this.certList.Name = "certList";
			this.certList.Size = new System.Drawing.Size(336, 112);
			this.certList.TabIndex = 0;
			this.certList.SelectedCertificateChanged += new System.EventHandler(this.certList_SelectedCertificateChanged);
			// 
			// textInputFile
			// 
			this.textInputFile.Location = new System.Drawing.Point(64, 160);
			this.textInputFile.Name = "textInputFile";
			this.textInputFile.Size = new System.Drawing.Size(256, 20);
			this.textInputFile.TabIndex = 1;
			this.textInputFile.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 160);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "Input:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonInputFile
			// 
			this.buttonInputFile.Location = new System.Drawing.Point(320, 160);
			this.buttonInputFile.Name = "buttonInputFile";
			this.buttonInputFile.Size = new System.Drawing.Size(24, 20);
			this.buttonInputFile.TabIndex = 3;
			this.buttonInputFile.Text = "…";
			this.buttonInputFile.Click += new System.EventHandler(this.buttonInputFile_Click);
			// 
			// buttonOutputFile
			// 
			this.buttonOutputFile.Location = new System.Drawing.Point(320, 184);
			this.buttonOutputFile.Name = "buttonOutputFile";
			this.buttonOutputFile.Size = new System.Drawing.Size(24, 20);
			this.buttonOutputFile.TabIndex = 6;
			this.buttonOutputFile.Text = "…";
			this.buttonOutputFile.Click += new System.EventHandler(this.buttonOutputFile_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 184);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 20);
			this.label2.TabIndex = 5;
			this.label2.Text = "Output:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textOutputFile
			// 
			this.textOutputFile.Location = new System.Drawing.Point(64, 184);
			this.textOutputFile.Name = "textOutputFile";
			this.textOutputFile.Size = new System.Drawing.Size(256, 20);
			this.textOutputFile.TabIndex = 4;
			this.textOutputFile.Text = "";
			// 
			// buttonSign
			// 
			this.buttonSign.Location = new System.Drawing.Point(8, 248);
			this.buttonSign.Name = "buttonSign";
			this.buttonSign.Size = new System.Drawing.Size(64, 24);
			this.buttonSign.TabIndex = 8;
			this.buttonSign.Text = "Sign";
			this.buttonSign.Click += new System.EventHandler(this.buttonSign_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "xml";
			this.openFileDialog.Filter = "XML files (*.xml)|*.xml|All files|*.*";
			this.openFileDialog.Title = "Select XML file to sign";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "xml";
			this.saveFileDialog.FileName = "doc1";
			this.saveFileDialog.Filter = "XML files (*.xml)|*.xml|All files|*.*";
			this.saveFileDialog.Title = "Select XML file to save";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 208);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 20);
			this.label3.TabIndex = 9;
			this.label3.Text = "Data ID";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textDataID
			// 
			this.textDataID.Location = new System.Drawing.Point(64, 208);
			this.textDataID.Name = "textDataID";
			this.textDataID.Size = new System.Drawing.Size(144, 20);
			this.textDataID.TabIndex = 10;
			this.textDataID.Text = "DataID";
			// 
			// textSerial
			// 
			this.textSerial.Location = new System.Drawing.Point(64, 128);
			this.textSerial.Name = "textSerial";
			this.textSerial.ReadOnly = true;
			this.textSerial.Size = new System.Drawing.Size(280, 20);
			this.textSerial.TabIndex = 11;
			this.textSerial.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 128);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 24);
			this.label4.TabIndex = 12;
			this.label4.Text = "Serial";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// MainForm
			// 
			this.AcceptButton = this.buttonSign;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(352, 279);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label4,
																		  this.textSerial,
																		  this.textDataID,
																		  this.label3,
																		  this.buttonSign,
																		  this.buttonOutputFile,
																		  this.label2,
																		  this.textOutputFile,
																		  this.buttonInputFile,
																		  this.label1,
																		  this.textInputFile,
																		  this.certList});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Sign XML File";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void buttonInputFile_Click(object sender, System.EventArgs e)
		{
			if( openFileDialog.ShowDialog()==DialogResult.OK )
				textInputFile.Text=openFileDialog.FileName;
		}

		private void buttonSign_Click(object sender, System.EventArgs e)
		{
			if( certList.SelectedCertificate==null )
			{
				MessageBox.Show( this, "No certificate selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
				return;
			}

			X509Certificate cert=certList.SelectedCertificate.ToX509();

			XmlDocument doc=new XmlDocument();
			doc.PreserveWhitespace=true;
			try
			{
				XmlTextReader reader=new XmlTextReader( textInputFile.Text );
				doc.Load( reader );
			}
			catch( Exception )
			{
				MessageBox.Show( this, "Couldn't load file \""+textInputFile.Text+"\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
				return;
			}

			XmlDocument ndoc=dk.hob.OCESCertificates.SignXML.Sign( doc, textDataID.Text, cert );

			try
			{
				ndoc.Save( textOutputFile.Text );
			}
			catch( Exception )
			{
				MessageBox.Show( this, "Couldn't save file \""+textOutputFile.Text+"\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
				return;
			}

			MessageBox.Show( this, "File \""+textOutputFile.Text+"\" saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information );
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void buttonOutputFile_Click(object sender, System.EventArgs e)
		{
			if( saveFileDialog.ShowDialog()==DialogResult.OK )
				textOutputFile.Text=saveFileDialog.FileName;
		}

		private void certList_SelectedCertificateChanged(object sender, System.EventArgs e)
		{
			if( certList.SelectedCertificate!=null )
				textSerial.Text=certList.SelectedCertificate.GetSerial();
			else
				textSerial.Text="";
		}
	}
	
}

