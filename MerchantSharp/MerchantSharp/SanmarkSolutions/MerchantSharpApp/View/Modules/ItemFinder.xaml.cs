using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
using System;
using System.Collections.Generic;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules {
	/// <summary>
	/// Interaction logic for ItemFinder.xaml
	/// </summary>
	public partial class ItemFinder : UserControl {

		private TextBox textBox = null;
		TextBox textBoxCategory;
		TextBox textBoxCompany;
		private ItemManagerImpl itemManagerImpl = null;

		public ItemFinder(TextBox textBox) {
			InitializeComponent();
			this.textBox = textBox;
			loadBasicDetails();
			itemManagerImpl = new ItemManagerImpl();
		}

		public ItemFinder(TextBox textBoxCategory, TextBox textBoxCompany, TextBox textBoxItem) {
			InitializeComponent();
			this.textBoxCategory = textBoxCategory;
			this.textBoxCompany = textBoxCompany;
			this.textBox = textBoxItem;
			loadBasicDetails();
			itemManagerImpl = new ItemManagerImpl();
		}

		private void loadBasicDetails() {
			try {
				textBox_item_findItem.ListBox = listBox_list_findItem;
				UIListBox.recentItems(listBox_list_findItem, 100);
				UIComboBox.categoriesForSelect(comboBox_category_selectItem);
				UIComboBox.companiesForSelect(comboBox_company_selectItem);
			} catch(Exception) {
			}
		}

		private void comboBox_category_selectItem_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			try {
				UIComboBox.companiesForCategory(comboBox_company_selectItem, comboBox_category_selectItem.Value);
				if(textBoxCategory != null) {
					textBoxCategory.Text = Convert.ToString(comboBox_category_selectItem.SelectedValue);
				}
			} catch(Exception) {
			}
		}

		private void comboBox_company_selectItem_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			try {
				UIComboBox.itemsForCategoryAndCompany(comboBox_item_selectItem, comboBox_category_selectItem.Value, comboBox_company_selectItem.Value);
				if(textBoxCompany != null) {
					textBoxCompany.Text = Convert.ToString(comboBox_company_selectItem.SelectedValue);
				}
			} catch(Exception) {
			}
		}

		private void comboBox_item_selectItem_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			try {
				if(comboBox_item_selectItem.Value > 0) {
					textBox.Text = comboBox_item_selectItem.Value.ToString();
				}
			} catch(Exception) {
			}
		}

		private void textBox_item_findItem_TextChanged(object sender, TextChangedEventArgs e) {
			try {
				if(textBox_item_findItem.IsNull()) {
					UIListBox.recentItems(listBox_list_findItem, 100);
				} else {
					UIListBox.loadItemsByCategoryAndCompanyAndName(listBox_list_findItem, textBox_item_findItem.Text);
				}
			} catch(Exception) {
			}
		}

		private void listBox_list_findItem_KeyDown(object sender, KeyEventArgs e) {
			try {
				if(e.Key == Key.Enter) {
					textBox.Text = listBox_list_findItem.SelectedID.ToString();
				}
			} catch(Exception) {
			}
		}

		private void listBox_list_findItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			try {
				if(listBox_list_findItem.SelectedID > 0) {
					textBox.Text = listBox_list_findItem.SelectedID.ToString();
				}
			} catch(Exception) {
			}
		}

		private void textBox_barcode_KeyDown(object sender, KeyEventArgs e) {
			try {
				if(!String.IsNullOrWhiteSpace(textBox_barcode.Text) && e.Key == Key.Enter) {
					Item item = itemManagerImpl.getItemByBarcode(textBox_barcode.Text);
					if(item != null) {
						textBox.Text = item.Id.ToString();
					} else {
						ShowMessage.error(Common.Messages.Error.Error004);
					}
				}
			} catch(Exception) {
			}
		}

		private void textBox_itemCode_KeyDown(object sender, KeyEventArgs e) {
			try {
				if(!String.IsNullOrWhiteSpace(textBox_itemCode.Text) && e.Key == Key.Enter) {
					Item item = itemManagerImpl.getItemByCode(textBox_itemCode.Text);
					if(item != null) {
						textBox.Text = item.Id.ToString();
					} else {
						ShowMessage.error(Common.Messages.Error.Error004);
					}
				}
			} catch(Exception) {
			}
		}

		private void UserControl_KeyUp( object sender, KeyEventArgs e ) {
			try {
				if ( e.Key == Key.F3 ) {
					textBox_barcode.Focus();
				} else if ( e.Key == Key.F4 ) {
					textBox_itemCode.Focus();
				}
			} catch ( Exception ) {
			}
		}
	}
}
