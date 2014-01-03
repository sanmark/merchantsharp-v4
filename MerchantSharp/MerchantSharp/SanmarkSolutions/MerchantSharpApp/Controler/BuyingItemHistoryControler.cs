using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class BuyingItemHistoryControler {

		private BuyingItemHistory buyingItemHistory;
		private BuyingInvoiceManagerImpl buyingInvoiceManagerImpl = null;

		public BuyingItemHistoryControler(BuyingItemHistory buyingItemHistory) {			
			this.buyingItemHistory = buyingItemHistory;
			buyingInvoiceManagerImpl = new BuyingInvoiceManagerImpl(buyingItemHistory);
		}

		internal void filter() {
			buyingInvoiceManagerImpl.filterItems();
		}

		internal void setRowsCount() {
			buyingInvoiceManagerImpl.setItemsRowsCount();
		}

		internal void UserControl_Loaded() {
			try {
				if(!buyingItemHistory.IsLoadedUI) {
					buyingInvoiceManagerImpl.buyingItemHistoryLoaded();
					buyingItemHistory.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}
	}
}
