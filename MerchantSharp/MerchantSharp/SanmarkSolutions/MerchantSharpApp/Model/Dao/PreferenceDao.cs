using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao {
	class PreferenceDao : BaseDao, IDao {

		private static PreferenceDao dao = null;

		private PreferenceDao() {
			base.TableName = "preference";
		}

		public static PreferenceDao getInstance() {
			try {
				if(dao == null) {
					dao = new PreferenceDao();
					dao.initializeTable();
				}				
			} catch( Exception ) {
			}
			return dao;
		}

		public int add(Entities.Entity entity) {
			return base.addEntity(entity);
		}

		public bool del(Entities.Entity entity) {
			return base.delEntity(entity);
		}

		public List<Entities.Entity> get(Entities.Entity entity) {
			return base.getEntity(entity);
		}

		public int upd(Entities.Entity entity) {
			return upd(entity);
		}
	}
}
