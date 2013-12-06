using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao {
	class SellingPriceDao : BaseDao, IDao {

		private static SellingPriceDao dao = null;

		private SellingPriceDao() {
			base.TableName = "selling_price";
		}

		public static SellingPriceDao getInstance() {
			try {
				if(dao == null) {
					dao = new SellingPriceDao();
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
			return base.updEntity(entity);
		}
	}
}
