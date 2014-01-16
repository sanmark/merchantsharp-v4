using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Settings.PreferenceViews {
	/// <summary>
	/// Interaction logic for AutoBackupLocation.xaml
	/// </summary>
	public partial class AutoBackupLocation : UserControl, IPreferences {

		private PreferenceManagerImpl preferenceManagerImpl = null;
		
		private Preference preference = null;

		public AutoBackupLocation() {
			InitializeComponent();
		}

		public void setImpl(object obj) {
			try {
				preferenceManagerImpl = (PreferenceManagerImpl)obj;
			} catch(Exception) {
			}
		}
		
		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			try {
				preference = preferenceManagerImpl.getPreference("autoBackupLocation");
				textBox_location.Text = preference.Value.Replace("\\\\", "\\");
			} catch(Exception) {
			}
		}

		private void button_dialog_Click(object sender, RoutedEventArgs e) {
			try {
				System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
				folderBrowserDialog.SelectedPath = preference.Value.Replace("\\\\", "\\");
				if(folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					textBox_location.Text = folderBrowserDialog.SelectedPath;
				}
			} catch(Exception) {
			}
		}

		private void button_save_Click(object sender, RoutedEventArgs e) {
			try {
				preference.Value = textBox_location.Text;
				CommonMethods.setCDMDForUpdate(preference);
				preferenceManagerImpl.upd(preference);
				ShowMessage.success(Common.Messages.Success.Success004);
			} catch(Exception) {
			}
		}

	}
}
