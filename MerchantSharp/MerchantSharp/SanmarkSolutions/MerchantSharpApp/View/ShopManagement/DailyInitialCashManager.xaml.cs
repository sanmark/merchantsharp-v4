using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement {
	/// <summary>
	/// Interaction logic for DailyCashManager.xaml
	/// </summary>
	public partial class DailyInitialCashManager : UserControl {

		private DailyInitialCashManagerControler dailyInitialCashManagerControler = null;

		private bool isLoadedUI = false;
		public bool IsLoadedUI {
			get { return isLoadedUI; }
			set { isLoadedUI = value; }
		}

		public DailyInitialCashManager() {
			InitializeComponent();
			dailyInitialCashManagerControler = new DailyInitialCashManagerControler(this);
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			dailyInitialCashManagerControler.UserControl_Loaded();
		}

		private void textBox_otherWidthraw_TextChanged(object sender, TextChangedEventArgs e) {
			dailyInitialCashManagerControler.textBox_otherWidthraw_TextChanged();
		}

		private void button_saveInitialCashAmount_Click(object sender, RoutedEventArgs e) {
			dailyInitialCashManagerControler.button_saveInitialCashAmount_Click();
		}
	}
}
