using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class CategoryManagerImpl {

		private IDao dao;
		private AddCategory addCategory;
		private   CategoryManager categoryManager;

		public CategoryManagerImpl() {
			dao = CategoryDao.getInstance();
		}

		public CategoryManagerImpl(AddCategory addCategory) {
			this.addCategory = addCategory;
			dao = CategoryDao.getInstance();
		}

		public CategoryManagerImpl(CategoryManager categoryManager) {
			this.categoryManager = categoryManager;
			dao = CategoryDao.getInstance();
		}

		public int add(Entity entity) {
			try {
				if(Session.Permission["canAddCategory"] == 1) {
					return dao.add(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return 0;
		}

		public bool del(Entity entity) {
			return dao.del(entity);
		}

		public List<Category> get(Entity entity) {
			return dao.get(entity).Cast<Category>().ToList();
		}

		public int upd(Entity entity) {
			try {
				if(Session.Permission["canUpdateCategory"] == 1) {
					return dao.upd(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return 0;
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

		public bool isDuplicate(String name, int id) {
			bool b = false;
			try {
				Category category = new Category();
				category.Name = name;
				List<Category> list = get(category);
				if(list.Count > 0 && id > 0) {
					foreach(Category c in list) {
						if(c.Id != id) {
							b = true;
						}
					}
				} else if(list.Count > 0) {
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
				} else if(isDuplicate(addCategory.textBox_name.TrimedText, 0)) {
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

		////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////

		internal void UserControl_Loaded() {
			try {
				categoryManager.DataTable = new DataTable();
				categoryManager.DataTable.Columns.Add("ID", typeof(int));
				categoryManager.DataTable.Columns.Add("Name", typeof(String));
				categoryManager.dataGrid.DataContext = categoryManager.DataTable.DefaultView;
				categoryManager.dataGrid.Columns[1].Width = 200;

				categoryManager.Pagination = new Pagination();
				categoryManager.Pagination.Filter = categoryManager;
				categoryManager.grid_pagination.Children.Add(categoryManager.Pagination);

				setRowsCount();
			} catch(Exception) {
			}
		}

		private Category getCategoryForFilter() {
			Category category = new Category();
			try {
				category.Name = "%" + categoryManager.textBox_name_filter.TrimedText + "%";
			} catch(Exception) {
			}
			return category;
		}

		internal void filter() {
			try {
				Category c = getCategoryForFilter();
				c.LimitStart = categoryManager.Pagination.LimitStart;
				c.LimitEnd = categoryManager.Pagination.LimitCount;
				List<Category> list = get(c);
				categoryManager.DataTable.Rows.Clear();
				foreach(Category category in list) {
					categoryManager.DataTable.Rows.Add(category.Id, category.Name);
				}
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				Category category = getCategoryForFilter();
				category.RowsCount = 1;
				category.LimitStart = categoryManager.Pagination.LimitStart;
				category.LimitEnd = categoryManager.Pagination.LimitCount;
				categoryManager.Pagination.RowsCount = get(category)[0].RowsCount;
			} catch(Exception) {
			}
		}

		internal bool addNewCategory() {
			bool b = false;
			try {
				bool isOkay = true;
				if(categoryManager.textBox_name_addCategory.IsNull()) {
					categoryManager.textBox_name_addCategory.ErrorMode(true);
					isOkay = false;
				}
				if(isDuplicate(categoryManager.textBox_name_addCategory.TrimedText, 0)) {
					categoryManager.textBox_name_addCategory.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					Category category = new Category();
					category.Name = categoryManager.textBox_name_addCategory.TrimedText;
					CommonMethods.setCDMDForAdd(category);
					if(add(category) > 0) {
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void resetAddForm() {
			try {
				categoryManager.textBox_name_addCategory.Clear();
			} catch(Exception) {
			}
		}

		internal bool updateCategory() {
			bool b = false;
			try {
				bool isOkay = true;
				if(categoryManager.textBox_name_addCategory.IsNull()) {
					categoryManager.textBox_name_addCategory.ErrorMode(true);
					isOkay = false;
				}
				if(isDuplicate(categoryManager.textBox_name_addCategory.TrimedText, categoryManager.SelectedCategory.Id)) {
					categoryManager.textBox_name_addCategory.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					Category category = categoryManager.SelectedCategory;
					category.Name = categoryManager.textBox_name_addCategory.TrimedText;
					CommonMethods.setCDMDForUpdate(category);
					upd(category);
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void switchToAddMode() {
			try {
				categoryManager.SelectedCategory = null;
				categoryManager.IsUpdateMode = false;
				categoryManager.button_save_addCategory.Content = "Save";
				categoryManager.button_reset_addCategory.Content = "Reset";
			} catch(Exception) {
			}
		}

		internal void switchToUpdateMode() {
			try {
				categoryManager.IsUpdateMode = true;
				Category category = getCategoryById(categoryManager.dataGrid.SelectedItemID);
				categoryManager.SelectedCategory = category;
				categoryManager.textBox_name_addCategory.Text = category.Name;

				categoryManager.button_save_addCategory.Content = "Update";
				categoryManager.button_reset_addCategory.Content = "Cancel";
			} catch(Exception) {
			}
		}
	}
}
