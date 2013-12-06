using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class StockManagerImpl {

		private IDao stockLocationDao = null;
		private IDao stockItemDao = null;

		public StockManagerImpl() {
			stockLocationDao = StockLocationDao.getInstance();
			stockItemDao = StockItemDao.getInstance();
		}

		public int addStockLocation(Entity entity) {
			return stockLocationDao.add(entity);
		}

		public bool delStockLocation(Entity entity) {
			return stockLocationDao.del(entity);
		}

		public List<StockLocation> getStockLocation(Entity entity) {
			return stockLocationDao.get(entity).Cast<StockLocation>().ToList();
		}

		public int updStockLocation(Entity entity) {
			return stockLocationDao.upd(entity);
		}

		/////////////

		public int addStockItem(Entity entity) {
			return stockItemDao.add(entity);
		}

		public bool delStockItem(Entity entity) {
			return stockItemDao.del(entity);
		}

		public List<StockItem> getStockItem(Entity entity) {
			return stockItemDao.get(entity).Cast<StockItem>().ToList();
		}

		public int updStockItem(Entity entity) {
			return stockItemDao.upd(entity);
		}


		////////////////////////////////////////////////////////////////////////////////////////////////////

		public List<StockLocation> getStockLocations() {
			try {
				return getStockLocation(new StockLocation());
			} catch(Exception) {
				return null;
			}
		}

		public StockItem getStockItemByStockLocationIdAndItemId(int stockLocationId, int itemId) {
			StockItem stockItem = null;
			try {
				StockItem i = new StockItem();
				i.StockLocationId = stockLocationId;
				i.ItemId = itemId;
				stockItem = getStockItem(i)[0];
			} catch(Exception) {
			}
			return stockItem;
		}

	}
}
