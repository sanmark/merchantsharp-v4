using CustomControls.SanmarkSolutions.WPFCustomControls.MSComboBox;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.StakeHolders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class BankManagerControler {

		private AddBank addBank;
		private MSComboBox mSComboBox;

		private BankManagerImpl bankManagerImpl = null;
		private   BankManager bankManager;

		public BankManagerControler(AddBank addBank, MSComboBox mSComboBox) {
			// TODO: Complete member initialization
			this.addBank = addBank;
			this.mSComboBox = mSComboBox;
			bankManagerImpl = new BankManagerImpl(addBank);
		}

		public BankManagerControler(BankManager bankManager) {
			this.bankManager = bankManager;
			bankManagerImpl = new BankManagerImpl(bankManager);
		}


		internal void addBank_addBank() {
			if(bankManagerImpl.addbankPopup()) {
				UIComboBox.banksForSelect(mSComboBox);
				mSComboBox.SelectedValue = addBank.AddedId;
				addBank.Hide();
				addBank.resetForm();
			}
		}

		internal void filter() {
			try {
				bankManagerImpl.filter();
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				bankManagerImpl.setRowsCount();
			} catch(Exception) {
			}
		}

		internal void UserControl_Loaded() {
			try {
				if(!bankManager.IsLoadedUI) {
					bankManagerImpl.UserControl_Loaded();
					bankManager.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void button_save_add_Click() {
			try {
				if(!bankManager.IsUpdateMode) {
					if(bankManagerImpl.addNewBank()) {
						ShowMessage.success(Common.Messages.Success.Success002);
						bankManagerImpl.resetAddForm();
						setRowsCount();
					}
				} else {
					if(bankManagerImpl.updateBank()) {
						ShowMessage.success(Common.Messages.Success.Success004);
						bankManagerImpl.switchToAddMode();
						bankManagerImpl.resetAddForm();
						setRowsCount();
					}
				}
			} catch(Exception) {
			}
		}

		internal void button_reset_add_Click() {
			try {
				bankManagerImpl.switchToAddMode();
				bankManagerImpl.resetAddForm();
			} catch(Exception) {
			}
		}

		internal void button_filter_Click() {
			setRowsCount();
		}

		internal void dataGrid_MouseDoubleClick() {
			try {
				if(bankManager.dataGrid.SelectedItemID > 0) {
					bankManagerImpl.switchToUpdateMode();
				}
			} catch(Exception) {
			}
		}
	}
}
