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
	class ItemManagerImpl {

		private IDao itemDao;
		private ItemManager itemManager;
		private CategoryManagerImpl categoryManagerImpl;
		private CompanyManagerImpl companyManagerImpl;
		private UnitManagerImpl unitManagerImpl;
		private StockManagerImpl stockManagerImpl;

		public ItemManagerImpl() {
			itemDao = ItemDao.getInstance();
		}

		public ItemManagerImpl(ItemManager itemManager) {
			itemDao = ItemDao.getInstance();
			categoryManagerImpl = new CategoryManagerImpl();
			companyManagerImpl = new CompanyManagerImpl();
			unitManagerImpl = new UnitManagerImpl();
			stockManagerImpl = new StockManagerImpl();
			this.itemManager = itemManager;
		}

		public int add(Entity entity) {
			try {
				if(Session.Permission["canAddItem"] == 1) {
					return itemDao.add(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
					return 0;
				}
			} catch(Exception) {
				return 0;
			}
		}

		public bool del(Entity entity) {
			return itemDao.del(entity);
		}

		public List<Item> get(Entity entity) {
			return itemDao.get(entity).Cast<Item>().ToList();
		}

		public int upd(Entity entity) {
			try {
				if(Session.Permission["canUpdateItem"] == 1) {
					return itemDao.upd(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
					return 0;
				}
			} catch(Exception) {
				return 0;
			}
		}

		/// <summary>
		/// Will get item by id
		/// </summary>
		/// <param name="itemId">ID of Item</param>
		/// <returns>Item</returns>
		internal Item getItemById(int itemId) {
			Item item = null;
			try {
				if(itemId > 0) {
					item = new Item();
					item.Id = itemId;
					item = get(item)[0];
				} else {
					item = null;
				}
			} catch(Exception) {
				item = null;
			}
			return item;
		}

		/// <summary>
		/// Will get Item by code
		/// </summary>
		/// <param name="code">The code of Item</param>
		/// <returns>Item Id</returns>
		internal int getItemIdByCode(string code) {
			int itemId = 0;
			try {
				Item item = new Item();
				item.Code = code;
				itemId = get(item)[0].Id;
			} catch(Exception) {
			}
			return itemId;
		}

		internal object getItemNameById(int itemId) {
			String name = null;
			try {
				if(itemId > 0) {
					Item item  = new Item();
					item.Id = itemId;
					name = get(item)[0].Name;
				} else {
				}
			} catch(Exception) {
			}
			return name;
		}

		public List<int> getCompanyIdsForCategory(int catId) {
			List<int> ids = new List<int>();
			try {
				Item item = new Item();
				item.CategoryId = catId;
				List<Item> list = get(item);
				foreach(Item i in list) {
					if(ids.IndexOf(i.CompanyId) < 0) {
						ids.Add(i.CompanyId);
					}
				}
			} catch(Exception) {
			}
			return ids;
		}

		public List<Item> getItemsForCategoryAndCompany(int catId, int comId) {
			List<Item> items = null;
			try {
				Item item = new Item();
				item.CategoryId = catId;
				item.CompanyId = comId;
				items = get(item);
			} catch(Exception) {
			}
			return items;
		}

		public bool isDublicateCode(String code, int id) {
			bool b = false;
			try {
				Item item = new Item();
				item.Code = code;
				if(id > 0) {
					List<Item> list = get(item);
					foreach(Item i in list) {
						if(i.Id != id) {
							b = true;
							break;
						}
					}
				} else {
					if(get(item).Count > 0) {
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		public bool isDublicateBarcode(String barCode, int id) {
			bool b = false;
			try {
				Item item = new Item();
				item.Barcode = barCode;
				if(id > 0) {
					List<Item> list = get(item);
					foreach(Item i in list) {
						if(i.Id != id) {
							b = true;
							break;
						}
					}
				} else {
					if(get(item).Count > 0) {
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		public bool isDublicateName(String name, int id) {
			bool b = false;
			try {
				Item item = new Item();
				item.Name = name;
				if(id > 0) {
					List<Item> list = get(item);
					foreach(Item i in list) {
						if(i.Id != id) {
							b = true;
							break;
						}
					}
				} else {
					if(get(item).Count > 0) {
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		public int getNextCode() {
			int code = 0;
			try {
				List<Item> itemList = null;
				Item item = null;
				String shortCodeGenerateBy = Session.Preference["itemCodeGenerateBy"];
				if(shortCodeGenerateBy == null) {
					shortCodeGenerateBy = "f";
				}

				int intCode = 0;
				if(shortCodeGenerateBy == "l") {
					itemList = get(new Item());
					item = itemList[itemList.Count - 1];
					intCode = Convert.ToInt32(item.Code);
				} else if(shortCodeGenerateBy == "f") {
					intCode = 0;
				}
				bool run = true;

				int intNewCode = intCode + 1;

				while(run) {
					if(intNewCode > 999999999) {
						run = false;
					} else {
						code = intNewCode;
					}
					if(!isDublicateCode(code.ToString(), 0)) {
						run = false;
					} else {
						intNewCode++;
					}
				}
			} catch(Exception) {
			}
			return code;
		}

		public int getNextCode(int categoryId, int companyId) {
			int code = 0;
			try {
				List<Item> itemList = null;
				Item item = null;
				String shortCodeGenerateBy = Session.Preference["itemCodeGenerateBy"];
				if(shortCodeGenerateBy == null) {
					shortCodeGenerateBy = "f";
				}

				int intCode = 0;
				if(shortCodeGenerateBy == "l") {
					itemList = get(new Item());
					item = itemList[itemList.Count - 1];
					intCode = Convert.ToInt32(item.Code);
				} else if(shortCodeGenerateBy == "f") {
					intCode = 0;
				}
				bool run = true;

				int intNewCode = intCode + 1;
				if(categoryId > 0 && companyId > 0) {
					try {
						item = new Item();
						item.CategoryId = categoryId;
						item.CompanyId = companyId;
						itemList = get(item);
						intNewCode = (Convert.ToInt32(itemList[itemList.Count - 1].Code)) + 1;
					} catch(Exception) {
						intNewCode = intCode + 1;
					}
				}
				while(run) {
					if(intNewCode > 999999999) {
						run = false;
					} else {
						code = intNewCode;
					}
					if(!isDublicateCode(code.ToString(), 0)) {
						run = false;
					} else {
						intNewCode++;
					}
				}
			} catch(Exception) {
			}
			return code;
		}



		//////////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////////////


		internal void UserControl_Loaded() {
			try {
				itemManager.Pagination = new Pagination();
				itemManager.Pagination.Filter = itemManager;
				itemManager.grid_pagination.Children.Add(itemManager.Pagination);
				itemManager.textBox_category_basicDetails.ListBox = itemManager.listBox_category_basicDetails;
				itemManager.textBox_company_basicDetails.ListBox = itemManager.listBox_company_basicDetails;

				UIComboBox.unitsForAddItem(itemManager.comboBox_unit_sellingDetails);
				UIListBox.loadCategories(itemManager.listBox_category_basicDetails, null);
				UIListBox.loadCompanies(itemManager.listBox_company_basicDetails, null);

				UIComboBox.categoriesForSelect(itemManager.comboBox_category_filter);
				UIComboBox.companiesForSelect(itemManager.comboBox_company_filter);
				UIComboBox.yesNoForSelect(itemManager.comboBox_sip_filter);
				UIComboBox.unitsForFilter(itemManager.comboBox_unit_filter);
				UIComboBox.yesNoForSelect(itemManager.comboBox_isActive_filter);

				itemManager.DataTable = new DataTable();
				itemManager.DataTable.Columns.Add("ID", typeof(int));
				itemManager.DataTable.Columns.Add("Category", typeof(String));
				itemManager.DataTable.Columns.Add("Company", typeof(String));
				itemManager.DataTable.Columns.Add("Name", typeof(String));
				itemManager.DataTable.Columns.Add("Unit", typeof(String));
				itemManager.DataTable.Columns.Add("Item Code", typeof(String));
				itemManager.DataTable.Columns.Add("Barcode", typeof(String));
				itemManager.DataTable.Columns.Add("POS Name", typeof(String));
				itemManager.DataTable.Columns.Add("QTY Per Pack", typeof(String));
				itemManager.DataTable.Columns.Add("Pack Name", typeof(String));
				itemManager.DataTable.Columns.Add("RO Level", typeof(String));
				itemManager.dataGrid_items_items.DataContext = itemManager.DataTable.DefaultView;
				checkBox_sip_sellingDetails_Click();
				setRowsCount();
				if(Session.Permission["canAddCategory"] == 0) {
					itemManager.button_addCategory.IsEnabled = false;
				}
				if(Session.Permission["canAddCompany"] == 0) {
					itemManager.button_addCompany.IsEnabled = false;
				}
				itemManager.AddCategory = new AddCategory(null);
				itemManager.AddCompany = new AddCompany(null);
			} catch(Exception) {
			}
		}

		private Item getItemForFilter() {
			Item item = null;
			try {
				item = new Item();
				item.Code = itemManager.textBox_shortCode_filter.IsNull() ? null : "%" + itemManager.textBox_shortCode_filter.TrimedText + "%";
				item.Barcode = itemManager.textBox_barCode_filter.IsNull() ? null : "%" + itemManager.textBox_barCode_filter.TrimedText + "%";
				item.Name = itemManager.textBox_itemName_filter.IsNull() ? null : "%" + itemManager.textBox_itemName_filter.TrimedText + "%";
				item.CategoryId = itemManager.comboBox_category_filter.Value > 0 ? itemManager.comboBox_category_filter.Value : -1;
				item.CompanyId = itemManager.comboBox_company_filter.Value > 0 ? itemManager.comboBox_company_filter.Value : -1;
				item.Sip = itemManager.comboBox_sip_filter.Value;
				item.UnitId = itemManager.comboBox_unit_filter.Value > 0 ? itemManager.comboBox_unit_filter.Value : -1;
				item.PackName = itemManager.textBox_packName_filter.IsNull() ? null : "%" + itemManager.textBox_packName_filter.TrimedText + "%";
				item.Details = itemManager.textBox_itemDetails_filter.IsNull() ? null : "%" + itemManager.textBox_itemDetails_filter.TrimedText + "%";
				item.Status = itemManager.comboBox_isActive_filter.Value;
			} catch(Exception) {
			}
			return item;
		}

		internal void filter() {
			try {
				itemManager.DataTable.Rows.Clear();
				Item i = getItemForFilter();
				i.LimitStart = itemManager.Pagination.LimitStart;
				i.LimitEnd = itemManager.Pagination.LimitCount;
				List<Item> list = get(i);
				DataRow row = null;
				foreach(Item item in list) {
					row = itemManager.DataTable.NewRow();
					row[0] = item.Id;
					row[1] = categoryManagerImpl.getCategoryNameById(item.CategoryId);
					row[2] = companyManagerImpl.getCompanyNameById(item.CompanyId);
					row[3] = item.Name;
					row[4] = unitManagerImpl.getUnitNameById(item.UnitId);
					row[5] = item.Code;
					row[6] = item.Barcode;
					row[7] = item.PosName;
					row[8] = item.QuantityPerPack;
					row[9] = item.PackName;
					row[10] = item.ReorderLevel;

					itemManager.DataTable.Rows.Add(row);
				}
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				Item item = getItemForFilter();
				item.RowsCount = 1;
				List<Item> list = get(item);
				itemManager.Pagination.RowsCount = list[0].RowsCount;
			} catch(Exception) {
			}
		}

		internal void checkBox_sip_sellingDetails_Click() {
			try {
				if(itemManager.checkBox_sip_sellingDetails.IsChecked == false) {
					itemManager.comboBox_unit_sellingDetails.SelectedIndex = 0;
					itemManager.comboBox_unit_sellingDetails.IsEnabled = false;
					itemManager.textBox_unitsPerPack_sellingDetails.Text = "1";
					itemManager.textBox_unitsPerPack_sellingDetails.IsEnabled = false;
					itemManager.textBox_packName_sellingDetails.Clear();
					itemManager.textBox_packName_sellingDetails.IsEnabled = false;
					itemManager.radioButton_defaultSellingModeUnit_sellingDetails.IsChecked = true;
					itemManager.radioButton_defaultSellingModePack_sellingDetails.IsEnabled = false;
					itemManager.radioButton_defaultBuyingModeUnit_sellingDetails.IsChecked = true;
					itemManager.radioButton_defaultBuyingModePack_sellingDetails.IsEnabled = false;
				} else {
					itemManager.comboBox_unit_sellingDetails.SelectedIndex = -1;
					itemManager.comboBox_unit_sellingDetails.IsEnabled = true;
					itemManager.textBox_unitsPerPack_sellingDetails.Clear();
					itemManager.textBox_unitsPerPack_sellingDetails.IsEnabled = true;
					itemManager.textBox_packName_sellingDetails.Clear();
					itemManager.textBox_packName_sellingDetails.IsEnabled = true;
					itemManager.radioButton_defaultSellingModeUnit_sellingDetails.IsChecked = true;
					itemManager.radioButton_defaultSellingModePack_sellingDetails.IsEnabled = true;
					itemManager.radioButton_defaultBuyingModeUnit_sellingDetails.IsChecked = true;
					itemManager.radioButton_defaultBuyingModePack_sellingDetails.IsEnabled = true;
				}
			} catch(Exception) {
			}
		}

		private bool validateItemForm() {
			bool isOkay = true;
			try {
				if(itemManager.checkBox_sip_sellingDetails.IsChecked == true && itemManager.textBox_packName_sellingDetails.IsNull()) {
					itemManager.textBox_packName_sellingDetails.ErrorMode(true);
					isOkay = false;
				}
				if(itemManager.checkBox_sip_sellingDetails.IsChecked == true && itemManager.textBox_unitsPerPack_sellingDetails.IntValue < 1) {
					itemManager.textBox_unitsPerPack_sellingDetails.ErrorMode(true);
					isOkay = false;
				}
				if(itemManager.checkBox_sip_sellingDetails.IsChecked == true && Convert.ToInt32(itemManager.comboBox_unit_sellingDetails.SelectedValue) < 1) {
					itemManager.comboBox_unit_sellingDetails.ErrorMode(true);
					isOkay = false;
				}
				if(itemManager.textBox_reorderLevel_sellingDetails.IsNull()) {
					itemManager.textBox_reorderLevel_sellingDetails.ErrorMode(true);
					isOkay = false;
				}
				if(itemManager.textBox_itemCode_basicDetails.IsNull()) {
					itemManager.textBox_itemCode_basicDetails.ErrorMode(true);
					isOkay = false;
				}
				if(isDublicateCode(itemManager.textBox_itemCode_basicDetails.TrimedText, itemManager.IsUpdateMode ? itemManager.SelectedItem.Id : 0)) {
					itemManager.textBox_itemCode_basicDetails.ErrorMode(true);
					isOkay = false;
				}
				if(!itemManager.textBox_barcode_basicDetails.IsNull() && isDublicateBarcode(itemManager.textBox_barcode_basicDetails.TrimedText, itemManager.IsUpdateMode ? itemManager.SelectedItem.Id : 0)) {
					itemManager.textBox_barcode_basicDetails.ErrorMode(true);
					isOkay = false;
				}
				if(itemManager.textBox_itemName_basicDetails.IsNull()) {
					itemManager.textBox_itemName_basicDetails.ErrorMode(true);
					isOkay = false;
				}
				if(isDublicateName(itemManager.textBox_itemName_basicDetails.TrimedText, itemManager.IsUpdateMode ? itemManager.SelectedItem.Id : 0)) {
					itemManager.textBox_itemName_basicDetails.ErrorMode(true);
					isOkay = false;
				}
				if(Convert.ToInt32(itemManager.listBox_company_basicDetails.SelectedValue) <= 0) {
					itemManager.listBox_company_basicDetails.ErrorMode(true);
					isOkay = false;
				}
				if(Convert.ToInt32(itemManager.listBox_category_basicDetails.SelectedValue) <= 0) {
					itemManager.listBox_category_basicDetails.ErrorMode(true);
					isOkay = false;
				}
			} catch(Exception) {
			}
			return isOkay;
		}

		internal bool addItem() {
			bool b = false;
			try {
				if(validateItemForm()) {
					Item item = new Item();
					item.CategoryId = Convert.ToInt32(itemManager.listBox_category_basicDetails.SelectedValue);
					item.CompanyId = Convert.ToInt32(itemManager.listBox_company_basicDetails.SelectedValue);
					item.UnitId = Convert.ToInt32(itemManager.comboBox_unit_sellingDetails.SelectedValue);
					item.Name = itemManager.textBox_itemName_basicDetails.TrimedText;
					item.Code = itemManager.textBox_itemCode_basicDetails.TrimedText;
					item.Barcode = itemManager.textBox_barcode_basicDetails.Text;
					item.PosName = itemManager.textBox_posName_basicDetails.TrimedText;
					item.Sip = itemManager.checkBox_sip_sellingDetails.IsChecked == true ? 1 : 0;
					item.QuantityPerPack = itemManager.textBox_unitsPerPack_sellingDetails.IntValue;
					item.PackName = itemManager.textBox_packName_sellingDetails.TrimedText;
					item.DisplayPercentage = 100;
					item.ReorderLevel = itemManager.textBox_reorderLevel_sellingDetails.IntValue;
					item.Details = itemManager.textBox_details_basicDetails.Text;
					item.ShowCategoryInPrintedInvoice = itemManager.checkBox_showCategoryInPrintedInvoice_basicDetails.IsChecked == true ? 1 : 0;
					item.ShowCompanyInPrintedInvoice = itemManager.checkBox_showCompanyInPrintedInvoice_basicDetails.IsChecked == true ? 1 : 0;
					item.DefaultBuyingMode = itemManager.radioButton_defaultBuyingModeUnit_sellingDetails.IsChecked == true ? "u" : "p";
					item.DefaultSellingMode = itemManager.radioButton_defaultSellingModeUnit_sellingDetails.IsChecked == true ? "u" : "p";
					item.PackBuyingPrice = 0;
					item.UnitBuyingPrice = 0;
					item.PackSellingPrice = 0;
					item.UnitSellingPrice = 0;
					item.Status = itemManager.checkBox_isActive_basicDetails.IsChecked == true ? 1 : 0;
					CommonMethods.setCDMDForAdd(item);
					stockManagerImpl.addStockItemForItemId(add(item));
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void resetAddForm() {
			try {
				itemManager.IsUpdateMode = false;
				itemManager.textBox_itemCode_basicDetails.Text = getNextCode(itemManager.listBox_category_basicDetails.SelectedID, itemManager.listBox_company_basicDetails.SelectedID).ToString();				
			} catch(Exception) {
			}
		}

		public void switchToAddMode() {
			try {
				resetAddForm();
				itemManager.button_save_addItem.Content = "Save";
				itemManager.button_reset_addItem.Content = "Reset";
			} catch(Exception) {
			}
		}

		internal void switchToUpdateMode() {
			try {
				itemManager.IsUpdateMode = true;
				Item item = getItemById(itemManager.dataGrid_items_items.SelectedItemID);
				itemManager.SelectedItem = item;
				itemManager.textBox_category_basicDetails.Clear();
				itemManager.textBox_company_basicDetails.Clear();
				itemManager.listBox_category_basicDetails.SelectedValue = item.CategoryId;
				itemManager.listBox_company_basicDetails.SelectedValue = item.CompanyId;
				itemManager.textBox_itemName_basicDetails.Text = item.Name;
				itemManager.textBox_posName_basicDetails.Text = item.PosName;
				itemManager.textBox_barcode_basicDetails.Text = item.Barcode;
				itemManager.textBox_itemCode_basicDetails.Text = item.Code;
				itemManager.textBox_reorderLevel_sellingDetails.Text = item.ReorderLevel + "";
				itemManager.checkBox_sip_sellingDetails.IsChecked = item.Sip == 1 ? true : false;
				checkBox_sip_sellingDetails_Click();
				itemManager.comboBox_unit_sellingDetails.SelectedValue = item.UnitId;
				itemManager.textBox_unitsPerPack_sellingDetails.Text = item.QuantityPerPack + "";
				itemManager.textBox_packName_sellingDetails.Text = item.PackName;
				itemManager.radioButton_defaultSellingModePack_sellingDetails.IsChecked = item.DefaultSellingMode == "p" ? true : false;
				itemManager.radioButton_defaultSellingModeUnit_sellingDetails.IsChecked = item.DefaultSellingMode == "u" ? true : false;
				itemManager.radioButton_defaultBuyingModePack_sellingDetails.IsChecked = item.DefaultBuyingMode == "p" ? true : false;
				itemManager.radioButton_defaultBuyingModeUnit_sellingDetails.IsChecked = item.DefaultBuyingMode == "u" ? true : false;
				itemManager.checkBox_isActive_basicDetails.IsChecked = item.Status == 1 ? true : false;
				itemManager.checkBox_showCategoryInPrintedInvoice_basicDetails.IsChecked = item.ShowCategoryInPrintedInvoice == 1 ? true : false;
				itemManager.checkBox_showCompanyInPrintedInvoice_basicDetails.IsChecked = item.ShowCompanyInPrintedInvoice == 1 ? true : false;
				itemManager.textBox_details_basicDetails.Text = item.Details;
				itemManager.button_save_addItem.Content = "Update";
				itemManager.button_reset_addItem.Content = "Cancel";
			} catch(Exception) {
			}
		}

		internal bool updateItem() {
			bool b = false;
			try {
				if(validateItemForm()) {
					Item item = itemManager.SelectedItem;
					item.CategoryId = Convert.ToInt32(itemManager.listBox_category_basicDetails.SelectedValue);
					item.CompanyId = Convert.ToInt32(itemManager.listBox_company_basicDetails.SelectedValue);
					item.UnitId = Convert.ToInt32(itemManager.comboBox_unit_sellingDetails.SelectedValue);
					item.Name = itemManager.textBox_itemName_basicDetails.TrimedText;
					item.Code = itemManager.textBox_itemCode_basicDetails.TrimedText;
					item.Barcode = itemManager.textBox_barcode_basicDetails.Text;
					item.PosName = itemManager.textBox_posName_basicDetails.TrimedText;
					item.Sip = itemManager.checkBox_sip_sellingDetails.IsChecked == true ? 1 : 0;
					item.QuantityPerPack = itemManager.textBox_unitsPerPack_sellingDetails.IntValue;
					item.PackName = itemManager.textBox_packName_sellingDetails.TrimedText;
					item.DisplayPercentage = 100;
					item.ReorderLevel = itemManager.textBox_reorderLevel_sellingDetails.IntValue;
					item.Details = itemManager.textBox_details_basicDetails.Text;
					item.ShowCategoryInPrintedInvoice = itemManager.checkBox_showCategoryInPrintedInvoice_basicDetails.IsChecked == true ? 1 : 0;
					item.ShowCompanyInPrintedInvoice = itemManager.checkBox_showCompanyInPrintedInvoice_basicDetails.IsChecked == true ? 1 : 0;
					item.DefaultBuyingMode = itemManager.radioButton_defaultBuyingModeUnit_sellingDetails.IsChecked == true ? "u" : "p";
					item.DefaultSellingMode = itemManager.radioButton_defaultSellingModeUnit_sellingDetails.IsChecked == true ? "u" : "p";
					item.PackBuyingPrice = 0;
					item.UnitBuyingPrice = 0;
					item.PackSellingPrice = 0;
					item.UnitSellingPrice = 0;
					item.Status = itemManager.checkBox_isActive_basicDetails.IsChecked == true ? 1 : 0;
					CommonMethods.setCDMDForUpdate(item);
					upd(item);
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}
	}
}
