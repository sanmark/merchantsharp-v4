using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class PermissionUser : Entity {

		private int id = 0;
		public int Id {
			get { return id; }
			set { id = value; }
		}

		private int userId = -1;
		public int UserId {
			get { return userId; }
			set { userId = value; }
		}

		private int permissionId = -1;
		public int PermissionId {
			get { return permissionId; }
			set { permissionId = value; }
		}

		private int value = -1;
		public int Value {
			get { return this.value; }
			set { this.value = value; }
		}
				
	}
}
