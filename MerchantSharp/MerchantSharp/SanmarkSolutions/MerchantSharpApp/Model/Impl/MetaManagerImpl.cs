using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class MetaManagerImpl {

		private IDao dao = null;

		public MetaManagerImpl() {
			dao = MetaDao.getInstance();
		}

		public List<Meta> get(Entity entity) {
			return dao.get(entity).Cast<Meta>().ToList();
		}

		public int upd(Entity entity) {
			return dao.upd(entity);
		}

		public void subtractTrial() {
			try {
				Meta meta = new Meta();
				meta.Key = "trialLeft";
				meta = get(meta)[0];
				meta.Value = Session.Meta["trialLeft"] - 1;
				upd(meta);
			} catch(Exception) {
			}
		}
	}
}
