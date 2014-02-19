using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ProductTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Controler {
	class CompanyReturnManagerControler {

		private AddCompanyReturn addCompanyReturn;
		private CompanyReturnManagerImpl companyReturnManagerImpl = null;

		public CompanyReturnManagerControler(AddCompanyReturn addCompanyReturn ) {
			this.addCompanyReturn = addCompanyReturn;
			companyReturnManagerImpl = new CompanyReturnManagerImpl(addCompanyReturn);
		}

		internal void Add_Window_Loaded() {
			try {
				companyReturnManagerImpl.Add_Window_Loaded();
			} catch ( Exception ) {
			}
		}

		internal void textBox_itemId_TextChanged() {
			try {
				if ( !addCompanyReturn.textBox_itemId.IsNull() ) {
					companyReturnManagerImpl.selectItemById();
					addCompanyReturn.textBox_itemId.Clear();
				}
			} catch ( Exception ) {
			}
		}

		internal void button_addItem_Click() {
			try {
				if ( companyReturnManagerImpl.addReturnItem() ) {
					ShowMessage.success(Common.Messages.Success.Success002);
				}
			} catch ( Exception ) {
			}
		}
	}
}
