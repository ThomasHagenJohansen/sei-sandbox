using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace DBSpeedTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button buttonIndexOf;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textSearch;
		private System.Windows.Forms.Label labelIndexOf;
		private System.Windows.Forms.Button buttonOwn;
		private System.Windows.Forms.Label labelReverse;

		class SearchString
		{
			public	SearchString( string a_Search )
			{
				Search=a_Search;
				foreach( char ch in Search )
					if( ch<256 )
						Map[ch]=true;
			}

			public bool	Contains( char ch )
			{
				return ch<256?Map[ch]:Search.IndexOf(ch)>=0;
			}

			public string	Search;
			bool[]	Map=new bool[256];
		}

		class DBEntry
		{
			public DBEntry( string a_Code, string a_Description )
			{
				Code=a_Code;
				Description=a_Description;
			}

			public bool	DescriptionContains( SearchString str )
			{
				int index=Description.Length-str.Search.Length;
				while( index>=0 )
				{
					if( !str.Contains(Description[index]) )
						index-=str.Search.Length;
					else if( Description.Substring(index,str.Search.Length)==str.Search )
						return true;
					else
						index-=1;
				}

				return false;
			}

			public bool	DescriptionContains2( SearchString str )
			{
				return Description.IndexOf( str.Search )>=0;
			}

			public string Code;
			public string Description;
		}

		Random DescRandom=new Random( 3987 );
		private System.Windows.Forms.Label labelLongest;
		int	LongestWord=0;

		public string GenerateDescription()
		{
			string s="";
			int count=DescRandom.Next( 2, 3 );
			for( int i=0; i<count; ++i )
				s=s+Words[DescRandom.Next(Words.Count-1)]+" ";

			s=s.Substring( 0, s.Length-1 );
			if( s.Length>LongestWord )
				LongestWord=s.Length;

			return s;
		}

		ArrayList	Words;
		DBEntry[]	Database;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			StreamReader f=File.OpenText( "../../words-da" );
			Words=new ArrayList();
			string s;
			while( (s=f.ReadLine())!=null )
			{
				Words.Add( s );
			}

			Database=new DBEntry[50000];
			for( int i=0; i<Database.Length; ++i )
			{
				Database[i]=new DBEntry( ""+i, GenerateDescription() );
			}

			labelLongest.Text="Længste er "+LongestWord;
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
			this.buttonIndexOf = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textSearch = new System.Windows.Forms.TextBox();
			this.labelIndexOf = new System.Windows.Forms.Label();
			this.buttonOwn = new System.Windows.Forms.Button();
			this.labelReverse = new System.Windows.Forms.Label();
			this.labelLongest = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonIndexOf
			// 
			this.buttonIndexOf.Location = new System.Drawing.Point(8, 40);
			this.buttonIndexOf.Name = "buttonIndexOf";
			this.buttonIndexOf.Size = new System.Drawing.Size(72, 24);
			this.buttonIndexOf.TabIndex = 0;
			this.buttonIndexOf.Text = "IndexOf()";
			this.buttonIndexOf.Click += new System.EventHandler(this.buttonIndexOf_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Søgestreng";
			// 
			// textSearch
			// 
			this.textSearch.Location = new System.Drawing.Point(80, 8);
			this.textSearch.Name = "textSearch";
			this.textSearch.Size = new System.Drawing.Size(104, 20);
			this.textSearch.TabIndex = 2;
			this.textSearch.Text = "nert";
			// 
			// labelIndexOf
			// 
			this.labelIndexOf.Location = new System.Drawing.Point(88, 48);
			this.labelIndexOf.Name = "labelIndexOf";
			this.labelIndexOf.Size = new System.Drawing.Size(136, 16);
			this.labelIndexOf.TabIndex = 3;
			// 
			// buttonOwn
			// 
			this.buttonOwn.Location = new System.Drawing.Point(8, 72);
			this.buttonOwn.Name = "buttonOwn";
			this.buttonOwn.Size = new System.Drawing.Size(72, 24);
			this.buttonOwn.TabIndex = 4;
			this.buttonOwn.Text = "Reverse";
			this.buttonOwn.Click += new System.EventHandler(this.buttonOwn_Click);
			// 
			// labelReverse
			// 
			this.labelReverse.Location = new System.Drawing.Point(88, 80);
			this.labelReverse.Name = "labelReverse";
			this.labelReverse.Size = new System.Drawing.Size(136, 16);
			this.labelReverse.TabIndex = 5;
			// 
			// labelLongest
			// 
			this.labelLongest.Location = new System.Drawing.Point(192, 8);
			this.labelLongest.Name = "labelLongest";
			this.labelLongest.Size = new System.Drawing.Size(128, 16);
			this.labelLongest.TabIndex = 6;
			this.labelLongest.Text = "label2";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(320, 117);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.labelLongest,
																		  this.labelReverse,
																		  this.buttonOwn,
																		  this.labelIndexOf,
																		  this.textSearch,
																		  this.label1,
																		  this.buttonIndexOf});
			this.Name = "Form1";
			this.Text = "Form1";
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

		private void buttonIndexOf_Click(object sender, System.EventArgs e)
		{
			labelIndexOf.Text="";
			int start=Environment.TickCount;
			SearchString s=new SearchString( textSearch.Text );
			int r=0;
			for( int i=0; i<Database.Length; ++i )
			{
				r+=Database[i].DescriptionContains2( s )?1:0;
			}

			int end=Environment.TickCount;
			int span=end-start;
			labelIndexOf.Text=""+r+" resultater, "+span+"ms";
		}

		private void buttonOwn_Click(object sender, System.EventArgs e)
		{
			labelReverse.Text="";
			int start=Environment.TickCount;
			SearchString s=new SearchString( textSearch.Text );
			int r=0;
			for( int i=0; i<Database.Length; ++i )
			{
				r+=Database[i].DescriptionContains( s )?1:0;
			}

			int end=Environment.TickCount;
			int span=end-start;
			labelReverse.Text=""+r+" resultater, "+span+"ms";
		}
	}
}
