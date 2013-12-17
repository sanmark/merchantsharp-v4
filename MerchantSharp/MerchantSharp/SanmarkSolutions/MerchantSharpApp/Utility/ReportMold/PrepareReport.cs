using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.ReportMold {
	class PrepareReport {

		private Dictionary<String, String> dictionary = null;

		public PrepareReport() {
			this.dictionary = new Dictionary<String, String>();
			//addToday();
			//addCommon();
		}

		public PrepareReport(SellingInvoice sellingInvoice) {
			this.dictionary = new Dictionary<String, String>();
			//addToday();
			//addCommon();
			//addSellingInvoicePara(sellingInvoice);
		}

		public void addParameter(String key, String value) {
			try {
				dictionary.Add(key, value);
			} catch(Exception) {
				dictionary.Remove(key);
				dictionary.Add(key, value);
			}
		}

		public Dictionary<String, String> getParameters() {
			return this.dictionary;
		}
		/*
		 *
		 * Get default values.
		 *
		 */
		private void addToday() {
			try {
				addParameter("reportDate", DateTime.Now.ToString("yyyy-MM-dd"));
			} catch(Exception) {
			}
		}

		public void addReportPeriod(string fromDate, string toDate) {
			try {
				if(!String.IsNullOrEmpty(fromDate) && !String.IsNullOrEmpty(toDate)) {
					addParameter("reportDescription", "From  " + CommonMethods.dateTimeFormat(fromDate, "yyyy-MM-dd") + "  To  " + CommonMethods.dateTimeFormat(toDate, "yyyy-MM-dd"));
				} else if(!String.IsNullOrEmpty(fromDate) || !String.IsNullOrEmpty(toDate)) {
					string selectedDate;
					if(!String.IsNullOrEmpty(fromDate)) { selectedDate = fromDate; } else { selectedDate = toDate; }
					addParameter("reportDescription", CommonMethods.dateTimeFormat(selectedDate, "yyyy-MM-dd"));
				} else {
					addParameter("reportDescription", "");
				}
			} catch(Exception) {

			}
		}

		private void addCommon() {
			try {
				addParameter("systemName", MerchantSharp.Properties.Resources.ProjectName);
				addParameter("companyName", MerchantSharp.Properties.Resources.CompanyName);
				addParameter("footerText", "Developed by Sanmark Solutions ( http://thesanmark.com )");
				//addParameter("reportDescription", "FROM 2001-01-02 TO 2001-02-01");
			} catch(Exception) {
			}
		}

		private void addSellingInvoicePara(SellingInvoice sellingInvoice) {
			try {
				TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
				/*addParameter("reportType", "Selling Invoice");
				addParameter("shopName", myTI.ToUpper(Session..getMeta("shopName")));
				addParameter("shopAddress1", myTI.ToUpper(metaImpl.getMeta("shopAddress1")));
				addParameter("shopAddress2", myTI.ToUpper(metaImpl.getMeta("shopAddress2")));
				addParameter("shopAddress3", myTI.ToUpper(metaImpl.getMeta("shopAddress3")));
				addParameter("shopTelephone", myTI.ToUpper(metaImpl.getMeta("shopTelephone")));

				string sellingInvoicePrint_language = metaImpl.getMeta("sellingInvoicePrint_language");
				string showDiscountOrOurPrice = metaImpl.getMeta("sellingInvoicePrint_showDiscountOrOurPrice");

				addParameter("reportDate", sellingInvoice.Date == DateTime.Now.ToString("yyyy-MM-dd") ? DateTime.Now.ToString("G") : sellingInvoice.Date);
				addParameter("reportInvoice", sellingInvoice.InvoiceNumber);
				addParameter("message", metaImpl.getMeta("sellingInvoicePrint_message"));
				addParameter("signature", sellingInvoice.IsCompletelyPaid == 0 ? "\n............." : "");


				if(sellingInvoicePrint_language == "d") {
					addParameter("ct_discountOrPrice", showDiscountOrOurPrice == "d" ? "DISCOUNT" : "OUR PRICE");
					addParameter("ct_invoiceNumber", sellingInvoice.IsCompletelyPaid == 1 ? "INVOICE NO " : "CREDIT INVOICE NO ");
					addParameter("ct_printedPrice", "PRICE");
					addParameter("ct_quantity", "QTY");
					addParameter("ct_lineTotal", "LINE TOTAL");
					addParameter("ct_subTotal", "SUB TOTAL");
					addParameter("ct_billDiscount", "BILL DISCOUNT");
					addParameter("ct_totalReturn", "TOTAL RETURN");
					addParameter("ct_netTotal", "NET TOTAL");
					addParameter("ct_moneyGiven", "GIVEN MONEY");
					addParameter("ct_balance", "BALANCE");
					addParameter("ct_customerName", "Cus. Name");
					addParameter("ct_totalMoney", sellingInvoice.IsQuickPaid == 1 ? "" : "TOTAL PAYMENT");
					addParameter("ct_userName", "CASHIER");
					addParameter("ct_numberOfItems", "Num. Of Items");
					addParameter("thankyouText", /*metaImpl.getMeta("sellingInvoicePrint_thankyouText")"THANK YOU. COME AGAIN.");
				} else if(sellingInvoicePrint_language == "c") {
					addParameter("ct_invoiceNumber", sellingInvoice.IsCompletelyPaid == 1 ? metaImpl.getMeta("sellingInvoicePrint_invoiceNumber_customLanguageText") : metaImpl.getMeta("sellingInvoicePrint_creditInvoice_customLanguageText"));
					addParameter("ct_discountOrPrice", metaImpl.getMeta(showDiscountOrOurPrice == "o" ? "sellingInvoicePrint_ourPrice_customLanguageText" : "sellingInvoicePrint_discount_customLanguageText"));
					addParameter("ct_printedPrice", metaImpl.getMeta("sellingInvoicePrint_printedPrice_customLanguageText"));
					addParameter("ct_quantity", metaImpl.getMeta("sellingInvoicePrint_quantity_customLanguageText"));
					addParameter("ct_lineTotal", metaImpl.getMeta("sellingInvoicePrint_lineTotal_customLanguageText"));
					addParameter("ct_subTotal", metaImpl.getMeta("sellingInvoicePrint_subTotal_customLanguageText"));
					addParameter("ct_billDiscount", metaImpl.getMeta("sellingInvoicePrint_billDiscount_customLanguageText"));
					addParameter("ct_totalReturn", metaImpl.getMeta("sellingInvoicePrint_totalReturn_customLanguageText"));
					addParameter("ct_netTotal", metaImpl.getMeta("sellingInvoicePrint_netTotal_customLanguageText"));
					addParameter("ct_moneyGiven", metaImpl.getMeta("sellingInvoicePrint_moneyGiven_customLanguageText"));
					addParameter("ct_balance", metaImpl.getMeta("sellingInvoicePrint_balance_customLanguageText"));
					addParameter("ct_customerName", metaImpl.getMeta("sellingInvoicePrint_customerName_customLanguageText"));
					addParameter("ct_totalMoney", sellingInvoice.IsQuickPaid == 1 ? "" : metaImpl.getMeta("sellingInvoicePrint_totalPayment_customLanguageText"));
					addParameter("ct_userName", metaImpl.getMeta("sellingInvoicePrint_userName_customLanguageText"));
					addParameter("ct_numberOfItems", metaImpl.getMeta("sellingInvoicePrint_numberOfItems_customLanguageText"));
					addParameter("thankyouText", metaImpl.getMeta("sellingInvoicePrint_thankyouText_customLanguageText"));
				}
				//ShowMessage.error(addSellingInvoice.textBox_balance_selectedItems.Text);

				addParameter("footerText", MetaImpl.getInstance().getMeta("footerText2").Replace("$", "" + System.Environment.NewLine));
				//addParameter("reportDescription", "Test text");
				addReportPeriod(null, null);*/
				/**/
			} catch(Exception) {
			}
		}

	}
}
