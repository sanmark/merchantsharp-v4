using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
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
	/// Interaction logic for DefaultStock.xaml
	/// </summary>
	public partial class DefaultStock : UserControl, IPreferences {

		private PreferenceManagerImpl preferenceManagerImpl = null;

		private Preference buyingStock = null;
		private Preference sellingStock = null;
		private Preference companyReturnStock = null;

		public DefaultStock() {
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
				UIComboBox.loadStocks(comboBox_defaultBuyingStock, "b");
				UIComboBox.loadStocks(comboBox_defaultSellingStock, "s");
				UIComboBox.loadStocks(comboBox_defaultCompanyReturnStock, "");

				buyingStock = preferenceManagerImpl.getPreference("defaultBuyingStock");
				sellingStock = preferenceManagerImpl.getPreference("defaultSellingStock");
				companyReturnStock = preferenceManagerImpl.getPreference("defaultCompanyReturnStock");

				comboBox_defaultBuyingStock.SelectedValue = Convert.ToInt32(buyingStock.Value);
				comboBox_defaultSellingStock.SelectedValue = Convert.ToInt32(sellingStock.Value);
				comboBox_defaultCompanyReturnStock.SelectedValue = Convert.ToInt32(companyReturnStock.Value);
			} catch(Exception) {
			}
		}

		private void button_save_Click(object sender, RoutedEventArgs e) {
			try {
				buyingStock.Value = comboBox_defaultBuyingStock.SelectedValue.ToString();
				sellingStock.Value = comboBox_defaultSellingStock.SelectedValue.ToString();
				companyReturnStock.Value = comboBox_defaultCompanyReturnStock.SelectedValue.ToString();
				CommonMethods.setCDMDForUpdate(buyingStock);
				CommonMethods.setCDMDForUpdate(sellingStock);
				CommonMethods.setCDMDForUpdate(companyReturnStock);
				preferenceManagerImpl.upd(buyingStock);
				preferenceManagerImpl.upd(sellingStock);
				preferenceManagerImpl.upd(companyReturnStock);
				ShowMessage.success(Common.Messages.Success.Success004);
			} catch(Exception) {
			}
		}
	}
}
