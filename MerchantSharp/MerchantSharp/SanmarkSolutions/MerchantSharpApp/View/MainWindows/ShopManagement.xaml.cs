using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.MainWindows {
	/// <summary>
	/// Interaction logic for ShopManagement.xaml
	/// </summary>
	public partial class ShopManagement : UserControl {
		public ShopManagement() {
			InitializeComponent();
			loadElementsForPlermission();
		}

		private void loadElementsForPlermission() {
			try {
				if(Session.Permission["canAccessUserManager"] == 0) {
					grid_userManager.IsEnabled = false;
				}
				if(Session.Permission["canAccessExpenseManager"] == 0) {
					grid_expenseManager.IsEnabled = false;
				}
				if(Session.Permission["canAccessCategoryManager"] == 0) {
					grid_categoryManager.IsEnabled = false;
				}
				/////////////////////////////////////////////////////////////////////////////////
				/////////////////////////////////////////////////////////////////////////////////
				/////////////////////////////////////////////////////////////////////////////////

				if(Session.Permission["canAccessItemManager"] == 0) {
					grid_itemManager.IsEnabled = false;
				}
				if(Session.Permission["canAccessUnitManager"] == 0) {
					grid_unitManager.IsEnabled = false;
				}
				if(Session.Permission["canAccessDiscountManager"] == 0) {
					grid_discountManager.IsEnabled = false;
				}
			} catch(Exception) {
			}
		}

		private void grid_itemManager_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new ItemManager(), "Item Manager");
		}

		private void grid_discountManager_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new DiscountManager(), "Discount Manager");
		}

		private void grid_userManager_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new UserManager(), "User Manager");
		}

		private void grid_expenseManager_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new ExpenseManager(), "Expense Manager");
		}

		private void grid_unitManager_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new UnitManager(), "Unit Manager");
		}

		private void grid_categoryManager_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new CategoryManager(), "Category Manager");
		}
	}
}
