using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
    class StockByPriceManagerImpl {

        private StockByPrice stockByPrice;
        private List<String[]> items;
        private BuyingInvoiceManagerImpl buyingInvoiceManagerImpl;
        private StockManagerImpl stockManagerImpl;

        public StockByPriceManagerImpl(StockByPrice stockByPrice) {
            this.stockByPrice = stockByPrice;
            buyingInvoiceManagerImpl = new BuyingInvoiceManagerImpl();
            stockManagerImpl = new StockManagerImpl();
        }

        internal void UserControl_Loaded() {
            try {
                stockByPrice.DataTable = new DataTable();
                stockByPrice.DataTable.Columns.Add("Item Name", typeof(String));
                stockByPrice.DataTable.Columns.Add("Buying Invoice", typeof(String));
                stockByPrice.DataTable.Columns.Add("Date", typeof(String));
                stockByPrice.DataTable.Columns.Add("Buying Price", typeof(String));
                stockByPrice.DataTable.Columns.Add("Selling Price", typeof(String));
                stockByPrice.DataTable.Columns.Add("Quantity", typeof(String));

                stockByPrice.DataGridFooter = new DataGridFooter();
                stockByPrice.dataGrid.IFooter = stockByPrice.DataGridFooter;
                stockByPrice.grid_footer.Children.Add(stockByPrice.DataGridFooter);
                stockByPrice.dataGrid.DataContext = stockByPrice.DataTable.DefaultView;

                stockByPrice.Pagination = new Pagination();
                stockByPrice.Pagination.Filter = stockByPrice;
                stockByPrice.grid_pagination.Children.Add(stockByPrice.Pagination);

                stockByPrice.ItemFinder = new ItemFinder(stockByPrice.textBox_itemId);
                stockByPrice.grid_itemFinder.Children.Add(stockByPrice.ItemFinder);

                setRowsCount();
            } catch (Exception) { 
            }
        }

        internal void setRowsCount() {
            try {
                int itemId = Convert.ToInt32(stockByPrice.textBox_itemId.Text);
                if(itemId > 0){
                    items = buyingInvoiceManagerImpl.getStockForPrice(itemId, stockManagerImpl.getStockQuantityByItemId(itemId));
                }
                stockByPrice.Pagination.RowsCount = Convert.ToInt32(items.Count);
            } catch (Exception) { 
            }
        }

        internal void filter() {
            try {
                stockByPrice.DataTable.Rows.Clear();
                int start = stockByPrice.Pagination.LimitStart;
                int count = stockByPrice.Pagination.LimitCount;
                int current = 1;
                int got = 0;
                foreach (String[] arr in items) {
                    if (current > start && got < count) {
                        stockByPrice.DataTable.Rows.Add(arr[0], arr[1], arr[2], arr[3], arr[4], arr[5]);
                        got++;
                    }
                    current++;
                }
            } catch (Exception) {
            }
        }
    }
}
