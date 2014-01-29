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
	/// Interaction logic for VendorManager.xaml
	/// </summary>
	public partial class VendorManager : UserControl, IFilter {

		private VendorManagerControler vendorManagerControler;

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

		private bool isUpdateMode = false;
		public bool IsUpdateMode {
			get { return isUpdateMode; }
			set { isUpdateMode = value; }
		}

		private Vendor selectedVendor = null;
		internal Vendor SelectedVendor {
			get { return selectedVendor; }
			set { selectedVendor = value; }
		}

		private DataGridFooter dataGridFooter = null;
		public DataGridFooter DataGridFooter {
			get { return dataGridFooter; }
			set { dataGridFooter = value; }
		}
		
		public VendorManager() {
			InitializeComponent();
			vendorManagerControler = new VendorManagerControler(this);
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			vendorManagerControler.UserControl_Loaded();
		}

		public void filter() {
			vendorManagerControler.filter();
		}

		public void setPagination() {
			vendorManagerControler.setRowsCount();
		}

		private void button_filter_Click(object sender, RoutedEventArgs e) {
			vendorManagerControler.setRowsCount();
		}

		private void button_save_addVendor_Click(object sender, RoutedEventArgs e) {
			vendorManagerControler.button_save_addVendor_Click();
		}

		private void button_reset_addVendor_Click(object sender, RoutedEventArgs e) {
			vendorManagerControler.button_reset_addVendor_Click();
		}

		private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			vendorManagerControler.dataGrid_MouseDoubleClick();
		}

		private void textBox_name_addVendor_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				vendorManagerControler.button_save_addVendor_Click();
			}
		}

		private void textBox_address_addVendor_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				vendorManagerControler.button_save_addVendor_Click();
			}
		}

		private void textBox_telephone_addVendor_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				vendorManagerControler.button_save_addVendor_Click();
			}
		}

		private void textBox_account_addVendor_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				vendorManagerControler.button_save_addVendor_Click();
			}
		}
	}
}
