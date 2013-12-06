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
	class BankManagerControler {

		private AddBank addBank;
		private MSComboBox mSComboBox;

		private BankManagerImpl bankManagerImpl = null;

		public BankManagerControler(AddBank addBank, MSComboBox mSComboBox) {
			// TODO: Complete member initialization
			this.addBank = addBank;
			this.mSComboBox = mSComboBox;
			bankManagerImpl = new BankManagerImpl(addBank);
		}


		internal void addBank_addBank() {
			if(bankManagerImpl.addbankPopup()) {
				UIComboBox.banksForSelect(mSComboBox);
				mSComboBox.SelectedValue = addBank.AddedId;
				addBank.Hide();
				addBank.resetForm();
			}
		}
	}
}
