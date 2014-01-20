﻿using CustomControls.SanmarkSolutions.WPFCustomControls.MSTextBox;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
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
	/// Interaction logic for ItemSearch.xaml
	/// </summary>
	public partial class ItemSearch : Window {

		MSTextBox mSTextBox = null;
		MSTextBox itemId = null;
		private DispatcherTimer dispatcherTimer = null;
		
		public ItemSearch(MSTextBox mSTextBox, MSTextBox itemId) {
			InitializeComponent();			
			try {
				this.mSTextBox = mSTextBox;
				this.itemId = itemId;
				this.mSTextBox.TextChanged += new TextChangedEventHandler(textBox_itemSearchTextChanged);
				textBox_itemName.ListBox = listBox;

				dispatcherTimer = new DispatcherTimer();
				dispatcherTimer.Tick += new EventHandler(timerCallBack);
				dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 300);
			} catch(Exception) {
			}
		}

		private void timerCallBack(object sender, EventArgs e) {
			try {
				textBox_itemName.Text = this.mSTextBox.Text;
				textBox_itemName.Focus();
				textBox_itemName.Select(textBox_itemName.Text.Length, 0);
				this.ShowDialog();				
				dispatcherTimer.Stop();
			} catch(Exception) {
			}
		}

		private void textBox_itemSearchTextChanged(object sender, TextChangedEventArgs e) {
			try {
				dispatcherTimer.Stop();
				dispatcherTimer.Start();
			} catch(Exception) {
			}
		}

		private void Window_KeyUp(object sender, KeyEventArgs e) {
			if(e.Key == Key.Escape) {
				this.Hide();
			}
		}

		private void textBox_itemName_TextChanged(object sender, TextChangedEventArgs e) {
			try {
				if(textBox_itemName.IsNull()) {
					UIListBox.recentItems(listBox, 100);
				} else {
					UIListBox.loadItemsByCategoryAndCompanyAndName(listBox, textBox_itemName.Text);
				}
			} catch(Exception) {
			}
		}

		private void textBox_itemName_KeyDown(object sender, KeyEventArgs e) {
			try {
				if(e.Key == Key.Enter && listBox.Items.Count > 0) {
					listBox.SelectedIndex = 0;
				}
			} catch(Exception) {
			}
		}

		private void listBox_KeyDown(object sender, KeyEventArgs e) {
			try {
				if(e.Key == Key.Enter) {
					this.Hide();
					itemId.Text = listBox.SelectedValue.ToString();					
				}
			} catch(Exception) {
			}
		}

	}
}