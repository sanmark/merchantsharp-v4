using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class SellingInvoice : Entity {

		private int id = 0;
		public int Id {
			get { return id; }
			set { id = value; }
		}

		private int customerId = -1;
		public int CustomerId {
			get { return customerId; }
			set { customerId = value; }
		}

		private String invoiceNumber = null;
		public String InvoiceNumber {
			get { return invoiceNumber; }
			set { invoiceNumber = value; }
		}

		private DateTime date;
		public DateTime Date {
			get { return date; }
			set { date = value; }
		}

		private String details = null;
		public String Details {
			get { return details; }
			set { details = value; }
		}

		private double discount = -1;
		public double Discount {
			get { return discount; }
			set { discount = value; }
		}

		private int isCompletelyPaid = -1;
		public int IsCompletelyPaid {
			get { return isCompletelyPaid; }
			set { isCompletelyPaid = value; }
		}

		private double referrerCommision = -1;
		public double ReferrerCommision {
			get { return referrerCommision; }
			set { referrerCommision = value; }
		}

		private int isQuickPaid = -1;
		public int IsQuickPaid {
			get { return isQuickPaid; }
			set { isQuickPaid = value; }
		}

		private double givenMoney = -1;
		public double GivenMoney {
			get { return givenMoney; }
			set { givenMoney = value; }
		}

		private double customerAccountBalanceChange = -1;
		public double CustomerAccountBalanceChange {
			get { return customerAccountBalanceChange; }
			set { customerAccountBalanceChange = value; }
		}

		private int status = -1;
		public int Status {
			get { return status; }
			set { status = value; }
		}

	}
}
