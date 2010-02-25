using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using dk.hob.Collections.Specialized;
using dk.hob.Collections.Specialized.CodeListHandling;
using dk.hob.Windows.Forms;
using dk.hob.Windows.Forms.DataGrid;
using dk.hob.Windows.Forms.DataGrid.Wrappers;

namespace DataGrid
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components = null;

		private System.Windows.Forms.DataGrid dataGrid2;
		private System.Data.DataSet dataSet1;
		private System.Data.DataTable dataTable1;
		private System.Data.DataColumn dataColumn1;
		private System.Data.DataColumn dataColumn2;
		private System.Data.DataColumn dataColumn3;
		private System.Data.DataColumn dataColumn4;
		private System.Data.DataColumn dataColumn5;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private dk.hob.Windows.Forms.DataGrid.DataGrid dataGrid1;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn1;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn2;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn3;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn4;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn5;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn6;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn7;
		private dk.hob.Windows.Forms.Binding.HOBBindingComponent hobBindingComponent1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn8;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn9;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn10;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn11;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn12;
		private System.Windows.Forms.ImageList imageList1;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn13;
		private System.Windows.Forms.Button button3;
		private dk.hob.Windows.Forms.DataGrid.DataGrid dataGrid3;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn14;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn15;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn16;
		private dk.hob.Windows.Forms.DataGrid.DataGrid dataGrid4;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn17;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn18;
		private dk.hob.Windows.Forms.DataGrid.DataGridColumn dataGridColumn19;
		private System.Data.DataColumn dataColumn6;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			ComboBoxDropListWrapper ctrl = (ComboBoxDropListWrapper)dataGrid1.Columns[0].ColumnType;
			ctrl.SetItems(new String[] { "Hejsa", "Dav", "Farvel" } );

			ComboBoxWrapper ctrl1 = (ComboBoxWrapper)dataGrid1.Columns[11].ColumnType;
			ctrl1.SetItems(new String[] { "Her er lidt", "at vælge imellem", "Du kan også skrive selv" } );

			DataRow dr = dataTable1.NewRow();
			dr[0] = "En tekst";
			dr[1] = 42;
			dr[2] = DateTime.Now;
			dataTable1.Rows.Add(dr);

			for (int j = 0; j < 10; j++)
			{
				dr = dataTable1.NewRow();
				dataTable1.Rows.Add(dr);
			}

