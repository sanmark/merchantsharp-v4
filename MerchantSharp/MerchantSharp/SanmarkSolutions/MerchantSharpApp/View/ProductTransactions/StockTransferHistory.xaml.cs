using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions {
	/// <summary>
	/// Interaction logic for StockTransactionHistory.xaml
	/// </summary>
	public partial class StockTransferHistory : UserControl, IFilter {

		private StockTransferControler stockTransferControler = null;

		private bool isLoadedUI = false;
		public bool IsLoadedUI {
			get { return isLoadedUI; }
			set { isLoadedUI = value; }
		}

		private Pagination pagination = null;
		public Pagination Pagination {
			get { return pagination; }
			set { pagination = value; }
		}

		private DataTable dataTable = null;
		public DataTable DataTable {
			get { return dataTable; }
			set { dataTable = value; }
		}

		public StockTransferHistory() {
			InitializeComponent();
			stockTransferControler = new StockTransferControler(this);
		}

		public void filter() {
			stockTransferControler.filter();
		}

		public void setPagination() {
			stockTransferControler.setRowsCount();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			stockTransferControler.stockTransferHistoryLoaded();
		}

		private void button_filter_Click(object sender, RoutedEventArgs e) {
			stockTransferControler.setRowsCount();
		}

		private void dataGrid_stockTransferHistory_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			try {
				if(dataGrid_stockTransferHistory.SelectedItemID > 0) {
					ThreadPool.openTab(new AddStockTransfer(dataGrid_stockTransferHistory.SelectedItemID), "View Stock Transfer");
				}
			} catch(Exception) {
			}
		}

	}
}
