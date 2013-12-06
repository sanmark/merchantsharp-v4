using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class PreferenceManagerImpl {

		private IDao preferenceDao;

		public PreferenceManagerImpl() {
			preferenceDao = PreferenceDao.getInstance();
		}

		public int add(Entity entity) {
			return preferenceDao.add(entity);
		}

		public bool del(Entity entity) {
			throw new NotImplementedException();
		}

		public List<Preference> get(Entity entity) {
			return preferenceDao.get(entity).Cast<Preference>().ToList();
		}

		public int upd(Entity entity) {
			return preferenceDao.upd(entity);
		}

		public String getPreferenceValueByKey(String key) {
			String value = null;
			try {
				Preference p = new Preference();
				p.Key = key;
				value = get(p)[0].Value;
			} catch(Exception) {
			}
			return value;
		}

	}
}
