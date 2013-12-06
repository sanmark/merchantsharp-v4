using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.StakeHolders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class VendorManagerImpl {

		private IDao dao = VendorDao.getInstance();
		private AddVendor addVendor;

		public VendorManagerImpl() {
		}

		public VendorManagerImpl(AddVendor addVendor) {
			this.addVendor = addVendor;
		}

		public int add(Entity entity) {
			return dao.add(entity);
		}

		public bool del(Entity entity) {
			return dao.del(entity);
		}

		public List<Vendor> get(Entity entity) {
			return dao.get(entity).Cast<Vendor>().ToList();
		}

		public int upd(Entity entity) {
			return dao.upd(entity);
		}

		public bool isDublicate(String name) {
			bool b = false;
			try {
				Vendor vendor = new Vendor();
				vendor.Name = name;
				if(get(vendor).Count > 0) {
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		public List<Vendor> getAllActivedVendors() {
			List<Vendor> list = null;
			try {
				Vendor vendor = new Vendor();
				vendor.Status = 1;
				list = get(vendor);
			} catch(Exception) {
			}
			return list;
		}


		////////////////////////////////////////   AddVendorWindow

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		internal bool addVendorPopup() {
			bool b = false;
			try {
				if(addVendor.textBox_name.IsNull()) {
					addVendor.textBox_name.ErrorMode(true);
				} else if(isDublicate(addVendor.textBox_name.TrimedText)) {
					addVendor.textBox_name.ErrorMode(true);
				} else {
					Vendor vendor = new Vendor();
					vendor.Name = addVendor.textBox_name.TrimedText;
					vendor.Address = addVendor.textBox_address.TrimedText;
					vendor.Telephone = addVendor.textBox_telephone.TrimedText;
					vendor.Status = 1;
					vendor.AccountBalance = 0;
					CommonMethods.setCDMDForAdd(vendor);
					addVendor.AddedId = add(vendor);
					ShowMessage.success(Common.Messages.Success.Success002);
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}
	}
}
