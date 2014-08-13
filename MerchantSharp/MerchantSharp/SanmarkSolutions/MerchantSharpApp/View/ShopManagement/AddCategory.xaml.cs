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
	/// Interaction logic for AddCategory.xaml
	/// </summary>
	public partial class AddCategory : Window {

		private MSComboBox mSComboBox;
		private int addedId = 0;
		public int AddedId {
			get { return addedId; }
			set { addedId = value; }
		}

		private CategoryManagerControler categoryManagerControler = null;

		public AddCategory(MSComboBox comboBox) {
			InitializeComponent();
			this.mSComboBox = comboBox;
			categoryManagerControler = new CategoryManagerControler(this, mSComboBox);
		}

		public void resetForm() {
			try {
				textBox_name.Text = null;
			} catch(Exception) {

			}
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			categoryManagerControler.addCategory_addCategory();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			textBox_name.Focus();
		}

		private void Window_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Escape) {
				//mSComboBox.SelectedIndex = -1;
				this.Hide();
			}
		}
	}
}
