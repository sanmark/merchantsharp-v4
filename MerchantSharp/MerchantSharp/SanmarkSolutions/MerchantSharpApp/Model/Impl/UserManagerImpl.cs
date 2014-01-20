using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.MainWindows;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Settings;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class UserManagerImpl {

		private IDao userDao;
		private Login login = null;
		private UserManager userManager;
		private Profile profile;
		private PermissionManagerImpl permissionManagerImpl = null;

		public UserManagerImpl() {
			userDao = UserDao.getInstance();
		}

		public UserManagerImpl(Login login) {
			userDao = UserDao.getInstance();
			this.login = login;
		}

		public UserManagerImpl(UserManager userManager) {
			this.userManager = userManager;
			userDao = UserDao.getInstance();
			permissionManagerImpl = new PermissionManagerImpl();
		}

		public UserManagerImpl(Profile profile) {
			this.profile = profile;
			userDao = UserDao.getInstance();
		}

		public int add(Entity entity) {
			try {
				if(Session.Permission["canAddUser"] == 1) {
					return userDao.add(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return 0;
		}

		public bool del(Entity entity) {
			return userDao.del(entity);
		}

		public List<User> get(Entity entity) {
			return userDao.get(entity).Cast<User>().ToList();
		}

		public int upd(Entity entity) {
			try {
				if(Session.Permission["canUpdateUser"] == 1) {
					return userDao.upd(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return 0;
		}


		public List<User> getAllUsers() {
			return get(new User());
		}

		public User getUserById(int id) {
			User user = null;
			try {
				User u = new User();
				u.Id = id;
				user = get(u)[0];
			} catch(Exception) {
			}
			return user;
		}

		public String getFullNameById(int id) {
			try {
				User user = getUserById(id);
				return user.FirstName + " " + user.LastName;
			} catch(Exception) {
				return "";
			}
		}

		public bool isDuplicate(String name, int id) {
			bool b = false;
			try {
				User user = new User();
				user.UserName = name;
				List<User> list = get(user);
				if(list.Count > 0 && id > 0) {
					foreach(User u in list) {
						if(u.Id != id) {
							b = true;
						}
					}
				} else if(list.Count > 0) {
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}


		//////////////////////////////////////  Manager methods //////////////////////////////////
		/// <summary>
		/// Will check user name and password what user entered and if success login, save the user just logged on in the Session.
		/// </summary>
		/// <returns>Returns true if looged.</returns>
		public bool loginUser() {
			bool b = false;
			try {
				bool isOkay = true;
				if(String.IsNullOrWhiteSpace(login.textBox_password.Password)) {
					isOkay = false;
				}
				if(login.textBox_username.IsNull()) {
					login.textBox_username.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					User user = new User();
					user.UserName = login.textBox_username.Text;
					user.Password = StringFormat.getSHA1(login.textBox_password.Password);
					user = get(user)[0];
					if(user.Status == 0) {
						ShowMessage.error(Common.Messages.Error.Error003);
					} else {
						Session.User = user;
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		///////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////

		internal void UserControl_Loaded() {
			try {
				UIComboBox.yesNoForSelect(userManager.comboBox_active_filter);
				userManager.DataTable = new DataTable();
				userManager.DataTable.Columns.Add("ID", typeof(int));
				userManager.DataTable.Columns.Add("User Name", typeof(String));
				userManager.DataTable.Columns.Add("First Name", typeof(String));
				userManager.DataTable.Columns.Add("Last Name", typeof(String));
				userManager.DataTable.Columns.Add("Is Active", typeof(String));
				userManager.dataGrid.DataContext = userManager.DataTable.DefaultView;

				userManager.Pagination = new Pagination();
				userManager.Pagination.Filter = userManager;
				userManager.grid_pagination.Children.Add(userManager.Pagination);

				setRowsCount();
			} catch(Exception) {
			}
		}

		private User getUserForFilter() {
			User user = null;
			try {
				user = new User();
				user.FirstName = userManager.textBox_firstName_filter.TrimedText + "%";
				user.LastName = userManager.textBox_lastName_filter.TrimedText + "%";
				user.Status = Convert.ToInt32(userManager.comboBox_active_filter.SelectedValue) > -1 ? Convert.ToInt32(userManager.comboBox_active_filter.SelectedValue) : -1;
			} catch(Exception) {
			}
			return user;
		}

		internal void filter() {
			try {
				userManager.DataTable.Rows.Clear();
				User u = getUserForFilter();
				u.LimitStart = userManager.Pagination.LimitStart;
				u.LimitEnd = userManager.Pagination.LimitCount;
				List<User> list = get(u);
				DataRow row = null;
				foreach(User user in list) {
					row = userManager.DataTable.NewRow();
					row[0] = user.Id;
					row[1] = user.UserName;
					row[2] = user.FirstName;
					row[3] = user.LastName;
					row[4] = CommonMethods.getYesNo(user.Status);

					userManager.DataTable.Rows.Add(row);
				}
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				User u = getUserForFilter();
				u.RowsCount = 1;
				List<User> list = get(u);
				userManager.Pagination.RowsCount = list[0].RowsCount;
			} catch(Exception) {
			}
		}

		public void resetAddForm() {
			try {
				userManager.textBox_userName_addUser.Clear();
				userManager.textBox_firstName_addUser.Clear();
				userManager.textBox_lastName_addUser.Clear();
				userManager.checkBox_active_addUser.IsChecked = true;
				userManager.textBox_password_addUser.Clear();
				userManager.textBox_confirmPassword_addUser.Clear();
			} catch(Exception) {
			}
		}

		internal bool addUser() {
			bool b = false;
			try {
				bool isOkay = true;
				if(String.IsNullOrWhiteSpace(userManager.textBox_password_addUser.Password) ||
					String.IsNullOrWhiteSpace(userManager.textBox_confirmPassword_addUser.Password) ||
					userManager.textBox_password_addUser.Password != userManager.textBox_confirmPassword_addUser.Password) {
					ShowMessage.error(Common.Messages.Error.Error015);
					isOkay = false;
				}
				if(userManager.textBox_firstName_addUser.IsNull()) {
					userManager.textBox_firstName_addUser.ErrorMode(true);
					isOkay = false;
				}
				if(userManager.textBox_userName_addUser.IsNull()) {
					userManager.textBox_userName_addUser.ErrorMode(true);
					isOkay = false;
				}
				if(isDuplicate(userManager.textBox_userName_addUser.TrimedText, 0)) {
					userManager.textBox_userName_addUser.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					User user = new User();
					user.UserName = userManager.textBox_userName_addUser.TrimedText;
					user.FirstName = userManager.textBox_firstName_addUser.TrimedText;
					user.LastName = userManager.textBox_lastName_addUser.TrimedText;
					user.Password = StringFormat.getSHA1(userManager.textBox_password_addUser.Password);
					user.Status = userManager.checkBox_active_addUser.IsChecked == true ? 1 : 0;
					int addedID = add(user);
					if(addedID > 0) {
						permissionManagerImpl.addPermissionUserForNewUser(addedID);
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal bool updateUser() {
			bool b = false;
			try {
				bool isOkay = true;
				if((!String.IsNullOrWhiteSpace(userManager.textBox_password_addUser.Password) || !String.IsNullOrWhiteSpace(userManager.textBox_confirmPassword_addUser.Password)) && (userManager.textBox_password_addUser.Password != userManager.textBox_confirmPassword_addUser.Password)) {
					ShowMessage.error(Common.Messages.Error.Error015);
					isOkay = false;
				}
				if(userManager.textBox_firstName_addUser.IsNull()) {
					userManager.textBox_firstName_addUser.ErrorMode(true);
					isOkay = false;
				}
				if(userManager.textBox_userName_addUser.IsNull()) {
					userManager.textBox_userName_addUser.ErrorMode(true);
					isOkay = false;
				}
				if(isDuplicate(userManager.textBox_userName_addUser.TrimedText, userManager.SelectedUser.Id)) {
					userManager.textBox_userName_addUser.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					User user = userManager.SelectedUser;
					user.UserName = userManager.textBox_userName_addUser.TrimedText;
					user.FirstName = userManager.textBox_firstName_addUser.TrimedText;
					user.LastName = userManager.textBox_lastName_addUser.TrimedText;
					if(!String.IsNullOrWhiteSpace(userManager.textBox_password_addUser.Password)) {
						user.Password = StringFormat.getSHA1(userManager.textBox_password_addUser.Password);
					}
					user.Status = userManager.checkBox_active_addUser.IsChecked == true ? 1 : 0;
					upd(user);
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void switchToAddMode() {
			try {
				userManager.SelectedUser = null;
				userManager.IsUpdateMode = false;
				userManager.button_save_addUser.Content = "Save";
				userManager.button_reset_addUser.Content = "Reset";
			} catch(Exception) {
			}
		}

		internal void switchToUpdateMode() {
			try {
				userManager.IsUpdateMode = true;
				User user = getUserById(userManager.dataGrid.SelectedItemID);
				userManager.SelectedUser = user;
				userManager.textBox_userName_addUser.Text = user.UserName;
				userManager.textBox_firstName_addUser.Text = user.FirstName;
				userManager.textBox_lastName_addUser.Text = user.LastName;
				userManager.checkBox_active_addUser.IsChecked = user.Status == 1 ? true : false;

				userManager.button_save_addUser.Content = "Update";
				userManager.button_reset_addUser.Content = "Cancel";
			} catch(Exception) {
			}
		}


		///////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////



		internal void Prifile_UserControl_Loaded() {
			try {
				profile.textBox_userName.Text = Session.User.UserName;
				profile.textBox_firstName.Text = Session.User.FirstName;
				profile.textBox_lastName.Text = Session.User.LastName;
			} catch(Exception) {
			}
		}

		internal bool updateProfile() {
			bool b = false;
			try {
				bool isOkay = true;
				if((!String.IsNullOrWhiteSpace(profile.textBox_password.Password) || !String.IsNullOrWhiteSpace(profile.textBox_confirmPassword.Password)) && (profile.textBox_password.Password != profile.textBox_confirmPassword.Password)) {
					ShowMessage.error(Common.Messages.Error.Error015);
					isOkay = false;
				}
				if(profile.textBox_firstName.IsNull()) {
					profile.textBox_firstName.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					Session.User.FirstName = profile.textBox_firstName.TrimedText;
					Session.User.LastName = profile.textBox_lastName.TrimedText;
					if(!String.IsNullOrWhiteSpace(profile.textBox_password.Password)) {
						Session.User.Password = StringFormat.getSHA1(profile.textBox_password.Password);
					}
					upd(Session.User);
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}
	}
}
