using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;

namespace MyNotifier
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader Start;
		private System.Windows.Forms.ColumnHeader Slut;
		private System.Windows.Forms.ColumnHeader Info;
		private Timer timer;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private ListView lwCur;

		private int iNdxCurItem;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			timer = new Timer();
			timer.Tick += new EventHandler(TimerEventHandler);
			timer.Interval = 1000*60*15;//1000*6;//
			timer.Start();
			this.listView1.Items.Clear();
			LoadXml("MyNotifier.xml");
			Closing += new CancelEventHandler(OnClose);
		}

		//------------------------------------------------------------------------------------------
		private void TimerEventHandler(Object myObject, EventArgs myEventArgs)
		{
			Show();
			WindowState = FormWindowState.Normal;
			DateTime dt = DateTime.Now;
			int hour2=0;
			int min2=0;
			int hour = dt.Hour;
			int min = dt.Minute;
			if(hour>=9 && hour <=17)
			{
				if(min<15) min =0;
				else if(min<30) min=15;
				else if(min<45) min=30;
				else min=45;
				if(min==45)
				{
					min2=0;
					hour2=hour+1;
				}
				else
				{
					min2=min+15;
					hour2=hour;
				}
				bool bActivate = false;
				InsertListViewEntry(""+hour+":"+min,""+hour2+":"+min2,"",dt.ToShortDateString()+"\r\n", bActivate);
			}
			Save();
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "bla bla",
																													 "bla 2 item"}, -1);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("bla 2");
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.butClose = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.listView1 = new System.Windows.Forms.ListView();
			this.Start = new System.Windows.Forms.ColumnHeader();
			this.Slut = new System.Windows.Forms.ColumnHeader();
			this.Info = new System.Windows.Forms.ColumnHeader();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.ContextMenu = this.contextMenu1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "Oles Tidsregistrerings Assistent";
			this.notifyIcon1.Visible = true;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1});
			this.contextMenu1.Popup += new System.EventHandler(this.contextMenu1_Popup);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Close";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// butClose
			// 
			this.butClose.Location = new System.Drawing.Point(416, 168);
			this.butClose.Name = "butClose";
			this.butClose.TabIndex = 3;
			this.butClose.Text = "Luk";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// textBox1
			// 
			this.textBox1.AcceptsReturn = true;
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point(240, 40);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(248, 112);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "";
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(240, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Notat";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(200, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "Periode";
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.Start,
																						this.Slut,
																						this.Info});
			this.listView1.FullRowSelect = true;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																					  listViewItem1,
																					  listViewItem2});
			this.listView1.Location = new System.Drawing.Point(8, 40);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(224, 112);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			// 
			// Start
			// 
			this.Start.Text = "Start";
			this.Start.Width = 43;
			// 
			// Slut
			// 
			this.Slut.Text = "Slut";
			this.Slut.Width = 41;
			// 
			// Info
			// 
			this.Info.Text = "Info";
			this.Info.Width = 135;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(328, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 16);
			this.label3.TabIndex = 6;
			this.label3.Text = "Info";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(352, 16);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(136, 20);
			this.textBox2.TabIndex = 1;
			this.textBox2.Text = "";
			this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(288, 168);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(120, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "Hent tekst fra forrige";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(208, 168);
			this.button2.Name = "button2";
			this.button2.TabIndex = 5;
			this.button2.Text = "Vis dag";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(496, 198);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.butClose);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Oles tidsregistrerings assistent";
			this.TopMost = true;
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		//------------------------------------------------------------------------------------------
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		//------------------------------------------------------------------------------------------
		private void contextMenu1_Popup(object sender, System.EventArgs e)
		{
			this.Show();
			this.WindowState = FormWindowState.Normal;
		}

		//------------------------------------------------------------------------------------------
		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		//------------------------------------------------------------------------------------------
		private void butClose_Click(object sender, System.EventArgs e)
		{
			timer.Enabled=true;
			this.WindowState = FormWindowState.Minimized;
			this.Hide();
		}

		//------------------------------------------------------------------------------------------
		private void LoadXml(String strFilename)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(strFilename);
			XmlNodeList nl = doc.DocumentElement.GetElementsByTagName("Entry");
			for(int i=0; i<nl.Count; i++)
			{
				XmlNode node = nl.Item(i);
				AddListViewEntry(
					node.Attributes.GetNamedItem("start").Value,
					node.Attributes.GetNamedItem("end").Value,
					node.Attributes.GetNamedItem("head").Value,
					node.Attributes.GetNamedItem("body").Value,
					false);
			}
		}

		//------------------------------------------------------------------------------------------
		private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			lwCur = (ListView)sender;
			if(lwCur.SelectedItems.Count>0)
			{
				textBox1.Enabled=true;
				textBox1.Text = lwCur.SelectedItems[0].SubItems[3].Text;
				textBox2.Text = lwCur.SelectedItems[0].SubItems[2].Text;
			}
		}

		//------------------------------------------------------------------------------------------
		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
			TextBox tb = (TextBox)sender;
			lwCur.SelectedItems[0].SubItems[3].Text = tb.Text;
		}

		//------------------------------------------------------------------------------------------
		private void textBox2_TextChanged(object sender, System.EventArgs e)
		{
			TextBox tb = (TextBox)sender;
			lwCur.SelectedItems[0].SubItems[2].Text = tb.Text;
		}

		//------------------------------------------------------------------------------------------
		private void AddListViewEntry(string start, string end, string head, string body, bool bActivate)
		{
			ListViewItem item = new ListViewItem(start);
			item.SubItems.Add(end);
			item.SubItems.Add(head);
			item.SubItems.Add(body);
			item = listView1.Items.Add(item);
			item.Selected = true;
		}

		//------------------------------------------------------------------------------------------
		private void InsertListViewEntry(string start, string end, string head, string body, bool bActivate)
		{
			ListViewItem item = new ListViewItem(start);
			item.SubItems.Add(end);
			item.SubItems.Add(head);
			item.SubItems.Add(body);
			item = listView1.Items.Insert(0,item);
			item.Selected = true;
			textBox1.Focus();
			iNdxCurItem=item.Index;
		}


		//------------------------------------------------------------------------------------------
		private void Form1_Load(object sender, System.EventArgs e)
		{
		
		}

		//------------------------------------------------------------------------------------------
		private void OnClose(object sender, CancelEventArgs e)
		{
			this.timer.Stop();
			Save();
			notifyIcon1.Dispose();
		}

		//------------------------------------------------------------------------------------------
		private void Save()
		{
			// Save new xml document
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(doc.CreateElement("root"));
			XmlElement el = doc.DocumentElement;
			for(int i=0; i<listView1.Items.Count; i++)
			{
				ListViewItem item = listView1.Items[i];
				XmlElement entry = doc.CreateElement("Entry");
				el.AppendChild(entry);
				XmlAttribute attr = null;
				atrr = doc.CreateAttribute("start");attr.Value=item.SubItems[0].Text;	entry.Attributes.Append(attr);
				attr = doc.CreateAttribute("end");	attr.Value=item.SubItems[1].Text;	entry.Attributes.Append(attr);
				attr = doc.CreateAttribute("head"); attr.Value=item.SubItems[2].Text;	entry.Attributes.Append(attr);
				attr = doc.CreateAttribute("body"); attr.Value=item.SubItems[3].Text;	entry.Attributes.Append(attr);
			}
			doc.Save("MyNotifier.xml");
		}

		//------------------------------------------------------------------------------------------
		private void button1_Click(object sender, System.EventArgs e)
		{
			if(iNdxCurItem< (listView1.Items.Count-1))
				textBox1.Text = listView1.Items[iNdxCurItem+1].SubItems[3].Text;
		}

	}
}
