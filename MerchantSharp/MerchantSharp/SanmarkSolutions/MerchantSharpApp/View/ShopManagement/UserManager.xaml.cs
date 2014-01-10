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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement {
	/// <summary>
	/// Interaction logic for UserManager.xaml
	/// </summary>
	public partial class UserManager : UserControl, IFilter {

		private UserManagerControler userManagerControler = null;

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

		private User selectedUser = null;
		internal User SelectedUser {
			get { return selectedUser; }
			set { selectedUser = value; }
		}

		public UserManager() {
			InitializeComponent();
			userManagerControler = new UserManagerControler(this);
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			userManagerControler.UserControl_Loaded();
		}

		public void filter() {
			userManagerControler.filter();
		}

		public void setPagination() {
			userManagerControler.setPagination();
		}

		private void button_filter_Click(object sender, RoutedEventArgs e) {
			userManagerControler.setPagination();
		}

		private void button_save_addUser_Click(object sender, RoutedEventArgs e) {
			userManagerControler.button_save_addUser_Click();
		}

		private void button_reset_addUser_Click(object sender, RoutedEventArgs e) {
			userManagerControler.button_reset_addUser_Click();
		}

		private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			userManagerControler.dataGrid_MouseDoubleClick();
		}

		private void textBox_userName_addUser_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				userManagerControler.button_save_addUser_Click();
			}
		}

		private void textBox_firstName_addUser_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				userManagerControler.button_save_addUser_Click();
			}
		}

		private void textBox_lastName_addUser_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				userManagerControler.button_save_addUser_Click();
			}
		}

		private void textBox_password_addUser_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				userManagerControler.button_save_addUser_Click();
			}
		}

		private void textBox_confirmPassword_addUser_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				userManagerControler.button_save_addUser_Click();
			}
		}
	}
}
