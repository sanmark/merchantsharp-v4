using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class StockTransferItem : Entity {

		private int id = 0;
		public int Id {
			get { return id; }
			set { id = value; }
		}

		private int stockTransferId = -1;
		public int StockTransferId {
			get { return stockTransferId; }
			set { stockTransferId = value; }
		}

		private int itemId = -1;
		public int ItemId {
			get { return itemId; }
			set { itemId = value; }
		}

		private double quantity = -1;
		public double Quantity {
			get { return quantity; }
			set { quantity = value; }
		}

	}
}
