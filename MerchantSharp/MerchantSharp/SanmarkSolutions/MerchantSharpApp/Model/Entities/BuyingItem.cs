using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class BuyingItem : Entity {

		private int id = 0;
		public int Id {
			get { return id; }
			set { id = value; }
		}

		private int buyingInvoiceId = -1;
		public int BuyingInvoiceId {
			get { return buyingInvoiceId; }
			set { buyingInvoiceId = value; }
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

		private double buyingPrice = -1;
		public double BuyingPrice {
			get { return buyingPrice; }
			set { buyingPrice = value; }
		}

		private double buyingPriceActual = -1;
		public double BuyingPriceActual {
			get { return buyingPriceActual; }
			set { buyingPriceActual = value; }
		}

		private double unitSellingPrice = -1;
		public double UnitSellingPrice {
			get { return unitSellingPrice; }
			set { unitSellingPrice = value; }
		}

		private double packSellingPrice = -1;
		public double PackSellingPrice {
			get { return packSellingPrice; }
			set { packSellingPrice = value; }
		}

		private double quantity = -1;
		public double Quantity {
			get { return quantity; }
			set { quantity = value; }
		}

		private double freeQuantity = -1;
		public double FreeQuantity {
			get { return freeQuantity; }
			set { freeQuantity = value; }
		}

		private String buyingMode = null;
		public String BuyingMode {
			get { return buyingMode; }
			set { buyingMode = value; }
		}

		private DateTime expiryDate;
		public DateTime ExpiryDate {
			get { return expiryDate; }
			set { expiryDate = value; }
		}
	}
}
