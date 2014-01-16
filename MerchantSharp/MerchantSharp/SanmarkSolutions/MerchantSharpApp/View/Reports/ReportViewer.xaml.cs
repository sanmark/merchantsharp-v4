using Microsoft.Reporting.WinForms;
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
using System.Windows.Shapes;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Reports {
	/// <summary>
	/// Interaction logic for ReportViewer.xaml
	/// </summary>
	public partial class ReportViewer : Window {

		DataTable dataTable;
		string type;
		Dictionary<String, String> keysAndValues;

		public ReportViewer(DataTable dataTable, string type, Dictionary<String, String> keysAndValues) {
			InitializeComponent();
			this.dataTable = dataTable;
			this.type = type;
			this.keysAndValues = keysAndValues;
		}

		public void load() {
			ReportDataSource theReportDataSource = new ReportDataSource();
			theReportDataSource.Name = "theDataSet";

			theReportDataSource.Value = dataTable;
			this.rvReport.LocalReport.DataSources.Add(theReportDataSource);

			this.rvReport.LocalReport.ReportEmbeddedResource = "MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.RDLC." + type + ".rdlc";

			String[] keysArray = keysAndValues.Keys.ToArray();
			ReportParameter[] reportParameterArray = new ReportParameter[keysArray.Length];

			for(int i = 0; i < keysArray.Length; i++) {
				reportParameterArray[i] = new ReportParameter(keysArray[i], keysAndValues[keysArray[i]]);
			}

			this.rvReport.LocalReport.SetParameters(reportParameterArray);
			this.rvReport.RefreshReport();
		}

		private void rvReport_Load(object sender, EventArgs e) {
			load();
		}
	}
}
