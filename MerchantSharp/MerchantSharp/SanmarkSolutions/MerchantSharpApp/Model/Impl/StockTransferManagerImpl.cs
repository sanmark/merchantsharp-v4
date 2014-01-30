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
using System.Windows;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class StockTransferManagerImpl {

		private AddStockTransfer addStockTransfer;
		private IDao stockTransferDao = null;
		private IDao stockTransferItemDao = null;

		private ItemManagerImpl itemManagerImpl = null;
		private CompanyManagerImpl companyManagerImpl = null;
		private StockManagerImpl stockManagerImpl = null;
		private StockTransferHistory stockTransferHistory;

		public StockTransferManagerImpl() {
			stockTransferDao = StockTransferDao.getInstance();
			stockTransferItemDao = StockTransferItemDao.getInstance();
		}

		public StockTransferManagerImpl(AddStockTransfer addStockTransfer) {
			this.addStockTransfer = addStockTransfer;
			stockTransferDao = StockTransferDao.getInstance();
			stockTransferItemDao = StockTransferItemDao.getInstance();

			itemManagerImpl = new ItemManagerImpl();
			companyManagerImpl = new CompanyManagerImpl();
			stockManagerImpl = new StockManagerImpl();
		}

		public StockTransferManagerImpl(StockTransferHistory stockTransferHistory) {
			this.stockTransferHistory = stockTransferHistory;
			stockTransferDao = StockTransferDao.getInstance();
			stockTransferItemDao = StockTransferItemDao.getInstance();

			stockManagerImpl = new StockManagerImpl();
		}

		public int addTransfer(Entity entity) {
			try {
				if(Session.Permission["canAddStockTransfer"] == 1) {
					return stockTransferDao.add(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return 0;
		}

		public bool delTransfer(Entity entity) {
			return stockTransferDao.del(entity);
		}

		public List<StockTransfer> getTransfer(Entity entity) {
			return stockTransferDao.get(entity).Cast<StockTransfer>().ToList();
		}

		public int updTransfer(Entity entity) {
			return stockTransferDao.upd(entity);
		}

		/////////////

		public int addTransferItem(Entity entity) {
			return stockTransferItemDao.add(entity);
		}

		public bool delTransferItem(Entity entity) {
			return stockTransferItemDao.del(entity);
		}

		public List<StockTransferItem> getTransferItem(Entity entity) {
			return stockTransferItemDao.get(entity).Cast<StockTransferItem>().ToList();
		}

		public int updTransferItem(Entity entity) {
			return stockTransferItemDao.upd(entity);
		}

		/////////////

		public StockTransfer getStockTransferById(int id) {
			StockTransfer stockTransfer = null;
			try {
				StockTransfer st = new StockTransfer();
				st.Id = id;
				List<StockTransfer> list = getTransfer(st);
				if(list.Count == 1) {
					stockTransfer = list[0];
				}
			} catch(Exception) {
			}
			return stockTransfer;
		}

		////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////

		internal void addStockTransferLoaded() {
			try {
				addStockTransfer.ItemFinder = new ItemFinder(addStockTransfer.textBox_itemId);
				addStockTransfer.grid_itemFinder.Children.Add(addStockTransfer.ItemFinder);

				UIComboBox.loadStocks(addStockTransfer.comboBox_from_selectStock, null);
				UIComboBox.loadStocks(addStockTransfer.comboBox_to_selectStock, null);

				addStockTransfer.comboBox_from_selectStock.SelectedValue = Convert.ToInt32(Session.Preference["defaultBuyingStock"]);
				addStockTransfer.comboBox_to_selectStock.SelectedValue = Convert.ToInt32(Session.Preference["defaultSellingStock"]);

				addStockTransfer.DataTable = new DataTable();
				addStockTransfer.DataTable.Columns.Add("ID", typeof(int));
				addStockTransfer.DataTable.Columns.Add("Item Name", typeof(String));
				addStockTransfer.DataTable.Columns.Add("Quantity", typeof(String));

				addStockTransfer.dataGrid.DataContext = addStockTransfer.DataTable.DefaultView;
				if(addStockTransfer.IsViewMode) {
					loadAllItemsToView();
				}
			} catch(Exception) {
			}
		}

		private void loadAllItemsToView() {
			try {
				addStockTransfer.grid_itemFinder.IsEnabled = false;
				addStockTransfer.button_saveTransfer.IsEnabled = false;
				addStockTransfer.button_addItem.IsEnabled = false;
				addStockTransfer.dataGrid.IsEnabled = false;
				StockTransfer stockTransfer = getStockTransferById(addStockTransfer.SelectedStockTransferID);
				if(stockTransfer != null) {
					addStockTransfer.comboBox_from_selectStock.SelectedValue = stockTransfer.FromLocationId;
					addStockTransfer.comboBox_to_selectStock.SelectedValue = stockTransfer.ToLocationId;
					addStockTransfer.datePicker_date_selectStock.SelectedDate = stockTransfer.Date;
					addStockTransfer.textBox_carrierName_selectStock.Text = stockTransfer.Carrier;
					addStockTransfer.textBox_details_selectStock.Text = stockTransfer.Details;

					StockTransferItem sti = new StockTransferItem();
					sti.StockTransferId = stockTransfer.Id;
					List<StockTransferItem> list = getTransferItem(sti);
					foreach(StockTransferItem stockTransferItem in list) {
						Item item = itemManagerImpl.getItemById(stockTransferItem.ItemId);
						addStockTransfer.DataTable.Rows.Add(stockTransferItem.Id, item.Name + " (" + companyManagerImpl.getCompanyNameById(item.CompanyId) + ")", stockTransferItem.Quantity);
					}
				}
			} catch(Exception) {
			}
		}

		internal void loadAvailableQuantitiesForMode() {
			try {
				StockItem stockItemFrom = stockManagerImpl.getStockItemByStockLocationIdAndItemId(Convert.ToInt32(addStockTransfer.comboBox_from_selectStock.SelectedValue), addStockTransfer.SelectedItem.Id);
				StockItem stockItemTo = stockManagerImpl.getStockItemByStockLocationIdAndItemId(Convert.ToInt32(addStockTransfer.comboBox_to_selectStock.SelectedValue), addStockTransfer.SelectedItem.Id);
				if(addStockTransfer.radioButton_pack.IsChecked == true) {
					addStockTransfer.label_availableQuantity_from.Content = (stockItemFrom.Quantity / addStockTransfer.SelectedItem.QuantityPerPack).ToString("#,##0.00");
					addStockTransfer.label_availableQuantity_to.Content = (stockItemTo.Quantity / addStockTransfer.SelectedItem.QuantityPerPack).ToString("#,##0.00");
				} else {
					addStockTransfer.label_availableQuantity_from.Content = stockItemFrom.Quantity.ToString("#,##0.00");
					addStockTransfer.label_availableQuantity_to.Content = stockItemTo.Quantity.ToString("#,##0.00");
				}
				addStockTransfer.textBox_quantity.Focus();
			} catch(Exception) {
			}
		}

		internal void populateAddItem() {
			try {
				Item item = itemManagerImpl.getItemById(Convert.ToInt32(addStockTransfer.textBox_itemId.Text));
				addStockTransfer.SelectedItem = item;
				addStockTransfer.label_itemName.Content = item.Name + " (" + companyManagerImpl.getCompanyNameById(item.CompanyId) + ")";

				if(item.DefaultBuyingMode == "u") {
					addStockTransfer.radioButton_unit.IsChecked = true;
				} else {
					addStockTransfer.radioButton_pack.IsChecked = true;
				}
				loadAvailableQuantitiesForMode();
			} catch(Exception) {
			}
		}

		private void resetAddForm() {
			try {
				addStockTransfer.SelectedItem = null;
				addStockTransfer.textBox_itemId.Clear();
				addStockTransfer.label_itemName.Content = "";
				addStockTransfer.radioButton_unit.IsChecked = false;
				addStockTransfer.radioButton_pack.IsChecked = false;
				addStockTransfer.label_availableQuantity_from.Content = "";
				addStockTransfer.label_availableQuantity_to.Content = "";
				addStockTransfer.textBox_quantity.Clear();
			} catch(Exception) {
			}
		}

		internal void addItemToTable() {
			try {
				if(addStockTransfer.SelectedItem == null) {
					ShowMessage.error(Common.Messages.Error.Error008);
				} else if(addStockTransfer.textBox_quantity.DoubleValue <= 0) {
					addStockTransfer.textBox_quantity.ErrorMode(true);
				} else {
					addStockTransfer.DataTable.Rows.Add(addStockTransfer.SelectedItem.Id, addStockTransfer.label_itemName.Content,
						(addStockTransfer.radioButton_unit.IsChecked == true) ? addStockTransfer.textBox_quantity.FormattedValue :
						(addStockTransfer.textBox_quantity.DoubleValue * addStockTransfer.SelectedItem.QuantityPerPack).ToString("#,##0.00"));
					resetAddForm();
				}
			} catch(Exception) {
			}
		}

		internal void removeSelectedItem() {
			try {
				if(ShowMessage.confirm(MerchantSharpApp.Common.Messages.Information.Info013) == MessageBoxResult.Yes) {
					int index = addStockTransfer.dataGrid.SelectedIndex;
					addStockTransfer.DataTable.Rows.RemoveAt(index);
				}
			} catch(Exception) {
			}
		}

		internal bool saveTransfer() {
			bool b = false;
			try {
				bool isOkay = true;
				if(addStockTransfer.textBox_carrierName_selectStock.IsNull()) {
					addStockTransfer.textBox_carrierName_selectStock.ErrorMode(true);
					isOkay = false;
				}
				if(addStockTransfer.datePicker_date_selectStock.SelectedDate == null) {
					addStockTransfer.datePicker_date_selectStock.ErrorMode(true);
					isOkay = false;
				}
				if(Convert.ToInt32(addStockTransfer.comboBox_to_selectStock.SelectedValue) <= 0) {
					addStockTransfer.comboBox_to_selectStock.ErrorMode(true);
					isOkay = false;
				}
				if(Convert.ToInt32(addStockTransfer.comboBox_from_selectStock.SelectedValue) <= 0) {
					addStockTransfer.comboBox_from_selectStock.ErrorMode(true);
					isOkay = false;
				}
				if(Convert.ToInt32(addStockTransfer.comboBox_to_selectStock.SelectedValue) == Convert.ToInt32(addStockTransfer.comboBox_from_selectStock.SelectedValue)) {
					addStockTransfer.comboBox_from_selectStock.ErrorMode(true);
					addStockTransfer.comboBox_to_selectStock.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					if(addStockTransfer.DataTable.Rows.Count > 0) {
						StockTransfer stockTransfer = new StockTransfer();
						stockTransfer.FromLocationId = Convert.ToInt32(addStockTransfer.comboBox_from_selectStock.SelectedValue);
						stockTransfer.ToLocationId = Convert.ToInt32(addStockTransfer.comboBox_to_selectStock.SelectedValue);
						stockTransfer.Date = Convert.ToDateTime(addStockTransfer.datePicker_date_selectStock.SelectedDate);
						stockTransfer.Carrier = addStockTransfer.textBox_carrierName_selectStock.TrimedText;
						stockTransfer.Details = addStockTransfer.textBox_details_selectStock.Text;
						CommonMethods.setCDMDForAdd(stockTransfer);
						int stockTransferId = addTransfer(stockTransfer);

						StockTransferItem stockTransferItem = null;
						StockItem stockItemFrom = null;
						StockItem stockItemTo = null;
						foreach(DataRow row in addStockTransfer.DataTable.Rows) {
							stockTransferItem = new StockTransferItem();
							stockTransferItem.StockTransferId = stockTransferId;
							stockTransferItem.ItemId = Convert.ToInt32(row["ID"]);
							stockTransferItem.Quantity = Convert.ToDouble(row["Quantity"]);
							CommonMethods.setCDMDForAdd(stockTransferItem);
							stockItemFrom = stockManagerImpl.getStockItemByStockLocationIdAndItemId(Convert.ToInt32(addStockTransfer.comboBox_from_selectStock.SelectedValue), Convert.ToInt32(row["ID"]));
							stockItemTo = stockManagerImpl.getStockItemByStockLocationIdAndItemId(Convert.ToInt32(addStockTransfer.comboBox_to_selectStock.SelectedValue), Convert.ToInt32(row["ID"]));
							stockItemFrom.Quantity -= stockTransferItem.Quantity;
							stockItemTo.Quantity += stockTransferItem.Quantity;
							addTransferItem(stockTransferItem);
							stockManagerImpl.updStockItem(stockItemFrom);
							stockManagerImpl.updStockItem(stockItemTo);
						}
						b = true;
					} else {
						ShowMessage.error(Common.Messages.Error.Error012);
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void resetUI() {
			try {
				addStockTransfer.DataTable.Rows.Clear();
				resetAddForm();
			} catch(Exception) {
			}
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////


		internal void stockTransferHistoryLoaded() {
			try {
				stockTransferHistory.DataTable = new DataTable();
				stockTransferHistory.DataTable.Columns.Add("ID", typeof(int));
				stockTransferHistory.DataTable.Columns.Add("Stock Location From", typeof(String));
				stockTransferHistory.DataTable.Columns.Add("Stock Location To", typeof(String));
				stockTransferHistory.DataTable.Columns.Add("Date", typeof(String));
				stockTransferHistory.DataTable.Columns.Add("Carrier", typeof(String));
				stockTransferHistory.DataTable.Columns.Add("Details", typeof(String));
				stockTransferHistory.dataGrid_stockTransferHistory.DataContext = stockTransferHistory.DataTable.DefaultView;

				UIComboBox.loadStocksForFilter(stockTransferHistory.comboBox_stockLocationFrom_filter);
				UIComboBox.loadStocksForFilter(stockTransferHistory.comboBox_stockLocationTo_filter);

				stockTransferHistory.comboBox_stockLocationFrom_filter.SelectedValue = -1;
				stockTransferHistory.comboBox_stockLocationTo_filter.SelectedValue = -1;

				stockTransferHistory.Pagination = new Pagination();
				stockTransferHistory.Pagination.Filter = stockTransferHistory;
				stockTransferHistory.grid_pagination.Children.Add(stockTransferHistory.Pagination);

				setRowsCount();
			} catch(Exception) {
			}
		}

		private StockTransfer getStockTransferForFilter() {
			StockTransfer stockTransfer = null;
			try {
				stockTransfer = new StockTransfer();
				stockTransfer.FromLocationId = Convert.ToInt32(stockTransferHistory.comboBox_stockLocationFrom_filter.SelectedValue);
				stockTransfer.ToLocationId = Convert.ToInt32(stockTransferHistory.comboBox_stockLocationTo_filter.SelectedValue);
				if(stockTransferHistory.datePicker_from_filter.SelectedDate != null && stockTransferHistory.datePicker_to_filter.SelectedDate != null) {
					stockTransfer.addDateCondition("date", "BETWEEN",
					Convert.ToDateTime(stockTransferHistory.datePicker_from_filter.SelectedDate).ToString("yyyy-MM-dd"),
					Convert.ToDateTime(stockTransferHistory.datePicker_to_filter.SelectedDate).ToString("yyyy-MM-dd"));
				} else if(stockTransferHistory.datePicker_from_filter.SelectedDate != null) {
					stockTransfer.Date = Convert.ToDateTime(stockTransferHistory.datePicker_from_filter.SelectedDate);
				} else if(stockTransferHistory.datePicker_to_filter.SelectedDate != null) {
					stockTransfer.Date = Convert.ToDateTime(stockTransferHistory.datePicker_to_filter.SelectedDate);
				}
				stockTransfer.Carrier = "%" + stockTransferHistory.textBox_carrier_filter.TrimedText + "%";
				stockTransfer.Details = "%" + stockTransferHistory.textBox_details_filter.Text + "%";
			} catch(Exception) {
			}
			return stockTransfer;
		}

		internal void filter() {
			try {
				stockTransferHistory.DataTable.Rows.Clear();
				StockTransfer s = getStockTransferForFilter();
				s.LimitStart = stockTransferHistory.Pagination.LimitStart;
				s.LimitEnd = stockTransferHistory.Pagination.LimitCount;
				List<StockTransfer> list = getTransfer(s);
				DataRow row = null;
				foreach(StockTransfer stockTransfer in list) {
					row = stockTransferHistory.DataTable.NewRow();
					row[0] = stockTransfer.Id;
					row[1] = stockManagerImpl.getStockLocationNameById(stockTransfer.FromLocationId);
					row[2] = stockManagerImpl.getStockLocationNameById(stockTransfer.ToLocationId);
					row[3] = stockTransfer.Date.ToString("yyyy-MM-dd");
					row[4] = stockTransfer.Carrier;
					row[5] = stockTransfer.Details;

					stockTransferHistory.DataTable.Rows.Add(row);
				}
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				StockTransfer stockTransfer = getStockTransferForFilter();
				stockTransfer.RowsCount = 1;
				List<StockTransfer> list = getTransfer(stockTransfer);
				stockTransferHistory.Pagination.RowsCount = list[0].RowsCount;
			} catch(Exception) {
			}
		}
	}
}
