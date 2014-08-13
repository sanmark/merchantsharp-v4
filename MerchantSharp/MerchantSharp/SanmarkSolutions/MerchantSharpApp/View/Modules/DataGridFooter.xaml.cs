using CustomControls.SanmarkSolutions.WPFCustomControls.MSDataGrid;
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
	/// Interaction logic for DataGridFooter.xaml
	/// </summary>
	public partial class DataGridFooter : UserControl, IFooter {

		private MSDataGrid msDataGrid = null;
		private Dictionary<int, ColumnDefinition> dicColumnDefinitions = null;
		//private Dictionary<int, Label> dicLabels = null;
		System.Windows.Threading.DispatcherTimer dispatcherTimer = null;

		public DataGridFooter() {
			InitializeComponent();
			dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
			dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
			dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);			
		}

		private void dispatcherTimer_Tick(object sender, EventArgs e) {
			try {

				int[] arr = msDataGrid.TotalColumnIndexes.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
				double[] arrTotal = new double[arr.Length];
				//createGridColumns(msDataGrid);

				int columnsCount = msDataGrid.Columns.Count;
				dicColumnDefinitions = new Dictionary<int, ColumnDefinition>();
				grid_footerRow.Children.Clear();
				grid_footerRow.ColumnDefinitions.Clear();
				double width = 0;

				for(int i = 0; i < msDataGrid.Columns.Count; i++) {

					ColumnDefinition cd = new ColumnDefinition();
					//if(msDataGrid.Columns[i].Visibility == System.Windows.Visibility.Visible) {
					cd.Width = new GridLength(Convert.ToDouble(((DataGridColumn)msDataGrid.Columns[i]).Width.DisplayValue), GridUnitType.Star);
					//}
					width += Convert.ToDouble(((DataGridColumn)msDataGrid.Columns[i]).Width.DisplayValue);
					dicColumnDefinitions.Add(i, cd);
					grid_footerRow.ColumnDefinitions.Add(cd);

					if(arr.Contains(i)) {
						Label l = new Label();
						l.VerticalAlignment = System.Windows.VerticalAlignment.Center;
						l.Padding = new Thickness(0, 0, 5, 0);
						l.Content = "0";
						l.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
						/*if(i % 2 == 0) {
							l.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#bbbccc"));
						} else {
							l.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#eeeeee"));
						}*/
						l.SetValue(FontWeightProperty, FontWeights.Bold);
						Grid.SetColumn(l, i);
						try {
							foreach(System.Data.DataRowView row in msDataGrid.Items) {
								try {
									l.Content = (Convert.ToDouble(l.Content) + Convert.ToDouble(row[i])).ToString("#,##0.00");
								} catch(Exception) {
								}
							}
						} catch(Exception) {
						}
						grid_footerRow.Children.Add(l);
					}
				}
				grid_footerRow.Width = width;

				(sender as System.Windows.Threading.DispatcherTimer).Stop();
			} catch(Exception) {
			}
		}

		public void dataContextBinded(MSDataGrid msDataGrid) {
			try {
				this.msDataGrid = msDataGrid;
				if(msDataGrid.TotalColumnIndexes != null) {
					dispatcherTimer.Start();					
				}
			} catch(Exception) {
			}
		}

		private void createGridColumns(MSDataGrid msDataGrid) {
			try {
				//DataGridColumn dgtc = (DataGridColumn)msDataGrid.Columns[4];

				//MessageBox.Show(dgtc.Width.DisplayValue.ToString());
				int columnsCount = msDataGrid.Columns.Count;
				dicColumnDefinitions = new Dictionary<int, ColumnDefinition>();
				grid_footerRow.Children.Clear();
				grid_footerRow.ColumnDefinitions.Clear();
				double width = 0;
				for(int i = 0; i < columnsCount; i++) {
					ColumnDefinition cd = new ColumnDefinition();
					//if(msDataGrid.Columns[i].Visibility == System.Windows.Visibility.Visible) {
						cd.Width = new GridLength(Convert.ToDouble(((DataGridColumn)msDataGrid.Columns[i]).Width.DisplayValue), GridUnitType.Star);
					//}
					width += Convert.ToDouble(((DataGridColumn)msDataGrid.Columns[i]).Width.DisplayValue);
					dicColumnDefinitions.Add(i, cd);
					grid_footerRow.ColumnDefinitions.Add(cd);
				}
				grid_footerRow.Width = width;
			} catch(Exception) {
			}
		}
	}
}
