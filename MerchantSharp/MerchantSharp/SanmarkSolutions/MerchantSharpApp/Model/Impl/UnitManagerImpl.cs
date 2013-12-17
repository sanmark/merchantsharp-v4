using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class UnitManagerImpl {

		private IDao dao = null;
		private AddUnit addUnit;

		public UnitManagerImpl() {
			dao = UnitDao.getInstance();
		}

		public UnitManagerImpl(AddUnit addUnit) {
			this.addUnit = addUnit;
			dao = UnitDao.getInstance();
		}

		public int add(Entity entity) {
			return dao.add(entity);
		}

		public bool del(Entity entity) {
			return dao.del(entity);
		}

		public List<Unit> get(Entity entity) {
			return dao.get(entity).Cast<Unit>().ToList();
		}

		public int upd(Entity entity) {
			return dao.upd(entity);
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

		public bool isDublicate(String name, int id) {
			bool b = false;
			try {
				Unit unit = new Unit();
				unit.Name = name;
				if(get(unit).Count > 0) {
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
				} else if(isDublicate(addUnit.textBox_name.TrimedText, 0)) {
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
	}
}
