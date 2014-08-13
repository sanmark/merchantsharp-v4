using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common.Messages;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.MainWindows {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		private DispatcherTimer authenticationTimer = null;

		private HomeWindow homeWindow = null;
		public HomeWindow HomeWindow {
			get { return homeWindow; }
		}

		public MainWindow() {
			InitializeComponent();
			startAuthentication();
		}

		public void startAuthentication() {
			try {
				if ( Session.Preference["isActiveSecureAuthentication"] == "1" ) {
					authenticationTimer = new DispatcherTimer();
					authenticationTimer.Interval = new TimeSpan(0, Convert.ToInt32(Session.Preference["secureAuthenticationTime"]), 0); //set the interval 1 second			
					//authenticationTimer.Interval = new TimeSpan(0, 0, 0, 2); //set the interval 1 second			
					authenticationTimer.Tick += new EventHandler(callBack_authentication); //set the timer tick
					authenticationTimer.Start(); //start the timer
				}
			} catch ( Exception ) {
			}
		}

		private void callBack_authentication( object sender, EventArgs e ) {
			try {
				authenticationTimer.Stop();
				Authentication authentication = new Authentication();
				authentication.ShowDialog();
			} catch ( Exception ) {
			}
		}

		private void resetAuthentication() {
			try {
				if ( authenticationTimer != null ) {
					authenticationTimer.Stop();
					authenticationTimer.Start();
				}
			} catch ( Exception ) {
			}
		}

		private void loadHomePage() {
			try {
				homeWindow = new HomeWindow();
				mainTab.Content = homeWindow;
			} catch ( Exception ) {
			}
		}

		private void Window_Loaded( object sender, RoutedEventArgs e ) {
			ShowMessage.vfdFirstLine("Welcome to " + Session.Preference["shopName"] + "!");
			ShowMessage.vfdSecondLine(VFD.VFD001);
			loadHomePage();
		}

		private void Window_Closing( object sender, System.ComponentModel.CancelEventArgs e ) {
			ThreadPool.closingMainWindow();
		}

		private void Window_MouseMove( object sender, MouseEventArgs e ) {
			try {
				resetAuthentication();
			} catch ( Exception ) {
			}
		}

		private void Window_KeyDown( object sender, KeyEventArgs e ) {
			try {
				resetAuthentication();
			} catch ( Exception ) {
			}
		}
	}
}
