using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class User : Entity {

		private int id = 0;
		public int Id {
			get { return id; }
			set { id = value; }
		}

		private String userName = null;
		public String UserName {
			get { return userName; }
			set { userName = value; }
		}

		private String password = null;
		public String Password {
			get { return password; }
			set { password = value; }
		}

		private String firstName = null;

		public String FirstName {
			get { return firstName; }
			set { firstName = value; }
		}

		private String lastName = null;
		public String LastName {
			get { return lastName; }
			set { lastName = value; }
		}

		/*private int isFake = -1;
		public int IsFake {
			get { return isFake; }
			set { isFake = value; }
		}*/

		private int status = -1;
		public int Status {
			get { return status; }
			set { status = value; }
		}

	}
}
