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
				foreach (DataRow row in dataSet.Tables[0].Rows) {

				}
			} catch (Exception) {
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
									"(item.`name` LIKE '" + name + "%' " +
									"OR item.`name` LIKE '% " + name + "%') " +
									"AND category.`name` LIKE '%" + category + "%' " +
									"AND company.`name` LIKE '%" + company + "%' " +
									"LIMIT 100";
				dataSet = DBConnector.getInstance().getDataSet(query);
			} catch (Exception) {
			}
			return dataSet;
		}

		public static DataSet getStockForFilter(int locationId, String itemName, String itemCode, String barcode,
			int categoryId, int companyId, bool belowROL, bool isCount, int start, int count) {
			DataSet dataSet = null;
			try {
				String query = null;

				if (isCount) {
					query = "SELECT COUNT(id) FROM (SELECT " +
										"stock_item.id, " +
										"stock_location.`name`, " +
										"item.`name` AS item_name, " +
										"category.`name` AS category_name, " +
										"company.`name` AS company_name, " +
										(locationId > 0 ? "stock_item.quantity, " : "(SUM(stock_item.quantity)) as quantity, ") +
										"item.reorder_level, " +
										"(item.unit_buying_price * " + (locationId > 0 ? "stock_item.quantity" : "SUM(stock_item.quantity)") + ") AS value, " +
										"stock_item.item_id as item_id " +
									"FROM stock_item " +
									"LEFT JOIN (stock_location, item, category, company) " +
										"ON (" +
											"stock_location.id=stock_item.stock_location_id " +
											"AND item.id=stock_item.item_id " +
											"AND item.category_id=category.id " +
											"AND item.company_id=company.id " +
										")" +
									"WHERE " +
										(locationId > 0 ? "stock_location.id = '" + locationId + "' " : "stock_location.id != '0' ") +
										"AND item.`name` LIKE '%" + (itemName != null ? itemName : "") + "%' " +
										(itemCode != null ? "AND item.`code` = '" + itemCode + "' " : "") +
										(barcode != null ? "AND item.barcode = '" + barcode + "' " : "") +
										(categoryId > 0 ? "AND item.category_id = '" + categoryId + "' " : "") +
										(companyId > 0 ? "AND item.company_id = '" + companyId + "' " : "") +
										(belowROL ? "AND item.reorder_level >= stock_item.quantity " : "") +
										(locationId <= 0 ? " GROUP BY stock_item.item_id " : "") + ") as dfe";
				} else {
					query = "SELECT " +
										"stock_item.id, " +
										"stock_location.`name`, " +
										"item.`name` AS item_name, " +
										"category.`name` AS category_name, " +
										"company.`name` AS company_name, " +
										(locationId > 0 ? "stock_item.quantity, " : "(SUM(stock_item.quantity)) as quantity, ") +
										"item.reorder_level, " +
										"(item.unit_buying_price * " + (locationId > 0 ? "stock_item.quantity" : "SUM(stock_item.quantity)") + ") AS value, " +
										"stock_item.item_id as item_id " +
									"FROM stock_item " +
									"LEFT JOIN (stock_location, item, category, company) " +
										"ON (" +
											"stock_location.id=stock_item.stock_location_id " +
											"AND item.id=stock_item.item_id " +
											"AND item.category_id=category.id " +
											"AND item.company_id=company.id " +
										")" +
									"WHERE " +
										(locationId > 0 ? "stock_location.id = '" + locationId + "' " : "stock_location.id != '0' ") +
										"AND item.`name` LIKE '%" + (itemName != null ? itemName : "") + "%' " +
										(itemCode != null ? "AND item.`code` = '" + itemCode + "' " : "") +
										(barcode != null ? "AND item.barcode = '" + barcode + "' " : "") +
										(categoryId > 0 ? "AND item.category_id = '" + categoryId + "' " : "") +
										(companyId > 0 ? "AND item.company_id = '" + companyId + "' " : "") +
										(belowROL ? "AND item.reorder_level >= stock_item.quantity " : "") +
										(locationId <= 0 ? " GROUP BY stock_item.item_id " : "") +
									"LIMIT " + start + "," + count;
				}
				dataSet = DBConnector.getInstance().getDataSet(query);
			} catch (Exception) {
			}
			return dataSet;
		}

		public static DataSet getBuyingItemForFilter(String itemName, String code, String barcode, int vendorId,
			String invoiceNumber, String grn, String dateFrom, String dateTo, bool isCount, int start, int count) {
			DataSet dataSet = null;
			try {
				String query = "SELECT " +
									(isCount ? "COUNT(*)" : "buying_item.id, " +
									"item.`name`, " +
									"(SELECT company.`name` FROM company WHERE id=item.company_id) as company_name, " +
									"(SELECT vendor.`name` FROM vendor WHERE vendor.id = buying_invoice.vendor_id) as vendor_name, " +
									"buying_invoice.invoice_number, " +
									"buying_invoice.grn, " +
									"buying_invoice.ordered_date, " +
									"buying_item.buying_price, " +
									"unit.`name` as unit_name, " +
									"buying_item.quantity, " +
									"buying_item.free_quantity, " +
									"(buying_item.quantity * buying_item.buying_price) as line_total ") +
								"FROM buying_item " +
								"LEFT JOIN (item, buying_invoice, vendor, unit) " +
									"ON (" +
										"item.id=buying_item.item_id " +
										"AND buying_invoice.id=buying_item.buying_invoice_id " +
										"AND buying_invoice.vendor_id=vendor.id " +
										"AND item.unit_id=unit.id " +
									")" +
								"WHERE " +
									"item.`name` LIKE '%" + itemName + "%' " +
									(code.Length > 0 ? ("AND item.`code` = '" + code + "' ") : "") +
									"AND item.barcode LIKE '%" + barcode + "%'" +
									(vendorId > 0 ? "AND vendor.id = '" + vendorId + "' " : "") +
									"AND buying_invoice.invoice_number LIKE '%" + invoiceNumber + "%' " +
									"AND buying_invoice.grn LIKE '%" + grn + "%' " +
									((dateFrom != null && dateTo != null) ? "AND (buying_invoice.ordered_date BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
									(dateFrom != null ? "AND buying_invoice.ordered_date LIKE '" + dateFrom + "' " :
									(dateTo != null ? "AND buying_invoice.ordered_date LIKE '" + dateTo + "' " : "")
									)) +
								"ORDER BY buying_item.id DESC " +
								"LIMIT " + start + "," + count;
				dataSet = DBConnector.getInstance().getDataSet(query);
			} catch (Exception) {
			}
			return dataSet;
		}

		public static DataSet getSellingItemForFilter(String itemName, String code, String barcode, int customerId,
			String invoiceNumber, String dateFrom, String dateTo, bool isCount, int start, int count) {
			DataSet dataSet = null;
			try {
				String query = "SELECT " +
									(isCount ? "COUNT(*)" : "selling_item.id, " +
									"item.`name`, " +
									"(SELECT company.`name` FROM company WHERE id=item.company_id) as company_name, " +
									"(SELECT customer.`name` FROM customer WHERE customer.id = selling_invoice.customer_id) as customer_name, " +
									"selling_invoice.invoice_number, " +
									"selling_invoice.date, " +
									"selling_item.default_price as price, " +
									"selling_item.discount, " +
									"unit.`name` as unit_name, " +
									"selling_item.selling_mode, " +
									"selling_item.quantity, " +
									"selling_item.market_return_quantity as cr, " +
									"selling_item.good_return_quantity as gr, " +
									"selling_item.waste_return_quantity as wr, " +
									"((selling_item.quantity - selling_item.good_return_quantity) * (selling_item.default_price - selling_item.discount)) as line_total ") +
								"FROM selling_item " +
								"LEFT JOIN (item, selling_invoice, customer, unit) " +
									"ON (" +
										"item.id=selling_item.item_id " +
										"AND selling_invoice.id=selling_item.selling_invoice_id " +
										"AND selling_invoice.customer_id=customer.id " +
										"AND item.unit_id=unit.id " +
									")" +
								"WHERE " +
									"item.`name` LIKE '%" + itemName + "%' ";

				if (code.Length > 0) {
					query += "AND item.`code` = '" + code + "' ";
				}
				query += "AND item.barcode LIKE '%" + barcode + "%'" +
				(customerId > 0 ? "AND customer.id = '" + customerId + "' " : "") +
				"AND selling_invoice.invoice_number LIKE '%" + invoiceNumber + "%' " +
				((dateFrom != null && dateTo != null) ? "AND (selling_invoice.date BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
				(dateFrom != null ? "AND selling_invoice.date LIKE '" + dateFrom + "' " :
				(dateTo != null ? "AND selling_invoice.date LIKE '" + dateTo + "' " : "")
				)) +
				"AND selling_invoice.`status` = '1' " +
			"ORDER BY selling_item.id DESC " +
			"LIMIT " + start + "," + count;
				dataSet = DBConnector.getInstance().getDataSet(query);
			} catch (Exception) {
			}
			return dataSet;
		}

		public static DataSet getStockBeforeSaleForFilter(int itemId, String dateFrom, String dateTo, bool isCount, int start, int count) {
			DataSet dataSet = null;
			try {
				String query = "SELECT " +
									(isCount ? "COUNT(*) " : "selling_item.id, " +
									"concat(item.`name`, ' (', company.`name`, ')') as item_name, " +
									"selling_invoice.invoice_number, " +
									"selling_invoice.date, " +
									"customer.`name` as customer_name,  " +
									"selling_item.quantity, " +
									"selling_item.market_return_quantity as cr, " +
									"selling_item.good_return_quantity as gr, " +
									"selling_item.waste_return_quantity as wr, " +
									"selling_item.stock_before_sale ") +
								"FROM selling_item " +
								"LEFT JOIN (selling_invoice, item, company, customer) " +
									"ON (" +
										"selling_item.selling_invoice_id=selling_invoice.id " +
										"AND selling_item.item_id=item.id " +
										"AND company.id=item.company_id " +
										"AND customer.id=selling_invoice.customer_id " +
									")" +
								"WHERE " +
								"selling_invoice.`status` = '1' " +
								(itemId > 0 ? " AND item.`id` = '" + itemId + "' " : "") +
									((dateFrom != null && dateTo != null) ? "AND (selling_invoice.date BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
									(dateFrom != null ? "AND selling_invoice.date LIKE '" + dateFrom + "%' " :
									(dateTo != null ? "AND selling_invoice.date LIKE '" + dateTo + "%' " : " ")
									)) +
								"ORDER BY selling_item.id DESC " +
								"LIMIT " + start + "," + count;
				dataSet = DBConnector.getInstance().getDataSet(query);
			} catch (Exception) {
			}
			return dataSet;
		}

		public static DataSet getCompanyReturnForFilter(String itemName, String code, String barcode, int vendorId,
			String invoiceNumber, String grn, String dateFrom, String dateTo, bool isCount, int start, int count) {
			DataSet dataSet = null;
			try {
				String query = "SELECT " +
									(isCount ? "COUNT(*)" : "company_return.id, " +
									"CONCAT(item.`name`, ' (', company.`name`, ', ', category.`name`, ')') AS `item_name`, " +
									"vendor.`name` AS vendor, " +
									"buying_invoice.invoice_number, " +
									"buying_invoice.grn, " +
									"DATE(company_return.date) AS date, " +
									"company_return.price, " +
									"company_return.quantity, " +
									"(company_return.price * company_return.quantity) AS line_total ") +
								"FROM company_return " +
								"INNER JOIN (item, company, category) " +
									"ON (item.id = company_return.item_id AND item.company_id = company.id AND item.category_id = category.id) " +
								"LEFT JOIN (buying_invoice, vendor) " +
									"ON (buying_invoice.id = company_return.buying_invoice_id AND buying_invoice.vendor_id = vendor.id) " +
								"WHERE " +
									"item.`name` LIKE '%" + itemName + "%' " +
									"AND item.`code` LIKE '%" + code + "%' " +
									"AND item.barcode LIKE '%" + barcode + "%' " +
									(vendorId > 0 ? "AND vendor.id = '" + vendorId + "' " : "") +
									"AND buying_invoice.invoice_number LIKE '%" + invoiceNumber + "%' " +
									"AND buying_invoice.grn LIKE '%" + grn + "%' " +
									((dateFrom != null && dateTo != null) ? "AND (company_return.date BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
									(dateFrom != null ? "AND company_return.date LIKE '" + dateFrom + "' " :
									(dateTo != null ? "AND company_return.date LIKE '" + dateTo + "' " : "")
									)) +
								"ORDER BY company_return.id DESC " +
								"LIMIT " + start + "," + count;
				dataSet = DBConnector.getInstance().getDataSet(query);
			} catch (Exception) {
			}
			return dataSet;
		}

		public static DataSet getSellingInvoiceForFilter(String invoiceNumber, int customerId, int userId, int isPaid, int status,
			String dateFrom, String dateTo, String details, bool isCount, int start, int count) {
			DataSet dataSet = null;
			try {
				String query = null;

				if (isCount) {
					query = "SELECT COUNT(id) FROM (SELECT " +
										"selling_invoice.id " +
									"FROM selling_invoice " +
									"INNER JOIN (selling_item ,customer, `user`) " +
										"ON (" +
											"selling_invoice.id = selling_item.selling_invoice_id " +
											"AND customer.id = selling_invoice.customer_id " +
											"AND `user`.id = selling_invoice.created_by " +
										")" +
									"WHERE selling_invoice.id != '0' " +
									(invoiceNumber != null ? "AND selling_invoice.invoice_number LIKE '" + invoiceNumber + "%' " : "") +
									(customerId > 0 ? "AND customer.id = '" + customerId + "' " : "") +
									(userId > 0 ? "AND selling_invoice.created_by = '" + userId + "' " : "") +
									(isPaid > -1 ? "AND selling_invoice.is_completely_paid = '" + isPaid + "' " : "") +
									(status > -1 ? "AND selling_invoice.`status` = '" + status + "' " : "") +
									((dateFrom != null && dateTo != null) ? "AND (selling_invoice.date BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
									(dateFrom != null ? "AND selling_invoice.date LIKE '" + dateFrom + "' " :
									(dateTo != null ? "AND selling_invoice.date LIKE '" + dateTo + "' " : "")
									)) +
									(details != null ? "AND selling_invoice.details LIKE '%" + details + "%' " : "") +
									"GROUP BY selling_invoice.id " +
									"ORDER BY selling_invoice.date DESC, selling_invoice.id DESC " +
										") AS sellingInvoices";
				} else {
					query = "SELECT " +
									"selling_invoice.id, " +
									"selling_invoice.invoice_number, " +
									"DATE(selling_invoice.date) AS date, " +
									"SUM((selling_item.sold_price - selling_item.discount) * (selling_item.quantity - selling_item.market_return_quantity - selling_item.good_return_quantity - selling_item.waste_return_quantity)) AS sub_total, " +
									"selling_invoice.discount, " +
									"selling_invoice.referrer_commision, " +
									"IF(selling_invoice.`status` = '1', (SUM((selling_item.sold_price - selling_item.discount) * (selling_item.quantity - selling_item.market_return_quantity - selling_item.good_return_quantity - selling_item.waste_return_quantity)) - selling_invoice.discount), '-') AS net_total, " +
									"(( " +
										"SELECT IFNULL(SUM(selling_cash.amount), 0) " +
										"FROM selling_cash  " +
										"WHERE selling_cash.selling_invoice_id = selling_invoice.id " +
									") +  " +
									"( " +
										"SELECT IFNULL(SUM(selling_cheque.amount), 0) " +
										"FROM selling_cheque  " +
										"WHERE selling_cheque.selling_invoice_id = selling_invoice.id " +
									") +  " +
									"( " +
										"SELECT IFNULL(SUM(selling_other.amount), 0) " +
										"FROM selling_other  " +
										"WHERE selling_other.selling_invoice_id = selling_invoice.id " +
									") + selling_invoice.customer_account_balance_change) AS total_payments,  " +
									"selling_invoice.customer_account_balance_change, " +
									"customer.`name` AS customer, " +
									"CONCAT(`user`.first_name, ' ', `user`.last_name) AS `user`, " +
									"IF (selling_invoice.is_completely_paid = '1', 'YES', 'No') as is_completely_paid, " +
									"(CASE " +
										"WHEN selling_invoice.`status` = '1' THEN 'Sold' " +
										"WHEN selling_invoice.`status` = '3' THEN 'Draft' " +
									"END) AS `status` " +
								"FROM selling_invoice " +
								"INNER JOIN (selling_item ,customer, `user`) " +
									"ON (" +
										"selling_invoice.id = selling_item.selling_invoice_id " +
										"AND customer.id = selling_invoice.customer_id " +
										"AND `user`.id = selling_invoice.created_by " +
									")" +
								"WHERE selling_invoice.id != '0' " +
								(invoiceNumber != null ? "AND selling_invoice.invoice_number LIKE '" + invoiceNumber + "%' " : "") +
								(customerId > 0 ? "AND customer.id = '" + customerId + "' " : "") +
								(userId > 0 ? "AND selling_invoice.created_by = '" + userId + "' " : "") +
								(isPaid > -1 ? "AND selling_invoice.is_completely_paid = '" + isPaid + "' " : "") +
								(status > -1 ? "AND selling_invoice.`status` = '" + status + "' " : "") +
								((dateFrom != null && dateTo != null) ? "AND (selling_invoice.date BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
								(dateFrom != null ? "AND selling_invoice.date LIKE '" + dateFrom + "' " :
								(dateTo != null ? "AND selling_invoice.date LIKE '" + dateTo + "' " : "")
								)) +
								(details != null ? "AND selling_invoice.details LIKE '%" + details + "%' " : "") +
								"GROUP BY selling_invoice.id " +
								"ORDER BY selling_invoice.date DESC, selling_invoice.id DESC " +
								"LIMIT " + start + "," + count;
				}
				dataSet = DBConnector.getInstance().getDataSet(query);
			} catch (Exception) {
			}
			return dataSet;
		}

		public static DataSet getBuyingInvoiceForFilter(String grn, String invoiceNumber, int vendorId, int userId, int isPaid, int status,
			String dateFrom, String dateTo, String epDateFrom, String epDateTo, String details, bool isCount, int start, int count) {
			DataSet dataSet = null;
			try {
				String query = null;

				if (isCount) {
					query = "SELECT COUNT(id) FROM (SELECT " +
										"buying_invoice.id " +
									"FROM buying_invoice " +
									"INNER JOIN (buying_item ,vendor, `user`) " +
										"ON (" +
											"buying_invoice.id = buying_item.buying_invoice_id " +
											"AND vendor.id = buying_invoice.vendor_id " +
											"AND `user`.id = buying_invoice.created_by " +
										")" +
									"LEFT JOIN company_return " +
										"ON(company_return.buying_invoice_id = buying_invoice.id) " +
									"WHERE buying_invoice.id != '0' " +
									(grn != null ? "AND buying_invoice.grn LIKE '" + grn + "%' " : "") +
									(invoiceNumber != null ? "AND buying_invoice.invoice_number LIKE '" + invoiceNumber + "%' " : "") +
									(vendorId > 0 ? "AND vendor.id = '" + vendorId + "' " : "") +
									(userId > 0 ? "AND buying_invoice.created_by = '" + userId + "' " : "") +
									(isPaid > -1 ? "AND buying_invoice.is_completely_paid = '" + isPaid + "' " : "") +
									(status > -1 ? "AND buying_invoice.`status` = '" + status + "' " : "") +
									((dateFrom != null && dateTo != null) ? "AND (buying_invoice.ordered_date BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
									(dateFrom != null ? "AND buying_invoice.ordered_date LIKE '" + dateFrom + "' " :
									(dateTo != null ? "AND buying_invoice.ordered_date LIKE '" + dateTo + "' " : "")
									)) +
									((epDateFrom != null && epDateTo != null) ? "AND (buying_invoice.expected_paying_date BETWEEN '" + epDateFrom + "' AND '" + epDateTo + "') " :
									(epDateFrom != null ? "AND buying_invoice.expected_paying_date LIKE '" + epDateFrom + "' " :
									(epDateTo != null ? "AND buying_invoice.expected_paying_date LIKE '" + epDateTo + "' " : "")
									)) +
									(details != null ? "AND buying_invoice.details LIKE '%" + details + "%' " : "") +
									"GROUP BY buying_invoice.id " +
									"ORDER BY buying_invoice.ordered_date DESC, buying_invoice.id DESC " +
										") AS buyingInvoices";
				} else {
					query = "SELECT " +
									"buying_invoice.id, " +
									"buying_invoice.grn, " +
									"buying_invoice.invoice_number, " +
									"DATE(buying_invoice.ordered_date) AS date, " +
									"TRUNCATE(IF(buying_invoice.`status` = 1, SUM(buying_item.buying_price * buying_item.quantity), '-'), 2) AS sub_total, " +
									"buying_invoice.discount, " +
									"IFNULL(buying_invoice.market_return_discount + (company_return.price * company_return.quantity), 0) AS company_return, " +
									"IF(buying_invoice.`status` = '1', (SUM((buying_item.buying_price) * (buying_item.quantity )) - IFNULL(buying_invoice.market_return_discount + SUM(company_return.price * company_return.quantity), 0) - buying_invoice.discount), '-') AS net_total, " +
									"((( " +
										"SELECT IFNULL(SUM(buying_cash.amount), 0) " +
											"FROM buying_cash " +
											"WHERE buying_cash.buying_invoice_id = buying_invoice.id " +
										") + " +
										"( " +
											"SELECT IFNULL(SUM(buying_cheque.amount), 0) " +
											"FROM buying_cheque " +
											"WHERE buying_cheque.buying_invoice_id = buying_invoice.id " +
										") + " +
										"( " +
											"SELECT IFNULL(SUM(buying_other.amount), 0) " +
											"FROM buying_other " +
											"WHERE buying_other.buying_invoice_id = buying_invoice.id " +
										") + buying_invoice.vendor_account_balance_change) " +
										"- IFNULL(buying_invoice.market_return_discount + SUM(company_return.price * company_return.quantity), 0))AS total_payments, " +
									"buying_invoice.vendor_account_balance_change, " +
									"IF(buying_invoice.expected_paying_date = '0001-01-01','-', buying_invoice.expected_paying_date) AS expected_paying_date, " +
									"vendor.`name` AS vendor, " +
									"CONCAT(`user`.first_name, ' ', `user`.last_name) AS `user`, " +
									"IF (buying_invoice.is_completely_paid = '1', 'YES', 'No') as is_completely_paid, " +
									"(CASE " +
										"WHEN buying_invoice.`status` = '1' THEN 'Received' " +
										"WHEN buying_invoice.`status` = '2' THEN 'Request' " +
										"WHEN buying_invoice.`status` = '3' THEN 'Draft' " +
									"END) AS `status` " +
								"FROM buying_invoice " +
								"INNER JOIN (buying_item ,vendor, `user`) " +
									"ON (" +
										"buying_invoice.id = buying_item.buying_invoice_id " +
										"AND vendor.id = buying_invoice.vendor_id " +
										"AND `user`.id = buying_invoice.created_by " +
									")" +
								"LEFT JOIN company_return " +
										"ON(company_return.buying_invoice_id = buying_invoice.id) " +
								"WHERE buying_invoice.id != '0' " +
								(grn != null ? "AND buying_invoice.grn LIKE '" + grn + "%' " : "") +
								(invoiceNumber != null ? "AND buying_invoice.invoice_number LIKE '" + invoiceNumber + "%' " : "") +
								(vendorId > 0 ? "AND vendor.id = '" + vendorId + "' " : "") +
								(userId > 0 ? "AND buying_invoice.created_by = '" + userId + "' " : "") +
								(isPaid > -1 ? "AND buying_invoice.is_completely_paid = '" + isPaid + "' " : "") +
								(status > -1 ? "AND buying_invoice.`status` = '" + status + "' " : "") +
								((dateFrom != null && dateTo != null) ? "AND (buying_invoice.ordered_date BETWEEN '" + dateFrom + "' AND '" + dateTo + "') " :
								(dateFrom != null ? "AND buying_invoice.ordered_date LIKE '" + dateFrom + "' " :
								(dateTo != null ? "AND buying_invoice.ordered_date LIKE '" + dateTo + "' " : "")
								)) +
								((epDateFrom != null && epDateTo != null) ? "AND (buying_invoice.expected_paying_date BETWEEN '" + epDateFrom + "' AND '" + epDateTo + "') " :
								(epDateFrom != null ? "AND buying_invoice.expected_paying_date LIKE '" + epDateFrom + "' " :
								(epDateTo != null ? "AND buying_invoice.expected_paying_date LIKE '" + epDateTo + "' " : "")
								)) +
								(details != null ? "AND buying_invoice.details LIKE '%" + details + "%' " : "") +
								"GROUP BY buying_invoice.id " +
								"ORDER BY buying_invoice.ordered_date DESC, buying_invoice.id DESC " +
								"LIMIT " + start + "," + count;
				}
				dataSet = DBConnector.getInstance().getDataSet(query);
			} catch (Exception) {
			}
			return dataSet;
		}

	}
}
