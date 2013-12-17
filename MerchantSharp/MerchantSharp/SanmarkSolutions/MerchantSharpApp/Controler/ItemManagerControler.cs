using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class ItemManagerControler {

		private ItemManager itemManager;
		private ItemManagerImpl itemManagerImpl = null;

		public ItemManagerControler(ItemManager itemManager) {
			this.itemManager = itemManager;
			itemManagerImpl = new ItemManagerImpl(itemManager);
		}

		internal void filter() {
			try {
				itemManagerImpl.filter();
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				itemManagerImpl.setRowsCount();
			} catch(Exception) {
			}
		}

		internal void UserControl_Loaded() {
			try {
				if(!itemManager.IsLoadedUI) {
					itemManagerImpl.UserControl_Loaded();
					itemManager.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void checkBox_sip_sellingDetails_Click() {
			try {
				itemManagerImpl.checkBox_sip_sellingDetails_Click();
			} catch(Exception) {
			}
		}

		internal void button_save_addItem_Click() {
			try {
				if(!itemManager.IsUpdateMode && itemManagerImpl.addItem()) {
					ShowMessage.success(Common.Messages.Success.Success004);
					itemManagerImpl.resetAddForm();
					itemManagerImpl.setRowsCount();
				} else if(itemManagerImpl.updateItem()) {
					ShowMessage.success(Common.Messages.Success.Success004);
					itemManagerImpl.switchToAddMode();
					itemManagerImpl.setRowsCount();
				}
			} catch(Exception) {
			}
		}

		internal void dataGrid_items_items_MouseDoubleClick() {
			try {
				if(itemManager.dataGrid_items_items.SelectedItemID > 0) {
					itemManagerImpl.switchToUpdateMode();
				}
			} catch(Exception) {
			}
		}

		internal void listBoxCatComChanged(){
			try {
				if(Convert.ToInt32(itemManager.listBox_category_basicDetails.SelectedValue) > 0 && Convert.ToInt32(itemManager.listBox_company_basicDetails.SelectedValue) > 0) {
					itemManager.textBox_itemCode_basicDetails.Text = itemManagerImpl.getNextCode(Convert.ToInt32(itemManager.listBox_category_basicDetails.SelectedValue), Convert.ToInt32(itemManager.listBox_company_basicDetails.SelectedValue)).ToString();
				}
			} catch(Exception) {
			}
		}

		internal void button_reset_addItem_Click() {
			try {
				itemManagerImpl.switchToAddMode();
			} catch(Exception) {
			}
		}
	}
}
