using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class CompanyManagerImpl {

		private IDao dao;
		private AddCompany addCompany;

		public CompanyManagerImpl() {
			dao = CompanyDao.getInstance();
		}

		public CompanyManagerImpl(AddCompany addCompany) {
			// TODO: Complete member initialization
			this.addCompany = addCompany;
			dao = CompanyDao.getInstance();
		}

		public int add(Entity entity) {
			return dao.add(entity);
		}

		public bool del(Entity entity) {
			return dao.del(entity);
		}

		public List<Company> get(Entity entity) {
			return dao.get(entity).Cast<Company>().ToList();
		}

		public int upd(Entity entity) {
			return dao.upd(entity);
		}

		/////////////////////////////////////////////////

		public List<Company> getAllCompanies() {
			return get(new Company());
		}

		public Company getCompanyById(int id) {
			Company company = null;
			try {
				Company com = new Company();
				com.Id = id;
				company = get(com)[0];
			} catch(Exception) {
			}
			return company;
		}

		public String getCompanyNameById(int id) {
			String name = "";
			try {
				name = getCompanyById(id).Name;
			} catch(Exception) {
			}
			return name;
		}

		public bool isDublicate(String name, int id) {
			bool b = false;
			try {
				Company company = new Company();
				company.Name = name;
				if(get(company).Count > 0) {
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		//////////////////////////////////////////////////////////////////////////////////////

		internal bool addCompanyPopup() {
			bool b = false;
			try {
				if(addCompany.textBox_name.IsNull()) {
					addCompany.textBox_name.ErrorMode(true);
				} else if(isDublicate(addCompany.textBox_name.TrimedText, 0)) {
					addCompany.textBox_name.ErrorMode(true);
					ShowMessage.error(Common.Messages.Error.Error007);
				} else {
					Company company = new Company();
					company.Name = addCompany.textBox_name.TrimedText;
					CommonMethods.setCDMDForAdd(company);
					addCompany.AddedId = add(company);
					ShowMessage.success(Common.Messages.Success.Success002);
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}
	}
}
