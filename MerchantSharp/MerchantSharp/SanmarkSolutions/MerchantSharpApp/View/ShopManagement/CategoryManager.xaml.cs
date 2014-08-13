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
	/// Interaction logic for CategoryManager.xaml
	/// </summary>
	public partial class CategoryManager : UserControl, IFilter {

		private CategoryManagerControler categoryManagerControler = null;

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

		private Category selectedCategory = null;
		internal Category SelectedCategory {
			get { return selectedCategory; }
			set { selectedCategory = value; }
		}		

		private bool isUpdateMode = false;
		public bool IsUpdateMode {
			get { return isUpdateMode; }
			set { isUpdateMode = value; }
		}

		public CategoryManager() {
			InitializeComponent();
			categoryManagerControler = new CategoryManagerControler(this);
		}

		public void filter() {
			categoryManagerControler.filter();
		}

		public void setPagination() {
			categoryManagerControler.setRowsCount();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			categoryManagerControler.UserControl_Loaded();
		}

		private void button_filter_Click(object sender, RoutedEventArgs e) {
			categoryManagerControler.setRowsCount();
		}

		private void button_save_addCategory_Click(object sender, RoutedEventArgs e) {
			categoryManagerControler.button_save_addCategory_Click();
		}

		private void button_reset_addCategory_Click(object sender, RoutedEventArgs e) {
			categoryManagerControler.button_reset_addCategory_Click();
		}

		private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			categoryManagerControler.dataGrid_MouseDoubleClick();
		}

		private void textBox_name_addCategory_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				categoryManagerControler.button_save_addCategory_Click();
			}
		}

	}
}
