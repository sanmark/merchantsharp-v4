using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Settings.PreferenceViews {
	/// <summary>
	/// Interaction logic for StockPreference.xaml
	/// </summary>
	public partial class ManageStock : UserControl, IPreferences {

		private StockManagerImpl stockManagerImpl = null;

		public ManageStock() {
			InitializeComponent();
			stockManagerImpl = new StockManagerImpl();
		}

		private DataTable dataTable = null;
		private bool isUpdateMode = false;
		private StockLocation selectedStockLocation = null;

		public void setImpl(object obj) {
			
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			try {
				dataTable = new DataTable();
				dataTable.Columns.Add("ID", typeof(int));
				dataTable.Columns.Add("Name", typeof(String));
				dataTable.Columns.Add("Is Active", typeof(String));
				dataGrid_stockLocations.DataContext = dataTable.DefaultView;
				dataGrid_stockLocations.Columns[1].Width = 100;

				UIComboBox.yesNoForAdd(comboBox_status);

				filter();
			} catch(Exception) {
			}
		}

		private void filter() {
			try {				
				List<StockLocation> list = stockManagerImpl.getStockLocations();
				dataTable.Rows.Clear();
				foreach(StockLocation stockLocation in list) {
					dataTable.Rows.Add(stockLocation.Id, stockLocation.Name, CommonMethods.getYesNo(stockLocation.Status));
				}
			} catch(Exception) {
			}
		}

		private bool isValidForm() {
			bool b = true;
			try {
				if(Convert.ToInt32(comboBox_status.SelectedValue) < 0) {
					comboBox_status.ErrorMode(true);
					b = false;
				}
				if(textBox_name.IsNull()) {
					textBox_name.ErrorMode(true);
					b = false;
				}
			} catch(Exception) {
			}
			return b;
		}

		private bool add() {
			bool b = false;
			try {
				if(isValidForm()) {
					StockLocation stockLocation = new StockLocation();
					stockLocation.Name = textBox_name.TrimedText;
					stockLocation.Status = Convert.ToInt32(comboBox_status.SelectedValue);
					CommonMethods.setCDMDForAdd(stockLocation);
					if(stockManagerImpl.addStockLocation(stockLocation) > 0) {
						b = true;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		private bool update() {
			bool b = false;
			try {
				if(isValidForm()) {
					StockLocation stockLocation = selectedStockLocation;
					stockLocation.Name = textBox_name.TrimedText;
					stockLocation.Status = Convert.ToInt32(comboBox_status.SelectedValue);
					CommonMethods.setCDMDForUpdate(stockLocation);
					stockManagerImpl.updStockLocation(stockLocation);
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}

		private void resetAddForm() {
			try {
				isUpdateMode = false;
				selectedStockLocation = null;
				comboBox_status.SelectedIndex = -1;
				textBox_name.Clear();
				button_save.Content = "Save";
			} catch(Exception) {
			}
		}
		
		private void button_save_Click(object sender, RoutedEventArgs e) {
			try {
				if(isUpdateMode && update()) {
					filter();
					resetAddForm();
					ShowMessage.success(Common.Messages.Success.Success004);
				} else if(add()) {
					filter();
					resetAddForm();
					ShowMessage.success(Common.Messages.Success.Success002);
				}
			} catch(Exception) {
			}
		}

		private void switchToUpdateMode() {
			try {
				isUpdateMode = true;
				int id = dataGrid_stockLocations.SelectedItemID;
				StockLocation stockLocation = stockManagerImpl.getStockLocationById(id);
				selectedStockLocation = stockLocation;
				comboBox_status.SelectedValue = stockLocation.Status;
				textBox_name.Text = stockLocation.Name;
				button_save.Content = "Update";
			} catch(Exception) {
			}
		}

		private void dataGrid_stockLocations_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
			try {
				if(dataGrid_stockLocations.SelectedItemID > 0) {
					switchToUpdateMode();
				}
			} catch(Exception) {
			}
		}
		
	}
}
