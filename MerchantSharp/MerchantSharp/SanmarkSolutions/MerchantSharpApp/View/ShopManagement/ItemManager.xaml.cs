using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
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
	/// Interaction logic for ItemManager.xaml
	/// </summary>
	public partial class ItemManager : UserControl, IFilter {

		private ItemManagerControler itemManagerControler = null;

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

		private Item selectedItem = null;
		internal Item SelectedItem {
			get { return selectedItem; }
			set { selectedItem = value; }
		}

		public ItemManager() {
			InitializeComponent();
			itemManagerControler = new ItemManagerControler(this);
		}

		public void filter() {
			itemManagerControler.filter();
		}

		public void setPagination() {
			itemManagerControler.setRowsCount();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			itemManagerControler.UserControl_Loaded();
		}

		private void button_filter_Click(object sender, RoutedEventArgs e) {
			setPagination();
		}

		private void textBox_category_basicDetails_TextChanged(object sender, TextChangedEventArgs e) {
			UIListBox.loadCategories(listBox_category_basicDetails, textBox_category_basicDetails.Text);
		}

		private void textBox_company_basicDetails_TextChanged(object sender, TextChangedEventArgs e) {
			UIListBox.loadCompanies(listBox_company_basicDetails, textBox_company_basicDetails.Text);
		}

		private void checkBox_sip_sellingDetails_Click(object sender, RoutedEventArgs e) {
			itemManagerControler.checkBox_sip_sellingDetails_Click();
		}

		private void button_save_addItem_Click(object sender, RoutedEventArgs e) {
			itemManagerControler.button_save_addItem_Click();
		}

		private void dataGrid_items_items_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			itemManagerControler.dataGrid_items_items_MouseDoubleClick();
		}

		private void listBox_category_basicDetails_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			itemManagerControler.listBoxCatComChanged();
			textBox_company_basicDetails.Focus();
		}

		private void listBox_company_basicDetails_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			itemManagerControler.listBoxCatComChanged();
			textBox_itemName_basicDetails.Focus();
		}

		private void button_reset_addItem_Click(object sender, RoutedEventArgs e) {
			itemManagerControler.button_reset_addItem_Click();
		}

		private void textBox_category_basicDetails_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter && listBox_category_basicDetails.Items.Count > 0) {
				listBox_category_basicDetails.SelectedIndex = -1;
				listBox_category_basicDetails.SelectedIndex = 0;
			}
		}

		private void textBox_company_basicDetails_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter && listBox_company_basicDetails.Items.Count > 0) {
				listBox_company_basicDetails.SelectedIndex = -1;
				listBox_company_basicDetails.SelectedIndex = 0;
			}
		}

		private void textBox_itemName_basicDetails_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.button_save_addItem_Click();
			}
		}

		private void textBox_posName_basicDetails_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.button_save_addItem_Click();
			}
		}

		private void textBox_itemCode_basicDetails_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.button_save_addItem_Click();
			}
		}

		private void textBox_reorderLevel_sellingDetails_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.button_save_addItem_Click();
			}
		}

		private void comboBox_unit_sellingDetails_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.button_save_addItem_Click();
			}
		}

		private void textBox_unitsPerPack_sellingDetails_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.button_save_addItem_Click();
			}
		}

		private void textBox_packName_sellingDetails_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.button_save_addItem_Click();
			}
		}

		private void textBox_details_basicDetails_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.button_save_addItem_Click();
			}
		}

		private void textBox_shortCode_filter_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.setRowsCount();
			}
		}

		private void textBox_barCode_filter_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.setRowsCount();
			}
		}

		private void textBox_itemName_filter_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.setRowsCount();
			}
		}

		private void comboBox_category_filter_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.setRowsCount();
			}
		}

		private void comboBox_company_filter_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.setRowsCount();
			}
		}

		private void comboBox_sip_filter_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.setRowsCount();
			}
		}

		private void comboBox_unit_filter_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.setRowsCount();
			}
		}

		private void textBox_packName_filter_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.setRowsCount();
			}
		}

		private void textBox_itemDetails_filter_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.setRowsCount();
			}
		}

		private void comboBox_isActive_filter_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				itemManagerControler.setRowsCount();
			}
		}
	}
}
