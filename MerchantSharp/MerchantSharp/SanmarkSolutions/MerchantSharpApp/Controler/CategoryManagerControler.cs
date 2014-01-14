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
	class CategoryManagerControler {

		private AddCategory addCategory;
		private MSComboBox mSComboBox;

		private CategoryManagerImpl categoryManagerImpl = null;
		private CategoryManager categoryManager;

		public CategoryManagerControler(AddCategory addCategory, MSComboBox mSComboBox) {
			this.addCategory = addCategory;
			this.mSComboBox = mSComboBox;
			categoryManagerImpl = new CategoryManagerImpl(addCategory);
		}

		public CategoryManagerControler(CategoryManager categoryManager) {
			this.categoryManager = categoryManager;
			categoryManagerImpl = new CategoryManagerImpl(categoryManager);
		}

		internal void addCategory_addCategory() {
			if(categoryManagerImpl.addCategoryPopup()) {
				//UIComboBox.unitsForAddItem(mSComboBox);
				//mSComboBox.SelectedValue = addCategory.AddedId;
				addCategory.Hide();
				addCategory.resetForm();
			}
		}

		
		internal void UserControl_Loaded() {
			try {
				if(!categoryManager.IsLoadedUI) {
					categoryManagerImpl.UserControl_Loaded();
					categoryManager.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void filter() {
			try {
				categoryManagerImpl.filter();
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				categoryManagerImpl.setRowsCount();
			} catch(Exception) {
			}
		}

		internal void button_save_addCategory_Click() {
			try {
				if(!categoryManager.IsUpdateMode) {
					if(categoryManagerImpl.addNewCategory()) {
						ShowMessage.success(Common.Messages.Success.Success002);
						categoryManagerImpl.resetAddForm();
						setRowsCount();
					}
				} else {
					if(categoryManagerImpl.updateCategory()) {
						ShowMessage.success(Common.Messages.Success.Success004);
						categoryManagerImpl.switchToAddMode();
						categoryManagerImpl.resetAddForm();
						setRowsCount();
					}
				}
			} catch(Exception) {
			}
		}

		internal void button_reset_addCategory_Click() {
			try {
				categoryManagerImpl.switchToAddMode();
				categoryManagerImpl.resetAddForm();
			} catch(Exception) {
			}
		}

		internal void dataGrid_MouseDoubleClick() {
			try {
				if(categoryManager.dataGrid.SelectedItemID > 0) {
					categoryManagerImpl.switchToUpdateMode();
				}
			} catch(Exception) {
			}
		}
	}
}