/*			ArrayList list = new ArrayList();
			list.Add(4);
			list.Add(34);
			list.Add(47);
			dataGrid2.DataSource = list;
*/
//			dataGrid3.DataSource = dataTable1;

			dataGrid4.Items.AddRange(new DataGridItemCollection[] {
							new DataGridItemCollection(new DataGridItem[] { new DataGridItem("kafkjfdk") }),
							new DataGridItemCollection(new DataGridItem[] { new DataGridItem("fjsdifjusi") }),
							new DataGridItemCollection(new DataGridItem[] { new DataGridItem("sfksdjfmsdlisl") }) } );
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("4534564");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("etete");
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("fjghjgh");
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("gfhfhfg");
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("hfghf");
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("jghjgh");
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("sdgg");
			System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("trytrytr");
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.dataGrid2 = new System.Windows.Forms.DataGrid();
			this.dataTable1 = new System.Data.DataTable();
			this.dataColumn1 = new System.Data.DataColumn();
			this.dataColumn2 = new System.Data.DataColumn();
			this.dataColumn3 = new System.Data.DataColumn();
			this.dataColumn4 = new System.Data.DataColumn();
			this.dataColumn5 = new System.Data.DataColumn();
			this.dataColumn6 = new System.Data.DataColumn();
			this.dataSet1 = new System.Data.DataSet();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.dataGrid1 = new dk.hob.Windows.Forms.DataGrid.DataGrid();
			this.hobBindingComponent1 = new dk.hob.Windows.Forms.Binding.HOBBindingComponent(this.components);
			this.dataGridColumn1 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.dataGridColumn2 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.dataGridColumn3 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.dataGridColumn4 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.dataGridColumn5 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.dataGridColumn6 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.dataGridColumn7 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.dataGridColumn8 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.dataGridColumn9 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.dataGridColumn10 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.dataGridColumn11 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.dataGridColumn12 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.dataGridColumn13 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.dataGrid3 = new dk.hob.Windows.Forms.DataGrid.DataGrid();
			this.dataGridColumn14 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.dataGridColumn15 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.dataGridColumn16 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.dataGrid4 = new dk.hob.Windows.Forms.DataGrid.DataGrid();
			this.dataGridColumn17 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.dataGridColumn18 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			this.dataGridColumn19 = new dk.hob.Windows.Forms.DataGrid.DataGridColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.hobBindingComponent1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid4)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid2
			// 
			this.dataGrid2.AccessibleName = "DataGrid";
			this.dataGrid2.AccessibleRole = System.Windows.Forms.AccessibleRole.Table;
			this.dataGrid2.CaptionText = ".NET";
			this.dataGrid2.DataMember = "";
			this.dataGrid2.DataSource = this.dataTable1;
			this.dataGrid2.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid2.Location = new System.Drawing.Point(64, 248);
			this.dataGrid2.Name = "dataGrid2";
			this.dataGrid2.Size = new System.Drawing.Size(512, 160);
			this.dataGrid2.TabIndex = 3;
			// 
			// dataTable1
			// 
			this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
																			  this.dataColumn1,
																			  this.dataColumn2,
																			  this.dataColumn3,
																			  this.dataColumn4,
																			  this.dataColumn5,
																			  this.dataColumn6});
			this.dataTable1.TableName = "Table1";
			// 
			// dataColumn1
			// 
			this.dataColumn1.ColumnName = "Column1";
			// 
			// dataColumn2
			// 
			this.dataColumn2.ColumnName = "Column2";
			this.dataColumn2.DataType = typeof(int);
			// 
			// dataColumn3
			// 
			this.dataColumn3.ColumnName = "Column3";
			this.dataColumn3.DataType = typeof(System.DateTime);
			// 
			// dataColumn4
			// 
			this.dataColumn4.ColumnName = "Column4";
			// 
			// dataColumn5
			// 
			this.dataColumn5.ColumnName = "Column5";
			// 
			// dataColumn6
			// 
			this.dataColumn6.ColumnName = "Column6";
			// 
			// dataSet1
			// 
			this.dataSet1.DataSetName = "NewDataSet";
			this.dataSet1.Locale = new System.Globalization.CultureInfo("da-DK");
			this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
																		  this.dataTable1});
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3});
			this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																					  listViewItem1,
																					  listViewItem2,
																					  listViewItem3,
																					  listViewItem4,
																					  listViewItem5,
																					  listViewItem6,
																					  listViewItem7,
																					  listViewItem8});
			this.listView1.Location = new System.Drawing.Point(56, 424);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(304, 97);
			this.listView1.TabIndex = 4;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 99;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Width = 132;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Width = 86;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(0, 16);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(40, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "textBox1";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(0, 264);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(48, 20);
			this.textBox2.TabIndex = 2;
			this.textBox2.Text = "textBox2";
			// 
			// dataGrid1
			// 
			this.dataGrid1.BindingComponent = this.hobBindingComponent1;
			this.dataGrid1.CaptionText = "Dette er en title";
			this.dataGrid1.CellBackColor = System.Drawing.SystemColors.Info;
			this.dataGrid1.CellForeColor = System.Drawing.SystemColors.HotTrack;
			this.dataGrid1.Columns.AddRange(new dk.hob.Windows.Forms.DataGrid.DataGridColumn[] {
																								   this.dataGridColumn1,
																								   this.dataGridColumn2,
																								   this.dataGridColumn3,
																								   this.dataGridColumn4,
																								   this.dataGridColumn5,
																								   this.dataGridColumn6,
																								   this.dataGridColumn7,
																								   this.dataGridColumn8,
																								   this.dataGridColumn9,
																								   this.dataGridColumn10,
																								   this.dataGridColumn11,
																								   this.dataGridColumn12,
																								   this.dataGridColumn13});
			this.dataGrid1.ImageList = this.imageList1;
			this.dataGrid1.Location = new System.Drawing.Point(56, 16);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(896, 216);
			this.dataGrid1.TabIndex = 1;
			this.dataGrid1.ColumnControlInitializing += new dk.hob.Windows.Forms.DataGrid.ColumnControlEventHandler(this.Grid_ColumnControlInitializing);
			this.dataGrid1.RowAdded += new dk.hob.Windows.Forms.DataGrid.RowEventHandler(this.Grid_RowAdded);
			this.dataGrid1.BeginEdit += new dk.hob.Windows.Forms.DataGrid.CellEventHandler(this.Grid_BeginEdit);
			// 
			// hobBindingComponent1
			// 
			this.hobBindingComponent1.HostingControl = this;
			// 
			// dataGridColumn1
			// 
			this.dataGridColumn1.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.ComboBoxDropListWrapper(this.dataGrid1, new string[0], new object[0]);
			this.dataGridColumn1.Label = "ComboDrop";
			this.dataGridColumn1.Width = 78;
			// 
			// dataGridColumn2
			// 
			this.dataGridColumn2.AllowNull = true;
			this.dataGridColumn2.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.TextBoxWrapper(this.dataGrid1, new string[] {
																																		 "MaxLength"}, new object[] {
																																										32767});
			this.dataGridColumn2.Label = "Svin!";
			this.dataGridColumn2.Width = 76;
			// 
			// dataGridColumn3
			// 
			this.dataGridColumn3.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.TextBoxWrapper(this.dataGrid1, new string[] {
																																		 "MaxLength"}, new object[] {
																																										32767});
			this.dataGridColumn3.Label = "Låst";
			this.dataGridColumn3.ReadOnly = true;
			this.dataGridColumn3.Width = 54;
			// 
			// dataGridColumn4
			// 
			this.dataGridColumn4.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.TextBoxWrapper(this.dataGrid1, new string[] {
																																		 "MaxLength"}, new object[] {
																																										32767});
			this.dataGridColumn4.Label = "teterte";
			this.dataGridColumn4.Width = 79;
			// 
			// dataGridColumn5
			// 
			this.dataGridColumn5.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.TextBoxWrapper(this.dataGrid1, new string[] {
																																		 "MaxLength"}, new object[] {
																																										8});
			this.dataGridColumn5.Label = "erttyfjh";
			this.dataGridColumn5.Width = 50;
			// 
			// dataGridColumn6
			// 
			this.dataGridColumn6.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.CheckBoxWrapper(this.dataGrid1, new string[0], new object[0]);
			this.dataGridColumn6.Label = "GU!";
			this.dataGridColumn6.Width = 114;
			// 
			// dataGridColumn7
			// 
			this.dataGridColumn7.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.StringLookupPairWrapper(this.dataGrid1, new string[] {
																																				  "DisplayKeyFirst"}, new object[] {
																																													   false});
			this.dataGridColumn7.Label = "Sygehus";
			this.dataGridColumn7.Width = 171;
			// 
			// dataGridColumn8
			// 
			this.dataGridColumn8.Label = "Sygehuskode";
			this.dataGridColumn8.Width = 79;
			// 
			// dataGridColumn9
			// 
			this.dataGridColumn9.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.CPRWrapper(this.dataGrid1, new string[] {
																																	 "AllowForeign",
																																	 "ShowHelp"}, new object[] {
																																								   false,
																																								   dk.hob.Windows.Forms.HelpTypes.Bubble});
			this.dataGridColumn9.Label = "CPR";
			this.dataGridColumn9.Width = 94;
			// 
			// dataGridColumn10
			// 
			this.dataGridColumn10.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.DateTimeWrapper(this.dataGrid1, new string[] {
																																		   "MarkOnFocus",
																																		   "IsTimeAndDateControl"}, new object[] {
																																													 -1,
																																													 true});
			this.dataGridColumn10.Label = "Dato";
			this.dataGridColumn10.Width = 107;
			// 
			// dataGridColumn11
			// 
			this.dataGridColumn11.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.NumberBoxWrapper(this.dataGrid1, new string[] {
																																			"ShowMinusOne",
																																			"MaxLength"}, new object[] {
																																										   false,
																																										   10});
			this.dataGridColumn11.Label = "Tal";
			// 
			// dataGridColumn12
			// 
			this.dataGridColumn12.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.ComboBoxWrapper(this.dataGrid1, new string[0], new object[0]);
			this.dataGridColumn12.Label = "ComboSkriv";
			this.dataGridColumn12.Width = 20;
			// 
			// dataGridColumn13
			// 
			this.dataGridColumn13.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.ImageWrapper(this.dataGrid1, new string[0], new object[0]);
			this.dataGridColumn13.Label = "Billede";
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(368, 432);
			this.button1.Name = "button1";
			this.button1.TabIndex = 5;
			this.button1.Text = "Grid->DS";
			this.button1.Click += new System.EventHandler(this.GridDS);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(456, 432);
			this.button2.Name = "button2";
			this.button2.TabIndex = 6;
			this.button2.Text = "DS->Grid";
			this.button2.Click += new System.EventHandler(this.DSGrid);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(368, 472);
			this.button3.Name = "button3";
			this.button3.TabIndex = 7;
			this.button3.Text = "Skift billede";
			this.button3.Click += new System.EventHandler(this.SwitchPicture);
			// 
			// dataGrid3
			// 
			this.dataGrid3.AllowSorting = false;
			this.dataGrid3.CaptionText = "Our own";
			this.dataGrid3.Columns.AddRange(new dk.hob.Windows.Forms.DataGrid.DataGridColumn[] {
																								   this.dataGridColumn14,
																								   this.dataGridColumn15,
																								   this.dataGridColumn16});
			this.dataGrid3.DataSource = this.dataTable1;
			this.dataGrid3.Location = new System.Drawing.Point(584, 248);
			this.dataGrid3.Name = "dataGrid3";
			this.dataGrid3.Size = new System.Drawing.Size(368, 160);
			this.dataGrid3.TabIndex = 8;
			// 
			// dataGridColumn14
			// 
			this.dataGridColumn14.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.TextBoxWrapper(null, new string[] {
																																"MaxLength"}, new object[] {
																																							   32767});
			this.dataGridColumn14.Label = "Streng";
			// 
			// dataGridColumn15
			// 
			this.dataGridColumn15.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.NumberBoxWrapper(null, new string[] {
																																  "ShowMinusOne",
																																  "MaxLength"}, new object[] {
																																								 false,
																																								 10});
			this.dataGridColumn15.Label = "Tal";
			// 
			// dataGridColumn16
			// 
			this.dataGridColumn16.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.DateTimeWrapper(null, new string[] {
																																 "MarkOnFocus",
																																 "IsTimeAndDateControl"}, new object[] {
																																										   -1,
																																										   false});
			this.dataGridColumn16.Label = "Dato";
			// 
			// dataGrid4
			// 
			this.dataGrid4.CaptionVisible = false;
			this.dataGrid4.Columns.AddRange(new dk.hob.Windows.Forms.DataGrid.DataGridColumn[] {
																								   this.dataGridColumn17,
																								   this.dataGridColumn18,
																								   this.dataGridColumn19});
			this.dataGrid4.CursorVisible = false;
			this.dataGrid4.GridLinesVisible = dk.hob.Windows.Forms.DataGrid.GridLines.None;
			this.dataGrid4.Location = new System.Drawing.Point(584, 416);
			this.dataGrid4.Name = "dataGrid4";
			this.dataGrid4.PixelsToAdd = 4;
			this.dataGrid4.ReadOnly = true;
			this.dataGrid4.RowHeadersVisible = false;
			this.dataGrid4.Size = new System.Drawing.Size(344, 120);
			this.dataGrid4.TabIndex = 9;
			// 
			// dataGridColumn17
			// 
			this.dataGridColumn17.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.TextBoxWrapper(null, new string[] {
																																"MaxLength"}, new object[] {
																																							   32767});
			this.dataGridColumn17.Label = "Column1";
			// 
			// dataGridColumn18
			// 
			this.dataGridColumn18.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.TextBoxWrapper(null, new string[] {
																																"MaxLength"}, new object[] {
																																							   32767});
			this.dataGridColumn18.Label = "Column2";
			// 
			// dataGridColumn19
			// 
			this.dataGridColumn19.ColumnType = new dk.hob.Windows.Forms.DataGrid.Wrappers.TextBoxWrapper(null, new string[] {
																																"MaxLength"}, new object[] {
																																							   32767});
			this.dataGridColumn19.Label = "Column3";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(968, 614);
			this.Controls.Add(this.dataGrid4);
			this.Controls.Add(this.dataGrid3);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.dataGrid2);
			this.Controls.Add(this.dataGrid1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form_Closing);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.hobBindingComponent1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid4)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Grid_BeginEdit(object sender, dk.hob.Windows.Forms.DataGrid.CellEventArgs cea)
		{
			if ((cea.Column == 3) && (cea.Row == -1))
			{
				cea.Item.Value = "Default værdi";
				cea.RowItems[4].Value = "Anden værdi";
			}
		}

		private void Grid_RowAdded(object sender, dk.hob.Windows.Forms.DataGrid.RowEventArgs rea)
		{
			rea.Row[12].Value = 0;
		}

		private void GridDS(object sender, System.EventArgs e)
		{
			DataSet ds = new DataSet();
			DataTable dt = ds.Tables.Add("GridTable");
			dt.Columns.Add("uiRowID", typeof(Guid));
			dt.Columns.Add("Col1", typeof(int));
			dt.Columns.Add("Col2");
			dt.Columns.Add("Col3");
			dt.Columns.Add("Col4");

			hobBindingComponent1.CopyToDataSet(ds);
			System.Windows.Forms.MessageBox.Show("Done!");
		}

		private void DSGrid(object sender, System.EventArgs e)
		{
			DataSet ds = new DataSet();
			DataTable dt = ds.Tables.Add("GridTable");
			dt.Columns.Add("uiRowID", typeof(Guid));
			dt.Columns.Add("Col1", typeof(int));
			dt.Columns.Add("Col2");
			dt.Columns.Add("Col3");
			dt.Columns.Add("Col4");

			DataRow dr = dt.NewRow();
			dr[0] = Guid.NewGuid();
			dr[1] = 1;
			dr[2] = "Hugo";
			dr[3] = "buko";
			dr[4] = "flødeost!";
			dt.Rows.Add(dr);

			dr = dt.NewRow();
			dr[0] = Guid.NewGuid();
			dr[1] = 0;
			dr[2] = DBNull.Value;
			dr[3] = "Dette er en lang tekst";
			dr[4] = "Her står også noget";
			dt.Rows.Add(dr);

			hobBindingComponent1.CopyFromDataSet(ds);
		}

		private void SwitchPicture(object sender, System.EventArgs e)
		{
			if (dataGrid1.Items.Count > 0)
			{
				dataGrid1.Items[0][12].Value = (int)dataGrid1.Items[0][12].Value == 0 ? 1 : 0;
				dataGrid1.InvalidateRow(0);
			}
		}

		private void Grid_ColumnControlInitializing(object sender, dk.hob.Windows.Forms.DataGrid.ColumnControlEventArgs ccea)
		{
			if (ccea.Column == 6)
			{
				StringLookupPairWrapper ctrl = (StringLookupPairWrapper)ccea.Control;

				using (new SleepCursor())
				{
					if (keyItemPair == null)
					{
						RangeCodeList codeList = new RangeCodeList("..\\..", "Sygehuse.sst.xml");
						keyItemPair = codeList.GetWholeCollection();
					}

					ctrl.KeyItemPairs = keyItemPair;
				}
			}
		}
		private static KeyItemPairCollection keyItemPair = null;

		private void Form_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = false;
		}
	}
}
