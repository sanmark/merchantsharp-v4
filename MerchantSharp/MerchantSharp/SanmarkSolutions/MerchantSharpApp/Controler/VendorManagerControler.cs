using CustomControls.SanmarkSolutions.WPFCustomControls.MSComboBox;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.StakeHolders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class VendorManagerControler {

		private AddVendor addVendor = null;
		private MSComboBox mSComboBox;

		private VendorManagerImpl vendorManagerImpl = null;
		private VendorManager vendorManager;

		public VendorManagerControler(AddVendor addVendor, MSComboBox mSComboBox) {
			this.addVendor = addVendor;
			this.mSComboBox = mSComboBox;
			vendorManagerImpl = new VendorManagerImpl(addVendor);
		}

		public VendorManagerControler(VendorManager vendorManager) {
			this.vendorManager = vendorManager;
			vendorManagerImpl = new VendorManagerImpl(vendorManager);
		}

		internal void addVendor_addVendor() {
			if(vendorManagerImpl.addVendorPopup()) {
				UIComboBox.vendorsForAddBuyingInvoice(mSComboBox);
				mSComboBox.SelectedValue = addVendor.AddedId;
				addVendor.Hide();
				addVendor.resetForm();
			}
		}

		internal void UserControl_Loaded() {
			try {
				if(!vendorManager.IsLoadedUI) {
					vendorManagerImpl.UserControl_Loaded();
					vendorManager.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void filter() {
			try {
				vendorManagerImpl.filter();
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				vendorManagerImpl.setRowsCount();
			} catch(Exception) {
			}
		}

		internal void button_save_addVendor_Click() {
			try {
				if(!vendorManager.IsUpdateMode) {
					if(vendorManagerImpl.addNewVendor()) {
						ShowMessage.success(Common.Messages.Success.Success002);
						vendorManagerImpl.resetAddForm();
						setRowsCount();
					}
				} else {
					if(vendorManagerImpl.updateVendor()) {
						ShowMessage.success(Common.Messages.Success.Success004);
						vendorManagerImpl.switchToAddMode();
						vendorManagerImpl.resetAddForm();
						setRowsCount();
					}
				}
			} catch(Exception) {
			}
		}

		internal void button_reset_addVendor_Click() {
			try {
				vendorManagerImpl.switchToAddMode();
				vendorManagerImpl.resetAddForm();
			} catch(Exception) {
			}
		}

		internal void dataGrid_MouseDoubleClick() {
			try {
				if(vendorManager.dataGrid.SelectedItemID > 0) {
					vendorManagerImpl.switchToUpdateMode();
				}
			} catch(Exception) {
			}
		}
	}
}
