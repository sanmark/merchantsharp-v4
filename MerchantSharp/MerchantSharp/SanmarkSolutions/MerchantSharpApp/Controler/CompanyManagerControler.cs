using CustomControls.SanmarkSolutions.WPFCustomControls.MSComboBox;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class CompanyManagerControler {

		private AddCompany addCompany;
		private MSComboBox mSComboBox;

		private CompanyManagerImpl companyManagerImpl = null;
		private   CompanyManager companyManager;

		public CompanyManagerControler(AddCompany addCompany, MSComboBox mSComboBox) {
			// TODO: Complete member initialization
			this.addCompany = addCompany;
			this.mSComboBox = mSComboBox;
			companyManagerImpl = new CompanyManagerImpl(addCompany);
		}

		public CompanyManagerControler(CompanyManager companyManager) {
			this.companyManager = companyManager;
			companyManagerImpl = new CompanyManagerImpl(companyManager);
		}

		internal void addCompany_addCompany() {
			if(companyManagerImpl.addCompanyPopup()) {
				//UIComboBox.unitsForAddItem(mSComboBox);
				//mSComboBox.SelectedValue = addCategory.AddedId;
				addCompany.Hide();
				addCompany.resetForm();
			}
		}

		internal void filter() {
			try {
				companyManagerImpl.filter();
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				companyManagerImpl.setRowsCount();
			} catch(Exception) {
			}
		}

		internal void UserControl_Loaded() {
			try {
				if(!companyManager.IsLoadedUI) {
					companyManagerImpl.UserControl_Loaded();
					companyManager.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void button_save_addCompany_Click() {
			try {
				if(!companyManager.IsUpdateMode) {
					if(companyManagerImpl.addNewCompany()) {
						ShowMessage.success(Common.Messages.Success.Success002);
						companyManagerImpl.resetAddForm();
						setRowsCount();
					}
				} else {
					if(companyManagerImpl.updateCompany()) {
						ShowMessage.success(Common.Messages.Success.Success004);
						companyManagerImpl.switchToAddMode();
						companyManagerImpl.resetAddForm();
						setRowsCount();
					}
				}
			} catch(Exception) {
			}
		}

		internal void button_reset_addCompany_Click() {
			try {
				companyManagerImpl.switchToAddMode();
				companyManagerImpl.resetAddForm();
			} catch(Exception) {
			}
		}

		internal void dataGrid_MouseDoubleClick() {
			try {
				if(companyManager.dataGrid.SelectedItemID > 0) {
					companyManagerImpl.switchToUpdateMode();
				}
			} catch(Exception) {
			}
		}
	}
}
