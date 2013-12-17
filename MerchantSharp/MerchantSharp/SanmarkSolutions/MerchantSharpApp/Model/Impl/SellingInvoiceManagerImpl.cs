using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.ReportMold;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class SellingInvoiceManagerImpl {

		private AddSellingInvoice addSellingInvoice;

		private IDao sellingInvoiceDao = null;
		private IDao sellingItemDao = null;

		private ItemManagerImpl itemManagerImpl = null;
		private UnitManagerImpl unitManagerImpl = null;
		private DiscountManagerImpl discountManagerImpl = null;
		private StockManagerImpl stockManagerImpl = null;
		private SellingInvoiceHistory sellingInvoiceHistory = null;
		private PaymentManagerImpl paymentManagerImpl = null;
		private UserManagerImpl userManagerImpl = null;
		private CustomerManagerImpl customerManagerImpl = null;

		public SellingInvoiceManagerImpl() {
			sellingInvoiceDao = SellingInvoiceDao.getInstance();
			sellingItemDao = SellingItemDao.getInstance();
		}

		public SellingInvoiceManagerImpl(AddSellingInvoice addSellingInvoice) {
			this.addSellingInvoice = addSellingInvoice;
			sellingInvoiceDao = SellingInvoiceDao.getInstance();
			sellingItemDao = SellingItemDao.getInstance();

			itemManagerImpl = new ItemManagerImpl();
			unitManagerImpl = new UnitManagerImpl();
			discountManagerImpl = new DiscountManagerImpl();
			stockManagerImpl = new StockManagerImpl();
			paymentManagerImpl = new PaymentManagerImpl();
			customerManagerImpl = new CustomerManagerImpl();
		}

		public SellingInvoiceManagerImpl(SellingInvoiceHistory sellingInvoiceHistory) {
			this.sellingInvoiceHistory = sellingInvoiceHistory;
			this.paymentManagerImpl = new PaymentManagerImpl();

			sellingInvoiceDao = SellingInvoiceDao.getInstance();
			sellingItemDao = SellingItemDao.getInstance();

			userManagerImpl = new UserManagerImpl();
			customerManagerImpl = new CustomerManagerImpl();
		}

		public int addInvoice(Entity entity) {
			return sellingInvoiceDao.add(entity);
		}

		public bool delInvoice(Entity entity) {
			return sellingInvoiceDao.del(entity);
		}

		public List<SellingInvoice> getInvoice(Entity entity) {
			return sellingInvoiceDao.get(entity).Cast<SellingInvoice>().ToList();
		}

		public int updInvoice(Entity entity) {
			return sellingInvoiceDao.upd(entity);
		}

		//////////////////////////////////////////////////

		public int addItem(Entity entity) {
			return sellingItemDao.add(entity);
		}

		public bool delItem(Entity entity) {
			return sellingItemDao.del(entity);
		}

		public List<SellingItem> getItem(Entity entity) {
			return sellingItemDao.get(entity).Cast<SellingItem>().ToList();
		}

		public int updItem(Entity entity) {
			return sellingItemDao.upd(entity);
		}

		////////////////////////////////////////////////////////////////////////////////////

		public SellingInvoice getInvoiceByInvoiceNumber(String invoiceNumber) {
			SellingInvoice sellingInvoice = null;
			try {
				SellingInvoice i = new SellingInvoice();
				i.InvoiceNumber = invoiceNumber;
				sellingInvoice = getInvoice(i)[0];
			} catch(Exception) {
			}
			return sellingInvoice;
		}

		public bool isDuplicateInvoiceNumber(String invoiceNumber) {
			bool b = false;
			try {
				if(getInvoiceByInvoiceNumber(invoiceNumber) != null) {
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		public SellingItem getSellingItemById(int id) {
			SellingItem sellingItem = null;
			try {
				SellingItem item = new SellingItem();
				item.Id = id;
				sellingItem = getItem(item)[0];
			} catch(Exception) {
			}
			return sellingItem;
		}

		internal SellingInvoice getInvoiceById(int id) {
			SellingInvoice sellingInvoice = null;
			try {
				SellingInvoice i = new SellingInvoice();
				i.Id = id;
				sellingInvoice = getInvoice(i)[0];
			} catch(Exception) {
			}
			return sellingInvoice;
		}

		public List<SellingItem> getSellingItemsByInvoiceId(int id) {
			List<SellingItem> list = null;
			try {
				SellingItem item = new SellingItem();
				item.SellingInvoiceId = id;
				list = getItem(item);
			} catch(Exception) {
			}
			return list;
		}

		public double getSubTotalByInvoiceId(int id) {
			double val = 0;
			try {
				SellingInvoice invoice = getInvoiceById(id);
				List<SellingItem> items = getSellingItemsByInvoiceId(id);
				foreach(SellingItem sellingItem in items) {
					val += ((sellingItem.SoldPrice - sellingItem.Discount) * sellingItem.Quantity);
				}
			} catch(Exception) {
			}
			return val;
		}

		public double getNetTotalByInvoiceId(int id) {
			double val = 0;
			try {
				SellingInvoice invoice = getInvoiceById(id);
				List<SellingItem> items = getSellingItemsByInvoiceId(id);
				foreach(SellingItem sellingItem in items) {
					val += ((sellingItem.SoldPrice - sellingItem.Discount) * (sellingItem.Quantity - sellingItem.MarketReturnQuantity - sellingItem.GoodReturnQuantity - sellingItem.WasteReturnQuantity));
				}
				val -= (invoice.Discount);
			} catch(Exception) {
			}
			return val;
		}

		public int getCustomerIdByInvoiceId(int id) {
			try {
				return getInvoiceById(id).CustomerId;
			} catch(Exception) {
				return 0;
			}
		}

		/// <summary>
		/// Will return next GRN
		/// </summary>
		/// <returns>String number</returns>
		public String getNextInvoiceNumber() {
			String code = null;
			try {
				SellingInvoice sellingInvoice = new SellingInvoice();
				sellingInvoice.OrderBy = "id";
				sellingInvoice.OrderType = "DESC";
				sellingInvoice.LimitStart = 0;
				List<SellingInvoice> listSellingInvoice = getInvoice(sellingInvoice);
				if(listSellingInvoice.Count == 0) {
					sellingInvoice = new SellingInvoice();
					sellingInvoice.InvoiceNumber = "0";
				} else {
					sellingInvoice = listSellingInvoice[listSellingInvoice.Count - 1];
				}
				bool run = true;
				Int64 intCode = Convert.ToInt64(!String.IsNullOrWhiteSpace(sellingInvoice.InvoiceNumber) ? sellingInvoice.InvoiceNumber : "0");
				Int64 intNewCode = intCode + 1;
				while(run) {
					if(intNewCode > 99999999999) {
						run = false;
					} else {
						if(intNewCode > 99999) {
							code = intNewCode.ToString();
						} else if(intNewCode > 9999) {
							code = "0" + intNewCode.ToString();
						} else if(intNewCode > 999) {
							code = "00" + intNewCode.ToString();
						} else if(intNewCode > 99) {
							code = "000" + intNewCode.ToString();
						} else if(intNewCode > 9) {
							code = "0000" + intNewCode.ToString();
						} else {
							code = "00000" + intNewCode.ToString();
						}
					}
					if(!isDuplicateInvoiceNumber(code)) {
						run = false;
					} else {
						intNewCode++;
					}
				}
			} catch(Exception) {
			}
			return code;
		}


		////////////////////////////////////////////////////////////////////////////
		// Add Selling Invoice

		private void loadAllItemsForView() {
			try {
				addSellingInvoice.SellingInvoice = getInvoiceById(addSellingInvoice.InvoiceId);
				addSellingInvoice.textBox_invoiceNumber_basicDetails.Text = addSellingInvoice.SellingInvoice.InvoiceNumber;
				addSellingInvoice.datePicker_date_basicDetails.SelectedDate = addSellingInvoice.SellingInvoice.Date;
				addSellingInvoice.comboBox_customer_basicDetails.Value = addSellingInvoice.SellingInvoice.CustomerId;
				addSellingInvoice.textBox_details_basicDetails.Text = addSellingInvoice.SellingInvoice.Details;

				addSellingInvoice.checkBox_completelyPaid_selectedItems.IsChecked = addSellingInvoice.SellingInvoice.IsCompletelyPaid == 1;
				addSellingInvoice.textBox_discount_selectedItems.DoubleValue = addSellingInvoice.SellingInvoice.Discount;
				addSellingInvoice.textBox_referrerCommision_selectedItems.DoubleValue = addSellingInvoice.SellingInvoice.ReferrerCommision;

				loadOldAllItemToDataTable();
				addSellingInvoice.textBox_cash_selectedItems.DoubleValue = addSellingInvoice.SellingInvoice.GivenMoney;
				addSellingInvoice.PaymentSection.InvoiceId = addSellingInvoice.InvoiceId;
				addSellingInvoice.PaymentSection.label_balance_vendorAccountSettlement.Content = customerManagerImpl.getAccountBalanceById(addSellingInvoice.comboBox_customer_basicDetails.Value).ToString("#,##0.00");

				addSellingInvoice.checkBox_quickPay_selectedItems.IsChecked = addSellingInvoice.SellingInvoice.IsQuickPaid == 1 ? true : false;

				if(addSellingInvoice.SellingInvoice.Status == 1) {
					addSellingInvoice.button_add_selectItem.IsEnabled = false;
					addSellingInvoice.dataGrid_selectedItems_selectedItems.IsEnabled = false;
					addSellingInvoice.textBox_discount_selectedItems.IsReadOnly = true;
					addSellingInvoice.checkBox_discountActivated.IsEnabled = false;
					addSellingInvoice.checkBox_quickPay_selectedItems.IsEnabled = false;
				} else {
					addSellingInvoice.dataGrid_selectedItems_selectedItems.IsEnabled = true;
					addSellingInvoice.textBox_discount_selectedItems.IsReadOnly = false;
					addSellingInvoice.checkBox_discountActivated.IsEnabled = true;
					addSellingInvoice.checkBox_quickPay_selectedItems.IsEnabled = true;
				}

			} catch(Exception) {
			}
		}

		private void loadOldAllItemToDataTable() {
			try {
				List<SellingItem> list = getSellingItemsByInvoiceId(addSellingInvoice.SellingInvoice.Id);
				addSellingInvoice.SelectedItems.Rows.Clear();
				foreach(SellingItem sellingItem in list) {
					DataRow dr = addSellingInvoice.SelectedItems.NewRow();
					dr[0] = sellingItem.Id.ToString();
					dr[1] = sellingItem.ItemId;
					dr[2] = itemManagerImpl.getItemNameById(sellingItem.ItemId);
					dr[3] = sellingItem.SellingMode == "p" ? "Pack" : "Unit";
					dr[4] = sellingItem.SoldPrice.ToString("#,##0.00");
					dr[5] = sellingItem.Discount.ToString("#,##0.00");
					dr[6] = sellingItem.Quantity.ToString("#,##0.00");
					dr[7] = ((sellingItem.DefaultPrice - sellingItem.Discount) * sellingItem.Quantity).ToString("#,##0.00");
					dr[8] = sellingItem.StockLocationId;
					dr[9] = sellingItem.MarketReturnQuantity.ToString("#,##0.00");
					dr[10] = sellingItem.GoodReturnQuantity.ToString("#,##0.00");
					dr[11] = sellingItem.WasteReturnQuantity.ToString("#,##0.00");
					addSellingInvoice.SelectedItems.Rows.Add(dr);
				}
				calculateSubTotal();
				calculateNetTotal();
				setItemCount();
			} catch(Exception) {
			}
		}

		internal void addSellingInvoiceLoaded() {
			try {
				addSellingInvoice.SelectedItems = new DataTable();
				addSellingInvoice.SelectedItems.Columns.Add("ID", typeof(int));
				addSellingInvoice.SelectedItems.Columns.Add("itemId", typeof(int));
				addSellingInvoice.SelectedItems.Columns.Add("Item", typeof(String));
				addSellingInvoice.SelectedItems.Columns.Add("Mode", typeof(String));
				addSellingInvoice.SelectedItems.Columns.Add("Price", typeof(String));
				addSellingInvoice.SelectedItems.Columns.Add("Discount", typeof(String));
				addSellingInvoice.SelectedItems.Columns.Add("Qty", typeof(String));
				addSellingInvoice.SelectedItems.Columns.Add("LineTotal", typeof(String));
				addSellingInvoice.SelectedItems.Columns.Add("stockId", typeof(int));
				addSellingInvoice.SelectedItems.Columns.Add("CR", typeof(String));
				addSellingInvoice.SelectedItems.Columns.Add("GR", typeof(String));
				addSellingInvoice.SelectedItems.Columns.Add("WR", typeof(String));
				addSellingInvoice.dataGrid_selectedItems_selectedItems.DataContext = addSellingInvoice.SelectedItems.DefaultView;

				addSellingInvoice.DataTableSellingPrices = new DataTable();
				addSellingInvoice.DataTableSellingPrices.Columns.Add("ID", typeof(int));
				addSellingInvoice.DataTableSellingPrices.Columns.Add("price", typeof(String));
				addSellingInvoice.comboBox_sellingPrice_selectItem.OptionGroup = addSellingInvoice.DataTableSellingPrices;

				addSellingInvoice.checkBox_quickPay_selectedItems.IsEnabled = false;
				addSellingInvoice.checkBox_completelyPaid_selectedItems.IsEnabled = false;

				UIComboBox.customersForAddSellingInvoice(addSellingInvoice.comboBox_customer_basicDetails);
				UIComboBox.loadStocks(addSellingInvoice.comboBox_stockId_selectItem, "s");

				if(Session.Meta["isActiveMultipleStocks"] == 0) {
					addSellingInvoice.label_stock_selectItem.Visibility = System.Windows.Visibility.Hidden;
					addSellingInvoice.comboBox_stockId_selectItem.Visibility = System.Windows.Visibility.Hidden;
				}
				if(addSellingInvoice.ItemFinder == null) {
					addSellingInvoice.ItemFinder = new ItemFinder(addSellingInvoice.textBox_itemId_selectItem);
					addSellingInvoice.grid_itemFinder.Children.Add(addSellingInvoice.ItemFinder);
				}
				if(addSellingInvoice.PaymentSection == null) {
					addSellingInvoice.PaymentSection = new PaymentSection("SellingInvoice");
					addSellingInvoice.grid_paymentSection.Children.Add(addSellingInvoice.PaymentSection);
				}
				addSellingInvoice.textBox_invoiceNumber_basicDetails.Text = "Guessed(" + getNextInvoiceNumber() + ")";
				if(Convert.ToInt32(Session.Preference["defaultItemSelectMode"]) == 0) {
					addSellingInvoice.textBox_item_selectItem.Focus();
				} else {
					addSellingInvoice.textBox_code_selectItem.Focus();
				}
				if(addSellingInvoice.IsInvoiceUpdateMode) {
					loadAllItemsForView();
				}
			} catch(Exception) {
			}
		}

		/// <summary>
		/// Fill item id text box if item is found
		/// </summary>
		internal void selectItemByCode() {
			try {
				String code = addSellingInvoice.textBox_code_selectItem.Text;
				int id = itemManagerImpl.getItemIdByCode(code);
				addSellingInvoice.textBox_itemId_selectItem.Text = id.ToString();
			} catch(Exception) {
			}
		}

		internal void loadSellingPrices() {
			try {
				UIComboBox.sellingPriceForSellingInvoice(addSellingInvoice.SelectedItem.Id, (addSellingInvoice.radioButton_unit_sellingMode.IsChecked == true ? "u" : "p"), addSellingInvoice.DataTableSellingPrices);
				addSellingInvoice.comboBox_sellingPrice_selectItem.SelectedIndex = 0;
			} catch(Exception) {
			}
		}

		internal void loadDiscounts() {
			try {
				addSellingInvoice.DiscountList = discountManagerImpl.getDiscountsByItemIdAndMode(addSellingInvoice.SelectedItem.Id, addSellingInvoice.radioButton_unit_sellingMode.IsChecked == true ? "u" : "p");
			} catch(Exception) {
			}
		}

		/// <summary>
		/// Will populate add item form when selec item.
		/// </summary>
		private void populateAddItemForm() {
			try {
				if(addSellingInvoice.SelectedItem != null) {
					addSellingInvoice.label_itemName_selectItem.Content = addSellingInvoice.SelectedItem.Name;
					addSellingInvoice.radioButton_unit_sellingMode.IsChecked = addSellingInvoice.SelectedItem.DefaultSellingMode == "u" ? true : false;
					addSellingInvoice.radioButton_pack_sellingMode.IsChecked = addSellingInvoice.SelectedItem.DefaultSellingMode == "p" ? true : false;
					addSellingInvoice.radioButton_unit_sellingMode.Content = "Unit (" + unitManagerImpl.getUnitNameById(addSellingInvoice.SelectedItem.UnitId) + ")";
					addSellingInvoice.radioButton_pack_sellingMode.Content = "Pack (" + (addSellingInvoice.SelectedItem.Sip == 1 ? addSellingInvoice.SelectedItem.PackName : unitManagerImpl.getUnitNameById(1)) + ")";
					addSellingInvoice.radioButton_pack_sellingMode.IsEnabled = addSellingInvoice.SelectedItem.Sip == 1 ? true : false;

					loadSellingPrices();
					addSellingInvoice.radioButton_pack_sellingMode.IsEnabled = addSellingInvoice.SelectedItem.Sip == 1 ? true : false;
					addSellingInvoice.textBox_sellingQuantity_selectItem.Focus();
					loadDiscounts();
				} else {
					addSellingInvoice.label_itemName_selectItem.Content = null;
					resetAddItemForm();
				}
			} catch(Exception) {
			}
		}

		private void resetAddItemForm() {
			try {
				addSellingInvoice.textBox_item_selectItem.Clear();
				addSellingInvoice.textBox_code_selectItem.Clear();
				addSellingInvoice.radioButton_unit_sellingMode.Content = "";
				addSellingInvoice.radioButton_pack_sellingMode.Content = "";
				addSellingInvoice.textBox_sellingQuantity_selectItem.Clear();
				addSellingInvoice.textBox_discount_selectItem.Clear();
				addSellingInvoice.DataTableSellingPrices.Rows.Clear();
				addSellingInvoice.textBox_lineTotal_selectItem.Clear();
				addSellingInvoice.textBox_marketReturn_selectItem.Clear();
				addSellingInvoice.textBox_goodReturn_selectItem.Clear();
				addSellingInvoice.textBox_wasteReturn_selectItem.Clear();
			} catch(Exception) {
			}
		}

		internal void selectItemById() {
			try {
				Item item = itemManagerImpl.getItemById(Convert.ToInt32(addSellingInvoice.textBox_itemId_selectItem.Text));
				if(item != null) {
					addSellingInvoice.SelectedItem = item;
					populateAddItemForm();
				} else {
					addSellingInvoice.SelectedItem = null;
					populateAddItemForm();
					ShowMessage.error(Common.Messages.Error.Error004);
				}
			} catch(Exception) {
			}
		}

		internal void calculateLineTotal() {
			try {
				addSellingInvoice.textBox_lineTotal_selectItem.DoubleValue = (addSellingInvoice.comboBox_sellingPrice_selectItem.DoubleValue - addSellingInvoice.textBox_discount_selectItem.DoubleValue) * addSellingInvoice.textBox_sellingQuantity_selectItem.DoubleValue;
			} catch(Exception) {
			}
		}

		private void setItemCount() {
			try {
				addSellingInvoice.textBox_itemCount_selectedItems.Text = addSellingInvoice.SelectedItems.Rows.Count.ToString();
			} catch(Exception) {
			}
		}

		internal void calculateNetTotal() {
			try {
				addSellingInvoice.textBox_netTotal_selectedItems.DoubleValue = addSellingInvoice.textBox_subTotal_selectedItems.DoubleValue - addSellingInvoice.textBox_discount_selectedItems.DoubleValue;
			} catch(Exception) {
			}
		}

		private void calculateSubTotal() {
			try {
				double subTotal = 0;
				foreach(DataRow row in addSellingInvoice.SelectedItems.Rows) {
					subTotal += (Convert.ToDouble(row["Price"]) - Convert.ToDouble(row["Discount"])) * Convert.ToDouble(row["Qty"]);
				}
				addSellingInvoice.textBox_subTotal_selectedItems.DoubleValue = subTotal;
			} catch(Exception) {
			}
		}

		internal void changePriceLabelName() {
			try {
				if(addSellingInvoice.radioButton_pack_sellingMode.IsChecked == true) {
					addSellingInvoice.label_sellingPrice_selectItem.Content = "Pack Price";
				} else {
					addSellingInvoice.label_sellingPrice_selectItem.Content = "Unit Price";
				}
			} catch(Exception) {
			}
		}


		internal void setDiscountForQuantity() {
			try {
				if(addSellingInvoice.textBox_sellingQuantity_selectItem.DoubleValue > 0) {
					foreach(Discount discount in addSellingInvoice.DiscountList) {
						if(addSellingInvoice.textBox_sellingQuantity_selectItem.DoubleValue >= discount.Quantity) {
							addSellingInvoice.textBox_discount_selectItem.DoubleValue = discount.Value;
							return;
						} else {
							addSellingInvoice.textBox_discount_selectItem.DoubleValue = 0;
						}
					}
				} else {
					addSellingInvoice.textBox_discount_selectItem.DoubleValue = 0;
				}
			} catch(Exception) {
			}
		}

		internal bool saveSellingInvoice(int status) {
			bool b = false;
			try {
				if(Convert.ToInt32(addSellingInvoice.comboBox_customer_basicDetails.SelectedValue) < 1) {
					ShowMessage.error(Common.Messages.Error.Error005);
				} else if(addSellingInvoice.datePicker_date_basicDetails.SelectedDate == null || addSellingInvoice.datePicker_date_basicDetails.SelectedDate.Value.Year == 1) {
					ShowMessage.error(Common.Messages.Error.Error005);
				} else if(status == 1 && addSellingInvoice.checkBox_quickPay_selectedItems.IsChecked == true && addSellingInvoice.textBox_cash_selectedItems.DoubleValue == 0) {
					ShowMessage.error(Common.Messages.Error.Error011);
				} else if(status == 1 && addSellingInvoice.SelectedItems.Rows.Count == 0) {
					ShowMessage.error(Common.Messages.Error.Error012);
				} else {
					SellingInvoice sellingInvoice = null;
					if(addSellingInvoice.SellingInvoice == null) {
						sellingInvoice = new SellingInvoice();
					} else {
						sellingInvoice = addSellingInvoice.SellingInvoice;
					}
					sellingInvoice.CustomerId = Convert.ToInt32(addSellingInvoice.comboBox_customer_basicDetails.SelectedValue);
					sellingInvoice.Date = addSellingInvoice.datePicker_date_basicDetails.SelectedValue;
					sellingInvoice.Discount = addSellingInvoice.textBox_discount_selectedItems.DoubleValue;
					sellingInvoice.IsCompletelyPaid = addSellingInvoice.checkBox_completelyPaid_selectedItems.IsChecked == true ? 1 : 0;
					sellingInvoice.ReferrerCommision = addSellingInvoice.textBox_referrerCommision_selectedItems.DoubleValue;
					sellingInvoice.IsQuickPaid = addSellingInvoice.checkBox_quickPay_selectedItems.IsChecked == true ? 1 : 0;
					sellingInvoice.Details = addSellingInvoice.textBox_details_basicDetails.TrimedText;
					sellingInvoice.CustomerAccountBalanceChange = 0;
					sellingInvoice.GivenMoney = addSellingInvoice.checkBox_quickPay_selectedItems.IsChecked == true ? addSellingInvoice.textBox_cash_selectedItems.DoubleValue : 0;
					sellingInvoice.Status = status;
					int invoiceId = 0;
					if(sellingInvoice.Id > 0) {
						CommonMethods.setCDMDForUpdate(sellingInvoice);
						if(status == 1) {
							if(sellingInvoice.InvoiceNumber == "" || sellingInvoice.InvoiceNumber == null) {
								sellingInvoice.InvoiceNumber = getNextInvoiceNumber();
							}
							updInvoice(sellingInvoice);
							saveAllSellingItems();
							if(!addSellingInvoice.IsInvoiceUpdateMode && addSellingInvoice.checkBox_quickPay_selectedItems.IsChecked == true) {
								SellingCash cash = new SellingCash();
								cash.SellingInvoiceId = sellingInvoice.Id;
								cash.Date = sellingInvoice.Date;
								cash.Amount = addSellingInvoice.textBox_netTotal_selectedItems.DoubleValue;
								cash.AccountTransfer = 0;
								cash.Notes = "";
								CommonMethods.setCDMDForAdd(cash);
								paymentManagerImpl.addSellingCash(cash);
							}
						} else {
							updInvoice(sellingInvoice);
							addSellingInvoice.checkBox_quickPay_selectedItems.IsEnabled = true;
						}
					} else {
						sellingInvoice.InvoiceNumber = "";
						CommonMethods.setCDMDForAdd(sellingInvoice);
						invoiceId = addInvoice(sellingInvoice);
						sellingInvoice.Id = invoiceId;
						addSellingInvoice.SellingInvoice = sellingInvoice;
						addSellingInvoice.checkBox_quickPay_selectedItems.IsEnabled = true;
					}
					if(status == 1) {
						addSellingInvoice.button_add_selectItem.IsEnabled = false;
						addSellingInvoice.dataGrid_selectedItems_selectedItems.IsEnabled = false;
						addSellingInvoice.textBox_discount_selectedItems.IsReadOnly = true;
						addSellingInvoice.textBox_referrerCommision_selectedItems.IsReadOnly = true;
						addSellingInvoice.checkBox_quickPay_selectedItems.IsEnabled = false;
						addSellingInvoice.checkBox_discountActivated.IsEnabled = false;
					}
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		private void saveAllSellingItems() {
			try {
				SellingItem sellingItem = null;
				StockItem stockItem = null;
				Item item = null;
				double subTotal = 0;
				foreach(DataRow dataRow_a in addSellingInvoice.SelectedItems.Rows) {
					subTotal += ((Convert.ToDouble(dataRow_a["Price"]) - Convert.ToDouble(dataRow_a["Discount"])) * Convert.ToDouble(dataRow_a["Qty"]));
				}

				foreach(DataRow row in addSellingInvoice.SelectedItems.Rows) {
					sellingItem = getSellingItemById(Convert.ToInt32(row["ID"]));
					double itemDiscount = (addSellingInvoice.checkBox_discountActivated.IsChecked == true) ? Convert.ToDouble(row["Discount"]) : 0;
					//sellingItem.SellingPriceActual = ((sellingItem.BuyingPrice * buyingItem.Quantity) / (buyingItem.Quantity + buyingItem.FreeQuantity)) - (((addsellingInvoice.textBox_discount_selectedItems.DoubleValue / Convert.ToDouble(row["Line Total"])) * (buyingItem.BuyingPrice * buyingItem.Quantity)) / (buyingItem.Quantity + buyingItem.FreeQuantity));
					sellingItem.SellingPriceActual = sellingItem.SoldPrice - itemDiscount - ((((addSellingInvoice.SellingInvoice.Discount + addSellingInvoice.SellingInvoice.ReferrerCommision) / subTotal) * ((sellingItem.SoldPrice - itemDiscount) * (sellingItem.Quantity == 0 ? 1 : sellingItem.Quantity))) / (sellingItem.Quantity == 0 ? 1 : sellingItem.Quantity));

					stockItem = stockManagerImpl.getStockItemByStockLocationIdAndItemId(sellingItem.StockLocationId, sellingItem.ItemId);
					item = itemManagerImpl.getItemById(sellingItem.ItemId);
					stockItem.Quantity -= ((sellingItem.Quantity - sellingItem.GoodReturnQuantity) * (sellingItem.SellingMode == "p" ? item.QuantityPerPack : 1));
					stockManagerImpl.updStockItem(stockItem);
					updItem(sellingItem);
				}
			} catch(Exception) {
			}
		}

		private bool validateAddItemForm() {
			bool isOkay = true;
			try {
				if(addSellingInvoice.SelectedItem == null) {
					isOkay = false;
					ShowMessage.error(Common.Messages.Error.Error008);
				} else {
					if(addSellingInvoice.textBox_sellingQuantity_selectItem.DoubleValue <= 0 &&
						addSellingInvoice.textBox_marketReturn_selectItem.DoubleValue <= 0 &&
						addSellingInvoice.textBox_goodReturn_selectItem.DoubleValue <= 0 &&
						addSellingInvoice.textBox_wasteReturn_selectItem.DoubleValue <= 0) {
						isOkay = false;
						addSellingInvoice.textBox_sellingQuantity_selectItem.ErrorMode(true);
					}
					if(addSellingInvoice.comboBox_sellingPrice_selectItem.DoubleValue <= 0) {
						isOkay = false;
						addSellingInvoice.comboBox_sellingPrice_selectItem.ErrorMode(true);
					}
				}
			} catch(Exception) {
			}
			return isOkay;
		}

		internal void addItemToDataGrid() {
			try {
				if(validateAddItemForm()) {
					DataRow dr = addSellingInvoice.SelectedItems.NewRow();

					dr[1] = addSellingInvoice.SelectedItem.Id;
					dr[2] = addSellingInvoice.SelectedItem.Name;
					dr[3] = addSellingInvoice.radioButton_pack_sellingMode.IsChecked == true ? "Pack" : "Unit";
					dr[4] = addSellingInvoice.comboBox_sellingPrice_selectItem.DisplayValue;
					dr[5] = addSellingInvoice.textBox_discount_selectItem.FormattedValue;
					dr[6] = addSellingInvoice.textBox_sellingQuantity_selectItem.FormattedValue;
					dr[7] = addSellingInvoice.textBox_lineTotal_selectItem.FormattedValue;
					dr[8] = addSellingInvoice.comboBox_stockId_selectItem.Value;
					dr[9] = addSellingInvoice.textBox_marketReturn_selectItem.FormattedValue;
					dr[10] = addSellingInvoice.textBox_goodReturn_selectItem.FormattedValue;
					dr[11] = addSellingInvoice.textBox_wasteReturn_selectItem.FormattedValue;
					SellingItem sellingItem = new SellingItem();
					sellingItem.SellingInvoiceId = addSellingInvoice.SellingInvoice.Id;
					sellingItem.ItemId = addSellingInvoice.SelectedItem.Id;
					sellingItem.StockLocationId = addSellingInvoice.comboBox_stockId_selectItem.Value;
					sellingItem.DefaultPrice = addSellingInvoice.comboBox_sellingPrice_selectItem.DoubleValue;
					sellingItem.SoldPrice = addSellingInvoice.comboBox_sellingPrice_selectItem.DoubleValue;
					sellingItem.SellingMode = addSellingInvoice.radioButton_unit_sellingMode.IsChecked == true ? "u" : "p";
					sellingItem.Quantity = addSellingInvoice.textBox_sellingQuantity_selectItem.DoubleValue;
					sellingItem.MarketReturnQuantity = addSellingInvoice.textBox_marketReturn_selectItem.DoubleValue;
					sellingItem.GoodReturnQuantity = addSellingInvoice.textBox_goodReturn_selectItem.DoubleValue;
					sellingItem.WasteReturnQuantity = addSellingInvoice.textBox_wasteReturn_selectItem.DoubleValue;
					sellingItem.Discount = addSellingInvoice.textBox_discount_selectItem.DoubleValue;
					sellingItem.StockBeforeSale = 0;
					sellingItem.SellingPriceActual = 0;
					sellingItem.BuyingPriceActual = 0;
					CommonMethods.setCDMDForAdd(sellingItem);
					dr[0] = addItem(sellingItem);
					addSellingInvoice.SelectedItems.Rows.Add(dr);
					resetAddItemForm();
					calculateSubTotal();
					calculateNetTotal();
					setItemCount();
					if(Convert.ToInt32(Session.Preference["defaultItemSelectMode"]) == 0) {
						addSellingInvoice.textBox_item_selectItem.Focus();
					} else {
						addSellingInvoice.textBox_code_selectItem.Focus();
					}
				}
			} catch(Exception) {
			}
		}

		internal void updItemInDataGrid() {
			try {
				if(validateAddItemForm()) {
					DataRow dr = addSellingInvoice.SelectedItems.Rows[addSellingInvoice.UpdateItemSelectedIndex];

					SellingItem sellingItem = getSellingItemById(Convert.ToInt32(dr[0]));
					sellingItem.StockLocationId = addSellingInvoice.comboBox_stockId_selectItem.Value;
					sellingItem.DefaultPrice = addSellingInvoice.comboBox_sellingPrice_selectItem.DoubleValue;
					sellingItem.SoldPrice = addSellingInvoice.comboBox_sellingPrice_selectItem.DoubleValue;
					sellingItem.SellingMode = addSellingInvoice.radioButton_unit_sellingMode.IsChecked == true ? "u" : "p";
					sellingItem.Quantity = addSellingInvoice.textBox_sellingQuantity_selectItem.DoubleValue;
					sellingItem.MarketReturnQuantity = addSellingInvoice.textBox_marketReturn_selectItem.DoubleValue;
					sellingItem.GoodReturnQuantity = addSellingInvoice.textBox_goodReturn_selectItem.DoubleValue;
					sellingItem.WasteReturnQuantity = addSellingInvoice.textBox_wasteReturn_selectItem.DoubleValue;
					sellingItem.Discount = addSellingInvoice.textBox_discount_selectItem.DoubleValue;
					sellingItem.StockBeforeSale = 0;
					sellingItem.SellingPriceActual = 0;
					sellingItem.BuyingPriceActual = 0;
					CommonMethods.setCDMDForUpdate(sellingItem);
					//dr[0] = addItem(sellingItem);
					updItem(sellingItem);
					//dr[1] = addsellingInvoice.SelectedItem.Id;
					dr[2] = addSellingInvoice.SelectedItem.Name;
					dr[3] = addSellingInvoice.radioButton_pack_sellingMode.IsChecked == true ? "Pack" : "Unit";
					dr[4] = addSellingInvoice.comboBox_sellingPrice_selectItem.DisplayValue;
					dr[5] = addSellingInvoice.textBox_discount_selectItem.FormattedValue;
					dr[6] = addSellingInvoice.textBox_sellingQuantity_selectItem.FormattedValue;
					dr[7] = addSellingInvoice.textBox_lineTotal_selectItem.FormattedValue;
					dr[8] = addSellingInvoice.comboBox_stockId_selectItem.Value;
					dr[9] = addSellingInvoice.textBox_marketReturn_selectItem.FormattedValue;
					dr[10] = addSellingInvoice.textBox_goodReturn_selectItem.FormattedValue;
					dr[11] = addSellingInvoice.textBox_wasteReturn_selectItem.FormattedValue;

					resetAddItemForm();
					addSellingInvoice.IsItemUpdateMode = false;
					addSellingInvoice.button_add_selectItem.Content = "Add";
					calculateSubTotal();
					calculateNetTotal();
					if(Convert.ToInt32(Session.Preference["defaultItemSelectMode"]) == 0) {
						addSellingInvoice.textBox_item_selectItem.Focus();
					} else {
						addSellingInvoice.textBox_code_selectItem.Focus();
					}
				}
			} catch(Exception) {
			}
		}

		internal void populateUpdateItemForm() {
			try {
				resetAddItemForm();
				addSellingInvoice.IsItemUpdateMode = true;
				addSellingInvoice.button_add_selectItem.Content = "Update";
				addSellingInvoice.UpdateItemSelectedIndex = addSellingInvoice.dataGrid_selectedItems_selectedItems.SelectedIndex;

				DataRow dataRow_items = addSellingInvoice.SelectedItems.Rows[addSellingInvoice.UpdateItemSelectedIndex];
				addSellingInvoice.textBox_itemId_selectItem.Text = dataRow_items["itemId"].ToString();

				if(Convert.ToString(dataRow_items["Mode"]) == "Pack") {
					addSellingInvoice.radioButton_pack_sellingMode.IsChecked = true;
				} else {
					addSellingInvoice.radioButton_unit_sellingMode.IsChecked = true;
				}
				addSellingInvoice.comboBox_sellingPrice_selectItem.Text = Convert.ToString(dataRow_items["Price"]);
				addSellingInvoice.textBox_sellingQuantity_selectItem.Text = Convert.ToDouble(dataRow_items["Qty"]).ToString();
				addSellingInvoice.textBox_discount_selectItem.Text = Convert.ToString(dataRow_items["Discount"]);
				addSellingInvoice.textBox_marketReturn_selectItem.Text = Convert.ToString(dataRow_items["CR"]);
				addSellingInvoice.textBox_goodReturn_selectItem.Text = Convert.ToString(dataRow_items["GR"]);
				addSellingInvoice.textBox_wasteReturn_selectItem.Text = Convert.ToString(dataRow_items["WR"]);
				addSellingInvoice.comboBox_stockId_selectItem.SelectedValue = Convert.ToInt32(dataRow_items["stockId"]);
			} catch(Exception) {
			}
		}

		internal void checkBox_quickPay_selectedItems_Click() {
			try {
				if(addSellingInvoice.checkBox_quickPay_selectedItems.IsChecked == true) {
					addSellingInvoice.checkBox_completelyPaid_selectedItems.IsEnabled = false;
					addSellingInvoice.checkBox_completelyPaid_selectedItems.IsChecked = true;
				} else {
					addSellingInvoice.checkBox_completelyPaid_selectedItems.IsEnabled = true;
					addSellingInvoice.checkBox_completelyPaid_selectedItems.IsChecked = false;
					saveSellingInvoice(1);
					addSellingInvoice.PaymentSection.InvoiceId = addSellingInvoice.SellingInvoice.Id;
				}
			} catch(Exception) {
			}
		}

		internal void printBill(int countOfCopies, bool isNewBill, int id) {
			try {
				ReportPrinter rp = new ReportPrinter();
				Microsoft.Reporting.WinForms.LocalReport r = new Microsoft.Reporting.WinForms.LocalReport();
				r.ReportEmbeddedResource = "MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.RDLC.SellingInvoicePOS.rdlc";
				PrepareReport prepareReport = new PrepareReport();
				rp.print(r, addSellingInvoice.SelectedItems, prepareReport.getParameters());
			} catch(Exception) {
			}
		}

		internal void calculateBalance() {
			try {
				addSellingInvoice.textBox_balance_selectedItems.Text = (addSellingInvoice.textBox_cash_selectedItems.DoubleValue - addSellingInvoice.textBox_netTotal_selectedItems.DoubleValue).ToString("#,##0.00");
				if(Convert.ToDouble(addSellingInvoice.textBox_balance_selectedItems.Text) < 0) {
					addSellingInvoice.textBox_balance_selectedItems.Foreground = System.Windows.Media.Brushes.Red;
				} else {
					addSellingInvoice.textBox_balance_selectedItems.Foreground = System.Windows.Media.Brushes.Black;
				}
			} catch(Exception) {
			}
		}


		///////////////////////////////////////////////////////////////////////////////////////////////////
		// History
		///////////////////////////////////////////////////////////////////////////////////////////////////

		private SellingInvoice getSellingInvoiceForFilter() {
			SellingInvoice sellingInvoice = null;
			try {
				sellingInvoice = new SellingInvoice();
				sellingInvoice.InvoiceNumber = sellingInvoiceHistory.textBox_invoiceNumber_filter.IsNull() ? null : "%" + sellingInvoiceHistory.textBox_invoiceNumber_filter.TrimedText + "%";
				sellingInvoice.CustomerId = sellingInvoiceHistory.comboBox_customer_filter.Value;
				sellingInvoice.CreatedBy = sellingInvoiceHistory.comboBox_user_filter.Value;
				if(sellingInvoiceHistory.datePicker_from_filter.SelectedDate != null || sellingInvoiceHistory.datePicker_to_filter.SelectedDate != null) {
					if(sellingInvoiceHistory.datePicker_from_filter.SelectedDate != null && sellingInvoiceHistory.datePicker_to_filter.SelectedDate != null) {
						sellingInvoice.Date = sellingInvoiceHistory.datePicker_from_filter.SelectedValue;
						sellingInvoice.addDateCondition("date", "BETWEEN", sellingInvoiceHistory.datePicker_from_filter.SelectedValue.ToString("yyyy-MM-dd"), sellingInvoiceHistory.datePicker_to_filter.SelectedValue.ToString("yyyy-MM-dd"));
					} else if(sellingInvoiceHistory.datePicker_from_filter.SelectedDate != null) {
						sellingInvoice.Date = sellingInvoiceHistory.datePicker_from_filter.SelectedValue;
					} else {
						sellingInvoice.Date = sellingInvoiceHistory.datePicker_to_filter.SelectedValue;
					}
				}
				sellingInvoice.Status = sellingInvoiceHistory.comboBox_status_filter.Value;
				sellingInvoice.IsCompletelyPaid = sellingInvoiceHistory.comboBox_isCompletelyPaid_filter.Value;
				sellingInvoice.Details = "%" + sellingInvoiceHistory.textBox_details_filter.TrimedText + "%";
			} catch(Exception) {
			}
			return sellingInvoice;
		}

		internal void filter() {
			try {
				sellingInvoiceHistory.DataTable.Rows.Clear();
				SellingInvoice invoice = getSellingInvoiceForFilter();
				invoice.LimitStart = sellingInvoiceHistory.Pagination.LimitStart;
				invoice.LimitEnd = sellingInvoiceHistory.Pagination.LimitCount;
				List<SellingInvoice> list = getInvoice(invoice);
				DataRow row = null;
				foreach(SellingInvoice sellingInvoice in list) {
					row = sellingInvoiceHistory.DataTable.NewRow();
					row[0] = sellingInvoice.Id;
					row[1] = sellingInvoice.InvoiceNumber;
					row[2] = sellingInvoice.Date.ToString("yyyy-MM-dd");
					row[3] = sellingInvoice.Status == 1 ? getSubTotalByInvoiceId(sellingInvoice.Id).ToString("#,##0.00") : "-";
					row[4] = sellingInvoice.Status == 1 ? sellingInvoice.Discount.ToString("#,##0.00") : "-";
					row[5] = sellingInvoice.Status == 1 ? sellingInvoice.ReferrerCommision.ToString("#,##0.00") : "-";
					double netTotal = getNetTotalByInvoiceId(sellingInvoice.Id);
					double totalPayments = paymentManagerImpl.getAllSellingPaidAmountForInvoice(sellingInvoice.Id);
					row[6] = sellingInvoice.Status == 1 ? netTotal.ToString("#,##0.00") : "-";
					row[7] = sellingInvoice.Status == 1 ? totalPayments.ToString("#,##0.00") : "-";
					double remainder = netTotal - totalPayments;
					if(Session.Meta["isActiveCustomerAccountBalance"] == 1) {
						row[8] = sellingInvoice.Status == 1 ? (remainder < 0 ? "0.00" : remainder.ToString("#,##0.00")) : "-";
						row[9] = sellingInvoice.Status == 1 ? (remainder < 0 ? (remainder * -1).ToString("#,##0.00") : sellingInvoice.CustomerAccountBalanceChange.ToString("#,##0.00")) : "-";
					} else {
						row[8] = sellingInvoice.Status == 1 ? "0.00" : "-";
						row[9] = sellingInvoice.Status == 1 ? "0.00" : "-";
					}
					row[10] = customerManagerImpl.getCustomerNameById(sellingInvoice.CustomerId);
					row[11] = userManagerImpl.getFullNameById(sellingInvoice.CreatedBy);
					row[12] = CommonMethods.getYesNo(sellingInvoice.IsCompletelyPaid);
					row[13] = CommonMethods.getStatusForSellingInvoice(sellingInvoice.Status);

					sellingInvoiceHistory.DataTable.Rows.Add(row);
				}
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				SellingInvoice invoice = getSellingInvoiceForFilter();
				invoice.RowsCount = 1;
				List<SellingInvoice> list = getInvoice(invoice);
				sellingInvoiceHistory.Pagination.RowsCount = list[0].RowsCount;
			} catch(Exception) {
			}
		}

		internal void sellingInvoiceHistoryLoaded() {
			try {
				UIComboBox.customersForFilter(sellingInvoiceHistory.comboBox_customer_filter);
				UIComboBox.usersForFilter(sellingInvoiceHistory.comboBox_user_filter);
				UIComboBox.sellingInvoiceStatusForSelect(sellingInvoiceHistory.comboBox_status_filter);
				UIComboBox.yesNoForSelect(sellingInvoiceHistory.comboBox_isCompletelyPaid_filter);
				sellingInvoiceHistory.Pagination = new Pagination();
				sellingInvoiceHistory.Pagination.Filter = sellingInvoiceHistory;
				sellingInvoiceHistory.grid_pagination.Children.Add(sellingInvoiceHistory.Pagination);

				sellingInvoiceHistory.DataTable = new DataTable();
				sellingInvoiceHistory.DataTable.Columns.Add("ID", typeof(int));
				sellingInvoiceHistory.DataTable.Columns.Add("Invoice #", typeof(String));
				sellingInvoiceHistory.DataTable.Columns.Add("Date", typeof(String));
				sellingInvoiceHistory.DataTable.Columns.Add("Sub Total", typeof(String));
				sellingInvoiceHistory.DataTable.Columns.Add("Discount", typeof(String));
				sellingInvoiceHistory.DataTable.Columns.Add("Referer Commision", typeof(String));
				sellingInvoiceHistory.DataTable.Columns.Add("Net Total", typeof(String));
				sellingInvoiceHistory.DataTable.Columns.Add("Total Payments", typeof(String));
				sellingInvoiceHistory.DataTable.Columns.Add("Remainder", typeof(String));
				sellingInvoiceHistory.DataTable.Columns.Add("Account Balance Change", typeof(String));
				sellingInvoiceHistory.DataTable.Columns.Add("Customer", typeof(String));
				sellingInvoiceHistory.DataTable.Columns.Add("User", typeof(String));
				sellingInvoiceHistory.DataTable.Columns.Add("Completely Paid", typeof(String));
				sellingInvoiceHistory.DataTable.Columns.Add("Status", typeof(String));
				sellingInvoiceHistory.dataGrid_sellingInvoiceHistory.DataContext = sellingInvoiceHistory.DataTable.DefaultView;
			} catch(Exception) {
			}
		}

		internal void dataGrid_sellingInvoiceHistory_MouseDoubleClick() {
			try {
				if(Session.Permission["canEditSellingInvoice"] == 1) {
					ThreadPool.openTab(new AddSellingInvoice(sellingInvoiceHistory.dataGrid_sellingInvoiceHistory.SelectedItemID), "Edit Selling Invoice");
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
		}
	}
}
