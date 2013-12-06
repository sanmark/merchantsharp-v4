using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class UnitManagerImpl {

		private IDao dao = null;

		public UnitManagerImpl() {
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

	}
}
