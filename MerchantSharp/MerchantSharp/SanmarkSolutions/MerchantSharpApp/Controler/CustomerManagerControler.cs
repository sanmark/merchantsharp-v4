using CustomControls.SanmarkSolutions.WPFCustomControls.MSComboBox;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.StakeHolders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class CustomerManagerControler {
		private AddCustomer addCustomer;
		private MSComboBox mSComboBox;

		private CustomerManagerImpl customerManagerImpl = null;

		public CustomerManagerControler(AddCustomer addCustomer, MSComboBox mSComboBox) {
			// TODO: Complete member initialization
			this.addCustomer = addCustomer;
			this.mSComboBox = mSComboBox;
			customerManagerImpl = new CustomerManagerImpl(addCustomer);
		}

		internal void addCustomer_addCustomer() {
			if(customerManagerImpl.addCustomerPopup()) {
				UIComboBox.customersForAddSellingInvoice(mSComboBox);
				mSComboBox.SelectedValue = addCustomer.AddedId;
				addCustomer.Hide();
				addCustomer.resetForm();
			}
		}
	}
}
