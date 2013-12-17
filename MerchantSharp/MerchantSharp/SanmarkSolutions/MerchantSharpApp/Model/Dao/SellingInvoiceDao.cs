using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao {
	class SellingInvoiceDao : BaseDao, IDao {

		private static SellingInvoiceDao dao = null;

		private SellingInvoiceDao() {
			base.TableName = "selling_invoice";
		}

		public static SellingInvoiceDao getInstance() {
			try {
				if(dao == null) {
					dao = new SellingInvoiceDao();
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
