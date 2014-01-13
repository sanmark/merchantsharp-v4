using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement {
	/// <summary>
	/// Interaction logic for ExpenseManager.xaml
	/// </summary>
	public partial class ExpenseManager : UserControl, IFilter {

		private ExpenseManagerControler expenseManagerControler = null;

		private bool isLoadedUI = false;
		public bool IsLoadedUI {
			get { return isLoadedUI; }
			set { isLoadedUI = value; }
		}

		private DataTable dataTable = null;
		public DataTable DataTable {
			get { return dataTable; }
			set { dataTable = value; }
		}

		private Pagination pagination = null;
		public Pagination Pagination {
			get { return pagination; }
			set { pagination = value; }
		}

		private bool isUpdateMode = false;
		public bool IsUpdateMode {
			get { return isUpdateMode; }
			set { isUpdateMode = value; }
		}

		private Expense selectedExpense = null;
		internal Expense SelectedExpense {
			get { return selectedExpense; }
			set { selectedExpense = value; }
		}

		public ExpenseManager() {
			InitializeComponent();
			expenseManagerControler = new ExpenseManagerControler(this);
		}

		public void filter() {
			expenseManagerControler.filter();
		}

		public void setPagination() {
			expenseManagerControler.setRowsCount();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			expenseManagerControler.UserControl_Loaded();
		}

		private void button_save_addExpense_Click(object sender, RoutedEventArgs e) {
			expenseManagerControler.button_save_addExpense_Click();
		}

		private void button_reset_addExpense_Click(object sender, RoutedEventArgs e) {
			expenseManagerControler.button_reset_addExpense_Click();
		}

		private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			expenseManagerControler.dataGrid_MouseDoubleClick();			
		}

		private void textBox_date_addExpense_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				expenseManagerControler.button_save_addExpense_Click();
			}
		}

		private void textBox_amount_addExpense_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				expenseManagerControler.button_save_addExpense_Click();
			}
		}

		private void textBox_receiver_addExpense_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				expenseManagerControler.button_save_addExpense_Click();
			}
		}

		private void button_filter_Click(object sender, RoutedEventArgs e) {
			setPagination();
		}
	}
}
