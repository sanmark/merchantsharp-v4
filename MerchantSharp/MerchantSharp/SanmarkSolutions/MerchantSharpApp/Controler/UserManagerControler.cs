using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.MainWindows;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Settings;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	/// <summary>
	/// All controls of user management.
	/// </summary>
	class UserManagerControler {

		private UserManagerImpl userManagerImpl = null;
		private Login login = null;
		private UserManager userManager;
		private Profile profile;
		public UserManagerControler(Login login) {
			userManagerImpl = new UserManagerImpl(login);
			this.login = login;
		}

		public UserManagerControler(UserManager userManager) {
			this.userManager = userManager;
			userManagerImpl = new UserManagerImpl(userManager);
		}

		public UserManagerControler(Profile profile) {
			this.profile = profile;
			userManagerImpl = new UserManagerImpl(profile);
		}

		/// <summary>
		/// Will be logon user.
		/// </summary>
		internal void loginUser() {
			try {
				if(userManagerImpl.loginUser()) {
					Session.MainWindow = new MainWindow();
					Session.MainWindow.Show();
					login.Close();
				}
			} catch(Exception) {
			}
		}

		internal void UserControl_Loaded() {
			try {
				if(!userManager.IsLoadedUI) {
					userManagerImpl.UserControl_Loaded();
					userManager.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void filter() {
			try {
				userManagerImpl.filter();
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				userManagerImpl.setRowsCount();
			} catch(Exception) {
			}
		}

		internal void button_save_addUser_Click() {
			try {
				if(!userManager.IsUpdateMode) {
					if(userManagerImpl.addUser()) {
						ShowMessage.success(Common.Messages.Success.Success002);
						userManagerImpl.resetAddForm();
						setRowsCount();
					}
				} else {
					if(userManagerImpl.updateUser()) {
						ShowMessage.success(Common.Messages.Success.Success004);
						userManagerImpl.switchToAddMode();
						userManagerImpl.resetAddForm();
						setRowsCount();
					}
				}
			} catch(Exception) {
			}
		}

		internal void button_reset_addUser_Click() {
			try {
				userManagerImpl.switchToAddMode();
				userManagerImpl.resetAddForm();
			} catch(Exception) {
			}
		}

		internal void dataGrid_MouseDoubleClick() {
			try {
				if(userManager.dataGrid.SelectedItemID > 0) {
					userManagerImpl.switchToUpdateMode();
				}
			} catch(Exception) {
			}
		}

		///////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////

		internal void Prifile_UserControl_Loaded() {
			try {
				if(!profile.IsLoadedUI) {
					userManagerImpl.Prifile_UserControl_Loaded();
					profile.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void updateProfile() {
			try {
				if(userManagerImpl.updateProfile()) {
					ShowMessage.success(Common.Messages.Success.Success004);
				}
			} catch(Exception) {
			}
		}
	}
}
