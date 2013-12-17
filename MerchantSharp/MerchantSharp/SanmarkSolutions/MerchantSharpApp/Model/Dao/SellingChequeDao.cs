using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao {
	class SellingChequeDao : BaseDao, IDao {

		private static SellingChequeDao dao = null;

		private SellingChequeDao() {
			base.TableName = "selling_cheque";
		}

		public static SellingChequeDao getInstance() {
			try {
				if(dao == null) {
					dao = new SellingChequeDao();
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
