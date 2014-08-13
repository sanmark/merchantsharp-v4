using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao {
	class DailyInitialCashDao : BaseDao, IDao {

		private static DailyInitialCashDao dao = null;

		private DailyInitialCashDao() {
			base.TableName = "daily_initial_cash";
		}

		public static DailyInitialCashDao getInstance() {
			try {
				if(dao == null) {
					dao = new DailyInitialCashDao();
					dao.initializeTable();
				}
			} catch(Exception) {
			}
			return dao;
		}


		public int add(Entities.Entity entity) {
			return base.addEntity(entity);
		}

		public bool del(Entities.Entity entity) {
			throw new NotImplementedException();
		}

		public List<Entities.Entity> get(Entities.Entity entity) {
			return base.getEntity(entity);
		}

		public int upd(Entities.Entity entity) {
			return base.updEntity(entity);
		}

	}
}
