using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions {
    /// <summary>
    /// Interaction logic for StockByPrice.xaml
    /// </summary>
    public partial class StockByPrice : UserControl, IFilter {

        private StockByPriceController stockByPriceController;

        private bool isLoadedUI = false;
        public bool IsLoadedUI {
            get { return isLoadedUI; }
            set { isLoadedUI = value; }
        }

        private DataTable dataTable;
        public DataTable DataTable {
            get { return dataTable; }
            set { dataTable = value; }
        }

        private DataGridFooter dataGridFooter;
        public DataGridFooter DataGridFooter {
            get { return dataGridFooter; }
            set { dataGridFooter = value; }
        }

        private Pagination pagination;
        public Pagination Pagination {
            get { return pagination; }
            set { pagination = value; }
        }

        private ItemFinder itemFinder;
        public ItemFinder ItemFinder {
            get { return itemFinder; }
            set { itemFinder = value; }
        }

        public StockByPrice() {
            InitializeComponent();
            stockByPriceController = new StockByPriceController(this);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            stockByPriceController.UserControl_Loaded();
        }


        public void filter() {
            stockByPriceController.filter();
        }

        public void setPagination() {
            stockByPriceController.setRowsCount();
        }

        private void textBox_itemId_TextChanged(object sender, TextChangedEventArgs e) {
            stockByPriceController.setRowsCount();
        }
    }
}
