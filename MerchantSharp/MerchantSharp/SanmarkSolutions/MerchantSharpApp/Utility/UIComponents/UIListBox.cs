using CustomControls.SanmarkSolutions.WPFCustomControls.MSListBox;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents {
	class UIListBox {

		private static CategoryManagerImpl categoryManagerImpl = new CategoryManagerImpl();
		private static CompanyManagerImpl companyManagerImpl = new CompanyManagerImpl();
		

		public static void recentItems(ListBox listBox, int count) {
			try {
				DataTable dataTable_item = new DataTable();
				dataTable_item.Columns.Add("ID", typeof(int));
				dataTable_item.Columns.Add("name", typeof(String));
				List<int> itemIds = CommonManagerImpl.getRecentSoldItemIds(count);

				listBox.ItemsSource = dataTable_item.DefaultView;
				listBox.DisplayMemberPath = "name";
				listBox.SelectedValuePath = "id";
			} catch(Exception) {
			}
		}

		public static void loadItemsByCategoryAndCompanyAndName(MSListBox list, String text) {
			try {
				DataTable dataTable_item = new DataTable();
				dataTable_item.Columns.Add("ID", typeof(int));
				dataTable_item.Columns.Add("name", typeof(String));

				String[] words = text.Split(',');
				String item = "%";
				String company = "%";
				String category = "%";
				for(int i = 0; i < words.Length; i++) {
					if(i == 0) {
						item += words[i] + "%";
					} else if(i == 1) {
						company += words[i] + "%";
					} else if(i == 2) {
						category += words[i] + "%";
					}
				}
				DataSet dataSet = CommonManagerImpl.getItemsForSearch(item, company, category);
				foreach(DataRow row in dataSet.Tables[0].Rows) {
					dataTable_item.Rows.Add(row[0], row[1] + " ( " + row[5] + ", " + row[3] + " )");
				}
				list.OptionGroup = dataTable_item;
			} catch(Exception) {
			}
		}

		public static void loadCategories(MSListBox listBox, String name) {
			try {
				Category c = new Category();
				c.Name = "%" + name + "%";
				List<Category> list = categoryManagerImpl.get(c);
				DataTable dataTableCategory = new DataTable();
				dataTableCategory.Columns.Add("ID", typeof(int));
				dataTableCategory.Columns.Add("Name", typeof(String));

				foreach(Category category in list) {
					dataTableCategory.Rows.Add(category.Id, category.Name);
				}
				listBox.OptionGroup = dataTableCategory;
			} catch(Exception) {
			}
		}

		public static void loadCompanies(MSListBox listBox, String name) {
			try {
				Company c = new Company();
				c.Name = "%" + name + "%";
				List<Company> list = companyManagerImpl.get(c);
				DataTable dataTableCompany = new DataTable();
				dataTableCompany.Columns.Add("ID", typeof(int));
				dataTableCompany.Columns.Add("Name", typeof(String));

				foreach(Company company in list) {
					dataTableCompany.Rows.Add(company.Id, company.Name);
				}
				listBox.OptionGroup = dataTableCompany;
			} catch(Exception) {
			}
		}

	}
}
