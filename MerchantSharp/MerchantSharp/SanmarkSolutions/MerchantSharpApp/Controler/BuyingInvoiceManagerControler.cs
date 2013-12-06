using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class BuyingInvoiceManagerControler {

		private BuyingInvoiceManagerImpl buyingInvoiceManagerImpl = null;
		private AddBuyingInvoice addBuyingInvoice = null;

		public BuyingInvoiceManagerControler() {
			buyingInvoiceManagerImpl = new BuyingInvoiceManagerImpl();
		}

		public BuyingInvoiceManagerControler(AddBuyingInvoice addBuyingInvoice) {
			buyingInvoiceManagerImpl = new BuyingInvoiceManagerImpl(addBuyingInvoice);
			this.addBuyingInvoice = addBuyingInvoice;
		}

		internal void UserControl_Loaded() {
			try {
				if(!addBuyingInvoice.IsLoadedUI) {
					buyingInvoiceManagerImpl.addBuyingInvoiceLoaded();
					addBuyingInvoice.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void textBox_code_selectItem_KeyDown() {
			try {
				if(!addBuyingInvoice.textBox_code_selectItem.IsNull()) {
					buyingInvoiceManagerImpl.selectItemByCode();
				}
			} catch(Exception) {
			}
		}

		internal void textBox_itemId_selectItem_TextChanged() {
			try {
				if(!addBuyingInvoice.textBox_itemId_selectItem.IsNull()) {
					buyingInvoiceManagerImpl.selectItemById();
					addBuyingInvoice.textBox_itemId_selectItem.Text = null;
				}
			} catch(Exception) {
			}
		}

		internal void UserControl_KeyDown(System.Windows.Input.KeyEventArgs e) {
			try {
				if(e.Key == System.Windows.Input.Key.F3) {
					addBuyingInvoice.textBox_item_selectItem.Focus();
				} else if(e.Key == System.Windows.Input.Key.F4) {
					addBuyingInvoice.textBox_code_selectItem.Focus();
				} else if(e.Key == System.Windows.Input.Key.F11) {
					button_saveInvoice_Click();
				}
			} catch(Exception) {
			}
		}

		internal void button_add_selectItem_Click() {
			if(addBuyingInvoice.IsItemUpdateMode) {
				buyingInvoiceManagerImpl.updItemInDataGrid();
			} else {
				if(addBuyingInvoice.BuyingInvoice != null || (addBuyingInvoice.BuyingInvoice == null && buyingInvoiceManagerImpl.saveBuyingInvoice(3))) {
					buyingInvoiceManagerImpl.addItemToDataGrid();
				}
			}
		}

		internal void deleteSellingPrice(String mode) {
			if(buyingInvoiceManagerImpl.deleteSellingPrice(mode)) {
				ShowMessage.success(Common.Messages.Success.Success003);
			}
		}

		internal void dataGrid_selectedItems_selectedItems_MouseDoubleClick() {
			try {
				if(addBuyingInvoice.dataGrid_selectedItems_selectedItems.SelectedIndex > -1) {
					buyingInvoiceManagerImpl.populateUpdateItemForm();
				}
			} catch(Exception) {
			}
		}

		internal void calculateLineTotal() {
			try {
				buyingInvoiceManagerImpl.calculateLineTotal();
			} catch(Exception) {
			}
		}

		internal void removeSelectedItem() {
			try {
				if(addBuyingInvoice.dataGrid_selectedItems_selectedItems.SelectedItemID > 0) {
					buyingInvoiceManagerImpl.removeSelectedItem();
				}				
			} catch(Exception) {
			}
		}

		internal void calculateNetTotal() {
			buyingInvoiceManagerImpl.calculateNetTotal();
		}

		internal void button_saveInvoice_Click() {
			try {
				int status = 0;
				if(addBuyingInvoice.checkBox_isRequestOrder_selectedItems.IsChecked == true) {
					status = 2;
				} else {
					status = 1;
				}
				if(buyingInvoiceManagerImpl.saveBuyingInvoice(status)) {
					ShowMessage.success(Common.Messages.Success.Success004);
					//buyingInvoiceManagerImpl.resetBuyingInvoiceUI();
				}
				if(status == 1) {
					addBuyingInvoice.PaymentSection.InvoiceId = addBuyingInvoice.BuyingInvoice.Id;
				}
			} catch(Exception) {
			}
		}

		internal void buyingModeRadioButtonClicked() {
			try {
				buyingInvoiceManagerImpl.calculateLineTotal();
				buyingInvoiceManagerImpl.changePriceLabelName();
			} catch(Exception) {
			}
		}

		internal void comboBox_vendor_basicDetails_SelectionChanged() {
			try {
				buyingInvoiceManagerImpl.loadAccountValueInPaymentSection();
			} catch(Exception) {
			}
		}

		internal void button_resetUI_Click() {
			try {
				buyingInvoiceManagerImpl.resetBuyingInvoiceUI();
				addBuyingInvoice.PaymentSection.resetAllElements();
			} catch(Exception) {
			}
		}

		internal void button_discount_Click() {
			try {
				if(addBuyingInvoice.SelectedItem != null) {
					buyingInvoiceManagerImpl.addDiscountManager();
				} else {
					ShowMessage.error(Common.Messages.Error.Error008);
				}
			} catch(Exception) {
			}
		}
	}
}
