using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class CommonManagerImpl {

		public static List<int> getRecentSoldItemIds(int count){
			List<int> itemIds = new List<int>();
			try {
				String query = "SELECT item_id, COUNT(*) FROM selling_item GROUP BY item_id ORDER BY COUNT(*) DESC LIMIT " + count + "";
				DataSet dataSet = DBConnector.getInstance().getDataSet(query);
				foreach(DataRow row in dataSet.Tables[0].Rows) {
					
				}
			} catch(Exception) {
			}
			return itemIds;
		}

		public static DataSet getItemsForSearch(String name, String company, String category) {
			DataSet dataSet = null;
			try {
				String query = "SELECT item.id, item.`name`, category.id, category.`name`, company.id, company.`name`" +
								"FROM item " +
								"INNER JOIN category ON item.category_id = category.id " +
								"INNER JOIN company ON item.company_id = company.id " +
								"WHERE " +
									"item.`name` LIKE '%" + name + "%' " +
									"AND category.`name` LIKE '%" + category + "%' " +
									"AND company.`name` LIKE '%" + company + "%' "+
									"LIMIT 100";
				dataSet = DBConnector.getInstance().getDataSet(query);				
			} catch(Exception) {
			}
			return dataSet;
		}

	}
}
