using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class OldStockBySellingInvoiceControler {

		private OldStockBySellingInvoice oldStockBySellingInvoice;
		private OldStockManagerImpl oldStockManagerImpl = null;

		public OldStockBySellingInvoiceControler(OldStockBySellingInvoice oldStockBySellingInvoice) {
			this.oldStockBySellingInvoice = oldStockBySellingInvoice;
			oldStockManagerImpl = new OldStockManagerImpl(oldStockBySellingInvoice);
		}

		internal void UserControl_Loaded() {
			try {
				if(!oldStockBySellingInvoice.IsLoadedUI) {
					oldStockManagerImpl.oldStockBySellingInvoiceUserControlLoaded();
					oldStockBySellingInvoice.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void filter() {
			try {
				oldStockManagerImpl.filterSelling();
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				oldStockManagerImpl.setRowsCountSelling();
			} catch(Exception) {
			}
		}
	}
}
