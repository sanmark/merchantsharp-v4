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
	class CompanyManagerImpl {

		private IDao dao;
		private AddCompany addCompany;
		private   CompanyManager companyManager;

		public CompanyManagerImpl() {
			dao = CompanyDao.getInstance();
		}

		public CompanyManagerImpl(AddCompany addCompany) {
			this.addCompany = addCompany;
			dao = CompanyDao.getInstance();
		}

		public CompanyManagerImpl(CompanyManager companyManager) {
			this.companyManager = companyManager;
			dao = CompanyDao.getInstance();
		}

		public int add(Entity entity) {
			try {
				if(Session.Permission["canAddCompany"] == 1) {
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

		public List<Company> get(Entity entity) {
			return dao.get(entity).Cast<Company>().ToList();
		}

		public int upd(Entity entity) {
			try {
				if(Session.Permission["canUpdateCompany"] == 1) {
					return dao.upd(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return 0;
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

		public bool isDuplicate(String name, int id) {
			bool b = false;
			try {
				Company company = new Company();
				company.Name = name;
				List<Company> list = get(company);
				if(list.Count > 0 && id > 0) {
					foreach(Company c in list) {
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

		//////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////

		internal bool addCompanyPopup() {
			bool b = false;
			try {
				if(addCompany.textBox_name.IsNull()) {
					addCompany.textBox_name.ErrorMode(true);
				} else if(isDuplicate(addCompany.textBox_name.TrimedText, 0)) {
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


		//////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////

		internal void UserControl_Loaded() {
			try {
				companyManager.DataTable = new DataTable();
				companyManager.DataTable.Columns.Add("ID", typeof(int));
				companyManager.DataTable.Columns.Add("Name", typeof(String));
				companyManager.dataGrid.DataContext = companyManager.DataTable.DefaultView;
				companyManager.dataGrid.Columns[1].Width = 200;

				companyManager.Pagination = new Pagination();
				companyManager.Pagination.Filter = companyManager;
				companyManager.grid_pagination.Children.Add(companyManager.Pagination);

				setRowsCount();
			} catch(Exception) {
			}
		}

		private Company getCompanyForFilter() {
			Company company = new Company();
			try {
				company.Name = "%" + companyManager.textBox_name_filter.TrimedText + "%";
			} catch(Exception) {
			}
			return company;
		}

		internal void filter() {
			try {
				Company c = getCompanyForFilter();
				c.LimitStart = companyManager.Pagination.LimitStart;
				c.LimitEnd = companyManager.Pagination.LimitCount;
				List<Company> list = get(c);
				companyManager.DataTable.Rows.Clear();
				foreach(Company company in list) {
					companyManager.DataTable.Rows.Add(company.Id, company.Name);
				}
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				Company company = getCompanyForFilter();
				company.RowsCount = 1;
				company.LimitStart = companyManager.Pagination.LimitStart;
				company.LimitEnd = companyManager.Pagination.LimitCount;
				companyManager.Pagination.RowsCount = get(company)[0].RowsCount;
			} catch(Exception) {
			}
		}

		internal bool addNewCompany() {
			bool b = false;
			try {
				bool isOkay = true;
				if(companyManager.textBox_name_addCompany.IsNull()) {
					companyManager.textBox_name_addCompany.ErrorMode(true);
					isOkay = false;
				}
				if(isDuplicate(companyManager.textBox_name_addCompany.TrimedText, 0)) {
					companyManager.textBox_name_addCompany.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					Company company = new Company();
					company.Name = companyManager.textBox_name_addCompany.TrimedText;
					CommonMethods.setCDMDForAdd(company);
					if(add(company) > 0) {
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void resetAddForm() {
			try {
				companyManager.textBox_name_addCompany.Clear();
			} catch(Exception) {
			}
		}

		internal bool updateCompany() {
			bool b = false;
			try {
				bool isOkay = true;
				if(companyManager.textBox_name_addCompany.IsNull()) {
					companyManager.textBox_name_addCompany.ErrorMode(true);
					isOkay = false;
				}
				if(isDuplicate(companyManager.textBox_name_addCompany.TrimedText, companyManager.SelectedCompany.Id)) {
					companyManager.textBox_name_addCompany.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					Company company = companyManager.SelectedCompany;
					company.Name = companyManager.textBox_name_addCompany.TrimedText;
					CommonMethods.setCDMDForUpdate(company);
					upd(company);
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void switchToAddMode() {
			try {
				companyManager.SelectedCompany = null;
				companyManager.IsUpdateMode = false;
				companyManager.button_save_addCompany.Content = "Save";
				companyManager.button_reset_addCompany.Content = "Reset";
			} catch(Exception) {
			}
		}

		internal void switchToUpdateMode() {
			try {
				companyManager.IsUpdateMode = true;
				Company company = getCompanyById(companyManager.dataGrid.SelectedItemID);
				companyManager.SelectedCompany = company;
				companyManager.textBox_name_addCompany.Text = company.Name;

				companyManager.button_save_addCompany.Content = "Update";
				companyManager.button_reset_addCompany.Content = "Cancel";
			} catch(Exception) {
			}
		}
	}
}
