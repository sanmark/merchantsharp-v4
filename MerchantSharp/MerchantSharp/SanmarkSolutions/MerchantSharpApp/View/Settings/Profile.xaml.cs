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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Settings {
	/// <summary>
	/// Interaction logic for Profile.xaml
	/// </summary>
	public partial class Profile : UserControl {

		private UserManagerControler userManagerControler = null;

		private bool isLoadedUI = false;
		public bool IsLoadedUI {
			get { return isLoadedUI; }
			set { isLoadedUI = value; }
		}

		public Profile() {
			InitializeComponent();
			userManagerControler = new UserManagerControler(this);
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			userManagerControler.Prifile_UserControl_Loaded();
		}

		private void button_save_Click(object sender, RoutedEventArgs e) {
			userManagerControler.updateProfile();
		}
	}
}
