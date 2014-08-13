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
	class UnitManagerImpl {

		private IDao dao = null;
		private AddUnit addUnit;
		private   UnitManager unitManager;

		public UnitManagerImpl() {
			dao = UnitDao.getInstance();
		}

		public UnitManagerImpl(AddUnit addUnit) {
			this.addUnit = addUnit;
			dao = UnitDao.getInstance();
		}

		public UnitManagerImpl(UnitManager unitManager) {
			this.unitManager = unitManager;
			dao = UnitDao.getInstance();
		}

		public int add(Entity entity) {
			try {
				if(Session.Permission["canAddUnit"] == 1) {
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

		public List<Unit> get(Entity entity) {
			return dao.get(entity).Cast<Unit>().ToList();
		}

		public int upd(Entity entity) {
			try {
				if(Session.Permission["canUpdateUnit"] == 1) {
					return dao.upd(entity);
				} else {
					ShowMessage.error(Common.Messages.Error.Error010);
				}
			} catch(Exception) {
			}
			return 0;
		}

		public Unit getUnitById(int unitId) {
			Unit unit = null;
			try {
				unit = new Unit();
				unit.Id = unitId;
				return get(unit)[0];
			} catch(Exception) {
				return null;
			}
		}

		public String getUnitNameById(int unitId) {
			try {
				return getUnitById(unitId).Name;
			} catch(Exception) {
				return "";
			}
		}

		public bool isDuplicate(String name, int id) {
			bool b = false;
			try {
				Unit unit = new Unit();
				unit.Name = name;
				List<Unit> list = get(unit);
				if(list.Count > 0 && id > 0) {
					foreach(Unit u in list) {
						if(u.Id != id) {
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

		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////


		internal bool addUnitPopup() {
			bool b = false;
			try {
				if(addUnit.textBox_name.IsNull()) {
					addUnit.textBox_name.ErrorMode(true);
				} else if(isDuplicate(addUnit.textBox_name.TrimedText, 0)) {
					addUnit.textBox_name.ErrorMode(true);
					ShowMessage.error(Common.Messages.Error.Error007);
				} else {
					Unit unit = new Unit();
					unit.Name = addUnit.textBox_name.TrimedText;
					CommonMethods.setCDMDForAdd(unit);
					addUnit.AddedId = add(unit);
					ShowMessage.success(Common.Messages.Success.Success002);
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////


		internal void UserControl_Loaded() {
			try {
				unitManager.DataTable = new DataTable();
				unitManager.DataTable.Columns.Add("ID", typeof(int));
				unitManager.DataTable.Columns.Add("Name", typeof(String));
				unitManager.dataGrid.DataContext = unitManager.DataTable.DefaultView;
				unitManager.dataGrid.Columns[1].Width = 200;

				unitManager.Pagination = new Pagination();
				unitManager.Pagination.Filter = unitManager;
				unitManager.grid_pagination.Children.Add(unitManager.Pagination);

				setRowsCount();
			} catch(Exception) {
			}
		}

		private Unit getUnitForFilter() {
			Unit unit = new Unit();
			try {
				unit.Name = unitManager.textBox_name_filter.TrimedText + "%";
			} catch(Exception) {
			}
			return unit;
		}

		internal void filter() {
			try {
				Unit u = getUnitForFilter();
				u.LimitStart = unitManager.Pagination.LimitStart;
				u.LimitEnd = unitManager.Pagination.LimitCount;
				List<Unit> list = get(u);
				unitManager.DataTable.Rows.Clear();
				foreach(Unit unit in list) {
					unitManager.DataTable.Rows.Add(unit.Id, unit.Name);
				}
			} catch(Exception) {
			}
		}

		internal void setRowsCount() {
			try {
				Unit unit = getUnitForFilter();
				unit.RowsCount = 1;
				unit.LimitStart = unitManager.Pagination.LimitStart;
				unit.LimitEnd = unitManager.Pagination.LimitCount;
				unitManager.Pagination.RowsCount = get(unit)[0].RowsCount;
			} catch(Exception) {
			}
		}

		internal bool addNewUnit() {
			bool b = false;
			try {
				bool isOkay = true;				
				if(unitManager.textBox_name_addUser.IsNull()) {
					unitManager.textBox_name_addUser.ErrorMode(true);
					isOkay = false;
				}
				if(isDuplicate(unitManager.textBox_name_addUser.TrimedText, 0)) {
					unitManager.textBox_name_addUser.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					Unit unit = new Unit();
					unit.Name = unitManager.textBox_name_addUser.TrimedText;
					CommonMethods.setCDMDForAdd(unit);
					if(add(unit) > 0) {
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void resetAddForm() {
			try {
				unitManager.textBox_name_addUser.Clear();
			} catch(Exception) {
			}
		}

		internal bool updateUnit() {
			bool b = false;
			try {
				bool isOkay = true;				
				if(unitManager.textBox_name_addUser.IsNull()) {
					unitManager.textBox_name_addUser.ErrorMode(true);
					isOkay = false;
				}
				if(isDuplicate(unitManager.textBox_name_addUser.TrimedText, unitManager.SelectedUnit.Id)) {
					unitManager.textBox_name_addUser.ErrorMode(true);
					isOkay = false;
				}
				if(isOkay) {
					Unit unit = unitManager.SelectedUnit;
					unit.Name = unitManager.textBox_name_addUser.TrimedText;
					CommonMethods.setCDMDForUpdate(unit);
					upd(unit);
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		internal void switchToAddMode() {
			try {
				unitManager.SelectedUnit = null;
				unitManager.IsUpdateMode = false;
				unitManager.button_save_addUnit.Content = "Save";
				unitManager.button_reset_addUnit.Content = "Reset";
			} catch(Exception) {
			}
		}

		internal void switchToUpdateMode() {
			try {
				unitManager.IsUpdateMode = true;
				Unit unit = getUnitById(unitManager.dataGrid.SelectedItemID);
				unitManager.SelectedUnit = unit;
				unitManager.textBox_name_addUser.Text = unit.Name;

				unitManager.button_save_addUnit.Content = "Update";
				unitManager.button_reset_addUnit.Content = "Cancel";
			} catch(Exception) {
			}
		}
	}
}
