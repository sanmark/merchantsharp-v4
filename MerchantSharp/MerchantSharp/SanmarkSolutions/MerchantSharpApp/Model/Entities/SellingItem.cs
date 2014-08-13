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

		private int stockLocationId = -1;
		public int StockLocationId {
			get { return stockLocationId; }
			set { stockLocationId = value; }
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

		private double goodReturnQuantity = -1;
		public double GoodReturnQuantity {
			get { return goodReturnQuantity; }
			set { goodReturnQuantity = value; }
		}

		private double marketReturnQuantity = -1;
		public double MarketReturnQuantity {
			get { return marketReturnQuantity; }
			set { marketReturnQuantity = value; }
		}

		private double wasteReturnQuantity = -1;
		public double WasteReturnQuantity {
			get { return wasteReturnQuantity; }
			set { wasteReturnQuantity = value; }
		}

		private double soldPrice = -1;
		public double SoldPrice {
			get { return soldPrice; }
			set { soldPrice = value; }
		}		

		private double discount = -1;
		public double Discount {
			get { return discount; }
			set { discount = value; }
		}

		private double stockBeforeSale = -1;
		public double StockBeforeSale {
			get { return stockBeforeSale; }
			set { stockBeforeSale = value; }
		}

		private double sellingPriceActual = -1;
		public double SellingPriceActual {
			get { return sellingPriceActual; }
			set { sellingPriceActual = value; }
		}

		private double buyingPriceActual = -1;
		public double BuyingPriceActual {
			get { return buyingPriceActual; }
			set { buyingPriceActual = value; }
		}		

	}
}
