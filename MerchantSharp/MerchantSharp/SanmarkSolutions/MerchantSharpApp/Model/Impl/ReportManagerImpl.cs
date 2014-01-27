using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class ReportManagerImpl {

		private SellingInvoiceManagerImpl sellingInvoiceManagerImpl = null;
		private PaymentManagerImpl paymentManagerImpl = null;
		private BankManagerImpl bankManagerImpl = null;

		private DailySale dailySale;
		private DailyItemSale dailyItemSale;
		private DailyProfit dailyProfit;
		private ProfitPerItem profitPerItem;
		private BuyingChequeRreport buyingChequeRreport;

		public ReportManagerImpl(DailySale dailySale) {
			this.dailySale = dailySale;
			sellingInvoiceManagerImpl = new SellingInvoiceManagerImpl();
			paymentManagerImpl = new PaymentManagerImpl();
		}

		public ReportManagerImpl(DailyItemSale dailyItemSale) {
			this.dailyItemSale = dailyItemSale;
		}

		public ReportManagerImpl(DailyProfit dailyProfit) {
			this.dailyProfit = dailyProfit;
		}

		public ReportManagerImpl(ProfitPerItem profitPerItem) {
			this.profitPerItem = profitPerItem;
		}

		public ReportManagerImpl(BuyingChequeRreport buyingChequeRreport) {
			this.buyingChequeRreport = buyingChequeRreport;
			paymentManagerImpl = new PaymentManagerImpl();
			bankManagerImpl = new BankManagerImpl();
		}

		internal void dailySale_UserContolLoaded() {
			try {
				dailySale.DataTable = new DataTable();
				dailySale.DataTable.Columns.Add("Date", typeof(String));
				dailySale.DataTable.Columns.Add("Gross Sale", typeof(String));
				dailySale.DataTable.Columns.Add("Discount", typeof(String));
				dailySale.DataTable.Columns.Add("Company RTN", typeof(String));
				dailySale.DataTable.Columns.Add("Good RTN", typeof(String));
				dailySale.DataTable.Columns.Add("Waste RTN", typeof(String));
				dailySale.DataTable.Columns.Add("Net Sale", typeof(String));
				dailySale.DataTable.Columns.Add("Cash", typeof(String));
				dailySale.DataTable.Columns.Add("Cheque", typeof(String));
				dailySale.DataTable.Columns.Add("Account", typeof(String));
				dailySale.DataTable.Columns.Add("Other", typeof(String));
				dailySale.DataTable.Columns.Add("Credit", typeof(String));
				dailySale.DataTable.Columns.Add("BadDebts", typeof(String));
				dailySale.dataGrid.DataContext = dailySale.DataTable.DefaultView;

				dailySale.Pagination = new Pagination();
				dailySale.Pagination.Filter = dailySale;
				dailySale.grid_pagination.Children.Add(dailySale.Pagination);
				setDailySaleRowsCount();
			} catch(Exception) {
			}
		}

		internal void filterDailySale() {
			try {
				DataSet dataSet = ReportDao.getDailySale((dailySale.datePicker_from.SelectedDate != null ? Convert.ToDateTime(dailySale.datePicker_from.SelectedDate).ToString("yyyy-MM-dd") : null),
					(dailySale.datePicker_to.SelectedDate != null ? Convert.ToDateTime(dailySale.datePicker_to.SelectedDate).ToString("yyyy-MM-dd") : null),
					false, dailySale.Pagination.LimitStart, dailySale.Pagination.LimitCount);
				dailySale.DataTable.Rows.Clear();
				List<SellingInvoice> listInvoice = null;
				SellingInvoice i = null;
				double cash = 0;
				double cheque = 0;
				double account = 0;
				double other = 0;
				double badDebts = 0;
				double credit = 0;
				foreach(DataRow row in dataSet.Tables[0].Rows) {
					i = new SellingInvoice();
					i.Date = Convert.ToDateTime(row[0]);
					i.Status = 1;
					listInvoice = sellingInvoiceManagerImpl.getInvoice(i);

					foreach(SellingInvoice sellingInvoice in listInvoice) {

						double billCash = 0;
						double billCheue = 0;
						double billAccount = 0;
						double billOther = 0;

						SellingCash a = new SellingCash();
						a.SellingInvoiceId = sellingInvoice.Id;
						a.Date = sellingInvoice.Date;
						List<SellingCash> cashs = paymentManagerImpl.getSellingCash(a);
						foreach(SellingCash sellingCash in cashs) {
							cash += sellingCash.Amount;
							billCash += sellingCash.Amount;
						}

						SellingCheque b = new SellingCheque();
						b.SellingInvoiceId = sellingInvoice.Id;
						b.IssuedDate = sellingInvoice.Date;
						List<SellingCheque> cheques = paymentManagerImpl.getSellingCheque(b);
						foreach(SellingCheque sellingCheque in cheques) {
							cheque += sellingCheque.Amount;
							billCheue += sellingCheque.Amount;
						}

						account += sellingInvoice.CustomerAccountBalanceChange;
						billAccount += sellingInvoice.CustomerAccountBalanceChange;

						SellingOther c = new SellingOther();
						c.SellingInvoiceId = sellingInvoice.Id;
						c.Date = sellingInvoice.Date;
						List<SellingOther> others = paymentManagerImpl.getSellingOther(c);
						foreach(SellingOther sellingOther in others) {
							other += sellingOther.Amount;
							billOther += sellingOther.Amount;
						}

						if(sellingInvoice.IsCompletelyPaid == 1) {
							badDebts += sellingInvoiceManagerImpl.getNetTotalByInvoiceId(sellingInvoice.Id) - (billCash + billCheue + billAccount + billOther);
						}

					}
					credit = Convert.ToDouble(row[6]) - (cash + cheque + account + other + badDebts);
					dailySale.DataTable.Rows.Add(Convert.ToDateTime(row[0]).ToString("yyyy-MM-dd"), Convert.ToDouble(row[1]).ToString("#,##0.00"),
						Convert.ToDouble(row[2]).ToString("#,##0.00"), Convert.ToDouble(row[3]).ToString("#,##0.00"),
						Convert.ToDouble(row[4]).ToString("#,##0.00"), Convert.ToDouble(row[5]).ToString("#,##0.00"),
						Convert.ToDouble(row[6]).ToString("#,##0.00"), cash.ToString("#,##0.00"), cheque.ToString("#,##0.00"),
						account.ToString("#,##0.00"), other.ToString("#,##0.00"), credit.ToString("#,##0.00"), badDebts.ToString("#,##0.00"));

					cash = 0;
					cheque = 0;
					account = 0;
					other = 0;
					badDebts = 0;
					credit = 0;
				}
			} catch(Exception) {
			}
		}

		internal void setDailySaleRowsCount() {
			try {
				DataSet dataSet = ReportDao.getDailySale((dailySale.datePicker_from.SelectedDate != null ? Convert.ToDateTime(dailySale.datePicker_from.SelectedDate).ToString("yyyy-MM-dd") : null),
					(dailySale.datePicker_to.SelectedDate != null ? Convert.ToDateTime(dailySale.datePicker_to.SelectedDate).ToString("yyyy-MM-dd") : null),
					true, dailySale.Pagination.LimitStart, dailySale.Pagination.LimitCount);
				dailySale.Pagination.RowsCount = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
			} catch(Exception) {
			}
		}

		///////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////

		internal void dailyItemSale_UserContolLoaded() {
			try {
				dailyItemSale.DataTable = new DataTable();
				dailyItemSale.DataTable.Columns.Add("Date", typeof(String));
				dailyItemSale.DataTable.Columns.Add("Item Name", typeof(String));
				dailyItemSale.DataTable.Columns.Add("Quantity", typeof(String));
				dailyItemSale.DataTable.Columns.Add("Gross Sale", typeof(String));
				dailyItemSale.DataTable.Columns.Add("Discount", typeof(String));
				dailyItemSale.DataTable.Columns.Add("Company RTN", typeof(String));
				dailyItemSale.DataTable.Columns.Add("Good RTN", typeof(String));
				dailyItemSale.DataTable.Columns.Add("Waste RTN", typeof(String));
				dailyItemSale.DataTable.Columns.Add("Net Sale", typeof(String));
				dailyItemSale.DataTable.Columns.Add("Profit", typeof(String));
				dailyItemSale.dataGrid.DataContext = dailyItemSale.DataTable.DefaultView;

				dailyItemSale.Pagination = new Pagination();
				dailyItemSale.Pagination.Filter = dailyItemSale;
				dailyItemSale.grid_pagination.Children.Add(dailyItemSale.Pagination);

				dailyItemSale.ItemFinder = new ItemFinder(dailyItemSale.textBox_categoryId, dailyItemSale.textBox_companyId, dailyItemSale.textBox_itemId);
				dailyItemSale.grid_itemFinder.Children.Add(dailyItemSale.ItemFinder);

				setDailyItemSaleRowsCount();
			} catch(Exception) {
			}
		}

		internal void filterDailyItemSale() {
			try {
				DataSet dataSet = ReportDao.getDailyItemSale((!String.IsNullOrWhiteSpace(dailyItemSale.textBox_categoryId.Text) ? Convert.ToInt32(dailyItemSale.textBox_categoryId.Text) : 0),
					(!String.IsNullOrWhiteSpace(dailyItemSale.textBox_companyId.Text) ? Convert.ToInt32(dailyItemSale.textBox_companyId.Text) : 0), (!String.IsNullOrWhiteSpace(dailyItemSale.textBox_itemId.Text) ? Convert.ToInt32(dailyItemSale.textBox_itemId.Text) : 0),
					(dailyItemSale.datePicker_from.SelectedDate != null ? Convert.ToDateTime(dailyItemSale.datePicker_from.SelectedDate).ToString("yyyy-MM-dd") : null),
					(dailyItemSale.datePicker_to.SelectedDate != null ? Convert.ToDateTime(dailyItemSale.datePicker_to.SelectedDate).ToString("yyyy-MM-dd") : null),
					false, dailyItemSale.Pagination.LimitStart, dailyItemSale.Pagination.LimitCount);
				dailyItemSale.DataTable.Rows.Clear();
				foreach(DataRow row in dataSet.Tables[0].Rows) {
					dailyItemSale.DataTable.Rows.Add(Convert.ToDateTime(row[0]).ToString("yyyy-MM-dd"), row[1], Convert.ToDouble(row[2]).ToString("#,##0.00"), Convert.ToDouble(row[3]).ToString("#,##0.00"),
						Convert.ToDouble(row[4]).ToString("#,##0.00"), Convert.ToDouble(row[5]).ToString("#,##0.00"),
						Convert.ToDouble(row[6]).ToString("#,##0.00"), Convert.ToDouble(row[7]).ToString("#,##0.00"),
						Convert.ToDouble(row[8]).ToString("#,##0.00"), Convert.ToDouble(row[9]).ToString("#,##0.00"));
				}
			} catch(Exception) {
			}
		}

		internal void setDailyItemSaleRowsCount() {
			try {
				if(String.IsNullOrWhiteSpace(dailyItemSale.textBox_itemId.Text)) {
					dailyItemSale.label_selectedItemName.Content = null;
				}
				DataSet dataSet = ReportDao.getDailyItemSale((!String.IsNullOrWhiteSpace(dailyItemSale.textBox_categoryId.Text) ? Convert.ToInt32(dailyItemSale.textBox_categoryId.Text) : 0),
					(!String.IsNullOrWhiteSpace(dailyItemSale.textBox_companyId.Text) ? Convert.ToInt32(dailyItemSale.textBox_companyId.Text) : 0), (!String.IsNullOrWhiteSpace(dailyItemSale.textBox_itemId.Text) ? Convert.ToInt32(dailyItemSale.textBox_itemId.Text) : 0),
					(dailyItemSale.datePicker_from.SelectedDate != null ? Convert.ToDateTime(dailyItemSale.datePicker_from.SelectedDate).ToString("yyyy-MM-dd") : null),
					(dailyItemSale.datePicker_to.SelectedDate != null ? Convert.ToDateTime(dailyItemSale.datePicker_to.SelectedDate).ToString("yyyy-MM-dd") : null),
					true, dailyItemSale.Pagination.LimitStart, dailyItemSale.Pagination.LimitCount);
				dailyItemSale.Pagination.RowsCount = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
			} catch(Exception) {
			}
		}

		///////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////

		internal void dailyProfit_UserContolLoaded() {
			try {
				dailyProfit.DataTable = new DataTable();
				dailyProfit.DataTable.Columns.Add("Date", typeof(String));
				dailyProfit.DataTable.Columns.Add("Selling Profit", typeof(String));
				dailyProfit.DataTable.Columns.Add("Expenses", typeof(String));
				dailyProfit.DataTable.Columns.Add("Net Profit", typeof(String));
				dailyProfit.dataGrid.DataContext = dailyProfit.DataTable.DefaultView;
				dailyProfit.dataGrid.Columns[0].Width = 100;
				dailyProfit.dataGrid.Columns[1].Width = 120;
				dailyProfit.dataGrid.Columns[2].Width = 100;
				dailyProfit.dataGrid.Columns[3].Width = 120;

				dailyProfit.Pagination = new Pagination();
				dailyProfit.Pagination.Filter = dailyProfit;
				dailyProfit.grid_pagination.Children.Add(dailyProfit.Pagination);
				setDailyProfitRowsCount();
			} catch(Exception) {
			}
		}

		internal void filterDailyProfit() {
			try {
				DataSet dataSet = ReportDao.getDailyProfit((dailyProfit.datePicker_from.SelectedDate != null ? Convert.ToDateTime(dailyProfit.datePicker_from.SelectedDate).ToString("yyyy-MM-dd") : null),
					(dailyProfit.datePicker_to.SelectedDate != null ? Convert.ToDateTime(dailyProfit.datePicker_to.SelectedDate).ToString("yyyy-MM-dd") : null),
					false, dailyProfit.Pagination.LimitStart, dailyProfit.Pagination.LimitCount);
				dailyProfit.DataTable.Rows.Clear();
				foreach(DataRow row in dataSet.Tables[0].Rows) {
					dailyProfit.DataTable.Rows.Add(Convert.ToDateTime(row[0]).ToString("yyyy-MM-dd"),
						Convert.ToDouble(row[1]).ToString("#,##0.00"), Convert.ToDouble(row[2]).ToString("#,##0.00"),
						Convert.ToDouble(row[3]).ToString("#,##0.00"));
				}
			} catch(Exception) {
			}
		}

		internal void setDailyProfitRowsCount() {
			try {
				DataSet dataSet = ReportDao.getDailyProfit((dailyProfit.datePicker_from.SelectedDate != null ? Convert.ToDateTime(dailyProfit.datePicker_from.SelectedDate).ToString("yyyy-MM-dd") : null),
					(dailyProfit.datePicker_to.SelectedDate != null ? Convert.ToDateTime(dailyProfit.datePicker_to.SelectedDate).ToString("yyyy-MM-dd") : null),
					true, dailyProfit.Pagination.LimitStart, dailyProfit.Pagination.LimitCount);
				dailyProfit.Pagination.RowsCount = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
			} catch(Exception) {
			}
		}

		///////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////


		internal void profitPerItem_UserContolLoaded() {
			try {
				UIComboBox.categoriesForSelect(profitPerItem.comboBox_categoryId);
				UIComboBox.companiesForCategory(profitPerItem.comboBox_companyId, Convert.ToInt32(profitPerItem.comboBox_categoryId.SelectedValue));

				profitPerItem.DataTable = new DataTable();
				profitPerItem.DataTable.Columns.Add("ID", typeof(String));
				profitPerItem.DataTable.Columns.Add("Category", typeof(String));
				profitPerItem.DataTable.Columns.Add("Company", typeof(String));
				profitPerItem.DataTable.Columns.Add("Item", typeof(String));
				profitPerItem.DataTable.Columns.Add("Profit", typeof(String));
				profitPerItem.dataGrid.DataContext = profitPerItem.DataTable.DefaultView;
				profitPerItem.dataGrid.Columns[0].Width = 100;
				profitPerItem.dataGrid.Columns[1].Width = 120;
				profitPerItem.dataGrid.Columns[2].Width = 100;
				profitPerItem.dataGrid.Columns[3].Width = 120;

				profitPerItem.Pagination = new Pagination();
				profitPerItem.Pagination.Filter = profitPerItem;
				profitPerItem.grid_pagination.Children.Add(profitPerItem.Pagination);
				setProfitPerItemRowsCount();
			} catch(Exception) {
			}
		}

		internal void setProfitPerItemRowsCount() {
			try {
				DataSet dataSet = ReportDao.getProfitPerItem((profitPerItem.datePicker_from.SelectedDate != null ? Convert.ToDateTime(profitPerItem.datePicker_from.SelectedDate).ToString("yyyy-MM-dd") : null),
					(profitPerItem.datePicker_to.SelectedDate != null ? Convert.ToDateTime(profitPerItem.datePicker_to.SelectedDate).ToString("yyyy-MM-dd") : null),
					Convert.ToInt32(profitPerItem.comboBox_categoryId.SelectedValue), Convert.ToInt32(profitPerItem.comboBox_companyId.SelectedValue), profitPerItem.textBox_itemName.TrimedText, true, profitPerItem.Pagination.LimitStart, profitPerItem.Pagination.LimitCount);
				profitPerItem.Pagination.RowsCount = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
			} catch(Exception) {
			}
		}

		internal void filterProfitPerItem() {
			try {
				DataSet dataSet = ReportDao.getProfitPerItem((profitPerItem.datePicker_from.SelectedDate != null ? Convert.ToDateTime(profitPerItem.datePicker_from.SelectedDate).ToString("yyyy-MM-dd") : null),
					(profitPerItem.datePicker_to.SelectedDate != null ? Convert.ToDateTime(profitPerItem.datePicker_to.SelectedDate).ToString("yyyy-MM-dd") : null),
					Convert.ToInt32(profitPerItem.comboBox_categoryId.SelectedValue), Convert.ToInt32(profitPerItem.comboBox_companyId.SelectedValue), profitPerItem.textBox_itemName.TrimedText, false, profitPerItem.Pagination.LimitStart, profitPerItem.Pagination.LimitCount);
				profitPerItem.DataTable.Rows.Clear();
				foreach(DataRow row in dataSet.Tables[0].Rows) {
					profitPerItem.DataTable.Rows.Add(row[0], row[1], row[2], row[3],
					Convert.ToDouble(row[4]).ToString("#,##0.00"));
				}
			} catch(Exception) {
			}
		}

		///////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////


		internal void buyingChequeReport_UserContolLoaded() {
			try {
				UIComboBox.chequeStatus(buyingChequeRreport.comboBox_status);
				buyingChequeRreport.comboBox_status.SelectedIndex = 0;

				buyingChequeRreport.DataTable = new DataTable();
				buyingChequeRreport.DataTable.Columns.Add("ID", typeof(int));
				buyingChequeRreport.DataTable.Columns.Add("Bank Name", typeof(String));
				buyingChequeRreport.DataTable.Columns.Add("Cheque Number", typeof(String));
				buyingChequeRreport.DataTable.Columns.Add("Issued Date", typeof(String));
				buyingChequeRreport.DataTable.Columns.Add("Payble Date", typeof(String));
				buyingChequeRreport.DataTable.Columns.Add("Amount", typeof(String));
				buyingChequeRreport.DataTable.Columns.Add("Status", typeof(String));
				buyingChequeRreport.dataGrid.DataContext = buyingChequeRreport.DataTable.DefaultView;

				buyingChequeRreport.Pagination = new Pagination();
				buyingChequeRreport.Pagination.Filter = buyingChequeRreport;
				buyingChequeRreport.grid_pagination.Children.Add(buyingChequeRreport.Pagination);
				setBuyingChequeRreportRowsCount();
			} catch(Exception) {
			}
		}

		private BuyingCheque getBuyingChequeForFilter() {
			BuyingCheque buyingCheque = null;
			try {
				buyingCheque = new BuyingCheque();
				if(buyingChequeRreport.datePicker_from.SelectedDate != null || buyingChequeRreport.datePicker_to.SelectedDate != null) {
					if(buyingChequeRreport.datePicker_from.SelectedDate != null && buyingChequeRreport.datePicker_to.SelectedDate != null) {
						buyingCheque.PayableDate = buyingChequeRreport.datePicker_from.SelectedValue;
						buyingCheque.addDateCondition("date", "BETWEEN", buyingChequeRreport.datePicker_from.SelectedValue.ToString("yyyy-MM-dd"), buyingChequeRreport.datePicker_to.SelectedValue.ToString("yyyy-MM-dd"));
					} else if(buyingChequeRreport.datePicker_from.SelectedDate != null) {
						buyingCheque.PayableDate = buyingChequeRreport.datePicker_from.SelectedValue;
					} else {
						buyingCheque.PayableDate = buyingChequeRreport.datePicker_to.SelectedValue;
					}
				}
				buyingCheque.Status = Convert.ToInt32(buyingChequeRreport.comboBox_status.SelectedValue);
				buyingCheque.Status = buyingChequeRreport.comboBox_status.Value;
			} catch(Exception) {
			}
			return buyingCheque;
		}

		internal void filterBuyingCheque() {
			try {
				BuyingCheque bc = getBuyingChequeForFilter();
				bc.LimitStart = buyingChequeRreport.Pagination.LimitStart;
				bc.LimitEnd = buyingChequeRreport.Pagination.LimitCount;
				List<BuyingCheque> list = paymentManagerImpl.getBuyingCheque(bc);
				buyingChequeRreport.DataTable.Rows.Clear();
				foreach(BuyingCheque buyingCheque in list) {
					buyingChequeRreport.DataTable.Rows.Add(buyingCheque.Id, bankManagerImpl.getBankById(buyingCheque.BankId).Name, buyingCheque.ChequeNumber,
						buyingCheque.IssuedDate.ToString("yyyy-MM-dd"), buyingCheque.PayableDate.ToString("yyyy-MM-dd"),
						buyingCheque.Amount.ToString("#,##0.00"), CommonMethods.getStatusForCheque(buyingCheque.Status));
				}
			} catch(Exception) {
			}
		}

		internal void setBuyingChequeRreportRowsCount() {
			try {
				BuyingCheque buyingCheque = getBuyingChequeForFilter();
				buyingCheque.RowsCount = 1;
				buyingCheque.LimitStart = buyingChequeRreport.Pagination.LimitStart;
				buyingCheque.LimitEnd = buyingChequeRreport.Pagination.LimitCount;
				List<BuyingCheque> list = paymentManagerImpl.getBuyingCheque(buyingCheque);
				buyingChequeRreport.Pagination.RowsCount = list[0].RowsCount;
			} catch(Exception) {
			}
		}
	}
}
