using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules {
	/// <summary>
	/// Interaction logic for Pagination.xaml
	/// </summary>
	public partial class Pagination : UserControl {

		IFilter filter = null;
		internal IFilter Filter {
			get { return filter; }
			set { filter = value; }
		}

		private bool isLoadedUI = false;

		private int limitStart = 0;
		public int LimitStart {
			get { return limitStart; }
		}

		private int limitCount = 0;
		public int LimitCount {
			get { return limitCount; }
		}

		private int rowsCount = 0;
		public int RowsCount {
			get { return rowsCount; }
			set {
				rowsCount = value;
				try {
					setPagination();
				} catch(Exception) {
				}
			}
		}

		public Pagination() {
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			try {
				if(!isLoadedUI) {
					limitCount = textBox_rowsPerPage_pagination.IntValue;
					isLoadedUI = true;
				}
			} catch(Exception) {
			}
		}

		private void setPagination() {
			try {
				limitStart = 0;
				int pageCount = rowsCount / Convert.ToInt32(textBox_rowsPerPage_pagination.IntValue);
				if(rowsCount % Convert.ToInt32(textBox_rowsPerPage_pagination.Text) > 0) {
					pageCount++;
				} else if(pageCount == 0) {
					pageCount++;
				}
				if(textBox_pageNumber_pagination.IntValue > pageCount) {
					textBox_pageNumber_pagination.IntValue = pageCount;
				}
				label_pageNumbers_pagination.Content = pageCount.ToString();
				limitStart = (textBox_pageNumber_pagination.IntValue * textBox_rowsPerPage_pagination.IntValue) - textBox_rowsPerPage_pagination.IntValue;
				limitCount = textBox_rowsPerPage_pagination.IntValue;
				filter.filter();
			} catch(Exception) {
			}
		}

		private void textBox_pageNumber_pagination_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				filter.setPagination();
				textBox_pageNumber_pagination.SelectAll();
			}
		}

		private void textBox_rowsPerPage_pagination_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter) {
				filter.setPagination();
				textBox_rowsPerPage_pagination.SelectAll();
			}
		}

		private void textBox_pageNumber_pagination_TextChanged(object sender, TextChangedEventArgs e) {
			try {

			} catch(Exception) {
			}
		}

		private void textBox_rowsPerPage_pagination_TextChanged(object sender, TextChangedEventArgs e) {
			try {
				limitCount = textBox_rowsPerPage_pagination.IntValue;
			} catch(Exception) {
			}
		}

		private void button_first_pagination_Click(object sender, RoutedEventArgs e) {
			try {
				if(textBox_pageNumber_pagination.Text != "1") {
					textBox_pageNumber_pagination.Text = "1";
					setPagination();
				}
			} catch(Exception) {
			}
		}

		private void button_back_pagination_Click(object sender, RoutedEventArgs e) {
			try {
				if(textBox_pageNumber_pagination.IntValue > 1) {
					textBox_pageNumber_pagination.IntValue--;
					setPagination();
				}
			} catch(Exception) {
			}
		}

		private void button_next_pagination_Click(object sender, RoutedEventArgs e) {
			try {
				if(textBox_pageNumber_pagination.IntValue < Convert.ToInt32(label_pageNumbers_pagination.Content)) {
					textBox_pageNumber_pagination.IntValue++;
					setPagination();
				}
			} catch(Exception) {
			}
		}

		private void button_last_pagination_Click(object sender, RoutedEventArgs e) {
			try {
				if(textBox_pageNumber_pagination.Text != label_pageNumbers_pagination.Content.ToString()) {
					textBox_pageNumber_pagination.Text = label_pageNumbers_pagination.Content.ToString();
					setPagination();
				}
			} catch(Exception) {
			}
		}
	}
}
