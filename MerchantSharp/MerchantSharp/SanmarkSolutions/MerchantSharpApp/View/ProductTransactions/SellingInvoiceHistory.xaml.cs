using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
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
	/// Interaction logic for SellingInvoiceHistory.xaml
	/// </summary>
	public partial class SellingInvoiceHistory : UserControl, IFilter {

		private SellingInvoiceHistoryControler sellingInvoiceHistoryControler = null;

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

		public SellingInvoiceHistory() {
			InitializeComponent();
			sellingInvoiceHistoryControler = new SellingInvoiceHistoryControler(this);
		}

		public void filter() {
			sellingInvoiceHistoryControler.filter();
		}

		public void setPagination() {
			sellingInvoiceHistoryControler.setRowsCount();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			sellingInvoiceHistoryControler.UserControl_Loaded();
		}

		private void button_filter_Click(object sender, RoutedEventArgs e) {
			setPagination();
		}

		private void dataGrid_sellingInvoiceHistory_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			sellingInvoiceHistoryControler.dataGrid_sellingInvoiceHistory_MouseDoubleClick();	
		}
	}
}
