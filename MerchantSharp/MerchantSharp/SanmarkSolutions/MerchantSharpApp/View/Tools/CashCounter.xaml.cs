using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.ReportMold;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Reports;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Tools {
	/// <summary>
	/// Interaction logic for CashCounter.xaml
	/// </summary>
	public partial class CashCounter : UserControl {

		List<Label> noteList = null;
		List<TextBox> quantityList = null;
		List<Label> lineTotalList = null;

		public CashCounter() {
			InitializeComponent();
			noteList = new List<Label>();
			quantityList = new List<TextBox>();
			lineTotalList = new List<Label>();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			try {
				noteList.Add(label_5000Note);
				noteList.Add(label_2000Note);
				noteList.Add(label_1000Note);
				noteList.Add(label_500Note);
				noteList.Add(label_200Note);
				noteList.Add(label_100Note);
				noteList.Add(label_50Note);
				noteList.Add(label_20Note);
				noteList.Add(label_10Note);
				noteList.Add(label_coinsNote);

				quantityList.Add(textBox_5000NoteQuantity);
				quantityList.Add(textBox_2000NoteQuantity);
				quantityList.Add(textBox_1000NoteQuantity);
				quantityList.Add(textBox_500NoteQuantity);
				quantityList.Add(textBox_200NoteQuantity);
				quantityList.Add(textBox_100NoteQuantity);
				quantityList.Add(textBox_50NoteQuantity);
				quantityList.Add(textBox_20NoteQuantity);
				quantityList.Add(textBox_10NoteQuantity);
				quantityList.Add(textBox_coinsNoteQuantity);

				lineTotalList.Add(label_5000lineTotal);
				lineTotalList.Add(label_2000lineTotal);
				lineTotalList.Add(label_1000lineTotal);
				lineTotalList.Add(label_500lineTotal);
				lineTotalList.Add(label_200lineTotal);
				lineTotalList.Add(label_100lineTotal);
				lineTotalList.Add(label_50lineTotal);
				lineTotalList.Add(label_20lineTotal);
				lineTotalList.Add(label_10lineTotal);
				lineTotalList.Add(label_coinslineTotal);
			} catch (Exception) {
			}
		}

		private void calculateMul(Label labelNote, TextBox textBox, Label labelView) {
			try {
				double note = 0;
				try {
					note = Convert.ToDouble(labelNote.Content);
				} catch (Exception) {
					note = 1;
				}
				double quantity = (!String.IsNullOrWhiteSpace(textBox.Text)) ? Convert.ToDouble(textBox.Text) : 0;
				labelView.Content = (note * quantity).ToString("#,##0.00");
			} catch (Exception) {

			}
		}

		private void calculateTotal() {
			try {
				double total = 0;

				foreach (Label label in lineTotalList) {
					try {
						total += Convert.ToDouble(label.Content);
					} catch (Exception) {
					}
				}
				label_total.Content = total.ToString("#,##0.00");
			} catch (Exception) {
			}
		}

		private void textBox_5000NoteQuantity_TextChanged(object sender, TextChangedEventArgs e) {
			calculateMul(label_5000Note, textBox_5000NoteQuantity, label_5000lineTotal);
			calculateTotal();
		}

		private void textBox_2000NoteQuantity_TextChanged(object sender, TextChangedEventArgs e) {
			calculateMul(label_2000Note, textBox_2000NoteQuantity, label_2000lineTotal);
			calculateTotal();
		}

		private void textBox_1000NoteQuantity_TextChanged(object sender, TextChangedEventArgs e) {
			calculateMul(label_1000Note, textBox_1000NoteQuantity, label_1000lineTotal);
			calculateTotal();
		}

		private void textBox_500NoteQuantity_TextChanged(object sender, TextChangedEventArgs e) {
			calculateMul(label_500Note, textBox_500NoteQuantity, label_500lineTotal);
			calculateTotal();
		}

		private void textBox_200NoteQuantity_TextChanged(object sender, TextChangedEventArgs e) {
			calculateMul(label_200Note, textBox_200NoteQuantity, label_200lineTotal);
			calculateTotal();
		}

		private void textBox_100NoteQuantity_TextChanged(object sender, TextChangedEventArgs e) {
			calculateMul(label_100Note, textBox_100NoteQuantity, label_100lineTotal);
			calculateTotal();
		}

		private void textBox_50NoteQuantity_TextChanged(object sender, TextChangedEventArgs e) {
			calculateMul(label_50Note, textBox_50NoteQuantity, label_50lineTotal);
			calculateTotal();
		}

		private void textBox_20NoteQuantity_TextChanged(object sender, TextChangedEventArgs e) {
			calculateMul(label_20Note, textBox_20NoteQuantity, label_20lineTotal);
			calculateTotal();
		}

		private void textBox_10NoteQuantity_TextChanged(object sender, TextChangedEventArgs e) {
			calculateMul(label_10Note, textBox_10NoteQuantity, label_10lineTotal);
			calculateTotal();
		}

		private void textBox_coinsNoteQuantity_TextChanged(object sender, TextChangedEventArgs e) {
			calculateMul(label_coinsNote, textBox_coinsNoteQuantity, label_coinslineTotal);
			calculateTotal();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e) {
			print();
		}

		private void UserControl_KeyDown_1(object sender, KeyEventArgs e) {
			if (e.Key == Key.F12) {
				print();
			}
		}

		private void print() {
			try {
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Id", typeof(String));
				dataTable.Columns.Add("Note", typeof(String));
				dataTable.Columns.Add("Quantity", typeof(String));
				dataTable.Columns.Add("LineTotal", typeof(String));
				double total = 0;
				int count = 0;
				foreach (Label label in noteList) {
					double note = 0;
					double quantity = 0;
					double lineTotal = 0;
					try {
						note = Convert.ToDouble(label.Content);
					} catch (Exception) {
						note = 1;
					}
					try {
						quantity = Convert.ToDouble(quantityList[count].Text);
					} catch (Exception) {
					}
					try {
						lineTotal = Convert.ToDouble(lineTotalList[count].Content);
					} catch (Exception) {
					}
					lineTotal = note * quantity;
					dataTable.Rows.Add("",(note == 1) ? "Coins" : note.ToString("#,##0.00"), quantity, lineTotal.ToString("#,##0.00"));
					total += lineTotal;
					count++;
				}

				PrepareReport prepareReport = new PrepareReport();
				prepareReport.addParameter("total", total.ToString("#,##0.00"));
				prepareReport.addParameter("shopName", Session.Preference["shopName"]);
				prepareReport.addParameter("date", DateTime.Now.ToString());
				new ReportViewer(dataTable, "CashCounterPos", prepareReport.getParameters()).Show();
			} catch (Exception) {
			}
		}

		private void textBox_5000NoteQuantity_GotFocus(object sender, RoutedEventArgs e) {
			try {
				((TextBox)sender).SelectAll();
			} catch (Exception) {
			}
		}

		private void textBox_2000NoteQuantity_GotFocus(object sender, RoutedEventArgs e) {
			try {
				((TextBox)sender).SelectAll();
			} catch (Exception) {
			}
		}

		private void textBox_1000NoteQuantity_GotFocus(object sender, RoutedEventArgs e) {
			try {
				((TextBox)sender).SelectAll();
			} catch (Exception) {
			}
		}

		private void textBox_500NoteQuantity_GotFocus(object sender, RoutedEventArgs e) {
			try {
				((TextBox)sender).SelectAll();
			} catch (Exception) {
			}
		}

		private void textBox_200NoteQuantity_GotFocus(object sender, RoutedEventArgs e) {
			try {
				((TextBox)sender).SelectAll();
			} catch (Exception) {
			}
		}

		private void textBox_100NoteQuantity_GotFocus(object sender, RoutedEventArgs e) {
			try {
				((TextBox)sender).SelectAll();
			} catch (Exception) {
			}
		}

		private void textBox_50NoteQuantity_GotFocus(object sender, RoutedEventArgs e) {
			try {
				((TextBox)sender).SelectAll();
			} catch (Exception) {
			}
		}

		private void textBox_20NoteQuantity_GotFocus(object sender, RoutedEventArgs e) {
			try {
				((TextBox)sender).SelectAll();
			} catch (Exception) {
			}
		}

		private void textBox_10NoteQuantity_GotFocus(object sender, RoutedEventArgs e) {
			try {
				((TextBox)sender).SelectAll();
			} catch (Exception) {
			}
		}

		private void textBox_coinsNoteQuantity_GotFocus(object sender, RoutedEventArgs e) {
			try {
				((TextBox)sender).SelectAll();
			} catch (Exception) {
			}
		}
	}
}
