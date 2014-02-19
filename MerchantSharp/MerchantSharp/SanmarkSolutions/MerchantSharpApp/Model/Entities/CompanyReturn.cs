using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class CompanyReturn : Entity {

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

		private DateTime date;
		public DateTime Date {
			get { return date; }
			set { date = value; }
		}

		private double price = -1;
		public double Price {
			get { return price; }
			set { price = value; }
		}

		private double quantity = -1;
		public double Quantity {
			get { return quantity; }
			set { quantity = value; }
		}

	}
}
