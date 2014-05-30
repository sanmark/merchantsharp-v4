using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Mold {
	class DailyProfitMold : BaseMold {

		public Int32 Id {
			get { return base.i; }
			set { base.i = value; }
		}

		public String Date {
			get { return base.s; }
			set { base.s = value; }
		}
		public String Amount {
			get { return base.s; }
			set { base.s = value; }
		}

		public String Expenses {
			get { return base.s; }
			set { base.s = value; }
		}

		public String Total {
			get { return base.s; }
			set { base.s = value; }
		}

	}
}
