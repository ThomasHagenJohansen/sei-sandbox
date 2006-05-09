using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using dk.hob.Windows.Forms;

namespace HOBBindingTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private TestDS myDataSet;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.CheckedListBox checkedListBox1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.HScrollBar hScrollBar1;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.DomainUpDown domainUpDown1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.ColumnHeader Bool;
		private System.Windows.Forms.ColumnHeader Integer;
		private System.Windows.Forms.ColumnHeader String;
		private System.Windows.Forms.ColumnHeader DateTime;
		private System.Windows.Forms.ColumnHeader InvertedBool;
		private System.Windows.Forms.ColumnHeader ValueChange;
		private System.Windows.Forms.ColumnHeader NoBinding;
		private dk.hob.Windows.Forms.RadiobuttonGroup radiobuttonGroup1;
		private dk.hob.Data.Binding.HOBValueChangeList bitmaskList;
		private System.Windows.Forms.ColumnHeader Bitmask;
		private System.Windows.Forms.TextBox textBox2;
		private dk.hob.Data.Binding.HOBValueChangeList domainList;
		private dk.hob.Data.Binding.HOBValueChangeList specielList;
		private dk.hob.Data.Binding.HOBValueChangeList radioList;
		private dk.hob.Data.Binding.HOBBindingComponent hobBindingComponent1;
		private dk.hob.Data.Binding.HOBValueChangeList multiList;
		private dk.hob.Windows.Forms.Paging paging1;
		private System.Windows.Forms.CheckBox pageCheckBox;
		private dk.hob.Windows.Forms.RadiobuttonGroup pageRadioButtonGroup;
		private System.Windows.Forms.TextBox pageTextBox;
		private HOBBindingTest.MultiTextBox multiTextBox1;
		private dk.hob.Windows.Forms.DateTimeEdit pageDateTimeEdit;
		private System.Windows.Forms.ListView pageListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private HOBBindingTest.MultiTextBox multiTextBox2;
		private System.Windows.Forms.TextBox guidText;
		private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			hobBindingComponent1.DocumentID = new Guid("b7852efc-e7ac-431b-8b42-d9d3032a55b6");

			myDataSet = new TestDS();

			myDataSet.MultiControl_ListView.Columns["uiRowID"].DefaultValue = Guid.Empty;
			myDataSet.ListView.Columns["uiRowID"].DefaultValue = Guid.Empty;
			myDataSet.Paging.Columns["uiRowID"].DefaultValue = Guid.Empty;
			myDataSet.PagingListView.Columns["uiRowID"].DefaultValue = Guid.Empty;
			myDataSet.PagingMulti_ListView.Columns["uiRowID"].DefaultValue = Guid.Empty;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "",
																													 "",
																													 "",
																													 "Hejsa"}, -1);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "",
																													 "",
																													 "",
																													 "Bent!"}, -1);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "",
																													 "",
																													 "",
																													 "Går det godt?"}, -1);
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "Række 0",
																													 "42",
																													 "Hugo",
																													 "True",
																													 "True",
																													 "08-06-2004",
																													 "33",
																													 "No1"}, -1);
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "endnu en række",
																													 "87",
																													 "Ingen som binder mig",
																													 "False",
																													 "False",
																													 "22-07-1971",
																													 "515",
																													 "No2"}, -1);
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "Bla bla bla",
																													 "112",
																													 "#%&/",
																													 "False",
																													 "True",
																													 "25-05-1969",
																													 "255",
																													 "No3"}, -1);
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "Sov sødt",
																													 "12345678",
																													 "ÆØÅæøÅ",
																													 "True",
																													 "False",
																													 "26-06-2004",
																													 "567",
																													 "No4"}, -1);
			this.label1 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.guidText = new System.Windows.Forms.TextBox();
			this.multiTextBox2 = new HOBBindingTest.MultiTextBox();
			this.pageListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.pageDateTimeEdit = new dk.hob.Windows.Forms.DateTimeEdit();
			this.pageCheckBox = new System.Windows.Forms.CheckBox();
			this.pageRadioButtonGroup = new dk.hob.Windows.Forms.RadiobuttonGroup();
			this.pageTextBox = new System.Windows.Forms.TextBox();
			this.paging1 = new dk.hob.Windows.Forms.Paging();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.listView1 = new System.Windows.Forms.ListView();
			this.String = new System.Windows.Forms.ColumnHeader();
			this.Integer = new System.Windows.Forms.ColumnHeader();
			this.NoBinding = new System.Windows.Forms.ColumnHeader();
			this.Bool = new System.Windows.Forms.ColumnHeader();
			this.InvertedBool = new System.Windows.Forms.ColumnHeader();
			this.DateTime = new System.Windows.Forms.ColumnHeader();
			this.Bitmask = new System.Windows.Forms.ColumnHeader();
			this.ValueChange = new System.Windows.Forms.ColumnHeader();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.domainUpDown1 = new System.Windows.Forms.DomainUpDown();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.hobBindingComponent1 = new dk.hob.Data.Binding.HOBBindingComponent(this.components);
			this.multiList = new dk.hob.Data.Binding.HOBValueChangeList(this.components);
			this.bitmaskList = new dk.hob.Data.Binding.HOBValueChangeList(this.components);
			this.domainList = new dk.hob.Data.Binding.HOBValueChangeList(this.components);
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.specielList = new dk.hob.Data.Binding.HOBValueChangeList(this.components);
			this.radiobuttonGroup1 = new dk.hob.Windows.Forms.RadiobuttonGroup();
			this.radioList = new dk.hob.Data.Binding.HOBValueChangeList(this.components);
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.multiTextBox1 = new HOBBindingTest.MultiTextBox();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.hobBindingComponent1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(8, 32);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.TabIndex = 1;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "linkLabel1";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(120, 8);
			this.button1.Name = "button1";
			this.button1.TabIndex = 5;
			this.button1.Text = "button1";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(8, 64);
			this.textBox1.Name = "textBox1";
			this.hobBindingComponent1.SetSimpleBindings(this.textBox1, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																													  new dk.hob.Data.Binding.HOBSimpleBinding("Text", "Info.textBox", dk.hob.Data.Binding.HOBBindingType.String, true, null, ((short)(2)))});
			this.textBox1.TabIndex = 6;
			this.textBox1.Text = "textBox1";
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(8, 96);
			this.checkBox1.Name = "checkBox1";
			this.hobBindingComponent1.SetSimpleBindings(this.checkBox1, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																													   new dk.hob.Data.Binding.HOBSimpleBinding("Checked", "Info.checkBoxNormal", dk.hob.Data.Binding.HOBBindingType.Boolean, false, null, ((short)(0)))});
			this.checkBox1.Size = new System.Drawing.Size(64, 24);
			this.checkBox1.TabIndex = 7;
			this.checkBox1.Text = "Normal";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.guidText);
			this.groupBox1.Controls.Add(this.multiTextBox2);
			this.groupBox1.Controls.Add(this.pageListView);
			this.groupBox1.Controls.Add(this.pageDateTimeEdit);
			this.groupBox1.Controls.Add(this.pageCheckBox);
			this.groupBox1.Controls.Add(this.pageRadioButtonGroup);
			this.groupBox1.Controls.Add(this.pageTextBox);
			this.groupBox1.Controls.Add(this.paging1);
			this.groupBox1.Location = new System.Drawing.Point(8, 240);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(744, 248);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "groupBox1";
			// 
			// guidText
			// 
			this.guidText.Location = new System.Drawing.Point(56, 128);
			this.guidText.Name = "guidText";
			this.hobBindingComponent1.SetPageControlLink(this.guidText, this.paging1);
			this.guidText.ReadOnly = true;
			this.hobBindingComponent1.SetSimpleBindings(this.guidText, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																													  new dk.hob.Data.Binding.HOBSimpleBinding("Text", "Paging.uiRowID", dk.hob.Data.Binding.HOBBindingType.Guid, false, null, ((short)(0)))});
			this.guidText.Size = new System.Drawing.Size(184, 20);
			this.guidText.TabIndex = 36;
			this.guidText.Text = "";
			// 
			// multiTextBox2
			// 
			this.multiTextBox2.Location = new System.Drawing.Point(296, 136);
			this.multiTextBox2.Name = "multiTextBox2";
			this.hobBindingComponent1.SetPageControlLink(this.multiTextBox2, this.paging1);
			this.multiTextBox2.Size = new System.Drawing.Size(440, 104);
			this.multiTextBox2.TabIndex = 35;
			this.hobBindingComponent1.SetTableBindings(this.multiTextBox2, new dk.hob.Data.Binding.HOBTableBinding[] {
																														 new dk.hob.Data.Binding.HOBTableBinding("PagingMulti_ListView", "ListView", "uiRowID", "uiSkemaID", new dk.hob.Data.Binding.HOBColumnBindingCollection(new dk.hob.Data.Binding.HOBColumnBinding[] {
																																																																															   new dk.hob.Data.Binding.HOBColumnBinding("txLine", dk.hob.Data.Binding.HOBBindingType.ValueChange, false, this.multiList),
																																																																															   new dk.hob.Data.Binding.HOBColumnBinding("iTal", dk.hob.Data.Binding.HOBBindingType.Integer, false, null)}), ((short)(0))),
																														 new dk.hob.Data.Binding.HOBTableBinding("PagingMulti_TextBox", "TextBox", "", "", new dk.hob.Data.Binding.HOBColumnBindingCollection(new dk.hob.Data.Binding.HOBColumnBinding[] {
																																																																											 new dk.hob.Data.Binding.HOBColumnBinding("txLine", dk.hob.Data.Binding.HOBBindingType.String, false, null)}), ((short)(0)))});
			// 
			// pageListView
			// 
			this.pageListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						   this.columnHeader1,
																						   this.columnHeader4,
																						   this.columnHeader2,
																						   this.columnHeader3});
			this.pageListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																						 listViewItem1,
																						 listViewItem2,
																						 listViewItem3});
			this.hobBindingComponent1.SetListViewBinding(this.pageListView, new dk.hob.Data.Binding.HOBListViewBinding("PagingListView", "uiRowID", "", new dk.hob.Data.Binding.HOBColumnBindingCollection(new dk.hob.Data.Binding.HOBColumnBinding[] {
																																																														  new dk.hob.Data.Binding.HOBColumnBinding("uiRowID", dk.hob.Data.Binding.HOBBindingType.Guid, false, null),
																																																														  new dk.hob.Data.Binding.HOBColumnBinding("uiParentID", dk.hob.Data.Binding.HOBBindingType.Guid, false, null),
																																																														  new dk.hob.Data.Binding.HOBColumnBinding("uiSkemaID", dk.hob.Data.Binding.HOBBindingType.Guid, false, null),
																																																														  new dk.hob.Data.Binding.HOBColumnBinding("txTekst", dk.hob.Data.Binding.HOBBindingType.String, false, null)}), ((short)(0))));
			this.pageListView.Location = new System.Drawing.Point(328, 16);
			this.pageListView.Name = "pageListView";
			this.hobBindingComponent1.SetPageControlLink(this.pageListView, this.paging1);
			this.pageListView.Size = new System.Drawing.Size(408, 112);
			this.pageListView.TabIndex = 34;
			this.pageListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "RowID";
			this.columnHeader1.Width = 78;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "ParentID";
			this.columnHeader4.Width = 84;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "SkemaID";
			this.columnHeader2.Width = 98;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Tekst";
			this.columnHeader3.Width = 140;
			// 
			// pageDateTimeEdit
			// 
			this.pageDateTimeEdit.AutomaticInsert = false;
			this.pageDateTimeEdit.ErrorInvalid = false;
			this.pageDateTimeEdit.InputChar = '_';
			this.pageDateTimeEdit.InputMask = "00/00-0000";
			this.pageDateTimeEdit.IsBlankDateAllowed = true;
			this.pageDateTimeEdit.IsTimeAndDateControl = false;
			this.pageDateTimeEdit.Location = new System.Drawing.Point(216, 48);
			this.pageDateTimeEdit.LowerDateTimeLimit = new System.DateTime(((long)(0)));
			this.pageDateTimeEdit.MaxLength = 10;
			this.pageDateTimeEdit.Name = "pageDateTimeEdit";
			this.hobBindingComponent1.SetPageControlLink(this.pageDateTimeEdit, this.paging1);
			this.pageDateTimeEdit.ShowHelper = true;
			this.hobBindingComponent1.SetSimpleBindings(this.pageDateTimeEdit, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																															  new dk.hob.Data.Binding.HOBSimpleBinding("Value", "Paging.dtDato", dk.hob.Data.Binding.HOBBindingType.Date, false, null, ((short)(0)))});
			this.pageDateTimeEdit.StdInputMask = dk.hob.Windows.Forms.InputMaskType.Custom;
			this.pageDateTimeEdit.TabIndex = 33;
			this.pageDateTimeEdit.UpperDateTimeLimit = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.pageDateTimeEdit.Value = new System.DateTime(((long)(0)));
			// 
			// pageCheckBox
			// 
			this.pageCheckBox.Location = new System.Drawing.Point(96, 48);
			this.pageCheckBox.Name = "pageCheckBox";
			this.hobBindingComponent1.SetPageControlLink(this.pageCheckBox, this.paging1);
			this.hobBindingComponent1.SetSimpleBindings(this.pageCheckBox, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																														  new dk.hob.Data.Binding.HOBSimpleBinding("Checked", "Paging.bFlag", dk.hob.Data.Binding.HOBBindingType.Boolean, false, null, ((short)(0)))});
			this.pageCheckBox.TabIndex = 32;
			this.pageCheckBox.Text = "Sjov i gaden";
			// 
			// pageRadioButtonGroup
			// 
			this.pageRadioButtonGroup.ExtraPaddingSpace = new int[0];
			this.pageRadioButtonGroup.Items = new string[] {
															   "Bil",
															   "Bus",
															   "Tog"};
			this.pageRadioButtonGroup.Location = new System.Drawing.Point(16, 24);
			this.pageRadioButtonGroup.Name = "pageRadioButtonGroup";
			this.hobBindingComponent1.SetPageControlLink(this.pageRadioButtonGroup, this.paging1);
			this.hobBindingComponent1.SetSimpleBindings(this.pageRadioButtonGroup, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																																  new dk.hob.Data.Binding.HOBSimpleBinding("SelectedIndex", "Paging.iTransport", dk.hob.Data.Binding.HOBBindingType.Index, false, null, ((short)(0)))});
			this.pageRadioButtonGroup.Size = new System.Drawing.Size(72, 80);
			this.pageRadioButtonGroup.TabIndex = 31;
			// 
			// pageTextBox
			// 
			this.pageTextBox.Location = new System.Drawing.Point(96, 16);
			this.pageTextBox.Name = "pageTextBox";
			this.hobBindingComponent1.SetPageControlLink(this.pageTextBox, this.paging1);
			this.hobBindingComponent1.SetSimpleBindings(this.pageTextBox, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																														 new dk.hob.Data.Binding.HOBSimpleBinding("Text", "Paging.txTekst", dk.hob.Data.Binding.HOBBindingType.String, false, null, ((short)(0)))});
			this.pageTextBox.Size = new System.Drawing.Size(224, 20);
			this.pageTextBox.TabIndex = 30;
			this.pageTextBox.Text = "textBox3";
			// 
			// paging1
			// 
			this.paging1.BackColor = System.Drawing.SystemColors.Control;
			this.paging1.CurrentPage = 0;
			this.paging1.Location = new System.Drawing.Point(8, 224);
			this.paging1.Name = "paging1";
			this.paging1.NumPages = 0;
			this.hobBindingComponent1.SetPagingBindings(this.paging1, new dk.hob.Data.Binding.HOBPagingBinding[] {
																													 new dk.hob.Data.Binding.HOBPagingBinding("Paging", "uiRowID", "uiSkemaID"),
																													 new dk.hob.Data.Binding.HOBPagingBinding("PagingListView", "uiParentID", "uiSkemaID"),
																													 new dk.hob.Data.Binding.HOBPagingBinding("PagingMulti_TextBox", "uiParentID", ""),
																													 new dk.hob.Data.Binding.HOBPagingBinding("PagingMulti_ListView", "uiParentID", "")});
			this.paging1.Size = new System.Drawing.Size(224, 17);
			this.paging1.TabIndex = 29;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(24, 16);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(56, 40);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Location = new System.Drawing.Point(8, 128);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(112, 64);
			this.panel1.TabIndex = 14;
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(264, 8);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(130, 96);
			this.dataGrid1.TabIndex = 2;
			// 
			// listBox1
			// 
			this.listBox1.Items.AddRange(new object[] {
														  "ghgh",
														  "gfhgf",
														  "gfhgf",
														  "tytu"});
			this.listBox1.Location = new System.Drawing.Point(400, 8);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(120, 95);
			this.listBox1.TabIndex = 10;
			// 
			// checkedListBox1
			// 
			this.checkedListBox1.Items.AddRange(new object[] {
																 "ttrhgf",
																 "ghtyd",
																 "sdfssd",
																 "f",
																 "ghbgf"});
			this.checkedListBox1.Location = new System.Drawing.Point(528, 8);
			this.checkedListBox1.Name = "checkedListBox1";
			this.checkedListBox1.Size = new System.Drawing.Size(120, 94);
			this.checkedListBox1.TabIndex = 11;
			// 
			// comboBox1
			// 
			this.comboBox1.Items.AddRange(new object[] {
														   "Valg nummer 1",
														   "Uha uha!",
														   "Gumsebent"});
			this.comboBox1.Location = new System.Drawing.Point(224, 112);
			this.comboBox1.Name = "comboBox1";
			this.hobBindingComponent1.SetSimpleBindings(this.comboBox1, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																													   new dk.hob.Data.Binding.HOBSimpleBinding("SelectedIndex", "Info.comboBox", dk.hob.Data.Binding.HOBBindingType.Index, false, null, ((short)(0)))});
			this.comboBox1.Size = new System.Drawing.Size(121, 21);
			this.comboBox1.TabIndex = 16;
			this.comboBox1.Text = "comboBox1";
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.String,
																						this.Integer,
																						this.NoBinding,
																						this.Bool,
																						this.InvertedBool,
																						this.DateTime,
																						this.Bitmask,
																						this.ValueChange});
			this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																					  listViewItem4,
																					  listViewItem5,
																					  listViewItem6,
																					  listViewItem7});
			this.hobBindingComponent1.SetListViewBinding(this.listView1, new dk.hob.Data.Binding.HOBListViewBinding("ListView", "uiRowID", "uiSkemaID", new dk.hob.Data.Binding.HOBColumnBindingCollection(new dk.hob.Data.Binding.HOBColumnBinding[] {
																																																														  new dk.hob.Data.Binding.HOBColumnBinding("txString", dk.hob.Data.Binding.HOBBindingType.String, false, null),
																																																														  new dk.hob.Data.Binding.HOBColumnBinding("iInteger", dk.hob.Data.Binding.HOBBindingType.Integer, false, null),
																																																														  new dk.hob.Data.Binding.HOBColumnBinding("", dk.hob.Data.Binding.HOBBindingType.NoBinding, false, null),
																																																														  new dk.hob.Data.Binding.HOBColumnBinding("bFlag", dk.hob.Data.Binding.HOBBindingType.Boolean, false, null),
																																																														  new dk.hob.Data.Binding.HOBColumnBinding("bInverted", dk.hob.Data.Binding.HOBBindingType.InvertedBoolean, false, null),
																																																														  new dk.hob.Data.Binding.HOBColumnBinding("dtDate", dk.hob.Data.Binding.HOBBindingType.DateString, false, null),
																																																														  new dk.hob.Data.Binding.HOBColumnBinding("txBitmask", dk.hob.Data.Binding.HOBBindingType.Bitmask, false, this.bitmaskList),
																																																														  new dk.hob.Data.Binding.HOBColumnBinding("txValueChange", dk.hob.Data.Binding.HOBBindingType.ValueChange, false, this.domainList)}), ((short)(1))));
			this.listView1.Location = new System.Drawing.Point(8, 496);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(624, 97);
			this.listView1.TabIndex = 26;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// String
			// 
			this.String.Text = "String";
			this.String.Width = 92;
			// 
			// Integer
			// 
			this.Integer.Text = "Integer";
			this.Integer.Width = 64;
			// 
			// NoBinding
			// 
			this.NoBinding.Text = "No Binding";
			this.NoBinding.Width = 117;
			// 
			// Bool
			// 
			this.Bool.Text = "Bool";
			this.Bool.Width = 42;
			// 
			// InvertedBool
			// 
			this.InvertedBool.Text = "Inverted Bool";
			this.InvertedBool.Width = 77;
			// 
			// DateTime
			// 
			this.DateTime.Text = "Date";
			this.DateTime.Width = 71;
			// 
			// Bitmask
			// 
			this.Bitmask.Text = "Bitmask";
			this.Bitmask.Width = 64;
			// 
			// ValueChange
			// 
			this.ValueChange.Text = "Value Change";
			this.ValueChange.Width = 89;
			// 
			// treeView1
			// 
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(352, 112);
			this.treeView1.Name = "treeView1";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																				  new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
																																									 new System.Windows.Forms.TreeNode("Node1", new System.Windows.Forms.TreeNode[] {
																																																														new System.Windows.Forms.TreeNode("Node2")})}),
																				  new System.Windows.Forms.TreeNode("Node3")});
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.TabIndex = 21;
			// 
			// tabControl1
			// 
			this.tabControl1.Location = new System.Drawing.Point(128, 128);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(88, 64);
			this.tabControl1.TabIndex = 15;
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(352, 216);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.hobBindingComponent1.SetSimpleBindings(this.dateTimePicker1, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																															 new dk.hob.Data.Binding.HOBSimpleBinding("Value", "Info.dateTimePicker", dk.hob.Data.Binding.HOBBindingType.Date, false, null, ((short)(0)))});
			this.dateTimePicker1.TabIndex = 22;
			// 
			// hScrollBar1
			// 
			this.hScrollBar1.Location = new System.Drawing.Point(480, 192);
			this.hScrollBar1.Name = "hScrollBar1";
			this.hobBindingComponent1.SetSimpleBindings(this.hScrollBar1, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																														 new dk.hob.Data.Binding.HOBSimpleBinding("Value", "Info.hScrollBar", dk.hob.Data.Binding.HOBBindingType.Integer, false, null, ((short)(0)))});
			this.hScrollBar1.TabIndex = 25;
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Location = new System.Drawing.Point(480, 112);
			this.vScrollBar1.Name = "vScrollBar1";
			this.hobBindingComponent1.SetSimpleBindings(this.vScrollBar1, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																														 new dk.hob.Data.Binding.HOBSimpleBinding("Value", "Info.vScrollBar", dk.hob.Data.Binding.HOBBindingType.Integer, false, null, ((short)(0)))});
			this.vScrollBar1.TabIndex = 24;
			// 
			// domainUpDown1
			// 
			this.domainUpDown1.Items.Add("No1");
			this.domainUpDown1.Items.Add("No2");
			this.domainUpDown1.Items.Add("No3");
			this.domainUpDown1.Items.Add("No4");
			this.domainUpDown1.Location = new System.Drawing.Point(224, 144);
			this.domainUpDown1.Name = "domainUpDown1";
			this.hobBindingComponent1.SetSimpleBindings(this.domainUpDown1, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																														   new dk.hob.Data.Binding.HOBSimpleBinding("Text", "Info.domainUpDown", dk.hob.Data.Binding.HOBBindingType.ValueChange, false, this.domainList, ((short)(0)))});
			this.domainUpDown1.TabIndex = 17;
			this.domainUpDown1.Text = "domainUpDown1";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(224, 176);
			this.numericUpDown1.Name = "numericUpDown1";
			this.hobBindingComponent1.SetSimpleBindings(this.numericUpDown1, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																															new dk.hob.Data.Binding.HOBSimpleBinding("Value", "Info.numericUpDown", dk.hob.Data.Binding.HOBBindingType.Integer, false, null, ((short)(0)))});
			this.numericUpDown1.TabIndex = 18;
			this.numericUpDown1.Value = new System.Decimal(new int[] {
																		 4,
																		 0,
																		 0,
																		 0});
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(568, 192);
			this.trackBar1.Name = "trackBar1";
			this.hobBindingComponent1.SetSimpleBindings(this.trackBar1, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																													   new dk.hob.Data.Binding.HOBSimpleBinding("Value", "Info.trackBar", dk.hob.Data.Binding.HOBBindingType.Integer, false, null, ((short)(0)))});
			this.trackBar1.TabIndex = 19;
			this.trackBar1.Value = 3;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(120, 40);
			this.progressBar1.Name = "progressBar1";
			this.hobBindingComponent1.SetSimpleBindings(this.progressBar1, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																														  new dk.hob.Data.Binding.HOBSimpleBinding("Value", "Info.progressBar", dk.hob.Data.Binding.HOBBindingType.Integer, false, null, ((short)(0)))});
			this.progressBar1.TabIndex = 20;
			this.progressBar1.Value = 42;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(656, 72);
			this.richTextBox1.Name = "richTextBox1";
			this.hobBindingComponent1.SetSimpleBindings(this.richTextBox1, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																														  new dk.hob.Data.Binding.HOBSimpleBinding("Text", "Info.richTextBox", dk.hob.Data.Binding.HOBBindingType.String, false, null, ((short)(1)))});
			this.richTextBox1.TabIndex = 12;
			this.richTextBox1.Text = "Hej dit svin!\nDette er en rich text box";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(656, 8);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(96, 23);
			this.button2.TabIndex = 3;
			this.button2.Text = "Prop->DataSet";
			this.button2.Click += new System.EventHandler(this.PropDataSet_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(656, 40);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(96, 23);
			this.button3.TabIndex = 4;
			this.button3.Text = "DataSet->Prop";
			this.button3.Click += new System.EventHandler(this.DataSetProp_Click);
			// 
			// checkBox2
			// 
			this.checkBox2.Location = new System.Drawing.Point(72, 96);
			this.checkBox2.Name = "checkBox2";
			this.hobBindingComponent1.SetSimpleBindings(this.checkBox2, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																													   new dk.hob.Data.Binding.HOBSimpleBinding("Checked", "Info.checkBoxInverted", dk.hob.Data.Binding.HOBBindingType.InvertedBoolean, false, null, ((short)(0)))});
			this.checkBox2.Size = new System.Drawing.Size(64, 24);
			this.checkBox2.TabIndex = 8;
			this.checkBox2.Text = "Inverted";
			// 
			// hobBindingComponent1
			// 
			this.hobBindingComponent1.HostingControl = this;
			// 
			// multiList
			// 
			this.multiList.List.AddRange(new dk.hob.Data.Binding.HOBValueChange[] {
																					  new dk.hob.Data.Binding.HOBValueChange("1111", "AAAA")});
			// 
			// bitmaskList
			// 
			this.bitmaskList.List.AddRange(new dk.hob.Data.Binding.HOBValueChange[] {
																						new dk.hob.Data.Binding.HOBValueChange("0", "AA01"),
																						new dk.hob.Data.Binding.HOBValueChange("1", "AA02"),
																						new dk.hob.Data.Binding.HOBValueChange("2", "AA04"),
																						new dk.hob.Data.Binding.HOBValueChange("3", "AA08"),
																						new dk.hob.Data.Binding.HOBValueChange("4", "AA10"),
																						new dk.hob.Data.Binding.HOBValueChange("5", "AA20"),
																						new dk.hob.Data.Binding.HOBValueChange("6", "AA40"),
																						new dk.hob.Data.Binding.HOBValueChange("7", "AA80")});
			// 
			// domainList
			// 
			this.domainList.List.AddRange(new dk.hob.Data.Binding.HOBValueChange[] {
																					   new dk.hob.Data.Binding.HOBValueChange("No1", "Uno"),
																					   new dk.hob.Data.Binding.HOBValueChange("No2", "QWER"),
																					   new dk.hob.Data.Binding.HOBValueChange("No4", "Hugobuko")});
			// 
			// checkBox3
			// 
			this.checkBox3.Location = new System.Drawing.Point(144, 96);
			this.checkBox3.Name = "checkBox3";
			this.hobBindingComponent1.SetSimpleBindings(this.checkBox3, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																													   new dk.hob.Data.Binding.HOBSimpleBinding("Checked", "Info.checkBoxSpeciel", dk.hob.Data.Binding.HOBBindingType.ValueChange, false, this.specielList, ((short)(0)))});
			this.checkBox3.Size = new System.Drawing.Size(64, 24);
			this.checkBox3.TabIndex = 9;
			this.checkBox3.Text = "Speciel";
			// 
			// specielList
			// 
			this.specielList.List.AddRange(new dk.hob.Data.Binding.HOBValueChange[] {
																						new dk.hob.Data.Binding.HOBValueChange("True", "47"),
																						new dk.hob.Data.Binding.HOBValueChange("False", "2")});
			// 
			// radiobuttonGroup1
			// 
			this.radiobuttonGroup1.ExtraPaddingSpace = new int[0];
			this.radiobuttonGroup1.Items = new string[] {
															"Lagkage",
															"Is",
															"Flødeskum"};
			this.radiobuttonGroup1.Location = new System.Drawing.Point(568, 112);
			this.radiobuttonGroup1.Name = "radiobuttonGroup1";
			this.hobBindingComponent1.SetSimpleBindings(this.radiobuttonGroup1, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																															   new dk.hob.Data.Binding.HOBSimpleBinding("SelectedIndex", "Info.radioButtonIndex", dk.hob.Data.Binding.HOBBindingType.Index, false, null, ((short)(-1))),
																															   new dk.hob.Data.Binding.HOBSimpleBinding("SelectedIndex", "Info.radioButtonText", dk.hob.Data.Binding.HOBBindingType.ValueChange, false, this.radioList, ((short)(3)))});
			this.radiobuttonGroup1.Size = new System.Drawing.Size(80, 80);
			this.radiobuttonGroup1.TabIndex = 23;
			// 
			// radioList
			// 
			this.radioList.List.AddRange(new dk.hob.Data.Binding.HOBValueChange[] {
																					  new dk.hob.Data.Binding.HOBValueChange("-1", ""),
																					  new dk.hob.Data.Binding.HOBValueChange("0", "Lagkage"),
																					  new dk.hob.Data.Binding.HOBValueChange("1", "Is"),
																					  new dk.hob.Data.Binding.HOBValueChange("2", "Flødeskum")});
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(120, 64);
			this.textBox2.Name = "textBox2";
			this.hobBindingComponent1.SetSimpleBindings(this.textBox2, new dk.hob.Data.Binding.HOBSimpleBinding[] {
																													  new dk.hob.Data.Binding.HOBSimpleBinding("Text", "Info.bitmask", dk.hob.Data.Binding.HOBBindingType.Bitmask, false, this.bitmaskList, ((short)(2)))});
			this.textBox2.TabIndex = 27;
			this.textBox2.Text = "42";
			// 
			// multiTextBox1
			// 
			this.multiTextBox1.Location = new System.Drawing.Point(8, 600);
			this.multiTextBox1.Name = "multiTextBox1";
			this.multiTextBox1.Size = new System.Drawing.Size(440, 150);
			this.multiTextBox1.TabIndex = 28;
			this.hobBindingComponent1.SetTableBindings(this.multiTextBox1, new dk.hob.Data.Binding.HOBTableBinding[] {
																														 new dk.hob.Data.Binding.HOBTableBinding("MultiControl_TextBox", "TextBox", "", "", new dk.hob.Data.Binding.HOBColumnBindingCollection(new dk.hob.Data.Binding.HOBColumnBinding[] {
																																																																											  new dk.hob.Data.Binding.HOBColumnBinding("txLine", dk.hob.Data.Binding.HOBBindingType.String, false, null)}), ((short)(0))),
																														 new dk.hob.Data.Binding.HOBTableBinding("MultiControl_ListView", "ListView", "uiRowID", "uiSkemaID", new dk.hob.Data.Binding.HOBColumnBindingCollection(new dk.hob.Data.Binding.HOBColumnBinding[] {
																																																																																new dk.hob.Data.Binding.HOBColumnBinding("txLine", dk.hob.Data.Binding.HOBBindingType.ValueChange, false, this.multiList),
																																																																																new dk.hob.Data.Binding.HOBColumnBinding("iTal", dk.hob.Data.Binding.HOBBindingType.Integer, false, null)}), ((short)(0)))});
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(760, 757);
			this.Controls.Add(this.multiTextBox1);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.radiobuttonGroup1);
			this.Controls.Add(this.checkBox3);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.trackBar1);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.domainUpDown1);
			this.Controls.Add(this.vScrollBar1);
			this.Controls.Add(this.hScrollBar1);
			this.Controls.Add(this.dateTimePicker1);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.checkedListBox1);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.groupBox1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.hobBindingComponent1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void PropDataSet_Click(object sender, System.EventArgs e)
		{
			using (new SleepCursor())
			{
				hobBindingComponent1.CopyToDataSet(myDataSet);
			}
		}

		private void DataSetProp_Click(object sender, System.EventArgs e)
		{
			using (new SleepCursor())
			{
				hobBindingComponent1.CopyFromDataSet(myDataSet);
			}
		}

		static int Main(string[] args) 
		{
			Form ff = new Form1();
			Application.Run(ff);
			return 0;
		}
	}
}
