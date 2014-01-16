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
	class CustomerManagerControler {
		private AddCustomer addCustomer;
		private MSComboBox mSComboBox;

		private CustomerManagerImpl customerManagerImpl = null;
		private   CustomerManager customerManager;

		public CustomerManagerControler(AddCustomer addCustomer, MSComboBox mSComboBox) {
			// TODO: Complete member initialization
			this.addCustomer = addCustomer;
			this.mSComboBox = mSComboBox;
			customerManagerImpl = new CustomerManagerImpl(addCustomer);
		}

		public CustomerManagerControler(CustomerManager customerManager) {
			this.customerManager = customerManager;
			customerManagerImpl = new CustomerManagerImpl(customerManager);
		}

		internal void addCustomer_addCustomer() {
			if(customerManagerImpl.addCustomerPopup()) {
				UIComboBox.customersForAddSellingInvoice(mSComboBox);
				mSComboBox.SelectedValue = addCustomer.AddedId;
				addCustomer.Hide();
				addCustomer.resetForm();
			}
		}

		internal void UserControl_Loaded() {
			try {
				if(!customerManager.IsLoadedUI) {
					customerManagerImpl.UserControl_Loaded();
					customerManager.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void filter() {
			try {
				customerManagerImpl.filter();
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				customerManagerImpl.setRowsCount();
			} catch(Exception) {
			}
		}

		internal void button_save_add_Click() {
			try {
				if(!customerManager.IsUpdateMode) {
					if(customerManagerImpl.addNewCustomer()) {
						ShowMessage.success(Common.Messages.Success.Success002);
						customerManagerImpl.resetAddForm();
						setRowsCount();
					}
				} else {
					if(customerManagerImpl.updateCustomer()) {
						ShowMessage.success(Common.Messages.Success.Success004);
						customerManagerImpl.switchToAddMode();
						customerManagerImpl.resetAddForm();
						setRowsCount();
					}
				}
			} catch(Exception) {
			}
		}

		internal void button_reset_add_Click() {
			try {
				customerManagerImpl.switchToAddMode();
				customerManagerImpl.resetAddForm();
			} catch(Exception) {
			}
		}

		internal void dataGrid_MouseDoubleClick() {
			try {
				if(customerManager.dataGrid.SelectedItemID > 0) {
					customerManagerImpl.switchToUpdateMode();
				}
			} catch(Exception) {
			}
		}
	}
}
