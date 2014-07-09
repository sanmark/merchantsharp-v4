using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class SellingItemHistoryControler {

		private SellingItemHistory sellingItemHistory;
		private SellingInvoiceManagerImpl sellingInvoiceManagerImpl = null;

		public SellingItemHistoryControler(SellingItemHistory sellingItemHistory) {			
			this.sellingItemHistory = sellingItemHistory;
			sellingInvoiceManagerImpl = new SellingInvoiceManagerImpl(sellingItemHistory);
		}

		internal void filter() {
			sellingInvoiceManagerImpl.filterItems();
		}

		internal void setRowsCount() {
			sellingInvoiceManagerImpl.setItemsRowsCount();
		}

		internal void UserControl_Loaded() {
			try {
				if(!sellingItemHistory.IsLoadedUI) {
					sellingInvoiceManagerImpl.sellingItemHistoryLoaded();
					sellingItemHistory.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

        internal void button_print_Click() {
            try {
                sellingInvoiceManagerImpl.printItems();
            } catch (Exception) {
            }
        }
    }
}
