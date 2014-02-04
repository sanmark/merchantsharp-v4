using CustomControls.SanmarkSolutions.WPFCustomControls.MSComboBox;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler;
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
using System.Windows.Shapes;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement {
	/// <summary>
	/// Interaction logic for AddSellingPrice.xaml
	/// </summary>
	public partial class AddSellingPrice : Window {
		
		private SellingPriceManagerControler sellingPriceManagerControler = null;

		private int itemId = 0;
		public int ItemId {
			get { return itemId; }
			set { itemId = value; }
		}

		private String mode = "u";
		public String Mode {
			get { return mode; }
			set { 
				mode = value;
				/*if(mode == "p") {
					radioButton_pack.IsChecked = true;
				} else {
					radioButton_unit.IsChecked = true;
				}*/
			}
		}

		private MSComboBox comboBox = null;
		public MSComboBox ComboBox {
			get { return comboBox; }
			set { comboBox = value; }
		}		

		public AddSellingPrice(String mode) {
			InitializeComponent();
			this.mode = mode;
			sellingPriceManagerControler = new SellingPriceManagerControler(this);
		}

		private void Window_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Escape) {
				this.Hide();
				try {
					if(comboBox != null) {
						comboBox.SelectedIndex = -1;
					}
				} catch(Exception) {
				}
			}
		}

		private void button_addPrice_Click(object sender, RoutedEventArgs e) {
			sellingPriceManagerControler.button_addPrice_Click();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			textBox_price.Focus();
		}

		private void textBox_price_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				sellingPriceManagerControler.button_addPrice_Click();
			}
		}		
	}
}
