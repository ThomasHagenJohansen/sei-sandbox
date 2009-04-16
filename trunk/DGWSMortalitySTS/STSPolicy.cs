using Microsoft.Web.Services3.Design;

namespace DGWSMortalitySTS
{
	public class STSPolicy : Policy
	{
		public STSPolicy()
		{
			Assertions.Add(new RequireActionHeaderAssertion());		// WSE policy
		}
	}
}
