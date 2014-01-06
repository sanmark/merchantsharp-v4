using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
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
	/// Interaction logic for ProductTransactions.xaml
	/// </summary>
	public partial class ProductTransactions : UserControl {
		public ProductTransactions() {
			InitializeComponent();
			loadElementsForPlermission();
		}

		private void loadElementsForPlermission() {
			try {
				/// Disable Request Buying Invoice Section
				if(Session.Permission["canAddRequestBuyingInvoice"] == 0) {
					grid_addRequestBuyingInvoice.IsEnabled = false;
				}
				if(Session.Permission["canAccessRequestBuyingInvoiceHistory"] == 0) {
					grid_viewRequestInvoices.IsEnabled = false;
				}

				/// Disable Buying Invoice Section
				if(Session.Permission["canAddBuyingInvoice"] == 0) {
					grid_addBuyingInvoice.IsEnabled = false;
				}
				if(Session.Permission["canAccessBuyingInvoiceHistory"] == 0) {
					grid_buyingInvoiceHistory.IsEnabled = false;
				}
				if(Session.Permission["canAccessBuyingItemHistory"] == 0) {
					grid_buyingItemHistory.IsEnabled = false;
				}

				/// Disable Inventory Section
				if(Session.Permission["canAccessStockManager"] == 0) {
					grid_stockManagement.IsEnabled = false;
				}
				if(Session.Permission["canAddStockTransaction"] == 0) {
					grid_addStockTransfer.IsEnabled = false;
				}
				if(Session.Permission["canAccessStockTransactionHistory"] == 0) {
					grid_stockTransactionHistory.IsEnabled = false;
				}
				if(Session.Permission["canAccessOldStockBySellingInvoice"] == 0) {
					grid_oldStockBySellingInvoice.IsEnabled = false;
				}

				/// Disable Selling Invoice Section
				if(Session.Permission["canAddSellingInvoice"] == 0) {
					grid_addSellingInvoice.IsEnabled = false;
				}
				if(Session.Permission["canAccessSellingInvoiceHistory"] == 0) {
					grid_sellingInvoiceHistory.IsEnabled = false;
				}
				if(Session.Permission["canAccessSellingItemHistory"] == 0) {
					grid_sellingItemHistory.IsEnabled = false;
				}
				if(Session.Permission["canAddSellingInvoicePayment"] == 0) {
					grid_addSellingInvoicePayment.IsEnabled = false;
				}
			} catch(Exception) {
			}
		}

		private void grid_addRequestBuyingInvoice_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new AddBuyingInvoice(true), "Add Request Buying Invoice");
		}

		private void grid_viewRequestInvoices_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new BuyingInvoiceHistory(true), "Request Invoice History");
		}

		private void grid_addBuyingInvoice_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new AddBuyingInvoice(false), "Add Buying Invoice");
		}

		private void grid_buyingInvoiceHistory_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new BuyingInvoiceHistory(), "Buying Invoice History");
		}

		private void grid_buyingItemHistory_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new BuyingItemHistory(), "Buying Item History");
		}

		private void grid_addSellingInvoice_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new AddSellingInvoice(), "Add Selling Invoice");
		}

		private void grid_sellingInvoiceHistory_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new SellingInvoiceHistory(), "Selling Invoice History");
		}

		private void grid_stockManagement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new StockManager(), "Stock Manager");
		}

		private void grid_addSellingInvoicePayment_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {

		}

		private void grid_sellingItemHistory_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new SellingItemHistory(), "Selling Item History");
		}

		private void grid_addStockTransfer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new AddStockTransfer(), "Add Stock Transfer");
		}
	}
}
