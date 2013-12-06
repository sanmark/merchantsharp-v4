using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class SellingPrice : Entity {

		private int id = 0;
		public int Id {
			get { return id; }
			set { id = value; }
		}

		private int itemId = -1;
		public int ItemId {
			get { return itemId; }
			set { itemId = value; }
		}

		private String mode = null;
		public String Mode {
			get { return mode; }
			set { mode = value; }
		}

		private double price = -1;
		public double Price {
			get { return price; }
			set { price = value; }
		}

	}
}
