using CustomControls.SanmarkSolutions.WPFCustomControls.MSComboBox;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class UnitManagerControler {

		private AddUnit addUnit;
		private MSComboBox mSComboBox;

		private UnitManagerImpl unitManagerImpl = null;

		public UnitManagerControler(AddUnit addUnit, MSComboBox mSComboBox) {
			this.addUnit = addUnit;
			this.mSComboBox = mSComboBox;
			unitManagerImpl = new UnitManagerImpl(addUnit);
		}

		internal void addUnit_addUnit() {
			if(unitManagerImpl.addUnitPopup()) {
				UIComboBox.unitsForAddItem(mSComboBox);
				mSComboBox.SelectedValue = addUnit.AddedId;
				addUnit.Hide();
				addUnit.resetForm();
			}
		}
	}
}
