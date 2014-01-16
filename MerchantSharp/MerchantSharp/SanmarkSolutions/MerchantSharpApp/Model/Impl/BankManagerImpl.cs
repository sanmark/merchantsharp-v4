using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.StakeHolders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class BankManagerImpl {

		private IDao dao = null;
		private AddBank addBank;
		private BankManager bankManager;

		public BankManagerImpl() {
			dao = BankDao.getInstance();
		}

		public BankManagerImpl(AddBank addBank) {
			this.addBank = addBank;
			dao = BankDao.getInstance();
		}

		public BankManagerImpl(BankManager bankManager) {
			this.bankManager = bankManager;
			dao = BankDao.getInstance();
		}

		/// <summary>
		/// Adds a new Entity.
		/// </summary>
		/// <param name="entity">Entity</param>
		/// <returns>Id of just Added Entity.</returns>
		public int add(Entity entity) {
			try {
				if(Session.Permission["canAddBank"] == 1) {
					return dao.add(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return 0;			
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
			try {
				if(Session.Permission["canUpdateBank"] == 1) {
					dao.upd(entity);
					return 1;
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return 0;
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
		public bool isDuplicate(String name, int id) {
			bool b = false;
			try {
				Bank bank = new Bank();
				bank.Name = name;
				List<Bank> list = get(bank);
				if(list.Count > 0 && id > 0) {
					foreach(Bank ba in list) {
						if(ba.Id != id) {
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

		public Bank getBankById(int bankId) {
			Bank bank = null;
			try {
				bank = new Bank();
				bank.Id = bankId;
				return get(bank)[0];
			} catch(Exception) {
				return null;
			}
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
				} else if(isDuplicate(addBank.textBox_name.TrimedText, 0)) {
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

		////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////


		internal void UserControl_Loaded() {
			try {
				bankManager.DataTable = new DataTable();
				bankManager.DataTable.Columns.Add("ID", typeof(int));
				bankManager.DataTable.Columns.Add("Name", typeof(String));
				bankManager.dataGrid.DataContext = bankManager.DataTable.DefaultView;
				bankManager.dataGrid.Columns[1].Width = 200;

				bankManager.Pagination = new Pagination();
				bankManager.Pagination.Filter = bankManager;
				bankManager.grid_pagination.Children.Add(bankManager.Pagination);

				setRowsCount();
			} catch(Exception) {
			}
		}

		private Bank getBankForFilter() {
			Bank bank = new Bank();
			try {
				bank.Name = bankManager.textBox_name_filter.TrimedText + "%";
			} catch(Exception) {
			}
			return bank;
		}

		internal void filter() {
			try {
				Bank b = getBankForFilter();
				b.LimitStart = bankManager.Pagination.LimitStart;
				b.LimitEnd = bankManager.Pagination.LimitCount;
				List<Bank> list = get(b);
				bankManager.DataTable.Rows.Clear();
				foreach(Bank bank in list) {
					bankManager.DataTable.Rows.Add(bank.Id, bank.Name);
				}
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				Bank bank = getBankForFilter();
				bank.RowsCount = 1;
				bank.LimitStart = bankManager.Pagination.LimitStart;
				bank.LimitEnd = bankManager.Pagination.LimitCount;
				bankManager.Pagination.RowsCount = get(bank)[0].RowsCount;
			} catch(Exception) {
			}
		}

		internal bool addNewBank() {
			bool b = false;
			try {
				bool isOkay = true;
				if(bankManager.textBox_name_add.IsNull()) {
					bankManager.textBox_name_add.ErrorMode(true);
					isOkay = false;
				}
				if(isDuplicate(bankManager.textBox_name_add.TrimedText, 0)) {
					bankManager.textBox_name_add.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					Bank bank = new Bank();
					bank.Name = bankManager.textBox_name_add.TrimedText;
					CommonMethods.setCDMDForAdd(bank);
					if(add(bank) > 0) {
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void resetAddForm() {
			try {
				bankManager.textBox_name_add.Clear();
			} catch(Exception) {
			}
		}

		internal bool updateBank() {
			bool b = false;
			try {
				bool isOkay = true;
				if(bankManager.textBox_name_add.IsNull()) {
					bankManager.textBox_name_add.ErrorMode(true);
					isOkay = false;
				}
				if(isDuplicate(bankManager.textBox_name_add.TrimedText, bankManager.SelectedBank.Id)) {
					bankManager.textBox_name_add.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					Bank bank = bankManager.SelectedBank;
					bank.Name = bankManager.textBox_name_add.TrimedText;
					CommonMethods.setCDMDForUpdate(bank);
					if(upd(bank) > 0) {
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void switchToAddMode() {
			try {
				bankManager.SelectedBank = null;
				bankManager.IsUpdateMode = false;
				bankManager.button_save_add.Content = "Save";
				bankManager.button_reset_add.Content = "Reset";
			} catch(Exception) {
			}
		}

		internal void switchToUpdateMode() {
			try {
				bankManager.IsUpdateMode = true;
				Bank bank = getBankById(bankManager.dataGrid.SelectedItemID);
				bankManager.SelectedBank = bank;
				bankManager.textBox_name_add.Text = bank.Name;

				bankManager.button_save_add.Content = "Update";
				bankManager.button_reset_add.Content = "Cancel";
			} catch(Exception) {
			}
		}
	}
}
