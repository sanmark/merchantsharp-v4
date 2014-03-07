using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class PaymentManagerControler {
		private PaymentSection paymentSection;
		private PaymentManagerImpl paymentManagerImpl;
		private AddSellingInvoicePayment addSellingInvoicePayment;

		public PaymentManagerControler(PaymentSection paymentSection) {
			this.paymentSection = paymentSection;
			paymentManagerImpl = new PaymentManagerImpl(paymentSection);
		}

		public PaymentManagerControler(AddSellingInvoicePayment addSellingInvoicePayment) {
			this.addSellingInvoicePayment = addSellingInvoicePayment;
			paymentManagerImpl = new PaymentManagerImpl(addSellingInvoicePayment);
		}

		public void activeElements() {
			try {
				if(paymentSection.InvoiceId > 0) {
					paymentSection.mainGrid.IsEnabled = true;
					paymentManagerImpl.loadedUI();
				} else {
					paymentSection.mainGrid.IsEnabled = false;
					paymentManagerImpl.unloadAllItems();
				}
			} catch(Exception) {
			}
		}

		internal void button_add_cashPayments_Click() {
			try {
				if(paymentManagerImpl.addCashPayment()) {
					paymentManagerImpl.loadAllCashPayments();
					paymentManagerImpl.clearCashSection();
					ShowMessage.success(Common.Messages.Success.Success004);
				}
			} catch(Exception) {
			}
		}

		internal void UserControl_Loaded() {
			if(!paymentSection.IsLoadedUI) {
				paymentManagerImpl.UserControl_Loaded();
				paymentSection.IsLoadedUI = true;
				activeElements();
			}
		}

		internal void button_delete_cashPayments_Click() {
			try {
				if(ShowMessage.confirm(Common.Messages.Information.Info013) == System.Windows.MessageBoxResult.Yes) {
					if(paymentSection.dataGrid_cashPayments_cashPayments.SelectedItemID > 0) {
						paymentManagerImpl.deleteCashPayment();
						paymentManagerImpl.loadAllCashPayments();
						ShowMessage.success(Common.Messages.Success.Success003);
					} else {
						ShowMessage.error(Common.Messages.Error.Error009);
					}
				}
			} catch(Exception) {
			}
		}

		internal void button_add_chequePayments_Click() {
			try {
				if(paymentManagerImpl.addChequePayment()) {
					paymentManagerImpl.loadAllChequePayments();
					paymentManagerImpl.clearChequeSection();
					ShowMessage.success(Common.Messages.Success.Success004);
				}
			} catch(Exception) {
			}
		}

		internal void button_delete_chequePayments_Click() {
			try {
				if(ShowMessage.confirm(Common.Messages.Information.Info013) == System.Windows.MessageBoxResult.Yes) {
					if(paymentSection.dataGrid_chequePayments_chequePayments.SelectedItemID > 0) {
						paymentManagerImpl.deleteChequePayment();
						paymentManagerImpl.loadAllChequePayments();
						ShowMessage.success(Common.Messages.Success.Success004);
					} else {
						ShowMessage.error(Common.Messages.Error.Error009);
					}
				}
			} catch(Exception) {
			}
		}

		internal void button_saveAccountChange_Click() {
			try {
				paymentManagerImpl.saveAccountChange();
			} catch(Exception) {
			}
		}

		internal void button_add_otherPayments_Click() {
			try {
				if(paymentManagerImpl.addOtherPayment()) {
					paymentManagerImpl.loadAllOtherPayments();
					paymentManagerImpl.clearOtherSection();
					ShowMessage.success(Common.Messages.Success.Success004);
				}
			} catch(Exception) {
			}
		}

		internal void button_delete_otherPayments_Click() {
			try {
				if(ShowMessage.confirm(Common.Messages.Information.Info013) == System.Windows.MessageBoxResult.Yes) {
					if(paymentSection.dataGrid_otherPayments_otherPayments.SelectedItemID > 0) {
						paymentManagerImpl.deleteOtherPayment();
						paymentManagerImpl.loadAllOtherPayments();
						ShowMessage.success(Common.Messages.Success.Success004);
					} else {
						ShowMessage.error(Common.Messages.Error.Error009);
					}
				}
			} catch(Exception) {
			}
		}

		internal void resetAllElements() {
			try {
				paymentManagerImpl.resetAllElements();
			} catch(Exception) {
			}
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////

		internal void addSellingInvoicePaymentUserControlLoaded() {
			try {
				if(!addSellingInvoicePayment.IsLoadedUI) {
					paymentManagerImpl.addSellingInvoicePaymentUserControlLoaded();
					addSellingInvoicePayment.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void filterSellingInvoice() {
			try {
				paymentManagerImpl.filterSellingInvoice();
			} catch(Exception) {
			}
		}

		internal void setRowsCountSellingInvoice() {
			try {
				paymentManagerImpl.setRowsCountSellingInvoice();
			} catch(Exception) {
			}
		}

		internal void sellingInvoicerid_MouseLeftButtonUp() {
			try {
				if(addSellingInvoicePayment.dataGrid_stockItems.SelectedItemID > 0) {
					paymentManagerImpl.sellingInvoicerid_MouseLeftButtonUp();
				}
			} catch(Exception) {
			}
		}

		internal void sellingInvoiceNumber_Enter() {
			try {
				paymentManagerImpl.sellingInvoiceNumber_Enter();
			} catch(Exception) {
			}
		}

		internal void saveSellingInvoice() {
			try {
				if(addSellingInvoicePayment.SelectedInvoice != null) {
					if(ShowMessage.confirm(Common.Messages.Information.Info013) == System.Windows.MessageBoxResult.Yes && paymentManagerImpl.saveSellingInvoice()) {
						ShowMessage.success(Common.Messages.Success.Success004);
					}
				} else {
					addSellingInvoicePayment.textBox_invoiceNumber.ErrorMode(true);
				}
			} catch(Exception) {
			}
		}

		internal void button_printCheque_Click() {
			try {
				paymentManagerImpl.printCheque();
			} catch ( Exception ) {
			}
		}
	}
}
