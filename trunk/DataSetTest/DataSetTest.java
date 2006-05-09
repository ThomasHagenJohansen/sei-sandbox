/*
 * Created on 11-09-2003
 *
 */

/**
 * @author Ole Thomas Jensen, HOB Business Software A/S
 *
 */
import java.io.File;
import java.util.ArrayList;
import org.jdom.Document;
import org.jdom.Element;
import org.jdom.input.SAXBuilder;
import java.util.Iterator;

import dk.hob.dotnet.*;
import dk.hob.dotnet.DataSet;


public class DataSetTest
{

	public static void main(String[] args)
	{
		try
		{

			
			DataSet ds = new DataSet("MyDataSet");
			DataTable dt = ds.AddTable("MyTable");
			dt.AddColumn("Name","xs:string","String");
			dt.AddColumn("Phone","xs:string","String");
			dt.AddColumn("Address","xs:string","String");
			dt.AddColumn("State","xs:string","String");

			DataRow dr = dt.NewRow();
			dr.SetColumnValue("Name","Ole Jensen");
			dr.SetColumnValue("Phone","40637168");
			dr.SetColumnValue("Address","Here");
			dr.SetColumnValue("State","Cph");

			dt.AddRow(dr);

			ds.SerializeToXmlFile("TestFunction","Dump3.xml");
		
		

			
			SAXBuilder builder = new SAXBuilder();
			Document doc = builder.build(new File("Input.xml"));

			
			ArrayList alDataSets = DataSet.GetDataSets(doc, "Tvang_CommitDatasetEllerNoget");
			((DataSet)alDataSets.get(0)).SerializeToXmlFile("Tvang_CommitDatasetEllerNoget","output.xml");
			
		
			int iNumDatasets = alDataSets.size(); 
			if(iNumDatasets > 0)
			{
				System.out.println("Num Datasets: " + iNumDatasets);
			
				// Display Dataset contents
				Iterator iDS = alDataSets.iterator();
				while(iDS.hasNext())
				{
					DataSet ds = (DataSet)iDS.next();
//					ds.SerializeToXml("Test","SSTTvang");
					System.out.println("Dataset Name: " + ds.Name);
					Iterator iDT = ds.Tables.iterator();
					while(iDT.hasNext())
					{
						DataTable dt = (DataTable)iDT.next();
						System.out.println("Table Name: " + ds.Name+"."+dt.Name);
						Iterator iDR = dt.Rows.iterator();
						while(iDR.hasNext())
						{
							DataRow dr = (DataRow)iDR.next();
							Iterator iDC = dr.Row.getChildren().iterator();
							while(iDC.hasNext())
							{
								Element elColumn = (Element)iDC.next();
								System.out.println(""+ds.Name+"."+dt.Name+"."+elColumn.getName()+" = " + elColumn.getText());
							}
						}
					}
				}
			}
		}
		catch(Exception e)
		{
			String a="";		
		}
	}
	

}
