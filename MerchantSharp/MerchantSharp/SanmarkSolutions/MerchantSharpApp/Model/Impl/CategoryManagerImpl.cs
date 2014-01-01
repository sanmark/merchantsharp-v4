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
	class CategoryManagerImpl {

		private IDao dao;
		private AddCategory addCategory;

		public CategoryManagerImpl() {
			dao = CategoryDao.getInstance();
		}

		public CategoryManagerImpl(AddCategory addCategory) {
			this.addCategory = addCategory;
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

		public bool isDublicate(String name, int id) {
			bool b = false;
			try {
				Category category = new Category();
				category.Name = name;
				if(get(category).Count > 0) {
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		//////////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////////////


		internal bool addCategoryPopup() {
			bool b = false;
			try {
				if(addCategory.textBox_name.IsNull()) {
					addCategory.textBox_name.ErrorMode(true);
				} else if(isDublicate(addCategory.textBox_name.TrimedText, 0)) {
					addCategory.textBox_name.ErrorMode(true);
					ShowMessage.error(Common.Messages.Error.Error007);
				} else {
					Category category = new Category();
					category.Name = addCategory.textBox_name.TrimedText;
					CommonMethods.setCDMDForAdd(category);
					addCategory.AddedId = add(category);
					ShowMessage.success(Common.Messages.Success.Success002);
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}
	}
}
