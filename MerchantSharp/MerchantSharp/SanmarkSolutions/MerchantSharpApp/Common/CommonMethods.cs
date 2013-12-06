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

	}
}
