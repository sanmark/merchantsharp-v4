using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.MainWindows;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class UserManagerImpl {

		private IDao userDao;
		private Login login = null;

		public UserManagerImpl() {
			userDao = UserDao.getInstance();
		}

		public UserManagerImpl(Login login) {
			userDao = UserDao.getInstance();
			this.login = login;
		}

		public int add(Entity entity) {
			return userDao.add(entity);
		}

		public bool del(Entity entity) {
			return userDao.del(entity);
		}

		public List<User> get(Entity entity) {
			return userDao.get(entity).Cast<User>().ToList();
		}

		public int upd(Entity entity) {
			return userDao.upd(entity);
		}


		public List<User> getAllUsers() {
			return get(new User());
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

	}
}
