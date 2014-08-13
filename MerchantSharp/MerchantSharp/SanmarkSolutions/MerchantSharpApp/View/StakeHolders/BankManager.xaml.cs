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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.StakeHolders {
	/// <summary>
	/// Interaction logic for BankManager.xaml
	/// </summary>
	public partial class BankManager : UserControl, IFilter {

		private BankManagerControler bankManagerControler = null;

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

		private Bank selectedBank = null;
		internal Bank SelectedBank {
			get { return selectedBank; }
			set { selectedBank = value; }
		}

		public BankManager() {
			InitializeComponent();
			bankManagerControler = new BankManagerControler(this);
		}

		public void filter() {
			bankManagerControler.filter();
		}

		public void setPagination() {
			bankManagerControler.setRowsCount();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			bankManagerControler.UserControl_Loaded();
		}

		private void button_save_add_Click(object sender, RoutedEventArgs e) {
			bankManagerControler.button_save_add_Click();
		}

		private void button_reset_add_Click(object sender, RoutedEventArgs e) {
			bankManagerControler.button_reset_add_Click();
		}

		private void button_filter_Click(object sender, RoutedEventArgs e) {
			bankManagerControler.button_filter_Click();
		}

		private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			bankManagerControler.dataGrid_MouseDoubleClick();
		}

		private void textBox_name_add_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				bankManagerControler.button_save_add_Click();
			}
		}
	}
}
