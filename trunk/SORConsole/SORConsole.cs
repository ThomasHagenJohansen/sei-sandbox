using System;
using System.Data;
using System.IO;
using System.Xml;
using System.Collections.Specialized;
using SORConsole.dk.imkit.sorwebservice;

namespace SORConsole
{
	/// <summary>
	/// Summary description for SORConsole.
	/// </summary>
	class SORConsole
	{
		private static string strRes ;
		private static SORWS ws;
		private static NameValueCollection nvcSletCode;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			/* Read item to be deleted */
            //XmlDocument xmlDoc = new XmlDocument();
            //String sletFile="skalslettes.xml";
            //nvcSletCode = new NameValueCollection();

            //xmlDoc.Load(sletFile);
            //XmlNodeList nodelist = xmlDoc.DocumentElement.ChildNodes;
            //foreach(XmlNode node in nodelist)
            //{
            //    if(node.NodeType == XmlNodeType.Element)
            //    {
            //        // Attempt to get values						
            //        XmlNode Code = node.SelectSingleNode("Kode");
            //        XmlNode Text = node.SelectSingleNode("Tekst");
            //        String strCode = Code.InnerText.Trim();
            //        String strText = Text.InnerText.Trim();
            //        nvcSletCode.Add(strCode,strText);
            //    }
            //}

			/* Perform search in WS */
			sorSearch searchParameters = new sorSearch();
			if (args.Length==0)
				searchParameters.longOrganizationType = 547211000005108 ; //Kommunal
			else
				searchParameters.longOrganizationType = long.Parse(args[0]);

			ws = new SORWS();
			try
			{
				sorSearchResult[] wsResult = ws.Search(searchParameters);
				strRes = "<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>\r\n<root>\r\n";
				            
				for (int i = 0; i < wsResult.Length; i++)
				{

					//Get health institutions
					DataSet dsHI = ws.GetChildrenAtNextLevel(long.Parse(wsResult[i].entity_code.ToString()));
					DataTable dtHI = dsHI.Tables[0];
					DataSet dsOU = new DataSet();
					for (int j = 0; j < dtHI.Rows.Count; j++)
					{
						//Get organizational units
						dsOU = ws.GetChildrenAtNextLevel(long.Parse(dtHI.Rows[j]["entity_code"].ToString()));
						PrintUnits(dsOU);
					}

				}
				strRes += "</root>";

				/*
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(strRes);
				XmlWriter xmlwriter = new XmlWriter();
				doc.WriteTo(xmlwriter);
				/*
				/* Output XML-file */
				TextWriter tw = new StreamWriter(new FileStream("output.xml", FileMode.Create),System.Text.Encoding.GetEncoding("iso-8859-1"));
				tw.Write(strRes);
				tw.Close();
                //Console.WriteLine(strRes);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Fejl: " + ex);
			}

            Console.WriteLine("slut");
            Console.ReadLine();

		}

		protected static void AddRow(string entity_code, string entity_name, DateTime dtFrom, DateTime dtTo)
		{
                long longCode = long.Parse(entity_code);
                String strName = entity_name;
                DateTime maxDate = new DateTime(2500, 1, 1);
                String strFrom = dtFrom.ToString("yyyy-MM-dd");
                String strTo = (dtTo == DateTime.MinValue || dtTo == DateTime.MaxValue ? maxDate.ToString("yyyy-MM-dd") : dtTo.Date.ToString("yyyy-MM-dd"));

                String strTmpRes = "";
                strTmpRes += "\t<record>\r\n";
                strTmpRes += "\t\t<Kode>" + longCode + "</Kode>\r\n";
                strTmpRes += "\t\t<Tekst>" + strName + "</Tekst>\r\n";
                strTmpRes += "\t\t<FraDato>" + strFrom + "</FraDato>\r\n";
                strTmpRes += "\t\t<TilDato>" + strTo + "</TilDato>\r\n";
                //strRes += "<Sex />";
                strTmpRes += "\t</record>\r\n";
                //if (nvcSletCode.Get(""+longCode)!=strName)
                    strRes += strTmpRes;


		}

		private static void PrintUnits(DataSet dsOU)
		{
            
			if (dsOU != null && dsOU.Tables.Count > 0 && dsOU.Tables[0] != null)
			{
				DataTable dtOU = dsOU.Tables[0];
				for (int k = 0; k < dtOU.Rows.Count; k++)
				{
					sorOrganizationalUnit ou = ws.GetOU(long.Parse(dtOU.Rows[k]["entity_code"].ToString()));

                    try
                    {
                        Console.WriteLine("PrintUnits: "+dtOU.Rows[k]["entity_code"].ToString() ?? "");
                        AddRow(
                            dtOU.Rows[k]["entity_code"].ToString() ?? "",
                            dtOU.Rows[k]["entity_name"].ToString() ?? "",
                            (ou!=null) ? ou.dtFromDate : DateTime.MinValue, //Fejler ved enkelte tilfælde ou er null
                            (ou!=null) ? ou.dtToDate : DateTime.MaxValue
                        );
                        DataSet dsChildOU = ws.GetChildrenAtNextLevel(long.Parse(dtOU.Rows[k]["entity_code"].ToString()));
                        PrintUnits(dsChildOU);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("Fejl: " + exp.ToString());
                        Console.WriteLine("1:"+dtOU.Rows[k]["entity_code"].ToString() ?? "");
                        Console.WriteLine("2:" + dtOU.Rows[k]["entity_name"].ToString() ?? "");
                        Console.WriteLine("3:" + ou.dtFromDate.ToString());
                        Console.WriteLine("4:" + ou.dtToDate.ToString());

                    }
				}
			}
			return;
		}
	}
}
