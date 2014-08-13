using CustomControls.SanmarkSolutions.WPFCustomControls.MSComboBox;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler;
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
using System.Windows.Shapes;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.StakeHolders {
	/// <summary>
	/// Interaction logic for AddVendor.xaml
	/// </summary>
	public partial class AddVendor : Window {

		private MSComboBox mSComboBox;
		private int addedId = 0;
		public int AddedId {
			get { return addedId; }
			set { addedId = value; }
		}

		private VendorManagerControler vendorManagerControler = null;

		public AddVendor() {
			InitializeComponent();
		}

		public void resetForm() {
			try {
				textBox_name.Text = null;
				textBox_address.Text = null;
				textBox_telephone.Text = null;
			} catch(Exception) {

			}
		}

		public AddVendor(MSComboBox mSComboBox) {
			InitializeComponent();
			this.mSComboBox = mSComboBox;
			vendorManagerControler = new VendorManagerControler(this, mSComboBox);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			textBox_name.Focus();
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			vendorManagerControler.addVendor_addVendor();
		}

		private void Window_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Escape) {
				this.Hide();
				mSComboBox.SelectedIndex = -1;
			}
		}
	}
}
