using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class SellingInvoiceHistoryControler {

		private SellingInvoiceHistory sellingInvoiceHistory;
		private SellingInvoiceManagerImpl sellingInvoiceManagerImpl = null;

		public SellingInvoiceHistoryControler(SellingInvoiceHistory sellingInvoiceHistory) {
			this.sellingInvoiceHistory = sellingInvoiceHistory;
			this.sellingInvoiceManagerImpl = new SellingInvoiceManagerImpl(sellingInvoiceHistory);
		}

		internal void filter() {
			try {
				sellingInvoiceManagerImpl.filter();
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				sellingInvoiceManagerImpl.setRowsCount();
			} catch(Exception) {
			}
		}

		internal void UserControl_Loaded() {
			try {
				if(!sellingInvoiceHistory.IsLoadedUI) {
					sellingInvoiceManagerImpl.sellingInvoiceHistoryLoaded();
					sellingInvoiceHistory.IsLoadedUI = true;
					sellingInvoiceHistory.dataGrid_sellingInvoiceHistory.CanUserSortColumns = true;
				}
			} catch(Exception) {
			}
		}

		internal void dataGrid_sellingInvoiceHistory_MouseDoubleClick() {
			try {
				if(sellingInvoiceHistory.dataGrid_sellingInvoiceHistory.SelectedItemID > 0) {
					sellingInvoiceManagerImpl.dataGrid_sellingInvoiceHistory_MouseDoubleClick();
				}
			} catch(Exception) {
			}
		}

		internal void button_print_filter_Click() {
			try {
				sellingInvoiceManagerImpl.printInvoices();
			} catch ( Exception ) {
			}
		}
	}
}
