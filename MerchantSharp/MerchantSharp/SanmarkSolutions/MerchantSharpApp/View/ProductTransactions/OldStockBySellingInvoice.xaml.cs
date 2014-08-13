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
	/// Interaction logic for OldStockBySellingInvoice.xaml
	/// </summary>
	public partial class OldStockBySellingInvoice : UserControl, IFilter {

		private OldStockBySellingInvoiceControler oldStockBySellingInvoiceControler = null;

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

		private ItemFinder itemFinder = null;
		public ItemFinder ItemFinder {
			get { return itemFinder; }
			set { itemFinder = value; }
		}

		private DataTable dataTable = null;
		public DataTable DataTable {
			get { return dataTable; }
			set { dataTable = value; }
		}

		private DataGridFooter dataGridFooter = null;
		public DataGridFooter DataGridFooter {
			get { return dataGridFooter; }
			set { dataGridFooter = value; }
		}
		
		public OldStockBySellingInvoice() {
			InitializeComponent();
			oldStockBySellingInvoiceControler = new OldStockBySellingInvoiceControler(this);
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			oldStockBySellingInvoiceControler.UserControl_Loaded();
		}

		public void filter() {
			oldStockBySellingInvoiceControler.filter();
		}

		public void setPagination() {
			oldStockBySellingInvoiceControler.setRowsCount();
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			oldStockBySellingInvoiceControler.setRowsCount();
		}

		private void datePicker_dateFrom_filter_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				oldStockBySellingInvoiceControler.setRowsCount();
			}
		}

		private void datePicker_dateTo_filter_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				oldStockBySellingInvoiceControler.setRowsCount();
			}
		}

		private void UserControl_KeyDown( object sender, KeyEventArgs e ) {
			try {
				if ( e.Key == Key.F3 ) {
					itemFinder.textBox_barcode.Focus();
				} else if ( e.Key == Key.F4 ) {
					itemFinder.textBox_itemCode.Focus();
				}
			} catch ( Exception ) {
			}
		}
	}
}
