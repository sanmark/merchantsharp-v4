using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
    class StockByPriceController {

        private StockByPrice stockByPrice;
        private StockByPriceManagerImpl stockByPriceManagerImpl;

        public StockByPriceController(StockByPrice stockByPrice) {
            this.stockByPrice = stockByPrice;
            stockByPriceManagerImpl = new StockByPriceManagerImpl(stockByPrice);
        }

        internal void UserControl_Loaded() {
            try {
                if (!stockByPrice.IsLoadedUI) {
                    stockByPriceManagerImpl.UserControl_Loaded();
                    stockByPrice.IsLoadedUI = true;
                }
            } catch (Exception) {
            }
        }

        internal void setRowsCount() {
            try {
                stockByPriceManagerImpl.setRowsCount();
            } catch (Exception) {
            }
        }

        internal void filter() {
            try {
                stockByPriceManagerImpl.filter();
            } catch (Exception) {
            }
        }
    }
}
