using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.MainWindows;
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
		public UserManagerControler(Login login) {
			userManagerImpl = new UserManagerImpl(login);
			this.login = login;
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
	}
}
