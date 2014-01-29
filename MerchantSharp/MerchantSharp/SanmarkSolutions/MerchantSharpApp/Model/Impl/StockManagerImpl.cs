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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class StockManagerImpl {

		private IDao stockLocationDao = null;
		private IDao stockItemDao = null;
		private StockManager stockManager;
		private ItemManagerImpl itemManagerImpl = null;
		private BuyingInvoiceManagerImpl buyingInvoiceManagerImpl = null;

		public StockManagerImpl() {
			stockLocationDao = StockLocationDao.getInstance();
			stockItemDao = StockItemDao.getInstance();
		}

		public StockManagerImpl(StockManager stockManager) {
			this.stockManager = stockManager;
			stockLocationDao = StockLocationDao.getInstance();
			stockItemDao = StockItemDao.getInstance();
			itemManagerImpl = new ItemManagerImpl();
			buyingInvoiceManagerImpl = new BuyingInvoiceManagerImpl();
		}

		public int addStockLocation(Entity entity) {
			try {
				if(Session.Permission["canAddStockLocation"] == 1) {
					return stockLocationDao.add(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return 0;
		}

		public bool delStockLocation(Entity entity) {
			return stockLocationDao.del(entity);
		}

		public List<StockLocation> getStockLocation(Entity entity) {
			return stockLocationDao.get(entity).Cast<StockLocation>().ToList();
		}

		public int updStockLocation(Entity entity) {
			try {
				if(Session.Permission["canUpdateStockLocation"] == 1) {
					return stockLocationDao.upd(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return 0;
		}

		/////////////

		public int addStockItem(Entity entity) {
			return stockItemDao.add(entity);
		}

		public bool delStockItem(Entity entity) {
			return stockItemDao.del(entity);
		}

		public List<StockItem> getStockItem(Entity entity) {
			return stockItemDao.get(entity).Cast<StockItem>().ToList();
		}

		public int updStockItem(Entity entity) {
			return stockItemDao.upd(entity);
		}


		////////////////////////////////////////////////////////////////////////////////////////////////////

		public List<StockLocation> getActivedStockLocations() {
			try {
				StockLocation sl = new StockLocation();
				sl.Status = 1;
				return getStockLocation(sl);
			} catch(Exception) {
				return null;
			}
		}

		public List<StockLocation> getStockLocations() {
			try {
				return getStockLocation(new StockLocation());
			} catch(Exception) {
				return null;
			}
		}

		public StockLocation getStockLocationById(int id) {
			StockLocation stockLocation = null;
			try {
				StockLocation sl = new StockLocation();
				sl.Id = id;
				List<StockLocation> list = getStockLocation(sl);
				if(list.Count == 1) {
					stockLocation = list[0];
				}
			} catch(Exception) {
			}
			return stockLocation;
		}

		public double getQuantityOfAllLocations(int itemId) {
			double d = 0;
			try {
				StockItem item = new StockItem();
				item.ItemId = itemId;
				List<StockItem> list = getStockItem(item);
				foreach(StockItem stockItem in list) {
					d += stockItem.Quantity;
				}
			} catch(Exception) {
			}
			return d;
		}

		public StockItem getStockItemByStockLocationIdAndItemId(int stockLocationId, int itemId) {
			StockItem stockItem = null;
			try {
				StockItem i = new StockItem();
				i.StockLocationId = stockLocationId;
				i.ItemId = itemId;
				stockItem = getStockItem(i)[0];
			} catch(Exception) {
			}
			return stockItem;
		}

		internal void addStockItemForItemId(int itemId) {
			try {
				List<StockLocation> list = getStockLocations();
				foreach(StockLocation location in list) {
					StockItem stockItem = new StockItem();
					stockItem.StockLocationId = location.Id;
					stockItem.ItemId = itemId;
					stockItem.Quantity = 0;
					CommonMethods.setCDMDForAdd(stockItem);
					addStockItem(stockItem);
				}
			} catch(Exception) {
			}
		}

		public StockItem getStockItemById(int id) {
			StockItem stockItem = null;
			try {
				StockItem i = new StockItem();
				i.Id = id;
				stockItem = getStockItem(i)[0];
			} catch(Exception) {
			}
			return stockItem;
		}

		public String getStockLocationNameById(int id) {
			String name = null;
			try {
				StockLocation i = new StockLocation();
				i.Id = id;
				name = getStockLocation(i)[0].Name;
			} catch(Exception) {
			}
			return name;
		}

		private double calculateValue(int theItemId, double theStockQuantity) {
			double returnValue = 0;

			try {
				Item item = itemManagerImpl.getItemById(theItemId);
				BuyingItem buyingItem_request = new BuyingItem();
				buyingItem_request.ItemId = theItemId;
				buyingItem_request.OrderBy = "modified_date DESC";
				buyingItem_request.LimitStart = 0;
				buyingItem_request.LimitEnd = 10;
				List<BuyingItem> list_buyingItem = buyingInvoiceManagerImpl.getItem(buyingItem_request);

				double itemsLeftForCalculating = theStockQuantity;
				double lastBuyingPrice = 0;
				String m = item.DefaultBuyingMode;
				double lastBPrice = 1;
				double quantity = 0;

				foreach(BuyingItem buyingItem_a in list_buyingItem) {
					lastBuyingPrice = buyingItem_a.BuyingPriceActual;
					m = buyingItem_a.BuyingMode;
					lastBPrice = buyingItem_a.BuyingPriceActual;
					quantity = buyingItem_a.BuyingMode == "p" ? (buyingItem_a.Quantity + buyingItem_a.FreeQuantity) * item.QuantityPerPack : (buyingItem_a.Quantity + buyingItem_a.FreeQuantity);
					if(itemsLeftForCalculating > 0) {
						if(itemsLeftForCalculating < (quantity)) {
							if(buyingItem_a.BuyingMode == "u") {
								returnValue += itemsLeftForCalculating * buyingItem_a.BuyingPriceActual;
							} else {
								returnValue += itemsLeftForCalculating * (buyingItem_a.BuyingPriceActual / item.QuantityPerPack);
							}
							itemsLeftForCalculating = 0;
						} else {
							returnValue += (buyingItem_a.Quantity + buyingItem_a.FreeQuantity) * buyingItem_a.BuyingPriceActual;
							itemsLeftForCalculating -= (quantity);
						}
					}
				}

				if(itemsLeftForCalculating > 0) {
					if(m == "u") {
						returnValue += lastBuyingPrice * itemsLeftForCalculating;
					} else {
						returnValue += (lastBuyingPrice * lastBPrice) * itemsLeftForCalculating;
					}
				}

			} catch(Exception) { }

			return returnValue;
		}

		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////



		internal void UserControl_Loaded() {
			try {
				UIComboBox.loadStocksForFilter(stockManager.comboBox_stockLocation);
				UIComboBox.categoriesForSelect(stockManager.comboBox_category);
				UIComboBox.companiesForSelect(stockManager.comboBox_company);

				if(Session.Meta["isActiveMultipleStocks"] == 0) {
					stockManager.comboBox_stockLocation.SelectedValue = 1;
					stockManager.comboBox_stockLocation.IsEnabled = false;
				}

				stockManager.DataTable = new DataTable();
				stockManager.DataTable.Columns.Add("ID", typeof(int));
				stockManager.DataTable.Columns.Add("Stock Location", typeof(String));
				stockManager.DataTable.Columns.Add("Category", typeof(String));
				stockManager.DataTable.Columns.Add("Company", typeof(String));
				stockManager.DataTable.Columns.Add("Item Name", typeof(String));
				stockManager.DataTable.Columns.Add("Quantity", typeof(double));
				stockManager.DataTable.Columns.Add("Re-Order Level", typeof(String));
				stockManager.DataTable.Columns.Add("Value", typeof(String));

				stockManager.DataGridFooter = new DataGridFooter();
				stockManager.dataGrid_stockItems.IFooter = stockManager.DataGridFooter;
				stockManager.grid_footer.Children.Add(stockManager.DataGridFooter);
				stockManager.dataGrid_stockItems.DataContext = stockManager.DataTable.DefaultView;

				stockManager.Pagination = new Pagination();
				stockManager.Pagination.Filter = stockManager;
				stockManager.grid_pagination.Children.Add(stockManager.Pagination);

				setRowsCount();
			} catch(Exception) {
			}
		}

		internal void filter() {
			try {
				DataSet dataSet = CommonManagerImpl.getStockForFilter(Convert.ToInt32(stockManager.comboBox_stockLocation.SelectedValue),
					(stockManager.textBox_itemName.IsNull() ? null : stockManager.textBox_itemName.Text), (stockManager.textBox_itemCode.IsNull() ? null : stockManager.textBox_itemCode.Text), (stockManager.textBox_barcode.IsNull() ? null : stockManager.textBox_barcode.Text),
					Convert.ToInt32(stockManager.comboBox_category.SelectedValue), Convert.ToInt32(stockManager.comboBox_company.SelectedValue),
					(stockManager.checkBox_belowReorderLevel.IsChecked == true ? true : false), false, stockManager.Pagination.LimitStart,
					stockManager.Pagination.LimitCount);

				stockManager.DataTable.Rows.Clear();
				foreach(DataRow row in dataSet.Tables[0].Rows) {
					stockManager.DataTable.Rows.Add(row[0], (Convert.ToInt32(stockManager.comboBox_stockLocation.SelectedValue) > 0 ? row[1] : "All"),
						row[3], row[4], row[2], Convert.ToDouble(row[5]), row[6],
						(
						Convert.ToInt32(stockManager.comboBox_stockLocation.SelectedValue) > 0 ? Convert.ToDouble(row[7]) :
						calculateValue(Convert.ToInt32(row[8]), Convert.ToDouble(row[5]))
						).ToString("#,##0.00"));
				}
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				DataSet dataSet = CommonManagerImpl.getStockForFilter(Convert.ToInt32(stockManager.comboBox_stockLocation.SelectedValue),
					(stockManager.textBox_itemName.IsNull() ? null : stockManager.textBox_itemName.Text), (stockManager.textBox_itemCode.IsNull() ? null : stockManager.textBox_itemCode.Text), (stockManager.textBox_barcode.IsNull() ? null : stockManager.textBox_barcode.Text),
					Convert.ToInt32(stockManager.comboBox_category.SelectedValue), Convert.ToInt32(stockManager.comboBox_company.SelectedValue),
					(stockManager.checkBox_belowReorderLevel.IsChecked == true ? true : false), true, stockManager.Pagination.LimitStart,
					stockManager.Pagination.LimitCount);
				/*DataSet dataSet = CommonManagerImpl.getStockForFilter(Convert.ToInt32(stockManager.comboBox_stockLocation.SelectedValue),
					stockManager.textBox_itemName.Text, stockManager.textBox_itemCode.Text, stockManager.textBox_barcode.Text,
					Convert.ToInt32(stockManager.comboBox_category.SelectedValue), Convert.ToInt32(stockManager.comboBox_company.SelectedValue),
					(stockManager.checkBox_belowReorderLevel.IsChecked == true ? true : false), true, stockManager.Pagination.LimitStart,
					stockManager.Pagination.LimitCount);*/
				stockManager.Pagination.RowsCount = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
			} catch(Exception) {
			}
		}

		internal bool updateStock() {
			bool b = false;
			try {
				if(Session.Permission["canUpdateStock"] == 1) {
					foreach(DataRow row in stockManager.DataTable.Rows) {
						StockItem stockItem = getStockItemById(Convert.ToInt32(row[0]));
						stockItem.Quantity = Convert.ToDouble(row["Quantity"]);
						CommonMethods.setCDMDForUpdate(stockItem);
						updStockItem(stockItem);
					}
					b = true;
					setRowsCount();
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return b;
		}
	}
}
