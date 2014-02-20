using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
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
using System.Windows.Shapes;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions {
	/// <summary>
	/// Interaction logic for AddReturn.xaml
	/// </summary>
	public partial class AddCompanyReturn : Window {

		private CompanyReturnManagerControler companyReturnManagerControler = null;

		private DataTable dataTable = null;
		public DataTable DataTable {
			get { return dataTable; }
			set { dataTable = value; }
		}

		private ItemFinder itemFinder = null;
		public ItemFinder ItemFinder {
			get { return itemFinder; }
			set { itemFinder = value; }
		}

		private Item selectedItem = null;
		internal Item SelectedItem {
			get { return selectedItem; }
			set { selectedItem = value; }
		}

		private int buyingInvoiceId = 0;
		public int BuyingInvoiceId {
			get { return buyingInvoiceId; }
			set { buyingInvoiceId = value; }
		}

		public AddCompanyReturn(int buyingInvoiceId) {
			InitializeComponent();
			this.buyingInvoiceId = buyingInvoiceId;
			companyReturnManagerControler = new CompanyReturnManagerControler(this);
		}

		private void Window_Loaded( object sender, RoutedEventArgs e ) {
			companyReturnManagerControler.Add_Window_Loaded();
		}

		private void Window_KeyUp( object sender, KeyEventArgs e ) {
			try {
				if ( e.Key == Key.Escape ) {
					this.Close();
				}
			} catch ( Exception ) {				
			}
		}

		private void textBox_itemId_TextChanged( object sender, TextChangedEventArgs e ) {
			companyReturnManagerControler.textBox_itemId_TextChanged();
		}

		private void button_addItem_Click( object sender, RoutedEventArgs e ) {
			companyReturnManagerControler.button_addItem_Click();
		}

		private void textBox_price_KeyDown( object sender, KeyEventArgs e ) {
			if ( e.Key == Key.Enter ) {
				companyReturnManagerControler.button_addItem_Click();
			}
		}

		private void textBox_quantity_KeyDown( object sender, KeyEventArgs e ) {
			if ( e.Key == Key.Enter ) {
				companyReturnManagerControler.button_addItem_Click();
			}
		}

	}
}
