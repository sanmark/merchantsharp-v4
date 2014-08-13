using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class StockTransfer : Entity {

		private int id = 0;
		public int Id {
			get { return id; }
			set { id = value; }
		}

		private int fromLocationId = -1;
		public int FromLocationId {
			get { return fromLocationId; }
			set { fromLocationId = value; }
		}

		private int toLocationId = -1;
		public int ToLocationId {
			get { return toLocationId; }
			set { toLocationId = value; }
		}

		private DateTime date;
		public DateTime Date {
			get { return date; }
			set { date = value; }
		}

		private String carrier = null;
		public String Carrier {
			get { return carrier; }
			set { carrier = value; }
		}

		private String details = null;
		public String Details {
			get { return details; }
			set { details = value; }
		}

	}
}
