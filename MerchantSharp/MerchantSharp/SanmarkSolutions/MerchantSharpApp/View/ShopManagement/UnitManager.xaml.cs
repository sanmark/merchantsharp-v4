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
	/// Interaction logic for UnitManager.xaml
	/// </summary>
	public partial class UnitManager : UserControl, IFilter {

		private UnitManagerControler unitManagerControler = null;

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

		private Unit selectedUnit = null;
		internal Unit SelectedUnit {
			get { return selectedUnit; }
			set { selectedUnit = value; }
		}

		private bool isUpdateMode = false;
		public bool IsUpdateMode {
			get { return isUpdateMode; }
			set { isUpdateMode = value; }
		}

		public UnitManager() {
			InitializeComponent();
			unitManagerControler = new UnitManagerControler(this);
		}

		public void filter() {
			unitManagerControler.filter();
		}

		public void setPagination() {
			unitManagerControler.setRowsCount();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			unitManagerControler.UserControl_Loaded();
		}

		private void button_filter_Click(object sender, RoutedEventArgs e) {
			unitManagerControler.setRowsCount();
		}

		private void button_save_addUnit_Click(object sender, RoutedEventArgs e) {
			unitManagerControler.button_save_addUnit_Click();
		}

		private void button_reset_addUnit_Click(object sender, RoutedEventArgs e) {
			unitManagerControler.button_reset_addUnit_Click();
		}

		private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			unitManagerControler.dataGrid_MouseDoubleClick();
		}

		private void textBox_name_addUser_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				unitManagerControler.button_save_addUnit_Click();
			}
		}
	}
}
