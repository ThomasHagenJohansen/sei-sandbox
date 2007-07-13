		package tietoenator.hcw.sei.investigation;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.PrintWriter;
import java.sql.Connection;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import dk.hob.data.ExtDataSet;
import dk.hob.filesystem.FileSystem;
import dk.hob.resources.AppConfig;
import dk.hob.sqldatabase.Database;
import dk.hob.sqldatabase.SQL;
import dk.sst.ei.Globals;
import dk.sst.ei.SessionContext;

public class MortalityInvestigation {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		try
		{
			Globals.Cfg = new AppConfig("C:\\Development\\KTI\\DriftEIBackend.config");

			// Set up a session context
			SessionContext ctx=new SessionContext();
			Database db = new Database(Database.DriverType.IBM_DB2_NET);
	        Connection Conn = db.Connect("localhost","6789","EISST","EISST","EISSTEISST");

	        ExtDataSet eds = new ExtDataSet(Conn);
	        //---
//	        try
//	        {
//	        	String ss=FileSystem.LoadString("c:\\Development\\mortuids.txt");
//	        	String[] as=ss.split("\r\n");
//	        	for(int i=0;i<as.length;i++)
//	        	{
//	        		eds.Execute("SELECT dtOprettet FROM Revisionsspor where uiSkemaID="+SQL.FmtGuid(as[i])+" order by dtOprettet desc Fetch first 1 rows only");
//	        		if(eds.Read())
//	        			System.out.println(eds.GetValue(0));
//	        	}
//	        	
//	        }
//	        catch(Exception e)
//	        {
//	        	
//	        }
//	        finally
//	        {
//	        	eds.Close();
//	        }
//        	System.exit(0);
	        //---

	        //---
	        eds = new ExtDataSet(Conn);
	        try
	        {
	        	Set allIds = new HashSet();
	        	//ArrayList alSrc = new ArrayList();
	        	String sql = "SELECT uiSkemaID FROM Mortality_Side1";
	        	eds.Execute(sql);
	        	while(eds.Read())
	        	{
	        		if(!allIds.contains(eds.GetValue(0)))
	        		{
	        			allIds.add(eds.GetValue(0));
	        		}
	        	}
	        	PrintSize(allIds);
	        	
	        	sql = "SELECT uiSkemaID FROM Mortality_Side2";
	        	eds.Execute(sql);
	        	while(eds.Read())
	        	{
	        		if(!allIds.contains(eds.GetValue(0)))
	        			allIds.add(eds.GetValue(0));
	        	}
	        	PrintSize(allIds);
	        	
	        	sql = "SELECT uiSkemaID FROM Gruppepostkasse WHERE uiPluginID='{EF5A6CEF-FC00-0200-4DDA-3010E3F4154A}'";
	        	eds.Execute(sql);
	        	while(eds.Read())
	        	{
	        		if(!allIds.contains(eds.GetValue(0)))
	        			allIds.add(eds.GetValue(0));
	        	}
	        	PrintSize(allIds);
	        	
	        	Set allRes=new HashSet();
	        	sql = "select uiskemaid from revisionsspor where uipluginid='{EF5A6CEF-FC00-0200-4DDA-3010E3F4154A}'";
	        	eds.Execute(sql);
	        	while(eds.Read())
	        	{
	        		if(!allIds.contains(eds.GetValue(0)))
	        			allRes.add(eds.GetValue(0));
	        	}
	        	
	        	File outputFile = new File("C:\\Development\\KTI\\data\\dødsattester der kun findes i revisionsspor.txt");
	        	PrintWriter out = new PrintWriter(new BufferedWriter(new FileWriter(outputFile)));
	        	
	        	Pattern pattern = Pattern.compile("<dk.hob.ei.mortality.Plugin_GetPatientDoctorInfo");
	        	
	        	for(Iterator it=allRes.iterator();it.hasNext();)
	        	{
	        		String skemaId = (String)it.next();
	        		System.out.println(skemaId);
	        		sql = "select uiskemaid, dtOprettet, UIBRUGERID, uigruppeid, cast(TXXMLDSIG as varchar(32672)) from revisionsspor where uipluginid='{EF5A6CEF-FC00-0200-4DDA-3010E3F4154A}'" + " AND uiskemaid = '" + skemaId + "'";
	        		eds.Execute(sql);
	        		if (eds.Read()) {
	        			Matcher matcher = pattern.matcher(eds.GetValue(4));
	        			if (!matcher.find()) {
		        			out.write(eds.GetValue(0) + ";" + eds.GetValue(1) + ";" + eds.GetValue(2) + ";" + eds.GetValue(3) + "\n");
		        			out.write(eds.GetValue(4) + "\n");
		        			out.flush();
	        			}
	        		}
	        	}
	        	
	        	out.close();
	        }
	        catch(Exception e)
	        {
	        	e.printStackTrace();
	        }
	        finally
	        {
	        	eds.Close();
	        }
		}
		catch (Exception e) {
			e.printStackTrace();
		}
		finally {
			
		}
	}
	
	private static void PrintSize(Set s) {
		System.out.println("Size of set " + s.size());
	}
}
