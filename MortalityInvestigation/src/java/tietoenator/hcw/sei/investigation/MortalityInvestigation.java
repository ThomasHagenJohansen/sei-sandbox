package tietoenator.hcw.sei.investigation;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.PrintWriter;
import java.sql.Connection;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import dk.hob.data.ExtDataSet;
import dk.hob.resources.AppConfig;
import dk.hob.sqldatabase.Database;
import dk.sst.ei.Globals;
import dk.sst.ei.SessionContext;

public class MortalityInvestigation {

	private static final String OUTPUT_FILE = "C:\\Development\\KTI\\data\\dødsattester der kun findes i revisionsspor.txt";
	private static final String EIBACKEND_CONFIG = "C:\\Development\\KTI\\DriftEIBackend.config";

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		try
		{
			Globals.Cfg = new AppConfig(EIBACKEND_CONFIG);

			// Set up a session context
			SessionContext ctx=new SessionContext();
			Database db = new Database(Database.DriverType.IBM_DB2_NET);
	        Connection Conn = db.Connect("localhost","6789","EISST","EISST","EISSTEISST");

	        ExtDataSet eds = new ExtDataSet(Conn);
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
	        	
	        	File outputFile = new File(OUTPUT_FILE);
	        	PrintWriter out = new PrintWriter(new BufferedWriter(new FileWriter(outputFile)));
	        	
	        	//Pattern pattern = Pattern.compile("<dk.hob.ei.mortality.Plugin_GetPatientDoctorInfo");
	        	//Pattern pattern2 = Pattern.compile("");
	        	
	        	for(Iterator it=allRes.iterator();it.hasNext();)
	        	{
	        		String skemaId = (String)it.next();
	        		sql = "select uiskemaid, dtOprettet, UIBRUGERID, uigruppeid, cast(TXXMLDSIG as varchar(32672)) from revisionsspor where uipluginid='{EF5A6CEF-FC00-0200-4DDA-3010E3F4154A}'" + " AND uiskemaid = '" + skemaId + "' AND ITYPE = 0";
	        		eds.Execute(sql);
	        		if (eds.Read()) {
	        			//Matcher matcher = pattern.matcher(eds.GetValue(4));
	        			//if (!matcher.find()) {
	        				System.out.println(skemaId);
		        			out.write(eds.GetValue(0) + ";" + eds.GetValue(1) + ";" + eds.GetValue(2) + ";" + eds.GetValue(3) + "\n");
		        			out.write(eds.GetValue(4) + "\n");
		        			out.flush();
	        			//}
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
