using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using TietoEnator.Security.Cryptography;

namespace CreateCryptedPassword
{
	public class Program
	{
		public static void Main(string[] args)
		{
			const String pw = "Test1234";

			SecureString ss = new SecureString();
			foreach (char c in pw)
				ss.AppendChar(c);

			String cryptedPW = Crypto.SimpleCrypt(ss);
			System.Diagnostics.Debug.WriteLine("\"" + cryptedPW + "\"");
		}
	}
}
