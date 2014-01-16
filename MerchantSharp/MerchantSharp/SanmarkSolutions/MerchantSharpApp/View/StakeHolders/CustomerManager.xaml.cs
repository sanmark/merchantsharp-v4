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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.StakeHolders {
	/// <summary>
	/// Interaction logic for CustomerManager.xaml
	/// </summary>
	public partial class CustomerManager : UserControl, IFilter {

		private CustomerManagerControler customerManagerControler = null;

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

		private bool isUpdateMode = false;
		public bool IsUpdateMode {
			get { return isUpdateMode; }
			set { isUpdateMode = value; }
		}

		private Customer selectedCustomer = null;
		internal Customer SelectedCustomer {
			get { return selectedCustomer; }
			set { selectedCustomer = value; }
		}

		public CustomerManager() {
			InitializeComponent();
			customerManagerControler = new CustomerManagerControler(this);
		}

		public void filter() {
			customerManagerControler.filter();
		}

		public void setPagination() {
			customerManagerControler.setRowsCount();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			customerManagerControler.UserControl_Loaded();
		}

		private void button_save_add_Click(object sender, RoutedEventArgs e) {
			customerManagerControler.button_save_add_Click();
		}

		private void button_reset_add_Click(object sender, RoutedEventArgs e) {
			customerManagerControler.button_reset_add_Click();
		}

		private void button_filter_Click(object sender, RoutedEventArgs e) {
			customerManagerControler.setRowsCount();
		}

		private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			customerManagerControler.dataGrid_MouseDoubleClick();
		}

		private void textBox_name_add_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				customerManagerControler.button_save_add_Click();
			}
		}

		private void textBox_address_add_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				customerManagerControler.button_save_add_Click();
			}
		}

		private void textBox_telephone_add_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				customerManagerControler.button_save_add_Click();
			}
		}

		private void textBox_account_add_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				customerManagerControler.button_save_add_Click();
			}
		}
	}
}
