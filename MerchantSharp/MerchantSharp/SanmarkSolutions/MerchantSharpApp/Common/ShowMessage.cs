using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common {
	class ShowMessage {

		static DispatcherTimer dispatcherTimer_fl = new DispatcherTimer();
		static String string_vfd_fl;
		static int count_fl;

		static DispatcherTimer dispatcherTimer_sl = new DispatcherTimer();
		static String string_vfd_sl;
		static int count_sl;

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


		public static void vfdFirstLine( String theString ) {
			try {
				if ( Session.Preference["isShowVFD"] == "1" ) {
					if ( !VFDMessage.threadTextChange.IsAlive ) {
						VFDMessage.threadTextChange.Start();
					}
					if ( theString.Length == 0 ) {
						VFDMessage.string_vfd_fl = null;
					} else if ( theString.Length > 0 && theString.Length < 21 ) {
						VFDMessage.string_vfd_fl = theString;
					} else {
						ShowMessage.count_fl = -1;
						string_vfd_fl = theString;
						//VFDMessage.startFlowLine1(string_vfd_fl.Substring(0, 20));
						VFDMessage.string_vfd_fl = string_vfd_fl;
					}
				}
			} catch ( Exception ) {
			}
		}

		public static void vfdSecondLine( String theString ) {
			try {
				if ( Session.Preference["isShowVFD"] == "1" ) {
					if ( !VFDMessage.threadTextChange.IsAlive ) {
						VFDMessage.threadTextChange.Start();
					}
					if ( theString.Length == 0 ) {
						VFDMessage.string_vfd_sl = null;
					} else if ( theString.Length > 0 && theString.Length < 21 ) {
						VFDMessage.string_vfd_sl = theString;
					} else {
						count_fl = -1;
						string_vfd_sl = theString;
						//VFDMessage.startFlowLine1(string_vfd_fl.Substring(0, 20));
						VFDMessage.string_vfd_sl = string_vfd_sl;
					}
				}
			} catch ( Exception ) {
			}
		}
	}
}
