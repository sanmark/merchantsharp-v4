using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class ChequeManagerImpl {

		private ChequeManager chequeManager;
		private PaymentManagerImpl paymentManagerImpl = null;
		private BankManagerImpl bankManagerImpl = null;

		public ChequeManagerImpl(ChequeManager chequeManager) {
			this.chequeManager = chequeManager;
			paymentManagerImpl = new PaymentManagerImpl();
			bankManagerImpl = new BankManagerImpl();
		}

		internal void UserControl_Loaded() {
			try {
				UIComboBox.banksForFilter(chequeManager.comboBox_bank_filter);
				chequeManager.comboBox_bank_filter.SelectedIndex = 0;
				UIComboBox.banksForSelect(chequeManager.comboBox_bank_update);
				UIComboBox.chequeStatus(chequeManager.comboBox_status_filter);
				chequeManager.comboBox_status_filter.SelectedIndex = 0;
				UIComboBox.chequeStatus(chequeManager.comboBox_status_update);
				chequeManager.comboBox_status_update.SelectedIndex = 0;

				chequeManager.DataTable = new DataTable();
				chequeManager.DataTable.Columns.Add("ID", typeof(int));
				chequeManager.DataTable.Columns.Add("Bank Name", typeof(String));
				chequeManager.DataTable.Columns.Add("Cheque Number", typeof(String));
				chequeManager.DataTable.Columns.Add("Issued Date", typeof(String));
				chequeManager.DataTable.Columns.Add("Payable Date", typeof(String));
				chequeManager.DataTable.Columns.Add("Amount", typeof(String));
				chequeManager.DataTable.Columns.Add("Notes", typeof(String));
				chequeManager.DataTable.Columns.Add("Status", typeof(String));

				chequeManager.DataGridFooter = new DataGridFooter();
				chequeManager.dataGrid.IFooter = chequeManager.DataGridFooter;
				chequeManager.grid_footer.Children.Add(chequeManager.DataGridFooter);
				chequeManager.dataGrid.DataContext = chequeManager.DataTable.DefaultView;

				chequeManager.Pagination = new Pagination();
				chequeManager.Pagination.Filter = chequeManager;
				chequeManager.grid_pagination.Children.Add(chequeManager.Pagination);
				setRowsCount();
			} catch(Exception) {
			}
		}

		private BuyingCheque getBuyingChequeForFilter() {
			BuyingCheque buyingCheque = new BuyingCheque();
			try {
				if(chequeManager.datePicker_issuedDate_filter.SelectedDate != null) {
					buyingCheque.IssuedDate = Convert.ToDateTime(chequeManager.datePicker_issuedDate_filter.SelectedDate);
				}
				if(chequeManager.datePicker_payable_filter.SelectedDate != null) {
					buyingCheque.PayableDate = Convert.ToDateTime(chequeManager.datePicker_payable_filter.SelectedDate);
				}
				buyingCheque.Amount = chequeManager.textBox_amount_filter.DoubleValue > 0 ? chequeManager.textBox_amount_filter.DoubleValue : -1;
				buyingCheque.ChequeNumber = chequeManager.textBox_chequeNumber_filter.Text + "%";
				buyingCheque.BankId = Convert.ToInt32(chequeManager.comboBox_bank_filter.SelectedValue);
				buyingCheque.Notes = "%" + chequeManager.textBox_notes_filter.Text + "%";
				buyingCheque.Status = Convert.ToInt32(chequeManager.comboBox_status_filter.SelectedValue);
			} catch(Exception) {
			}
			return buyingCheque;
		}

		private SellingCheque getSellingChequeForFilter() {
			SellingCheque sellingCheque = new SellingCheque();
			try {
				if(chequeManager.datePicker_issuedDate_filter.SelectedDate != null) {
					sellingCheque.IssuedDate = Convert.ToDateTime(chequeManager.datePicker_issuedDate_filter.SelectedDate);
				}
				if(chequeManager.datePicker_payable_filter.SelectedDate != null) {
					sellingCheque.PayableDate = Convert.ToDateTime(chequeManager.datePicker_payable_filter.SelectedDate);
				}
				sellingCheque.Amount = chequeManager.textBox_amount_filter.DoubleValue > 0 ? chequeManager.textBox_amount_filter.DoubleValue : -1;
				sellingCheque.ChequeNumber = chequeManager.textBox_chequeNumber_filter.Text + "%";
				sellingCheque.BankId = Convert.ToInt32(chequeManager.comboBox_bank_filter.SelectedValue);
				sellingCheque.Notes = "%" + chequeManager.textBox_notes_filter.Text + "%";
				sellingCheque.Status = Convert.ToInt32(chequeManager.comboBox_status_filter.SelectedValue);
			} catch(Exception) {
			}
			return sellingCheque;
		}

		internal void filter() {
			try {
				if(chequeManager.radioButton_buying_filter.IsChecked == true) {
					BuyingCheque bc = getBuyingChequeForFilter();
					bc.LimitStart = chequeManager.Pagination.LimitStart;
					bc.LimitEnd = chequeManager.Pagination.LimitCount;
					List<BuyingCheque> list = paymentManagerImpl.getBuyingCheque(bc);
					chequeManager.DataTable.Rows.Clear();
					foreach(BuyingCheque buyingCheque in list) {
						chequeManager.DataTable.Rows.Add(
							buyingCheque.Id, bankManagerImpl.getBankById(buyingCheque.BankId).Name, buyingCheque.ChequeNumber,
							buyingCheque.IssuedDate.ToString("yyyy-MM-dd"), buyingCheque.PayableDate.ToString("yyyy-MM-dd"),
							buyingCheque.Amount.ToString("#,##0.00"), buyingCheque.Notes, CommonMethods.getStatusForCheque(buyingCheque.Status)
						);
					}
				} else {
					SellingCheque sc = getSellingChequeForFilter();
					sc.LimitStart = chequeManager.Pagination.LimitStart;
					sc.LimitEnd = chequeManager.Pagination.LimitCount;
					List<SellingCheque> list = paymentManagerImpl.getSellingCheque(sc);
					chequeManager.DataTable.Rows.Clear();
					foreach(SellingCheque sellingCheque in list) {
						chequeManager.DataTable.Rows.Add(
							sellingCheque.Id, bankManagerImpl.getBankById(sellingCheque.BankId).Name, sellingCheque.ChequeNumber,
							sellingCheque.IssuedDate.ToString("yyyy-MM-dd"), sellingCheque.PayableDate.ToString("yyyy-MM-dd"),
							sellingCheque.Amount.ToString("#,##0.00"), sellingCheque.Notes, CommonMethods.getStatusForCheque(sellingCheque.Status)
						);
					}
				}
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				if(chequeManager.radioButton_buying_filter.IsChecked == true) {
					BuyingCheque buyingCheque = getBuyingChequeForFilter();
					buyingCheque.RowsCount = 1;
					buyingCheque.LimitStart = chequeManager.Pagination.LimitStart;
					buyingCheque.LimitEnd = chequeManager.Pagination.LimitCount;
					chequeManager.Pagination.RowsCount = paymentManagerImpl.getBuyingCheque(buyingCheque)[0].RowsCount;
				} else {
					SellingCheque sellingCheque = getSellingChequeForFilter();
					sellingCheque.RowsCount = 1;
					sellingCheque.LimitStart = chequeManager.Pagination.LimitStart;
					sellingCheque.LimitEnd = chequeManager.Pagination.LimitCount;
					chequeManager.Pagination.RowsCount = paymentManagerImpl.getSellingCheque(sellingCheque)[0].RowsCount;
				}
			} catch(Exception) {
			}
		}

		internal void resetUpdateForm() {
			try {
				chequeManager.SelectedBuyingCheque = null;
				chequeManager.SelectedSellingCheque = null;
				chequeManager.datePicker_issuedDate_update.SelectedDate = null;
				chequeManager.datePicker_payableDate_update.SelectedDate = null;
				chequeManager.textBox_amount_update.Clear();
				chequeManager.textBox_chequeNumber_update.Clear();
				chequeManager.comboBox_bank_update.SelectedValue = -1;
				chequeManager.textBox_notes_update.Clear();
				chequeManager.comboBox_status_update.SelectedValue = 0;
			} catch(Exception) {
			}
		}

		internal void populateUpdateForm() {
			try {
				int id = chequeManager.dataGrid.SelectedItemID;
				if(chequeManager.radioButton_buying_filter.IsChecked == true) {
					BuyingCheque buyingCheque = paymentManagerImpl.getBuyingChequeById(id);
					chequeManager.SelectedBuyingCheque = buyingCheque;
					chequeManager.datePicker_issuedDate_update.SelectedDate = buyingCheque.IssuedDate;
					chequeManager.datePicker_payableDate_update.SelectedDate = buyingCheque.PayableDate;
					chequeManager.textBox_amount_update.DoubleValue = buyingCheque.Amount;
					chequeManager.textBox_chequeNumber_update.Text = buyingCheque.ChequeNumber;
					chequeManager.comboBox_bank_update.SelectedValue = buyingCheque.BankId;
					chequeManager.textBox_notes_update.Text = buyingCheque.Notes;
					chequeManager.comboBox_status_update.SelectedValue = buyingCheque.Status;
				} else {
					SellingCheque sellingCheque = paymentManagerImpl.getSellingChequeById(id);
					chequeManager.SelectedSellingCheque = sellingCheque;
					chequeManager.datePicker_issuedDate_update.SelectedDate = sellingCheque.IssuedDate;
					chequeManager.datePicker_payableDate_update.SelectedDate = sellingCheque.PayableDate;
					chequeManager.textBox_amount_update.DoubleValue = sellingCheque.Amount;
					chequeManager.textBox_chequeNumber_update.Text = sellingCheque.ChequeNumber;
					chequeManager.comboBox_bank_update.SelectedValue = sellingCheque.BankId;
					chequeManager.textBox_notes_update.Text = sellingCheque.Notes;
					chequeManager.comboBox_status_update.SelectedValue = sellingCheque.Status;
				}
			} catch(Exception) {
			}
		}

		private bool isValidateUpdateForm(){
			bool b = true;
			try {
				if(Convert.ToInt32(chequeManager.comboBox_status_update.SelectedValue) < 0) {
					chequeManager.comboBox_status_update.ErrorMode(true);
					b = false;
				}
				if(Convert.ToInt32(chequeManager.comboBox_bank_update.SelectedValue) < 1) {
					chequeManager.comboBox_bank_update.ErrorMode(true);
					b = false;
				}
				if(chequeManager.textBox_chequeNumber_update.IsNull()) {
					chequeManager.textBox_chequeNumber_update.ErrorMode(true);
					b = false;
				}
				if(chequeManager.textBox_amount_update.DoubleValue <= 0) {
					chequeManager.textBox_amount_update.ErrorMode(true);
					b = false;
				}
				if(chequeManager.datePicker_payableDate_update.SelectedDate == null) {
					chequeManager.datePicker_payableDate_update.ErrorMode(true);
					b = false;
				}
				if(chequeManager.datePicker_issuedDate_update.SelectedDate == null) {
					chequeManager.datePicker_issuedDate_update.ErrorMode(true);
					b = false;
				}
			} catch(Exception) {
			}
			return b;
		}

		internal bool updateCheque() {
			bool b = false;
			try {
				if(isValidateUpdateForm()) {
					if(chequeManager.radioButton_buying_filter.IsChecked == true) {
						BuyingCheque buyingCheque = chequeManager.SelectedBuyingCheque;
						buyingCheque.IssuedDate = Convert.ToDateTime(chequeManager.datePicker_issuedDate_update.SelectedDate);
						buyingCheque.PayableDate = Convert.ToDateTime(chequeManager.datePicker_payableDate_update.SelectedDate);
						buyingCheque.Amount = chequeManager.textBox_amount_update.DoubleValue;
						buyingCheque.ChequeNumber = chequeManager.textBox_chequeNumber_update.Text;
						buyingCheque.BankId = Convert.ToInt32(chequeManager.comboBox_bank_update.SelectedValue);
						buyingCheque.Notes = chequeManager.textBox_notes_update.Text;
						buyingCheque.Status = Convert.ToInt32(chequeManager.comboBox_status_update.SelectedValue);
						CommonMethods.setCDMDForUpdate(buyingCheque);
						paymentManagerImpl.updBuyingCheque(buyingCheque);
						b = true;
					} else {
						SellingCheque sellingCheque = chequeManager.SelectedSellingCheque;
						sellingCheque.IssuedDate = Convert.ToDateTime(chequeManager.datePicker_issuedDate_update.SelectedDate);
						sellingCheque.PayableDate = Convert.ToDateTime(chequeManager.datePicker_payableDate_update.SelectedDate);
						sellingCheque.Amount = chequeManager.textBox_amount_update.DoubleValue;
						sellingCheque.ChequeNumber = chequeManager.textBox_chequeNumber_update.Text;
						sellingCheque.BankId = Convert.ToInt32(chequeManager.comboBox_bank_update.SelectedValue);
						sellingCheque.Notes = chequeManager.textBox_notes_update.Text;
						sellingCheque.Status = Convert.ToInt32(chequeManager.comboBox_status_update.SelectedValue);
						CommonMethods.setCDMDForUpdate(sellingCheque);
						paymentManagerImpl.updSellingCheque(sellingCheque);
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}
	}
}
