using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class SellingInvoiceManagerControler {

		private AddSellingInvoice addSellingInvoice = null;

		private SellingInvoiceManagerImpl sellingInvoiceManagerImpl = null;

		public SellingInvoiceManagerControler(AddSellingInvoice addSellingInvoice) {
			this.addSellingInvoice = addSellingInvoice;
			sellingInvoiceManagerImpl = new SellingInvoiceManagerImpl(addSellingInvoice);
		}

		internal void UserControl_Loaded() {
			try {
				if(!addSellingInvoice.IsLoadedUI) {
					sellingInvoiceManagerImpl.addSellingInvoiceLoaded();
					addSellingInvoice.IsLoadedUI = true;
				} else {
					addSellingInvoice.comboBox_customer_basicDetails.SelectedIndex = 0;
					addSellingInvoice.comboBox_reason_selectItem.SelectedIndex = 0;
				}
				addSellingInvoice.comboBox_stockId_selectItem.SelectedValue = Convert.ToInt32(Session.Preference["defaultSellingStock"]);
			} catch(Exception) {
			}
		}

		internal void textBox_itemId_selectItem_TextChanged() {
			try {
				if(!addSellingInvoice.textBox_itemId_selectItem.IsNull()) {
					sellingInvoiceManagerImpl.selectItemById();
					addSellingInvoice.textBox_itemId_selectItem.Text = null;
				}
			} catch(Exception) {
			}
		}

		internal void textBox_code_selectItem_KeyDown() {
			try {
				if(!addSellingInvoice.textBox_code_selectItem.IsNull()) {
					sellingInvoiceManagerImpl.selectItemByCode();
				}
			} catch(Exception) {
			}
		}

		internal void sellingModeRadioButtonClicked() {
			try {
				sellingInvoiceManagerImpl.calculateLineTotal();
				sellingInvoiceManagerImpl.changePriceLabelName();
				sellingInvoiceManagerImpl.loadSellingPrices();
				sellingInvoiceManagerImpl.loadDiscounts();
				sellingInvoiceManagerImpl.setDiscountForQuantity();
				sellingInvoiceManagerImpl.showCurrentItemOnVFD();
			} catch(Exception) {
			}
		}

		internal void sellingPriceChanged() {
			try {
				sellingInvoiceManagerImpl.calculateLineTotal();
			} catch(Exception) {
			}
		}

		internal void textBox_discount_selectItem_TextChanged() {
			try {
				sellingInvoiceManagerImpl.calculateLineTotal();
			} catch(Exception) {
			}
		}

		internal void textBox_sellingQuantity_selectItem_TextChanged() {
			try {
				sellingInvoiceManagerImpl.setDiscountForQuantity();
				sellingInvoiceManagerImpl.calculateLineTotal();
			} catch(Exception) {
			}
		}

		internal void button_add_selectItem_Click() {
			try {
				if ( addSellingInvoice.SellingInvoice == null || addSellingInvoice.SellingInvoice.Status != 1 ) {
					if ( addSellingInvoice.IsItemUpdateMode ) {
						sellingInvoiceManagerImpl.updItemInDataGrid();
					} else {
						if ( addSellingInvoice.SellingInvoice != null || ( addSellingInvoice.SellingInvoice == null && sellingInvoiceManagerImpl.saveSellingInvoice(3) ) ) {
							sellingInvoiceManagerImpl.addItemToDataGrid();
						}
					}
				}
			} catch(Exception) {
			}
		}

		internal void dataGrid_selectedItems_selectedItems_MouseDoubleClick() {
			try {
				if ( addSellingInvoice.dataGrid_selectedItems_selectedItems.SelectedIndex > -1 && ( addSellingInvoice.SellingInvoice == null || addSellingInvoice.SellingInvoice.Status != 1 ) ) {
					sellingInvoiceManagerImpl.populateUpdateItemForm();
				}
			} catch(Exception) {
			}
		}

		internal void UserControl_KeyDown(System.Windows.Input.KeyEventArgs e) {
			try {
				if(e.Key == System.Windows.Input.Key.F3) {
					addSellingInvoice.textBox_item_selectItem.Focus();
				} else if(e.Key == System.Windows.Input.Key.F4) {
					addSellingInvoice.textBox_code_selectItem.Focus();
				} else if ( e.Key == System.Windows.Input.Key.F6 ) {
					addSellingInvoice.comboBox_sellingPrice_selectItem.Focus();
				} else if ( e.Key == System.Windows.Input.Key.F7 ) {
					addSellingInvoice.textBox_discount_selectItem.Focus();
				} else if ( e.Key == System.Windows.Input.Key.F8 ) {
					addSellingInvoice.textBox_sellingQuantity_selectItem.Focus();
				} else if ( e.Key == System.Windows.Input.Key.F9 ) {
					addSellingInvoice.textBox_cash_selectedItems.Focus();
				} else if(e.Key == System.Windows.Input.Key.F12) {
					button_saveAndPrint_Click();
				} else if(e.Key == System.Windows.Input.Key.F11) {
					button_saveInvoice_Click();
				} 
			} catch(Exception) {
			}
		}

		internal void checkBox_quickPay_selectedItems_Click() {
			try {
				sellingInvoiceManagerImpl.checkBox_quickPay_selectedItems_Click();
			} catch(Exception) {
			}
		}

		internal void button_saveAndPrint_Click() {
			try {
				if(sellingInvoiceManagerImpl.saveSellingInvoice(1)) {
					ShowMessage.success(Common.Messages.Success.Success004);
					sellingInvoiceManagerImpl.printBill(true, addSellingInvoice.SellingInvoice);
					button_resetUI_Click();
				}				
			} catch(Exception) {
			}
		}

		internal void button_saveInvoice_Click() {
			try {
				if(sellingInvoiceManagerImpl.saveSellingInvoice(1)) {
					ShowMessage.success(Common.Messages.Success.Success004);
					button_resetUI_Click();
				}
			} catch(Exception) {
			}
		}

		internal void textBox_cash_selectedItems_TextChanged() {
			try {
				sellingInvoiceManagerImpl.calculateBalance();
			} catch(Exception) {
			}
		}

		internal void button_resetUI_Click() {
			try {
				sellingInvoiceManagerImpl.resetSellingInvoiceUI();
				addSellingInvoice.PaymentSection.resetAllElements();
			} catch(Exception) {
			}
		}

		internal void textBox_discount_selectedItems_TextChanged() {
			try {
				sellingInvoiceManagerImpl.calculateNetTotal();
			} catch(Exception) {
			}
		}

		internal void comboBox_stockId_selectItem_SelectionChanged() {
			try {
				sellingInvoiceManagerImpl.loadAvailableQuantity();
			} catch(Exception) {
			}
		}

		internal void removeSelectedItem() {
			try {
				if(addSellingInvoice.SellingInvoice.Status == 3) {
					sellingInvoiceManagerImpl.removeSelectedItem();
				}
			} catch(Exception) {
			}
		}

		internal void returnBoxTextChanged() {
			try {
				sellingInvoiceManagerImpl.calculateLineTotal();
			} catch(Exception) {
			}
		}

		internal void checkBox_discountActivated_Click() {
			try {
				sellingInvoiceManagerImpl.enableOrDisableDiscount();
			} catch ( Exception ) {
			}
		}

		internal void comboBox_reason_selectItem_SelectionChanged() {
			try {
				if ( Session.Meta["isActiveCompanyReturnManager"] == 1 ) {
					if ( Convert.ToInt32(addSellingInvoice.comboBox_reason_selectItem.SelectedValue) == 2 ) {
						addSellingInvoice.comboBox_stockId_selectItem.SelectedValue = Convert.ToInt32(Session.Preference["defaultCompanyReturnStock"]);
					} else {
						addSellingInvoice.comboBox_stockId_selectItem.SelectedValue = Convert.ToInt32(Session.Preference["defaultSellingStock"]);
					}
				}
			} catch ( Exception ) {
			}
		}

		internal void button_quatationPrint_Click() {
			try {
				sellingInvoiceManagerImpl.button_quatationPrint_Click();
			} catch ( Exception ) {
			}
		}

        internal void comboBox_vendor_basicDetails_SelectionChanged() {
            try {
                sellingInvoiceManagerImpl.loadAccountValueInPaymentSection();
            } catch (Exception) {
            }
        }
    }
}
