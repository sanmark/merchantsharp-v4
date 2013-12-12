using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class BuyingInvoiceHistoryControler {

		private BuyingInvoiceHistory buyingInvoiceHistory;
		private BuyingInvoiceManagerImpl buyingInvoiceManagerImpl = null;

		public BuyingInvoiceHistoryControler(BuyingInvoiceHistory buyingInvoiceHistory) {
			this.buyingInvoiceHistory = buyingInvoiceHistory;
			this.buyingInvoiceManagerImpl = new BuyingInvoiceManagerImpl(buyingInvoiceHistory);
		}

		internal void UserControl_Loaded() {
			try {
				if(!buyingInvoiceHistory.IsLoadedUI) {
					buyingInvoiceManagerImpl.buyingInvoiceHistoryLoaded();
					buyingInvoiceHistory.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void filter() {
			try {
				buyingInvoiceManagerImpl.filter();
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				buyingInvoiceManagerImpl.setRowsCount();
			} catch(Exception) {
			}
		}
	}
}
