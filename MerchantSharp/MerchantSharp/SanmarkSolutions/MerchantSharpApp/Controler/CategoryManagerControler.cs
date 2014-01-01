using CustomControls.SanmarkSolutions.WPFCustomControls.MSComboBox;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class CategoryManagerControler {

		private AddCategory addCategory;
		private MSComboBox mSComboBox;

		private CategoryManagerImpl categoryManagerImpl = null;

		public CategoryManagerControler(AddCategory addCategory, MSComboBox mSComboBox) {
			this.addCategory = addCategory;
			this.mSComboBox = mSComboBox;
			categoryManagerImpl = new CategoryManagerImpl(addCategory);
		}

		internal void addCategory_addCategory() {
			if(categoryManagerImpl.addCategoryPopup()) {
				//UIComboBox.unitsForAddItem(mSComboBox);
				//mSComboBox.SelectedValue = addCategory.AddedId;
				addCategory.Hide();
				addCategory.resetForm();
			}
		}
	}
}
