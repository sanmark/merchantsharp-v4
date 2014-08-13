using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class Permission : Entity {

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

		private String label = null;
		public String Label {
			get { return label; }
			set { label = value; }
		}

		private int parent = -1;
		public int Parent {
			get { return parent; }
			set { parent = value; }
		}

	}
}
