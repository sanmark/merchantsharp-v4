using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class BuyingInvoice : Entity {

		private int id = 0;
		public int Id {
			get { return id; }
			set { id = value; }
		}

		private int vendorId = -1;
		public int VendorId {
			get { return vendorId; }
			set { vendorId = value; }
		}

		private String invoiceNumber = null;
		public String InvoiceNumber {
			get { return invoiceNumber; }
			set { invoiceNumber = value; }
		}

		private String grn = null;
		public String Grn {
			get { return grn; }
			set { grn = value; }
		}

		private DateTime orderedDate;
		public DateTime OrderedDate {
			get { return orderedDate; }
			set { orderedDate = value; }
		}

		private DateTime receivedDate;
		public DateTime ReceivedDate {
			get { return receivedDate; }
			set { receivedDate = value; }
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

		private double marketReturnDiscount = -1;
		public double MarketReturnDiscount {
			get { return marketReturnDiscount; }
			set { marketReturnDiscount = value; }
		}

		private DateTime expectedPayingDate;
		public DateTime ExpectedPayingDate {
			get { return expectedPayingDate; }
			set { expectedPayingDate = value; }
		}

		private String details = null;
		public String Details {
			get { return details; }
			set { details = value; }
		}

		private int vendorAccountBalanceType = -1;
		public int VendorAccountBalanceType {
			get { return vendorAccountBalanceType; }
			set { vendorAccountBalanceType = value; }
		}

		private double vendorAccountBalanceChange = -1;
		public double VendorAccountBalanceChange {
			get { return vendorAccountBalanceChange; }
			set { vendorAccountBalanceChange = value; }
		}

		private double laterDiscount = -1;
		public double LaterDiscount {
			get { return laterDiscount; }
			set { laterDiscount = value; }
		}

		private int status = -1;
		public int Status {
			get { return status; }
			set { status = value; }
		}
		
	}
}
