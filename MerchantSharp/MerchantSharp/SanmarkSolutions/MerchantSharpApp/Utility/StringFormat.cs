using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility {
	class StringFormat {

		public static String getSHA1(String theString) {
			String encodedString = null;
			Byte[] originalBytes;
			Byte[] encodedBytes;
			SHA1 sha1;

			try {
				sha1 = new SHA1CryptoServiceProvider();
				originalBytes = ASCIIEncoding.Default.GetBytes(theString);
				encodedBytes = sha1.ComputeHash(originalBytes);
				encodedString = BitConverter.ToString(encodedBytes);
			} catch(Exception) {
			}
			return encodedString;
		}

		public static bool hasUnicode( string theString ) {
			bool hasUnicode = false;
			for ( int i = 0; i < theString.Length; ++i ) {
				char c = theString[i];
				if ( ( ( int )c ) > 127 ) {
					hasUnicode = true;
				}
			}
			return hasUnicode;
		}

	}
}
