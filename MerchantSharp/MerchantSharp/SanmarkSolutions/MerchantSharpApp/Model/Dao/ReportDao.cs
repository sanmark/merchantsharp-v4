using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao {
	class ReportDao {

		public static DataSet getDailySale( String dateFrom, String dateTo, bool isCount, int start, int count ) {
			DataSet dataSet = null;
			try {
				String query = "";
				if ( isCount ) {
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
						( ( dateFrom != null && dateTo != null ) ? "AND (DATE(selling_invoice.date) BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
						( dateFrom != null ? "AND DATE(selling_invoice.date) LIKE '" + dateFrom + "' " :
						( dateTo != null ? "AND DATE(selling_invoice.date) LIKE '" + dateTo + "' " : "" )
						) ) +
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
						") AS net_total, " +
						"payment_table.cash_amount AS cash, " +
						"payment_table.cheque_amount AS cheque, " +
						"SUM(selling_invoice.customer_account_balance_change) as account, " +
						"payment_table.other_amount AS other, " +
						"IFNULL((credit_bills.net_total - IFNULL(paid_credit_bills.amount, 0)), 0) AS credit, " +
						"(completely_paid_bills_net_total.net_total - completely_paid_bills_paid_amount.amount) AS baddebts " +
					"FROM " +
						"selling_item " +
					"INNER JOIN `selling_invoice` ON `selling_invoice`.`id` = `selling_item`.`selling_invoice_id` " +

					"LEFT JOIN ( " +
						"SELECT  " +
						"DATE(selling_invoice.date) as date, " +
						"IFNULL(SUM(selling_cash.amount), 0) as cash_amount, " +
						"IFNULL(SUM(selling_cheque.amount), 0) as cheque_amount, " +
						"IFNULL(SUM(selling_other.amount), 0) as other_amount " +
						"FROM selling_invoice " +
						"LEFT JOIN selling_cash " +
							"ON (selling_invoice.id = selling_cash.selling_invoice_id AND DATE(selling_cash.date) = (selling_invoice.date)) " +
						"LEFT JOIN selling_cheque " +
							"ON (selling_invoice.id = selling_cheque.selling_invoice_id AND DATE(selling_cheque.issued_date) = (selling_invoice.date)) " +
						"LEFT JOIN selling_other  " +
							"ON (selling_invoice.id = selling_other.selling_invoice_id AND DATE(selling_other.date) = (selling_invoice.date)) " +
						"WHERE selling_invoice.`status` = '1' " +
						"GROUP BY DATE(selling_invoice.date)  " +
					") AS payment_table ON (payment_table.date = DATE(selling_invoice.date)) " +

					"LEFT JOIN ( " +
						"SELECT  " +
						"DATE(selling_invoice.date) as date, " +
						"(IFNULL(SUM(selling_cash.amount), 0) + IFNULL(SUM(selling_cheque.amount), 0)	+ IFNULL(SUM(selling_other.amount), 0))	AS amount " +
						"FROM selling_invoice " +
						"LEFT JOIN selling_cash " +
							"ON (selling_invoice.id = selling_cash.selling_invoice_id AND DATE(selling_cash.date) = (selling_invoice.date)) " +
						"LEFT JOIN selling_cheque " +
							"ON (selling_invoice.id = selling_cheque.selling_invoice_id AND DATE(selling_cheque.issued_date) = (selling_invoice.date)) " +
						"LEFT JOIN selling_other  " +
							"ON (selling_invoice.id = selling_other.selling_invoice_id AND DATE(selling_other.date) = (selling_invoice.date)) " +
						"WHERE selling_invoice.`status` = '1' AND selling_invoice.is_completely_paid = '0' " +
						"GROUP BY DATE(selling_invoice.date) " +
					") AS paid_credit_bills ON (paid_credit_bills.date = DATE(selling_invoice.date)) " +

					"LEFT JOIN ( " +
						"SELECT " +
							"DATE(`selling_invoice`.`date`) `date`, " +
							"(SUM(`selling_item`.`default_price` * `selling_item`.`quantity`) -  " +
								"(SUM(selling_item.discount * selling_item.quantity) + selling_invoice.discount) - " +
								"SUM(selling_item.default_price * selling_item.market_return_quantity) -  " +
								"SUM(selling_item.default_price * selling_item.good_return_quantity) -  " +
								"SUM(selling_item.default_price * selling_item.waste_return_quantity) " +
							") AS net_total " +
						"FROM  " +
						"selling_item  " +
						"INNER JOIN `selling_invoice` ON (`selling_invoice`.`id` = `selling_item`.`selling_invoice_id`) " +
						"WHERE  " +
						"selling_invoice.`status` = '1' AND selling_invoice.is_completely_paid = '0'  " +
						"GROUP BY DATE(selling_invoice.date) " +
					") AS credit_bills ON (credit_bills.date = DATE(selling_invoice.date)) " +

					"LEFT JOIN ( " +
						"SELECT " +
							"DATE(`selling_invoice`.`date`) `date`, " +
							"(SUM(`selling_item`.`default_price` * `selling_item`.`quantity`) -  " +
								"(SUM(selling_item.discount * selling_item.quantity) + selling_invoice.discount) - " +
								"SUM(selling_item.default_price * selling_item.market_return_quantity) -  " +
								"SUM(selling_item.default_price * selling_item.good_return_quantity) -  " +
								"SUM(selling_item.default_price * selling_item.waste_return_quantity) " +
							") AS net_total " +
						"FROM  " +
						"selling_item  " +
						"INNER JOIN `selling_invoice` ON (`selling_invoice`.`id` = `selling_item`.`selling_invoice_id`) " +
						"WHERE  " +
						"selling_invoice.`status` = '1' AND selling_invoice.is_completely_paid = '1'  " +
						"GROUP BY DATE(selling_invoice.date) " +
					") AS completely_paid_bills_net_total ON (completely_paid_bills_net_total.date = DATE(selling_invoice.date)) " +

					"LEFT JOIN ( " +
						"SELECT  " +
						"DATE(selling_invoice.date) as date, " +
						"(IFNULL(SUM(selling_cash.amount), 0) + IFNULL(SUM(selling_cheque.amount), 0) + IFNULL(SUM(selling_other.amount), 0)) as amount " +
						"FROM selling_invoice " +
						"LEFT JOIN selling_cash " +
							"ON (selling_invoice.id = selling_cash.selling_invoice_id AND DATE(selling_cash.date) = (selling_invoice.date)) " +
						"LEFT JOIN selling_cheque " +
							"ON (selling_invoice.id = selling_cheque.selling_invoice_id AND DATE(selling_cheque.issued_date) = (selling_invoice.date)) " +
						"LEFT JOIN selling_other  " +
							"ON (selling_invoice.id = selling_other.selling_invoice_id AND DATE(selling_other.date) = (selling_invoice.date)) " +
						"WHERE selling_invoice.`status` = '1' AND selling_invoice.is_completely_paid = '1' " +
						"GROUP BY DATE(selling_invoice.date)  " +
					") AS completely_paid_bills_paid_amount ON (completely_paid_bills_paid_amount.date = DATE(selling_invoice.date)) " +

					"WHERE " +
						"selling_invoice.`status` = '1'	" +
						( ( dateFrom != null && dateTo != null ) ? "AND (DATE(selling_invoice.date) BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
						( dateFrom != null ? "AND DATE(selling_invoice.date) LIKE '" + dateFrom + "' " :
						( dateTo != null ? "AND DATE(selling_invoice.date) LIKE '" + dateTo + "' " : "" )
						) ) +
					"GROUP BY DATE(selling_invoice.date)" +
					"ORDER BY DATE(selling_invoice.date) DESC " +
					") AS daily_sale /*ORDER BY daily_sale.date ASC*/ " +
					"LIMIT " + start + "," + count;
				}
				dataSet = DBConnector.getInstance().getDataSet(query);
			} catch ( Exception ) {
			}
			return dataSet;
		}

		public static DataSet getDailyItemSale( int categoryId, int companyId, int ItemId, String dateFrom, String dateTo, bool isCount, int start, int count ) {
			DataSet dataSet = null;
			try {
				String query = "";
				if ( isCount ) {
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
						( categoryId > 0 ? "AND category.id = '" + categoryId + "' " : "" ) +
						( companyId > 0 ? "AND company.id = '" + companyId + "' " : "" ) +
						( ItemId > 0 ? "AND item.id = '" + ItemId + "' " : "" ) +
						( ( dateFrom != null && dateTo != null ) ? "AND (DATE(selling_invoice.date) BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
						( dateFrom != null ? "AND DATE(selling_invoice.date) LIKE '" + dateFrom + "' " :
						( dateTo != null ? "AND DATE(selling_invoice.date) LIKE '" + dateTo + "' " : "" )
						) ) +
					"GROUP BY DATE(selling_invoice.date), item.id) AS daily_item_sale";
				} else {
					query = "SELECT * FROM( SELECT " +
						"DATE(selling_invoice.date) as date, " +
						"CONCAT(item.`name`, ' (', company.`name`, ')') as item_name, " +
						"SUM(selling_item.quantity) as quantity, " +
						"SUM(`selling_item`.`default_price` * `selling_item`.`quantity`) `gross_sale`, " +
						"(SUM(selling_item.discount * selling_item.quantity)) AS discount, " +
						"SUM(selling_item.default_price * selling_item.market_return_quantity) AS cr, " +
						"SUM(selling_item.default_price * selling_item.good_return_quantity) AS gr, " +
						"SUM(selling_item.default_price * selling_item.waste_return_quantity) AS wr, " +
						"(SUM(`selling_item`.`default_price` * `selling_item`.`quantity`) - " +
							"(SUM(selling_item.discount * selling_item.quantity) + selling_invoice.discount) - " +
							"SUM(selling_item.default_price * selling_item.market_return_quantity) - " +
							"SUM(selling_item.default_price * selling_item.good_return_quantity) - " +
							"SUM(selling_item.default_price * selling_item.waste_return_quantity) " +
						") AS net_total, " +
						"SUM((selling_item.selling_price_actual - selling_item.buying_price_actual) * (selling_item.quantity - (selling_item.market_return_quantity + selling_item.good_return_quantity + selling_item.waste_return_quantity))) as profit " +
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
						( categoryId > 0 ? "AND category.id = '" + categoryId + "' " : "" ) +
						( companyId > 0 ? "AND company.id = '" + companyId + "' " : "" ) +
						( ItemId > 0 ? "AND item.id = '" + ItemId + "' " : "" ) +
						( ( dateFrom != null && dateTo != null ) ? "AND (DATE(selling_invoice.date) BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
						( dateFrom != null ? "AND DATE(selling_invoice.date) LIKE '" + dateFrom + "' " :
						( dateTo != null ? "AND DATE(selling_invoice.date) LIKE '" + dateTo + "' " : "" )
						) ) +
					"GROUP BY DATE(selling_invoice.date), item.id " +
					"ORDER BY DATE(selling_invoice.date) DESC " +
					") AS daily_item_sale ORDER BY daily_item_sale.date ASC " +
					"LIMIT " + start + "," + count;
				}
				dataSet = DBConnector.getInstance().getDataSet(query);
			} catch ( Exception ) {
			}
			return dataSet;
		}

		public static DataSet getDailyProfit( String dateFrom, String dateTo, bool isCount, int start, int count ) {
			DataSet dataSet = null;
			try {
				String query = "DROP TABLE IF EXISTS `tmp_expenses`; " +
								"DROP TABLE IF EXISTS `tmp_selling_invoice`; ";
				query += "CREATE TABLE `tmp_expenses` " +
							"SELECT `expense`.`date`, " +
							"SUM(`expense`.`amount`) `expense` " +
							"FROM `expense` " +
							"WHERE id != '0' " +
							( ( dateFrom != null && dateTo != null ) ? "AND (DATE(date) BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
							( dateFrom != null ? "AND DATE(date) LIKE '" + dateFrom + "' " :
							( dateTo != null ? "AND DATE(date) LIKE '" + dateTo + "' " : "" )
							) ) +
							"GROUP BY `expense`.`date`; ";
				query += "CREATE TABLE `tmp_selling_invoice` " +
							"SELECT DATE(`selling_invoice`.`date`) `date`, " +
                            "SUM((`selling_item`.`selling_price_actual` - `selling_item`.`buying_price_actual`) * `selling_item`.quantity) `profit` " +
							"FROM `selling_invoice` " +
							"INNER JOIN `selling_item` " +
							"ON `selling_item`.`selling_invoice_id` = `selling_invoice`.`id` " +
							"WHERE selling_invoice.`status` = '1'  " +
							( ( dateFrom != null && dateTo != null ) ? "AND (DATE(selling_invoice.date) BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
							( dateFrom != null ? "AND DATE(selling_invoice.date) LIKE '" + dateFrom + "' " :
							( dateTo != null ? "AND DATE(selling_invoice.date) LIKE '" + dateTo + "' " : "" )
							) ) +
							"GROUP BY DATE(`selling_invoice`.`date`);";

				if ( isCount ) {
					query += "SELECT COUNT(*) " +
							"FROM (SELECT  " +
							"IFNULL(`edate`,`pdate`) `date`,  " +
							"IFNULL(`profit`,0) profit,  " +
							"IFNULL(`expense`, 0) expense,  " +
							"FORMAT(IFNULL(`profit`,0) - IFNULL(`expense`,0),2) `net_profit`  " +
						"FROM (SELECT `tmp_expenses`.`date` `edate`, `tmp_expenses`.`expense`, `tmp_selling_invoice`.`date` `pdate`, `tmp_selling_invoice`.`profit` FROM `tmp_expenses` " +
						"LEFT JOIN `tmp_selling_invoice` ON `tmp_expenses`.`date` = `tmp_selling_invoice`.`date` " +
						"UNION " +
						"SELECT `tmp_expenses`.`date` `edate`, `tmp_expenses`.`expense`, `tmp_selling_invoice`.`date` `pdate`, `tmp_selling_invoice`.`profit` FROM `tmp_expenses` " +
						"RIGHT JOIN `tmp_selling_invoice` ON `tmp_expenses`.`date` = `tmp_selling_invoice`.`date`) `aaa`  " +
						"ORDER BY date DESC) AS daily_profit;";
				} else {
					query += "SELECT * FROM (SELECT  " +
								"IFNULL(`edate`,`pdate`) `date`, " +
								"IFNULL(`profit`,0) profit, " +
								"IFNULL(`expense`, 0) expense, " +
								"FORMAT(IFNULL(`profit`,0) - IFNULL(`expense`,0),2) `net_profit` " +
							"FROM (SELECT `tmp_expenses`.`date` `edate`, `tmp_expenses`.`expense`, `tmp_selling_invoice`.`date` `pdate`, `tmp_selling_invoice`.`profit` FROM `tmp_expenses` " +
							"LEFT JOIN `tmp_selling_invoice` ON `tmp_expenses`.`date` = `tmp_selling_invoice`.`date` " +
							"UNION " +
							"SELECT `tmp_expenses`.`date` `edate`, `tmp_expenses`.`expense`, `tmp_selling_invoice`.`date` `pdate`, `tmp_selling_invoice`.`profit` FROM `tmp_expenses` " +
							"RIGHT JOIN `tmp_selling_invoice` ON `tmp_expenses`.`date` = `tmp_selling_invoice`.`date`) `aaa` " +
							"ORDER BY date DESC LIMIT " + start + ", " + count + ") AS daily_profit;";
				}
				query += "DROP TABLE IF EXISTS `tmp_expenses`; " +
						"DROP TABLE IF EXISTS `tmp_selling_invoice`; ";
				dataSet = DBConnector.getInstance().getDataSet(query);
			} catch ( Exception ) {
			}
			return dataSet;
		}

		public static DataSet getProfitPerItem( String dateFrom, String dateTo, int categoryId, int companyId, String itemName, bool isCount, int start, int count ) {
			DataSet dataSet = null;
			try {
				String query = "";
				if ( isCount ) {
					query = "SELECT COUNT(*) " +
					"FROM " +
					"(SELECT " +
						"item.id " +
					"FROM " +
						"selling_invoice " +
					"INNER JOIN (selling_item, item, category, company) " +
					"ON (" +
						"selling_invoice.id = selling_item.selling_invoice_id " +
						"AND item.id = selling_item.item_id " +
						"AND item.category_id = category.id " +
						"AND item.company_id = company.id " +
					") " +
					"WHERE " +
						"selling_invoice.`status` = '1' " +
						( categoryId > 0 ? "AND category.`id` LIKE '" + categoryId + "' " : "" ) +
						( companyId > 0 ? "AND company.`id` LIKE '" + companyId + "' " : "" ) +
						( itemName != null ? "AND item.`name` LIKE '%" + itemName + "%' " : "" ) +
						( ( dateFrom != null && dateTo != null ) ? "AND (DATE(selling_invoice.date) BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
						( dateFrom != null ? "AND DATE(selling_invoice.date) LIKE '" + dateFrom + "' " :
						( dateTo != null ? "AND DATE(selling_invoice.date) LIKE '" + dateTo + "' " : "" )
						) ) +
					"GROUP BY item.id " +
					"ORDER BY selling_invoice.date DESC) as ddd ";
				} else {
					query = "SELECT * FROM (SELECT " +
						"item.id, " +
						"category.`name` as category, " +
						"company.`name` as company, " +
						"item.`name`, " +
						"SUM((selling_item.selling_price_actual - selling_item.buying_price_actual) * selling_item.quantity) as profit " +
					"FROM " +
						"selling_invoice " +
					"INNER JOIN (selling_item, item, category, company) " +
					"ON (" +
						"selling_invoice.id = selling_item.selling_invoice_id " +
						"AND item.id = selling_item.item_id " +
						"AND item.category_id = category.id " +
						"AND item.company_id = company.id " +
					") " +
					"WHERE " +
						"selling_invoice.`status` = '1' " +
						( categoryId > 0 ? "AND category.`id` LIKE '" + categoryId + "' " : "" ) +
						( companyId > 0 ? "AND company.`id` LIKE '" + companyId + "' " : "" ) +
						( itemName != null ? "AND item.`name` LIKE '%" + itemName + "%' " : "" ) +
						( ( dateFrom != null && dateTo != null ) ? "AND (DATE(selling_invoice.date) BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
						( dateFrom != null ? "AND DATE(selling_invoice.date) LIKE '" + dateFrom + "' " :
						( dateTo != null ? "AND DATE(selling_invoice.date) LIKE '" + dateTo + "' " : "" )
						) ) +
					"GROUP BY item.id " +
					"ORDER BY selling_invoice.date DESC) as ddd " +
					"LIMIT " + start + "," + count;
				}
				dataSet = DBConnector.getInstance().getDataSet(query);
			} catch ( Exception ) {
			}
			return dataSet;
		}

	}
}
