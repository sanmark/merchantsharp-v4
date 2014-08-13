using CustomControls.SanmarkSolutions.WPFCustomControls.MSComboBox;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class UnitManagerControler {

		private AddUnit addUnit;
		private MSComboBox mSComboBox;

		private UnitManagerImpl unitManagerImpl = null;
		private UnitManager unitManager;

		public UnitManagerControler(AddUnit addUnit, MSComboBox mSComboBox) {
			this.addUnit = addUnit;
			this.mSComboBox = mSComboBox;
			unitManagerImpl = new UnitManagerImpl(addUnit);
		}

		public UnitManagerControler(UnitManager unitManager) {
			this.unitManager = unitManager;
			unitManagerImpl = new UnitManagerImpl(unitManager);
		}

		internal void addUnit_addUnit() {
			if(unitManagerImpl.addUnitPopup()) {
				UIComboBox.unitsForAddItem(mSComboBox);
				mSComboBox.SelectedValue = addUnit.AddedId;
				addUnit.Hide();
				addUnit.resetForm();
			}
		}

		internal void UserControl_Loaded() {
			try {
				if(!unitManager.IsLoadedUI) {
					unitManagerImpl.UserControl_Loaded();
					unitManager.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void filter() {
			try {
				unitManagerImpl.filter();
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				unitManagerImpl.setRowsCount();
			} catch(Exception) {
			}
		}

		internal void button_save_addUnit_Click() {
			try {
				if(!unitManager.IsUpdateMode) {
					if(unitManagerImpl.addNewUnit()) {
						ShowMessage.success(Common.Messages.Success.Success002);
						unitManagerImpl.resetAddForm();
						setRowsCount();
					}
				} else {
					if(unitManagerImpl.updateUnit()) {
						ShowMessage.success(Common.Messages.Success.Success004);
						unitManagerImpl.switchToAddMode();
						unitManagerImpl.resetAddForm();
						setRowsCount();
					}
				}
			} catch(Exception) {
			}
		}

		internal void button_reset_addUnit_Click() {
			try {
				unitManagerImpl.switchToAddMode();
				unitManagerImpl.resetAddForm();
			} catch(Exception) {
			}
		}

		internal void dataGrid_MouseDoubleClick() {
			try {
				if(unitManager.dataGrid.SelectedItemID > 0) {
					unitManagerImpl.switchToUpdateMode();
				}
			} catch(Exception) {
			}
		}
	}
}
