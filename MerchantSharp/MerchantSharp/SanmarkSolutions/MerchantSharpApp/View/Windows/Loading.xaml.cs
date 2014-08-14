using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Initialize;
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

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Windows {
	/// <summary>
	/// Interaction logic for Loading.xaml
	/// </summary>
	public partial class Loading : Window {

		private InitializeSystem initializeSystem;

		public Loading() {
			InitializeComponent();
			initializeSystem = new InitializeSystem(this);
		}

	}
}
