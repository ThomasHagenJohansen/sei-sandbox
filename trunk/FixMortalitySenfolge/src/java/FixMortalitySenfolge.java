import java.sql.Connection;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import dk.hob.data.ExtDataSet;
import dk.hob.Globals;
import dk.hob.resources.AppConfig;
import dk.hob.sqldatabase.DBConnection;
import dk.hob.sqldatabase.SQL;

/*
 * Created on 2005-06-16
 *
 */

/**
 * @author otj
 */
//--------------------------------------------------------------------------------------
public class FixMortalitySenfolge 
{
	/*
	 * Ret op på DødsAttest tabeller.
	 * - Udtræk skemaID'er fra Mortality_side1
	 * - Udtræk nyeste skema fra revisionsspor tabellen med det givne id
	 * - Udtræk bSenFolge kolonneværdien hvis den eksisterer (benyt regex)
	 * - Opdater feltet i Mortality_Side1 tabellen med bSenFolge værdien
	 * Hvis bSenFolge ikke kan findes i SOAP, spring skemaet over.
	 * 
	 * */
	
	//--------------------------------------------------------------------------------------
    public static void main(String[] args) throws Exception
    {
    	//args=new String[]{"-p0"};
    	
	    // Display program Title
		System.out.println("Fix Mortality Senfølge");

		// Load Application Configuration
		if(System.getProperty("configfile") != null)
			Globals.Cfg = new AppConfig(System.getProperty("configfile"));
		else
			Globals.Cfg = new AppConfig("EIBackend.config");

		Arguments.Parse(args);

		if(Arguments.Perform)
			System.out.println("Updates will be performed");
		
        int iCnt1 = 0;
        int iCnt2 = 0;
        int iCnt3 = 0;
        
		try
		{
			String sql="";
	        Connection conn = null;
	        Connection conn2 = null;
	        DBConnection dbc = null;
	        ExtDataSet eds = null;
	        ExtDataSet eds2=null;

			try
		    {
		        dbc = new DBConnection();
		        conn = dbc.ConnectTo("EISST");
		        conn2 = dbc.ConnectTo("EISST");
				conn2.setAutoCommit(false);

		        eds = new ExtDataSet(conn);
    			String uiMortPluginId = (String)eds.ExecuteScalar("SELECT uiPluginID FROM Plugins where txNavn='dk.hob.ei.mortality.Plugin'");
    			uiMortPluginId = SQL.FmtGuid(uiMortPluginId);
    			
    			int iTotalCnt = eds.ExecuteScalarInt("SELECT count(*) FROM fixmortality_to_handle");

		        sql =  "SELECT uiSkemaID FROM fixmortality_to_handle";
		        eds.Execute(sql);
		        
		        Set skemaIds = new HashSet();
		        try {
		        	while (eds.Read())
        			{
        				String uiSkemaID = new StringBuffer(40).append('\'').append(eds.GetValue(0).toUpperCase()).append('\'').toString();
	                	skemaIds.add(uiSkemaID);
        			}
		        }
		        catch (Exception e) {
		        	e.printStackTrace();
		        	throw e;
		        }
		        finally {
		        	System.out.println("Total number of rows to be handled: " + skemaIds.size());
		        	eds.Close();
		        }

		        String strElementName = "bSenfolge";
		    	Pattern pattern = Pattern.compile("&lt;\\w*?:?" + strElementName + ".*?&gt;(.*?)&lt;/\\w*?:?" + strElementName + "&gt;");
		        
	            try
	            {
	            	eds2 = new ExtDataSet(conn2);
	            	String s1="SELECT cast(txXmlDSig as varchar(32000)) FROM RevisionsSpor WHERE uiSkemaID=";
	            	String s2=" ORDER BY dtOprettet DESC FETCH FIRST 1 ROWS ONLY FOR READ ONLY";
	            	
        			String s3 = "UPDATE Mortality_Side2 SET bSenfolge=";
        			String s4 = " WHERE uiSkemaID=";
        			String s5 = " AND bSenfolge <>";
        			
        			Iterator iter = skemaIds.iterator();
	                while(iter.hasNext())
	                {
	                	String uiSkemaID = (String)iter.next();
	                		
	                	sql = new StringBuffer(s1).append(uiSkemaID).append(s2).toString();
	                	String soap = (String)eds2.ExecuteScalar(sql);
	                	if(soap!=null)
	                	{
	                		iCnt1++;
	        		    	Matcher matcher = pattern.matcher(soap);
	        		    	if ( ! matcher.find())
	                		{
	                			iCnt2++;
	                		}
	                		else
	                		{
	                			iCnt3++;
	        		    		String bSenFolge = matcher.group(1);
	                			if(Arguments.Perform)
	                			{
		                			//String strSenFolge = SQL.FmtBoolean(bSenFolge);
		                			String strSenFolge = bSenFolge.equals("true")? "1":"0";
		    	                	sql = new StringBuffer(s3).append(strSenFolge).append(s4).append(uiSkemaID).append(s5).append(strSenFolge).toString();
		                			try
		                			{
		                				eds2.Execute(sql);
		                				eds2.Execute("DELETE FROM fixmortality_to_handle WHERE uiskemaid=" + uiSkemaID);
		                				conn2.commit();
		                			}
		                			catch(Exception e)
		                			{
		    	                    	conn2.rollback();
		    	                    	throw e;
		                			}
	                			}
	                		}
	        		    	if((iCnt1%1000)==0)
	        		    		System.out.println(iCnt1+"/"+iTotalCnt+" ("+((100*iCnt1)/iTotalCnt)+"%)  fundet:"+iCnt3);
	                	}
	                }
	            }
	            catch(Exception e)
		        {
	            	throw e;
		        }
		    }
			catch(Exception e)
			{
			    e.printStackTrace();
			    System.out.println("SQL: "+sql);
			}
			finally
			{
			    if(eds2!=null) eds2.Close();
			    if(dbc!=null && conn2!=null)
			    	dbc.CloseConnections();
			}
		}
		catch (Exception e)
		{
			e.printStackTrace();
		}
		finally
		{
			System.out.println("\nTotal: "+iCnt1);
			System.out.println("\nIkke fundet: "+iCnt2);
			System.out.println("\nFundet: "+iCnt3);
		}
    }
}
