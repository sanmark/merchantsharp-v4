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
	/// Interaction logic for ErrorMessage.xaml
	/// </summary>
	public partial class SuccessMessage : Window {

		//private String message = null;
		private DispatcherTimer timer = null;
		private int count = 0;
		private DispatcherTimer toUp = null;
		private int topCount = 0;

		public SuccessMessage(String msg) {
			InitializeComponent();
			try {
				label.Content = msg;
				timer = new DispatcherTimer();
				timer.Tick += new EventHandler(dispatcherTimer_Tick);
				timer.Interval = new TimeSpan(0, 0, 1);

				topCount = (Convert.ToInt32(this.Height) / 3);
				toUp = new DispatcherTimer();
				toUp.Tick += new EventHandler(toUp_tick);
				toUp.Interval = new TimeSpan(0, 0, 0, 0, 1);
			} catch(Exception) {
			}
		}

		private void toUp_tick(object sender, EventArgs e) {
			try {
				if(topCount >= 0) {
					this.Top -= 3;
				} else {
					toUp.Stop();
				}
				topCount--;
			} catch(Exception) {
			}
		}

		private void dispatcherTimer_Tick(object sender, EventArgs e) {
			try {

				if(count > 4) {
					timer.Stop();
					this.Close();
				}
				count++;
			} catch(Exception) {
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			try {
				this.Left = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Right - this.Width;
				this.Top = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Bottom /*- this.Height*/;
				//this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
				timer.Start();
				toUp.Start();
			} catch(Exception) {
			}
		}
	}
}
