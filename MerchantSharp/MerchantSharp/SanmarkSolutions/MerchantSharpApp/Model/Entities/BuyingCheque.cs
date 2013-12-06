using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class BuyingCheque : Entity {

		private int id = 0;
		public int Id {
			get { return id; }
			set { id = value; }
		}

		private int buyingInvoiceId = -1;
		public int BuyingInvoiceId {
			get { return buyingInvoiceId; }
			set { buyingInvoiceId = value; }
		}

		private int bankId = -1;
		public int BankId {
			get { return bankId; }
			set { bankId = value; }
		}

		private String chequeNumber = null;
		public String ChequeNumber {
			get { return chequeNumber; }
			set { chequeNumber = value; }
		}

		private DateTime issuedDate;
		public DateTime IssuedDate {
			get { return issuedDate; }
			set { issuedDate = value; }
		}

		private DateTime payableDate;
		public DateTime PayableDate {
			get { return payableDate; }
			set { payableDate = value; }
		}

		private double amount = -1;
		public double Amount {
			get { return amount; }
			set { amount = value; }
		}

		private String notes = null;
		public String Notes {
			get { return notes; }
			set { notes = value; }
		}

		private int status = -1;
		public int Status {
			get { return status; }
			set { status = value; }
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
