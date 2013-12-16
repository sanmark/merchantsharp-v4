using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common {
	class CommonMethods {

		public static void setCDMDForAdd(Entity entity) {
			try {
				entity.CreatedBy = Session.User.Id;
				entity.ModifiedBy = Session.User.Id;
				DateTime dateTime = DateTime.Now;
				entity.CreatedDate = dateTime;
				entity.ModifiedDate = dateTime;
			} catch(Exception) {
			}
		}

		public static void setCDMDForUpdate(Entity entity) {
			try {
				entity.ModifiedBy = Session.User.Id;
				DateTime dateTime = DateTime.Now;
				entity.ModifiedDate = dateTime;
			} catch(Exception) {
			}
		}

		public static String getStatusForCheque(int status) {
			String s = null;
			try {
				if(status == 0) {
					s = "On Hand";
				} else if(status == 1) {
					s = "Banked";
				} else if(status == 2) {
					s = "Changed";
				} else if(status == 3) {
					s = "Returned";
				}
			} catch(Exception) {
			}
			return s;
		}

		public static String getStatusForBuyingInvoice(int status) {
			String s = null;
			try {
				if(status == 1) {
					s = "Received";
				} else if(status == 2) {
					s = "Request";
				} else if(status == 3) {
					s = "Draft";
				}
			} catch(Exception) {
			}
			return s;
		}

		public static String getYesNo(int status) {
			String s = null;
			try {
				if(status == 1) {
					s = "Yes";
				} else if(status == 0) {
					s = "No";
				}
			} catch(Exception) {
			}
			return s;
		}

	}
}
