using System;
using System.Data;
using System.IO;
using TietoEnator.Data.SqlClient.Builder;
using TietoEnator.Data.SqlClient.Builder.Aggregates;
using TietoEnator.Data.SqlClient.Builder.Criterias;
using TietoEnator.Data.SqlClient.Builder.Statements;
using TietoEnator.Data.SqlClient.DB2;
using TietoEnator.Data.SqlClient.Runner;

namespace CleanMortalityGroupmails
{
	public class Program
	{
		public static void Main(String[] args)
		{
			try
			{
				DBRunner runner       = new DBRunner();
				ISQLExecuter executer = new DB2_SQLExecuter(new DB2_SQL());
				DBDatabase database   = new DBDatabase("EISST");

				// Open connection to the database
				Output("Open connection to database");

				IConnectionInfo connInfo = new DB2_ConnectionInfo("vagn", "EISST", "EISST", "EISSTEISST");
				DBConnection connection  = runner.OpenConnection(executer, database, connInfo);

				try
				{
					// Extract meta data for all the tables we need
					Output("Extract table metadata");

					DBTable pluginsTable = runner.GetTableMetaData(executer, connection, database, "EISST", "Plugins");
					database.AddTable(pluginsTable);

					DBTable grupperTable = runner.GetTableMetaData(executer, connection, database, "EISST", "Grupper");
					database.AddTable(grupperTable);

					DBTable gruppePostkasseTable = runner.GetTableMetaData(executer, connection, database, "EISST", "GruppePostkasse");
					database.AddTable(gruppePostkasseTable);

					DBTable side1Table = runner.GetTableMetaData(executer, connection, database, "EISST", "Mortality_Side1");
					database.AddTable(side1Table);

					DBTable side2Table = runner.GetTableMetaData(executer, connection, database, "EISST", "Mortality_Side2");
					database.AddTable(side2Table);

					Output("Find needed ids");

					// Find the plug-in id for mortality documents
					Guid pluginID = FindPluginID(pluginsTable, runner, executer, connection);
					if (pluginID.Equals(Guid.Empty))
					{
						Output("Couldn't find plug-in ID");
						return;
					}

					Output("Plug-in ID: " + pluginID);

					// Find the group id where the documents are stored
					Guid groupID = FindGroupID(grupperTable, runner, executer, connection);
					if (groupID.Equals(Guid.Empty))
					{
						Output("Couldn't find group ID");
						return;
					}

					Output("Group ID: " + groupID);

					Output("Delete documents that are stored in the database");
					FixMailBox(pluginID, groupID, gruppePostkasseTable, side1Table, side2Table, runner, executer, connection);
				}
				finally
				{
					Output("Close connection");
					connection.Close();
				}
			}
			catch(Exception ex)
			{
				Output("Exception failure: " + ex);
			}
		}


		private static void Output(String msg)
		{
			Console.WriteLine(msg);
			System.Diagnostics.Debug.WriteLine(msg);
		}

		private static Guid FindPluginID(DBTable pluginsTable, DBRunner runner, ISQLExecuter executer, DBConnection connection)
		{
			SQL_SelectStatement sqlSelect = new SQL_SelectStatement();
			sqlSelect.AddColumn(pluginsTable, "uiPluginID");
			sqlSelect.AddCriteria(new Crit_MatchCriteria(pluginsTable, "txNavn", MatchType.Equal, "dk.hob.ei.mortality.Plugin"));
			DBRow row = runner.SelectAndReturnFirstRow(executer, connection, sqlSelect);
			if (row == null)
				return Guid.Empty;

			return (Guid)row["uiPluginID"];
		}


		private static Guid FindGroupID(DBTable gruppeTable, DBRunner runner, ISQLExecuter executer, DBConnection connection)
		{
			SQL_SelectStatement sqlSelect = new SQL_SelectStatement();
			sqlSelect.AddColumn(gruppeTable, "uiGruppeID");
			sqlSelect.AddCriteria(new Crit_MatchCriteria(gruppeTable, "txNavn", MatchType.Equal, "Dødsattester midlertidig opbevaring"));
			DBRow row = runner.SelectAndReturnFirstRow(executer, connection, sqlSelect);
			if (row == null)
				return Guid.Empty;

			return (Guid)row["uiGruppeID"];
		}


		private static void FixMailBox(Guid pluginID, Guid groupID, DBTable gruppePostkasseTable, DBTable side1Table, DBTable side2Table, DBRunner runner, ISQLExecuter executer, DBConnection connection)
		{
			SQL_SelectStatement sqlSelect = new SQL_SelectStatement();
			sqlSelect.AddColumns(gruppePostkasseTable, "uiBrevID", "uiSkemaID", "txBesked");
			sqlSelect.AddCriteria(new Crit_MatchCriteria(gruppePostkasseTable, "uiGruppeID", MatchType.Equal, groupID));
			sqlSelect.AddCriteria(new Crit_MatchCriteria(gruppePostkasseTable, "uiPluginID", MatchType.Equal, pluginID));

			using (DBReader reader = runner.GetReader(executer, connection, sqlSelect))
			{
				DBRow row;
                int count = 0;

                try
                {
                    while ((row = reader.GetNextRow()) != null)
                    {
                        Guid documentID = (Guid)row["uiSkemaID"];

                        if (HasID(side1Table, documentID, runner, executer, connection))
                        {
                            if (HasID(side2Table, documentID, runner, executer, connection) || OnlyOnePage((String)row["txBesked"]))
                            {
                                // Delete the document
                                Output("Delete document " + documentID);
                                SQL_DeleteStatement sqlDelete = new SQL_DeleteStatement(gruppePostkasseTable);
                                sqlDelete.AddCriteria(new Crit_MatchCriteria(gruppePostkasseTable, "uiBrevID", MatchType.Equal, row["uiBrevID"]));
                                //runner.Delete(executer, connection, sqlDelete);
                                count++;
                            }
                        }
                    }
                }
                finally
                {
                    Output("Deleted " + count + " documents");
                }
			}
		}


		private static bool HasID(DBTable table, Guid documentID, DBRunner runner, ISQLExecuter executer, DBConnection connection)
		{
			SQL_SelectStatement sqlSelect = new SQL_SelectStatement();
			sqlSelect.AddTable(table);
			sqlSelect.AddAggregate(new Aggre_Count());
			sqlSelect.AddCriteria(new Crit_MatchCriteria(table, "uiSkemaID", MatchType.Equal, documentID));

			return runner.SelectWithSingleAggregate(executer, connection, sqlSelect) > 0;
		}


		private static bool OnlyOnePage(String xml)
		{
            DataSet ds = new DataSet();

            using (StringReader sr = new StringReader(xml))
            {
                ds.ReadXml(sr);
                ds.AcceptChanges();
            }

            try
            {
                DataTable side1 = ds.Tables["Mortality_Side1"];
                if (side1.Columns.Contains("bSide2"))
                    return !((bool)side1.Rows[0]["bSide2"]);

                DataTable side2 = ds.Tables["Mortality_Side2"];
                if (side2.Rows.Count == 0)
                    return true;

                DataRow row2 = side2.Rows[0];
                if (row2["txDodsaarsagA"].Equals("") && row2["txDodsaarsagB"].Equals("") && row2["txDodsaarsagC"].Equals("") && row2["txDodsaarsagD"].Equals(""))
                    return true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return false;
        }
	}
}
