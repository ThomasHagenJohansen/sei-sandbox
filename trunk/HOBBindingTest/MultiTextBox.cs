using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using dk.hob.Data.Binding;

namespace HOBBindingTest
{
	/// <summary>
	/// Summary description for MultiTextBox.
	/// </summary>
	public class MultiTextBox : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MultiTextBox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "",
																													 "",
																													 "Hejsa",
																													 "42"}, -1);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "",
																													 "",
																													 "1111",
																													 "87"}, -1);
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.textBox1.Location = new System.Drawing.Point(8, 8);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(160, 136);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "Uhhhh, dette her er\r\nfor vildt. Man kan\r\nen helt masse";
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.columnHeader4});
			this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																					  listViewItem1,
																					  listViewItem2});
			this.listView1.Location = new System.Drawing.Point(176, 8);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(256, 136);
			this.listView1.TabIndex = 1;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "RowID";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "SkemaID";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Tekst";
			this.columnHeader3.Width = 69;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Tal";
			// 
			// MultiTextBox
			// 
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.textBox1);
			this.Name = "MultiTextBox";
			this.Size = new System.Drawing.Size(440, 150);
			this.ResumeLayout(false);

		}
		#endregion

		public RowCollection TextBox
		{
			get
			{
				RowCollection result = new RowCollection();
				String[] strings = textBox1.Text.Split('\n');

				foreach (String str in strings)
				{
					Hashtable row = new Hashtable();
					row["txLine"] = str.Trim();
					result.Add(row);
				}

				return result;
			}

			set
			{
				String text = "";

				if (value != null)
				{
					foreach (Hashtable row in value)
						text += row["txLine"] + "\r\n";
				}

				textBox1.Text = text.TrimEnd();
			}
		}

		public RowCollection ListView
		{
			get
			{
				RowCollection result = new RowCollection();

				foreach (ListViewItem item in listView1.Items)
				{
					Hashtable row = new Hashtable();

					if (!item.SubItems[0].Text.Equals(""))
						row["uiRowID"] = new Guid(item.SubItems[0].Text);

					if (!item.SubItems[1].Text.Equals(""))
						row["uiSkemaID"] = new Guid(item.SubItems[1].Text);

					row["txLine"] = item.SubItems[2].Text;
					row["iTal"]   = Convert.ToInt32(item.SubItems[3].Text);

					result.Add(row);
				}

				return result;
			}

			set
			{
				listView1.Items.Clear();

				if (value != null)
				{
					foreach (Hashtable row in value)
					{
						ListViewItem item = new ListViewItem();

						item.SubItems[0].Text = row["uiRowID"].ToString();
						item.SubItems.Add(row["uiSkemaID"].ToString());
						item.SubItems.Add((String)row["txLine"]);
						item.SubItems.Add(row["iTal"].ToString());

						listView1.Items.Add(item);
					}
				}
			}
		}
	}
}
