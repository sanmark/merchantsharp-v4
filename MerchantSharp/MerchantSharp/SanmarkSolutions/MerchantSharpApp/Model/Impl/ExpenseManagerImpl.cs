using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.ReportMold;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Reports;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class ExpenseManagerImpl {

		private ExpenseManager expenseManager;
		private IDao dao = null;

		public ExpenseManagerImpl() {
			dao = ExpenseDao.getInstance();
		}

		public ExpenseManagerImpl(ExpenseManager expenseManager) {
			dao = ExpenseDao.getInstance();
			this.expenseManager = expenseManager;
		}

		public int add(Entity entity) {
			try {
				if(Session.Permission["canAddExpense"] == 1) {
					return dao.add(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return 0;
		}

		public bool del(Entity entity) {
			return dao.del(entity);
		}

		public List<Expense> get(Entity entity) {
			return dao.get(entity).Cast<Expense>().ToList();
		}

		public int upd(Entity entity) {
			try {
				if(Session.Permission["canUpdateExpense"] == 1) {
					return dao.upd(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return 0;
		}

		public Expense getExpenseById(int id) {
			Expense expense = null;
			try {
				Expense e = new Expense();
				e.Id = id;
				expense = get(e)[0];
			} catch(Exception) {
			}
			return expense;
		}


		///////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////

		internal void UserControl_Loaded() {
			try {
				expenseManager.DataTable = new DataTable();
				expenseManager.DataTable.Columns.Add("ID", typeof(int));
				expenseManager.DataTable.Columns.Add("Date", typeof(String));
				expenseManager.DataTable.Columns.Add("Amount", typeof(String));
				expenseManager.DataTable.Columns.Add("Receiver", typeof(String));
				expenseManager.DataTable.Columns.Add("Description", typeof(String));

				expenseManager.DataGridFooter = new DataGridFooter();
				expenseManager.dataGrid.IFooter = expenseManager.DataGridFooter;
				expenseManager.grid_footer.Children.Add(expenseManager.DataGridFooter);
				expenseManager.dataGrid.DataContext = expenseManager.DataTable.DefaultView;

				expenseManager.Pagination = new Pagination();
				expenseManager.Pagination.Filter = expenseManager;
				expenseManager.grid_pagination.Children.Add(expenseManager.Pagination);
				
				setRowsCount();
			} catch(Exception) {
			}
		}

		private Expense getExpenseForFilter() {
			Expense expense = new Expense();
			try {
				if(expenseManager.datePicker_from_filter.SelectedDate != null || expenseManager.datePicker_to_filter.SelectedDate != null) {
					if(expenseManager.datePicker_from_filter.SelectedDate != null && expenseManager.datePicker_to_filter.SelectedDate != null) {
						expense.Date = expenseManager.datePicker_from_filter.SelectedValue;
						expense.addDateCondition("date", "BETWEEN", expenseManager.datePicker_from_filter.SelectedValue.ToString("yyyy-MM-dd"), expenseManager.datePicker_to_filter.SelectedValue.ToString("yyyy-MM-dd"));
					} else if(expenseManager.datePicker_from_filter.SelectedDate != null) {
						expense.Date = expenseManager.datePicker_from_filter.SelectedValue;
					} else {
						expense.Date = expenseManager.datePicker_to_filter.SelectedValue;
					}
				}
				expense.Amount = expenseManager.textBox_amount_filter.DoubleValue > 0 ? expenseManager.textBox_amount_filter.DoubleValue : -1;
				expense.Receiver = "%" + expenseManager.textBox_receiver_filter.TrimedText + "%";
				expense.Details = "%" + expenseManager.textBox_description_filter.Text + "%";
			} catch(Exception) {
			}
			return expense;
		}

		internal void filter() {
			try {
				Expense e = getExpenseForFilter();
				e.LimitStart = expenseManager.Pagination.LimitStart;
				e.LimitEnd = expenseManager.Pagination.LimitCount;
				List<Expense> list = get(e);
				expenseManager.DataTable.Rows.Clear();
				foreach(Expense expense in list) {
					expenseManager.DataTable.Rows.Add(expense.Id, expense.Date.ToString("yyyy-MM-dd"), expense.Amount.ToString("#,##0.00"), expense.Receiver, expense.Details);
				}
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				Expense expense = getExpenseForFilter();
				expense.RowsCount = 1;
				expenseManager.Pagination.RowsCount = get(expense)[0].RowsCount;
			} catch(Exception) {
			}
		}

		internal bool addExpense() {
			bool b = false;
			try {
				bool isOkay = true;
				if(expenseManager.textBox_receiver_addExpense.IsNull()) {
					expenseManager.textBox_receiver_addExpense.ErrorMode(true);
					isOkay = false;
				}
				if(expenseManager.textBox_amount_addExpense.DoubleValue <= 0) {
					expenseManager.textBox_amount_addExpense.ErrorMode(true);
					isOkay = false;
				}
				if(expenseManager.textBox_date_addExpense.SelectedDate == null) {
					expenseManager.textBox_date_addExpense.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					Expense expense = new Expense();
					expense.Date = Convert.ToDateTime(expenseManager.textBox_date_addExpense.SelectedDate);
					expense.Amount = expenseManager.textBox_amount_addExpense.DoubleValue;
					expense.Receiver = expenseManager.textBox_receiver_addExpense.TrimedText;
					expense.Details = expenseManager.textBox_description_addExpense.Text;
					CommonMethods.setCDMDForAdd(expense);
					if(add(expense) > 0) {
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void resetAddForm() {
			try {
				expenseManager.textBox_date_addExpense.SelectedDate = DateTime.Today;
				expenseManager.textBox_amount_addExpense.Clear();
				expenseManager.textBox_receiver_addExpense.Clear();
				expenseManager.textBox_description_addExpense.Clear();
			} catch(Exception) {
			}
		}

		internal bool updateExpense() {
			bool b = false;
			try {
				bool isOkay = true;
				if(expenseManager.textBox_receiver_addExpense.IsNull()) {
					expenseManager.textBox_receiver_addExpense.ErrorMode(true);
					isOkay = false;
				}
				if(expenseManager.textBox_amount_addExpense.DoubleValue <= 0) {
					expenseManager.textBox_amount_addExpense.ErrorMode(true);
					isOkay = false;
				}
				if(expenseManager.textBox_date_addExpense.SelectedDate == null) {
					expenseManager.textBox_date_addExpense.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					Expense expense = expenseManager.SelectedExpense;
					expense.Date = Convert.ToDateTime(expenseManager.textBox_date_addExpense.SelectedDate);
					expense.Amount = expenseManager.textBox_amount_addExpense.DoubleValue;
					expense.Receiver = expenseManager.textBox_receiver_addExpense.TrimedText;
					expense.Details = expenseManager.textBox_description_addExpense.Text;
					CommonMethods.setCDMDForUpdate(expense);
					upd(expense);
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void switchToAddMode() {
			try {
				expenseManager.SelectedExpense = null;
				expenseManager.IsUpdateMode = false;
				expenseManager.button_save_addExpense.Content = "Save";
				expenseManager.button_reset_addExpense.Content = "Reset";
			} catch(Exception) {
			}
		}

		internal void switchToUpdateMode() {
			try {
				expenseManager.IsUpdateMode = true;
				Expense expense = getExpenseById(expenseManager.dataGrid.SelectedItemID);
				expenseManager.SelectedExpense = expense;
				expenseManager.textBox_date_addExpense.SelectedDate = expense.Date;
				expenseManager.textBox_amount_addExpense.DoubleValue = expense.Amount;
				expenseManager.textBox_receiver_addExpense.Text = expense.Receiver;
				expenseManager.textBox_description_addExpense.Text = expense.Details;

				expenseManager.button_save_addExpense.Content = "Update";
				expenseManager.button_reset_addExpense.Content = "Cancel";
			} catch(Exception) {
			}
		}

		internal void print() {
			try {
				DataTable dT = new DataTable();
				dT.Columns.Add("ID", typeof(int));
				dT.Columns.Add("Date", typeof(String));
				dT.Columns.Add("Amount", typeof(String));
				dT.Columns.Add("Receiver", typeof(String));
				dT.Columns.Add("Details", typeof(String));

				Expense e = getExpenseForFilter();
				e.LimitStart = expenseManager.Pagination.LimitStart;
				e.LimitEnd = expenseManager.Pagination.LimitCount;
				List<Expense> list = get(e);
				double tot = 0;
				foreach ( Expense expense in list ) {
					tot += expense.Amount;
					dT.Rows.Add(expense.Id, expense.Date.ToString("yyyy-MM-dd"), expense.Amount.ToString("#,##0.00"), expense.Receiver, expense.Details);
				}

				PrepareReport prepareReport = new PrepareReport();
				prepareReport.addCommon();
				prepareReport.addParameter("reportType", "Expense Details");
				prepareReport.addParameter("reportDescription", "");

				prepareReport.addParameter("totalValue", tot.ToString("#,##0.00"));

				new ReportViewer(dT, "Expences", prepareReport.getParameters()).Show();
			} catch ( Exception ) {				
			}
		}
	}
}
