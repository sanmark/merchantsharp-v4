using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class StockManagerControler {

		private StockManager stockManager;
		private StockManagerImpl stockManagerImpl;

		public StockManagerControler(StockManager stockManager) {
			this.stockManager = stockManager;
			stockManagerImpl = new StockManagerImpl(stockManager);
		}

		internal void UserControl_Loaded() {
			try {
				if(!stockManager.IsLoadedUI) {
					stockManagerImpl.UserControl_Loaded();
					stockManager.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void filter() {
			try {
				stockManagerImpl.filter();
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				stockManagerImpl.setRowsCount();
			} catch(Exception) {
			}
		}

		internal void button_updateStock_Click() {
			try {
				if(Convert.ToInt32(stockManager.comboBox_stockLocation.SelectedValue) <= 0) {
					ShowMessage.error(Common.Messages.Error.Error013);
				} else if(ShowMessage.confirm(Common.Messages.Information.Info013) == System.Windows.MessageBoxResult.Yes && stockManagerImpl.updateStock()) {
					ShowMessage.success(Common.Messages.Success.Success004);
				}
			} catch(Exception) {
			}
		}
	}
}
