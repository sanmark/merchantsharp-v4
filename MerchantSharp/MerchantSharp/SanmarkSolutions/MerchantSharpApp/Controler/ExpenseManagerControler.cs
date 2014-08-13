using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class ExpenseManagerControler {

		private ExpenseManager expenseManager;
		private ExpenseManagerImpl expenseManagerImpl = null;

		public ExpenseManagerControler(ExpenseManager expenseManager) {
			this.expenseManager = expenseManager;
			expenseManagerImpl = new ExpenseManagerImpl(expenseManager);
		}

		internal void UserControl_Loaded() {
			try {
				if(!expenseManager.IsLoadedUI) {
					expenseManagerImpl.UserControl_Loaded();
					expenseManager.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void filter() {
			try {
				expenseManagerImpl.filter();
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				expenseManagerImpl.setRowsCount();
			} catch(Exception) {
			}
		}

		internal void button_save_addExpense_Click() {
			try {
				if(!expenseManager.IsUpdateMode) {
					if(expenseManagerImpl.addExpense()) {
						ShowMessage.success(Common.Messages.Success.Success002);
						expenseManagerImpl.resetAddForm();
						setRowsCount();
					}
				} else {
					if(expenseManagerImpl.updateExpense()) {
						ShowMessage.success(Common.Messages.Success.Success004);
						expenseManagerImpl.switchToAddMode();
						expenseManagerImpl.resetAddForm();
						setRowsCount();
					}
				}
			} catch(Exception) {
			}
		}

		internal void button_reset_addExpense_Click() {
			try {
				expenseManagerImpl.switchToAddMode();
				expenseManagerImpl.resetAddForm();
			} catch(Exception) {
			}
		}

		internal void dataGrid_MouseDoubleClick() {
			try {
				if(expenseManager.dataGrid.SelectedItemID > 0) {
					expenseManagerImpl.switchToUpdateMode();
				}
			} catch(Exception) {
			}
		}

		internal void button_print_Click() {
			expenseManagerImpl.print();
		}
	}
}
