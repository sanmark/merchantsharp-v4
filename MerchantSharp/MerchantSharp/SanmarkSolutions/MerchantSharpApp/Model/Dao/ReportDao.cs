using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao {
	class ReportDao {

		public static DataSet getDailySale(String dateFrom, String dateTo, bool isCount, int start, int count) {
			DataSet dataSet = null;
			try {
				String query = "";
				if(isCount) {
					query = "SELECT COUNT(*) " +
					"FROM " +
					"(SELECT " +
						"DATE(selling_invoice.date) AS date " +
					"FROM " +
						"selling_item " +
					"INNER JOIN (selling_invoice) " +
					"ON ( " +
						"selling_item.selling_invoice_id = selling_invoice.id " +
					") " +
					"WHERE " +
						"selling_invoice.`status` = '1' " +
						((dateFrom != null && dateTo != null) ? "AND (DATE(selling_invoice.date) BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
						(dateFrom != null ? "AND DATE(selling_invoice.date) LIKE '" + dateFrom + "' " :
						(dateTo != null ? "AND DATE(selling_invoice.date) LIKE '" + dateTo + "' " : "")
						)) +
					"GROUP BY " +
						"DATE(selling_invoice.date)) `daily_sale`";
				} else {
					query = "SELECT * FROM( SELECT " +
						"DATE(`selling_invoice`.`date`) `date`, " +
						"SUM(`selling_item`.`default_price` * `selling_item`.`quantity`) `gross_sale`, " +
						"(SUM(selling_item.discount * selling_item.quantity) + selling_invoice.discount) AS discount, " +
						"SUM(selling_item.default_price * selling_item.market_return_quantity) AS cr, " +
						"SUM(selling_item.default_price * selling_item.good_return_quantity) AS gr, " +
						"SUM(selling_item.default_price * selling_item.waste_return_quantity) AS wr, " +
						"(SUM(`selling_item`.`default_price` * `selling_item`.`quantity`) - " +
							"(SUM(selling_item.discount * selling_item.quantity) + selling_invoice.discount) - " +
							"SUM(selling_item.default_price * selling_item.market_return_quantity) - " +
							"SUM(selling_item.default_price * selling_item.good_return_quantity) - " +
							"SUM(selling_item.default_price * selling_item.waste_return_quantity) " +
						") AS net_total " +
					"FROM " +
						"selling_item " +
					"INNER JOIN `selling_invoice` ON `selling_invoice`.`id` = `selling_item`.`selling_invoice_id` " +
					"WHERE " +
						"selling_invoice.`status` = '1'	" +
						((dateFrom != null && dateTo != null) ? "AND (DATE(selling_invoice.date) BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
						(dateFrom != null ? "AND DATE(selling_invoice.date) LIKE '" + dateFrom + "' " :
						(dateTo != null ? "AND DATE(selling_invoice.date) LIKE '" + dateTo + "' " : "")
						)) +
					"GROUP BY DATE(selling_invoice.date)" +
					"ORDER BY DATE(selling_invoice.date) DESC " +
					") AS daily_sale ORDER BY daily_sale.date ASC " +
					"LIMIT " + start + "," + count;
				}
				dataSet = DBConnector.getInstance().getDataSet(query);
			} catch(Exception) {
			}
			return dataSet;
		}

		public static DataSet getDailyItemSale(int categoryId, int companyId, int ItemId, String dateFrom, String dateTo, bool isCount, int start, int count) {
			DataSet dataSet = null;
			try {
				String query = "";
				if(isCount) {
					query = "SELECT COUNT(*) " +
					"FROM " +
					"(SELECT " +
						"DATE(selling_invoice.date) AS date " +
					"FROM " +
						"selling_item " +
					"INNER JOIN selling_invoice " +
					"ON ( " +
						"selling_item.selling_invoice_id = selling_invoice.id " +
					") " +
					"LEFT JOIN (item, category, company) " +
					"ON (" +
						"item.id = selling_item.item_id " +
						"AND item.category_id = category.id " +
						"AND item.company_id = company.id " +
					")" +
					"WHERE " +
						"selling_invoice.`status` = '1' " +
						(categoryId > 0 ? "AND category.id = '" + categoryId + "' " : "") +
						(companyId > 0 ? "AND company.id = '" + companyId + "' " : "") +
						(ItemId > 0 ? "AND item.id = '" + ItemId + "' " : "") +
						((dateFrom != null && dateTo != null) ? "AND (DATE(selling_invoice.date) BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
						(dateFrom != null ? "AND DATE(selling_invoice.date) LIKE '" + dateFrom + "' " :
						(dateTo != null ? "AND DATE(selling_invoice.date) LIKE '" + dateTo + "' " : "")
						)) +
					"GROUP BY DATE(selling_invoice.date), item.id) AS daily_item_sale";
				} else {
					query = "SELECT * FROM( SELECT " +
						"DATE(selling_invoice.date) as date, " +
						"CONCAT(item.`name`, ' (', company.`name`, ')') as item_name, " +
						"SUM(selling_item.quantity) as quantity, " +
						"SUM(`selling_item`.`default_price` * `selling_item`.`quantity`) `gross_sale`, " +
						"(SUM(selling_item.discount * selling_item.quantity) + selling_invoice.discount) AS discount, " +
						"SUM(selling_item.default_price * selling_item.market_return_quantity) AS cr, " +
						"SUM(selling_item.default_price * selling_item.good_return_quantity) AS gr, " +
						"SUM(selling_item.default_price * selling_item.waste_return_quantity) AS wr, " +
						"(SUM(`selling_item`.`default_price` * `selling_item`.`quantity`) - " +
							"(SUM(selling_item.discount * selling_item.quantity) + selling_invoice.discount) - " +
							"SUM(selling_item.default_price * selling_item.market_return_quantity) - " +
							"SUM(selling_item.default_price * selling_item.good_return_quantity) - " +
							"SUM(selling_item.default_price * selling_item.waste_return_quantity) " +
						") AS net_total, " +
						"SUM(selling_item.selling_price_actual - selling_item.buying_price_actual) as profit " +
					"FROM " +
						"selling_item " +
					"INNER JOIN `selling_invoice` ON `selling_invoice`.`id` = `selling_item`.`selling_invoice_id` " +
					"LEFT JOIN (item, category, company)" +
					"ON (" +
						"item.id = selling_item.item_id " +
						"AND item.category_id = category.id " +
						"AND item.company_id = company.id " +
					") " +
					"WHERE " +
						"selling_invoice.`status` = '1' " +
						(categoryId > 0 ? "AND category.id = '" + categoryId + "' " : "") +
						(companyId > 0 ? "AND company.id = '" + companyId + "' " : "") +
						(ItemId > 0 ? "AND item.id = '" + ItemId + "' " : "") +
						((dateFrom != null && dateTo != null) ? "AND (DATE(selling_invoice.date) BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
						(dateFrom != null ? "AND DATE(selling_invoice.date) LIKE '" + dateFrom + "' " :
						(dateTo != null ? "AND DATE(selling_invoice.date) LIKE '" + dateTo + "' " : "")
						)) +
					"GROUP BY DATE(selling_invoice.date), item.id " +
					"ORDER BY DATE(selling_invoice.date) DESC " +
					") AS daily_item_sale ORDER BY daily_item_sale.date ASC " +
					"LIMIT " + start + "," + count;
				}
				dataSet = DBConnector.getInstance().getDataSet(query);
			} catch(Exception) {
			}
			return dataSet;
		}

	}
}
