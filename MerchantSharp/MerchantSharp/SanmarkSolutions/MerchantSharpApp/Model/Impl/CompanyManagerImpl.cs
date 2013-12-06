using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class CompanyManagerImpl {

		private IDao dao;

		public CompanyManagerImpl() {
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

	}
}
