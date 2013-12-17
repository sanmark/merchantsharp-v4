using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.ReportMold {
	class ReportPrinter {

		#region Reporting
		private int m_currentPageIndex;
		private IList<Stream> m_streams;

		private Stream CreateStream(string name, string fileNameExtension, Encoding encoding,
			string mimeType, bool willSeek) {
			Stream stream = new MemoryStream();
			m_streams.Add(stream);
			return stream;
		}

		private void Export(LocalReport report) {
			string deviceInfo =
                "<DeviceInfo>" +
				  "  <OutputFormat>EMF</OutputFormat>" +
				  "  <PageWidth>8.5in</PageWidth>" +
				  "  <PageHeight>11in</PageHeight>" +
				  "  <MarginTop>0.25in</MarginTop>" +
				  "  <MarginLeft>0.25in</MarginLeft>" +
				  "  <MarginRight>0.25in</MarginRight>" +
				  "  <MarginBottom>0.25in</MarginBottom>" +
				  "</DeviceInfo>";
			Warning[] warnings;
			m_streams = new List<Stream>();
			report.Render("Image", deviceInfo, CreateStream, out warnings);
			foreach(Stream stream in m_streams)
				stream.Position = 0;
		}

		private void ReportPrint() {
			if(m_streams == null || m_streams.Count == 0)
				throw new Exception("Error: no stream to print.");
			PrintDocument printDoc = new PrintDocument();
			//printDoc.PrinterSettings.PrinterName = EDI_PRINTER;
			printDoc.PrinterSettings.PrinterName = new PrinterSettings().PrinterName;
			//PaperSize paperSize = new PaperSize("MyCustomSize", 480, 100); //numbers are optional
			//paperSize.RawKind = (int)PaperKind.Custom;
			//printDoc.DefaultPageSettings.PaperSize = paperSize;
			if(!printDoc.PrinterSettings.IsValid) {
				throw new Exception("Error: cannot find printer.");
			} else {
				printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
				m_currentPageIndex = 0;
				printDoc.Print();
			}

		}

		private void printDoc_PrintPage(object sender, PrintPageEventArgs ev) {
			Metafile pageImage = new
   Metafile(m_streams[m_currentPageIndex]);

			// Adjust rectangular area with printer margins.
			Rectangle adjustedRect = new Rectangle(
				ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
				ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
				ev.PageBounds.Width,
				ev.PageBounds.Height);

			// Draw a white background for the report
			ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

			// Draw the report content
			ev.Graphics.DrawImage(pageImage, adjustedRect);

			// Prepare for the next page. Make sure we haven't hit the end.
			m_currentPageIndex++;
			ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
		}
		#endregion

		public void print(LocalReport report, DataTable dataTable, Dictionary<String, String> dic) {
			try {
				ReportDataSource theReportDataSource = new ReportDataSource();
				theReportDataSource.Name = "theDataSet";
				theReportDataSource.Value = dataTable;
				report.DataSources.Add(theReportDataSource);
				String[] keysArray = dic.Keys.ToArray();
				ReportParameter[] reportParameterArray = new ReportParameter[keysArray.Length];
				for(int i = 0; i < keysArray.Length; i++) {
					reportParameterArray[i] = new ReportParameter(keysArray[i], dic[keysArray[i]]);
				}
				report.SetParameters(reportParameterArray);
				Export(report);
				ReportPrint();
			} catch(Exception e) {
				System.Windows.MessageBox.Show(e.Message);
			}
		}
	}
}
