using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility {
	class DBConnector {

		private static MySqlConnection connection;
		private static DBConnector dBConnector;

		private DBConnector() {
			try {
				if(connection == null) {
					connection = new MySqlConnection(Properties.Settings.Default.connectionString);
				}
			} catch(Exception) {
			}
		}

		public static DBConnector getInstance() {
			try {
				if(dBConnector == null) {
					dBConnector = new DBConnector();
				}
			} catch(Exception) {
			}
			return dBConnector;
		}

		public bool isSucceededConnection() {
			bool b = false;
			try {
				connection.Open();
				b = true;
				connection.Close();
			} catch(Exception) {
				b = false;
			}
			return b;
		}

		public MySqlConnection getConnection() {
			return connection;
		}

		public DataSet getDataSet(String query) {
			DataSet a = new DataSet();
			try {
				connection.Open();
				MySqlCommand command = new MySqlCommand(query, connection);
				new MySqlDataAdapter(command).Fill(a);
				connection.Close();
			} catch(Exception) {
			}
			return a;
		}

		private object getPropValue(object src, string propName) {
			try {
				return src.GetType().GetProperty(propName).GetValue(src, null);
			} catch(Exception) {
				return null;
			}
		}

		private object getPropDateValue(object src, string propName) {
			try {
				DateTime date = Convert.ToDateTime(src.GetType().GetProperty(propName).GetValue(src, null));
				return date.ToString("yyyy-MM-dd");
			} catch(Exception) {
				return null;
			}
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

		private String getPropType(object src, string propName) {
			return src.GetType().GetProperty(propName).PropertyType.ToString();
		}

		private int addEntity(Entity entity, String INSERT_TEXT, String[][] tableDetailsArray) {
			try {
				MySqlCommand cmd = new MySqlCommand();
				connection.Open();
				cmd.Connection = connection;
				cmd.CommandText = INSERT_TEXT;
				cmd.Prepare();
				//String[][] arr = tableDetails.ToArray();
				for(int i = 0; i < tableDetailsArray.Length; i++) {
					if(tableDetailsArray[i][5] != "auto_increment") {
						cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
					}
				}
				int re = Convert.ToInt32(cmd.ExecuteScalar());
				connection.Close();
				return re;
			} catch(Exception) {
				connection.Close();
				return 0;
			}
		}

		private bool delEntity(Entity entity, String DELETE_TEXT, String[][] tableDetailsArray) {
			try {
				MySqlCommand cmd = new MySqlCommand();
				connection.Open();
				cmd.Connection = connection;
				cmd.CommandText = DELETE_TEXT;
				cmd.Prepare();
				//String[][] arr = tableDetails.ToArray();
				for(int i = 0; i < tableDetailsArray.Length; i++) {
					if(tableDetailsArray[i][5] == "auto_increment") {
						cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
						break;
					}
				}
				cmd.ExecuteNonQuery();
				connection.Close();
				return true;
			} catch(Exception) {
				connection.Close();
				return false;
			}
		}

		private DataSet getEntity(Entity entity, String SELECT_TEXT, String[][] tableDetailsArray, String tableName) {
			List<Entity> list = new List<Entity>();
			try {
				MySqlCommand cmd = new MySqlCommand();
				connection.Open();
				cmd.Connection = connection;
				SELECT_TEXT = "SELECT " + (entity.RowsCount > 0 ? "COUNT(*)" : "*") + " FROM `" + tableName + "` WHERE";
				String param = "";
				//String[][] arr = tableDetails.ToArray();
				bool isIdRunId = false;
				/*if(tableName == "buying_invoice") {
					//MessageBox.Show(getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[4][0])).ToString());
					String s = (getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[4][0])).ToString() != "1/1/0001 12:00:00 AM").ToString();
				}*/
				
				for(int i = 0; i < tableDetailsArray.Length; i++) {
					bool canBeDouble = false;
					try {
						if(tableDetailsArray[i][1].Length > 5) {
							canBeDouble = true;
						}
					} catch(Exception) {
					}
					if(tableDetailsArray[i][1].Substring(0, 3) == "int" && tableDetailsArray[i][5] == "auto_increment" && Convert.ToInt32(getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0]))) > 0) {
						param += " AND `" + tableDetailsArray[i][0] + "` LIKE @" + tableDetailsArray[i][0];
						cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
						isIdRunId = true;
					} else if(tableDetailsArray[i][1].Substring(0, 3) == "int" && tableDetailsArray[i][5] != "auto_increment" && Convert.ToInt32(getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0]))) > -1) {
						param += " AND `" + tableDetailsArray[i][0] + "` LIKE @" + tableDetailsArray[i][0];
						cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
					} else if((tableDetailsArray[i][1] == "date" || tableDetailsArray[i][1].Substring(0, 4) == "date") && getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])).ToString() != "1/1/0001 12:00:00 AM") {
						if(entity.dateCondition == null) {
							param += " AND `" + tableDetailsArray[i][0] + "` LIKE @" + tableDetailsArray[i][0];
							cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropDateValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
						} else {
							try {
								if(entity.dateCondition[tableDetailsArray[i][0]] != null) {
									param += " AND (`" + tableDetailsArray[i][0] + "` " + entity.dateCondition[tableDetailsArray[i][0]][0] + " @" + tableDetailsArray[i][0] + "1" + " AND @" + tableDetailsArray[i][0] + "2" + ")";
									cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0] + "1", entity.dateCondition[tableDetailsArray[i][0]][1]);
									cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0] + "2", entity.dateCondition[tableDetailsArray[i][0]][2]);
								} else {
									param += " AND `" + tableDetailsArray[i][0] + "` LIKE @" + tableDetailsArray[i][0];
									cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropDateValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
								}
							} catch(Exception) {
							}
						}
					} else if(canBeDouble && tableDetailsArray[i][1].Substring(0, 6) == "double" && Convert.ToDouble(getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0]))) > 0) {
						if(entity.doubleCondition == null) {
							param += " AND `" + tableDetailsArray[i][0] + "` = @" + tableDetailsArray[i][0];
							cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
						} else {
							try {
								if(entity.doubleCondition[tableDetailsArray[i][0]] != null) {
									param += " AND `" + tableDetailsArray[i][0] + "` " + entity.doubleCondition[tableDetailsArray[i][0]] + " @" + tableDetailsArray[i][0];
									cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
								} else {
									param += " AND `" + tableDetailsArray[i][0] + "` = @" + tableDetailsArray[i][0];
									cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
								}
							} catch(Exception) {
							}
						}
					} else if(tableDetailsArray[i][5] != "auto_increment" && getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])) != null && getPropType(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])) != "System.Double" && getPropType(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])) != "System.Int32" && (getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])).ToString() != "1/1/0001 12:00:00 AM")) {
						param += " AND `" + tableDetailsArray[i][0] + "` LIKE @" + tableDetailsArray[i][0];
						cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
					}
				}
				if(!isIdRunId) {
					param = " AND `id` != '0'" + param;
				}
				param = param.Substring(4);
				if(entity.OrderBy != null) {
					param += " ORDER BY " + entity.OrderBy;
					/*param += " ORDER BY @orderBy @orderType";
					cmd.Parameters.AddWithValue("@orderBy", entity.OrderBy);
					cmd.Parameters.AddWithValue("@orderType", entity.OrderType);*/
				}
				if(entity.LimitStart > -1) {
					param += " LIMIT @limitStart , @limitEnd";
					cmd.Parameters.AddWithValue("@limitStart", entity.LimitStart);
					cmd.Parameters.AddWithValue("@limitEnd", entity.LimitEnd);
				}
				/*if(entity.OrderBy != null) {
					param += " ORDER BY @orderBy @orderType";
					cmd.Parameters.AddWithValue("@orderBy", entity.OrderBy);
					cmd.Parameters.AddWithValue("@orderType", entity.OrderType);
				}
				if(entity.LimitStart > -1) {
					param += " LIMIT @limitStart , @limitEnd";
					cmd.Parameters.AddWithValue("@limitStart", entity.LimitStart);
					cmd.Parameters.AddWithValue("@limitEnd", entity.LimitEnd);
				}*/

				SELECT_TEXT += param + ";";
				cmd.CommandText = SELECT_TEXT;
				cmd.Prepare();

				DataSet dataSet = new DataSet();
				new MySqlDataAdapter(cmd).Fill(dataSet);

				connection.Close();
				return dataSet;
			} catch(Exception) {
				connection.Close();
				return null;
			}
		}

		private int updEntity(Entity entity, String UPDATE_TEXT, String[][] tableDetailsArray) {
			try {
				MySqlCommand cmd = new MySqlCommand();
				connection.Open();
				cmd.Connection = connection;
				cmd.CommandText = UPDATE_TEXT;
				cmd.Prepare();
				//String[][] arr = tableDetails.ToArray();
				for(int i = 0; i < tableDetailsArray.Length; i++) {
					cmd.Parameters.AddWithValue("@" + tableDetailsArray[i][0], getPropValue(entity, getPropertyNameByColumnName(tableDetailsArray[i][0])));
				}
				int re = Convert.ToInt32(cmd.ExecuteScalar());
				connection.Close();
				return re;
			} catch(Exception) {
				connection.Close();
				return 0;
			}
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public Object executeCommand(Entity entity, String[][] tableDetailsArray, String statement, String tableName, String type) {
			Object obj = null;
			try {
				if(type == "add") {
					obj = addEntity(entity, statement, tableDetailsArray);
				} else if(type == "del") {
					obj = delEntity(entity, statement, tableDetailsArray);
				} else if(type == "get") {
					obj = getEntity(entity, null, tableDetailsArray, tableName);
				} else if(type == "upd") {
					obj = updEntity(entity, statement, tableDetailsArray);
				}
			} catch(Exception) {
			}
			return obj;
		}
	}
}
