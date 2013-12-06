using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities {
	class Entity {

		private int rowsCount = 0;
		public int RowsCount {
			get { return rowsCount; }
			set { rowsCount = value; }
		}

		private String orderBy = null;
		public String OrderBy {
			get { return orderBy; }
			set { orderBy = value; }
		}

		private String orderType = "ASC";
		public String OrderType {
			get { return orderType; }
			set { orderType = value; }
		}

		private int limitStart = -1;
		public int LimitStart {
			get { return limitStart; }
			set { limitStart = value; }
		}

		private int limitEnd = 100;
		public int LimitEnd {
			get { return limitEnd; }
			set { limitEnd = value; }
		}

		public Dictionary<String, String> doubleCondition = null;
		public void addDoubleCondition(String key, String con) {
			try {
				if(doubleCondition == null) {
					doubleCondition = new Dictionary<String, String>();
				}
				doubleCondition.Add(key, con);
			} catch(Exception) {
			}
		}

		public Dictionary<String, String[]> dateCondition = null;
		public void addDateCondition(String key, String con, String dateFrom, String dateTo) {
			try {
				if(dateCondition == null) {
					dateCondition = new Dictionary<String, String[]>();
				}
				dateCondition.Add(key, new String[] { con, dateFrom, dateTo });
			} catch(Exception) {
			}
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
