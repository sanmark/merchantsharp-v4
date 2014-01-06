using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class StockTransferControler {

		private AddStockTransfer addStockTransfer;
		private StockTransferManagerImpl stockTransferManagerImpl = null;
		private StockTransferHistory stockTransferHistory;

		public StockTransferControler(AddStockTransfer addStockTransfer) {
			this.addStockTransfer = addStockTransfer;
			stockTransferManagerImpl = new StockTransferManagerImpl(addStockTransfer);
		}

		public StockTransferControler(StockTransferHistory stockTransferHistory) {
			this.stockTransferHistory = stockTransferHistory;
			stockTransferManagerImpl = new StockTransferManagerImpl(stockTransferHistory);
		}

		internal void addStockTransferLoaded() {
			try {
				if(!addStockTransfer.IsLoadedUI) {
					stockTransferManagerImpl.addStockTransferLoaded();
					addStockTransfer.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void textBox_itemId_TextChanged() {
			try {
				if(!String.IsNullOrWhiteSpace(addStockTransfer.textBox_itemId.Text)) {
					stockTransferManagerImpl.populateAddItem();
				}
			} catch(Exception) {
			}
		}

		internal void transferModeCheked() {
			try {
				stockTransferManagerImpl.loadAvailableQuantitiesForMode();
			} catch(Exception) {
			}
		}

		internal void Button_Click() {
			try {
				stockTransferManagerImpl.addItemToTable();
			} catch(Exception) {
			}
		}

		internal void removeSelectedItem() {
			try {
				if(addStockTransfer.dataGrid.SelectedItemID > 0) {
					stockTransferManagerImpl.removeSelectedItem();
				}
			} catch(Exception) {
			}
		}

		internal void button_saveTransfer_Click() {
			try {
				if(stockTransferManagerImpl.saveTransfer()) {
					ShowMessage.success(Common.Messages.Success.Success004);
					stockTransferManagerImpl.resetUI();
				}
			} catch(Exception) {
			}
		}


		//////////////////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////////////////////

		internal void stockTransferHistoryLoaded() {
			try {
				if(!stockTransferHistory.IsLoadedUI) {
					stockTransferManagerImpl.stockTransferHistoryLoaded();
					stockTransferHistory.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void filter() {
			try {
				stockTransferManagerImpl.filter();
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				stockTransferManagerImpl.setRowsCount();
			} catch(Exception) {
			}
		}
	}
}
