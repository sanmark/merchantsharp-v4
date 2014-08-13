using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class Preference : Entity {

		private int id = 0;
		public int Id {
			get { return id; }
			set { id = value; }
		}

		private String key = null;
		public String Key {
			get { return key; }
			set { key = value; }
		}

		private String value = null;
		public String Value {
			get { return this.value; }
			set { this.value = value; }
		}
		
	}
}
