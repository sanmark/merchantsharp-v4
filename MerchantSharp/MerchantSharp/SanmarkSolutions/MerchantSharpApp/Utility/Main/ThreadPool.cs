using CustomControls.SanmarkSolutions.WPFCustomControls.MSClosableTab;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Initialize;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main {
	class ThreadPool {

		private static UserControl uc = null;
		private static String headerName = null;
		private static String theMessage = null;

		[MethodImpl(MethodImplOptions.Synchronized)]
		private static void openTab() {
			try {
				Session.MainWindow.Dispatcher.Invoke((Action)(() => {
					ClosableTab tab = new ClosableTab();
					tab.Title = headerName;
					tab.Content = uc;
					Session.MainWindow.tabControl_tabControl.Items.Add(tab);
					tab.Focus();
				}));
			} catch(Exception) {
			}
		}

		/// <summary>
		/// Will opens a new closable tab in MainWindow
		/// </summary>
		/// <param name="userControl">Content of Tab</param>
		/// <param name="header">Title of tab</param>
		public static void openTab(UserControl userControl, String header) {
			try {
				uc = userControl;
				headerName = header;
				/*Thread t = new Thread(delegate() {
					openTab();
				});
				t.Start();*/
				Thread t = new Thread(openTab);
				t.SetApartmentState(ApartmentState.STA);
				t.Start();
			} catch(Exception) {
			}
		}


		public static void closingMainWindow() {
			try {
				System.Environment.Exit(1);
				InitializeSystem.timer.Stop();
			} catch(Exception) {
			}
		}

		public static void showSuccessMessage(String msg) {
			try {
				theMessage = msg;
				SuccessMessage successMessage = new SuccessMessage(theMessage);
				successMessage.Show();
				//Thread t = new Thread(callback_showMsg);
				//t.SetApartmentState(ApartmentState.STA);
				//t.Start();
			} catch(Exception) {
			}
		}
				
	}
}
