using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class SellingItem : Entity {

		private int id = 0;
		public int Id {
			get { return id; }
			set { id = value; }
		}

		private int sellingInvoiceId = -1;
		public int SellingInvoiceId {
			get { return sellingInvoiceId; }
			set { sellingInvoiceId = value; }
		}

		private int itemId = -1;
		public int ItemId {
			get { return itemId; }
			set { itemId = value; }
		}

		private String sellingMode = null;
		public String SellingMode {
			get { return sellingMode; }
			set { sellingMode = value; }
		}

		private double defaultPrice = -1;
		public double DefaultPrice {
			get { return defaultPrice; }
			set { defaultPrice = value; }
		}

		private double quantity = -1;
		public double Quantity {
			get { return quantity; }
			set { quantity = value; }
		}

		private double good_return_quantity = -1;
		public double Good_return_quantity {
			get { return good_return_quantity; }
			set { good_return_quantity = value; }
		}

		private double market_return_quantity = -1;
		public double Market_return_quantity {
			get { return market_return_quantity; }
			set { market_return_quantity = value; }
		}


		private double waste_return_quantity = -1;
		public double Waste_return_quantity {
			get { return waste_return_quantity; }
			set { waste_return_quantity = value; }
		}

		private double sold_price = -1;
		public double Sold_price {
			get { return sold_price; }
			set { sold_price = value; }
		}

		private double discount = -1;
		public double Discount {
			get { return discount; }
			set { discount = value; }
		}

		private double stock_before_sale = -1;
		public double Stock_before_sale {
			get { return stock_before_sale; }
			set { stock_before_sale = value; }
		}

		private double selling_price_actual = -1;
		public double Selling_price_actual {
			get { return selling_price_actual; }
			set { selling_price_actual = value; }
		}

		private double buying_price_actual = -1;
		public double Buying_price_actual {
			get { return buying_price_actual; }
			set { buying_price_actual = value; }
		}

	}
}
