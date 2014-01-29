using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
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
	/// Interaction logic for AddSellingInvoicePayment.xaml
	/// </summary>
	public partial class AddSellingInvoicePayment : UserControl, IFilter {

		private PaymentManagerControler paymentManagerControler = null;

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

		private PaymentSection paymentSection = null;
		public PaymentSection PaymentSection {
			get { return paymentSection; }
			set { paymentSection = value; }
		}

		private DataTable dataTable = null;
		public DataTable DataTable {
			get { return dataTable; }
			set { dataTable = value; }
		}

		private SellingInvoice selectedInvoice = null;
		internal SellingInvoice SelectedInvoice {
			get { return selectedInvoice; }
			set { selectedInvoice = value; }
		}

		private DataGridFooter dataGridFooter = null;
		public DataGridFooter DataGridFooter {
			get { return dataGridFooter; }
			set { dataGridFooter = value; }
		}

		public AddSellingInvoicePayment() {
			InitializeComponent();
			paymentManagerControler = new PaymentManagerControler(this);
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			paymentManagerControler.addSellingInvoicePaymentUserControlLoaded();
		}

		public void filter() {
			paymentManagerControler.filterSellingInvoice();
		}

		public void setPagination() {
			paymentManagerControler.setRowsCountSellingInvoice();
		}

		private void button_filter_Click(object sender, RoutedEventArgs e) {
			paymentManagerControler.setRowsCountSellingInvoice();
		}

		private void dataGrid_stockItems_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			paymentManagerControler.sellingInvoicerid_MouseLeftButtonUp();
		}

		private void textBox_invoiceNumber_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				paymentManagerControler.sellingInvoiceNumber_Enter();
			}
		}

		private void button_save_Click(object sender, RoutedEventArgs e) {
			paymentManagerControler.saveSellingInvoice();
		}
	}
}
