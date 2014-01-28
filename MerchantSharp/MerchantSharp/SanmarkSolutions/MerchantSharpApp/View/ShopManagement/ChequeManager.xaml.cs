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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement {
	/// <summary>
	/// Interaction logic for ChequeManager.xaml
	/// </summary>
	public partial class ChequeManager : UserControl, IFilter {

		private ChequeManagerControler chequeManagerControler = null;

		private bool isLoadedUI = false;
		public bool IsLoadedUI {
			get { return isLoadedUI; }
			set { isLoadedUI = value; }
		}

		private DataTable dataTable = null;
		public DataTable DataTable {
			get { return dataTable; }
			set { dataTable = value; }
		}

		private Pagination pagination = null;
		public Pagination Pagination {
			get { return pagination; }
			set { pagination = value; }
		}

		private BuyingCheque selectedBuyingCheque = null;
		internal BuyingCheque SelectedBuyingCheque {
			get { return selectedBuyingCheque; }
			set { selectedBuyingCheque = value; }
		}

		private SellingCheque selectedSellingCheque = null;
		internal SellingCheque SelectedSellingCheque {
			get { return selectedSellingCheque; }
			set { selectedSellingCheque = value; }
		}

		public ChequeManager() {
			InitializeComponent();
			chequeManagerControler = new ChequeManagerControler(this);
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			chequeManagerControler.UserControl_Loaded();
		}

		public void filter() {
			chequeManagerControler.filter();
		}

		public void setPagination() {
			chequeManagerControler.setRowsCount();
		}

		private void radioButton_buying_filter_Click(object sender, RoutedEventArgs e) {
			chequeManagerControler.radioButtonChecked();
		}

		private void radioButton_selling_filter_Click(object sender, RoutedEventArgs e) {
			chequeManagerControler.radioButtonChecked();
		}

		private void button_filter_Click(object sender, RoutedEventArgs e) {
			chequeManagerControler.setRowsCount();
		}

		private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			chequeManagerControler.dataGrid_MouseDoubleClick();
		}

		private void button_save_Click(object sender, RoutedEventArgs e) {
			chequeManagerControler.button_save_Click();
		}

		private void datePicker_issuedDate_update_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				chequeManagerControler.button_save_Click();
			}
		}

		private void datePicker_payableDate_update_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				chequeManagerControler.button_save_Click();
			}
		}

		private void textBox_amount_update_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				chequeManagerControler.button_save_Click();
			}
		}

		private void textBox_chequeNumber_update_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				chequeManagerControler.button_save_Click();
			}
		}

		private void comboBox_bank_update_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				chequeManagerControler.button_save_Click();
			}
		}

		private void comboBox_status_update_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				chequeManagerControler.button_save_Click();
			}
		}
	}
}
