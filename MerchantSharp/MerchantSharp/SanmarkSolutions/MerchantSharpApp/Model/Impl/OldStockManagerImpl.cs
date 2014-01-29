using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class OldStockManagerImpl {

		private OldStockBySellingInvoice oldStockBySellingInvoice;

		public OldStockManagerImpl(OldStockBySellingInvoice oldStockBySellingInvoice) {
			this.oldStockBySellingInvoice = oldStockBySellingInvoice;
		}

		///////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////


		internal void oldStockBySellingInvoiceUserControlLoaded() {
			try {
				oldStockBySellingInvoice.DataTable = new DataTable();
				oldStockBySellingInvoice.DataTable.Columns.Add("ID", typeof(int));
				oldStockBySellingInvoice.DataTable.Columns.Add("Item Name", typeof(String));
				oldStockBySellingInvoice.DataTable.Columns.Add("Invoice #", typeof(String));
				oldStockBySellingInvoice.DataTable.Columns.Add("Date", typeof(String));
				oldStockBySellingInvoice.DataTable.Columns.Add("Customer Name", typeof(String));
				oldStockBySellingInvoice.DataTable.Columns.Add("Quantity", typeof(String));
				oldStockBySellingInvoice.DataTable.Columns.Add("Company RTN", typeof(String));
				oldStockBySellingInvoice.DataTable.Columns.Add("Good RTN", typeof(String));
				oldStockBySellingInvoice.DataTable.Columns.Add("Waste RTN", typeof(String));
				oldStockBySellingInvoice.DataTable.Columns.Add("Stock Before Sale", typeof(String));

				oldStockBySellingInvoice.DataGridFooter = new DataGridFooter();
				oldStockBySellingInvoice.dataGrid.IFooter = oldStockBySellingInvoice.DataGridFooter;
				oldStockBySellingInvoice.grid_footer.Children.Add(oldStockBySellingInvoice.DataGridFooter);
				oldStockBySellingInvoice.dataGrid.DataContext = oldStockBySellingInvoice.DataTable.DefaultView;

				oldStockBySellingInvoice.Pagination = new Pagination();
				oldStockBySellingInvoice.Pagination.Filter = oldStockBySellingInvoice;
				oldStockBySellingInvoice.grid_pagination.Children.Add(oldStockBySellingInvoice.Pagination);

				oldStockBySellingInvoice.ItemFinder = new ItemFinder(oldStockBySellingInvoice.textBox_itemId);
				oldStockBySellingInvoice.grid_itemFinder.Children.Add(oldStockBySellingInvoice.ItemFinder);

				setRowsCountSelling();
			} catch(Exception) {
			}
		}

		internal void filterSelling() {
			try {
				DataSet dataSet = CommonManagerImpl.getStockBeforeSaleForFilter(
					(!String.IsNullOrWhiteSpace(oldStockBySellingInvoice.textBox_itemId.Text) ? Convert.ToInt32(oldStockBySellingInvoice.textBox_itemId.Text) : 0),
					(oldStockBySellingInvoice.datePicker_dateFrom_filter.SelectedDate != null ? Convert.ToDateTime(oldStockBySellingInvoice.datePicker_dateFrom_filter.SelectedDate).ToString("yyyy-MM-dd") : null),
					(oldStockBySellingInvoice.datePicker_dateTo_filter.SelectedDate != null ? Convert.ToDateTime(oldStockBySellingInvoice.datePicker_dateTo_filter.SelectedDate).ToString("yyyy-MM-dd") : null),
					false, oldStockBySellingInvoice.Pagination.LimitStart, oldStockBySellingInvoice.Pagination.LimitCount);
				oldStockBySellingInvoice.DataTable.Rows.Clear();
				//Convert.ToDouble(row[6]).ToString("#,##0.00")
				foreach(DataRow row in dataSet.Tables[0].Rows) {
					oldStockBySellingInvoice.DataTable.Rows.Add(row[0], row[1], row[2], Convert.ToDateTime(row[3]).ToString("yyyy-MM-dd"),
						row[4], Convert.ToDouble(row[5]).ToString("#,##0.00"), Convert.ToDouble(row[6]).ToString("#,##0.00"), Convert.ToDouble(row[7]).ToString("#,##0.00"),
						Convert.ToDouble(row[8]).ToString("#,##0.00"), Convert.ToDouble(row[9]).ToString("#,##0.00"));
				}
			} catch(Exception) {
			}
		}

		internal void setRowsCountSelling() {
			try {
				DataSet dataSet = CommonManagerImpl.getStockBeforeSaleForFilter(
					(!String.IsNullOrWhiteSpace(oldStockBySellingInvoice.textBox_itemId.Text) ? Convert.ToInt32(oldStockBySellingInvoice.textBox_itemId.Text) : 0),
					(oldStockBySellingInvoice.datePicker_dateFrom_filter.SelectedDate != null ? Convert.ToDateTime(oldStockBySellingInvoice.datePicker_dateFrom_filter.SelectedDate).ToString("yyyy-MM-dd") : null),
					(oldStockBySellingInvoice.datePicker_dateTo_filter.SelectedDate != null ? Convert.ToDateTime(oldStockBySellingInvoice.datePicker_dateTo_filter.SelectedDate).ToString("yyyy-MM-dd") : null),
					true, oldStockBySellingInvoice.Pagination.LimitStart, oldStockBySellingInvoice.Pagination.LimitCount);
				oldStockBySellingInvoice.Pagination.RowsCount = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
			} catch(Exception) {
			}
		}
	}
}
