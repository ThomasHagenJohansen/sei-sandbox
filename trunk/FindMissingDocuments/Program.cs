using System;
using System.Collections.Generic;
using System.Text;
using TietoEnator.Data.SqlClient.Builder;
using TietoEnator.Data.SqlClient.Builder.Criterias;
using TietoEnator.Data.SqlClient.Builder.Statements;
using TietoEnator.Data.SqlClient.DB2;
using TietoEnator.Data.SqlClient.Runner;

namespace FindMissingDocuments
{
	public static class Program
	{
		public static void Main()
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

					DBTable gruppePostkasseTable = runner.GetTableMetaData(executer, connection, database, "EISST", "GruppePostkasse");
					database.AddTable(gruppePostkasseTable);

					DBTable revisionssporTable = runner.GetTableMetaData(executer, connection, database, "EISST", "Revisionsspor");
					database.AddTable(revisionssporTable);

					FindDocuments(pluginsTable, gruppePostkasseTable, revisionssporTable, runner, executer, connection, "dk.hob.ei.mortality.Plugin", "Mortality_Side1", "Mortality_Side2");
//					FindDocuments(pluginsTable, gruppePostkasseTable, revisionssporTable, runner, executer, connection, "dk.hob.ei.sengepladser.Plugin", "Sengepladser_Grundoplysninger");
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


		private static void FindDocuments(DBTable pluginsTable, DBTable gruppePostkasseTable, DBTable revisionssporTable, DBRunner runner, ISQLExecuter executer, DBConnection connection, String plugin, params String[] tables)
		{
			Output("");
			Output("Find plugin ID for " + plugin);

			SQL_SelectStatement sqlSelect = new SQL_SelectStatement();
			sqlSelect.AddColumn(pluginsTable, "uiPluginID");
			sqlSelect.AddCriteria(new Crit_MatchCriteria(pluginsTable, "txNavn", MatchType.Equal, plugin));
			DBRow row = runner.SelectAndReturnFirstRow(executer, connection, sqlSelect);
			if (row == null)
				throw new Exception("Couldn't find plugin ID");

			Guid pluginID = (Guid)row["uiPluginID"];

			// Find all the document IDs
			Dictionary<Guid, Object> documentIDs = new Dictionary<Guid, Object>();

			Output("Find document IDs for " + gruppePostkasseTable.TableName);
			sqlSelect = new SQL_SelectStatement();
			sqlSelect.Distinct = true;
			sqlSelect.AddColumn(gruppePostkasseTable, "uiSkemaID");
			sqlSelect.AddCriteria(new Crit_MatchCriteria(gruppePostkasseTable, "uiPluginID", MatchType.Equal, pluginID));
			AddIDs(sqlSelect, runner, executer, connection, documentIDs);

			foreach (String tableName in tables)
			{
				Output("Find document IDs for " + tableName);
				DBTable table = pluginsTable.Database.AddTable("EISST", tableName);
				table.AddColumn("uiSkemaID", ColumnType.Guid, ColumnFlag.None);

				sqlSelect = new SQL_SelectStatement();
				sqlSelect.AddColumn(table, "uiSkemaID");
				AddIDs(sqlSelect, runner, executer, connection, documentIDs);
			}

			Output("Check the revisionsspor to see which documents are missing");
			sqlSelect = new SQL_SelectStatement();
			sqlSelect.Distinct = true;
			sqlSelect.AddColumn(revisionssporTable, "uiSkemaID");
			sqlSelect.AddCriteria(new Crit_MatchCriteria(revisionssporTable, "uiPluginID", MatchType.Equal, pluginID));
			sqlSelect.AddCriteria(new Crit_MatchCriteria(revisionssporTable, "iType", MatchType.Equal, (short)0));
			sqlSelect.AddCriteria(new Crit_MatchCriteria(revisionssporTable, "uiSkemaID", MatchType.IsNotNull));
			sqlSelect.AddSort(revisionssporTable, "uiSkemaID", Order.Ascending);

			using (DBReader reader = runner.GetReader(executer, connection, sqlSelect))
			{
				while ((row = reader.GetNextRow()) != null)
				{
					Guid id = (Guid)row["uiSkemaID"];

					if (!documentIDs.ContainsKey(id))
						Output(id.ToString("B").ToUpper());
				}
			}
		}



		private static void AddIDs(SQL_SelectStatement sqlSelect, DBRunner runner, ISQLExecuter executer, DBConnection connection, Dictionary<Guid, Object> ids)
		{
			using (DBReader reader = runner.GetReader(executer, connection, sqlSelect))
			{
				DBRow row;

				while ((row = reader.GetNextRow()) != null)
				{
					Guid id = (Guid)row["uiSkemaID"];
					ids[id] = null;
				}
			}
		}
	}
}
