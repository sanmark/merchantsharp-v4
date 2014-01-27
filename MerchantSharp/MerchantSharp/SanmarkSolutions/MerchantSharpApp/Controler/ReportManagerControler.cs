using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class ReportManagerControler {

		private ReportManagerImpl reportManagerImpl = null;
		private DailySale dailySale;
		private DailyItemSale dailyItemSale;
		private   DailyProfit dailyProfit;
		private   ProfitPerItem profitPerItem;

		public ReportManagerControler(DailySale dailySale) {
			this.dailySale = dailySale;
			reportManagerImpl = new ReportManagerImpl(dailySale);
		}

		public ReportManagerControler(DailyItemSale dailyItemSale) {
			this.dailyItemSale = dailyItemSale;
			reportManagerImpl = new ReportManagerImpl(dailyItemSale);
		}

		public ReportManagerControler(DailyProfit dailyProfit) {
			this.dailyProfit = dailyProfit;
			reportManagerImpl = new ReportManagerImpl(dailyProfit);
		}

		public ReportManagerControler(ProfitPerItem profitPerItem) {
			this.profitPerItem = profitPerItem;
			reportManagerImpl = new ReportManagerImpl(profitPerItem);
		}

		internal void dailySale_UserContolLoaded() {
			try {
				if(!dailySale.IsLoadedUI) {
					reportManagerImpl.dailySale_UserContolLoaded();
					dailySale.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void filterDailySale() {
			try {
				reportManagerImpl.filterDailySale();
			} catch(Exception) {
			}
		}

		internal void setDailySaleRowsCount() {
			try {
				reportManagerImpl.setDailySaleRowsCount();
			} catch(Exception) {
			}
		}

		////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////

		internal void dailyItemSale_UserContolLoaded() {
			try {
				if(!dailyItemSale.IsLoadedUI) {
					reportManagerImpl.dailyItemSale_UserContolLoaded();
					dailyItemSale.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void setDailyItemSaleFilter() {
			try {
				reportManagerImpl.filterDailyItemSale();
			} catch(Exception) {
			}
		}

		internal void setDailyItemSaleRowsCount() {
			try {
				reportManagerImpl.setDailyItemSaleRowsCount();
			} catch(Exception) {
			}
		}

		////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////

		internal void dailyProfit_UserContolLoaded() {
			try {
				if(!dailyProfit.IsLoadedUI) {
					reportManagerImpl.dailyProfit_UserContolLoaded();
					dailyProfit.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void setDailyProfitRowsCount() {
			try {
				reportManagerImpl.setDailyProfitRowsCount();
			} catch(Exception) {
			}
		}

		internal void filterDailyProfit() {
			try {
				reportManagerImpl.filterDailyProfit();
			} catch(Exception) {
			}
		}

		////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////

		internal void profitPerItem_UserControlerLoaded() {
			try {
				if(!profitPerItem.IsLoadedUI) {
					reportManagerImpl.profitPerItem_UserContolLoaded();
					profitPerItem.IsLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		internal void setProfitPerItemRowsCount() {
			try {
				reportManagerImpl.setProfitPerItemRowsCount();
			} catch(Exception) {
			}
		}

		internal void filterProfitPerItem() {
			try {
				reportManagerImpl.filterProfitPerItem();
			} catch(Exception) {
			}
		}
	}
}
