using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class PaymentManagerControler {
		private PaymentSection paymentSection;
		private PaymentManagerImpl paymentManagerImpl;

		public PaymentManagerControler(PaymentSection paymentSection) {
			this.paymentSection = paymentSection;
			paymentManagerImpl = new PaymentManagerImpl(paymentSection);
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
						ShowMessage.success(Common.Messages.Success.Success004);
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
	}
}
