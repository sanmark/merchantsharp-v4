using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class DailyInitialCashManagerControler {

		private DailyInitialCashManager dailyInitialCashManager;
		private DailyInitialCashManagerImpl dailyInitialCashManagerImpl = null;

		public DailyInitialCashManagerControler(DailyInitialCashManager dailyInitialCashManager) {
			this.dailyInitialCashManager = dailyInitialCashManager;
			dailyInitialCashManagerImpl = new DailyInitialCashManagerImpl(dailyInitialCashManager);
		}

		internal void UserControl_Loaded() {
			try {
				if(!dailyInitialCashManager.IsLoadedUI) {
					dailyInitialCashManagerImpl.UserControl_Loaded();
					dailyInitialCashManager.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void textBox_otherWidthraw_TextChanged() {
			try {
				double initialCashAmount = string.IsNullOrWhiteSpace(dailyInitialCashManager.textBox_initialCashAmount.Text) ? 0 : Convert.ToDouble(dailyInitialCashManager.textBox_initialCashAmount.Text);
				double totalIncome = string.IsNullOrWhiteSpace(dailyInitialCashManager.textBox_totalIncome.Text) ? 0 : Convert.ToDouble(dailyInitialCashManager.textBox_totalIncome.Text);
				double totalExpense = string.IsNullOrWhiteSpace(dailyInitialCashManager.textBox_totalExpences.Text) ? 0 : Convert.ToDouble(dailyInitialCashManager.textBox_totalExpences.Text);
				double otherWithdrawals = string.IsNullOrWhiteSpace(dailyInitialCashManager.textBox_otherWidthraw.Text) ? 0 : Convert.ToDouble(dailyInitialCashManager.textBox_otherWidthraw.Text);
				dailyInitialCashManager.textBox_finalCashAmount.Text = dailyInitialCashManagerImpl.getFinalCashAmount(initialCashAmount, totalIncome, totalExpense, otherWithdrawals).ToString("#,##0.00");
			} catch(Exception) {
			}
		}

		internal void button_saveInitialCashAmount_Click() {
			try {
				if(dailyInitialCashManagerImpl.saveDailyInitialCash()) {
					ShowMessage.success(Common.Messages.Success.Success004);
				}
			} catch(Exception) {
			}
		}
	}
}
