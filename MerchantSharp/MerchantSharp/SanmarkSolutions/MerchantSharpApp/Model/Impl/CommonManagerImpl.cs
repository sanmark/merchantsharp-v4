using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class CommonManagerImpl {

		public static List<int> getRecentSoldItemIds(int count) {
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
									"AND company.`name` LIKE '%" + company + "%' " +
									"LIMIT 100";
				dataSet = DBConnector.getInstance().getDataSet(query);
			} catch(Exception) {
			}
			return dataSet;
		}

		public static DataSet getStockForFilter(int locationId, String itemName, String itemCode, String barcode,
			int categoryId, int companyId, bool belowROL, bool isCount, int start, int count) {
			DataSet dataSet = null;
			try {
				String query = "SELECT " +
									(isCount ? "COUNT(*)" : "stock_item.id, " +
									"stock_location.`name`, " +
									"item.`name` AS item_name, " +
									"category.`name` AS category_name, " +
									"company.`name` AS company_name, " +
									(locationId > 0 ? "stock_item.quantity, " : "(SUM(stock_item.quantity)) as quantity, ") +
									//"stock_item.quantity, " +
									"item.reorder_level, " +
									"(item.unit_buying_price * " + (locationId > 0 ? "stock_item.quantity" : "SUM(stock_item.quantity)") + ") AS value, " +
									"stock_item.item_id as item_id ") +
								"FROM stock_item " +
								"LEFT JOIN (stock_location, item, category, company) " +
									"ON (" +
										"stock_location.id=stock_item.stock_location_id " +
										"AND item.id=stock_item.item_id " +
										"AND item.category_id=category.id " +
										"AND item.company_id=company.id " +
									")" +
								"WHERE " +
									"stock_location.id LIKE '%" + (locationId > 0 ? locationId + "" : "%") + "' " +
									"AND item.`name` LIKE '%" + (itemName != null ? itemName : "") + "%' " +
									"AND item.`code` LIKE '%" + (itemCode != null ? itemCode : "") + "%' " +
									"AND item.barcode LIKE '%" + (barcode != null ? barcode : "") + "%' " +
									"AND item.category_id LIKE '%" + (categoryId > 0 ? categoryId + "" : "") + "%' " +
									"AND item.company_id LIKE '%" + (companyId > 0 ? companyId + "" : "") + "%' " +
									(belowROL ? "AND item.reorder_level >= stock_item.quantity " : "") +
									(locationId <= 0 ? " GROUP BY stock_item.item_id " : "") +
								"LIMIT " + start + "," + count;
				dataSet = DBConnector.getInstance().getDataSet(query);
			} catch(Exception) {
			}
			return dataSet;
		}

	}
}
