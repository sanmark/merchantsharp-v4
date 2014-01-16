using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Settings;
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
	/// Interaction logic for Settings.xaml
	/// </summary>
	public partial class Settings : UserControl {
		public Settings() {
			InitializeComponent();
			loadElementsForPlermission();
		}

		private void loadElementsForPlermission() {
			try {
				if(Session.Permission["canAccessPreferences"] == 0) {
					grid_preferences.IsEnabled = false;
				}
			} catch(Exception) {
			}
		}

		private void grid_preferences_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			try {
				Preferences preferences = new Preferences();
				preferences.ShowDialog();
			} catch(Exception) {
			}
		}
	}
}
