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
	class CompanyReturnManagerImpl {

		private AddCompanyReturn addCompanyReturn;
		private IDao dao = null;
		private ItemManagerImpl itemManagerImpl = null;
		private SellingPriceManagerImpl sellingPriceManagerImpl = null;
		private StockManagerImpl stockManagerImpl = null;
		private CompanyReturnHistory companyReturnHistory;

		public CompanyReturnManagerImpl() {
			dao = CompanyReturnDao.getInstance();
		}

		public CompanyReturnManagerImpl( AddCompanyReturn addCompanyReturn ) {
			this.addCompanyReturn = addCompanyReturn;
			dao = CompanyReturnDao.getInstance();
			itemManagerImpl = new ItemManagerImpl();
			sellingPriceManagerImpl = new SellingPriceManagerImpl();
			stockManagerImpl = new StockManagerImpl();
		}

		public CompanyReturnManagerImpl( CompanyReturnHistory companyReturnHistory ) {
			this.companyReturnHistory = companyReturnHistory;
			dao = CompanyReturnDao.getInstance();
		}

		//////////////////////////////////////////////////////////////////////////////////
		public int add( Entity entity ) {
			int id = 0;
			try {
				id = dao.add(entity);
				/*if ( Session.Permission["canAddUnit"] == 1 ) {
					return dao.add(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}*/
			} catch ( Exception ) {
			}
			return id;
		}

		public bool del( Entity entity ) {
			return dao.del(entity);
		}

		public List<CompanyReturn> get( Entity entity ) {
			return dao.get(entity).Cast<CompanyReturn>().ToList();
		}

		public int upd( Entity entity ) {
			int id = 0;
			try {
				id = dao.upd(entity);
				/*if ( Session.Permission["canUpdateUnit"] == 1 ) {
					return dao.upd(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}*/
			} catch ( Exception ) {
			}
			return id;
		}
		//////////////////////////////////////////////////////////////////////////////////

		private List<CompanyReturn> getAllReturnedItemsByBuyingInvoiceId( int buyingInvoiceId ) {
			List<CompanyReturn> list = null;
			try {
				CompanyReturn cr = new CompanyReturn();
				cr.BuyingInvoiceId = buyingInvoiceId;
				list = get(cr);
			} catch ( Exception ) {
			}
			return list;
		}

		public double getReturnItemsValueByInvoiceId( int buyingInvoiceId ) {
			double d = 0;
			try {
				List<CompanyReturn> list = getAllReturnedItemsByBuyingInvoiceId(buyingInvoiceId);
				if ( list != null ) {
					foreach ( CompanyReturn companyReturn in list ) {
						d += companyReturn.Price * companyReturn.Quantity;
					}
				}
			} catch ( Exception ) {
			}
			return d;
		}


		//////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////

		internal void Add_Window_Loaded() {
			try {
				addCompanyReturn.DataTable = new DataTable();
				addCompanyReturn.DataTable.Columns.Add("ID", typeof(int));
				addCompanyReturn.DataTable.Columns.Add("Date", typeof(String));
				addCompanyReturn.DataTable.Columns.Add("Item Name", typeof(String));
				addCompanyReturn.DataTable.Columns.Add("Price", typeof(String));
				addCompanyReturn.DataTable.Columns.Add("Quantity", typeof(String));
				addCompanyReturn.DataTable.Columns.Add("Line Total", typeof(String));
				addCompanyReturn.dataGrid.DataContext = addCompanyReturn.DataTable.DefaultView;

				addCompanyReturn.ItemFinder = new ItemFinder(addCompanyReturn.textBox_itemId);
				addCompanyReturn.grid_itemFinder.Children.Add(addCompanyReturn.ItemFinder);
				loadAllReturnedItems();
			} catch ( Exception ) {
			}
		}

		private void loadAllReturnedItems() {
			try {
				List<CompanyReturn> list = getAllReturnedItemsByBuyingInvoiceId(addCompanyReturn.BuyingInvoiceId);
				if ( list != null ) {
					foreach ( CompanyReturn companyReturn in list ) {
						addCompanyReturn.DataTable.Rows.Add(companyReturn.Id, companyReturn.Date.ToString("yyyy-MM-dd"),
							itemManagerImpl.getItemNameById(companyReturn.ItemId), companyReturn.Price.ToString("#,##0.00"),
							companyReturn.Quantity.ToString("#,##0.00"), ( companyReturn.Price * companyReturn.Quantity ).ToString("#,##0.00"));
					}
				}
			} catch ( Exception ) {
			}
		}

		internal void selectItemById() {
			try {
				addCompanyReturn.SelectedItem = itemManagerImpl.getItemById(addCompanyReturn.textBox_itemId.IntValue);
				addCompanyReturn.label_itemName.Content = addCompanyReturn.SelectedItem.Name;
				addCompanyReturn.textBox_price.DoubleValue = sellingPriceManagerImpl.getSellingPriceByItemAndMode(addCompanyReturn.SelectedItem.Id, "u")[0].Price;
				addCompanyReturn.textBox_quantity.Focus();
			} catch ( Exception ) {
			}
		}

		private bool isValidForm() {
			bool b = true;
			try {
				if ( addCompanyReturn.SelectedItem == null ) {
					ShowMessage.error(Common.Messages.Error.Error008);
					b = false;
				}
				if ( addCompanyReturn.textBox_quantity.DoubleValue < 0 ) {
					addCompanyReturn.textBox_quantity.ErrorMode(true);
					b = false;
				}
				if ( addCompanyReturn.textBox_price.DoubleValue < 0 ) {
					addCompanyReturn.textBox_price.ErrorMode(true);
					b = false;
				}
				if ( addCompanyReturn.datePicker_date.SelectedDate == null ) {
					addCompanyReturn.datePicker_date.ErrorMode(true);
					b = false;
				}
			} catch ( Exception ) {
			}
			return b;
		}

		internal bool addReturnItem() {
			bool b = false;
			try {
				if ( isValidForm() ) {
					CompanyReturn companyReturn = new CompanyReturn();
					companyReturn.BuyingInvoiceId = addCompanyReturn.BuyingInvoiceId;
					companyReturn.ItemId = addCompanyReturn.SelectedItem.Id;
					companyReturn.Date = addCompanyReturn.datePicker_date.SelectedValue;
					companyReturn.Price = addCompanyReturn.textBox_price.DoubleValue;
					companyReturn.Quantity = addCompanyReturn.textBox_quantity.DoubleValue;
					CommonMethods.setCDMDForAdd(companyReturn);
					if ( add(companyReturn) > 0 ) {
						StockItem stockItem = stockManagerImpl.getStockItemByStockLocationIdAndItemId(Convert.ToInt32(Session.Preference["defaultCompanyReturnStock"]), addCompanyReturn.SelectedItem.Id);
						stockItem.Quantity -= addCompanyReturn.textBox_quantity.DoubleValue;
						CommonMethods.setCDMDForUpdate(stockItem);
						stockManagerImpl.updStockItem(stockItem);
						b = true;
						loadAllReturnedItems();
					}
				}
			} catch ( Exception ) {
			}
			return b;
		}

		//////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////


		internal void CompanyReturnHistoryLoaded() {
			try {
				UIComboBox.vendorsForFilter(companyReturnHistory.comboBox_vendor);
				companyReturnHistory.Pagination = new Pagination();
				companyReturnHistory.Pagination.Filter = companyReturnHistory;
				companyReturnHistory.grid_pagination.Children.Add(companyReturnHistory.Pagination);

				companyReturnHistory.DataTable = new DataTable();
				companyReturnHistory.DataTable.Columns.Add("ID", typeof(int));
				companyReturnHistory.DataTable.Columns.Add("Item Name", typeof(String));
				companyReturnHistory.DataTable.Columns.Add("Vendor", typeof(String));
				companyReturnHistory.DataTable.Columns.Add("Invoice #", typeof(String));
				companyReturnHistory.DataTable.Columns.Add("GRN", typeof(String));
				companyReturnHistory.DataTable.Columns.Add("Date", typeof(String));
				companyReturnHistory.DataTable.Columns.Add("Price", typeof(String));
				companyReturnHistory.DataTable.Columns.Add("Quantity", typeof(String));
				companyReturnHistory.DataTable.Columns.Add("Line Total", typeof(String));

				companyReturnHistory.DataGridFooter = new DataGridFooter();
				companyReturnHistory.dataGrid.IFooter = companyReturnHistory.DataGridFooter;
				companyReturnHistory.grid_footer.Children.Add(companyReturnHistory.DataGridFooter);
				companyReturnHistory.dataGrid.DataContext = companyReturnHistory.DataTable.DefaultView;

				companyReturnHistory.dataGrid.Columns[1].MinWidth = 300;
				companyReturnHistory.dataGrid.Columns[1].MaxWidth = 350;
				companyReturnHistory.dataGrid.Columns[2].Width = 200;
				companyReturnHistory.dataGrid.Columns[3].Width = 150;
				companyReturnHistory.dataGrid.Columns[4].Width = 100;
				companyReturnHistory.dataGrid.Columns[5].Width = 100;
				companyReturnHistory.dataGrid.Columns[6].MinWidth = 100;
				companyReturnHistory.dataGrid.Columns[7].Width = 100;
				companyReturnHistory.dataGrid.Columns[8].Width = 150;

				setCompanyReturnRowsCount();
			} catch ( Exception ) {
			}
		}

		internal void filterCompanyReturn() {
			try {
				DataSet dataSet = CommonManagerImpl.getCompanyReturnForFilter(companyReturnHistory.textBox_itemName.Text, companyReturnHistory.textBox_itemCode.Text,
					companyReturnHistory.textBox_barcode.Text, Convert.ToInt32(companyReturnHistory.comboBox_vendor.SelectedValue),
					companyReturnHistory.textBox_invoice.Text, companyReturnHistory.textBox_grn.Text,
					( companyReturnHistory.datePicker_from.SelectedDate != null ? Convert.ToDateTime(companyReturnHistory.datePicker_from.SelectedDate).ToString("yyyy-MM-dd") : null ),
					( companyReturnHistory.datePicker_to.SelectedDate != null ? Convert.ToDateTime(companyReturnHistory.datePicker_to.SelectedDate).ToString("yyyy-MM-dd") : null ),
					false, companyReturnHistory.Pagination.LimitStart, companyReturnHistory.Pagination.LimitCount);

				companyReturnHistory.DataTable.Rows.Clear();
				foreach ( DataRow row in dataSet.Tables[0].Rows ) {
					companyReturnHistory.DataTable.Rows.Add(row[0], row[1], row[2], row[3], row[4], Convert.ToDateTime(row[5]).ToString("yyyy-MM-dd"),
						Convert.ToDouble(row[6]).ToString("#,##0.00"), Convert.ToDouble(row[7]).ToString("#,##0.00"), Convert.ToDouble(row[8]).ToString("#,##0.00"));
				}
			} catch ( Exception ) {
			}
		}

		internal void setCompanyReturnRowsCount() {
			try {
				DataSet dataSet = CommonManagerImpl.getCompanyReturnForFilter(companyReturnHistory.textBox_itemName.Text, companyReturnHistory.textBox_itemCode.Text,
					companyReturnHistory.textBox_barcode.Text, Convert.ToInt32(companyReturnHistory.comboBox_vendor.SelectedValue),
					companyReturnHistory.textBox_invoice.Text, companyReturnHistory.textBox_grn.Text,
					( companyReturnHistory.datePicker_from.SelectedDate != null ? Convert.ToDateTime(companyReturnHistory.datePicker_from.SelectedDate).ToString("yyyy-MM-dd") : null ),
					( companyReturnHistory.datePicker_to.SelectedDate != null ? Convert.ToDateTime(companyReturnHistory.datePicker_to.SelectedDate).ToString("yyyy-MM-dd") : null ),
					true, companyReturnHistory.Pagination.LimitStart, companyReturnHistory.Pagination.LimitCount);
				companyReturnHistory.Pagination.RowsCount = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);				
			} catch ( Exception ) {
			}
		}
	}
}
