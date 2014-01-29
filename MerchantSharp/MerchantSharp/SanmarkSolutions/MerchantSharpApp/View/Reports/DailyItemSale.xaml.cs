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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Reports {
	/// <summary>
	/// Interaction logic for DailyItemSale.xaml
	/// </summary>
	public partial class DailyItemSale : UserControl, IFilter {

		private ReportManagerControler reportManagerControler = null;
		private MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl.ItemManagerImpl itemManagerImpl = null;

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

		private ItemFinder itemFinder = null;
		public ItemFinder ItemFinder {
			get { return itemFinder; }
			set { itemFinder = value; }
		}
		
		private DataTable dataTable = null;
		public DataTable DataTable {
			get { return dataTable;}
			set { dataTable = value; }
		}

		private DataGridFooter dataGridFooter = null;
		public DataGridFooter DataGridFooter {
			get { return dataGridFooter; }
			set { dataGridFooter = value; }
		}

		public DailyItemSale() {
			InitializeComponent();
			reportManagerControler = new ReportManagerControler(this);
			itemManagerImpl = new Model.Impl.ItemManagerImpl();
		}

		public void filter() {
			reportManagerControler.setDailyItemSaleFilter();
		}

		public void setPagination() {
			reportManagerControler.setDailyItemSaleRowsCount();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			reportManagerControler.dailyItemSale_UserContolLoaded();
		}

		private void textBox_itemId_TextChanged(object sender, TextChangedEventArgs e) {
			try {
				if(String.IsNullOrWhiteSpace(textBox_itemId.Text)) {
					label_selectedItemName.Content = null;
				} else {
					Item item = itemManagerImpl.getItemById(Convert.ToInt32(textBox_itemId.Text));
					label_selectedItemName.Content = item.Name;
				}
			} catch(Exception) {
			}
		}

		private void button_clearItem_Click(object sender, RoutedEventArgs e) {
			try {
				textBox_itemId.Clear();
				textBox_categoryId.Clear();
				textBox_companyId.Clear();
				itemFinder.comboBox_category_selectItem.SelectedIndex = 0;
				itemFinder.comboBox_company_selectItem.SelectedIndex = 0;
				itemFinder.comboBox_item_selectItem.SelectedIndex = 0;
			} catch(Exception) {
			}
		}

		private void button_filter_Click(object sender, RoutedEventArgs e) {
			reportManagerControler.setDailyItemSaleRowsCount();
		}
	}
}
