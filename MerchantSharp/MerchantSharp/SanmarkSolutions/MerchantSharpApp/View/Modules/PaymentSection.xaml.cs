using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules {
	/// <summary>
	/// Interaction logic for PaymentSection.xaml
	/// </summary>
	public partial class PaymentSection : UserControl {

		private PaymentManagerControler paymentManagerControler = null;

		private bool isLoadedUI = false;
		public bool IsLoadedUI {
			get { return isLoadedUI; }
			set { isLoadedUI = value; }
		}

		private int invoiceId = 0;
		public int InvoiceId {
			get { return invoiceId; }
			set { invoiceId = value; paymentManagerControler.activeElements(); }
		}

		private String type = null;
		public String Type {
			get { return type; }
			set { type = value; }
		}

		private DataTable dataTableCashPayments = null;
		public DataTable DataTableCashPayments {
			get { return dataTableCashPayments; }
			set { dataTableCashPayments = value; }
		}

		private DataTable dataTableChequePayments = null;
		public DataTable DataTableChequePayments {
			get { return dataTableChequePayments; }
			set { dataTableChequePayments = value; }
		}

		private DataTable dataTableOtherPayments = null;
		public DataTable DataTableOtherPayments {
			get { return dataTableOtherPayments; }
			set { dataTableOtherPayments = value; }
		}

		public PaymentSection(String type) {
			InitializeComponent();
			this.type = type;			
			paymentManagerControler = new PaymentManagerControler(this);
			InvoiceId = 0;
		}
		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			paymentManagerControler.UserControl_Loaded();
		}

		private void button_add_cashPayments_Click(object sender, RoutedEventArgs e) {
			paymentManagerControler.button_add_cashPayments_Click();
		}

		private void textBox_amount_cashPayments_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				paymentManagerControler.button_add_cashPayments_Click();
			}
		}

		private void button_delete_cashPayments_Click(object sender, RoutedEventArgs e) {
			paymentManagerControler.button_delete_cashPayments_Click();
		}

		private void dataGrid_cashPayments_cashPayments_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Delete) {
				paymentManagerControler.button_delete_cashPayments_Click();
			}
		}

		private void button_add_chequePayments_Click(object sender, RoutedEventArgs e) {
			paymentManagerControler.button_add_chequePayments_Click();
		}

		private void textBox_amount_chequePayments_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				paymentManagerControler.button_add_chequePayments_Click();
			}
		}

		private void textBox_chequeNumber_chequePayments_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				paymentManagerControler.button_add_chequePayments_Click();
			}
		}

		private void comboBox_bank_chequePayments_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				paymentManagerControler.button_add_chequePayments_Click();
			}
		}

		private void dataGrid_chequePayments_chequePayments_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Delete) {
				paymentManagerControler.button_delete_chequePayments_Click();
			}
		}

		private void button_delete_chequePayments_Click(object sender, RoutedEventArgs e) {
			paymentManagerControler.button_delete_chequePayments_Click();
		}

		private void textBox_amount_vendorAccountSettlement_TextChanged(object sender, TextChangedEventArgs e) {
			try {
				if(textBox_amount_vendorAccountSettlement.DoubleValue > Convert.ToDouble(label_balance_vendorAccountSettlement.Content)) {
					TextChange textChange = e.Changes.ElementAt<TextChange>(0);
					int iAddedLength = textChange.AddedLength;
					int iOffset = textChange.Offset;
					textBox_amount_vendorAccountSettlement.Text = textBox_amount_vendorAccountSettlement.Text.Remove(iOffset, iAddedLength);
					textBox_amount_vendorAccountSettlement.SelectionStart = textBox_amount_vendorAccountSettlement.Text.Length;
				}
			} catch(Exception) {
			}
		}

		private void button_saveAccountChange_Click(object sender, RoutedEventArgs e) {
			paymentManagerControler.button_saveAccountChange_Click();
		}

		private void button_add_otherPayments_Click(object sender, RoutedEventArgs e) {
			paymentManagerControler.button_add_otherPayments_Click();
		}

		private void textBox_amount_otherPayments_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				paymentManagerControler.button_add_otherPayments_Click();
			}
		}

		private void textBox_notes_otherPayments_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				paymentManagerControler.button_add_otherPayments_Click();
			}
		}

		private void button_delete_otherPayments_Click(object sender, RoutedEventArgs e) {
			paymentManagerControler.button_delete_otherPayments_Click();
		}

		private void dataGrid_otherPayments_otherPayments_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Delete) {
				paymentManagerControler.button_delete_otherPayments_Click();
			}
		}


		public void resetAllElements() {
			paymentManagerControler.resetAllElements();
		}
				
	}
}
