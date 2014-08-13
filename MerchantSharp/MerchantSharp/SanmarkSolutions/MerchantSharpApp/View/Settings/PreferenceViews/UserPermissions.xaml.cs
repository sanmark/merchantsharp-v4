using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Settings.PreferenceViews {
	/// <summary>
	/// Interaction logic for UserPermissions.xaml
	/// </summary>
	public partial class UserPermissions : UserControl, IPreferences {

		private PermissionManagerImpl permissionManagerImpl = null;
		List<Permission> listPermission = null;
		List<PermissionUser> listPermissionUser = null;
		Dictionary<int, CheckBox> dic = null;
		

		public UserPermissions() {
			InitializeComponent();
			permissionManagerImpl = new PermissionManagerImpl();
			dic = new Dictionary<int, CheckBox>();
		}

		public void setImpl(object obj) {

		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			try {
				UIComboBox.usersForFilter(comboBox_user);
				Permission p = new Permission();
				p.Parent = 0;
				//p.OrderBy = "parent ASC";
				listPermission = permissionManagerImpl.getPermission(p);
				int i = 0;
				foreach(Permission permission in listPermission) {
					CheckBox checkBox = new CheckBox();
					checkBox.Content = permission.Label;
					grid_main.RowDefinitions.Add(new RowDefinition());
					Grid.SetRow(checkBox, i);
					dic.Add(permission.Id, checkBox);
					grid_main.Children.Add(checkBox);
					i++;
					p = new Permission();
					p.Parent = permission.Id;
					List<Permission> list = permissionManagerImpl.getPermission(p);
					grid_main.ColumnDefinitions.Add(new ColumnDefinition());
					grid_main.ColumnDefinitions.Add(new ColumnDefinition());
					foreach(Permission per in list) {
						CheckBox ch = new CheckBox();
						ch.Content = per.Label;
						grid_main.RowDefinitions.Add(new RowDefinition());
						Grid.SetRow(ch, i);
						Grid.SetColumn(ch, 1);
						dic.Add(per.Id, ch);
						grid_main.Children.Add(ch);
						i++;
					}
				}
			} catch(Exception) {
			}
		}

		private void resetAllCheckBoxes() {
			try {
				foreach(CheckBox c in dic.Values) {
					c.IsChecked = false;
				}
			} catch(Exception) {
			}
		}

		private void comboBox_user_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			try {
				int userID = Convert.ToInt32(comboBox_user.SelectedValue);
				if(userID > 0) {
					PermissionUser pu = new PermissionUser();
					pu.UserId = userID;
					listPermissionUser = permissionManagerImpl.getPermissionUser(pu);
					foreach(PermissionUser permissionUser in listPermissionUser) {
						dic[permissionUser.PermissionId].IsChecked = permissionUser.Value == 1 ? true : false;
					}
				} else {
					listPermissionUser = null;
					resetAllCheckBoxes();
				}
			} catch(Exception) {
			}
		}

		private void button_save_Click(object sender, RoutedEventArgs e) {
			try {
				foreach(PermissionUser permissionUser in listPermissionUser) {
					permissionUser.Value = dic[permissionUser.PermissionId].IsChecked == true ? 1 : 0;
					CommonMethods.setCDMDForUpdate(permissionUser);
					permissionManagerImpl.updPermissionUser(permissionUser);
				}
				ShowMessage.success(Common.Messages.Success.Success004);
			} catch(Exception) {
			}
		}
	}
}
