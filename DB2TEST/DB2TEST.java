/*
 * Created on 10-09-2003
 *
 * To change the template for this generated file go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */

/**
 * @author thj
 *
 * To change the template for this generated type comment go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
import java.sql.*;
public class DB2TEST
{
	public static void main(String[] args)
	{
		try
		{
			// Register the driver with DriverManager
			Class.forName("COM.ibm.db2.jdbc.net.DB2Driver").newInstance();
		}
		catch (Exception e)
		{
		  e.printStackTrace();
		}

		Connection con=null;
		String url="jdbc:db2://hagen:6789/eisst";
		String uid="EISST";
		String pwd="EISSTEISST";
		try
		{
			con=DriverManager.getConnection(url, uid, pwd);
		}
	  	catch(Exception e)
	  	{
			e.printStackTrace();
	  	}

		try
		{
			// retrieve data from the database
			System.out.println("Retrieve some data from the database...");
			Statement stmt = con.createStatement();
			ResultSet rs = stmt.executeQuery("SELECT * FROM INSTALLATIONER");
			
			System.out.println("Received results:");
	 
			// display the result set
			// rs.next() returns false when there are no more rows
			while( rs.next() )
			{
			   String f1 = rs.getString(1);
			   String f2 = rs.getString(2);
			   if (f1.length() == 38)
			   { 
					if (f1.charAt(0) == '{' &&
						f1.charAt(f1.length()-1) == '}' &&
						f1.charAt(9) == '-' &&
						f1.charAt(14) == '-' &&
						f1.charAt(19) == '-')
					{
						System.out.print("It's a GUID");
					}
				}					
			   System.out.print("GUID = " + f1);
			   System.out.print("");
			}
	 
			rs.close();
			stmt.close();
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}
}
