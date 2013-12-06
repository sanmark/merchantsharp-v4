using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class DiscountManagerControler {

		private DiscountManager discountManager;

		private DiscountManagerImpl discountManagerImpl = null;

		public DiscountManagerControler(DiscountManager discountManager) {
			this.discountManager = discountManager;
			discountManagerImpl = new DiscountManagerImpl(discountManager);
		}

		internal void loadUI() {
			try {
				discountManagerImpl.loadUI();
			} catch(Exception) {
			}
		}

		internal void textBox_selectedItemId_TextChanged() {
			try {
				if(!discountManager.textBox_selectedItemId.IsNull()) {
					discountManagerImpl.loadDiscountsForItemId();
				}
			} catch(Exception) {
			}
		}

		internal void button_addUnitDiscount_Click() {
			try {
				if(discountManager.IsUnitUpdateMode) {
					if(discountManagerImpl.updateUnitDiscount()) {
						discountManagerImpl.loadDiscountsForItemId();
						discountManagerImpl.resetUnitForm();
						discountManagerImpl.switchToAddMode("u");
						ShowMessage.success(Common.Messages.Success.Success004);
					}
				} else {
					if(discountManagerImpl.addUnitDiscount()) {
						discountManagerImpl.loadDiscountsForItemId();
						discountManagerImpl.resetUnitForm();
						ShowMessage.success(Common.Messages.Success.Success002);
					}
				}
			} catch(Exception) {
			}
		}

		internal void button_addPackDiscount_Click() {
			try {
				if(discountManager.IsPackUpdateMode) {
					if(discountManagerImpl.updatePackDiscount()) {
						discountManagerImpl.loadDiscountsForItemId();
						discountManagerImpl.resetPackForm();
						discountManagerImpl.switchToAddMode("p");
						ShowMessage.success(Common.Messages.Success.Success004);
					}
				} else {
					if(discountManagerImpl.addPackDiscount()) {
						discountManagerImpl.loadDiscountsForItemId();
						discountManagerImpl.resetPackForm();
						ShowMessage.success(Common.Messages.Success.Success002);
					}
				}
			} catch(Exception) {
			}
		}

		internal void dataGrid_unitDiscount_MouseDoubleClick() {
			try {
				if(discountManager.dataGrid_unitDiscount.SelectedItemID > 0) {
					discountManagerImpl.switchToUpdateMode("u");
				}
			} catch(Exception) {
			}
		}

		internal void dataGrid_packDiscount_MouseDoubleClick() {
			try {
				if(discountManager.dataGrid_packDiscount.SelectedItemID > 0) {
					discountManagerImpl.switchToUpdateMode("p");
				}
			} catch(Exception) {
			}
		}

		internal void button_deleteUnitDiscount_Click() {
			try {
				if(discountManager.dataGrid_unitDiscount.SelectedItemID > 0 && ShowMessage.confirm(Common.Messages.Information.Info013) == System.Windows.MessageBoxResult.Yes) {
					discountManagerImpl.deleteDiscount("u");
					discountManagerImpl.loadDiscountsForItemId();
				}
			} catch(Exception) {
			}
		}

		internal void button_deletePackDiscount_Click() {
			try {
				if(discountManager.dataGrid_packDiscount.SelectedItemID > 0 && ShowMessage.confirm(Common.Messages.Information.Info013) == System.Windows.MessageBoxResult.Yes) {
					discountManagerImpl.deleteDiscount("p");
					discountManagerImpl.loadDiscountsForItemId();
				}
			} catch(Exception) {
			}
		}
	}
}
