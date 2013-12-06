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
	/// Interaction logic for DiscountManager.xaml
	/// </summary>
	public partial class DiscountManager : UserControl {

		private DiscountManagerControler discountManagerControler = null;
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

		private DataTable dataTableUnitDiscount = null;
		public DataTable DataTableUnitDiscount {
			get { return dataTableUnitDiscount; }
			set { dataTableUnitDiscount = value; }
		}

		private DataTable dataTablePackDiscount = null;
		public DataTable DataTablePackDiscount {
			get { return dataTablePackDiscount; }
			set { dataTablePackDiscount = value; }
		}

		private bool isUnitUpdateMode = false;
		public bool IsUnitUpdateMode {
			get { return isUnitUpdateMode; }
			set { isUnitUpdateMode = value; }
		}

		private Discount unitDiscount = null;
		internal Discount UnitDiscount {
			get { return unitDiscount; }
			set { unitDiscount = value; }
		}

		private bool isPackUpdateMode = false;
		public bool IsPackUpdateMode {
			get { return isPackUpdateMode; }
			set { isPackUpdateMode = value; }
		}

		private Discount packDiscount = null;
		internal Discount PackDiscount {
			get { return packDiscount; }
			set { packDiscount = value; }
		}

		public DiscountManager() {
			InitializeComponent();
			discountManagerControler = new DiscountManagerControler(this);
			discountManagerControler.loadUI();
		}

		public DiscountManager(int itemId) {
			InitializeComponent();
			discountManagerControler = new DiscountManagerControler(this);
			this.SelectedItemId = itemId;
			discountManagerControler.loadUI();
		}

		private void textBox_selectedItemId_TextChanged(object sender, TextChangedEventArgs e) {
			discountManagerControler.textBox_selectedItemId_TextChanged();
		}

		private void button_addUnitDiscount_Click(object sender, RoutedEventArgs e) {
			discountManagerControler.button_addUnitDiscount_Click();
		}

		private void button_addPackDiscount_Click(object sender, RoutedEventArgs e) {
			discountManagerControler.button_addPackDiscount_Click();
		}

		private void textBox_unitQuantity_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				discountManagerControler.button_addUnitDiscount_Click();
			}
		}

		private void textBox_unitDiscount_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				discountManagerControler.button_addUnitDiscount_Click();
			}
		}

		private void textBox_packQuantity_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				discountManagerControler.button_addPackDiscount_Click();
			}
		}

		private void textBox_packDiscount_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				discountManagerControler.button_addPackDiscount_Click();
			}
		}

		private void dataGrid_unitDiscount_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			discountManagerControler.dataGrid_unitDiscount_MouseDoubleClick();
		}

		private void dataGrid_packDiscount_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			discountManagerControler.dataGrid_packDiscount_MouseDoubleClick();
		}

		private void button_deleteUnitDiscount_Click(object sender, RoutedEventArgs e) {
			discountManagerControler.button_deleteUnitDiscount_Click();
		}

		private void button_deletePackDiscount_Click(object sender, RoutedEventArgs e) {
			discountManagerControler.button_deletePackDiscount_Click();
		}

		private void dataGrid_unitDiscount_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Delete) {
				discountManagerControler.button_deleteUnitDiscount_Click();
			}
		}

		private void dataGrid_packDiscount_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Delete) {
				discountManagerControler.button_deletePackDiscount_Click();
			}
		}
	}
}
