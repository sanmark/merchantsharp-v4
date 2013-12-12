using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class BuyingInvoiceManagerImpl {

		private IDao buyingInvoiceDao = BuyingInvoiceDao.getInstance();
		private IDao buyingItemDao = BuyingItemDao.getInstance();
		private AddBuyingInvoice addBuyingInvoice;
		private BuyingInvoiceHistory buyingInvoiceHistory;

		private ItemManagerImpl itemManagerImpl = null;
		private UnitManagerImpl unitManagerImpl = null;
		private SellingPriceManagerImpl sellingPriceManagerImpl = null;
		private StockManagerImpl stockManagerImpl = null;
		private VendorManagerImpl vendorManagerImpl = new VendorManagerImpl();


		public BuyingInvoiceManagerImpl() {
			itemManagerImpl = new ItemManagerImpl();
		}

		public BuyingInvoiceManagerImpl(AddBuyingInvoice addBuyingInvoice) {
			this.addBuyingInvoice = addBuyingInvoice;
			itemManagerImpl = new ItemManagerImpl();
			unitManagerImpl = new UnitManagerImpl();
			sellingPriceManagerImpl = new SellingPriceManagerImpl();
			stockManagerImpl = new StockManagerImpl();

		}

		public BuyingInvoiceManagerImpl(BuyingInvoiceHistory buyingInvoiceHistory) {
			this.buyingInvoiceHistory = buyingInvoiceHistory;
		}

		public int addInvoice(Entity entity) {
			return buyingInvoiceDao.add(entity);
		}

		public bool delInvoice(Entity entity) {
			return buyingInvoiceDao.del(entity);
		}

		public List<BuyingInvoice> getInvoice(Entity entity) {
			return buyingInvoiceDao.get(entity).Cast<BuyingInvoice>().ToList();
		}

		public int updInvoice(Entity entity) {
			return buyingInvoiceDao.upd(entity);
		}

		/////////////

		public int addItem(Entity entity) {
			return buyingItemDao.add(entity);
		}

		public bool delItem(Entity entity) {
			return buyingItemDao.del(entity);
		}

		public List<BuyingItem> getItem(Entity entity) {
			return buyingItemDao.get(entity).Cast<BuyingItem>().ToList();
		}

		public int updItem(Entity entity) {
			return buyingItemDao.upd(entity);
		}


		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////// Buying Invoice Manager //////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////


		public BuyingInvoice getInvoiceByGRN(String grn) {
			BuyingInvoice buyingInvoice = null;
			try {
				BuyingInvoice i = new BuyingInvoice();
				i.Grn = grn;
				buyingInvoice = getInvoice(i)[0];
			} catch(Exception) {
			}
			return buyingInvoice;
		}

		public bool isDuplicateGRN(String grn) {
			bool b = false;
			try {
				if(getInvoiceByGRN(grn) != null) {
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		public BuyingItem getBuyingItemById(int id) {
			BuyingItem buyingItem = null;
			try {
				BuyingItem item = new BuyingItem();
				item.Id = id;
				buyingItem = getItem(item)[0];
			} catch(Exception) {
			}
			return buyingItem;
		}

		/// <summary>
		/// Will return next GRN
		/// </summary>
		/// <returns>String number</returns>
		public String getNextGRN() {
			String code = null;
			try {
				BuyingInvoice buyingInvoice = new BuyingInvoice();
				buyingInvoice.OrderBy = "id";
				buyingInvoice.OrderType = "DESC";
				buyingInvoice.LimitStart = 0;
				List<BuyingInvoice> listBuyingInvoice = getInvoice(buyingInvoice);
				if(listBuyingInvoice.Count == 0) {
					buyingInvoice = new BuyingInvoice();
					buyingInvoice.Grn = "0";
				} else {
					buyingInvoice = listBuyingInvoice[listBuyingInvoice.Count - 1];
				}
				bool run = true;
				Int64 intCode = Convert.ToInt64(!String.IsNullOrWhiteSpace(buyingInvoice.Grn) ? buyingInvoice.Grn : "0");
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
					if(!isDuplicateGRN(code)) {
						run = false;
					} else {
						intNewCode++;
					}
				}
			} catch(Exception) {
			}
			return code;
		}

		public bool deleteBuyingItemById(int id) {
			try {
				BuyingItem item = new BuyingItem();
				item.Id = id;
				return delItem(item);
			} catch(Exception) {
				return false;
			}
		}

		internal BuyingInvoice getInvoiceById(int id) {
			BuyingInvoice buyingInvoice = null;
			try {
				BuyingInvoice i = new BuyingInvoice();
				i.Id = id;
				buyingInvoice = getInvoice(i)[0];
			} catch(Exception) {
			}
			return buyingInvoice;
		}

		public List<BuyingItem> getBuyingItemsByInvoiceId(int id) {
			List<BuyingItem> list = null;
			try {
				BuyingItem item = new BuyingItem();
				item.BuyingInvoiceId = id;
				list = getItem(item);
			} catch(Exception) {
			}
			return list;
		}

		public double getNetTotalByInvoiceId(int id) {
			double val = 0;
			try {
				BuyingInvoice invoice = getInvoiceById(id);
				List<BuyingItem> items = getBuyingItemsByInvoiceId(id);
				foreach(BuyingItem buyingItem in items) {
					val += (buyingItem.BuyingPrice * buyingItem.Quantity);
				}
				val -= (invoice.Discount + invoice.MarketReturnDiscount + invoice.LaterDiscount);
			} catch(Exception) {
			}
			return val;
		}

		public int getVendorIdByInvoiceId(int id) {
			try {
				return getInvoiceById(id).VendorId;
			} catch(Exception) {
				return 0;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////// Add Buying Invoice Implementation //////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////


		internal void addBuyingInvoiceLoaded() {
			try {
				addBuyingInvoice.SelectedItems = new DataTable();
				addBuyingInvoice.SelectedItems.Columns.Add("ID", typeof(int));
				addBuyingInvoice.SelectedItems.Columns.Add("itemId", typeof(int));
				addBuyingInvoice.SelectedItems.Columns.Add("Item", typeof(String));
				addBuyingInvoice.SelectedItems.Columns.Add("Mode", typeof(String));
				addBuyingInvoice.SelectedItems.Columns.Add("USP", typeof(String));
				addBuyingInvoice.SelectedItems.Columns.Add("PSP", typeof(String));
				//addBuyingInvoice.SelectedItems.Columns.Add("PD", typeof(String));
				//addBuyingInvoice.SelectedItems.Columns.Add("UD", typeof(String));
				//addBuyingInvoice.SelectedItems.Columns.Add("Unit", typeof(String));
				addBuyingInvoice.SelectedItems.Columns.Add("Price", typeof(String));
				addBuyingInvoice.SelectedItems.Columns.Add("Qty", typeof(String));
				addBuyingInvoice.SelectedItems.Columns.Add("Free Qty", typeof(String));
				addBuyingInvoice.SelectedItems.Columns.Add("Line Total", typeof(String));
				addBuyingInvoice.SelectedItems.Columns.Add("stockId", typeof(int));
				addBuyingInvoice.dataGrid_selectedItems_selectedItems.DataContext = addBuyingInvoice.SelectedItems.DefaultView;

				addBuyingInvoice.checkBox_isRequestOrder_selectedItems.IsChecked = addBuyingInvoice.IsRequestOrder;
				addBuyingInvoice.checkBox_isRequestOrder_selectedItems.IsEnabled = !addBuyingInvoice.IsRequestOrder;
				addBuyingInvoice.textBox_grnNumber_basicDetails.Text = "Guessed(" + getNextGRN() + ")";
				UIComboBox.vendorsForAddBuyingInvoice(addBuyingInvoice.comboBox_vendor_basicDetails);
				UIComboBox.loadStocks(addBuyingInvoice.comboBox_stock_selectItem);
				addBuyingInvoice.AddSellingPriceUnit = new AddSellingPrice();
				addBuyingInvoice.AddSellingPricePack = new AddSellingPrice();

				if(Session.Permission["canDeleteSellingPrice"] == 0) {
					addBuyingInvoice.button_sellingPricePerPackDelete_selectItem.IsEnabled = false;
					addBuyingInvoice.button_sellingPricePerUnitDelete_selectItem.IsEnabled = false;
				}
				if(Session.Meta["isActiveExpiryDate"] == 0) {
					addBuyingInvoice.label_expiryDate_selectItem.Visibility = Visibility.Hidden;
					addBuyingInvoice.datePicker_expiryDate_selectItem.Visibility = Visibility.Hidden;
				}
				if(Session.Meta["isActiveMultipleStocks"] == 0) {
					addBuyingInvoice.label_stock_selectItem.Visibility = Visibility.Hidden;
					addBuyingInvoice.comboBox_stock_selectItem.Visibility = Visibility.Hidden;
				}
				if(addBuyingInvoice.ItemFinder == null) {
					addBuyingInvoice.ItemFinder = new ItemFinder(addBuyingInvoice.textBox_itemId_selectItem);
					addBuyingInvoice.grid_itemFinder.Children.Add(addBuyingInvoice.ItemFinder);
				}
				if(addBuyingInvoice.PaymentSection == null) {
					addBuyingInvoice.PaymentSection = new PaymentSection("BuyingInvoice");
					addBuyingInvoice.grid_paymentSection.Children.Add(addBuyingInvoice.PaymentSection);
				}
			} catch(Exception) {
			}
		}

		/// <summary>
		/// Fill item id text box if item is found
		/// </summary>
		internal void selectItemByCode() {
			try {
				String code = addBuyingInvoice.textBox_code_selectItem.Text;
				int id = itemManagerImpl.getItemIdByCode(code);
				addBuyingInvoice.textBox_itemId_selectItem.Text = id.ToString();
			} catch(Exception) {
			}
		}

		/// <summary>
		/// Will populate add item form when selec item.
		/// </summary>
		private void populateAddItemForm() {
			try {
				if(addBuyingInvoice.SelectedItem != null) {
					addBuyingInvoice.label_itemName_selectItem.Content = addBuyingInvoice.SelectedItem.Name;
					addBuyingInvoice.radioButton_unit_buyingMode.IsChecked = addBuyingInvoice.SelectedItem.DefaultBuyingMode == "u" ? true : false;
					addBuyingInvoice.radioButton_pack_buyingMode.IsChecked = addBuyingInvoice.SelectedItem.DefaultBuyingMode == "p" ? true : false;
					//addBuyingInvoice.radioButton_unit_buyingMode.Content = "Unit (" + (addBuyingInvoice.SelectedItem.Sip == 0 ? unitManagerImpl.getUnitNameById(addBuyingInvoice.SelectedItem.UnitId) : "") + ")";
					addBuyingInvoice.radioButton_unit_buyingMode.Content = "Unit (" + unitManagerImpl.getUnitNameById(addBuyingInvoice.SelectedItem.UnitId) + ")";
					addBuyingInvoice.radioButton_pack_buyingMode.Content = "Pack (" + (addBuyingInvoice.SelectedItem.Sip == 1 ? addBuyingInvoice.SelectedItem.PackName : unitManagerImpl.getUnitNameById(1)) + ")";
					addBuyingInvoice.radioButton_pack_buyingMode.IsEnabled = addBuyingInvoice.SelectedItem.Sip == 1 ? true : false;

					UIComboBox.sellingPriceForItemAndMode(addBuyingInvoice.comboBox_sellingPricePerUnit_selectItem, addBuyingInvoice.SelectedItem.Id, "u", addBuyingInvoice.AddSellingPriceUnit);
					UIComboBox.sellingPriceForItemAndMode(addBuyingInvoice.comboBox_sellingPricePerPack_selectItem, addBuyingInvoice.SelectedItem.Id, "p", addBuyingInvoice.AddSellingPricePack);

					addBuyingInvoice.comboBox_sellingPricePerPack_selectItem.IsEnabled = addBuyingInvoice.SelectedItem.Sip == 1 ? true : false;
					addBuyingInvoice.textBox_buyingQuantity_selectItem.Focus();
				} else {
					addBuyingInvoice.label_itemName_selectItem.Content = null;
				}
			} catch(Exception) {
			}
		}

		/// <summary>
		/// Will select item by item id.
		/// </summary>
		internal void selectItemById() {
			try {
				Item item = itemManagerImpl.getItemById(Convert.ToInt32(addBuyingInvoice.textBox_itemId_selectItem.Text));
				if(item != null) {
					addBuyingInvoice.SelectedItem = item;
					populateAddItemForm();
				} else {
					addBuyingInvoice.SelectedItem = null;
					populateAddItemForm();
					ShowMessage.error(Common.Messages.Error.Error004);
				}
			} catch(Exception) {
			}
		}
		internal bool saveBuyingInvoice(int status) {
			bool b = false;
			try {
				if(addBuyingInvoice.comboBox_vendor_basicDetails.Value < 1) {
					ShowMessage.error(Common.Messages.Error.Error005);
				} else if(addBuyingInvoice.checkBox_isRequestOrder_selectedItems.IsChecked == false && addBuyingInvoice.textBox_invoiceNumber_basicDetails.IsNull()) {
					ShowMessage.error(Common.Messages.Error.Error005);
				} else {
					BuyingInvoice buyingInvoice = null;
					if(addBuyingInvoice.BuyingInvoice == null) {
						buyingInvoice = new BuyingInvoice();
					} else {
						buyingInvoice = addBuyingInvoice.BuyingInvoice;
					}
					buyingInvoice.VendorId = addBuyingInvoice.comboBox_vendor_basicDetails.Value;
					buyingInvoice.InvoiceNumber = addBuyingInvoice.textBox_invoiceNumber_basicDetails.TrimedText;
					buyingInvoice.Grn = "";
					buyingInvoice.OrderedDate = addBuyingInvoice.datePicker_date_basicDetails.SelectedValue;
					buyingInvoice.Discount = addBuyingInvoice.textBox_discount_selectedItems.DoubleValue;
					buyingInvoice.IsCompletelyPaid = addBuyingInvoice.checkBox_completelyPaid_selectedItems.IsChecked == true ? 1 : 0;
					buyingInvoice.MarketReturnDiscount = addBuyingInvoice.textBox_companyReturn_selectedItems.DoubleValue;
					buyingInvoice.ExpectedPayingDate = addBuyingInvoice.datePicker_expectedPayingDate_basicDetails.SelectedValue;
					buyingInvoice.Details = addBuyingInvoice.textBox_details_basicDetails.TrimedText;
					buyingInvoice.VendorAccountBalanceChange = 0;
					buyingInvoice.VendorAccountBalanceType = 0;
					buyingInvoice.LaterDiscount = addBuyingInvoice.textBox_laterDiscount_selectedItems.DoubleValue;
					buyingInvoice.Status = status;
					int invoiceId = 0;
					if(buyingInvoice.Id > 0) {
						CommonMethods.setCDMDForUpdate(buyingInvoice);
						if(status == 1) {
							if(status != 3) {
								buyingInvoice.Grn = getNextGRN();
							}
							updInvoice(buyingInvoice);
							saveAllBuyingItems();
						} else {
							updInvoice(buyingInvoice);
						}
					} else {
						CommonMethods.setCDMDForAdd(buyingInvoice);
						invoiceId = addInvoice(buyingInvoice);
						buyingInvoice.Id = invoiceId;
						addBuyingInvoice.BuyingInvoice = buyingInvoice;
					}
					if(status == 1) {
						addBuyingInvoice.button_add_selectItem.IsEnabled = false;
						addBuyingInvoice.dataGrid_selectedItems_selectedItems.IsEnabled = false;
						addBuyingInvoice.textBox_discount_selectedItems.IsReadOnly = true;
						addBuyingInvoice.textBox_companyReturn_selectedItems.IsReadOnly = true;
						addBuyingInvoice.checkBox_isRequestOrder_selectedItems.IsEnabled = false;
					}
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		private void saveAllBuyingItems() {
			try {
				BuyingItem buyingItem = null;
				StockItem stockItem = null;
				Item item = null;
				foreach(DataRow row in addBuyingInvoice.SelectedItems.Rows) {
					buyingItem = getBuyingItemById(Convert.ToInt32(row["ID"]));
					//buyingItem.BuyingPriceActual = ((Convert.ToDouble(row["Price"]) * Convert.ToDouble(row["Qty"])) / (Convert.ToDouble(row["Qty"]) + Convert.ToDouble(row["Free Qty"]))) - (((addBuyingInvoice.textBox_discount_selectedItems.DoubleValue / Convert.ToDouble(row["Line Total"])) * (buyingItem.BuyingPrice * buyingItem.Quantity)) / (buyingItem.Quantity + buyingItem.FreeQuantity));
					buyingItem.BuyingPriceActual = ((buyingItem.BuyingPrice * buyingItem.Quantity) / (buyingItem.Quantity + buyingItem.FreeQuantity)) - (((addBuyingInvoice.textBox_discount_selectedItems.DoubleValue / Convert.ToDouble(row["Line Total"])) * (buyingItem.BuyingPrice * buyingItem.Quantity)) / (buyingItem.Quantity + buyingItem.FreeQuantity));

					stockItem = stockManagerImpl.getStockItemByStockLocationIdAndItemId(buyingItem.StockLocationId, buyingItem.ItemId);
					item = itemManagerImpl.getItemById(buyingItem.ItemId);
					stockItem.Quantity += ((buyingItem.Quantity + buyingItem.FreeQuantity) * (buyingItem.BuyingMode == "p" ? item.QuantityPerPack : 1));
					stockManagerImpl.updStockItem(stockItem);
					updItem(buyingItem);
				}
			} catch(Exception) {
			}
		}

		private bool validateAddItemForm() {
			bool isOkay = true;
			try {
				if(addBuyingInvoice.SelectedItem == null) {
					isOkay = false;
					ShowMessage.error(Common.Messages.Error.Error008);
				} else {
					if(addBuyingInvoice.comboBox_sellingPricePerPack_selectItem.Value <= 0 && addBuyingInvoice.comboBox_sellingPricePerPack_selectItem.IsEnabled) {
						isOkay = false;
						addBuyingInvoice.comboBox_sellingPricePerPack_selectItem.ErrorMode(true);
					}
					if(addBuyingInvoice.comboBox_sellingPricePerUnit_selectItem.Value <= 0 && addBuyingInvoice.comboBox_sellingPricePerUnit_selectItem.IsEnabled) {
						isOkay = false;
						addBuyingInvoice.comboBox_sellingPricePerUnit_selectItem.ErrorMode(true);
					}
					if(addBuyingInvoice.textBox_buyingPrice_selectItem.IsNull()) {
						isOkay = false;
						addBuyingInvoice.textBox_buyingPrice_selectItem.ErrorMode(true);
					}
					if(addBuyingInvoice.textBox_buyingQuantity_selectItem.DoubleValue == 0 && addBuyingInvoice.textBox_buyingQuantityFree_selectItem.DoubleValue == 0) {
						isOkay = false;
						addBuyingInvoice.textBox_buyingQuantity_selectItem.ErrorMode(true);
					}
				}
			} catch(Exception) {
			}
			return isOkay;
		}

		internal void addItemToDataGrid() {
			try {
				if(validateAddItemForm()) {
					DataRow dr = addBuyingInvoice.SelectedItems.NewRow();
					dr[1] = addBuyingInvoice.SelectedItem.Id;
					dr[2] = addBuyingInvoice.SelectedItem.Name;
					dr[3] = addBuyingInvoice.radioButton_pack_buyingMode.IsChecked == true ? "Pack" : "Unit";
					dr[4] = addBuyingInvoice.comboBox_sellingPricePerUnit_selectItem.DisplayValue;
					dr[5] = addBuyingInvoice.comboBox_sellingPricePerPack_selectItem.DisplayValue;
					//dr[5] = unitManagerImpl.getUnitNameById(addBuyingInvoice.SelectedItem.UnitId);
					dr[6] = addBuyingInvoice.textBox_buyingPrice_selectItem.FormattedValue;
					dr[7] = addBuyingInvoice.textBox_buyingQuantity_selectItem.FormattedValue;
					dr[8] = addBuyingInvoice.textBox_buyingQuantityFree_selectItem.FormattedValue;
					dr[9] = (addBuyingInvoice.textBox_buyingPrice_selectItem.DoubleValue * addBuyingInvoice.textBox_buyingQuantity_selectItem.DoubleValue).ToString("#,##0.00");
					dr[10] = addBuyingInvoice.comboBox_stock_selectItem.Value;
					BuyingItem buyingItem = new BuyingItem();
					buyingItem.BuyingInvoiceId = addBuyingInvoice.BuyingInvoice.Id;
					buyingItem.ItemId = addBuyingInvoice.SelectedItem.Id;
					buyingItem.StockLocationId = addBuyingInvoice.comboBox_stock_selectItem.Value;
					buyingItem.BuyingPrice = addBuyingInvoice.textBox_buyingPrice_selectItem.DoubleValue;
					// TODO When Save
					buyingItem.BuyingPriceActual = 0;
					buyingItem.UnitSellingPrice = Convert.ToDouble(addBuyingInvoice.comboBox_sellingPricePerUnit_selectItem.SelectedValue);
					buyingItem.PackSellingPrice = Convert.ToDouble(addBuyingInvoice.comboBox_sellingPricePerPack_selectItem.SelectedValue);
					buyingItem.Quantity = addBuyingInvoice.textBox_buyingQuantity_selectItem.DoubleValue;
					buyingItem.FreeQuantity = addBuyingInvoice.textBox_buyingQuantityFree_selectItem.DoubleValue;
					buyingItem.BuyingMode = addBuyingInvoice.radioButton_unit_buyingMode.IsChecked == true ? "u" : "p";
					buyingItem.ExpiryDate = (DateTime)addBuyingInvoice.datePicker_expiryDate_selectItem.SelectedDate;
					CommonMethods.setCDMDForAdd(buyingItem);
					dr[0] = addItem(buyingItem);
					addBuyingInvoice.SelectedItems.Rows.Add(dr);
					resetAddItemForm();
					calculateSubTotal();
					calculateNetTotal();
					setItemCount();
				}
			} catch(Exception) {
			}
		}

		internal void updItemInDataGrid() {
			try {
				if(validateAddItemForm()) {
					DataRow dr = addBuyingInvoice.SelectedItems.Rows[addBuyingInvoice.UpdateItemSelectedIndex];

					BuyingItem buyingItem = getBuyingItemById(Convert.ToInt32(dr[0]));
					buyingItem.BuyingInvoiceId = addBuyingInvoice.BuyingInvoice.Id;
					//buyingItem.ItemId = addBuyingInvoice.SelectedItem.Id;
					buyingItem.StockLocationId = addBuyingInvoice.comboBox_stock_selectItem.Value;
					buyingItem.BuyingPrice = addBuyingInvoice.textBox_buyingPrice_selectItem.DoubleValue;
					buyingItem.BuyingPriceActual = 0;
					buyingItem.UnitSellingPrice = Convert.ToDouble(addBuyingInvoice.comboBox_sellingPricePerUnit_selectItem.SelectedValue);
					buyingItem.PackSellingPrice = Convert.ToDouble(addBuyingInvoice.comboBox_sellingPricePerPack_selectItem.SelectedValue);
					buyingItem.Quantity = addBuyingInvoice.textBox_buyingQuantity_selectItem.DoubleValue;
					buyingItem.FreeQuantity = addBuyingInvoice.textBox_buyingQuantityFree_selectItem.DoubleValue;
					buyingItem.BuyingMode = addBuyingInvoice.radioButton_unit_buyingMode.IsChecked == true ? "u" : "p";
					buyingItem.ExpiryDate = (DateTime)addBuyingInvoice.datePicker_expiryDate_selectItem.SelectedDate;
					CommonMethods.setCDMDForUpdate(buyingItem);
					//dr[0] = addItem(buyingItem);
					updItem(buyingItem);
					//dr[1] = addBuyingInvoice.SelectedItem.Id;
					dr[2] = addBuyingInvoice.SelectedItem.Name;
					dr[3] = addBuyingInvoice.radioButton_pack_buyingMode.IsChecked == true ? "Pack" : "Unit";
					dr[4] = addBuyingInvoice.comboBox_sellingPricePerUnit_selectItem.DisplayValue;
					dr[5] = addBuyingInvoice.comboBox_sellingPricePerPack_selectItem.DisplayValue;
					dr[6] = addBuyingInvoice.textBox_buyingPrice_selectItem.FormattedValue;
					dr[7] = addBuyingInvoice.textBox_buyingQuantity_selectItem.FormattedValue;
					dr[8] = addBuyingInvoice.textBox_buyingQuantityFree_selectItem.FormattedValue;
					dr[9] = (addBuyingInvoice.textBox_buyingPrice_selectItem.DoubleValue * addBuyingInvoice.textBox_buyingQuantity_selectItem.DoubleValue).ToString("#,##0.00");
					dr[10] = addBuyingInvoice.comboBox_stock_selectItem.Value;

					resetAddItemForm();
					addBuyingInvoice.IsItemUpdateMode = false;
					addBuyingInvoice.button_add_selectItem.Content = "Add";
					calculateSubTotal();
					calculateNetTotal();
				}
			} catch(Exception) {
			}
		}

		internal bool deleteSellingPrice(String mode) {
			bool b = false;
			try {
				if(mode == "u") {
					b = sellingPriceManagerImpl.deleteSellingPriceById(addBuyingInvoice.comboBox_sellingPricePerUnit_selectItem.Value);
					UIComboBox.sellingPriceForItemAndMode(addBuyingInvoice.comboBox_sellingPricePerUnit_selectItem, addBuyingInvoice.SelectedItem.Id, "u", addBuyingInvoice.AddSellingPriceUnit);
				} else {
					b = sellingPriceManagerImpl.deleteSellingPriceById(addBuyingInvoice.comboBox_sellingPricePerPack_selectItem.Value);
					UIComboBox.sellingPriceForItemAndMode(addBuyingInvoice.comboBox_sellingPricePerPack_selectItem, addBuyingInvoice.SelectedItem.Id, "p", addBuyingInvoice.AddSellingPricePack);
				}
			} catch(Exception) {
			}
			return b;
		}

		private void resetAddItemForm() {
			try {
				addBuyingInvoice.textBox_item_selectItem.Clear();
				addBuyingInvoice.textBox_code_selectItem.Clear();
				addBuyingInvoice.radioButton_unit_buyingMode.Content = "";
				addBuyingInvoice.radioButton_pack_buyingMode.Content = "";
				addBuyingInvoice.textBox_buyingQuantity_selectItem.Clear();
				addBuyingInvoice.textBox_buyingQuantityFree_selectItem.Clear();
				addBuyingInvoice.textBox_buyingPrice_selectItem.Clear();
				addBuyingInvoice.textBox_lineTotal_selectItem.Clear();
				addBuyingInvoice.comboBox_sellingPricePerUnit_selectItem.SelectedIndex = -1;
				addBuyingInvoice.comboBox_sellingPricePerPack_selectItem.SelectedIndex = -1;
				addBuyingInvoice.datePicker_expiryDate_selectItem.SelectedDate = DateTime.Today;
			} catch(Exception) {
			}
		}

		internal void populateUpdateItemForm() {
			try {
				resetAddItemForm();
				addBuyingInvoice.IsItemUpdateMode = true;
				addBuyingInvoice.button_add_selectItem.Content = "Update";
				addBuyingInvoice.UpdateItemSelectedIndex = addBuyingInvoice.dataGrid_selectedItems_selectedItems.SelectedIndex;

				DataRow dataRow_items = addBuyingInvoice.SelectedItems.Rows[addBuyingInvoice.UpdateItemSelectedIndex];
				addBuyingInvoice.textBox_itemId_selectItem.Text = dataRow_items["itemId"].ToString();

				if(Convert.ToString(dataRow_items["Mode"]) == "Pack") {
					addBuyingInvoice.radioButton_pack_buyingMode.IsChecked = true;
				} else {
					addBuyingInvoice.radioButton_unit_buyingMode.IsChecked = true;
				}
				addBuyingInvoice.textBox_buyingQuantity_selectItem.Text = Convert.ToDouble(dataRow_items["Qty"]).ToString();
				addBuyingInvoice.textBox_buyingQuantityFree_selectItem.Text = Convert.ToDouble(dataRow_items["Free Qty"]).ToString();
				addBuyingInvoice.textBox_buyingPrice_selectItem.Text = Convert.ToString(dataRow_items["Price"]);
				addBuyingInvoice.comboBox_sellingPricePerUnit_selectItem.Text = Convert.ToString(dataRow_items["USP"]);
				addBuyingInvoice.comboBox_sellingPricePerPack_selectItem.Text = Convert.ToString(dataRow_items["PSP"]);
				addBuyingInvoice.comboBox_stock_selectItem.SelectedValue = Convert.ToInt32(dataRow_items["stockId"]);
			} catch(Exception) {
			}
		}

		internal void calculateLineTotal() {
			try {
				addBuyingInvoice.textBox_lineTotal_selectItem.DoubleValue = addBuyingInvoice.textBox_buyingPrice_selectItem.DoubleValue * addBuyingInvoice.textBox_buyingQuantity_selectItem.DoubleValue;
			} catch(Exception) {
			}
		}

		internal void removeSelectedItem() {
			try {
				if(ShowMessage.confirm(MerchantSharpApp.Common.Messages.Information.Info013) == MessageBoxResult.Yes) {
					int index = addBuyingInvoice.dataGrid_selectedItems_selectedItems.SelectedIndex;
					deleteBuyingItemById(addBuyingInvoice.dataGrid_selectedItems_selectedItems.SelectedItemID);
					addBuyingInvoice.SelectedItems.Rows.RemoveAt(index);
					calculateSubTotal();
					calculateNetTotal();
					setItemCount();
					resetAddItemForm();
				}
			} catch(Exception) {
			}
		}

		private void setItemCount() {
			try {
				addBuyingInvoice.textBox_itemCount_selectedItems.Text = addBuyingInvoice.SelectedItems.Rows.Count.ToString();
			} catch(Exception) {
			}
		}

		internal void calculateNetTotal() {
			try {
				addBuyingInvoice.textBox_netTotal_selectedItems.DoubleValue = addBuyingInvoice.textBox_subTotal_selectedItems.DoubleValue - addBuyingInvoice.textBox_discount_selectedItems.DoubleValue - addBuyingInvoice.textBox_companyReturn_selectedItems.DoubleValue;
			} catch(Exception) {
			}
		}

		private void calculateSubTotal() {
			try {
				double subTotal = 0;
				foreach(DataRow row in addBuyingInvoice.SelectedItems.Rows) {
					subTotal += Convert.ToDouble(row["Price"]) * Convert.ToDouble(row["Qty"]);
				}
				addBuyingInvoice.textBox_subTotal_selectedItems.DoubleValue = subTotal;
			} catch(Exception) {
			}
		}

		internal void changePriceLabelName() {
			try {
				if(addBuyingInvoice.radioButton_pack_buyingMode.IsChecked == true) {
					addBuyingInvoice.label_buyingPrice_selectItem.Content = "Pack Buying Price";
				} else {
					addBuyingInvoice.label_buyingPrice_selectItem.Content = "Unit Buying Price";
				}
			} catch(Exception) {
			}
		}

		public void resetBuyingInvoiceUI() {
			try {
				addBuyingInvoice.textBox_invoiceNumber_basicDetails.Clear();
				addBuyingInvoice.textBox_grnNumber_basicDetails.Text = "Guessed(" + getNextGRN() + ")";
				addBuyingInvoice.datePicker_date_basicDetails.SelectedDate = DateTime.Today;
				addBuyingInvoice.comboBox_vendor_basicDetails.SelectedIndex = -1;
				addBuyingInvoice.datePicker_expectedPayingDate_basicDetails.SelectedDate = DateTime.Today;
				addBuyingInvoice.textBox_details_basicDetails.Clear();
				addBuyingInvoice.label_itemName_selectItem.Content = "";
				addBuyingInvoice.textBox_itemId_selectItem.Clear();
				resetAddItemForm();
				addBuyingInvoice.SelectedItems.Rows.Clear();
				//addBuyingInvoice.InvoiceId = 0;
				addBuyingInvoice.SelectedItem = null;
				addBuyingInvoice.IsInvoiceUpdateMode = false;
				addBuyingInvoice.BuyingInvoice = null;
				calculateSubTotal();
				calculateNetTotal();
				addBuyingInvoice.textBox_itemCount_selectedItems.Clear();

				addBuyingInvoice.button_add_selectItem.IsEnabled = true;
				addBuyingInvoice.dataGrid_selectedItems_selectedItems.IsEnabled = true;
				addBuyingInvoice.textBox_discount_selectedItems.IsReadOnly = false;
				addBuyingInvoice.textBox_companyReturn_selectedItems.IsReadOnly = false;
				addBuyingInvoice.checkBox_isRequestOrder_selectedItems.IsEnabled = true;
			} catch(Exception) {
			}
		}

		internal void loadAccountValueInPaymentSection() {
			try {
				if(addBuyingInvoice.comboBox_vendor_basicDetails.Value > 0) {
					addBuyingInvoice.PaymentSection.label_balance_vendorAccountSettlement.Content = vendorManagerImpl.getAccountBalanceById(addBuyingInvoice.comboBox_vendor_basicDetails.Value).ToString("#,##0.00");
				} else {
					addBuyingInvoice.PaymentSection.label_balance_vendorAccountSettlement.Content = "0.00";
				}
			} catch(Exception) {
			}
		}

		internal void addDiscountManager() {
			try {
				if(Session.Permission["canAccessDiscountManager"] == 1) {
					addBuyingInvoice.DiscountManager = new DiscountManager(addBuyingInvoice.SelectedItem.Id);
					addBuyingInvoice.DiscountManager.mainGrid.Width = 950;
					addBuyingInvoice.DiscountManager.mainGrid.Height = 450;
					Window window = new Window();
					window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
					window.Content = addBuyingInvoice.DiscountManager;
					window.Title = "Discount Manager";
					window.ShowDialog();
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
		}



		///////////////////////////////////////////////////////////////////////////////////////////////////
		// History
		///////////////////////////////////////////////////////////////////////////////////////////////////



		internal void buyingInvoiceHistoryLoaded() {
			try {
				UIComboBox.vendorsForFilter(buyingInvoiceHistory.comboBox_vendor_filter);
				UIComboBox.usersForFilter(buyingInvoiceHistory.comboBox_user_filter);
				UIComboBox.buyingInvoiceStatusForSelect(buyingInvoiceHistory.comboBox_status_filter);
				UIComboBox.yesNoForSelect(buyingInvoiceHistory.comboBox_isCompletelyPaid_filter);
				buyingInvoiceHistory.Pagination = new Pagination();
				buyingInvoiceHistory.Pagination.Filter = buyingInvoiceHistory;
				buyingInvoiceHistory.grid_pagination.Children.Add(buyingInvoiceHistory.Pagination);

				buyingInvoiceHistory.DataTable = new DataTable();
				buyingInvoiceHistory.DataTable.Columns.Add("ID", typeof(int));
				buyingInvoiceHistory.DataTable.Columns.Add("GRN", typeof(String));
				buyingInvoiceHistory.DataTable.Columns.Add("Invoice #", typeof(String));
				buyingInvoiceHistory.DataTable.Columns.Add("Date", typeof(String));
				buyingInvoiceHistory.DataTable.Columns.Add("Sub Total", typeof(String));
				buyingInvoiceHistory.DataTable.Columns.Add("Discount", typeof(String));
				buyingInvoiceHistory.DataTable.Columns.Add("Company Return", typeof(String));
				buyingInvoiceHistory.DataTable.Columns.Add("Net Total", typeof(String));
				buyingInvoiceHistory.DataTable.Columns.Add("Total Payments", typeof(String));
				buyingInvoiceHistory.DataTable.Columns.Add("Remainder", typeof(String));
				buyingInvoiceHistory.DataTable.Columns.Add("Account Balance Change", typeof(String));
				buyingInvoiceHistory.DataTable.Columns.Add("EPD", typeof(String));
				buyingInvoiceHistory.DataTable.Columns.Add("Vendor", typeof(String));
				buyingInvoiceHistory.DataTable.Columns.Add("User", typeof(String));
				buyingInvoiceHistory.DataTable.Columns.Add("Completely Paid", typeof(String));
				buyingInvoiceHistory.DataTable.Columns.Add("Status", typeof(String));
				buyingInvoiceHistory.dataGrid_buyingInvoiceHistory.DataContext = buyingInvoiceHistory.DataTable.DefaultView;
			} catch(Exception) {
			}
		}

		private BuyingInvoice getBuyingInvoiceForFilter() {
			BuyingInvoice buyingInvoice = null;
			try {
				buyingInvoice = new BuyingInvoice();
				buyingInvoice.Grn = buyingInvoiceHistory.textBox_grnNumber_filter.IsNull() ? null : buyingInvoiceHistory.textBox_grnNumber_filter.Text + "%";
				buyingInvoice.InvoiceNumber = buyingInvoiceHistory.textBox_invoiceNumber_filter.IsNull() ? null : "%" + buyingInvoiceHistory.textBox_invoiceNumber_filter.TrimedText + "%";
				buyingInvoice.VendorId = buyingInvoiceHistory.comboBox_vendor_filter.Value;
				buyingInvoice.CreatedBy = buyingInvoiceHistory.comboBox_user_filter.Value;
				if(buyingInvoiceHistory.datePicker_from_filter.SelectedDate != null || buyingInvoiceHistory.datePicker_to_filter.SelectedDate != null) {
					if(buyingInvoiceHistory.datePicker_from_filter.SelectedDate != null && buyingInvoiceHistory.datePicker_to_filter.SelectedDate != null) {
						buyingInvoice.OrderedDate = buyingInvoiceHistory.datePicker_from_filter.SelectedValue;
						buyingInvoice.addDateCondition("ordered_date", "BETWEEN", buyingInvoiceHistory.datePicker_from_filter.SelectedValue.ToString("yyyy-MM-dd"), buyingInvoiceHistory.datePicker_to_filter.SelectedValue.ToString("yyyy-MM-dd"));
					} else if(buyingInvoiceHistory.datePicker_from_filter.SelectedDate != null) {
						buyingInvoice.OrderedDate = buyingInvoiceHistory.datePicker_from_filter.SelectedValue;
					} else {
						buyingInvoice.OrderedDate = buyingInvoiceHistory.datePicker_to_filter.SelectedValue;
					}
				}
				if(buyingInvoiceHistory.datePicker_expectedPayingDateFrom_filter.SelectedDate != null || buyingInvoiceHistory.datePicker_expectedPayingDateTo_filter.SelectedDate != null) {
					if(buyingInvoiceHistory.datePicker_expectedPayingDateFrom_filter.SelectedDate != null && buyingInvoiceHistory.datePicker_expectedPayingDateTo_filter.SelectedDate != null) {
						buyingInvoice.ExpectedPayingDate = buyingInvoiceHistory.datePicker_expectedPayingDateFrom_filter.SelectedValue;
						buyingInvoice.addDateCondition("ordered_date", "BETWEEN", buyingInvoiceHistory.datePicker_expectedPayingDateFrom_filter.SelectedValue.ToString("yyyy-MM-dd"), buyingInvoiceHistory.datePicker_expectedPayingDateTo_filter.SelectedValue.ToString("yyyy-MM-dd"));
					} else if(buyingInvoiceHistory.datePicker_expectedPayingDateFrom_filter.SelectedDate != null) {
						buyingInvoice.ExpectedPayingDate = buyingInvoiceHistory.datePicker_expectedPayingDateFrom_filter.SelectedValue;
					} else {
						buyingInvoice.ExpectedPayingDate = buyingInvoiceHistory.datePicker_expectedPayingDateTo_filter.SelectedValue;
					}
				}
				buyingInvoice.Status = buyingInvoiceHistory.comboBox_status_filter.Value;
				buyingInvoice.IsCompletelyPaid = buyingInvoiceHistory.comboBox_isCompletelyPaid_filter.Value;
				buyingInvoice.Details = "%" + buyingInvoiceHistory.textBox_details_filter.TrimedText + "%";
			} catch(Exception) {
			}
			return buyingInvoice;
		}

		internal void filter() {
			try {
				buyingInvoiceHistory.DataTable.Rows.Clear();
				BuyingInvoice invoice = getBuyingInvoiceForFilter();
				invoice.LimitStart = buyingInvoiceHistory.Pagination.LimitStart;
				invoice.LimitEnd = buyingInvoiceHistory.Pagination.LimitCount;
				List<BuyingInvoice> list = getInvoice(invoice);
				DataRow row = null;
				foreach(BuyingInvoice buyingInvoice in list) {
					row = buyingInvoiceHistory.DataTable.NewRow();
					row[0] = buyingInvoice.Id;
					row[1] = buyingInvoice.Grn;
					row[2] = buyingInvoice.InvoiceNumber;
					row[3] = buyingInvoice.OrderedDate.ToString("yyyy-MM-dd");
					buyingInvoiceHistory.DataTable.Rows.Add(row);
				}
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				BuyingInvoice invoice = getBuyingInvoiceForFilter();
				invoice.RowsCount = 1;
				List<BuyingInvoice> list = getInvoice(invoice);
				buyingInvoiceHistory.Pagination.RowsCount = list[0].RowsCount;
			} catch(Exception) {
			}
		}
	}
}
