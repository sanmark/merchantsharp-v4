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
	/// Interaction logic for BuyingInvoiceHistory.xaml
	/// </summary>
	public partial class BuyingInvoiceHistory : UserControl, IFilter {

		private BuyingInvoiceHistoryControler buyingInvoiceHistoryControler = null;

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

		private bool isRequest = false;
		public bool IsRequest {
			get { return isRequest; }
		}

		private DataGridFooter dataGridFooter = null;
		public DataGridFooter DataGridFooter {
			get { return dataGridFooter; }
			set { dataGridFooter = value; }
		}

		public BuyingInvoiceHistory() {
			InitializeComponent();
			buyingInvoiceHistoryControler = new BuyingInvoiceHistoryControler(this);
		}

		public BuyingInvoiceHistory(bool isRequest) {
			InitializeComponent();
			buyingInvoiceHistoryControler = new BuyingInvoiceHistoryControler(this);
			this.isRequest = isRequest;
		}

		public void filter() {
			buyingInvoiceHistoryControler.filter();
		}

		public void setPagination() {
			buyingInvoiceHistoryControler.setRowsCount();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			buyingInvoiceHistoryControler.UserControl_Loaded();
		}

		private void button_filter_Click(object sender, RoutedEventArgs e) {
			setPagination();
			filter();
		}

		private void dataGrid_buyingInvoiceHistory_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			buyingInvoiceHistoryControler.dataGrid_buyingInvoiceHistory_MouseDoubleClick();
		}

		private void datePicker_general_KeyUp(object sender, KeyEventArgs e) {
			if (e.Key == Key.Enter) {
				filter();
			}
		}

		private void dataGrid_buyingInvoiceHistory_KeyUp(object sender, KeyEventArgs e) {
			if (e.Key == Key.Delete) {
				buyingInvoiceHistoryControler.deleteInvoice();
			}
		}

	}
}
