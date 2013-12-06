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

		public ItemFinder(TextBox textBox) {
			InitializeComponent();
			this.textBox = textBox;
			loadBasicDetails();
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
			} catch(Exception) {
			}
		}

		private void comboBox_company_selectItem_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			try {
				UIComboBox.itemsForCategoryAndCompany(comboBox_item_selectItem, comboBox_category_selectItem.Value, comboBox_company_selectItem.Value);
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
	}
}
