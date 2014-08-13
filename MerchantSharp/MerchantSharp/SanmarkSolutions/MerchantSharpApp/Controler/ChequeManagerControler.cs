using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class ChequeManagerControler {

		private ChequeManager chequeManager;
		private ChequeManagerImpl chequeManagerImpl = null;

		public ChequeManagerControler(ChequeManager chequeManager) {
			this.chequeManager = chequeManager;
			chequeManagerImpl = new ChequeManagerImpl(chequeManager);
		}

		internal void UserControl_Loaded() {
			try {
				if(!chequeManager.IsLoadedUI) {
					chequeManagerImpl.UserControl_Loaded();
					chequeManager.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void filter() {
			try {
				chequeManagerImpl.filter();
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				chequeManagerImpl.setRowsCount();
			} catch(Exception) {
			}
		}

		internal void radioButtonChecked() {
			try {
				chequeManagerImpl.resetUpdateForm();
				chequeManagerImpl.setRowsCount();
			} catch(Exception) {
			}
		}

		internal void dataGrid_MouseDoubleClick() {
			try {
				if(chequeManager.dataGrid.SelectedItemID > 0) {
					chequeManagerImpl.populateUpdateForm();
				}
			} catch(Exception) {
			}
		}

		internal void button_save_Click() {
			try {
				if((chequeManager.SelectedBuyingCheque != null || chequeManager.SelectedSellingCheque != null) && chequeManagerImpl.updateCheque()) {
					chequeManagerImpl.resetUpdateForm();
					setRowsCount();
					ShowMessage.success(Common.Messages.Success.Success004);
				}
			} catch(Exception) {
			}
		}
	}
}
