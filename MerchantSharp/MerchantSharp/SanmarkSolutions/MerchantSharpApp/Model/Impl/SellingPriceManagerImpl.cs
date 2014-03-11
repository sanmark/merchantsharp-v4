using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class SellingPriceManagerImpl {

		private IDao dao = null;
		private AddSellingPrice addSellingPrice;
		private SellingPriceManager sellingPriceManager;
		private ItemManagerImpl itemManagerImpl;
		private CompanyManagerImpl companyManagerImpl;
		private CategoryManagerImpl categoryManagerImpl;

		public SellingPriceManagerImpl() {
			dao = SellingPriceDao.getInstance();
		}

		public SellingPriceManagerImpl(AddSellingPrice addSellingPrice) {
			this.addSellingPrice = addSellingPrice;
			dao = SellingPriceDao.getInstance();
		}

		public SellingPriceManagerImpl(SellingPriceManager sellingPriceManager) {
			this.sellingPriceManager = sellingPriceManager;
			dao = SellingPriceDao.getInstance();
			itemManagerImpl = new ItemManagerImpl();
			companyManagerImpl = new CompanyManagerImpl();
			categoryManagerImpl = new CategoryManagerImpl();
		}

		public int add(Entity entity) {
			try {
				if(Session.Permission["canAddSellingPrice"] == 1) {
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
				if(Session.Permission["canDeleteSellingPrice"] == 1) {
					return dao.del(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
					return false;
				}
			} catch(Exception) {
				return false;
			}
		}

		public List<SellingPrice> get(Entity entity) {
			return dao.get(entity).Cast<SellingPrice>().ToList();
		}

		public int upd(Entity entity) {
			try {
				if(Session.Permission["canUpdateSellingPrice"] == 1) {
					return dao.upd(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
					return 0;
				}
			} catch(Exception) {
				return 0;
			}
		}

		public List<SellingPrice> getSellingPriceByItemAndMode(int itemId, String mode) {
			List<SellingPrice> list = null;
			try {
				SellingPrice sellingPrice = new SellingPrice();
				sellingPrice.ItemId = itemId;
				sellingPrice.Mode = mode;
				list = get(sellingPrice);
			} catch(Exception) {
			}
			return list;
		}

		public List<SellingPrice> getSellingPriceByItemId(int itemId) {
			List<SellingPrice> list = null;
			try {
				SellingPrice sellingPrice = new SellingPrice();
				sellingPrice.ItemId = itemId;
				list = get(sellingPrice);
			} catch(Exception) {
			}
			return list;
		}

		public SellingPrice getSellingPriceById(int id) {
			SellingPrice sellingPrice = null;
			try {
				SellingPrice p = new SellingPrice();
				p.Id = id;
				sellingPrice = get(p)[0];
			} catch(Exception) {
			}
			return sellingPrice;
		}

		public bool isDublicatePrice(int itemId, String mode, double price, int id) {
			bool b = false;
			try {
				SellingPrice sellingPrice = new SellingPrice();
				sellingPrice.ItemId = itemId;
				sellingPrice.Mode = mode;
				//sellingPrice.Price = price;
				List<SellingPrice> list = get(sellingPrice);
				if(id < 1) {
					foreach(SellingPrice p in list) {
						if(price == p.Price) {
							b = true;
							return b;
						}
					}
				} else if(list.Count > 0) {
					foreach(SellingPrice p in list) {
						if(p.Price == price && p.Id != id) {
							b = true;
							return b;
						}
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		public bool deleteSellingPriceById(int id) {
			bool b = false;
			try {
				SellingPrice sellingPrice = new SellingPrice();
				sellingPrice.Id = id;
				b = del(sellingPrice);
			} catch(Exception) {
			}
			return b;
		}


		/// ********************************************************************************************************* ///

		internal bool addPriceFromAddSellingPrice() {
			bool b = false;
			try {
				if(addSellingPrice.textBox_price.DoubleValue == 0) {
					ShowMessage.error(Common.Messages.Error.Error006);
				} else if(isDublicatePrice(addSellingPrice.ItemId, addSellingPrice.Mode, addSellingPrice.textBox_price.DoubleValue, 0)) {
					ShowMessage.error(Common.Messages.Error.Error007);
				} else {
					SellingPrice sellingPrice = new SellingPrice();
					sellingPrice.ItemId = addSellingPrice.ItemId;
					sellingPrice.Mode = addSellingPrice.Mode;
					sellingPrice.Price = addSellingPrice.textBox_price.DoubleValue;
					CommonMethods.setCDMDForAdd(sellingPrice);
					int id = add(sellingPrice);
					UIComboBox.sellingPriceForItemAndMode(addSellingPrice.ComboBox, addSellingPrice.ItemId, addSellingPrice.Mode, addSellingPrice);
					addSellingPrice.ComboBox.SelectedValue = id;					
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////

		internal void UserControl_Loaded() {
			try {
				sellingPriceManager.DataTableUnitPrice = new DataTable();
				sellingPriceManager.DataTableUnitPrice.Columns.Add("ID", typeof(int));
				sellingPriceManager.DataTableUnitPrice.Columns.Add("Price", typeof(String));
				sellingPriceManager.dataGrid_unitPrice.DataContext = sellingPriceManager.DataTableUnitPrice.DefaultView;
				sellingPriceManager.dataGrid_unitPrice.Columns[1].Width = 150;

				sellingPriceManager.DataTablePackPrice = new DataTable();
				sellingPriceManager.DataTablePackPrice.Columns.Add("ID", typeof(int));
				sellingPriceManager.DataTablePackPrice.Columns.Add("Price", typeof(String));
				sellingPriceManager.dataGrid_packPrice.DataContext = sellingPriceManager.DataTablePackPrice.DefaultView;
				sellingPriceManager.dataGrid_packPrice.Columns[1].Width = 150;

				sellingPriceManager.ItemFinder = new ItemFinder(sellingPriceManager.textBox_selectedItemId);
				sellingPriceManager.grid_itemFinder.Children.Add(sellingPriceManager.ItemFinder);
				if(sellingPriceManager.SelectedItemId > 0) {
					sellingPriceManager.grid_itemFinder.IsEnabled = false;
					sellingPriceManager.textBox_selectedItemId.Text = sellingPriceManager.SelectedItemId.ToString();
				}
			} catch(Exception) {
				emergencyLoad();
			}
		}

		private void emergencyLoad() {
			try {
				sellingPriceManager.ItemFinder = new ItemFinder(sellingPriceManager.textBox_selectedItemId);
				sellingPriceManager.grid_itemFinder.Children.Add(sellingPriceManager.ItemFinder);
				if ( sellingPriceManager.SelectedItemId > 0 ) {
					sellingPriceManager.grid_itemFinder.IsEnabled = false;
					sellingPriceManager.textBox_selectedItemId.Text = sellingPriceManager.SelectedItemId.ToString();
				}
			} catch ( Exception ) {
			}
		}

		internal void loadPricesForItemId() {
			try {
				sellingPriceManager.DataTableUnitPrice.Rows.Clear();
				sellingPriceManager.DataTablePackPrice.Rows.Clear();
				Item item = itemManagerImpl.getItemById(sellingPriceManager.textBox_selectedItemId.IntValue);
				sellingPriceManager.SelectedItemId = item.Id;
				sellingPriceManager.label_selectedItem.Content = companyManagerImpl.getCompanyNameById(item.CompanyId) + ", " + item.Name + " " + categoryManagerImpl.getCategoryNameById(item.CategoryId);
				List<SellingPrice> list = getSellingPriceByItemId(sellingPriceManager.textBox_selectedItemId.IntValue);
				DataRow dataRow = null;
				foreach(SellingPrice sellingPrice in list) {
					if(sellingPrice.Mode == "u") {
						dataRow = sellingPriceManager.DataTableUnitPrice.NewRow();
						sellingPriceManager.DataTableUnitPrice.Rows.Add(dataRow);
					} else {
						dataRow = sellingPriceManager.DataTablePackPrice.NewRow();
						sellingPriceManager.DataTablePackPrice.Rows.Add(dataRow);
					}
					dataRow[0] = sellingPrice.Id;
					dataRow[1] = sellingPrice.Price.ToString("#,##0.00");
				}
			} catch(Exception) {
			}
		}

		internal bool addUnitPrice() {
			bool b = false;
			try {
				bool isOkay = true;
				if(sellingPriceManager.textBox_unitPrice.IsNull()) {
					sellingPriceManager.textBox_unitPrice.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					if(isDublicatePrice(sellingPriceManager.textBox_selectedItemId.IntValue, "u", sellingPriceManager.textBox_unitPrice.DoubleValue, 0)) {
						ShowMessage.error(Common.Messages.Error.Error007);
					} else {
						SellingPrice sellingPrice = new SellingPrice();
						sellingPrice.ItemId = sellingPriceManager.textBox_selectedItemId.IntValue;
						sellingPrice.Mode = "u";
						sellingPrice.Price = sellingPriceManager.textBox_unitPrice.DoubleValue;
						CommonMethods.setCDMDForAdd(sellingPrice);
						if(add(sellingPrice) > 0) {
							b = true;
						}
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void resetUnitForm() {
			try {
				sellingPriceManager.textBox_unitPrice.Clear();
			} catch(Exception) {
			}
		}

		internal bool updateUnitPrice() {
			bool b = false;
			try {
				bool isOkay = true;
				if(sellingPriceManager.textBox_unitPrice.IsNull()) {
					sellingPriceManager.textBox_unitPrice.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					if(isDublicatePrice(sellingPriceManager.textBox_selectedItemId.IntValue, "u", sellingPriceManager.textBox_unitPrice.DoubleValue, sellingPriceManager.UnitPrice.Id)) {
						ShowMessage.error(Common.Messages.Error.Error007);
					} else {
						SellingPrice sellingPrice = sellingPriceManager.UnitPrice;
						sellingPrice.Price = sellingPriceManager.textBox_unitPrice.DoubleValue;
						CommonMethods.setCDMDForUpdate(sellingPrice);
						upd(sellingPrice);
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void switchToUpdateMode(String mode) {
			try {
				SellingPrice sellingPrice = getSellingPriceById((mode == "u") ? sellingPriceManager.dataGrid_unitPrice.SelectedItemID : sellingPriceManager.dataGrid_packPrice.SelectedItemID);
				if(mode == "u") {
					sellingPriceManager.IsUnitUpdateMode = true;
					sellingPriceManager.UnitPrice = sellingPrice;
					sellingPriceManager.textBox_unitPrice.Text = sellingPrice.Price.ToString("#,##0.00");
					sellingPriceManager.button_addUnitPrice.Content = "Update";
				} else {
					sellingPriceManager.IsPackUpdateMode = true;
					sellingPriceManager.PackPrice = sellingPrice;
					sellingPriceManager.textBox_packPrice.Text = sellingPrice.Price.ToString("#,##0.00");
					sellingPriceManager.button_addPackPrice.Content = "Update";
				}
			} catch(Exception) {
			}
		}

		internal void switchToAddMode(String mode) {
			try {
				if(mode == "u") {
					sellingPriceManager.IsUnitUpdateMode = false;
					sellingPriceManager.UnitPrice = null;
					sellingPriceManager.button_addUnitPrice.Content = "Add";
				} else {
					sellingPriceManager.IsPackUpdateMode = false;
					sellingPriceManager.PackPrice = null;
					sellingPriceManager.button_addPackPrice.Content = "Add";
				}
			} catch(Exception) {
			}
		}

		internal bool addPackPrice() {
			bool b = false;
			try {
				bool isOkay = true;
				if(sellingPriceManager.textBox_packPrice.IsNull()) {
					sellingPriceManager.textBox_packPrice.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					if(isDublicatePrice(sellingPriceManager.textBox_selectedItemId.IntValue, "p", sellingPriceManager.textBox_packPrice.DoubleValue, 0)) {
						ShowMessage.error(Common.Messages.Error.Error007);
					} else {
						SellingPrice sellingPrice = new SellingPrice();
						sellingPrice.ItemId = sellingPriceManager.textBox_selectedItemId.IntValue;
						sellingPrice.Mode = "p";
						sellingPrice.Price = sellingPriceManager.textBox_packPrice.DoubleValue;
						CommonMethods.setCDMDForAdd(sellingPrice);
						if(add(sellingPrice) > 0) {
							b = true;
						}
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void resetPackForm() {
			try {
				sellingPriceManager.textBox_packPrice.Clear();
			} catch(Exception) {
			}
		}

		internal bool updatePackPrice() {
			bool b = false;
			try {
				bool isOkay = true;
				if(sellingPriceManager.textBox_packPrice.IsNull()) {
					sellingPriceManager.textBox_packPrice.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					if(isDublicatePrice(sellingPriceManager.textBox_selectedItemId.IntValue, "p", sellingPriceManager.textBox_packPrice.DoubleValue, sellingPriceManager.PackPrice.Id)) {
						ShowMessage.error(Common.Messages.Error.Error007);
					} else {
						SellingPrice sellingPrice = sellingPriceManager.PackPrice;
						sellingPrice.Price = sellingPriceManager.textBox_packPrice.DoubleValue;
						CommonMethods.setCDMDForUpdate(sellingPrice);
						upd(sellingPrice);
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void deletePrice(String mode) {
			try {
				SellingPrice sellingPrice = null;
				if(mode == "u") {
					sellingPrice = getSellingPriceById(sellingPriceManager.dataGrid_unitPrice.SelectedItemID);
				} else {
					sellingPrice = getSellingPriceById(sellingPriceManager.dataGrid_packPrice.SelectedItemID);
				}
				del(sellingPrice);
			} catch(Exception) {
			}
		}
	}
}
