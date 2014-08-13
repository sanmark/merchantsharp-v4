using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Settings.PreferenceViews {
	/// <summary>
	/// Interaction logic for SellingInvoicePrint.xaml
	/// </summary>
	public partial class SellingInvoicePrint : UserControl, IPreferences {

		private PreferenceManagerImpl preferenceManagerImpl = null;
		Dictionary<String, Preference> dic = null;

		public SellingInvoicePrint() {
			InitializeComponent();
		}

		public void setImpl( object obj ) {
			try {
				preferenceManagerImpl = ( PreferenceManagerImpl )obj;

				dic = new Dictionary<String, Preference>();
				dic.Add("sellingInvoicePrint_displayLineDiscount", preferenceManagerImpl.getPreference("sellingInvoicePrint_displayLineDiscount"));
				dic.Add("sellingInvoicePrint_showDiscountOrOurPrice", preferenceManagerImpl.getPreference("sellingInvoicePrint_showDiscountOrOurPrice"));
				dic.Add("sellingInvoicePrint_numberOfCopiesOfCreditBill", preferenceManagerImpl.getPreference("sellingInvoicePrint_numberOfCopiesOfCreditBill"));
				dic.Add("sellingInvoicePrint_thankyouText", preferenceManagerImpl.getPreference("sellingInvoicePrint_thankyouText"));
				dic.Add("sellingInvoicePrint_message", preferenceManagerImpl.getPreference("sellingInvoicePrint_message"));
				dic.Add("sellingInvoicePrint_language", preferenceManagerImpl.getPreference("sellingInvoicePrint_language"));

				dic.Add("sellingInvoicePrint_invoiceNumber_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_invoiceNumber_customLanguageText"));
				dic.Add("sellingInvoicePrint_creditInvoice_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_creditInvoice_customLanguageText"));
				dic.Add("sellingInvoicePrint_printedPrice_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_printedPrice_customLanguageText"));
				dic.Add("sellingInvoicePrint_ourPrice_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_ourPrice_customLanguageText"));
				dic.Add("sellingInvoicePrint_discount_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_discount_customLanguageText"));
				dic.Add("sellingInvoicePrint_quantity_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_quantity_customLanguageText"));
				dic.Add("sellingInvoicePrint_lineTotal_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_lineTotal_customLanguageText"));
				dic.Add("sellingInvoicePrint_subTotal_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_subTotal_customLanguageText"));
				dic.Add("sellingInvoicePrint_billDiscount_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_billDiscount_customLanguageText"));
				dic.Add("sellingInvoicePrint_totalReturn_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_totalReturn_customLanguageText"));
				dic.Add("sellingInvoicePrint_netTotal_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_netTotal_customLanguageText"));
				dic.Add("sellingInvoicePrint_givenMoney_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_givenMoney_customLanguageText"));
				dic.Add("sellingInvoicePrint_balance_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_balance_customLanguageText"));
				dic.Add("sellingInvoicePrint_totalDiscount_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_totalDiscount_customLanguageText"));
				dic.Add("sellingInvoicePrint_userName_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_userName_customLanguageText"));
				dic.Add("sellingInvoicePrint_customerName_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_customerName_customLanguageText"));
				dic.Add("sellingInvoicePrint_totalPayment_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_totalPayment_customLanguageText"));
				dic.Add("sellingInvoicePrint_numberOfItems_customLanguageText", preferenceManagerImpl.getPreference("sellingInvoicePrint_numberOfItems_customLanguageText"));
			} catch ( Exception ) {
			}
		}

		private void UserControl_Loaded( object sender, RoutedEventArgs e ) {
			try {
				// a or p
				if ( dic["sellingInvoicePrint_displayLineDiscount"].Value == "p" ) {
					radioButton_percentage.IsChecked = true;
				} else {
					radioButton_amount.IsChecked = true;
				}
				// o or d
				if ( dic["sellingInvoicePrint_showDiscountOrOurPrice"].Value == "o" ) {
					radioButton_ourPrice_showingLineDiscount.IsChecked = true;
				} else {
					radioButton_discount_showingLineDiscount.IsChecked = true;
				}
				textBox_numberOfCopies.Text = dic["sellingInvoicePrint_numberOfCopiesOfCreditBill"].Value;
				textBox_message_thankYouText.Text = dic["sellingInvoicePrint_thankyouText"].Value;
				textBox_message_footerMessage.Text = dic["sellingInvoicePrint_message"].Value;
				// d or c
				if ( dic["sellingInvoicePrint_language"].Value == "d" ) {
					radioButton_default_language.IsChecked = true;
				} else {
					radioButton_custom_language.IsChecked = true;
				}

				textBox_invoiceNumber_customLanguageText.Text = dic["sellingInvoicePrint_invoiceNumber_customLanguageText"].Value;
				textBox_creditInvoice_customLanguageText.Text = dic["sellingInvoicePrint_creditInvoice_customLanguageText"].Value;
				textBox_printedPrice_customLanguageText.Text = dic["sellingInvoicePrint_printedPrice_customLanguageText"].Value;
				textBox_ourPrice_customLanguageText.Text = dic["sellingInvoicePrint_ourPrice_customLanguageText"].Value;
				textBox_discount_customLanguageText.Text = dic["sellingInvoicePrint_discount_customLanguageText"].Value;
				textBox_quantity_customLanguageText.Text = dic["sellingInvoicePrint_quantity_customLanguageText"].Value;
				textBox_lineTotal_customLanguageText.Text = dic["sellingInvoicePrint_lineTotal_customLanguageText"].Value;
				textBox_subTotal_customLanguageText.Text = dic["sellingInvoicePrint_subTotal_customLanguageText"].Value;
				textBox_billDiscount_customLanguageText.Text = dic["sellingInvoicePrint_billDiscount_customLanguageText"].Value;
				textBox_totalReturn_customLanguageText.Text = dic["sellingInvoicePrint_totalReturn_customLanguageText"].Value;
				textBox_netTotal_customLanguageText.Text = dic["sellingInvoicePrint_netTotal_customLanguageText"].Value;
				textBox_moneyGiven_customLanguageText.Text = dic["sellingInvoicePrint_givenMoney_customLanguageText"].Value;
				textBox_balance_customLanguageText.Text = dic["sellingInvoicePrint_balance_customLanguageText"].Value;
				textBox_totalDiscount_customLanguageText.Text = dic["sellingInvoicePrint_totalDiscount_customLanguageText"].Value;
				textBox_userName_customLanguageText.Text = dic["sellingInvoicePrint_userName_customLanguageText"].Value;
				textBox_customerName_customLanguageText.Text = dic["sellingInvoicePrint_customerName_customLanguageText"].Value;
				textBox_totalPayment_customLanguageText.Text = dic["sellingInvoicePrint_totalPayment_customLanguageText"].Value;
				textBox_numberOfItems_customLanguageText.Text = dic["sellingInvoicePrint_numberOfItems_customLanguageText"].Value;

				languageClicked();
			} catch ( Exception ) {
			}
		}

		private bool isChanged( String key, String value ) {
			bool b = false;
			try {
				if ( dic[key].Value != value ) {
					b = true;
				}
			} catch ( Exception ) {
			}
			return b;
		}

		private void button_save_Click( object sender, RoutedEventArgs e ) {
			try {				
				if ( isChanged("sellingInvoicePrint_displayLineDiscount", ( radioButton_percentage.IsChecked == true ? "p" : "a" )) ) {
					dic["sellingInvoicePrint_displayLineDiscount"].Value = radioButton_percentage.IsChecked == true ? "p" : "a";
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_displayLineDiscount"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_displayLineDiscount"]);
				}
				if ( isChanged("sellingInvoicePrint_showDiscountOrOurPrice", ( radioButton_ourPrice_showingLineDiscount.IsChecked == true ? "o" : "d" )) ) {
					dic["sellingInvoicePrint_showDiscountOrOurPrice"].Value = radioButton_ourPrice_showingLineDiscount.IsChecked == true ? "o" : "d";
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_showDiscountOrOurPrice"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_showDiscountOrOurPrice"]);
				}
				if ( isChanged("sellingInvoicePrint_numberOfCopiesOfCreditBill", textBox_numberOfCopies.Text) ) {
					dic["sellingInvoicePrint_numberOfCopiesOfCreditBill"].Value = textBox_numberOfCopies.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_numberOfCopiesOfCreditBill"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_numberOfCopiesOfCreditBill"]);
				}
				if ( isChanged("sellingInvoicePrint_thankyouText", textBox_message_thankYouText.Text) ) {
					dic["sellingInvoicePrint_thankyouText"].Value = textBox_message_thankYouText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_thankyouText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_thankyouText"]);
				}
				if ( isChanged("sellingInvoicePrint_message", textBox_message_footerMessage.Text) ) {
					dic["sellingInvoicePrint_message"].Value = textBox_message_footerMessage.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_message"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_message"]);
				}
				if ( isChanged("sellingInvoicePrint_language", ( radioButton_default_language.IsChecked == true ? "d" : "c" )) ) {
					dic["sellingInvoicePrint_language"].Value = radioButton_default_language.IsChecked == true ? "d" : "c";
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_language"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_language"]);
				}

				///

				if ( isChanged("sellingInvoicePrint_invoiceNumber_customLanguageText", textBox_invoiceNumber_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_invoiceNumber_customLanguageText"].Value = textBox_invoiceNumber_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_invoiceNumber_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_invoiceNumber_customLanguageText"]);
				}
				if ( isChanged("sellingInvoicePrint_creditInvoice_customLanguageText", textBox_creditInvoice_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_creditInvoice_customLanguageText"].Value = textBox_creditInvoice_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_creditInvoice_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_creditInvoice_customLanguageText"]);
				}
				if ( isChanged("sellingInvoicePrint_printedPrice_customLanguageText", textBox_printedPrice_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_printedPrice_customLanguageText"].Value = textBox_printedPrice_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_printedPrice_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_printedPrice_customLanguageText"]);
				}
				if ( isChanged("sellingInvoicePrint_ourPrice_customLanguageText", textBox_ourPrice_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_ourPrice_customLanguageText"].Value = textBox_ourPrice_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_ourPrice_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_ourPrice_customLanguageText"]);
				}
				if ( isChanged("sellingInvoicePrint_discount_customLanguageText", textBox_discount_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_discount_customLanguageText"].Value = textBox_discount_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_discount_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_discount_customLanguageText"]);
				}
				if ( isChanged("sellingInvoicePrint_quantity_customLanguageText", textBox_quantity_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_quantity_customLanguageText"].Value = textBox_quantity_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_quantity_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_quantity_customLanguageText"]);
				}
				if ( isChanged("sellingInvoicePrint_lineTotal_customLanguageText", textBox_lineTotal_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_lineTotal_customLanguageText"].Value = textBox_lineTotal_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_lineTotal_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_lineTotal_customLanguageText"]);
				}
				if ( isChanged("sellingInvoicePrint_subTotal_customLanguageText", textBox_subTotal_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_subTotal_customLanguageText"].Value = textBox_subTotal_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_subTotal_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_subTotal_customLanguageText"]);
				}
				if ( isChanged("sellingInvoicePrint_billDiscount_customLanguageText", textBox_billDiscount_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_billDiscount_customLanguageText"].Value = textBox_billDiscount_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_billDiscount_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_billDiscount_customLanguageText"]);
				}
				if ( isChanged("sellingInvoicePrint_totalReturn_customLanguageText", textBox_totalReturn_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_totalReturn_customLanguageText"].Value = textBox_totalReturn_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_totalReturn_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_totalReturn_customLanguageText"]);
				}
				if ( isChanged("sellingInvoicePrint_netTotal_customLanguageText", textBox_netTotal_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_netTotal_customLanguageText"].Value = textBox_netTotal_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_netTotal_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_netTotal_customLanguageText"]);
				}
				if ( isChanged("sellingInvoicePrint_givenMoney_customLanguageText", textBox_moneyGiven_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_givenMoney_customLanguageText"].Value = textBox_moneyGiven_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_givenMoney_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_givenMoney_customLanguageText"]);
				}
				if ( isChanged("sellingInvoicePrint_balance_customLanguageText", textBox_balance_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_balance_customLanguageText"].Value = textBox_balance_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_balance_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_balance_customLanguageText"]);
				}
				if ( isChanged("sellingInvoicePrint_totalDiscount_customLanguageText", textBox_totalDiscount_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_totalDiscount_customLanguageText"].Value = textBox_totalDiscount_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_totalDiscount_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_totalDiscount_customLanguageText"]);
				}
				if ( isChanged("sellingInvoicePrint_userName_customLanguageText", textBox_userName_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_userName_customLanguageText"].Value = textBox_userName_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_userName_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_userName_customLanguageText"]);
				}
				if ( isChanged("sellingInvoicePrint_customerName_customLanguageText", textBox_customerName_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_customerName_customLanguageText"].Value = textBox_customerName_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_customerName_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_customerName_customLanguageText"]);
				}
				if ( isChanged("sellingInvoicePrint_totalPayment_customLanguageText", textBox_totalPayment_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_totalPayment_customLanguageText"].Value = textBox_totalPayment_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_totalPayment_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_totalPayment_customLanguageText"]);
				}
				if ( isChanged("sellingInvoicePrint_numberOfItems_customLanguageText", textBox_numberOfItems_customLanguageText.Text) ) {
					dic["sellingInvoicePrint_numberOfItems_customLanguageText"].Value = textBox_numberOfItems_customLanguageText.Text;
					CommonMethods.setCDMDForUpdate(dic["sellingInvoicePrint_numberOfItems_customLanguageText"]);
					preferenceManagerImpl.upd(dic["sellingInvoicePrint_numberOfItems_customLanguageText"]);
				}

				ShowMessage.success(Common.Messages.Success.Success004);
			} catch ( Exception ) {
			}
		}

		private void languageClicked() {
			try {
				if ( radioButton_default_language .IsChecked == true) {
					groupBox_customLanguageText.IsEnabled = false;
				} else{
					groupBox_customLanguageText.IsEnabled = true;
				}
			} catch ( Exception ) {
			}
		}

		private void radioButton_default_language_Click( object sender, RoutedEventArgs e ) {
			languageClicked();
		}

		private void radioButton_custom_language_Click( object sender, RoutedEventArgs e ) {
			languageClicked();
		}
	}
}
