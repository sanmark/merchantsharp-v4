using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class SellingPriceManagerControler {

		private AddSellingPrice addSellingPrice;

		private SellingPriceManagerImpl sellingPriceManagerImpl = null;

		public SellingPriceManagerControler() {
		}

		public SellingPriceManagerControler(View.ShopManagement.AddSellingPrice addSellingPrice) {
			// TODO: Complete member initialization
			this.addSellingPrice = addSellingPrice;
			sellingPriceManagerImpl = new SellingPriceManagerImpl(addSellingPrice);
		}

		internal void button_addPrice_Click() {
			try {
				if(sellingPriceManagerImpl.addPriceFromAddSellingPrice()) {
					addSellingPrice.Hide();
					addSellingPrice.textBox_price.Clear();
				}
			} catch(Exception) {
			}
		}
	}
}
