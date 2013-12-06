using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao {
	interface IDao {
		int add(Entity entity);

		bool del(Entity entity);

		List<Entity> get(Entity entity);

		int upd(Entity entity);

	}
}
