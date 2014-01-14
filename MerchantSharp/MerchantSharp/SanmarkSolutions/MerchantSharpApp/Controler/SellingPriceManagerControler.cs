using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class SellingPriceManagerControler {

		private AddSellingPrice addSellingPrice;

		private SellingPriceManagerImpl sellingPriceManagerImpl = null;
		private SellingPriceManager sellingPriceManager;

		public SellingPriceManagerControler() {
		}

		public SellingPriceManagerControler(AddSellingPrice addSellingPrice) {
			this.addSellingPrice = addSellingPrice;
			sellingPriceManagerImpl = new SellingPriceManagerImpl(addSellingPrice);
		}

		public SellingPriceManagerControler(SellingPriceManager sellingPriceManager) {
			this.sellingPriceManager = sellingPriceManager;
			sellingPriceManagerImpl = new SellingPriceManagerImpl(sellingPriceManager);
		}

		internal void button_addPrice_Click() {
			try {
				if(sellingPriceManagerImpl.addPriceFromAddSellingPrice()) {
					addSellingPrice.Hide();
					addSellingPrice.textBox_price.Clear();
				}
			} catch(Exception) {
			}
		}

		internal void UserControl_Loaded() {
			try {
				if(!sellingPriceManager.IsLoadedUI) {
					sellingPriceManagerImpl.UserControl_Loaded();
					sellingPriceManager.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void textBox_selectedItemId_TextChanged() {
			try {
				if(!sellingPriceManager.textBox_selectedItemId.IsNull()) {
					sellingPriceManagerImpl.loadPricesForItemId();
				}
			} catch(Exception) {
			}
		}

		internal void button_addUnitPrice_Click() {
			try {
				if(sellingPriceManager.IsUnitUpdateMode) {
					if(sellingPriceManagerImpl.updateUnitPrice()) {
						sellingPriceManagerImpl.loadPricesForItemId();
						sellingPriceManagerImpl.resetUnitForm();
						sellingPriceManagerImpl.switchToAddMode("u");
						ShowMessage.success(Common.Messages.Success.Success004);
					}
				} else {
					if(sellingPriceManagerImpl.addUnitPrice()) {
						sellingPriceManagerImpl.loadPricesForItemId();
						sellingPriceManagerImpl.resetUnitForm();
						ShowMessage.success(Common.Messages.Success.Success002);
					}
				}
			} catch(Exception) {
			}
		}

		internal void dataGrid_unitPrice_MouseDoubleClick() {
			try {
				if(sellingPriceManager.dataGrid_unitPrice.SelectedItemID > 0) {
					sellingPriceManagerImpl.switchToUpdateMode("u");
				}
			} catch(Exception) {
			}
		}

		internal void dataGrid_packPrice_MouseDoubleClick() {
			try {
				if(sellingPriceManager.dataGrid_packPrice.SelectedItemID > 0) {
					sellingPriceManagerImpl.switchToUpdateMode("p");
				}
			} catch(Exception) {
			}
		}

		internal void button_addPackPrice_Click() {
			try {
				if(sellingPriceManager.IsPackUpdateMode) {
					if(sellingPriceManagerImpl.updatePackPrice()) {
						sellingPriceManagerImpl.loadPricesForItemId();
						sellingPriceManagerImpl.resetPackForm();
						sellingPriceManagerImpl.switchToAddMode("p");
						ShowMessage.success(Common.Messages.Success.Success004);
					}
				} else {
					if(sellingPriceManagerImpl.addPackPrice()) {
						sellingPriceManagerImpl.loadPricesForItemId();
						sellingPriceManagerImpl.resetPackForm();
						ShowMessage.success(Common.Messages.Success.Success002);
					}
				}
			} catch(Exception) {
			}
		}

		internal void button_deletePackPrice_Click() {
			try {
				if(sellingPriceManager.dataGrid_packPrice.SelectedItemID > 0 && ShowMessage.confirm(Common.Messages.Information.Info013) == System.Windows.MessageBoxResult.Yes) {
					sellingPriceManagerImpl.deletePrice("p");
					sellingPriceManagerImpl.loadPricesForItemId();
					ShowMessage.success(Common.Messages.Success.Success003);
				}
			} catch(Exception) {
			}
		}

		internal void button_deleteUnitPrice_Click() {
			try {
				if(sellingPriceManager.dataGrid_unitPrice.SelectedItemID > 0 && ShowMessage.confirm(Common.Messages.Information.Info013) == System.Windows.MessageBoxResult.Yes) {
					sellingPriceManagerImpl.deletePrice("u");
					sellingPriceManagerImpl.loadPricesForItemId();
					ShowMessage.success(Common.Messages.Success.Success003);
				}
			} catch(Exception) {
			}
		}
	}
}
