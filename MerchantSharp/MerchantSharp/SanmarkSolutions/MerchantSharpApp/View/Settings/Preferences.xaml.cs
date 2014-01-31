using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Settings {
	/// <summary>
	/// Interaction logic for Preferences.xaml
	/// </summary>
	public partial class Preferences : Window {

		private DataTable dataTable = null;
		private PreferenceManagerImpl preferenceManagerImpl = null;

		public Preferences() {
			InitializeComponent();
			preferenceManagerImpl = new PreferenceManagerImpl();
			load();
		}

		private void load() {
			try {
				dataTable = new DataTable();
				dataTable.Columns.Add("Window", typeof(String));
				dataTable.Columns.Add("Title", typeof(String));

				dataTable.Rows.Add("AutoBackupLocation", "Auto Backup Location");
				if(Session.Meta["isActiveMultipleStocks"] == 1) {
					dataTable.Rows.Add("DefaultStock", "Stock Options");
					dataTable.Rows.Add("ManageStock", "Manage Stock");
				}
				dataTable.Rows.Add("DefaultItemSelect", "Item Options");
				dataTable.Rows.Add("UserPermissions", "User Permissions");
				dataTable.Rows.Add("SecureAuthentication", "Secure Authentication");

				listBox.ItemsSource = dataTable.DefaultView;
				listBox.SelectedValuePath = "Window";
				listBox.DisplayMemberPath = "Title";

				listBox.SelectedIndex = 0;
			} catch(Exception) {
			}
		}

		private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			try {
				Object obj = Activator.CreateInstance(Type.GetType(string.Format("{0}.{1}", "MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Settings.PreferenceViews", listBox.SelectedValue.ToString())));
				IPreferences p = (IPreferences)obj;
				p.setImpl(preferenceManagerImpl);
				grid_content.Children.Clear();
				grid_content.Children.Add((UserControl)obj);
			} catch(Exception) {
			}
		}

		private void Window_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Escape) {
				this.Close();
			}
		}
	}
}
