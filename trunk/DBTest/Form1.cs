using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using TietoEnator.Data.SqlClient.Builder;
using TietoEnator.Data.SqlClient.DB2;
using TietoEnator.Data.SqlClient.Builder.Aggregates;
using TietoEnator.Data.SqlClient.Builder.Criterias;
using TietoEnator.Data.SqlClient.Builder.Functions;
using TietoEnator.Data.SqlClient.Builder.Joins;
using TietoEnator.Data.SqlClient.Builder.Statements;
using TietoEnator.Data.SqlClient.Runner;
using TietoEnator.Data.SqlClient.VistaDB;
using TietoEnator.Threading;

namespace DBTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : Form
	{
		#region Windows Form Designer generated code
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox providerComboBox;
		private System.Windows.Forms.ListBox outputListBox;
		private System.Windows.Forms.Button startButton;
		private System.ComponentModel.Container components = null;
		#endregion

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			providerComboBox.SelectedIndex = 0;
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
			this.label1 = new System.Windows.Forms.Label();
			this.providerComboBox = new System.Windows.Forms.ComboBox();
			this.outputListBox = new System.Windows.Forms.ListBox();
			this.startButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Provider:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// providerComboBox
			// 
			this.providerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.providerComboBox.Items.AddRange(new object[] {
																  "VistaDB",
																  "DB2"});
			this.providerComboBox.Location = new System.Drawing.Point(80, 16);
			this.providerComboBox.Name = "providerComboBox";
			this.providerComboBox.Size = new System.Drawing.Size(256, 21);
			this.providerComboBox.TabIndex = 1;
			// 
			// outputListBox
			// 
			this.outputListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.outputListBox.HorizontalScrollbar = true;
			this.outputListBox.Location = new System.Drawing.Point(16, 72);
			this.outputListBox.Name = "outputListBox";
			this.outputListBox.ScrollAlwaysVisible = true;
			this.outputListBox.Size = new System.Drawing.Size(528, 290);
			this.outputListBox.TabIndex = 2;
			// 
			// startButton
			// 
			this.startButton.Location = new System.Drawing.Point(352, 16);
			this.startButton.Name = "startButton";
			this.startButton.TabIndex = 3;
			this.startButton.Text = "Start";
			this.startButton.Click += new System.EventHandler(this.Start_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(552, 374);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.outputListBox);
			this.Controls.Add(this.providerComboBox);
			this.Controls.Add(this.label1);
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

		private void Start_Click(object sender, EventArgs e)
		{
			outputListBox.Items.Clear();

			if (providerComboBox.SelectedIndex >= 0)
			{
				Guid id = ThreadManager.AddThread(TestThread);
				ThreadManager.StartThread(id, providerComboBox.SelectedIndex);
			}
		}

		private void TestThread(ManualResetEvent exitEvent, Object userData)
		{
			try
			{
				ISQL builder;
				ISQLExecuter executer;
				IConnectionInfo connInfo;

				switch ((int)userData)
				{
					// VistaDB
					case 0:
					{
						String path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
						path        = Path.Combine(path, "VistaDB");
						if (Directory.Exists(path))
							Directory.Delete(path, true);

						builder  = new VistaDB_SQL();
						executer = new VistaDB_SQLExecuter((VistaDB_SQL)builder, path);
						connInfo = new VistaDB_ConnectionInfo(true);
						break;
					}

					// DB2
					case 1:
					{
						builder  = new DB2_SQL();
						executer = new DB2_SQLExecuter((DB2_SQL)builder);
						connInfo = new DB2_ConnectionInfo("sei-backend", "testdb", "eisst", "EISSTEISST");
						break;
					}

					default:
						return;
				}

				// Create and start the runner
				DBRunner runner = new DBRunner();

				try
				{
					// Show provider
					IProvider prov = executer.Provider;
					Output("Provider: " + prov.Name + " - Version: " + prov.Version);
					Output("");

					// Create the database and test tables
					DBDatabase db = new DBDatabase("DBTest.MyTest");

					DBTable table1 = db.AddTable("Table1");
					table1.AddColumn("uiNoegle", ColumnType.Guid, ColumnFlag.PrimaryKey | ColumnFlag.NotNull);
					table1.AddColumn("txTekst", ColumnType.String, 20, ColumnFlag.None);
					table1.AddColumn("iTal", ColumnType.Int, ColumnFlag.PrimaryKey | ColumnFlag.NotNull);
					table1.AddColumn("dtDato", ColumnType.DateTime, ColumnFlag.IndexAsc);
					table1.AddColumn("sLilleTal", ColumnType.Small, ColumnFlag.None);
					table1.AddColumn("lStortTal", ColumnType.Long, ColumnFlag.NotNull);
					table1.AddColumn("txStorTekst", ColumnType.Clob, 32 * 1024, ColumnFlag.None);
					table1.AddColumn("bValg", ColumnType.Boolean, ColumnFlag.NotNull);
					table1.AddColumn("biBillede", ColumnType.Blob, 10 * 1024 * 1024, ColumnFlag.Compressed);
					table1.AddColumn("iAutoTaeller", ColumnType.Int, ColumnFlag.NotNull | ColumnFlag.Identity);

					DBTable table2 = db.AddTable("Table2");
					table2.AddColumn("uiNoegle", ColumnType.Guid, ColumnFlag.PrimaryKey | ColumnFlag.NotNull);
					table2.AddColumn("txTekst", ColumnType.String, 20, ColumnFlag.None);
					table2.AddColumn("iTal", ColumnType.Int, ColumnFlag.PrimaryKey | ColumnFlag.NotNull);
					table2.AddColumn("dtDato", ColumnType.DateTime, ColumnFlag.IndexAsc);
					table2.AddColumn("sLilleTal", ColumnType.Small, ColumnFlag.None);
					table2.AddColumn("lStortTal", ColumnType.Long, ColumnFlag.NotNull);
					table2.AddColumn("txStorTekst", ColumnType.Clob, 32 * 1024, ColumnFlag.None);
					table2.AddColumn("bValg", ColumnType.Boolean, ColumnFlag.NotNull);
					table2.AddColumn("biBillede", ColumnType.Blob, 10 * 1024 * 1024, ColumnFlag.Compressed);

					TestDatabase(runner, executer, db);
					TestConnection(runner, executer, db, connInfo);
					TestTable(runner, executer, builder, db, table2, connInfo);

					{
						Output("Create table again for other tests");
						DBConnection conn = runner.OpenConnection(executer, db, connInfo);

						try
						{
							SQL_CreateTableStatement sqlCreate = new SQL_CreateTableStatement(table1);
							runner.CreateTable(executer, conn, sqlCreate);

							sqlCreate = new SQL_CreateTableStatement(table2);
							runner.CreateTable(executer, conn, sqlCreate);
						}
						finally
						{
							conn.Close();
							Output("");
						}
					}

					TestTable2(runner, executer, db, "Table1", connInfo);
					TestSmallInsert(runner, executer, builder, db, table1, connInfo);
					TestSmallSelect(runner, executer, builder, db, table1, connInfo);
					TestSmallDelete(runner, executer, builder, db, table1, connInfo);
					TestTransactions(runner, executer, db, table2, connInfo);
					TestUpdate(runner, executer, builder, db, table2, connInfo);
					TestFunctions(runner, executer, builder, db, table2, connInfo);
					TestUnion(runner, executer, builder, db, table1, table2, connInfo);
					TestSQLBuild(builder, db, table1);

					{
						Output("Dropping testing tables");
						DBConnection conn = runner.OpenConnection(executer, db, connInfo);

						try
						{
							SQL_DropTableStatement sqlDrop = new SQL_DropTableStatement(table1);
							runner.DropTable(executer, conn, sqlDrop);

							sqlDrop = new SQL_DropTableStatement(table2);
							runner.DropTable(executer, conn, sqlDrop);
						}
						finally
						{
							conn.Close();
							Output("Done");
						}
					}
				}
				finally
				{
					runner.Close();
				}
			}
			catch(Exception ex)
			{
				Output("Whole test failed with an exception:");
				Output(ex);
			}
		}

		private delegate void Output_Delegate(String msg);

		private void OutputInvoke(String msg)
		{
			int index = outputListBox.Items.Add(msg);
			outputListBox.TopIndex = index;
			System.Diagnostics.Debug.WriteLine(msg);
		}

		private void Output(String str)
		{
			Invoke(new Output_Delegate(OutputInvoke), str);
		}

		private void Output(Exception ex)
		{
			foreach (String line in ex.ToString().Split('\n'))
				Output(line.Trim());
		}

		private void TestDatabase(DBRunner runner, ISQLExecuter executer, DBDatabase db)
		{
			Output("TestDatabase:");
			Output("");

			try
			{
				bool result = runner.DatabaseExists(executer, db);
				Output("Database exists: " + result + " / False");

				result = runner.CreateDatabase(executer, db);
				Output("Create database: " + (result ? "Ok" : "Failed"));

				if (result)
				{
					result = runner.DatabaseExists(executer, db);
					Output("Database exists: " + result + " / True");
				}
			}
			catch(Exception ex)
			{
				Output("TestDatabase failed with an exception:");
				Output(ex);
			}
			finally
			{
				Output("");
				Output("");
			}
		}

		private void TestConnection(DBRunner runner, ISQLExecuter executer, DBDatabase db, IConnectionInfo connInfo)
		{
			Output("TestConnection:");
			Output("");

			try
			{
				Output("Open connection");
				DBConnection conn = runner.OpenConnection(executer, db, connInfo);
				Output("Got connection");

				Output("Close connection");
				conn.Close();
				Output("Connection closed");
			}
			catch(Exception ex)
			{
				Output("TestConnection failed with an exception:");
				Output(ex);
			}
			finally
			{
				Output("");
				Output("");
			}
		}

		private void TestTable(DBRunner runner, ISQLExecuter executer, ISQL builder, DBDatabase db, DBTable table, IConnectionInfo connInfo)
		{
			Output("TestTable:");
			Output("");

			try
			{
				DBConnection conn = runner.OpenConnection(executer, db, connInfo);

				try
				{
					String name = table.TableName;

					bool result = runner.TableExists(executer, conn, name);
					Output("Table " + name + " exists: " + result + " / False");
					Output("");

					Output("Create table " + name);
					SQL_CreateTableStatement sqlCreate = new SQL_CreateTableStatement(table);
					Output(builder.ToSQL(sqlCreate));
					runner.CreateTable(executer, conn, sqlCreate);
					Output("Table created");
					Output("");

					result = runner.TableExists(executer, conn, name);
					Output("Table " + name + " exists: " + result + " / True");
					Output("");

					if (result)
					{
						Output("Drop table " + name);
						SQL_DropTableStatement sqlDrop = new SQL_DropTableStatement(table);
						Output(builder.ToSQL(sqlDrop));
						runner.DropTable(executer, conn, sqlDrop);
						Output("Table dropped");
						Output("");

						result = runner.TableExists(executer, conn, name);
						Output("Table " + name + " exists: " + result + " / False");
					}
				}
				finally
				{
					conn.Close();
				}
			}
			catch(Exception ex)
			{
				Output("TestTable failed with an exception:");
				Output(ex);
			}
			finally
			{
				Output("");
				Output("");
			}
		}

		private void TestTable2(DBRunner runner, ISQLExecuter executer, DBDatabase db, String tableName, IConnectionInfo connInfo)
		{
			Output("TestTable 2:");
			Output("");

			try
			{
				Output("Getting metadata for table " + tableName);

				using (DBConnection conn = runner.OpenConnection(executer, db, connInfo))
				{
					DBTable table = runner.GetTableMetaData(executer, conn, db, tableName);
					if (table == null)
						Output("No metadata found");
					else
						Output("Found metadata for table: " + table.TableName + " - Columns: " + table.NumberOfColumns);
				}

				Output("");
				Output("Getting metadata");
				DBDatabase newDB = new DBDatabase(db.DatabaseName);
				runner.GetMetaData(executer, newDB, connInfo);

				if (newDB.NumberOfTables == 0)
					Output("No tables found");
				else
				{
					foreach (DBTable table in newDB.Tables)
						Output("Table: " + table.TableName + " - Columns: " + table.NumberOfColumns);
				}
			}
			catch(Exception ex)
			{
				Output("TestTable 2 failed with an exception:");
				Output(ex);
			}
			finally
			{
				Output("");
				Output("");
			}
		}

		private void TestSmallInsert(DBRunner runner, ISQLExecuter executer, ISQL builder, DBDatabase db, DBTable table, IConnectionInfo connInfo)
		{
			Output("TestSmallInsert:");
			Output("");

			try
			{
				DBConnection conn = runner.OpenConnection(executer, db, connInfo);

				try
				{
					Output("Insert single row");
					SQL_InsertStatement sqlInsert = new SQL_InsertStatement(table);
					sqlInsert.AddColumns("uiNoegle", "iTal", "lStortTal", "dtDato", "bValg");
					sqlInsert.AddValues(Guid.NewGuid(), 87, (long)2394287487, DateTime.Now, false);
					Output(builder.ToSQL(sqlInsert));
					runner.Insert(executer, conn, sqlInsert);
					Output("Row inserted");
				}
				finally
				{
					conn.Close();
				}
			}
			catch(Exception ex)
			{
				Output("TestSmallInsert failed with an exception:");
				Output(ex);
			}
			finally
			{
				Output("");
				Output("");
			}
		}

		private void TestSmallSelect(DBRunner runner, ISQLExecuter executer, ISQL builder, DBDatabase db, DBTable table, IConnectionInfo connInfo)
		{
			Output("TestSmallSelect:");
			Output("");

			try
			{
				DBConnection conn = runner.OpenConnection(executer, db, connInfo);

				try
				{
					Output("Get count");
					SQL_SelectStatement sqlSelect = new SQL_SelectStatement();
					sqlSelect.AddTable(table);
					sqlSelect.AddAggregate(new Aggre_Count());
					Output(builder.ToSQL(sqlSelect));
					long result = runner.SelectWithSingleAggregate(executer, conn, sqlSelect);
					Output("Count: " + result + " / 1");
				}
				finally
				{
					conn.Close();
				}
			}
			catch(Exception ex)
			{
				Output("TestSmallSelect failed with an exception:");
				Output(ex);
			}
			finally
			{
				Output("");
				Output("");
			}
		}

		private void TestSmallDelete(DBRunner runner, ISQLExecuter executer, ISQL builder, DBDatabase db, DBTable table, IConnectionInfo connInfo)
		{
			Output("TestSmallDelete:");
			Output("");

			try
			{
				DBConnection conn = runner.OpenConnection(executer, db, connInfo);

				try
				{
					Output("Delete all rows");
					SQL_DeleteStatement sqlDelete = new SQL_DeleteStatement(table);
					Output(builder.ToSQL(sqlDelete));
					runner.Delete(executer, conn, sqlDelete);
					Output("Rows deleted");

					SQL_SelectStatement sqlSelect = new SQL_SelectStatement();
					sqlSelect.AddTable(table);
					sqlSelect.AddAggregate(new Aggre_Count());
					long result = runner.SelectWithSingleAggregate(executer, conn, sqlSelect);
					Output("Count: " + result + " / 0");
				}
				finally
				{
					conn.Close();
				}
			}
			catch(Exception ex)
			{
				Output("TestSmallDelete failed with an exception:");
				Output(ex);
			}
			finally
			{
				Output("");
				Output("");
			}
		}

		private void TestTransactions(DBRunner runner, ISQLExecuter executer, DBDatabase db, DBTable table, IConnectionInfo connInfo)
		{
			Output("TestTransactions:");
			Output("");

			try
			{
				DBConnection conn = runner.OpenConnection(executer, db, connInfo);

				try
				{
					SQL_InsertStatement sqlInsert;
					SQL_SelectStatement sqlSelect;
					long result;

					Output("Begin transaction");
					DBTransaction trans = runner.CreateTransaction(executer);
					trans.Begin(conn);

					try
					{
						Output("Insert row");
						sqlInsert = new SQL_InsertStatement(table);
						sqlInsert.AddColumns("uiNoegle", "iTal", "lStortTal", "dtDato", "bValg");
						sqlInsert.AddValues(Guid.NewGuid(), 87, (long)2394287487, DateTime.Now, false);
						runner.Insert(executer, conn, sqlInsert);

						sqlSelect = new SQL_SelectStatement();
						sqlSelect.AddTable(table);
						sqlSelect.AddAggregate(new Aggre_Count());
						result = runner.SelectWithSingleAggregate(executer, conn, sqlSelect);
						Output("Count: " + result + " / 1");

						Output("Rollback");
						trans.RollbackAll();
					}
					catch(Exception)
					{
						trans.RollbackAll();
						throw;
					}

					sqlSelect = new SQL_SelectStatement();
					sqlSelect.AddTable(table);
					sqlSelect.AddAggregate(new Aggre_Count());
					result = runner.SelectWithSingleAggregate(executer, conn, sqlSelect);
					Output("Count: " + result + " / 0");
					Output("");

					Output("Begin new transaction");
					trans = runner.CreateTransaction(executer);
					trans.Begin(conn);

					try
					{
						Output("Insert row");
						sqlInsert = new SQL_InsertStatement(table);
						sqlInsert.AddColumns("uiNoegle", "iTal", "lStortTal", "dtDato", "bValg");
						sqlInsert.AddValues(Guid.NewGuid(), 87, (long)2394287487, DateTime.Now, false);
						runner.Insert(executer, conn, sqlInsert);

						sqlSelect = new SQL_SelectStatement();
						sqlSelect.AddTable(table);
						sqlSelect.AddAggregate(new Aggre_Count());
						result = runner.SelectWithSingleAggregate(executer, conn, sqlSelect);
						Output("Count: " + result + " / 1");

						Output("Commit");
						trans.CommitAll();
					}
					catch(Exception)
					{
						trans.RollbackAll();
						throw;
					}

					sqlSelect = new SQL_SelectStatement();
					sqlSelect.AddTable(table);
					sqlSelect.AddAggregate(new Aggre_Count());
					result = runner.SelectWithSingleAggregate(executer, conn, sqlSelect);
					Output("Count: " + result + " / 1");
				}
				finally
				{
					conn.Close();
				}
			}
			catch(Exception ex)
			{
				Output("TestTransactions failed with an exception:");
				Output(ex);
			}
			finally
			{
				Output("");
				Output("");
			}
		}

		private void ShowContents(DBRunner runner, ISQLExecuter executer, DBConnection conn, DBTable table)
		{
			Output("Display contents of table " + table.TableName);

			SQL_SelectStatement sqlSelect = new SQL_SelectStatement();
			sqlSelect.AddAllColumns(table);

			ShowContents(sqlSelect, runner, executer, conn);
		}

		private void ShowContents(SQL_SelectStatement statement, DBRunner runner, ISQLExecuter executer, DBConnection conn)
		{
			DBRowCollection coll = runner.Select(executer, conn, statement);

			String line = "";
			foreach (DBMetaColumn meta in coll.Meta)
				line += meta.ColumnName + ", ";

			Output(line.Substring(0, line.Length - 2));

			foreach (DBRow r in coll)
			{
				line = "";
				int count = r.Count;
				for (int i = 0; i < count; i++)
					line += r[i] == null ? "null, " : (r[i] + ", ");

				Output(line.Substring(0, line.Length - 2));
			}
		}

		private void TestUpdate(DBRunner runner, ISQLExecuter executer, ISQL builder, DBDatabase db, DBTable table, IConnectionInfo connInfo)
		{
			Output("TestUpdate:");
			Output("");

			try
			{
				DBConnection conn = runner.OpenConnection(executer, db, connInfo);

				try
				{
					Output("Insert more rows");
					SQL_InsertStatement sqlInsert = new SQL_InsertStatement(table);
					sqlInsert.AddAllColumns();
					sqlInsert.AddValues(Guid.NewGuid(), "Dette er en tekst", 42, DateTime.Now, null, 6576547634);
					sqlInsert.AddParameter("MEGET STOR TEKST");
					sqlInsert.AddValue(true);
					sqlInsert.AddParameter(new byte[432]);
					Output(builder.ToSQL(sqlInsert));
					runner.Insert(executer, conn, sqlInsert);
					Output("Rows inserted");
					Output("");

					ShowContents(runner, executer, conn, table);
					Output("");

					Output("Update 1");
					SQL_UpdateStatement sqlUpdate = new SQL_UpdateStatement(table);
					sqlUpdate.AddColumns("txTekst", "iTal");
					sqlUpdate.AddValues("En ny tekst", 534);
					sqlUpdate.AddCriteria(new Crit_MatchCriteria(table, "iTal", MatchType.Equal, 42));
					Output(builder.ToSQL(sqlUpdate));
					runner.Update(executer, conn, sqlUpdate);
					Output("");

					ShowContents(runner, executer, conn, table);
					Output("");

					Output("Update 2");
					sqlUpdate = new SQL_UpdateStatement(table);
					sqlUpdate.AddColumn("txStorTekst");
					sqlUpdate.AddParameter("DETTE STÅR MED STORT!");
					sqlUpdate.AddCriteria(new Crit_MatchCriteria(table, "txStorTekst", MatchType.IsNull));
					Output(builder.ToSQL(sqlUpdate));
					runner.Update(executer, conn, sqlUpdate);
					Output("");

					ShowContents(runner, executer, conn, table);
					Output("");

					SQL_SelectStatement sqlSelect = new SQL_SelectStatement();
					sqlSelect.AddTable(table);
					sqlSelect.AddFunction(new Func_SubString(table, "txTekst", 3, 8));
					Output(builder.ToSQL(sqlSelect));
					ShowContents(sqlSelect, runner, executer, conn);
					Output("");

					Output("Delete");
					SQL_DeleteStatement sqlDelete = new SQL_DeleteStatement(table);
					sqlDelete.AddCriteria(new Crit_MatchCriteria(table, "bValg", MatchType.Equal, false));
					Output(builder.ToSQL(sqlDelete));
					runner.Delete(executer, conn, sqlDelete);
					Output("");

					ShowContents(runner, executer, conn, table);
				}
				finally
				{
					conn.Close();
				}
			}
			catch(Exception ex)
			{
				Output("TestUpdate failed with an exception:");
				Output(ex);
			}
			finally
			{
				Output("");
				Output("");
			}
		}

		private void TestFunctions(DBRunner runner, ISQLExecuter executer, ISQL builder, DBDatabase db, DBTable table, IConnectionInfo connInfo)
		{
			Output("TestFunctions:");
			Output("");

			try
			{
				DBConnection conn = runner.OpenConnection(executer, db, connInfo);

				try
				{
					Output("Insert single row");
					SQL_InsertStatement sqlInsert = new SQL_InsertStatement(table);
					sqlInsert.AddColumns("uiNoegle", "iTal", "lStortTal", "dtDato", "bValg");
					sqlInsert.AddValues(Guid.NewGuid(), 87, (long)2394287487, DateTime.Now, false);
					Output(builder.ToSQL(sqlInsert));
					runner.Insert(executer, conn, sqlInsert);

					sqlInsert = new SQL_InsertStatement(table);
					sqlInsert.AddColumns("uiNoegle", "txTekst", "iTal", "lStortTal", "dtDato", "bValg");
					sqlInsert.AddValues(Guid.NewGuid(), "Blåbærgrød", 87, (long)2394287487, DateTime.Now, false);
					Output(builder.ToSQL(sqlInsert));
					runner.Insert(executer, conn, sqlInsert);
					Output("Rows inserted");
					Output("");

					ShowContents(runner, executer, conn, table);
					Output("");

					SQL_SelectStatement sqlSelect = new SQL_SelectStatement();
					sqlSelect.AddTable(table);
					IFunction func = new Func_SubString(table, "txTekst", 3, 8);
					sqlSelect.AddFunction(new Func_SubString(func, 0, 2));
					Output(builder.ToSQL(sqlSelect));
					ShowContents(sqlSelect, runner, executer, conn);
					Output("");

					sqlSelect = new SQL_SelectStatement();
					sqlSelect.AddTable(table);
					sqlSelect.AddFunction(new Func_ToLower(table, "txTekst"));
					Output(builder.ToSQL(sqlSelect));
					ShowContents(sqlSelect, runner, executer, conn);
					Output("");

					sqlSelect = new SQL_SelectStatement();
					sqlSelect.AddTable(table);
					sqlSelect.AddFunction(new Func_ToUpper(table, "txTekst"));
					Output(builder.ToSQL(sqlSelect));
					ShowContents(sqlSelect, runner, executer, conn);
				}
				finally
				{
					conn.Close();
				}
			}
			catch(Exception ex)
			{
				Output("TestUpdate failed with an exception:");
				Output(ex);
			}
			finally
			{
				Output("");
				Output("");
			}
		}

		private void TestUnion(DBRunner runner, ISQLExecuter executer, ISQL builder, DBDatabase db, DBTable table1, DBTable table2, IConnectionInfo connInfo)
		{
			Output("TestUnion:");
			Output("");

			try
			{
				DBConnection conn = runner.OpenConnection(executer, db, connInfo);

				try
				{
					Output("Insert more test rows");
					SQL_InsertStatement sqlInsert = new SQL_InsertStatement(table1);
					sqlInsert.AddColumns("uiNoegle", "txTekst", "iTal", "lStortTal", "dtDato", "bValg");
					sqlInsert.AddValues(Guid.NewGuid(), "Giv mig en bog!", 123, (long)213142566, DateTime.Now, true);
					Output(builder.ToSQL(sqlInsert));
					runner.Insert(executer, conn, sqlInsert);
					Output("Rows inserted");
					Output("");

					ShowContents(runner, executer, conn, table1);
					Output("");
					ShowContents(runner, executer, conn, table2);
					Output("");

					SQL_SelectStatement sqlSelect1 = new SQL_SelectStatement();
					sqlSelect1.AddColumns(table1, "uiNoegle", "txTekst", "iTal", "lStortTal");

					SQL_SelectStatement sqlSelect2 = new SQL_SelectStatement();
					sqlSelect2.AddColumns(table2, "uiNoegle", "txTekst", "iTal", "lStortTal");

					SQL_SelectStatement sqlUnion = new SQL_SelectStatement();
					sqlUnion.AddColumns(table1, "uiNoegle", "txTekst", "iTal", "lStortTal");
					sqlUnion.AddUnion(sqlSelect1);
					sqlUnion.AddUnion(sqlSelect2);
					sqlUnion.AddSort(table1, "iTal", Order.Ascending);
					Output(builder.ToSQL(sqlUnion));
					ShowContents(sqlUnion, runner, executer, conn);
				}
				finally
				{
					conn.Close();
				}
			}
			catch(Exception ex)
			{
				Output("TestUpdate failed with an exception:");
				Output(ex);
			}
			finally
			{
				Output("");
				Output("");
			}
		}

		private void TestSQLBuild(ISQL builder, DBDatabase db, DBTable table)
		{
			Output("TestSQLBuild:");
			Output("");

			try
			{
				Output("Criterias:");
				ICriteria crit1 = new Crit_MatchCriteria(table, "lStortTal", MatchType.Equal, 6576547634);
				ICriteria crit2 = new Crit_MatchCriteria(table, "txTekst", MatchType.Different, "Bent");
				ICriteria crit3 = new Crit_MatchCriteria(table, "sLilleTal", MatchType.IsNull);

				SQL_SelectStatement sqlSelect = new SQL_SelectStatement();
				sqlSelect.AddAllColumns(table);
				sqlSelect.AddCriteria(crit1);
				sqlSelect.AddCriteria(crit2);
				sqlSelect.AddCriteria(crit3);
				Output(builder.ToSQL(sqlSelect));

				sqlSelect = new SQL_SelectStatement();
				sqlSelect.AddAllColumns(table);
				sqlSelect.AddCriteria(new Crit_Or(crit1, crit2));
				sqlSelect.AddCriteria(crit3);
				Output(builder.ToSQL(sqlSelect));

				sqlSelect = new SQL_SelectStatement();
				sqlSelect.AddAllColumns(table);
				ICriteria tempCrit = new Crit_And(crit2, crit3);
				sqlSelect.AddCriteria(new Crit_Or(crit1, tempCrit));
				Output(builder.ToSQL(sqlSelect));

				sqlSelect = new SQL_SelectStatement();
				sqlSelect.AddAllColumns(table);
				sqlSelect.AddCriteria(new Crit_Or(new Crit_Or(crit1, crit2), crit3));
				Output(builder.ToSQL(sqlSelect));

				sqlSelect = new SQL_SelectStatement();
				sqlSelect.AddAllColumns(table);
				sqlSelect.AddCriteria(crit1);
				sqlSelect.AddCriteria(crit2);
				sqlSelect.AddCriteria(new Crit_InCriteria(table, "iTal", true, 3, 5, 254, 31));
				Output(builder.ToSQL(sqlSelect));

				SQL_SelectStatement sqlSelect1 = new SQL_SelectStatement();
				sqlSelect1.AddColumn(table, "iTal");

				sqlSelect = new SQL_SelectStatement();
				sqlSelect.AddAllColumns(table);
				sqlSelect.AddCriteria(new Crit_SubQueryCriteria(table, "iTal", sqlSelect1));
				Output(builder.ToSQL(sqlSelect));

				sqlSelect = new SQL_SelectStatement();
				sqlSelect.AddAllColumns(table);
				sqlSelect.AddCriteria(new Crit_SubQueryCriteria(table, "iTal", true, sqlSelect1));
				Output(builder.ToSQL(sqlSelect));
				Output("");

				Output("Aggregates:");
				sqlSelect = new SQL_SelectStatement();
				sqlSelect.AddColumn(table, "iTal");
				sqlSelect.Distinct = true;
				Output(builder.ToSQL(sqlSelect));

				sqlSelect = new SQL_SelectStatement();
				sqlSelect.AddTable(table);
				sqlSelect.AddAggregate(new Aggre_Count());
				Output(builder.ToSQL(sqlSelect));

				sqlSelect = new SQL_SelectStatement();
				sqlSelect.AddTable(table);
				sqlSelect.AddAggregate(new Aggre_Count(table, "iTal"));
				Output(builder.ToSQL(sqlSelect));

				sqlSelect = new SQL_SelectStatement();
				sqlSelect.AddTable(table);
				sqlSelect.AddAggregate(new Aggre_Max(table, "iTal"));
				Output(builder.ToSQL(sqlSelect));

				sqlSelect = new SQL_SelectStatement();
				sqlSelect.AddTable(table);
				sqlSelect.AddAggregate(new Aggre_Min(table, "iTal"));
				Output(builder.ToSQL(sqlSelect));
				Output("");

				Output("Create tables:");
				DBTable employees = db.AddTable("Employees");
				employees.AddColumn("Employee_ID", ColumnType.String, 2, ColumnFlag.PrimaryKey | ColumnFlag.NotNull);
				employees.AddColumn("Name", ColumnType.String, 50, ColumnFlag.NotNull);

				DBTable orders = db.AddTable("Orders");
				orders.AddColumn("Prod_ID", ColumnType.Int, ColumnFlag.PrimaryKey | ColumnFlag.NotNull);
				orders.AddColumn("Product", ColumnType.String, 50, ColumnFlag.NotNull | ColumnFlag.IndexUnique);
				orders.AddColumn("Employee_ID", ColumnType.String, 2, ColumnFlag.NotNull);

				DBTable storage = db.AddTable("Storage");
				storage.AddColumn("Storage_ID", ColumnType.Int, ColumnFlag.PrimaryKey | ColumnFlag.NotNull);
				storage.AddColumn("Prod_ID", ColumnType.Int, ColumnFlag.NotNull);
				storage.AddColumn("Count", ColumnType.Int, ColumnFlag.NotNull | ColumnFlag.IndexDesc);

				SQL_CreateTableStatement sqlCreate = new SQL_CreateTableStatement(employees);
				Output(builder.ToSQL(sqlCreate));

				sqlCreate = new SQL_CreateTableStatement(orders);
				Output(builder.ToSQL(sqlCreate));

				sqlCreate = new SQL_CreateTableStatement(storage);
				Output(builder.ToSQL(sqlCreate));
				Output("");

				Output("Joins:");
				sqlSelect = new SQL_SelectStatement();
				sqlSelect.AddColumn(employees, "Name");
				sqlSelect.AddColumn(orders, "Product");
				sqlSelect.AddJoin(new Join_Inner(employees, "Employee_ID", orders, "Employee_ID"));
				sqlSelect.AddColumn(storage, "Count");
				sqlSelect.AddJoin(new Join_Inner(orders, "Prod_ID", storage, "Prod_ID"));
				sqlSelect.AddCriteria(new Crit_MatchCriteria(storage, "Count", MatchType.Bigger, 10));
				sqlSelect.AddSort(employees, "Name", Order.Ascending);
				sqlSelect.AddSort(orders, "Product", Order.Descending);
				Output(builder.ToSQL(sqlSelect));

				sqlSelect = new SQL_SelectStatement();
				sqlSelect.AddColumn(employees, "Name");
				sqlSelect.AddColumn(orders, "Product");
				sqlSelect.AddJoin(new Join_Left(employees, "Employee_ID", orders, "Employee_ID"));
				Output(builder.ToSQL(sqlSelect));

				sqlSelect = new SQL_SelectStatement();
				sqlSelect.AddColumn(employees, "Name");
				sqlSelect.AddColumn(orders, "Product");
				sqlSelect.AddJoin(new Join_Right(employees, "Employee_ID", orders, "Employee_ID"));
				Output(builder.ToSQL(sqlSelect));
				Output("");

				Output("Misc");
				DBTable employees1 = db.AddTable("Employees1");
				employees1.AddColumn("Employee_ID", ColumnType.String, 2, ColumnFlag.PrimaryKey | ColumnFlag.NotNull);
				employees1.AddColumn("Name", ColumnType.String, 50, ColumnFlag.NotNull);

				sqlSelect = new SQL_SelectStatement();
				sqlSelect.AddAllColumns(employees);
				SQL_InsertStatement sqlInsert = new SQL_InsertStatement(employees1);
				sqlInsert.InsertFromSelect = sqlSelect;
				Output(builder.ToSQL(sqlInsert));
			}
			catch(Exception ex)
			{
				Output("TestSQLBuild failed with an exception:");
				Output(ex);
			}
			finally
			{
				Output("");
				Output("");
			}
		}
	}
}
