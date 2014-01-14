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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement {
	/// <summary>
	/// Interaction logic for SellingPriceManager.xaml
	/// </summary>
	public partial class SellingPriceManager : UserControl {

		private SellingPriceManagerControler sellingPriceManagerControler = null;

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

		private int selectedItemId = 0;
		public int SelectedItemId {
			get { return selectedItemId; }
			set { selectedItemId = value; }
		}

		private DataTable dataTableUnitPrice = null;
		public DataTable DataTableUnitPrice {
			get { return dataTableUnitPrice; }
			set { dataTableUnitPrice = value; }
		}

		private DataTable dataTablePackPrice = null;
		public DataTable DataTablePackPrice {
			get { return dataTablePackPrice; }
			set { dataTablePackPrice = value; }
		}

		private bool isUnitUpdateMode = false;
		public bool IsUnitUpdateMode {
			get { return isUnitUpdateMode; }
			set { isUnitUpdateMode = value; }
		}

		private SellingPrice unitPrice = null;
		internal SellingPrice UnitPrice {
			get { return unitPrice; }
			set { unitPrice = value; }
		}

		private bool isPackUpdateMode = false;
		public bool IsPackUpdateMode {
			get { return isPackUpdateMode; }
			set { isPackUpdateMode = value; }
		}

		private SellingPrice packPrice = null;
		internal SellingPrice PackPrice {
			get { return packPrice; }
			set { packPrice = value; }
		}

		public SellingPriceManager() {
			InitializeComponent();
			sellingPriceManagerControler = new SellingPriceManagerControler(this);
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			sellingPriceManagerControler.UserControl_Loaded();
		}

		private void textBox_selectedItemId_TextChanged(object sender, TextChangedEventArgs e) {
			sellingPriceManagerControler.textBox_selectedItemId_TextChanged();
		}

		private void button_addUnitPrice_Click(object sender, RoutedEventArgs e) {
			sellingPriceManagerControler.button_addUnitPrice_Click();
		}

		private void textBox_unitPrice_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				sellingPriceManagerControler.button_addUnitPrice_Click();
			}
		}

		private void dataGrid_unitPrice_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			sellingPriceManagerControler.dataGrid_unitPrice_MouseDoubleClick();
		}

		private void dataGrid_packPrice_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			sellingPriceManagerControler.dataGrid_packPrice_MouseDoubleClick();
		}

		private void button_addPackPrice_Click(object sender, RoutedEventArgs e) {
			sellingPriceManagerControler.button_addPackPrice_Click();
		}

		private void button_deletePackPrice_Click(object sender, RoutedEventArgs e) {
			sellingPriceManagerControler.button_deletePackPrice_Click();
		}

		private void button_deleteUnitPrice_Click(object sender, RoutedEventArgs e) {
			sellingPriceManagerControler.button_deleteUnitPrice_Click();
		}

		private void textBox_packPrice_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				sellingPriceManagerControler.button_addPackPrice_Click();
			}
		}
	}
}
