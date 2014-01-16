using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility {
	class NumbersToWords {

		private static string NumberToWords(int number) {
			if(number == 0)
				return "Zero";

			if(number < 0)
				return "Minus " + NumberToWords(Math.Abs(number));

			string words = "";

			if((number / 1000000) > 0) {
				words += NumberToWords(number / 1000000) + " Million ";
				number %= 1000000;
			}

			if((number / 1000) > 0) {
				words += NumberToWords(number / 1000) + " Thousand ";
				number %= 1000;
			}

			if((number / 100) > 0) {
				words += NumberToWords(number / 100) + " Hundred ";
				number %= 100;
			}

			if(number > 0) {
				if(words != "")
					words += "and ";

				var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
				var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

				if(number < 20)
					words += unitsMap[number];
				else {
					words += tensMap[number / 10];
					if((number % 10) > 0)
						words += "-" + unitsMap[number % 10];
				}
			}

			return words;
		}

		public static string decimalNumberToWords(string decimalNumber) {
			string words = null;
			if(decimalNumber.Contains('.')) {
				string[] wordsArray = decimalNumber.Split('.');
				if(!string.IsNullOrWhiteSpace(wordsArray[0])) {
					words = NumberToWords(Convert.ToInt32(wordsArray[0]));
					words += " Rupees";
				}
				if(!string.IsNullOrWhiteSpace(wordsArray[1])) {
					if(!string.IsNullOrWhiteSpace(wordsArray[0])) {
						words += " and ";
					}
					if(wordsArray[1].Length == 1) {
						wordsArray[1] += "0";
					}
					words += NumberToWords(Convert.ToInt32(wordsArray[1]));
					words += " Cents";
				}

			} else {
				words = NumberToWords(Convert.ToInt32(decimalNumber));
				words += " Rupees";
			}
			words += " Only.";
			return words;
		}

	}
}
