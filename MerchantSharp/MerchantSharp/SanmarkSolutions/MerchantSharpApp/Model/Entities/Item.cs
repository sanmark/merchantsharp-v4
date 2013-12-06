using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class Item : Entity {

		private int id = 0;
		public int Id {
			get { return id; }
			set { id = value; }
		}

		private int categoryId = -1;
		public int CategoryId {
			get { return categoryId; }
			set { categoryId = value; }
		}

		private int companyId = -1;
		public int CompanyId {
			get { return companyId; }
			set { companyId = value; }
		}

		private int unitId = -1;
		public int UnitId {
			get { return unitId; }
			set { unitId = value; }
		}

		private String name = null;
		public String Name {
			get { return name; }
			set { name = value; }
		}

		private String code = null;
		public String Code {
			get { return code; }
			set { code = value; }
		}

		private String barcode = null;
		public String Barcode {
			get { return barcode; }
			set { barcode = value; }
		}

		private String posName = null;
		public String PosName {
			get { return posName; }
			set { posName = value; }
		}

		private int si = -1;
		public int Sip {
			get { return si; }
			set { si = value; }
		}

		private double quantityPerPack = -1;
		public double QuantityPerPack {
			get { return quantityPerPack; }
			set { quantityPerPack = value; }
		}

		private String packName = null;
		public String PackName {
			get { return packName; }
			set { packName = value; }
		}

		private double displayPercentage = -1;
		public double DisplayPercentage {
			get { return displayPercentage; }
			set { displayPercentage = value; }
		}

		private double reorderLevel = -1;
		public double ReorderLevel {
			get { return reorderLevel; }
			set { reorderLevel = value; }
		}

		private String details = null;
		public String Details {
			get { return details; }
			set { details = value; }
		}

		private int showCategoryInPrintedInvoice = -1;
		public int ShowCategoryInPrintedInvoice {
			get { return showCategoryInPrintedInvoice; }
			set { showCategoryInPrintedInvoice = value; }
		}

		private int showCompanyInPrintedInvoice = -1;
		public int ShowCompanyInPrintedInvoice {
			get { return showCompanyInPrintedInvoice; }
			set { showCompanyInPrintedInvoice = value; }
		}

		private String defaultBuyingMode = null;
		public String DefaultBuyingMode {
			get { return defaultBuyingMode; }
			set { defaultBuyingMode = value; }
		}

		private String defaultSellingMode = null;
		public String DefaultSellingMode {
			get { return defaultSellingMode; }
			set { defaultSellingMode = value; }
		}

		private double packBuyingPrice = -1;
		public double PackBuyingPrice {
			get { return packBuyingPrice; }
			set { packBuyingPrice = value; }
		}

		private double unitBuyingPrice = -1;
		public double UnitBuyingPrice {
			get { return unitBuyingPrice; }
			set { unitBuyingPrice = value; }
		}

		private double packSellingPrice = -1;
		public double PackSellingPrice {
			get { return packSellingPrice; }
			set { packSellingPrice = value; }
		}

		private double unitSellingPrice = -1;
		public double UnitSellingPrice {
			get { return unitSellingPrice; }
			set { unitSellingPrice = value; }
		}

		private int status = -1;
		public int Status {
			get { return status; }
			set { status = value; }
		}

		private int createdBy = -1;
		public int CreatedBy {
			get { return createdBy; }
			set { createdBy = value; }
		}

		private DateTime createdDate;
		public DateTime CreatedDate {
			get { return createdDate; }
			set { createdDate = value; }
		}

		private int modifiedBy = -1;
		public int ModifiedBy {
			get { return modifiedBy; }
			set { modifiedBy = value; }
		}

		private DateTime modifiedDate;
		public DateTime ModifiedDate {
			get { return modifiedDate; }
			set { modifiedDate = value; }
		}

	}
}
