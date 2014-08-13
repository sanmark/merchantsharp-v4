using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.ReportMold;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Reports;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Tools {
	/// <summary>
	/// Interaction logic for ChequePrint.xaml
	/// </summary>
	public partial class ChequePrint : UserControl {
		public ChequePrint() {
			InitializeComponent();
		}

		public ChequePrint(string date, string toPay, string amount) {
			InitializeComponent();
			datePicker_date.Text = date;
			textBox_payTo.Text = toPay;
			textBox_amountNumeric.Text = amount;
		}

		private bool isValidForm() {
			bool b = false;
			try {
				if(datePicker_date.SelectedDate == null) {
					datePicker_date.ErrorMode(true);
				} else if(String.IsNullOrWhiteSpace(textBox_payTo.Text)) {
					textBox_payTo.ErrorMode(true);
				} else if(String.IsNullOrWhiteSpace(textBox_amountNumeric.Text)) {
					textBox_amountNumeric.ErrorMode(true);
				} else {
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		private void textBox_amountNumeric_TextChanged(object sender, TextChangedEventArgs e) {
			try {
				textBox_amountText.Text = "*** " + NumbersToWords.decimalNumberToWords(textBox_amountNumeric.Text) + " ***";
			} catch(Exception) {
			}
		}

		private String getChequeDate() {
			String date = "";
			try {
				DateTime dt = (DateTime)datePicker_date.SelectedDate;
				date += (dt.Day < 10) ? "0 " + dt.Day + " " : (dt.Day + "").Insert(1, " ") + " ";
				date += (dt.Month < 10) ? "0 " + dt.Month : (dt.Month + "").Insert(1, " ");
				date += "     " + ((dt.Year + "").Substring(2)).Insert(1, " ");
			} catch(Exception) {
			}
			return date;
		}

		private String getAmountInText() {
			String text = "";
			try {
				text = "      " + textBox_amountText.Text;
				if(text.Length > 40) {
					text = text.Insert(40, "\n\n");
				}
				if(text.Length > 80) {
					text = text.Insert(80, "\n\n");
				}
			} catch(Exception) {
			}
			return text;
		}

		private void button_print_Click(object sender, RoutedEventArgs e) {
			try {
				if(isValidForm()) {
					PrepareReport prepareReport = new PrepareReport();
					prepareReport.addParameter("reportType", "Cheque Print");
					//prepareReport.addParameter("reportDescription", "Test text");
					prepareReport.addReportPeriod(null, null);
					prepareReport.addParameter("date", getChequeDate());
					prepareReport.addParameter("payTo", textBox_payTo.TrimedText);

					prepareReport.addParameter("amountText", getAmountInText());
					prepareReport.addParameter("amountNumeric", textBox_amountNumeric.FormattedValue);
					prepareReport.addParameter("cross", (checkBox_cross.IsChecked == true) ? "//" : "");
					prepareReport.addParameter("accountPayeeOnly", (checkBox_accountPayeeOnly.IsChecked == true) ? "Account Payee Only" : "");
					new ReportViewer(null, "ChequePrint", prepareReport.getParameters()).Show();
				}
			} catch(Exception) {
			}
		}
	}
}
