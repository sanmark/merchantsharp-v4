using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class PaymentManagerImpl {

		private PaymentSection paymentSection;
		private IDao buyingCashDao = null;
		private IDao buyingChequeDao = null;
		private IDao buyingOtherDao = null;
		private BuyingInvoiceManagerImpl buyingInvoiceManagerImpl = null;
		private VendorManagerImpl vendorManagerImpl = null;
		private SellingInvoiceManagerImpl sellingInvoiceManagerImpl = null;
		private CustomerManagerImpl customerManagerImpl = null;

		private IDao sellingCashDao = null;
		private IDao sellingChequeDao = null;
		private IDao sellingOtherDao = null;
		private AddSellingInvoicePayment addSellingInvoicePayment;

		public PaymentManagerImpl() {
			buyingCashDao = BuyingCashDao.getInstance();
			buyingChequeDao = BuyingChequeDao.getInstance();
			buyingOtherDao = BuyingOtherDao.getInstance();

			sellingCashDao = SellingCashDao.getInstance();
			sellingChequeDao = SellingChequeDao.getInstance();
			sellingOtherDao = SellingOtherDao.getInstance();
			buyingInvoiceManagerImpl = new BuyingInvoiceManagerImpl();
		}

		public PaymentManagerImpl(PaymentSection paymentSection) {
			buyingCashDao = BuyingCashDao.getInstance();
			buyingChequeDao = BuyingChequeDao.getInstance();
			buyingOtherDao = BuyingOtherDao.getInstance();

			sellingCashDao = SellingCashDao.getInstance();
			sellingChequeDao = SellingChequeDao.getInstance();
			sellingOtherDao = SellingOtherDao.getInstance();
			this.paymentSection = paymentSection;
			enableAcccessElements();
			buyingInvoiceManagerImpl = new BuyingInvoiceManagerImpl();
		}

		public PaymentManagerImpl(AddSellingInvoicePayment addSellingInvoicePayment) {
			this.addSellingInvoicePayment = addSellingInvoicePayment;
			sellingInvoiceManagerImpl = new SellingInvoiceManagerImpl();			

			sellingCashDao = SellingCashDao.getInstance();
			sellingChequeDao = SellingChequeDao.getInstance();
			sellingOtherDao = SellingOtherDao.getInstance();
		}

		public int addBuyingCash(Entity entity) {
			if(Session.Permission["canAddBuyingCash"] == 1) {
				return buyingCashDao.add(entity);
			} else {
				ShowMessage.error(Common.Messages.Error.Error010);
				return 0;
			}
		}

		public bool delBuyingCash(Entity entity) {
			if(Session.Permission["canDeleteBuyingCash"] == 1) {
				return buyingCashDao.del(entity);
			} else {
				ShowMessage.error(Common.Messages.Error.Error010);
				return false;
			}
		}

		public List<BuyingCash> getBuyingCash(Entity entity) {
			return buyingCashDao.get(entity).Cast<BuyingCash>().ToList();
		}

		public int updBuyingCash(Entity entity) {
			return buyingCashDao.upd(entity);
		}

		///

		public int addBuyingCheque(Entity entity) {
			if(Session.Permission["canAddBuyingCheque"] == 1) {
				return buyingChequeDao.add(entity);
			} else {
				ShowMessage.error(Common.Messages.Error.Error010);
				return 0;
			}
		}

		public bool delBuyingCheque(Entity entity) {
			if(Session.Permission["canDeleteBuyingCheque"] == 1) {
				return buyingChequeDao.del(entity);
			} else {
				ShowMessage.error(Common.Messages.Error.Error010);
				return false;
			}
		}

		public List<BuyingCheque> getBuyingCheque(Entity entity) {
			return buyingChequeDao.get(entity).Cast<BuyingCheque>().ToList();
		}

		public int updBuyingCheque(Entity entity) {
			return buyingChequeDao.upd(entity);
		}

		/// //////////////////////////

		public int addBuyingOther(Entity entity) {
			if(Session.Permission["canAddBuyingOther"] == 1) {
				return buyingOtherDao.add(entity);
			} else {
				ShowMessage.error(Common.Messages.Error.Error010);
				return 0;
			}
		}

		public bool delBuyingOther(Entity entity) {
			if(Session.Permission["canDeleteBuyingOther"] == 1) {
				return buyingOtherDao.del(entity);
			} else {
				ShowMessage.error(Common.Messages.Error.Error010);
				return false;
			}
		}

		public List<BuyingOther> getBuyingOther(Entity entity) {
			return buyingOtherDao.get(entity).Cast<BuyingOther>().ToList();
		}

		public int updBuyingOther(Entity entity) {
			return buyingOtherDao.upd(entity);
		}

		////////////////////////////////////////////////////////////////////


		public int addSellingCash(Entity entity) {
			if(Session.Permission["canAddSellingCash"] == 1) {
				return sellingCashDao.add(entity);
			} else {
				ShowMessage.error(Common.Messages.Error.Error010);
				return 0;
			}
		}

		public bool delSellingCash(Entity entity) {
			if(Session.Permission["canDeleteSellingCash"] == 1) {
				return sellingCashDao.del(entity);
			} else {
				ShowMessage.error(Common.Messages.Error.Error010);
				return false;
			}
		}

		public List<SellingCash> getSellingCash(Entity entity) {
			return sellingCashDao.get(entity).Cast<SellingCash>().ToList();
		}

		public int updSellingCash(Entity entity) {
			return sellingCashDao.upd(entity);
		}

		///

		public int addSellingCheque(Entity entity) {
			if(Session.Permission["canAddSellingCheque"] == 1) {
				return sellingChequeDao.add(entity);
			} else {
				ShowMessage.error(Common.Messages.Error.Error010);
				return 0;
			}
		}

		public bool delSellingCheque(Entity entity) {
			if(Session.Permission["canDeleteSellingCheque"] == 1) {
				return sellingChequeDao.del(entity);
			} else {
				ShowMessage.error(Common.Messages.Error.Error010);
				return false;
			}
		}

		public List<SellingCheque> getSellingCheque(Entity entity) {
			return sellingChequeDao.get(entity).Cast<SellingCheque>().ToList();
		}

		public int updSellingCheque(Entity entity) {
			return sellingChequeDao.upd(entity);
		}

		/// //////////////////////////

		public int addSellingOther(Entity entity) {
			if(Session.Permission["canAddSellingOther"] == 1) {
				return sellingOtherDao.add(entity);
			} else {
				ShowMessage.error(Common.Messages.Error.Error010);
				return 0;
			}
		}

		public bool delSellingOther(Entity entity) {
			if(Session.Permission["canDeleteSellingOther"] == 1) {
				return sellingOtherDao.del(entity);
			} else {
				ShowMessage.error(Common.Messages.Error.Error010);
				return false;
			}
		}

		public List<SellingOther> getSellingOther(Entity entity) {
			return sellingOtherDao.get(entity).Cast<SellingOther>().ToList();
		}

		public int updSellingOther(Entity entity) {
			return sellingOtherDao.upd(entity);
		}

		//////////////////////////////////////////////////////////////////


		public double getBuyingCashAmountForInvoice(int id) {
			double d = 0;
			try {
				BuyingCash cash = new BuyingCash();
				cash.BuyingInvoiceId = id;
				List<BuyingCash> cashs = getBuyingCash(cash);
				foreach(BuyingCash buyingCash in cashs) {
					d += buyingCash.Amount;
				}
			} catch(Exception) {
			}
			return d;
		}

		public double getBuyingChequeAmountForInvoice(int id) {
			double d = 0;
			try {
				BuyingCheque cheque = new BuyingCheque();
				cheque.BuyingInvoiceId = id;
				List<BuyingCheque> cheques = getBuyingCheque(cheque);
				foreach(BuyingCheque buyingCheque in cheques) {
					d += buyingCheque.Amount;
				}
			} catch(Exception) {
			}
			return d;
		}

		public double getBuyingOtherAmountForInvoice(int id) {
			double d = 0;
			try {
				BuyingOther other = new BuyingOther();
				other.BuyingInvoiceId = id;
				List<BuyingOther> others = getBuyingOther(other);
				foreach(BuyingOther buyingOther in others) {
					d += buyingOther.Amount;
				}
			} catch(Exception) {
			}
			return d;
		}

		public double getAllBuyingPaidAmountForInvoice(int id) {
			double d = 0;
			try {
				d += getBuyingCashAmountForInvoice(id);
				d += getBuyingChequeAmountForInvoice(id);
				d += getBuyingOtherAmountForInvoice(id);
				BuyingInvoice invoice = buyingInvoiceManagerImpl.getInvoiceById(id);
				//d += invoice.LaterDiscount;
				if(invoice.VendorAccountBalanceChange > 0) {
					d += invoice.VendorAccountBalanceChange;
				}
			} catch(Exception) {
			}
			return d;
		}

		public BuyingCash getBuyingCashById(int id) {
			BuyingCash buyingCash = null;
			try {
				BuyingCash cash = new BuyingCash();
				cash.Id = id;
				buyingCash = getBuyingCash(cash)[0];
			} catch(Exception) {
			}
			return buyingCash;
		}

		public BuyingCheque getBuyingChequeById(int id) {
			BuyingCheque buyingCheque = null;
			try {
				BuyingCheque cheque = new BuyingCheque();
				cheque.Id = id;
				buyingCheque = getBuyingCheque(cheque)[0];
			} catch(Exception) {
			}
			return buyingCheque;
		}

		public BuyingOther getBuyingOtherById(int id) {
			BuyingOther buyingOther = null;
			try {
				BuyingOther other = new BuyingOther();
				other.Id = id;
				buyingOther = getBuyingOther(other)[0];
			} catch(Exception) {
			}
			return buyingOther;
		}

		/* ******************************************************************************************* */

		public double getSellingCashAmountForInvoice(int id) {
			double d = 0;
			try {
				SellingCash cash = new SellingCash();
				cash.SellingInvoiceId = id;
				List<SellingCash> cashs = getSellingCash(cash);
				foreach(SellingCash sellingCash in cashs) {
					d += sellingCash.Amount;
				}
			} catch(Exception) {
			}
			return d;
		}

		public double getSellingChequeAmountForInvoice(int id) {
			double d = 0;
			try {
				SellingCheque cheque = new SellingCheque();
				cheque.SellingInvoiceId = id;
				List<SellingCheque> cheques = getSellingCheque(cheque);
				foreach(SellingCheque sellingCheque in cheques) {
					d += sellingCheque.Amount;
				}
			} catch(Exception) {
			}
			return d;
		}

		public double getSellingOtherAmountForInvoice(int id) {
			double d = 0;
			try {
				SellingOther other = new SellingOther();
				other.SellingInvoiceId = id;
				List<SellingOther> others = getSellingOther(other);
				foreach(SellingOther sellingOther in others) {
					d += sellingOther.Amount;
				}
			} catch(Exception) {
			}
			return d;
		}

		public double getAllSellingPaidAmountForInvoice(int id) {
			double d = 0;
			try {
				d += getSellingCashAmountForInvoice(id);
				d += getSellingChequeAmountForInvoice(id);
				d += getSellingOtherAmountForInvoice(id);
				SellingInvoice invoice = sellingInvoiceManagerImpl.getInvoiceById(id);
				//d += invoice.LaterDiscount;
				if(invoice.CustomerAccountBalanceChange > 0) {
					d += invoice.CustomerAccountBalanceChange;
				}
			} catch(Exception) {
			}
			return d;
		}

		public SellingCash getSellingCashById(int id) {
			SellingCash sellingCash = null;
			try {
				SellingCash cash = new SellingCash();
				cash.Id = id;
				sellingCash = getSellingCash(cash)[0];
			} catch(Exception) {
			}
			return sellingCash;
		}

		public SellingCheque getSellingChequeById(int id) {
			SellingCheque sellingCheque = null;
			try {
				SellingCheque cheque = new SellingCheque();
				cheque.Id = id;
				sellingCheque = getSellingCheque(cheque)[0];
			} catch(Exception) {
			}
			return sellingCheque;
		}

		public SellingOther getSellingOtherById(int id) {
			SellingOther sellingOther = null;
			try {
				SellingOther other = new SellingOther();
				other.Id = id;
				sellingOther = getSellingOther(other)[0];
			} catch(Exception) {
			}
			return sellingOther;
		}



		////////////////////////////////////////////////////////////////////////////////////////

		internal void UserControl_Loaded() {
			try {
				UIComboBox.banksForSelect(paymentSection.comboBox_bank_chequePayments);
				paymentSection.DataTableCashPayments = new DataTable();
				paymentSection.DataTableCashPayments.Columns.Add("ID", typeof(int));
				paymentSection.DataTableCashPayments.Columns.Add("Date", typeof(String));
				paymentSection.DataTableCashPayments.Columns.Add("Amount", typeof(String));
				paymentSection.dataGrid_cashPayments_cashPayments.DataContext = paymentSection.DataTableCashPayments.DefaultView;

				paymentSection.DataTableChequePayments = new DataTable();
				paymentSection.DataTableChequePayments.Columns.Add("ID", typeof(int));
				paymentSection.DataTableChequePayments.Columns.Add("Bank", typeof(String));
				paymentSection.DataTableChequePayments.Columns.Add("Cheque Number", typeof(String));
				paymentSection.DataTableChequePayments.Columns.Add("Issued Date", typeof(String));
				paymentSection.DataTableChequePayments.Columns.Add("Payble Date", typeof(String));
				paymentSection.DataTableChequePayments.Columns.Add("Amount", typeof(String));
				paymentSection.DataTableChequePayments.Columns.Add("Notes", typeof(String));
				paymentSection.DataTableChequePayments.Columns.Add("Status", typeof(String));
				paymentSection.dataGrid_chequePayments_chequePayments.DataContext = paymentSection.DataTableChequePayments.DefaultView;

				paymentSection.DataTableOtherPayments = new DataTable();
				paymentSection.DataTableOtherPayments.Columns.Add("ID", typeof(int));
				paymentSection.DataTableOtherPayments.Columns.Add("Date", typeof(String));
				paymentSection.DataTableOtherPayments.Columns.Add("Amount", typeof(String));
				paymentSection.DataTableOtherPayments.Columns.Add("Notes", typeof(String));
				paymentSection.dataGrid_otherPayments_otherPayments.DataContext = paymentSection.DataTableOtherPayments.DefaultView;

			} catch(Exception) {
			}
		}

		private void enableAcccessElements() {
			try {
				if(paymentSection.Type == "BuyingInvoice") {
					int position = 1;
					if(Session.Meta["isActiveVendorAccountBalance"] == 0) {
						paymentSection.groupBox_accountBalacePayment.Visibility = System.Windows.Visibility.Hidden;
					} else {
						position++;
						paymentSection.groupBox_accountBalacePayment.SetValue(Grid.ColumnProperty, position);
					}
					if(Session.Meta["isActiveBuyingOtherPayments"] == 0) {
						paymentSection.groupBox_otherPayments.Visibility = System.Windows.Visibility.Hidden;
					} else {
						position++;
						paymentSection.groupBox_otherPayments.SetValue(Grid.ColumnProperty, position);
					}
					buyingInvoiceManagerImpl = new BuyingInvoiceManagerImpl();
					vendorManagerImpl = new VendorManagerImpl();
				} else if(paymentSection.Type == "SellingInvoice") {
					int position = 1;
					if(Session.Meta["isActiveCustomerAccountBalance"] == 0) {
						paymentSection.groupBox_accountBalacePayment.Visibility = System.Windows.Visibility.Hidden;
					} else {
						position++;
						paymentSection.groupBox_accountBalacePayment.SetValue(Grid.ColumnProperty, position);
					}
					if(Session.Meta["isActiveSellingOtherPayments"] == 0) {
						paymentSection.groupBox_otherPayments.Visibility = System.Windows.Visibility.Hidden;
					} else {
						position++;
						paymentSection.groupBox_otherPayments.SetValue(Grid.ColumnProperty, position);
					}
					sellingInvoiceManagerImpl = new SellingInvoiceManagerImpl();
					customerManagerImpl = new CustomerManagerImpl();
				}
			} catch(Exception) {
			}
		}

		public void loadAllCashPayments() {
			try {
				if(paymentSection.Type == "BuyingInvoice") {
					BuyingCash cash = new BuyingCash();
					cash.BuyingInvoiceId = paymentSection.InvoiceId;
					List<BuyingCash> cashList = getBuyingCash(cash);
					paymentSection.DataTableCashPayments.Rows.Clear();
					foreach(BuyingCash buyingCash in cashList) {
						DataRow row = paymentSection.DataTableCashPayments.NewRow();
						row[0] = buyingCash.Id;
						row[1] = buyingCash.Date.ToString("yyyy-MM-dd");
						row[2] = buyingCash.Amount.ToString("#,##0.00");
						paymentSection.DataTableCashPayments.Rows.Add(row);
					}
				} else if(paymentSection.Type == "SellingInvoice") {
					SellingCash cash = new SellingCash();
					cash.SellingInvoiceId = paymentSection.InvoiceId;
					List<SellingCash> cashList = getSellingCash(cash);
					paymentSection.DataTableCashPayments.Rows.Clear();
					foreach(SellingCash sellingCash in cashList) {
						DataRow row = paymentSection.DataTableCashPayments.NewRow();
						row[0] = sellingCash.Id;
						row[1] = sellingCash.Date.ToString("yyyy-MM-dd");
						row[2] = sellingCash.Amount.ToString("#,##0.00");
						paymentSection.DataTableCashPayments.Rows.Add(row);
					}
				}
			} catch(Exception) {
			}
		}

		public void loadAllChequePayments() {
			try {
				if(paymentSection.Type == "BuyingInvoice") {
					BuyingCheque cheque = new BuyingCheque();
					cheque.BuyingInvoiceId = paymentSection.InvoiceId;
					List<BuyingCheque> chequeList = getBuyingCheque(cheque);
					paymentSection.DataTableChequePayments.Rows.Clear();
					foreach(BuyingCheque buyingCheque in chequeList) {
						DataRow row = paymentSection.DataTableChequePayments.NewRow();
						row[0] = buyingCheque.Id;
						row[1] = buyingCheque.BankId.ToString();
						row[2] = buyingCheque.ChequeNumber;
						row[3] = buyingCheque.IssuedDate.ToString("yyyy-MM-dd");
						row[4] = buyingCheque.PayableDate.ToString("yyyy-MM-dd");
						row[5] = buyingCheque.Amount.ToString("#,##0.00");
						row[6] = buyingCheque.Notes;
						row[7] = CommonMethods.getStatusForCheque(buyingCheque.Status);
						paymentSection.DataTableChequePayments.Rows.Add(row);
					}
				} else if(paymentSection.Type == "SellingInvoice") {
					SellingCheque cheque = new SellingCheque();
					cheque.SellingInvoiceId = paymentSection.InvoiceId;
					List<SellingCheque> chequeList = getSellingCheque(cheque);
					paymentSection.DataTableChequePayments.Rows.Clear();
					foreach(SellingCheque sellingCheque in chequeList) {
						DataRow row = paymentSection.DataTableChequePayments.NewRow();
						row[0] = sellingCheque.Id;
						row[1] = sellingCheque.BankId.ToString();
						row[2] = sellingCheque.ChequeNumber;
						row[3] = sellingCheque.IssuedDate.ToString("yyyy-MM-dd");
						row[4] = sellingCheque.PayableDate.ToString("yyyy-MM-dd");
						row[5] = sellingCheque.Amount.ToString("#,##0.00");
						row[6] = sellingCheque.Notes;
						row[7] = CommonMethods.getStatusForCheque(sellingCheque.Status);
						paymentSection.DataTableChequePayments.Rows.Add(row);
					}
				}
			} catch(Exception) {
			}
		}

		internal void loadAllOtherPayments() {
			try {
				if(paymentSection.Type == "BuyingInvoice") {
					BuyingOther other = new BuyingOther();
					other.BuyingInvoiceId = paymentSection.InvoiceId;
					List<BuyingOther> otherList = getBuyingOther(other);
					paymentSection.DataTableOtherPayments.Rows.Clear();
					foreach(BuyingOther buyingOther in otherList) {
						DataRow row = paymentSection.DataTableOtherPayments.NewRow();
						row[0] = buyingOther.Id;
						row[1] = buyingOther.Date.ToString("yyyy-MM-dd");
						row[2] = buyingOther.Amount;
						row[3] = buyingOther.Notes;
						paymentSection.DataTableOtherPayments.Rows.Add(row);
					}
				} else if(paymentSection.Type == "SellingInvoice") {
					SellingOther other = new SellingOther();
					other.SellingInvoiceId = paymentSection.InvoiceId;
					List<SellingOther> otherList = getSellingOther(other);
					paymentSection.DataTableOtherPayments.Rows.Clear();
					foreach(SellingOther sellingOther in otherList) {
						DataRow row = paymentSection.DataTableOtherPayments.NewRow();
						row[0] = sellingOther.Id;
						row[1] = sellingOther.Date.ToString("yyyy-MM-dd");
						row[2] = sellingOther.Amount;
						row[3] = sellingOther.Notes;
						paymentSection.DataTableOtherPayments.Rows.Add(row);
					}
				}
			} catch(Exception) {
			}
		}

		internal void loadedUI() {
			try {
				loadAllCashPayments();
				loadAllChequePayments();
				loadAllOtherPayments();
				BuyingInvoice invoice = buyingInvoiceManagerImpl.getInvoiceById(paymentSection.InvoiceId);
				if(invoice.Status == 3 || invoice.Status == 2) {
					paymentSection.mainGrid.IsEnabled = false;
				}
			} catch(Exception) {
			}
		}

		internal void unloadAllItems() {
			try {
				paymentSection.DataTableCashPayments.Rows.Clear();
				paymentSection.DataTableChequePayments.Rows.Clear();
				clearCashSection();
				clearChequeSection();
				paymentSection.label_balance_vendorAccountSettlement.Content = "0.00";
				paymentSection.textBox_amount_vendorAccountSettlement.Clear();
			} catch(Exception) {
			}
		}

		public void clearCashSection() {
			try {
				paymentSection.datepicker_date_cashPayments.SelectedDate = DateTime.Today;
				paymentSection.textBox_amount_cashPayments.Clear();
			} catch(Exception) {
			}
		}

		internal bool addCashPayment() {
			bool b = false;
			try {
				bool isOkay = true;
				if(paymentSection.datepicker_date_cashPayments.SelectedDate == null) {
					paymentSection.datepicker_date_cashPayments.ErrorMode(true);
					isOkay = false;
				}
				if(paymentSection.textBox_amount_cashPayments.DoubleValue <= 0) {
					paymentSection.textBox_amount_cashPayments.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					if(paymentSection.Type == "BuyingInvoice") {
						BuyingCash buyingCash = new BuyingCash();
						buyingCash.BuyingInvoiceId = paymentSection.InvoiceId;
						buyingCash.Date = paymentSection.datepicker_date_cashPayments.SelectedValue;
						buyingCash.Amount = paymentSection.textBox_amount_cashPayments.DoubleValue;
						buyingCash.Notes = "";
						if(Session.Meta["isActiveVendorAccountBalance"] == 1) {
							double payedAmount = getAllBuyingPaidAmountForInvoice(paymentSection.InvoiceId);
							double netTotal = buyingInvoiceManagerImpl.getNetTotalByInvoiceId(paymentSection.InvoiceId);
							if((payedAmount - netTotal) > 0) {
								buyingCash.AccountTransfer = buyingCash.Amount;
							} else if((payedAmount + buyingCash.Amount) > netTotal) {
								buyingCash.AccountTransfer = (payedAmount + buyingCash.Amount) - netTotal;
							} else {
								buyingCash.AccountTransfer = 0;
							}

							if(buyingCash.AccountTransfer > 0) {
								vendorManagerImpl.additionAccountBalanceById(buyingInvoiceManagerImpl.getVendorIdByInvoiceId(buyingCash.BuyingInvoiceId), buyingCash.AccountTransfer);
							}
						} else {
							buyingCash.AccountTransfer = 0;
						}
						CommonMethods.setCDMDForAdd(buyingCash);
						if(addBuyingCash(buyingCash) > 0) {
							b = true;
						}
					} else if(paymentSection.Type == "SellingInvoice") {
						SellingCash sellingCash = new SellingCash();
						sellingCash.SellingInvoiceId = paymentSection.InvoiceId;
						sellingCash.Date = paymentSection.datepicker_date_cashPayments.SelectedValue;
						sellingCash.Amount = paymentSection.textBox_amount_cashPayments.DoubleValue;
						sellingCash.Notes = "";
						if(Session.Meta["isActiveCustomerAccountBalance"] == 1) {
							double payedAmount = getAllBuyingPaidAmountForInvoice(paymentSection.InvoiceId);
							double netTotal = sellingInvoiceManagerImpl.getNetTotalByInvoiceId(paymentSection.InvoiceId);
							if((payedAmount - netTotal) > 0) {
								sellingCash.AccountTransfer = sellingCash.Amount;
							} else if((payedAmount + sellingCash.Amount) > netTotal) {
								sellingCash.AccountTransfer = (payedAmount + sellingCash.Amount) - netTotal;
							} else {
								sellingCash.AccountTransfer = 0;
							}

							if(sellingCash.AccountTransfer > 0) {
								customerManagerImpl.additionAccountBalanceById(sellingInvoiceManagerImpl.getCustomerIdByInvoiceId(sellingCash.SellingInvoiceId), sellingCash.AccountTransfer);
							}
						} else {
							sellingCash.AccountTransfer = 0;
						}
						CommonMethods.setCDMDForAdd(sellingCash);
						if(addSellingCash(sellingCash) > 0) {
							b = true;
						}
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal bool deleteCashPayment() {
			bool b = false;
			try {
				if(paymentSection.Type == "BuyingInvoice") {
					BuyingCash cash = getBuyingCashById(paymentSection.dataGrid_cashPayments_cashPayments.SelectedItemID);
					if(cash.AccountTransfer > 0) {
						vendorManagerImpl.substractAccountBalanceById(buyingInvoiceManagerImpl.getVendorIdByInvoiceId(cash.BuyingInvoiceId), cash.AccountTransfer);
					}
					b = delBuyingCash(cash);
				} else if(paymentSection.Type == "SellingInvoice") {
					SellingCash cash = getSellingCashById(paymentSection.dataGrid_cashPayments_cashPayments.SelectedItemID);
					if(cash.AccountTransfer > 0) {
						customerManagerImpl.substractAccountBalanceById(buyingInvoiceManagerImpl.getVendorIdByInvoiceId(cash.SellingInvoiceId), cash.AccountTransfer);
					}
					b = delSellingCash(cash);
				}
			} catch(Exception) {
			}
			return b;
		}

		internal bool addChequePayment() {
			bool b = false;
			try {
				bool isOkay = true;
				if(paymentSection.comboBox_bank_chequePayments.Value < 1) {
					paymentSection.comboBox_bank_chequePayments.ErrorMode(true);
					isOkay = false;
				}
				if(paymentSection.textBox_chequeNumber_chequePayments.IsNull()) {
					paymentSection.textBox_chequeNumber_chequePayments.ErrorMode(true);
					isOkay = false;
				}
				if(paymentSection.textBox_amount_chequePayments.DoubleValue <= 0) {
					paymentSection.textBox_amount_chequePayments.ErrorMode(true);
					isOkay = false;
				}
				if(paymentSection.datePicker_payableDate_chequePayments.SelectedDate == null) {
					paymentSection.datePicker_payableDate_chequePayments.ErrorMode(true);
					isOkay = false;
				}
				if(paymentSection.datePicker_issuedDate_chequePayments.SelectedDate == null) {
					paymentSection.datePicker_issuedDate_chequePayments.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					if(paymentSection.Type == "BuyingInvoice") {
						BuyingCheque buyingCheque = new BuyingCheque();
						buyingCheque.BuyingInvoiceId = paymentSection.InvoiceId;
						buyingCheque.IssuedDate = paymentSection.datePicker_issuedDate_chequePayments.SelectedValue;
						buyingCheque.PayableDate = paymentSection.datePicker_payableDate_chequePayments.SelectedValue;
						buyingCheque.Amount = paymentSection.textBox_amount_chequePayments.DoubleValue;
						buyingCheque.ChequeNumber = paymentSection.textBox_chequeNumber_chequePayments.TrimedText;
						buyingCheque.BankId = paymentSection.comboBox_bank_chequePayments.Value;
						buyingCheque.Notes = "";
						if(Session.Meta["isActiveVendorAccountBalance"] == 1) {
							double payedAmount = getAllBuyingPaidAmountForInvoice(paymentSection.InvoiceId);
							double netTotal = buyingInvoiceManagerImpl.getNetTotalByInvoiceId(paymentSection.InvoiceId);
							if((payedAmount - netTotal) > 0) {
								buyingCheque.AccountTransfer = buyingCheque.Amount;
							} else if((payedAmount + buyingCheque.Amount) > netTotal) {
								buyingCheque.AccountTransfer = (payedAmount + buyingCheque.Amount) - netTotal;
							} else {
								buyingCheque.AccountTransfer = 0;
							}

							if(buyingCheque.AccountTransfer > 0) {
								vendorManagerImpl.additionAccountBalanceById(buyingInvoiceManagerImpl.getVendorIdByInvoiceId(buyingCheque.BuyingInvoiceId), buyingCheque.AccountTransfer);
							}
						} else {
							buyingCheque.AccountTransfer = 0;
						}
						buyingCheque.Status = 0;
						CommonMethods.setCDMDForAdd(buyingCheque);
						if(addBuyingCheque(buyingCheque) > 0) {
							b = true;
						}
					} else if(paymentSection.Type == "SellingInvoice") {
						SellingCheque sellingCheque = new SellingCheque();
						sellingCheque.SellingInvoiceId = paymentSection.InvoiceId;
						sellingCheque.IssuedDate = paymentSection.datePicker_issuedDate_chequePayments.SelectedValue;
						sellingCheque.PayableDate = paymentSection.datePicker_payableDate_chequePayments.SelectedValue;
						sellingCheque.Amount = paymentSection.textBox_amount_chequePayments.DoubleValue;
						sellingCheque.ChequeNumber = paymentSection.textBox_chequeNumber_chequePayments.TrimedText;
						sellingCheque.BankId = paymentSection.comboBox_bank_chequePayments.Value;
						sellingCheque.Notes = "";
						if(Session.Meta["isActiveCustomerAccountBalance"] == 1) {
							double payedAmount = getAllSellingPaidAmountForInvoice(paymentSection.InvoiceId);
							double netTotal = sellingInvoiceManagerImpl.getNetTotalByInvoiceId(paymentSection.InvoiceId);
							if((payedAmount - netTotal) > 0) {
								sellingCheque.AccountTransfer = sellingCheque.Amount;
							} else if((payedAmount + sellingCheque.Amount) > netTotal) {
								sellingCheque.AccountTransfer = (payedAmount + sellingCheque.Amount) - netTotal;
							} else {
								sellingCheque.AccountTransfer = 0;
							}

							if(sellingCheque.AccountTransfer > 0) {
								customerManagerImpl.additionAccountBalanceById(buyingInvoiceManagerImpl.getVendorIdByInvoiceId(sellingCheque.SellingInvoiceId), sellingCheque.AccountTransfer);
							}
						} else {
							sellingCheque.AccountTransfer = 0;
						}
						sellingCheque.Status = 0;
						CommonMethods.setCDMDForAdd(sellingCheque);
						if(addSellingCheque(sellingCheque) > 0) {
							b = true;
						}
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void clearChequeSection() {
			try {
				paymentSection.datePicker_issuedDate_chequePayments.SelectedDate = DateTime.Today;
				paymentSection.datePicker_payableDate_chequePayments.SelectedDate = null;
				paymentSection.textBox_amount_chequePayments.Clear();
				paymentSection.textBox_chequeNumber_chequePayments.Clear();
				paymentSection.comboBox_bank_chequePayments.SelectedIndex = -1;
			} catch(Exception) {
			}
		}

		internal bool deleteChequePayment() {
			bool b = false;
			try {
				if(paymentSection.Type == "BuyingInvoice") {
					BuyingCheque buyingCheque = getBuyingChequeById(paymentSection.dataGrid_chequePayments_chequePayments.SelectedItemID);
					if(buyingCheque.AccountTransfer > 0) {
						vendorManagerImpl.substractAccountBalanceById(buyingInvoiceManagerImpl.getVendorIdByInvoiceId(buyingCheque.BuyingInvoiceId), buyingCheque.AccountTransfer);
					}
					b = delBuyingCheque(buyingCheque);
				} else if(paymentSection.Type == "SellingInvoice") {
					SellingCheque sellingCheque = getSellingChequeById(paymentSection.dataGrid_chequePayments_chequePayments.SelectedItemID);
					if(sellingCheque.AccountTransfer > 0) {
						customerManagerImpl.substractAccountBalanceById(buyingInvoiceManagerImpl.getVendorIdByInvoiceId(sellingCheque.SellingInvoiceId), sellingCheque.AccountTransfer);
					}
					b = delSellingCheque(sellingCheque);
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void saveAccountChange() {
			try {
				if(paymentSection.Type == "BuyingInvoice") {
					BuyingInvoice buyingInvoice = buyingInvoiceManagerImpl.getInvoiceById(paymentSection.InvoiceId);
					if(buyingInvoice.VendorAccountBalanceType == 0) {
						buyingInvoice.VendorAccountBalanceType = 1;
						buyingInvoice.VendorAccountBalanceChange = paymentSection.textBox_amount_vendorAccountSettlement.DoubleValue;
						buyingInvoiceManagerImpl.updInvoice(buyingInvoice);
						Vendor vendor = vendorManagerImpl.getVendorById(buyingInvoice.VendorId);
						vendor.AccountBalance -= paymentSection.textBox_amount_vendorAccountSettlement.DoubleValue;
						vendorManagerImpl.upd(vendor);
					}
				} else if(paymentSection.Type == "SellingInvoice") {
					SellingInvoice sellingInvoice = sellingInvoiceManagerImpl.getInvoiceById(paymentSection.InvoiceId);
					if(sellingInvoice.CustomerAccountBalanceChange == 0) {
						sellingInvoice.CustomerAccountBalanceChange = paymentSection.textBox_amount_vendorAccountSettlement.DoubleValue;
						sellingInvoiceManagerImpl.updInvoice(sellingInvoice);
						Customer customer = customerManagerImpl.getCustomerById(sellingInvoice.CustomerId);
						customer.AccountBalance -= paymentSection.textBox_amount_vendorAccountSettlement.DoubleValue;
						customerManagerImpl.upd(customer);
					}
				}
			} catch(Exception) {
			}
		}

		internal bool addOtherPayment() {
			bool b = false;
			try {
				bool isOkay = true;
				if(paymentSection.calendar_date_otherPayments.SelectedDate == null) {
					paymentSection.calendar_date_otherPayments.ErrorMode(true);
					isOkay = false;
				}
				if(paymentSection.textBox_amount_otherPayments.DoubleValue <= 0) {
					paymentSection.textBox_amount_otherPayments.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					if(paymentSection.Type == "BuyingInvoice") {
						BuyingOther buyingOther = new BuyingOther();
						buyingOther.BuyingInvoiceId = paymentSection.InvoiceId;
						buyingOther.Date = paymentSection.calendar_date_otherPayments.SelectedValue;
						buyingOther.Amount = paymentSection.textBox_amount_otherPayments.DoubleValue;
						buyingOther.Notes = paymentSection.textBox_notes_otherPayments.TrimedText;
						if(Session.Meta["isActiveVendorAccountBalance"] == 1) {
							double payedAmount = getAllBuyingPaidAmountForInvoice(paymentSection.InvoiceId);
							double netTotal = buyingInvoiceManagerImpl.getNetTotalByInvoiceId(paymentSection.InvoiceId);
							if((payedAmount - netTotal) > 0) {
								buyingOther.AccountTransfer = buyingOther.Amount;
							} else if((payedAmount + buyingOther.Amount) > netTotal) {
								buyingOther.AccountTransfer = (payedAmount + buyingOther.Amount) - netTotal;
							} else {
								buyingOther.AccountTransfer = 0;
							}

							if(buyingOther.AccountTransfer > 0) {
								vendorManagerImpl.additionAccountBalanceById(buyingInvoiceManagerImpl.getVendorIdByInvoiceId(buyingOther.BuyingInvoiceId), buyingOther.AccountTransfer);
							}
						} else {
							buyingOther.AccountTransfer = 0;
						}
						CommonMethods.setCDMDForAdd(buyingOther);
						if(addBuyingOther(buyingOther) > 0) {
							b = true;
						}
					} else if(paymentSection.Type == "SellingInvoice") {
						SellingOther sellingOther = new SellingOther();
						sellingOther.SellingInvoiceId = paymentSection.InvoiceId;
						sellingOther.Date = paymentSection.calendar_date_otherPayments.SelectedValue;
						sellingOther.Amount = paymentSection.textBox_amount_otherPayments.DoubleValue;
						sellingOther.Notes = paymentSection.textBox_notes_otherPayments.TrimedText;
						if(Session.Meta["isActiveCustomerAccountBalance"] == 1) {
							double payedAmount = getAllSellingPaidAmountForInvoice(paymentSection.InvoiceId);
							double netTotal = sellingInvoiceManagerImpl.getNetTotalByInvoiceId(paymentSection.InvoiceId);
							if((payedAmount - netTotal) > 0) {
								sellingOther.AccountTransfer = sellingOther.Amount;
							} else if((payedAmount + sellingOther.Amount) > netTotal) {
								sellingOther.AccountTransfer = (payedAmount + sellingOther.Amount) - netTotal;
							} else {
								sellingOther.AccountTransfer = 0;
							}

							if(sellingOther.AccountTransfer > 0) {
								customerManagerImpl.additionAccountBalanceById(sellingInvoiceManagerImpl.getCustomerIdByInvoiceId(sellingOther.SellingInvoiceId), sellingOther.AccountTransfer);
							}
						} else {
							sellingOther.AccountTransfer = 0;
						}
						CommonMethods.setCDMDForAdd(sellingOther);
						if(addSellingOther(sellingOther) > 0) {
							b = true;
						}
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void clearOtherSection() {
			try {
				paymentSection.calendar_date_otherPayments.SelectedDate = DateTime.Today;
				paymentSection.textBox_amount_otherPayments.Clear();
				paymentSection.textBox_notes_otherPayments.Clear();
			} catch(Exception) {
			}
		}

		internal bool deleteOtherPayment() {
			bool b = false;
			try {
				if(paymentSection.Type == "BuyingInvoice") {
					BuyingOther other = getBuyingOtherById(paymentSection.dataGrid_otherPayments_otherPayments.SelectedItemID);
					if(other.AccountTransfer > 0) {
						vendorManagerImpl.substractAccountBalanceById(buyingInvoiceManagerImpl.getVendorIdByInvoiceId(other.BuyingInvoiceId), other.AccountTransfer);
					}
					b = delBuyingOther(other);
				} else if(paymentSection.Type == "SellingInvoice") {
					SellingOther other = getSellingOtherById(paymentSection.dataGrid_otherPayments_otherPayments.SelectedItemID);
					if(other.AccountTransfer > 0) {
						customerManagerImpl.substractAccountBalanceById(sellingInvoiceManagerImpl.getCustomerIdByInvoiceId(other.SellingInvoiceId), other.AccountTransfer);
					}
					b = delSellingOther(other);
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void resetAllElements() {
			try {
				paymentSection.InvoiceId = 0;
				clearCashSection();
				clearChequeSection();
				clearOtherSection();
				paymentSection.label_balance_vendorAccountSettlement.Content = "0.00";
				paymentSection.textBox_amount_vendorAccountSettlement.Clear();
				paymentSection.DataTableCashPayments.Rows.Clear();
				paymentSection.DataTableChequePayments.Rows.Clear();
				paymentSection.DataTableOtherPayments.Rows.Clear();
			} catch(Exception) {
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////////

		internal void addSellingInvoicePaymentUserControlLoaded() {
			try {
				addSellingInvoicePayment.DataTable = new DataTable();
				addSellingInvoicePayment.DataTable.Columns.Add("ID", typeof(int));
				addSellingInvoicePayment.DataTable.Columns.Add("Invoice #", typeof(String));
				addSellingInvoicePayment.DataTable.Columns.Add("Date", typeof(String));
				addSellingInvoicePayment.DataTable.Columns.Add("Net Total", typeof(String));
				addSellingInvoicePayment.DataTable.Columns.Add("Paid Amount", typeof(String));
				addSellingInvoicePayment.DataTable.Columns.Add("Remaining Amount", typeof(String));

				addSellingInvoicePayment.DataGridFooter = new DataGridFooter();
				addSellingInvoicePayment.dataGrid_stockItems.IFooter = addSellingInvoicePayment.DataGridFooter;
				addSellingInvoicePayment.grid_footer.Children.Add(addSellingInvoicePayment.DataGridFooter);
				addSellingInvoicePayment.dataGrid_stockItems.DataContext = addSellingInvoicePayment.DataTable.DefaultView;

				UIComboBox.customersForFilter(addSellingInvoicePayment.comboBox_customer_filter);
				addSellingInvoicePayment.comboBox_customer_filter.SelectedIndex = 0;

				addSellingInvoicePayment.Pagination = new Pagination();
				addSellingInvoicePayment.Pagination.Filter = addSellingInvoicePayment;
				addSellingInvoicePayment.grid_pagination.Children.Add(addSellingInvoicePayment.Pagination);

				addSellingInvoicePayment.PaymentSection = new PaymentSection("SellingInvoice");
				addSellingInvoicePayment.grid_paymentSection.Children.Add(addSellingInvoicePayment.PaymentSection);
				setRowsCountSellingInvoice();
			} catch(Exception) {
			}
		}

		private SellingInvoice getSellingInvoiceForFilter() {
			SellingInvoice sellingInvoice = new SellingInvoice();
			try {
				sellingInvoice.CustomerId = Convert.ToInt32(addSellingInvoicePayment.comboBox_customer_filter.SelectedValue);
				sellingInvoice.InvoiceNumber = addSellingInvoicePayment.textBox_invoiceNumber_filter.Text + "%";
				sellingInvoice.IsCompletelyPaid = 0;
				sellingInvoice.Status = 1;
			} catch(Exception) {
			}
			return sellingInvoice;
		}

		internal void filterSellingInvoice() {
			try {
				List<SellingInvoice> list = sellingInvoiceManagerImpl.getInvoice(getSellingInvoiceForFilter());
				addSellingInvoicePayment.DataTable.Rows.Clear();
				foreach(SellingInvoice sellingInvoice in list) {
					double netTotal = sellingInvoiceManagerImpl.getNetTotalByInvoiceId(sellingInvoice.Id);
					double paid = getAllSellingPaidAmountForInvoice(sellingInvoice.Id);
					addSellingInvoicePayment.DataTable.Rows.Add(sellingInvoice.Id, sellingInvoice.InvoiceNumber, sellingInvoice.Date,
						netTotal.ToString("#,##0.00"), paid.ToString("#,##0.00"), (netTotal - paid).ToString("#,##0.00"));
				}
			} catch(Exception) {
			}
		}

		internal void setRowsCountSellingInvoice() {
			try {
				SellingInvoice i = getSellingInvoiceForFilter();
				i.RowsCount = 1;
				List<SellingInvoice> list = sellingInvoiceManagerImpl.getInvoice(i);
				addSellingInvoicePayment.Pagination.RowsCount = list[0].RowsCount;
			} catch(Exception) {
			}
		}

		internal void sellingInvoicerid_MouseLeftButtonUp() {
			try {
				DataRowView dataRowView_dRV = (DataRowView)addSellingInvoicePayment.dataGrid_stockItems.SelectedItem;
				addSellingInvoicePayment.textBox_invoiceNumber.Text = dataRowView_dRV[1].ToString();
				sellingInvoiceNumber_Enter();
			} catch(Exception) {
			}
		}

		internal void sellingInvoiceNumber_Enter() {
			try {
				SellingInvoice sellingInvoice = sellingInvoiceManagerImpl.getInvoiceByInvoiceNumber(addSellingInvoicePayment.textBox_invoiceNumber.Text);
				if(String.IsNullOrWhiteSpace(addSellingInvoicePayment.textBox_invoiceNumber.Text)) {
					addSellingInvoicePayment.PaymentSection.InvoiceId = 0;
				}
				if(sellingInvoice == null || sellingInvoice.IsCompletelyPaid == 1) {
					addSellingInvoicePayment.PaymentSection.InvoiceId = 0;
					ShowMessage.error(Common.Messages.Error.Error014);
					sellingInvoice = null;
					addSellingInvoicePayment.textBox_invoiceNumber.SelectAll();
				} else {
					addSellingInvoicePayment.SelectedInvoice = sellingInvoice;
					addSellingInvoicePayment.PaymentSection.InvoiceId = sellingInvoice.Id;
				}
			} catch(Exception) {
			}
		}

		internal bool saveSellingInvoice() {
			bool b = false;
			try {
				addSellingInvoicePayment.SelectedInvoice.IsCompletelyPaid = 1;
				CommonMethods.setCDMDForUpdate(addSellingInvoicePayment.SelectedInvoice);

				sellingInvoiceManagerImpl.updInvoice(addSellingInvoicePayment.SelectedInvoice);
				b = true;
				addSellingInvoicePayment.SelectedInvoice = null;
				addSellingInvoicePayment.textBox_invoiceNumber.Clear();
				addSellingInvoicePayment.setPagination();
				addSellingInvoicePayment.PaymentSection.InvoiceId = 0;

			} catch(Exception) {
			}
			return b;
		}
	}
}
