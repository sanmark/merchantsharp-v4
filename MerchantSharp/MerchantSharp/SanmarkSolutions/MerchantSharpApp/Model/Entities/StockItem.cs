using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class StockItem : Entity {

		private int id = 0;
		public int Id {
			get { return id; }
			set { id = value; }
		}

		private int stockLocationId = -1;
		public int StockLocationId {
			get { return stockLocationId; }
			set { stockLocationId = value; }
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
