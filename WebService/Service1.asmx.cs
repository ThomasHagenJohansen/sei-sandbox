using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using Microsoft.Web.Services;
using Microsoft.Web.Services.Security;

namespace webservice
{
	/// <summary>
	/// Summary description for Service1.
	/// </summary>
	public enum ReadyCode
	{
		Ready=0x00,
		Error_Unknown=0x01,
		Error_CertificateExpired=0x02
	}

	public class ReadyStatus
	{
		public ReadyCode	Status;
		public string		Description;
	}

	public class Service1 : System.Web.Services.WebService
	{
		public Service1()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		[WebMethod]
		public string DescribeReadyCode( ReadyCode code )
		{
			// Dump HTTP reqquest
			System.Web.HttpContext cntx = this.Context;
			cntx.Request.SaveAs(Server.MapPath("httpdump.txt"),true);

			// Verifies that a SOAP request was received.  
			SoapContext requestContext = HttpSoapContext.RequestContext;

			if (requestContext == null)
			{
				throw new 
					ApplicationException("Either a non-SOAP request was " +
					"received or the WSE is not properly installed for " +
					"the Web application hosting the XML Web service.");
			}
			X509SecurityToken theToken = GetFirstX509Token( requestContext.Security );

			switch( code )
			{
				case ReadyCode.Ready:
					return "Systemet er klar til at modtage indberetningsklienten.";

				case ReadyCode.Error_CertificateExpired:
					return "Certifikatet er udløbet.";

				default:
				case ReadyCode.Error_Unknown:
					return "Ukendt fejl.";
			}
		}

		[WebMethod]
		public ReadyStatus Ready()
		{
			// Dump HTTP reqquest
			System.Web.HttpContext cntx = this.Context;
			cntx.Request.SaveAs(Server.MapPath("httpdump.txt"),true);

			// Verifies that a SOAP request was received.  
			SoapContext requestContext = HttpSoapContext.RequestContext;

			if (requestContext == null)
			{
				throw new 
					ApplicationException("Either a non-SOAP request was " +
					"received or the WSE is not properly installed for " +
					"the Web application hosting the XML Web service.");
			}
			X509SecurityToken theToken = GetFirstX509Token( requestContext.Security );

			ReadyStatus r=new ReadyStatus();

			r.Status=ReadyCode.Ready;
			r.Description=DescribeReadyCode( r.Status );

			return r;
		}

		private X509SecurityToken GetFirstX509Token( Security sec ) 
		{
			X509SecurityToken retval = null;
			if ( sec.Tokens.Count > 0 ) 
			{
				foreach ( SecurityToken tok in sec.Tokens ) 
				{
					retval = tok as X509SecurityToken;
					if ( retval != null ) 
					{
						return retval;
					}
				}
			}
			return retval;
		}

	}
}
