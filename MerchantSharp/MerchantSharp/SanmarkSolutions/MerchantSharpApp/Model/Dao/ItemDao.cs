using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao {
	class ItemDao : BaseDao, IDao {

		private static ItemDao dao = null;

		private ItemDao() {
			base.TableName = "item";
		}

		public static ItemDao getInstance() {
			try {
				if(dao == null) {
					dao = new ItemDao();
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
