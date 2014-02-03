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
			addSellingInvoicePara(sellingInvoice);
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
				//addParameter("reportType", "Selling Invoice");
				addParameter("shopName", myTI.ToUpper(Session.Preference["shopName"]));
				addParameter("shopAddress1", myTI.ToUpper(Session.Preference["shopAddress1"]));
				addParameter("shopAddress2", myTI.ToUpper(Session.Preference["shopAddress2"]));
				addParameter("shopAddress3", myTI.ToUpper(Session.Preference["shopAddress3"]));
				addParameter("shopTelephone", myTI.ToUpper(Session.Preference["shopTelephone"]));

				string sellingInvoicePrint_language = Session.Preference["sellingInvoicePrint_language"];
				string showDiscountOrOurPrice = Session.Preference["sellingInvoicePrint_showDiscountOrOurPrice"];

				addParameter("reportDate", sellingInvoice.Date.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd") ? DateTime.Now.ToString("G") : sellingInvoice.Date.ToString("yyyy-MM-dd"));
				addParameter("reportInvoice", sellingInvoice.InvoiceNumber);
				addParameter("message", Session.Preference["sellingInvoicePrint_message"]);
				addParameter("signature", sellingInvoice.IsCompletelyPaid == 0 ? "\n............." : "");


				if(sellingInvoicePrint_language == "d") {
					addParameter("ct_reportInvoice", sellingInvoice.IsCompletelyPaid == 1 ? "INVOICE NO " : "CREDIT INVOICE NO ");
					addParameter("ct_userName", "CASHIER");
					addParameter("ct_customerName", "Cus. Name");
					addParameter("ct_printedPrice", "PRICE");
					addParameter("ct_discountOrPrice", showDiscountOrOurPrice == "d" ? "DISCOUNT" : "OUR PRICE");
					addParameter("ct_quantity", "QTY");
					addParameter("ct_lineTotal", "LINE TOTAL");
					addParameter("ct_subTotal", "SUB TOTAL");
					addParameter("ct_numberOfItems", "Num. Of Items");
					addParameter("ct_billDiscount", "BILL DISCOUNT");
					addParameter("ct_totalReturn", "TOTAL RETURN");
					addParameter("ct_netTotal", "NET TOTAL");
					addParameter("ct_givenMoney", "GIVEN MONEY");
					addParameter("ct_totalMoney", sellingInvoice.IsQuickPaid == 1 ? "" : "TOTAL PAYMENT");
					addParameter("ct_balance", "BALANCE");					
				} else if(sellingInvoicePrint_language == "c") {
					addParameter("ct_reportInvoice", sellingInvoice.IsCompletelyPaid == 1 ? Session.Preference["sellingInvoicePrint_invoiceNumber_customLanguageText"] : Session.Preference["sellingInvoicePrint_creditInvoice_customLanguageText"]);
					addParameter("ct_userName", Session.Preference["sellingInvoicePrint_userName_customLanguageText"]);
					addParameter("ct_customerName", Session.Preference["sellingInvoicePrint_customerName_customLanguageText"]);
					addParameter("ct_printedPrice", Session.Preference["sellingInvoicePrint_printedPrice_customLanguageText"]);
					addParameter("ct_discountOrPrice", Session.Preference[(showDiscountOrOurPrice == "o" ? "sellingInvoicePrint_ourPrice_customLanguageText" : "sellingInvoicePrint_discount_customLanguageText")]);
					addParameter("ct_quantity", Session.Preference["sellingInvoicePrint_quantity_customLanguageText"]);
					addParameter("ct_lineTotal", Session.Preference["sellingInvoicePrint_lineTotal_customLanguageText"]);
					addParameter("ct_subTotal", Session.Preference["sellingInvoicePrint_subTotal_customLanguageText"]);
					addParameter("ct_numberOfItems", Session.Preference["sellingInvoicePrint_numberOfItems_customLanguageText"]);
					addParameter("ct_billDiscount", Session.Preference["sellingInvoicePrint_billDiscount_customLanguageText"]);
					addParameter("ct_totalReturn", Session.Preference["sellingInvoicePrint_totalReturn_customLanguageText"]);
					addParameter("ct_netTotal", Session.Preference["sellingInvoicePrint_netTotal_customLanguageText"]);
					addParameter("ct_givenMoney", Session.Preference["sellingInvoicePrint_givenMoney_customLanguageText"]);
					addParameter("ct_totalMoney", sellingInvoice.IsQuickPaid == 1 ? "" : Session.Preference["sellingInvoicePrint_totalPayment_customLanguageText"]);
					addParameter("ct_balance", Session.Preference["sellingInvoicePrint_balance_customLanguageText"]);
				}
				addParameter("thankyouText", Session.Preference["sellingInvoicePrint_thankyouText"]);
				//ShowMessage.error(addSellingInvoice.textBox_balance_selectedItems.Text);

				addParameter("footerText", Session.Preference["sellingInvoiceFooterText"].Replace("$", "" + System.Environment.NewLine));
				//addParameter("reportDescription", "Test text");
				//addReportPeriod(null, null);
				/**/
			} catch(Exception) {
			}
		}

	}
}
