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
	/// Interaction logic for DefaultItemSelect.xaml
	/// </summary>
	public partial class DefaultItemSelect : UserControl, IPreferences {

		private PreferenceManagerImpl preferenceManagerImpl = null;

		private Preference defaultSelect = null;
		private Preference codeGenerateBy = null;

		public DefaultItemSelect() {
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
				defaultSelect = preferenceManagerImpl.getPreference("defaultItemSelectMode");
				codeGenerateBy = preferenceManagerImpl.getPreference("itemCodeGenerateBy");

				radioButton_name.IsChecked = defaultSelect.Value == "0" ? true : false;
				radioButton_code.IsChecked = defaultSelect.Value == "1" ? true : false;

				radioButton_first.IsChecked = codeGenerateBy.Value == "f" ? true : false;
				radioButton_last.IsChecked = codeGenerateBy.Value == "l" ? true : false;
			} catch(Exception) {
			}
		}

		private void button_save_Click(object sender, RoutedEventArgs e) {
			try {
				defaultSelect.Value = radioButton_name.IsChecked == true ? "0" : "1";
				codeGenerateBy.Value = radioButton_first.IsChecked == true ? "f" : "l";

				CommonMethods.setCDMDForUpdate(defaultSelect);
				CommonMethods.setCDMDForUpdate(codeGenerateBy);

				preferenceManagerImpl.upd(defaultSelect);
				preferenceManagerImpl.upd(codeGenerateBy);

				ShowMessage.success(Common.Messages.Success.Success004);
			} catch(Exception) {
			}
		}
	}
}
