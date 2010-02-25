import dk.hob.time.DateTime;

//------------------------------------------------------------------------------------------------
public class Arguments 
{
	static public boolean	Perform=false;
	static public DateTime	dtFrom=null;
	static public DateTime	dtTo=null;
	static public String	SkemaId=null;
	
	//--------------------------------------------------------------------------------------
	static public void Parse(String[] args) throws Exception
	{
		for(int a=0; a<args.length; a++)
		{
			String arg = args[a];
			if(arg.startsWith("-"))
			{
				char c = arg.charAt(1);
				arg = arg.substring(2);
				switch(c)
				{
				case 'p':						// Actually perform updates or not
					Perform=true;
					break;
					
				case 'f':						// From date
					dtFrom = DateTime.Parse(arg);
					break;
				
				case 't':						// To date
					dtTo = DateTime.Parse(arg);
					break;
				
				case 's':						// Skemaid
					SkemaId = arg;
					break;
				
				default:
					throw new Exception("Unexpected commandline argument");
				}
			}
			else throw new Exception("Unexpected commandline argument");
		}
	}
	
}
