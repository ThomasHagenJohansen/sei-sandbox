using System;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Globalization;

namespace ResourceTest
{
	/// <summary>
	/// Holds functions to retrieve all your strings
	/// </summary>
	public class MyResource
	{
		/// <summary>
		/// Class that handles instances of the resource manager
		/// </summary>
		private class MyResourceResourceManager
		{
			/********************************************************************/
			/// <summary>
			/// Returns an instance of the ResourceManager object
			/// </summary>
			/********************************************************************/
			public static ResourceManager Instance
			{
				get
				{
					// Make sure we can make an instance in multi-threaded
					// applications
					lock (typeof(MyResourceResourceManager))
					{
						if (_Instance == null)
						{
							Assembly callAss    = Assembly.GetCallingAssembly();
							String assemblyName = callAss.GetName().Name;
							String baseName     = String.Format("{0}.MyResource", assemblyName);
							_Instance           = new ResourceManager(baseName, callAss);
						}

						return _Instance;
					}
				}
			}
			private static ResourceManager _Instance = null;
		}



		/********************************************************************/
		/// <summary>
		/// Protect the constructor, to make sure no instances are made
		/// </summary>
		/********************************************************************/
		private MyResource()
		{
		}



		/********************************************************************/
		/// <summary>
		/// Will return a formatted string
		/// </summary>
		/********************************************************************/
		private static String FormatString(String resourceID, params Object[] objects)
		{
			String format = MyResourceResourceManager.Instance.GetString(resourceID);
			return String.Format(format, objects);
		}



		/********************************************************************/
		/// <summary>
		/// Switch the user interface locale to another one
		/// </summary>
		/// <param name="cultureName">The name of the language you want to use</param>
		/********************************************************************/
		public static void SwitchLocale(String cultureName)
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
		}



		/********************************************************************/
		/// <summary>
		/// MIN_TEKST
		/// </summary>
		/********************************************************************/
		public static String MIN_TEKST
		{
			get { return MyResourceResourceManager.Instance.GetString("MIN_TEKST");	}
		}



		/********************************************************************/
		/// <summary>
		/// TEKST2
		/// </summary>
		/********************************************************************/
		public static String TEKST2(Object arg0)
		{
			return MyResource.FormatString("TEKST2", arg0);
		}
	}
}
