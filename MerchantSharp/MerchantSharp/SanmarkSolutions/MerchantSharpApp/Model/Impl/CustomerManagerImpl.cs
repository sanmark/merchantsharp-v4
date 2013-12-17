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
	class CustomerManagerImpl {

		private IDao dao = null;
		private AddCustomer addCustomer;

		public CustomerManagerImpl() {
			dao = CustomerDao.getInstance();
		}

		public CustomerManagerImpl(AddCustomer addCustomer) {
			this.addCustomer = addCustomer;
			dao = CustomerDao.getInstance();
		}

		public int add(Entity entity) {
			return dao.add(entity);
		}

		public bool del(Entity entity) {
			return dao.del(entity);
		}

		public List<Customer> get(Entity entity) {
			return dao.get(entity).Cast<Customer>().ToList();
		}

		public int upd(Entity entity) {
			return dao.upd(entity);
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

		private bool isDublicate(string name) {
			bool b = false;
			try {
				Customer customer = new Customer();
				customer.Name = name;
				if(get(customer).Count > 0) {
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
				} else if(isDublicate(addCustomer.textBox_name.TrimedText)) {
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
		
	}
}
