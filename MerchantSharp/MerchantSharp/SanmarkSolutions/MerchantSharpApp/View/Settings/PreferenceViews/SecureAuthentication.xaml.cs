using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Settings.PreferenceViews {
	/// <summary>
	/// Interaction logic for SecureAuthentication.xaml
	/// </summary>
	public partial class SecureAuthentication : UserControl, IPreferences {

		private PreferenceManagerImpl preferenceManagerImpl = null;
		private Preference preferenceIsActive = null;
		private Preference preferenceTime = null;

		public SecureAuthentication() {
			InitializeComponent();
		}

		public void setImpl( object obj ) {
			try {
				preferenceManagerImpl = ( PreferenceManagerImpl )obj;
			} catch ( Exception ) {
			}
		}

		private void UserControl_Loaded( object sender, RoutedEventArgs e ) {
			try {
				preferenceIsActive = preferenceManagerImpl.getPreference("isActiveSecureAuthentication");
				preferenceTime = preferenceManagerImpl.getPreference("secureAuthenticationTime");

				checkBox_isActive.IsChecked = preferenceIsActive.Value == "1" ? true : false;
				textBox_time.Text = preferenceTime.Value;
			} catch ( Exception ) {
			}
		}

		private void button_save_Click( object sender, RoutedEventArgs e ) {
			try {
				if ( ( preferenceIsActive.Value == "1" && checkBox_isActive.IsChecked == false ) || ( preferenceIsActive.Value == "0" && checkBox_isActive.IsChecked == true ) ) {
					preferenceIsActive.Value = checkBox_isActive.IsChecked == true ? "1" : "0";
					CommonMethods.setCDMDForUpdate(preferenceIsActive);
					preferenceManagerImpl.upd(preferenceIsActive);
				}
				if ( preferenceTime.Value != textBox_time.Text ) {
					preferenceTime.Value = textBox_time.IntValue.ToString();
					CommonMethods.setCDMDForUpdate(preferenceTime);
					preferenceManagerImpl.upd(preferenceTime);
				}
				ShowMessage.success(Common.Messages.Success.Success004);
			} catch ( Exception ) {
			}
		}
	}
}
