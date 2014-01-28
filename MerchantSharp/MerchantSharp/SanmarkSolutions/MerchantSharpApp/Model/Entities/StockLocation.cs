using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class StockLocation : Entity {

		private int id = 0;
		public int Id {
			get { return id; }
			set { id = value; }
		}

		private String name = null;
		public String Name {
			get { return name; }
			set { name = value; }
		}

		private int status = -1;
		public int Status {
			get { return status; }
			set { status = value; }
		}

	}
}
