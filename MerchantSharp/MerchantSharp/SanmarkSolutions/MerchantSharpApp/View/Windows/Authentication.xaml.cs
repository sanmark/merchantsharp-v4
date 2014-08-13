using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Windows {
	/// <summary>
	/// Interaction logic for Authentication.xaml
	/// </summary>
	public partial class Authentication : Window {

		private int count = 0;

		public Authentication() {
			InitializeComponent();
		}

		private void checkAuthentication() {
			try {
				if ( StringFormat.getSHA1(password.Password) == Session.User.Password ) {
					Session.MainWindow.startAuthentication();
					this.Close();
				} else {
					ShowMessage.error(Common.Messages.Error.Error017);
					password.Clear();
					if ( count >= 2 ) {
						logoutAndRestart();
					}
					count++;
				}
			} catch ( Exception ) {
			}
		}

		private void logoutAndRestart() {
			try {
				System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
				Application.Current.Shutdown();
			} catch ( Exception ) {
			}
		}

		private void button_logout_Click( object sender, RoutedEventArgs e ) {
			logoutAndRestart();
		}

		private void button_resume_Click( object sender, RoutedEventArgs e ) {
			checkAuthentication();
		}

		private void Window_Loaded( object sender, RoutedEventArgs e ) {
			try {
				label_userName.Content = "Hello " + Session.User.UserName + ",";
				button_logout.Content = "I'm not " + Session.User.UserName;
			} catch ( Exception ) {
			}
			password.Focus();
		}
	}
}
