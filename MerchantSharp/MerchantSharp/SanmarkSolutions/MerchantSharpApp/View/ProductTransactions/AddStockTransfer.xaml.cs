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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions {
	/// <summary>
	/// Interaction logic for AddStockTransaction.xaml
	/// </summary>
	public partial class AddStockTransfer : UserControl {

		private StockTransferControler stockTransactionControler = null;

		private bool isLoadedUI = false;
		public bool IsLoadedUI {
			get { return isLoadedUI; }
			set { isLoadedUI = value; }
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

		private DataTable dataTable = null;
		public DataTable DataTable {
			get { return dataTable; }
			set { dataTable = value; }
		}

		private bool isViewMode = false;
		public bool IsViewMode {
			get { return isViewMode; }
			set { isViewMode = value; }
		}

		private int selectedStockTransferID = 0;
		public int SelectedStockTransferID {
			get { return selectedStockTransferID; }
			set { selectedStockTransferID = value; }
		}

		public AddStockTransfer() {
			InitializeComponent();
			stockTransactionControler = new StockTransferControler(this);
		}

		public AddStockTransfer(int transferId) {
			InitializeComponent();
			isViewMode = true;
			selectedStockTransferID = transferId;
			stockTransactionControler = new StockTransferControler(this);
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			stockTransactionControler.addStockTransferLoaded();
		}

		private void textBox_itemId_TextChanged(object sender, TextChangedEventArgs e) {
			stockTransactionControler.textBox_itemId_TextChanged();
		}

		private void radioButton_unit_Click(object sender, RoutedEventArgs e) {
			stockTransactionControler.transferModeCheked();
		}

		private void radioButton_pack_Click(object sender, RoutedEventArgs e) {
			stockTransactionControler.transferModeCheked();
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			stockTransactionControler.Button_Click();
		}

		private void textBox_quantity_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				stockTransactionControler.Button_Click();
			}
		}

		private void dataGrid_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Delete) {
				stockTransactionControler.removeSelectedItem();
			}
		}

		private void button_saveTransfer_Click(object sender, RoutedEventArgs e) {
			stockTransactionControler.button_saveTransfer_Click();
		}

		private void UserControl_KeyDown(object sender, KeyEventArgs e) {
			try {
				if ( e.Key == Key.F11 ) {
					stockTransactionControler.button_saveTransfer_Click();
				} else if ( e.Key == Key.F3 ) {
					itemFinder.textBox_barcode.Focus();
				} else if ( e.Key == Key.F4 ) {
					itemFinder.textBox_itemCode.Focus();
				}
			} catch ( Exception ) {				
			}
		}

	}
}
