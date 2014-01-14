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
	/// Interaction logic for CompanyManager.xaml
	/// </summary>
	public partial class CompanyManager : UserControl, IFilter {

		private CompanyManagerControler companyManagerControler = null;

		private bool isLoadedUI = false;
		public bool IsLoadedUI {
			get { return isLoadedUI; }
			set { isLoadedUI = value; }
		}

		private Pagination pagination = null;
		public Pagination Pagination {
			get { return pagination; }
			set { pagination = value; }
		}

		private DataTable dataTable = null;
		public DataTable DataTable {
			get { return dataTable; }
			set { dataTable = value; }
		}

		private Company selectedCompany = null;
		internal Company SelectedCompany {
			get { return selectedCompany; }
			set { selectedCompany = value; }
		}
		

		private bool isUpdateMode = false;
		public bool IsUpdateMode {
			get { return isUpdateMode; }
			set { isUpdateMode = value; }
		}

		public CompanyManager() {
			InitializeComponent();
			companyManagerControler = new CompanyManagerControler(this);
		}

		public void filter() {
			companyManagerControler.filter();
		}

		public void setPagination() {
			companyManagerControler.setRowsCount();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			companyManagerControler.UserControl_Loaded();
		}

		private void button_filter_Click(object sender, RoutedEventArgs e) {
			companyManagerControler.setRowsCount();
		}

		private void button_save_addCompany_Click(object sender, RoutedEventArgs e) {
			companyManagerControler.button_save_addCompany_Click();
		}

		private void button_reset_addCompany_Click(object sender, RoutedEventArgs e) {
			companyManagerControler.button_reset_addCompany_Click();
		}

		private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			companyManagerControler.dataGrid_MouseDoubleClick();
		}

		private void textBox_name_addCompany_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				companyManagerControler.button_save_addCompany_Click();
			}
		}
	}
}
