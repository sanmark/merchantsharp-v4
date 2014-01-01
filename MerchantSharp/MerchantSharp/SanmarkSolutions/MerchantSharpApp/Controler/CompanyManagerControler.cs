using CustomControls.SanmarkSolutions.WPFCustomControls.MSComboBox;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class CompanyManagerControler {

		private AddCompany addCompany;
		private MSComboBox mSComboBox;

		private CompanyManagerImpl companyManagerImpl = null;

		public CompanyManagerControler(AddCompany addCompany, MSComboBox mSComboBox) {
			// TODO: Complete member initialization
			this.addCompany = addCompany;
			this.mSComboBox = mSComboBox;
			companyManagerImpl = new CompanyManagerImpl(addCompany);
		}

		internal void addCompany_addCompany() {
			if(companyManagerImpl.addCompanyPopup()) {
				//UIComboBox.unitsForAddItem(mSComboBox);
				//mSComboBox.SelectedValue = addCategory.AddedId;
				addCompany.Hide();
				addCompany.resetForm();
			}
		}
	}
}
