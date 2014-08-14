using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Windows {
	/// <summary>
	/// Interaction logic for Login.xaml
	/// </summary>
	public partial class Login : Window {

		UserManagerControler userManagerControler = null;		

		public Login() {
			InitializeComponent();
			userManagerControler = new UserManagerControler(this);
			textBlock_licensedTo.Text = Session.Preference["licensedTo"];

			SellingInvoiceManagerImpl sellingInvoiceManagerImpl = new SellingInvoiceManagerImpl();
			sellingInvoiceManagerImpl.assignInvoiceNumbersToSellingInvoicesWithoutOne();
		}

		private void button_quit_Click(object sender, RoutedEventArgs e) {
			this.Close();
		}

		private void Window_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Escape) {
				this.Close();
			}
		}

		private void button_login_Click(object sender, RoutedEventArgs e) {
			userManagerControler.loginUser();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			textBox_username.Focus();
		}
	}
}
