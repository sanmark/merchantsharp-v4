using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao {
	class BaseDao {

		private String tableName = null;
		public String TableName {
			get { return tableName; }
			protected set { tableName = value; }
		}

		protected HashSet<String[]> tableDetails = null;
		private String INSERT_TEXT = null;
		private String DELETE_TEXT = null;
		//private String SELECT_TEXT = null;
		private String UPDATE_TEXT = null;
		private String CLASS_NAME = null;
		private String [][] tableDetailsArray = null;

		protected BaseDao() {
		}

		protected void initializeTable() {
			try {
				setTableDetails();
				setInsertText();
				setDeleteText();
				setUpdateText();
				CLASS_NAME = getEntityNameByTableName();
			} catch(Exception) {
			}
		}

		private void setTableDetails() {
			try {
				//tableDetails = new HashSet<String[]>();
				//String query = "SHOW COLUMNS FROM `" + tableName + "`";
				//MySqlDataReader reader = DBConnector.getResult(query);
				//String[] arr = null;

				tableDetails = new HashSet<String[]>();
				String query = "SHOW COLUMNS FROM `" + tableName + "`";
				DataSet dataSet = DBConnector.getInstance().getDataSet(query);
				String[] arr = null;
				foreach(DataRow row in dataSet.Tables[0].Rows) {
					arr = new String[6];
					//Console.WriteLine(row["Field"]);
					arr[0] = row["Field"].ToString();
					arr[1] = row["Type"].ToString();
					arr[2] = row["Null"].ToString();
					arr[3] = row["Key"].ToString();
					arr[4] = row["Default"].ToString();
					arr[5] = row["Extra"].ToString();
					tableDetails.Add(arr);
				}
				tableDetailsArray = tableDetails.ToArray();
			} catch(Exception) {
			}
		}

		private void setInsertText() {
			try {
				INSERT_TEXT = "INSERT INTO `" + tableName + "` VALUES(";
				String param = "";
				for(int i = 0; i < tableDetailsArray.Length; i++) {
					if(tableDetailsArray[i][5] == "auto_increment") {
						param += "NULL, ";
					} else {
						param += "@" + tableDetailsArray[i][0] + ", ";
					}
				}
				param = param.Substring(0, param.Length - 2);
				INSERT_TEXT += param + "); SELECT last_insert_id();";
			} catch(Exception) {
			}
		}

		private void setDeleteText() {
			try {
				DELETE_TEXT = "DELETE FROM `" + tableName + "` WHERE ";
				String param = "";
				for(int i = 0; i < tableDetailsArray.Length; i++) {
					if(tableDetailsArray[i][5] == "auto_increment") {
						param += "`" + tableDetailsArray[i][0] + "`=" + "@" + tableDetailsArray[i][0];
						break;
					}
				}
				DELETE_TEXT += param + ";";
			} catch(Exception) {
			}
		}

		private void setUpdateText() {
			try {
				UPDATE_TEXT = "UPDATE `" + tableName + "` SET ";
				String param = "";
				String WHERE = " WHERE ";
				//String[][] arr = tableDetails.ToArray();
				for(int i = 0; i < tableDetailsArray.Length; i++) {
					if(tableDetailsArray[i][5] == "auto_increment") {
						WHERE += "`" + tableDetailsArray[i][0] + "`=@" + tableDetailsArray[i][0];
					} else {
						param += "`" + tableDetailsArray[i][0] + "`=@" + tableDetailsArray[i][0] + ", ";
					}
				}
				param = param.Substring(0, param.Length - 2);
				UPDATE_TEXT += param + WHERE + ";";
			} catch(Exception) {
			}
		}

		private String getEntityNameByTableName() {
			String name = "";
			try {
				String[] names = this.tableName.Split('_');
				for(int i = 0; i < names.Length; i++) {
					name += Char.ToUpper(names[i][0]) + names[i].Substring(1);
				}
			} catch(Exception) {
			}
			return name;
		}

		protected String getPropertyNameByColumnName(String columnName) {
			String name = "";
			try {
				String[] names = columnName.Split('_');
				for(int i = 0; i < names.Length; i++) {
					name += Char.ToUpper(names[i][0]) + names[i].Substring(1);
				}
			} catch(Exception) {
			}
			return name;
		}

		private object getPropValue(object src, string propName) {
			return src.GetType().GetProperty(propName).GetValue(src, null);
		}


		private String getPropType(object src, string propName) {
			return src.GetType().GetProperty(propName).PropertyType.ToString();
		}

		protected int addEntity(Entity entity) {
			try {
				return Convert.ToInt32(DBConnector.getInstance().executeCommand(entity, tableDetailsArray, INSERT_TEXT, tableName, "add"));
			} catch(Exception) {
				return 0;
			}
		}

		protected bool delEntity(Entity entity) {
			try {
				return Convert.ToBoolean(DBConnector.getInstance().executeCommand(entity, tableDetailsArray, DELETE_TEXT, tableName, "del"));
			} catch(Exception) {
				return false;
			}
		}

		protected List<Entity> getEntity(Entity entity) {
			List<Entity> list = new List<Entity>();
			try {
				DataSet dataSet = (DataSet)DBConnector.getInstance().executeCommand(entity, tableDetailsArray, null, tableName, "get");
				Object e = null;
				foreach(DataRow row in dataSet.Tables[0].Rows) {
					var type = Type.GetType(string.Format("{0}.{1}", entity.GetType().Namespace, CLASS_NAME));
					e = Activator.CreateInstance(type);
					if(entity.RowsCount > 0) {
						PropertyInfo propertyInfo = e.GetType().GetProperty("RowsCount");
						long tempLong = ((Convert.ToInt64(row[0]) >> 32) << 32); //shift it right then left 32 bits, which zeroes the lower half of the long
						int yourInt = (int)(Convert.ToInt64(row[0]) - tempLong);
						//propertyInfo.SetValue(e, row[0]);
						propertyInfo.SetValue(e, yourInt);
					} else {
						for(int i = 0; i < tableDetailsArray.Length; i++) {
							try {
								PropertyInfo propertyInfo = e.GetType().GetProperty(getPropertyNameByColumnName(tableDetailsArray[i][0]));
								propertyInfo.SetValue(e, row[i]);
							} catch(Exception) {
							}
						}
					}
					list.Add((Entity)e);
				}
				return list;
			} catch(Exception) {
				return null;
			}
		}

		protected int updEntity(Entity entity) {
			try {
				return Convert.ToInt32(DBConnector.getInstance().executeCommand(entity, tableDetailsArray, UPDATE_TEXT, tableName, "upd"));
			} catch(Exception) {
				return 0;
			}
		}

	}
}
