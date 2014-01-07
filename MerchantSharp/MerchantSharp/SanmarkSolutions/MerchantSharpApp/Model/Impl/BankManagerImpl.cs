using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.StakeHolders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class BankManagerImpl {

		private IDao dao = null;
		private   View.StakeHolders.AddBank addBank;

		public BankManagerImpl() {
			dao = BankDao.getInstance();
		}

		public BankManagerImpl(AddBank addBank) {
			this.addBank = addBank;
			dao = BankDao.getInstance();
		}

		/// <summary>
		/// Adds a new Entity.
		/// </summary>
		/// <param name="entity">Entity</param>
		/// <returns>Id of just Added Entity.</returns>
		public int add(Entity entity) {
			return dao.add(entity);
		}

		/// <summary>
		/// Deletes an Entity.
		/// </summary>
		/// <param name="entity">Entity</param>
		/// <returns>Will return true if successfully deleted.</returns>
		public bool del(Entity entity) {
			return dao.del(entity);
		}

		/// <summary>
		/// Return Entities.
		/// </summary>
		/// <param name="entity">Entity</param>
		/// <returns>List of Entities.</returns>
		public List<Bank> get(Entity entity) {
			return dao.get(entity).Cast<Bank>().ToList();
		}

		/// <summary>
		/// Updates an Entity.
		/// </summary>
		/// <param name="entity">Entity</param>
		/// <returns>Integer</returns>
		public int upd(Entity entity) {
			return dao.upd(entity);
		}

		/// <summary>
		/// Returns all Banks
		/// </summary>
		/// <returns>List of Banks.</returns>
		public List<Bank> getAllBanks() {
			return get(new Bank());
		}

		/// <summary>
		/// Check the bank name is duplicate when adding and updating.
		/// </summary>
		/// <param name="name">The name of Bank.</param>
		/// <param name="id">The ID of Bank.</param>
		/// <returns>Returns true if duplicate.</returns>
		public bool isDublicate(String name, int id) {
			bool b = false;
			try {
				Bank bank = new Bank();
				bank.Name = name;
				if(get(bank).Count > 0) {
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		/// <summary>
		/// Ads an Bank in AddBank Window.
		/// </summary>
		/// <returns>Returns true if added.</returns>
		internal bool addbankPopup() {
			bool b = false;
			try {
				if(addBank.textBox_name.IsNull()) {
					addBank.textBox_name.ErrorMode(true);
				} else if(isDublicate(addBank.textBox_name.TrimedText, 0)) {
					addBank.textBox_name.ErrorMode(true);
					ShowMessage.error(Common.Messages.Error.Error007);
				} else {
					Bank bank = new Bank();
					bank.Name = addBank.textBox_name.TrimedText;
					CommonMethods.setCDMDForAdd(bank);
					addBank.AddedId = add(bank);
					ShowMessage.success(Common.Messages.Success.Success002);
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}
	}
}
