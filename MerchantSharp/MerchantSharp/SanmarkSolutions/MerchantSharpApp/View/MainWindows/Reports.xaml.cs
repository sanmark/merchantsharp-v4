using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Reports;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.MainWindows {
	/// <summary>
	/// Interaction logic for Reports.xaml
	/// </summary>
	public partial class Reports : UserControl {
		public Reports() {
			InitializeComponent();
			loadElementsForPlermission();
		}

		private void loadElementsForPlermission() {
			try {
				/// Disable Request Buying Invoice Section
				if(Session.Permission["canAccessDailySale"] == 0) {
					grid_dailySale.IsEnabled = false;
				}
			} catch(Exception) {
			}
		}

		private void grid_dailySale_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ThreadPool.openTab(new DailySale(), "Daily Sale");
		}
	}
}
