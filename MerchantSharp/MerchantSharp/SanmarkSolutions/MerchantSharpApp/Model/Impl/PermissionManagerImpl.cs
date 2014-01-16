using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class PermissionManagerImpl {

		private PermissionDao permissionDao = null;
		private PermissionUserDao permissionUserDao = null;

		public PermissionManagerImpl() {
			permissionDao = PermissionDao.getInstance();
			permissionUserDao = PermissionUserDao.getInstance();
		}

		public int addPermission(Entity entity) {
			return permissionDao.add(entity);
		}

		public bool delPermission(Entity entity) {
			return permissionDao.del(entity);
		}

		public List<Permission> getPermission(Entity entity) {
			return permissionDao.get(entity).Cast<Permission>().ToList();
		}

		public int updPermission(Entity entity) {
			return permissionDao.upd(entity);
		}

		public int addPermissionUser(Entity entity) {
			return permissionUserDao.add(entity);
		}

		public bool delPermissionUser(Entity entity) {
			return permissionUserDao.del(entity);
		}

		public List<PermissionUser> getPermissionUser(Entity entity) {
			return permissionUserDao.get(entity).Cast<PermissionUser>().ToList();
		}

		public int updPermissionUser(Entity entity) {
			return permissionUserDao.upd(entity);
		}

		public int getPermissionValue(int permissionId, int userId) {
			try {
				PermissionUser p = new PermissionUser();
				p.PermissionId = permissionId;
				p.UserId = userId;
				return getPermissionUser(p)[0].Value;
			} catch(Exception) {
				return 0;
			}
		}

		public Dictionary<String, int> getPermissionForSession(int userId) {
			Dictionary<String, int> dic = new Dictionary<String, int>();
			try {
				List<Permission> list = getPermission(new Permission());
				foreach(Permission permission in list){
					dic.Add(permission.Name, getPermissionValue(permission.Id, userId));
				}
			} catch(Exception) {
			}
			return dic;
		}		

	}
}
