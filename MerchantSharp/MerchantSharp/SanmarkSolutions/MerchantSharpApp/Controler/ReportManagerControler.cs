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

		public ReportManagerControler(DailySale dailySale) {
			this.dailySale = dailySale;
			reportManagerImpl = new ReportManagerImpl(dailySale);
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
	}
}
