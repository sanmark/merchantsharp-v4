using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using Microsoft.Win32;
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
	/// Interaction logic for Tools.xaml
	/// </summary>
	public partial class Tools : UserControl {
		public Tools() {
			InitializeComponent();
			loadElementsForPlermission();
		}

		private void loadElementsForPlermission() {
			try {
				if(Session.Permission["canBackupDatabase"] == 0) {
					grid_dbBackup.IsEnabled = false;
				}
			} catch(Exception) {
			}
		}

		private void grid_dbBackup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			try {
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.FileName = "M# " + DateTime.Now.ToString("yyyy-MM-dd HH.mm"); // Default file name
				saveFileDialog.DefaultExt = ".sql"; // Default file extension
				saveFileDialog.Filter = "(.sql)|*.sql"; // Filter files by extension
				if(saveFileDialog.ShowDialog() == true) {
					new MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.DatabaseBackup().backup(saveFileDialog.FileName);
					ShowMessage.success(Common.Messages.Success.Success005);
				}
			} catch(Exception) {
			}
		}
	}
}
