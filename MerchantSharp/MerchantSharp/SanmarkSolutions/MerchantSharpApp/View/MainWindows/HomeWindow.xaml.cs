using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
	/// Interaction logic for HomeWindow.xaml
	/// </summary>
	public partial class HomeWindow : UserControl {

		private static PermissionManagerImpl permissionManagerImpl = null;
		private bool isLoaded = false;

		public HomeWindow() {
			InitializeComponent();
			permissionManagerImpl = new PermissionManagerImpl();
		}

		private void loadFeaturesByPermission() {
			try {
				if(!isLoaded) {
					Thread t = new Thread(delegate() {
						loadFeaturesCallBack();
					});
					t.Start();
					isLoaded = true;
				}
			} catch(Exception) {
			}
		}

		private TabItem getTabItem(UserControl userControl, String header) {
			TabItem item = new TabItem();
			try {
				item.Header = header;
				item.Content = userControl;
			} catch(Exception) {
			}
			return item;
		}

		private void loadFeaturesCallBack() {
			try {
				this.Dispatcher.Invoke((Action)(() => {
					Session.Permission = permissionManagerImpl.getPermissionForSession(Session.User.Id);
					if(Session.Permission["canAccessProductTransactions"] == 1) {
						Session.MainWindow.HomeWindow.homeTabControl.Items.Add(getTabItem(new ProductTransactions(), "PRODUCT TRANSACTIONS"));
					}
					if(Session.Permission["canAccessReports"] == 1) {
						Session.MainWindow.HomeWindow.homeTabControl.Items.Add(getTabItem(new Reports(), "REPORTS"));
					}
					if(Session.Permission["canAccessShopManagement"] == 1) {
						Session.MainWindow.HomeWindow.homeTabControl.Items.Add(getTabItem(new ShopManagement(), "SHOP MANAGEMENT"));
					}
					if(Session.Permission["canAccessStakeHolders"] == 1) {
						Session.MainWindow.HomeWindow.homeTabControl.Items.Add(getTabItem(new StakeHolders(), "STAKE HOLDERS"));
					}
					if(Session.Permission["canAccessTools"] == 1) {
						Session.MainWindow.HomeWindow.homeTabControl.Items.Add(getTabItem(new Tools(), "TOOLS"));
					}
					if(Session.Permission["canAccessSettings"] == 1) {
						Session.MainWindow.HomeWindow.homeTabControl.Items.Add(getTabItem(new Settings(), "SETTINGS"));
					}
					try {
						((TabItem)Session.MainWindow.HomeWindow.homeTabControl.Items[0]).Focus();
					} catch(Exception) {
					}
				}));

			} catch(Exception) {
			}
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			loadFeaturesByPermission();
		}
	}
}
