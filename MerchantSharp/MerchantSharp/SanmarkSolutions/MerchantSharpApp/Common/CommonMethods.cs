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

		public static String getStatusForSellingInvoice(int status) {
			String s = null;
			try {
				if(status == 1) {
					s = "Sold";
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

		public static String dateTimeFormat(String date, String format) {
			try {
				if(String.IsNullOrEmpty(date)) {
					return null;
				}
				String done = Convert.ToDateTime(date).ToString(format);
				return done;
			} catch(Exception) {
				return null;
			}
		}

		public static String getReason( int status ) {
			String s = null;
			try {
				if ( status == 1 ) {
					s = "Normal";
				} else if ( status == 2 ) {
					s = "CR";
				} else if ( status == 3 ) {
					s = "GR";
				} else if ( status == 4 ) {
					s = "WR";
				}
			} catch ( Exception ) {
			}
			return s;
		}

		public static int getReason( String status ) {
			int s = 0;
			try {
				if ( status == "Normal" ) {
					s = 1;
				} else if ( status == "CR" ) {
					s = 2;
				} else if ( status == "GR" ) {
					s = 3;
				} else if ( status == "WR" ) {
					s = 4;
				}
			} catch ( Exception ) {
			}
			return s;
		}

	}
}
