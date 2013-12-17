using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class SellingOther : Entity {

		private int id = 0;
		public int Id {
			get { return id; }
			set { id = value; }
		}

		private int sellingInvoiceId = -1;
		public int SellingInvoiceId {
			get { return sellingInvoiceId; }
			set { sellingInvoiceId = value; }
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

		private double accountTransfer = -1;
		public double AccountTransfer {
			get { return accountTransfer; }
			set { accountTransfer = value; }
		}

		private String notes = null;
		public String Notes {
			get { return notes; }
			set { notes = value; }
		}

	}
}
