using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Reports;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.MainWindows {
	/// <summary>
	/// Interaction logic for Reports.xaml
	/// </summary>
	public partial class Reports : UserControl {
		public Reports() {
			InitializeComponent();
			loadElementsForPlermission();
		}

		private void loadElementsForPlermission() {
			try {
				if(Session.Permission["canAccessDailySale"] == 0) {
					//grid_dailySale.IsEnabled = false;
				}
				if(Session.Permission["canAccessDailyItemSale"] == 0) {
					//grid_dailyItemSale.IsEnabled = false;
				}
				if(Session.Permission["canAccessDailyProfit"] == 0) {
					//grid_dailyProfits.IsEnabled = false;
				}
				if(Session.Permission["canAccessProfitPerItem"] == 0) {
					//grid_dailyProfitsPerItem.IsEnabled = false;
				}
				if(Session.Permission["canAccessBuyingCheques"] == 0) {
					//grid_buyingCheques.IsEnabled = false;
				}
				if(Session.Permission["canAccessSellingCheques"] == 0) {
					//grid_sellingCheques.IsEnabled = false;
				}
			} catch(Exception) {
			}
		}

		private void grid_dailySale_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			if(Session.Permission["canAccessDailySale"] == 0) {
				ShowMessage.error(Common.Messages.Error.Error016);
			} else {
				ThreadPool.openTab(new DailySale(), "Daily Sale");
			}
		}

		private void grid_dailyItemSale_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			if(Session.Permission["canAccessDailyItemSale"] == 0) {
				ShowMessage.error(Common.Messages.Error.Error016);
			} else {
				ThreadPool.openTab(new DailyItemSale(), "Daily ItemSale");
			}
		}

		private void grid_dailyProfits_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			if(Session.Permission["canAccessDailyProfit"] == 0) {
				ShowMessage.error(Common.Messages.Error.Error016);
			} else {
				ThreadPool.openTab(new DailyProfit(), "Daily Profit");
			}
		}

		private void grid_dailyProfitsPerItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			if(Session.Permission["canAccessProfitPerItem"] == 0) {
				ShowMessage.error(Common.Messages.Error.Error016);
			} else {
				ThreadPool.openTab(new ProfitPerItem(), "Profit Per Item");
			}
		}

		private void grid_buyingCheques_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			if(Session.Permission["canAccessBuyingCheques"] == 0) {
				ShowMessage.error(Common.Messages.Error.Error016);
			} else {
				ThreadPool.openTab(new BuyingChequeRreport(), "Buying Cheque");
			}
		}

		private void grid_sellingCheques_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			if(Session.Permission["canAccessSellingCheques"] == 0) {
				ShowMessage.error(Common.Messages.Error.Error016);
			} else {
				ThreadPool.openTab(new SellingChequeReport(), "Selling Cheque");
			}
		}
	}
}
