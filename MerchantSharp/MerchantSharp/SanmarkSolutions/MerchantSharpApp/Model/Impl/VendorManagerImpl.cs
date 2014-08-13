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
	class VendorManagerImpl {

		private IDao dao;
		private AddVendor addVendor;
		private VendorManager vendorManager;

		public VendorManagerImpl() {
			dao = VendorDao.getInstance();
		}

		public VendorManagerImpl(AddVendor addVendor) {
			this.addVendor = addVendor;
			dao = VendorDao.getInstance();
		}

		public VendorManagerImpl(VendorManager vendorManager) {
			this.vendorManager = vendorManager;
			dao = VendorDao.getInstance();
		}

		public int add(Entity entity) {
			try {
				if(Session.Permission["canAddVendor"] == 1) {
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

		public List<Vendor> get(Entity entity) {
			return dao.get(entity).Cast<Vendor>().ToList();
		}

		public int upd(Entity entity) {
			try {
				if(Session.Permission["canUpdateVendor"] == 1) {
					dao.upd(entity);
					return 1;
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return 0;
		}

		public bool isDuplicate(String name, int id) {
			bool b = false;
			try {
				Vendor vendor = new Vendor();
				vendor.Name = name;
				List<Vendor> list = get(vendor);
				if(list.Count > 0 && id > 0) {
					foreach(Vendor v in list) {
						if(v.Id != id) {
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

		public List<Vendor> getAllActivedVendors() {
			List<Vendor> list = null;
			try {
				Vendor vendor = new Vendor();
				vendor.Status = 1;
                vendor.OrderBy = "name ASC";
				list = get(vendor);
			} catch(Exception) {
			}
			return list;
		}

		internal double getAccountBalanceById(int id) {
			double d = 0;
			try {
				Vendor vendor = new Vendor();
				vendor.Id = id;
				d = get(vendor)[0].AccountBalance;
			} catch(Exception) {
			}
			return d;
		}

		internal Vendor getVendorById(int id) {
			Vendor vendor = null;
			try {
				Vendor v = new Vendor();
				v.Id = id;
				vendor = get(v)[0];
			} catch(Exception) {
			}
			return vendor;
		}

		internal String getVendorNameById(int id) {
			String vendor = null;
			try {
				Vendor v = new Vendor();
				v.Id = id;
				vendor = get(v)[0].Name;
			} catch(Exception) {
			}
			return vendor;
		}

		public void substractAccountBalanceById(int vendorId, double val) {
			try {
				Vendor vendor = getVendorById(vendorId);
				vendor.AccountBalance -= val;
				upd(vendor);
			} catch(Exception) {
			}
		}

		public void additionAccountBalanceById(int vendorId, double val) {
			try {
				Vendor vendor = getVendorById(vendorId);
				vendor.AccountBalance += val;
				upd(vendor);
			} catch(Exception) {
			}
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
				} else if(isDuplicate(addVendor.textBox_name.TrimedText, 0)) {
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

		///////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////

		internal void UserControl_Loaded() {
			try {
				UIComboBox.yesNoForSelect(vendorManager.comboBox_active_filter);
				vendorManager.DataTable = new DataTable();
				vendorManager.DataTable.Columns.Add("ID", typeof(int));
				vendorManager.DataTable.Columns.Add("Name", typeof(String));
				vendorManager.DataTable.Columns.Add("Address", typeof(String));
				vendorManager.DataTable.Columns.Add("Telephone", typeof(String));
				vendorManager.DataTable.Columns.Add("Account Balance", typeof(String));
				vendorManager.DataTable.Columns.Add("Is Active", typeof(String));

				vendorManager.DataGridFooter = new DataGridFooter();
				vendorManager.dataGrid.IFooter = vendorManager.DataGridFooter;
				vendorManager.grid_footer.Children.Add(vendorManager.DataGridFooter);
				vendorManager.dataGrid.DataContext = vendorManager.DataTable.DefaultView;

				vendorManager.Pagination = new Pagination();
				vendorManager.Pagination.Filter = vendorManager;
				vendorManager.grid_pagination.Children.Add(vendorManager.Pagination);

				setRowsCount();
			} catch(Exception) {
			}
		}

		private Vendor getVendorForFilter() {
			Vendor vendor = null;
			try {
				vendor = new Vendor();
				vendor.Name = "%" + vendorManager.textBox_name_filter.TrimedText + "%";
				vendor.Address = "%" + vendorManager.textBox_address_filter.TrimedText + "%";
				vendor.Telephone = "%" + vendorManager.textBox_telephone_filter.TrimedText + "%";
				vendor.AccountBalance = vendorManager.textBox_account_filter.DoubleValue > 0 ? vendorManager.textBox_account_filter.DoubleValue : -1;
				vendor.Status = Convert.ToInt32(vendorManager.comboBox_active_filter.SelectedValue) > -1 ? Convert.ToInt32(vendorManager.comboBox_active_filter.SelectedValue) : -1;
			} catch(Exception) {
			}
			return vendor;
		}

		internal void filter() {
			try {
				vendorManager.DataTable.Rows.Clear();
				Vendor v = getVendorForFilter();
				v.LimitStart = vendorManager.Pagination.LimitStart;
				v.LimitEnd = vendorManager.Pagination.LimitCount;
				List<Vendor> list = get(v);
				DataRow row = null;
				foreach(Vendor vendor in list) {
					row = vendorManager.DataTable.NewRow();
					row[0] = vendor.Id;
					row[1] = vendor.Name;
					row[2] = vendor.Address;
					row[3] = vendor.Telephone;
					row[4] = vendor.AccountBalance.ToString("#,##0.00");
					row[5] = CommonMethods.getYesNo(vendor.Status);

					vendorManager.DataTable.Rows.Add(row);
				}
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				Vendor v = getVendorForFilter();
				v.RowsCount = 1;
				List<Vendor> list = get(v);
				vendorManager.Pagination.RowsCount = list[0].RowsCount;
			} catch(Exception) {
			}
		}

		internal bool addNewVendor() {
			bool b = false;
			try {
				bool isOkay = true;
				if(vendorManager.textBox_name_addVendor.IsNull()) {
					vendorManager.textBox_name_addVendor.ErrorMode(true);
					isOkay = false;
				}
				if(isDuplicate(vendorManager.textBox_name_addVendor.TrimedText, 0)) {
					vendorManager.textBox_name_addVendor.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					Vendor vendor = new Vendor();
					vendor.Name = vendorManager.textBox_name_addVendor.TrimedText;
					vendor.Address = vendorManager.textBox_address_addVendor.TrimedText;
					vendor.Telephone = vendorManager.textBox_telephone_addVendor.TrimedText;
					vendor.AccountBalance = vendorManager.textBox_account_addVendor.DoubleValue;
					vendor.Status = vendorManager.checkBox_active_addVendor.IsChecked == true ? 1 : 0;
					CommonMethods.setCDMDForAdd(vendor);
					if(add(vendor) > 0) {
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void resetAddForm() {
			try {
				vendorManager.textBox_name_addVendor.Clear();
				vendorManager.textBox_address_addVendor.Clear();
				vendorManager.textBox_telephone_addVendor.Clear();				
				vendorManager.textBox_account_addVendor.Clear();
				vendorManager.checkBox_active_addVendor.IsChecked = true;
			} catch(Exception) {
			}
		}

		internal bool updateVendor() {
			bool b = false;
			try {
				bool isOkay = true;
				if(vendorManager.textBox_name_addVendor.IsNull()) {
					vendorManager.textBox_name_addVendor.ErrorMode(true);
					isOkay = false;
				}
				if(isDuplicate(vendorManager.textBox_name_addVendor.TrimedText, vendorManager.SelectedVendor.Id)) {
					vendorManager.textBox_name_addVendor.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					Vendor vendor = vendorManager.SelectedVendor;
					vendor.Name = vendorManager.textBox_name_addVendor.TrimedText;
					vendor.Address = vendorManager.textBox_address_addVendor.TrimedText;
					vendor.Telephone = vendorManager.textBox_telephone_addVendor.TrimedText;
					vendor.AccountBalance = vendorManager.textBox_account_addVendor.DoubleValue;
					vendor.Status = vendorManager.checkBox_active_addVendor.IsChecked == true ? 1 : 0;
					CommonMethods.setCDMDForUpdate(vendor);
					if(upd(vendor) > 0) {
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void switchToAddMode() {
			try {
				vendorManager.SelectedVendor = null;
				vendorManager.IsUpdateMode = false;
				vendorManager.button_save_addVendor.Content = "Save";
				vendorManager.button_reset_addVendor.Content = "Reset";
			} catch(Exception) {
			}
		}

		internal void switchToUpdateMode() {
			try {
				vendorManager.IsUpdateMode = true;
				Vendor vendor = getVendorById(vendorManager.dataGrid.SelectedItemID);
				vendorManager.SelectedVendor = vendor;
				vendorManager.textBox_name_addVendor.Text = vendor.Name;
				vendorManager.textBox_address_addVendor.Text = vendor.Address;
				vendorManager.textBox_telephone_addVendor.Text = vendor.Telephone;
				vendorManager.textBox_account_addVendor.DoubleValue = vendor.AccountBalance;
				vendorManager.checkBox_active_addVendor.IsChecked = vendor.Status == 1 ? true : false;

				vendorManager.button_save_addVendor.Content = "Update";
				vendorManager.button_reset_addVendor.Content = "Cancel";
			} catch(Exception) {
			}
		}
	}
}
