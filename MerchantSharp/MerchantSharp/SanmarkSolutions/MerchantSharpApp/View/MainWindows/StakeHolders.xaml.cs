using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.StakeHolders;
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
	/// Interaction logic for StakeHolders.xaml
	/// </summary>
	public partial class StakeHolders : UserControl {
		public StakeHolders() {
			InitializeComponent();
			loadElementsForPlermission();
		}

		private void loadElementsForPlermission() {
			try {
				if(Session.Permission["canAccessVendorManager"] == 0) {
					grid_vendorManager.IsEnabled = false;
				}
				if(Session.Permission["canAccessCustomerManager"] == 0) {
					grid_customerManager.IsEnabled = false;
				}
			} catch(Exception) {
			}
		}

		private void grid_vendorManager_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new VendorManager(), "Vendor Manager");
		}

		private void grid_customerManager_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new CustomerManager(), "Customer Manager");
		}
	}
}
