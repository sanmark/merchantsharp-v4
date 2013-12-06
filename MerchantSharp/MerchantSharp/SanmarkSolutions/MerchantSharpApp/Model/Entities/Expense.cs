using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class Expense : Entity {

		private int id = 0;
		public int Id {
			get { return id; }
			set { id = value; }
		}

		private DateTime date;
		public DateTime Date {
			get { return date; }
			set { date = value; }
		}

		private double amount = -1;
		public double Amount {
			get { return amount; }
			set { amount = value; }
		}

		private String receiver = null;
		public String Receiver {
			get { return receiver; }
			set { receiver = value; }
		}

		private String details = null;
		public String Details {
			get { return details; }
			set { details = value; }
		}

		private int createdBy = -1;
		public int CreatedBy {
			get { return createdBy; }
			set { createdBy = value; }
		}

		private DateTime createdDate;
		public DateTime CreatedDate {
			get { return createdDate; }
			set { createdDate = value; }
		}

		private int modifiedBy = -1;
		public int ModifiedBy {
			get { return modifiedBy; }
			set { modifiedBy = value; }
		}

		private DateTime modifiedDate;
		public DateTime ModifiedDate {
			get { return modifiedDate; }
			set { modifiedDate = value; }
		}

	}
}
