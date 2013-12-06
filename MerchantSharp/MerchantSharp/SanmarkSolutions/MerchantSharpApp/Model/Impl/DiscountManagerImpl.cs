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
	class DiscountManagerImpl {

		private IDao dao = null;
		private DiscountManager discountManager;
		private ItemManagerImpl itemManagerImpl = null;
		private CompanyManagerImpl companyManagerImpl = null;
		private CategoryManagerImpl categoryManagerImpl = null;

		public DiscountManagerImpl() {
			dao = DiscountDao.getInstance();
		}

		public DiscountManagerImpl(DiscountManager discountManager) {
			this.discountManager = discountManager;
			dao = DiscountDao.getInstance();
			itemManagerImpl = new ItemManagerImpl();
			companyManagerImpl = new CompanyManagerImpl();
			categoryManagerImpl = new CategoryManagerImpl();
		}

		public int add(Entity entity) {
			try {
				if(Session.Permission["canAddDiscount"] == 1) {
					return dao.add(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
					return 0;
				}
			} catch(Exception) {
				return 0;
			}
		}

		public bool del(Entity entity) {
			try {
				if(Session.Permission["canDeleteDiscount"] == 1) {
					return dao.del(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
					return false;
				}
			} catch(Exception) {
				return false;
			}
		}

		public List<Discount> get(Entity entity) {
			return dao.get(entity).Cast<Discount>().ToList();
		}

		public int upd(Entity entity) {
			try {
				if(Session.Permission["canUpdateDiscount"] == 1) {
					return dao.upd(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
					return 0;
				}
			} catch(Exception) {
				return 0;
			}
		}

		public List<Discount> getDiscountsByItemId(int itemId) {
			List<Discount> list = null;
			try {
				Discount discount = new Discount();
				discount.ItemId = itemId;
				list = get(discount);
			} catch(Exception) {
			}
			return list;
		}

		public Discount getDiscountById(int id) {
			Discount discount = new Discount();
			try {
				discount.Id = id;
				discount = get(discount)[0];
			} catch(Exception) {
				discount = null;
			}
			return discount;
		}

		public bool isDublicate(int itemId, double quantity, String mode, int id) {
			bool b = false;
			try {
				Discount discount = new Discount();
				discount.ItemId = itemId;
				discount.Quantity = quantity;
				discount.Mode = mode;
				List<Discount> list = get(discount);
				if(id > 0) {
					foreach(Discount dis in list) {
						if(dis.Id != id) {
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


		/////////////////////////////////////////////////////////////////////////////////////////

		internal void loadUI() {
			try {
				discountManager.DataTableUnitDiscount = new DataTable();
				discountManager.DataTableUnitDiscount.Columns.Add("ID", typeof(int));
				discountManager.DataTableUnitDiscount.Columns.Add("Quantity", typeof(String));
				discountManager.DataTableUnitDiscount.Columns.Add("Value", typeof(String));
				discountManager.dataGrid_unitDiscount.DataContext = discountManager.DataTableUnitDiscount.DefaultView;

				discountManager.DataTablePackDiscount = new DataTable();
				discountManager.DataTablePackDiscount.Columns.Add("ID", typeof(int));
				discountManager.DataTablePackDiscount.Columns.Add("Quantity", typeof(String));
				discountManager.DataTablePackDiscount.Columns.Add("Value", typeof(String));
				discountManager.dataGrid_packDiscount.DataContext = discountManager.DataTablePackDiscount.DefaultView;

				discountManager.ItemFinder = new ItemFinder(discountManager.textBox_selectedItemId);
				discountManager.grid_itemFinder.Children.Add(discountManager.ItemFinder);
				if(discountManager.SelectedItemId > 0) {
					discountManager.grid_itemFinder.IsEnabled = false;
					discountManager.textBox_selectedItemId.Text = discountManager.SelectedItemId.ToString();
				}
			} catch(Exception) {
			}
		}

		internal void loadDiscountsForItemId() {
			try {
				discountManager.DataTableUnitDiscount.Rows.Clear();
				discountManager.DataTablePackDiscount.Rows.Clear();
				Item item = itemManagerImpl.getItemById(discountManager.textBox_selectedItemId.IntValue);
				discountManager.label_selectedItem.Content = companyManagerImpl.getCompanyNameById(item.CompanyId) + ", " + item.Name + " " + categoryManagerImpl.getCategoryNameById(item.CategoryId);
				List<Discount> list = getDiscountsByItemId(discountManager.textBox_selectedItemId.IntValue);
				DataRow dataRow = null;
				foreach(Discount discount in list) {
					if(discount.Mode == "u") {
						dataRow = discountManager.DataTableUnitDiscount.NewRow();
						discountManager.DataTableUnitDiscount.Rows.Add(dataRow);
					} else {
						dataRow = discountManager.DataTablePackDiscount.NewRow();
						discountManager.DataTablePackDiscount.Rows.Add(dataRow);
					}
					dataRow[0] = discount.Id;
					dataRow[1] = discount.Quantity.ToString("#,##0.00");
					dataRow[2] = discount.Value.ToString("#,##0.00");
				}
			} catch(Exception) {
			}
		}

		internal bool addUnitDiscount() {
			bool b = false;
			try {
				bool isOkay = true;
				if(discountManager.textBox_unitDiscount.IsNull()) {
					discountManager.textBox_unitDiscount.ErrorMode(true);
					isOkay = false;
				}
				if(discountManager.textBox_unitQuantity.IsNull()) {
					discountManager.textBox_unitQuantity.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					if(isDublicate(discountManager.textBox_selectedItemId.IntValue, discountManager.textBox_unitQuantity.DoubleValue, "u", 0)) {
						ShowMessage.error(Common.Messages.Error.Error007);
					} else {
						Discount discount = new Discount();
						discount.ItemId = discountManager.textBox_selectedItemId.IntValue;
						discount.Mode = "u";
						discount.Quantity = discountManager.textBox_unitQuantity.DoubleValue;
						discount.Value = discountManager.textBox_unitDiscount.DoubleValue;
						CommonMethods.setCDMDForAdd(discount);
						add(discount);
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void resetUnitForm() {
			try {
				discountManager.textBox_unitDiscount.Clear();
				discountManager.textBox_unitQuantity.Clear();
			} catch(Exception) {
			}
		}

		internal bool addPackDiscount() {
			bool b = false;
			try {
				bool isOkay = true;
				if(discountManager.textBox_packDiscount.IsNull()) {
					discountManager.textBox_packDiscount.ErrorMode(true);
					isOkay = false;
				}
				if(discountManager.textBox_packQuantity.IsNull()) {
					discountManager.textBox_packQuantity.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					if(isDublicate(discountManager.textBox_selectedItemId.IntValue, discountManager.textBox_packQuantity.DoubleValue, "p", 0)) {
						ShowMessage.error(Common.Messages.Error.Error007);
					} else {
						Discount discount = new Discount();
						discount.ItemId = discountManager.textBox_selectedItemId.IntValue;
						discount.Mode = "p";
						discount.Quantity = discountManager.textBox_packQuantity.DoubleValue;
						discount.Value = discountManager.textBox_packDiscount.DoubleValue;
						CommonMethods.setCDMDForAdd(discount);
						add(discount);
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void resetPackForm() {
			try {
				discountManager.textBox_packDiscount.Clear();
				discountManager.textBox_packQuantity.Clear();
			} catch(Exception) {
			}
		}

		internal void switchToUpdateMode(String mode) {
			try {
				Discount discount = getDiscountById((mode == "u") ? discountManager.dataGrid_unitDiscount.SelectedItemID : discountManager.dataGrid_packDiscount.SelectedItemID);
				if(mode == "u") {
					discountManager.IsUnitUpdateMode = true;
					discountManager.UnitDiscount = discount;
					discountManager.textBox_unitDiscount.Text = discount.Value.ToString("#,##0.00");
					discountManager.textBox_unitQuantity.Text = discount.Quantity.ToString("#,##0.00");
					discountManager.button_addUnitDiscount.Content = "Update";
				} else {
					discountManager.IsPackUpdateMode = true;
					discountManager.PackDiscount = discount;
					discountManager.textBox_packDiscount.Text = discount.Value.ToString("#,##0.00");
					discountManager.textBox_packQuantity.Text = discount.Quantity.ToString("#,##0.00");
					discountManager.button_addPackDiscount.Content = "Update";
				}
			} catch(Exception) {
			}
		}

		internal void switchToAddMode(String mode) {
			try {
				if(mode == "u") {
					discountManager.IsUnitUpdateMode = false;
					discountManager.UnitDiscount = null;
					discountManager.button_addUnitDiscount.Content = "Add";
				} else {
					discountManager.IsPackUpdateMode = false;
					discountManager.PackDiscount = null;
					discountManager.button_addPackDiscount.Content = "Add";
				}
			} catch(Exception) {
			}
		}

		internal bool updateUnitDiscount() {
			bool b = false;
			try {
				bool isOkay = true;
				if(discountManager.textBox_unitDiscount.IsNull()) {
					discountManager.textBox_unitDiscount.ErrorMode(true);
					isOkay = false;
				}
				if(discountManager.textBox_unitQuantity.IsNull()) {
					discountManager.textBox_unitQuantity.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					if(isDublicate(discountManager.textBox_selectedItemId.IntValue, discountManager.textBox_unitQuantity.DoubleValue, "u", discountManager.UnitDiscount.Id)) {
						ShowMessage.error(Common.Messages.Error.Error007);
					} else {
						Discount discount = discountManager.UnitDiscount;
						discount.Quantity = discountManager.textBox_unitQuantity.DoubleValue;
						discount.Value = discountManager.textBox_unitDiscount.DoubleValue;
						CommonMethods.setCDMDForUpdate(discount);
						upd(discount);
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal bool updatePackDiscount() {
			bool b = false;
			try {
				bool isOkay = true;
				if(discountManager.textBox_packDiscount.IsNull()) {
					discountManager.textBox_packDiscount.ErrorMode(true);
					isOkay = false;
				}
				if(discountManager.textBox_packQuantity.IsNull()) {
					discountManager.textBox_packQuantity.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					if(isDublicate(discountManager.textBox_selectedItemId.IntValue, discountManager.textBox_packQuantity.DoubleValue, "p", discountManager.PackDiscount.Id)) {
						ShowMessage.error(Common.Messages.Error.Error007);
					} else {
						Discount discount = discountManager.PackDiscount;
						discount.Quantity = discountManager.textBox_packQuantity.DoubleValue;
						discount.Value = discountManager.textBox_packDiscount.DoubleValue;
						CommonMethods.setCDMDForUpdate(discount);
						upd(discount);
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void deleteDiscount(String mode) {
			try {
				Discount discount = null;
				if(mode == "u"){
					discount = getDiscountById(discountManager.dataGrid_unitDiscount.SelectedItemID);
				}else{
					discount = getDiscountById(discountManager.dataGrid_packDiscount.SelectedItemID);
				}
				del(discount);
			} catch(Exception) {
			}
		}
	}
}
