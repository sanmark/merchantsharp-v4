using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common {
	class ShowMessage {

		public static void error(String theMessage) {
			MessageBox.Show(theMessage, Properties.Resources.ApplicationFullName, MessageBoxButton.OK, MessageBoxImage.Error);
		}

		public static void success(String theMessage) {
			ThreadPool.showSuccessMessage(theMessage);
			//MessageBox.Show(theMessage, Properties.Resources.ApplicationFullName, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		public static void information(String theMessage) {
			MessageBox.Show(theMessage, Properties.Resources.ApplicationFullName, MessageBoxButton.OK, MessageBoxImage.Information);
		}

		internal static MessageBoxResult confirm(String theMessage) {
			return MessageBox.Show(theMessage, Properties.Resources.ApplicationFullName, MessageBoxButton.YesNo, MessageBoxImage.Question);
		}

	}
}
