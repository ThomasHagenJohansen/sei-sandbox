/*
 * Created on 29-09-2004
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package Saxon;

import net.sf.saxon.Transform;

/**
 * @author cso
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class SaxonTest {

	public static void main(String[] args)
	{
		try
		{
			/*
			String dt="2003-01-01T15:17:18.1009663+01:00";
			
			Timestamp hmm=java.sql.Timestamp.valueOf( dt );
			Timestamp ts=Conversion.ConvFromDateTimeToTimestamp( dt );
			String gmt=ts.toGMTString();
			String db2=Conversion.ConvFromDateTimeToDB2( dt );
			db2=Conversion.ConvFromTimestampToDB2( ts );
			ts=Conversion.ConvFromDB2ToTimestamp( db2 );
			*/

			Transform.main( args );
		}
		catch( Exception e )
		{
			System.out.print( e.toString() );
		}
	}
}
