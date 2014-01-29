using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.StakeHolders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class CustomerManagerImpl {

		private IDao dao = null;
		private AddCustomer addCustomer;
		private   CustomerManager customerManager;

		public CustomerManagerImpl() {
			dao = CustomerDao.getInstance();
		}

		public CustomerManagerImpl(AddCustomer addCustomer) {
			this.addCustomer = addCustomer;
			dao = CustomerDao.getInstance();
		}

		public CustomerManagerImpl(CustomerManager customerManager) {
			this.customerManager = customerManager;
			dao = CustomerDao.getInstance();
		}

		public int add(Entity entity) {
			try {
				if(Session.Permission["canAddCustomer"] == 1) {
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

		public List<Customer> get(Entity entity) {
			return dao.get(entity).Cast<Customer>().ToList();
		}

		public int upd(Entity entity) {
			try {
				if(Session.Permission["canUpdateCustomer"] == 1) {
					dao.upd(entity);
					return 1;
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return 0;
		}

		public List<Customer> getAllActivedCustomers() {
			List<Customer> list = null;
			try {
				Customer c = new Customer();
				c.Status = 1;
				list = get(c);
			} catch(Exception) {
			}
			return list;
		}

		private bool isDuplicate(string name, int id) {
			bool b = false;
			try {
				Customer customer = new Customer();
				customer.Name = name;
				List<Customer> list = get(customer);
				if(list.Count > 0 && id > 0) {
					foreach(Customer c in list) {
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

		public Customer getCustomerById(int id) {
			Customer customer = null;
			try {
				Customer c = new Customer();
				c.Id = id;
				customer = get(c)[0];
			} catch(Exception) {
			}
			return customer;
		}

		public String getCustomerNameById(int id) {
			String name = "";
			try {
				name = getCustomerById(id).Name;
			} catch(Exception) {
			}
			return name;
		}

		internal double getAccountBalanceById(int id) {
			double d = 0;
			try {
				Customer customer = new Customer();
				customer.Id = id;
				d = get(customer)[0].AccountBalance;
			} catch(Exception) {
			}
			return d;
		}

		public void additionAccountBalanceById(int customerId, double val) {
			try {
				Customer cstomer = getCustomerById(customerId);
				cstomer.AccountBalance += val;
				upd(cstomer);
			} catch(Exception) {
			}
		}

		public void substractAccountBalanceById(int customerId, double val) {
			try {
				Customer customer = getCustomerById(customerId);
				customer.AccountBalance -= val;
				upd(customer);
			} catch(Exception) {
			}
		}


		///////////////////////////////////////////////////////////////////////////////////////////////

		internal bool addCustomerPopup() {
			bool b = false;
			try {
				if(addCustomer.textBox_name.IsNull()) {
					addCustomer.textBox_name.ErrorMode(true);
				} else if(isDuplicate(addCustomer.textBox_name.TrimedText, 0)) {
					addCustomer.textBox_name.ErrorMode(true);
				} else {
					Customer customer = new Customer();
					customer.Name = addCustomer.textBox_name.TrimedText;
					customer.Address = addCustomer.textBox_address.TrimedText;
					customer.Telephone = addCustomer.textBox_telephone.TrimedText;
					customer.Status = 1;
					customer.AccountBalance = 0;
					CommonMethods.setCDMDForAdd(customer);
					addCustomer.AddedId = add(customer);
					ShowMessage.success(Common.Messages.Success.Success002);
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		///////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////

		internal void UserControl_Loaded() {
			try {
				UIComboBox.yesNoForSelect(customerManager.comboBox_active_filter);
				customerManager.DataTable = new DataTable();
				customerManager.DataTable.Columns.Add("ID", typeof(int));
				customerManager.DataTable.Columns.Add("Name", typeof(String));
				customerManager.DataTable.Columns.Add("Address", typeof(String));
				customerManager.DataTable.Columns.Add("Telephone", typeof(String));
				customerManager.DataTable.Columns.Add("Account Balance", typeof(String));
				customerManager.DataTable.Columns.Add("Is Active", typeof(String));

				customerManager.DataGridFooter = new DataGridFooter();
				customerManager.dataGrid.IFooter = customerManager.DataGridFooter;
				customerManager.grid_footer.Children.Add(customerManager.DataGridFooter);
				customerManager.dataGrid.DataContext = customerManager.DataTable.DefaultView;

				customerManager.Pagination = new Pagination();
				customerManager.Pagination.Filter = customerManager;
				customerManager.grid_pagination.Children.Add(customerManager.Pagination);

				setRowsCount();
			} catch(Exception) {
			}
		}

		private Customer getCustomerForFilter() {
			Customer customer = null;
			try {
				customer = new Customer();
				customer.Name = "%" + customerManager.textBox_name_filter.TrimedText + "%";
				customer.Address = "%" + customerManager.textBox_address_filter.TrimedText + "%";
				customer.Telephone = "%" + customerManager.textBox_telephone_filter.TrimedText + "%";
				customer.AccountBalance = customerManager.textBox_account_filter.DoubleValue > 0 ? customerManager.textBox_account_filter.DoubleValue : -1;
				customer.Status = Convert.ToInt32(customerManager.comboBox_active_filter.SelectedValue) > -1 ? Convert.ToInt32(customerManager.comboBox_active_filter.SelectedValue) : -1;
			} catch(Exception) {
			}
			return customer;
		}

		internal void filter() {
			try {
				customerManager.DataTable.Rows.Clear();
				Customer c = getCustomerForFilter();
				c.LimitStart = customerManager.Pagination.LimitStart;
				c.LimitEnd = customerManager.Pagination.LimitCount;
				List<Customer> list = get(c);
				DataRow row = null;
				foreach(Customer customer in list) {
					row = customerManager.DataTable.NewRow();
					row[0] = customer.Id;
					row[1] = customer.Name;
					row[2] = customer.Address;
					row[3] = customer.Telephone;
					row[4] = customer.AccountBalance.ToString("#,##0.00");
					row[5] = CommonMethods.getYesNo(customer.Status);

					customerManager.DataTable.Rows.Add(row);
				}
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				Customer c = getCustomerForFilter();
				c.RowsCount = 1;
				List<Customer> list = get(c);
				customerManager.Pagination.RowsCount = list[0].RowsCount;
			} catch(Exception) {
			}
		}

		internal bool addNewCustomer() {
			bool b = false;
			try {
				bool isOkay = true;
				if(customerManager.textBox_name_add.IsNull()) {
					customerManager.textBox_name_add.ErrorMode(true);
					isOkay = false;
				}
				if(isDuplicate(customerManager.textBox_name_add.TrimedText, 0)) {
					customerManager.textBox_name_add.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					Customer customer = new Customer();
					customer.Name = customerManager.textBox_name_add.TrimedText;
					customer.Address = customerManager.textBox_address_add.TrimedText;
					customer.Telephone = customerManager.textBox_telephone_add.TrimedText;
					customer.AccountBalance = customerManager.textBox_account_add.DoubleValue;
					customer.Status = customerManager.checkBox_active_add.IsChecked == true ? 1 : 0;
					CommonMethods.setCDMDForAdd(customer);
					if(add(customer) > 0) {
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void resetAddForm() {
			try {
				customerManager.textBox_name_add.Clear();
				customerManager.textBox_address_add.Clear();
				customerManager.textBox_telephone_add.Clear();
				customerManager.textBox_account_add.Clear();
				customerManager.checkBox_active_add.IsChecked = true;
			} catch(Exception) {
			}
		}

		internal bool updateCustomer() {
			bool b = false;
			try {
				bool isOkay = true;
				if(customerManager.textBox_name_add.IsNull()) {
					customerManager.textBox_name_add.ErrorMode(true);
					isOkay = false;
				}
				if(isDuplicate(customerManager.textBox_name_add.TrimedText, customerManager.SelectedCustomer.Id)) {
					customerManager.textBox_name_add.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					Customer customer = customerManager.SelectedCustomer;
					customer.Name = customerManager.textBox_name_add.TrimedText;
					customer.Address = customerManager.textBox_address_add.TrimedText;
					customer.Telephone = customerManager.textBox_telephone_add.TrimedText;
					customer.AccountBalance = customerManager.textBox_account_add.DoubleValue;
					customer.Status = customerManager.checkBox_active_add.IsChecked == true ? 1 : 0;
					CommonMethods.setCDMDForUpdate(customer);
					if(upd(customer) > 0) {
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void switchToAddMode() {
			try {
				customerManager.SelectedCustomer = null;
				customerManager.IsUpdateMode = false;
				customerManager.button_save_add.Content = "Save";
				customerManager.button_reset_add.Content = "Reset";
			} catch(Exception) {
			}
		}

		internal void switchToUpdateMode() {
			try {
				customerManager.IsUpdateMode = true;
				Customer customer = getCustomerById(customerManager.dataGrid.SelectedItemID);
				customerManager.SelectedCustomer = customer;
				customerManager.textBox_name_add.Text = customer.Name;
				customerManager.textBox_address_add.Text = customer.Address;
				customerManager.textBox_telephone_add.Text = customer.Telephone;
				customerManager.textBox_account_add.DoubleValue = customer.AccountBalance;
				customerManager.checkBox_active_add.IsChecked = customer.Status == 1 ? true : false;

				customerManager.button_save_add.Content = "Update";
				customerManager.button_reset_add.Content = "Cancel";
			} catch(Exception) {
			}
		}
	}
}
