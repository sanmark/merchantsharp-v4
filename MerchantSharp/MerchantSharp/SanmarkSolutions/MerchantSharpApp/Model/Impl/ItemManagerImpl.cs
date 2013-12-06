using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class ItemManagerImpl {

		private IDao itemDao;

		public ItemManagerImpl() {
			itemDao = ItemDao.getInstance();
		}

		public int add(Entity entity) {
			return itemDao.add(entity);
		}

		public bool del(Entity entity) {
			return itemDao.del(entity);
		}

		public List<Item> get(Entity entity) {
			return itemDao.get(entity).Cast<Item>().ToList();
		}

		public int upd(Entity entity) {
			return itemDao.upd(entity);
		}

		/// <summary>
		/// Will get item by id
		/// </summary>
		/// <param name="itemId">ID of Item</param>
		/// <returns>Item</returns>
		internal Item getItemById(int itemId) {
			Item item = null;
			try {
				if(itemId > 0) {
					item = new Item();
					item.Id = itemId;
					item = get(item)[0];
				} else {
					item = null;
				}
			} catch(Exception) {
				item = null;
			}
			return item;
		}

		/// <summary>
		/// Will get Item by code
		/// </summary>
		/// <param name="code">The code of Item</param>
		/// <returns>Item Id</returns>
		internal int getItemIdByCode(string code) {
			int itemId = 0;
			try {
				Item item = new Item();
				item.Code = code;
				itemId = get(item)[0].Id;
			} catch(Exception) {
			}
			return itemId;
		}

		public List<int> getCompanyIdsForCategory(int catId) {
			List<int> ids = new List<int>();
			try {
				Item item = new Item();
				item.CategoryId = catId;
				List<Item> list = get(item);
				foreach(Item i in list) {
					if(ids.IndexOf(i.CompanyId) < 0) {
						ids.Add(i.CompanyId);
					}					
				}
			} catch(Exception) {
			}
			return ids;
		}

		public List<Item> getItemsForCategoryAndCompany(int catId, int comId) {
			List<Item> items = null;
			try {
				Item item = new Item();
				item.CategoryId = catId;
				item.CompanyId = comId;
				items = get(item);
			} catch(Exception) {
			}
			return items;
		}

	}
}
