using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class DailyInitialCashManagerImpl {

		private DailyInitialCashManager dailyInitialCashManager;
		private DailyInitialCashDao dao = null;
		private PaymentManagerImpl paymentManagerImpl = null;
		private ExpenseManagerImpl expenseManagerImpl = null;

		public DailyInitialCashManagerImpl(DailyInitialCashManager dailyInitialCashManager) {
			this.dailyInitialCashManager = dailyInitialCashManager;
			dao = DailyInitialCashDao.getInstance();
			paymentManagerImpl = new PaymentManagerImpl();
			expenseManagerImpl = new ExpenseManagerImpl();
		}

		public int add(Entity entity) {
			try {
				if(Session.Permission["canAddDailyInitialCash"] == 1) {
					return dao.add(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return 0;
		}

		public List<DailyInitialCash> get(Entity entity) {
			return dao.get(entity).Cast<DailyInitialCash>().ToList();
		}

		public int upd(Entity entity) {
			try {
				if(Session.Permission["canUpdateDailyInitialCash"] == 1) {
					dao.upd(entity);
					return 1;
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return 0;
		}

		public DailyInitialCash getDailyInitialCashByDate(DateTime date) {
			DailyInitialCash dailyInitialCash = null;
			try {
				DailyInitialCash d = new DailyInitialCash();
				d.Date = date;
				List<DailyInitialCash> list = get(d);
				if(list.Count == 1) {
					dailyInitialCash = get(d)[0];
				}
			} catch(Exception) {
			}
			return dailyInitialCash;
		}

		public bool isDuplicate(DateTime theDate) {
			bool hasARecordForTheDate = false;
			try {
				DailyInitialCash d = new DailyInitialCash();
				d.Date = theDate;
				List<DailyInitialCash> list = get(d);
				if(list.Count > 0) {
					hasARecordForTheDate = true;
				}				
			} catch(Exception) {
			}
			return hasARecordForTheDate;
		}

		////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////

		internal void UserControl_Loaded() {
			try {
				DailyInitialCash dailyInitialCash = getDailyInitialCashByDate(DateTime.Today);
				if(dailyInitialCash != null) {
					dailyInitialCashManager.textBox_initialCashAmount.DoubleValue = dailyInitialCash.Amount;
				} else {
					dailyInitialCashManager.textBox_initialCashAmount.DoubleValue = 0;
				}

				double totalIncome = 0;
				SellingCash sellingCash_request = new SellingCash();
				sellingCash_request.Date = DateTime.Today;
				List<SellingCash> listSellingCash = paymentManagerImpl.getSellingCash(sellingCash_request);
				foreach(SellingCash sellingCash_a in listSellingCash) {
					totalIncome += sellingCash_a.Amount;
				}
				dailyInitialCashManager.textBox_totalIncome.Text = totalIncome.ToString("#,##0.00");

				double totalExpences = 0;
				BuyingCash buyingCash_request = new BuyingCash();
				buyingCash_request.Date = DateTime.Today;
				List<BuyingCash> listBuyingCash = paymentManagerImpl.getBuyingCash(buyingCash_request);
				foreach(BuyingCash buyingCash_a in listBuyingCash) {
					totalExpences += buyingCash_a.Amount;
				}
				Expense expense_request = new Expense();
				expense_request.Date = DateTime.Today;
				List<Expense> listExpense = expenseManagerImpl.get(expense_request);
				foreach(Expense expense_a in listExpense) {
					totalExpences += expense_a.Amount;
				}
				dailyInitialCashManager.textBox_totalExpences.Text = totalExpences.ToString("#,##0.00");

				dailyInitialCashManager.textBox_finalCashAmount.Text = getFinalCashAmount(Convert.ToDouble(dailyInitialCashManager.textBox_initialCashAmount.Text), totalIncome, totalExpences, Convert.ToDouble(string.IsNullOrWhiteSpace(dailyInitialCashManager.textBox_otherWidthraw.Text) ? "0" : dailyInitialCashManager.textBox_otherWidthraw.Text)).ToString("#,##0.00");
			} catch(Exception) {
			}
		}

		public double getFinalCashAmount(double theInitialCashAmount, double theTotalIncome, double theTotalExpences, double theOtherWithdrawAmount) {
			double finalCashAmount = 0;
			try {
				finalCashAmount = theInitialCashAmount + theTotalIncome - theTotalExpences - theOtherWithdrawAmount;
			} catch(Exception) {
			}
			return finalCashAmount;
		}

		internal bool saveDailyInitialCash() {
			bool isDone = false;
			try {
				DailyInitialCash dailyInitialCash_the = new DailyInitialCash();
				dailyInitialCash_the.Date = DateTime.Today;
				dailyInitialCash_the.Amount = dailyInitialCashManager.textBox_initialCashAmount.DoubleValue;
				CommonMethods.setCDMDForAdd(dailyInitialCash_the);
				if(isDuplicate(DateTime.Today)) {
					dailyInitialCash_the = getDailyInitialCashByDate(DateTime.Today);
					dailyInitialCash_the.Amount = dailyInitialCashManager.textBox_initialCashAmount.DoubleValue;
					CommonMethods.setCDMDForUpdate(dailyInitialCash_the);
					upd(dailyInitialCash_the);
					isDone = true;
				} else if(add(dailyInitialCash_the) > 0) {
					isDone = true;
				}
			} catch(Exception) {
			}
			return isDone;
		}
	}
}
