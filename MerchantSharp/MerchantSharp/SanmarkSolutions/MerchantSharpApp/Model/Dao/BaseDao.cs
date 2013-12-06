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
		private String SELECT_TEXT = null;
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

				/*while(reader.Read()) {
					arr = new String[6];
					arr[0] = reader.IsDBNull(reader.GetOrdinal("Field")) ? null : reader.GetString(0);
					arr[1] = reader.IsDBNull(reader.GetOrdinal("Type")) ? null : reader.GetString(1);
					arr[2] = reader.IsDBNull(reader.GetOrdinal("Null")) ? null : reader.GetString(2);
					arr[3] = reader.IsDBNull(reader.GetOrdinal("Key")) ? null : reader.GetString(3);
					arr[4] = reader.IsDBNull(reader.GetOrdinal("Default")) ? null : reader.GetString(4);
					arr[5] = reader.IsDBNull(reader.GetOrdinal("Extra")) ? null : reader.GetString(5);
					tableDetails.Add(arr);
				}*/
				tableDetailsArray = tableDetails.ToArray();
			} catch(Exception) {
			}
		}

		private void setInsertText() {
			try {
				INSERT_TEXT = "INSERT INTO `" + tableName + "` VALUES(";
				String param = "";
				//String[][] arr = tableDetails.ToArray();
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
				//String[][] arr = tableDetails.ToArray();
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

		/*private void setSelectText(Entity entity) {
			try {
				cmd = new MySqlCommand();
				cmd.Connection = DBConnector.getConnection();
				SELECT_TEXT = "SELECT " + (entity.RowsCount > 0 ? "COUNT(*)" : "*") + " FROM `" + tableName + "` WHERE";
				String param = "";
				//String[][] arr = tableDetails.ToArray();
				bool isIdRunId = false;
				for(int i = 0; i < tableDetailsArray.Length; i++) {
					if(tableDetailsArray[i][1].Substring(0, 3) == "int" && tableDetailsArray[i][5] == "auto_increment" && Convert.ToInt32(getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0]))) > 0) {
						param += " AND `" + tableDetailsArray[i][0] + "` LIKE @" + tableDetailsArray[i][0];
						cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
						isIdRunId = true;
					} else if(tableDetailsArray[i][1].Substring(0, 3) == "int" && tableDetailsArray[i][5] != "auto_increment" && Convert.ToInt32(getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0]))) > -1) {
						param += " AND `" + tableDetailsArray[i][0] + "` LIKE @" + tableDetailsArray[i][0];
						cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
					} else if(tableDetailsArray[i][1].Substring(0, 6) == "double" && Convert.ToDouble(getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0]))) > 0) {
						if(entity.doubleCondition == null) {
							param += " AND `" + tableDetailsArray[i][0] + "` LIKE @" + tableDetailsArray[i][0];
							cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
						} else {
							try {
								if(entity.doubleCondition[tableDetailsArray[i][0]] != null) {
									param += " AND `" + tableDetailsArray[i][0] + "` " + entity.doubleCondition[tableDetailsArray[i][0]] + " @" + tableDetailsArray[i][0];
									cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
								} else {
									param += " AND `" + tableDetailsArray[i][0] + "` LIKE @" + tableDetailsArray[i][0];
									cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
								}
							} catch(Exception) {
							}
						}
					} else if(tableDetailsArray[i][1].Substring(0, 4) == "date" && getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])) != null) {
						if(entity.dateCondition == null) {
							param += " AND `" + tableDetailsArray[i][0] + "` LIKE @" + tableDetailsArray[i][0];
							cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
						} else {
							try {
								if(entity.dateCondition[tableDetailsArray[i][0]] != null) {
									param += " AND (`" + tableDetailsArray[i][0] + "` " + entity.dateCondition[tableDetailsArray[i][0]][0] + " @" + tableDetailsArray[i][0] + "1" + " AND @" + tableDetailsArray[i][0] + "2" + ")";
									cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0] + "1", entity.dateCondition[tableDetailsArray[i][0]][1]);
									cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0] + "2", entity.dateCondition[tableDetailsArray[i][0]][2]);
								} else {
									param += " AND `" + tableDetailsArray[i][0] + "` LIKE @" + tableDetailsArray[i][0];
									cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
								}
							} catch(Exception) {
							}
						}
					} else if(tableDetailsArray[i][5] != "auto_increment" && getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])) != null && getPropType(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])) != "System.Double" && getPropType(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])) != "System.Int32") {
						param += " AND `" + tableDetailsArray[i][0] + "` LIKE @" + tableDetailsArray[i][0];
						cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
					}
				}
				if(!isIdRunId) {
					param = " AND `id` != '0'" + param;
				}
				param = param.Substring(4);
				SELECT_TEXT += param + ";";
				cmd.CommandText = SELECT_TEXT;
				cmd.Prepare();
			} catch(Exception) {
			}
		}*/

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
				DataSet dataSet = (DataSet)DBConnector.getInstance().executeCommand(entity, tableDetailsArray, INSERT_TEXT, tableName, "get");
				Object e = null;
				foreach(DataRow row in dataSet.Tables[0].Rows) {
					var type = Type.GetType(string.Format("{0}.{1}", entity.GetType().Namespace, CLASS_NAME));
					e = Activator.CreateInstance(type);
					if(entity.RowsCount > 0) {
						PropertyInfo propertyInfo = e.GetType().GetProperty("RowsCount");
						propertyInfo.SetValue(e, row[0]);
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
				/*while(reader.Read()) {
					var type = Type.GetType(string.Format("{0}.{1}", entity.GetType().Namespace, CLASS_NAME));
					e = Activator.CreateInstance(type);
					if(entity.RowsCount > 0) {
						PropertyInfo propertyInfo = e.GetType().GetProperty("RowsCount");
						propertyInfo.SetValue(e, reader.GetInt32(0));
					} else {
						for(int i = 0; i < tableDetailsArray.Length; i++) {
							try {
								PropertyInfo propertyInfo = e.GetType().GetProperty(getPropertyNameByColumnName(tableDetailsArray[i][0]));
								propertyInfo.SetValue(e, reader[i]);
							} catch(Exception) {
							}
						}
					}
					list.Add((Entity)e);
				}*/
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
