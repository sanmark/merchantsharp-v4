using CustomControls.SanmarkSolutions.WPFCustomControls.MSComboBox;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.StakeHolders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class VendorManagerControler {

		private AddVendor addVendor = null;
		private MSComboBox mSComboBox;

		private VendorManagerImpl vendorManagerImpl = null;

		public VendorManagerControler(AddVendor addVendor, MSComboBox mSComboBox) {
			this.addVendor = addVendor;
			this.mSComboBox = mSComboBox;
			vendorManagerImpl = new VendorManagerImpl(addVendor);
		}

		internal void addVendor_addVendor() {
			if(vendorManagerImpl.addVendorPopup()) {
				UIComboBox.vendorsForAddBuyingInvoice(mSComboBox);
				mSComboBox.SelectedValue = addVendor.AddedId;
				addVendor.Hide();
				addVendor.resetForm();
			}
		}
	}
}
