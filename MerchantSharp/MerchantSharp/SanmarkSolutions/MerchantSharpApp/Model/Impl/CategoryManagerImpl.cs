using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class CategoryManagerImpl {

		private IDao dao;

		public CategoryManagerImpl() {
			dao = CategoryDao.getInstance();
		}

		public int add(Entity entity) {
			return dao.add(entity);
		}

		public bool del(Entity entity) {
			return dao.del(entity);
		}

		public List<Category> get(Entity entity) {
			return dao.get(entity).Cast<Category>().ToList();
		}

		public int upd(Entity entity) {
			return dao.upd(entity);
		}

		/////////////////////////////////////////////////

		public List<Category> getAllCategories() {
			return get(new Category());
		}

		public Category getCategoryById(int id) {
			Category category = null;
			try {
				Category cat = new Category();
				cat.Id = id;
				category = get(cat)[0];
			} catch(Exception) {
			}
			return category;
		}

		public String getCategoryNameById(int id) {
			String name = "";
			try {
				name = getCategoryById(id).Name;
			} catch(Exception) {
			}
			return name;
		}

	}
}
