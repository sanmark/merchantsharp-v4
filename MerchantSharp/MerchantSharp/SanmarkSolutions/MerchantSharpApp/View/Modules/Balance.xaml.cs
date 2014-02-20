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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules {
	/// <summary>
	/// Interaction logic for Balance.xaml
	/// </summary>
	public partial class Balance : Window {

		DispatcherTimer dispatcherTimer = null;
		int count = 10;

		public Balance( String balance ) {
			InitializeComponent();
			try {

				label_balance.Content = balance;

				dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
				dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
				dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

			} catch ( Exception ) {
			}
		}

		private void dispatcherTimer_Tick( object sender, EventArgs e ) {
			try {
				count--;
				if ( count < 1 ) {
					this.Close();
				}
			} catch ( Exception ) {
			}
		}

		private void Window_Loaded( object sender, RoutedEventArgs e ) {
			try {
				dispatcherTimer.Start();
			} catch ( Exception ) {
			}
		}
	}
}
