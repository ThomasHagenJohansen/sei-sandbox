using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;

namespace DGWSSignedMortalityClient
{
	partial class Program
	{
		private static Hashtable SerializerCache;
		XmlSerializer GetSerializer(Type t)
		{
			if (SerializerCache == null) SerializerCache = new Hashtable();
			if (SerializerCache.ContainsKey(t) == false)
				SerializerCache.Add(t, new XmlSerializer(t));
			return ((XmlSerializer)SerializerCache[t]);
		}

		String Serialize(Object o, Type t)
		{
			StringWriter sw = new StringWriter();
			GetSerializer(t).Serialize(sw, o);
			return (sw.ToString());
		}

		Object Deserialize(String source, Type t)
		{
			return (GetSerializer(t).Deserialize(new StringReader(source)));
		}
	}
}