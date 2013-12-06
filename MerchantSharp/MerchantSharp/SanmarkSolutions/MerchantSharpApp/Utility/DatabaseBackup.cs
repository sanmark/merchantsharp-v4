using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Initialize;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Modules;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility {
	class DatabaseBackup {

		public DatabaseBackup() {
		}

		public void autoBackup() {
			try {
				InitializeSystem.runningStatus = Common.Messages.Information.Info003;
				Thread.Sleep(200);
				PreferenceManagerImpl preferenceImpl = new PreferenceManagerImpl();
				String path = preferenceImpl.getPreferenceValueByKey("autoBackupLocation");
				if(String.IsNullOrWhiteSpace(path)) {
					MessageBox.Show(Common.Messages.Error.Error002);
				} else {
					InitializeSystem.runningStatus = Common.Messages.Information.Info004;
					DirectoryInfo dirInfo = new DirectoryInfo(path);
					String saveFileName = DateTime.Today.ToString("yyyy-MM-dd") + ".sql";
					if(!File.Exists(path + "\\" + saveFileName)) {
						InitializeSystem.runningStatus = Common.Messages.Information.Info005;
						Thread.Sleep(200);
						MySqlBackup mb = new MySqlBackup(DBConnector.getInstance().getConnection());
						ExportInformations info = new ExportInformations();
						info.FileName = path + "\\" + saveFileName;
						mb.Export(info);
					}
				}
			} catch(Exception) {
			}
		}

	}
}
