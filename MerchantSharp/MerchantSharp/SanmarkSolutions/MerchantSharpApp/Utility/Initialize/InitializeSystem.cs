using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Threading;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Initialize {
	class InitializeSystem {

		private Loading loading = null;
		public static String runningStatus = "";
		public static bool isFinishedThread = false;
		public static DispatcherTimer timer;
		private static bool hasError = false;

		public InitializeSystem(Loading loading) {
			this.loading = loading;
			startInitialize();
		}

		internal void startInitialize() {
			try {
				timer = new DispatcherTimer();
				timer.Tick += TimerCallBack;
				timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
				timer.Start();
				Thread t = new Thread(delegate() {
					run();
				});
				t.IsBackground = true;
				t.Start();
			} catch(Exception) {
			}
		}

		private void TimerCallBack(object sender, EventArgs e) {
			try {
				loading.label_status.Content = runningStatus;
				if(hasError) {
					timer.Stop();
					loading.Close();
				}
				if(isFinishedThread) {
					new Login().Show();
					timer.Stop();
					loading.Close();
				}
			} catch(Exception) {
			}
		}

		private void run() {
			try {
				runningStatus = Common.Messages.Information.Info001;
				if(!DBConnector.getInstance().isSucceededConnection()) {
					hasError = true;
					ShowMessage.error(Common.Messages.Error.Error001);
				} else {
					runningStatus = Common.Messages.Information.Info002;
					/*DatabaseBackup databaseBackup = new DatabaseBackup();
					databaseBackup.autoBackup();
					runningStatus = Common.Messages.Information.Info006;*/
					runningStatus = Common.Messages.Information.Info007;
					MetaManagerImpl metaManagerImpl = new MetaManagerImpl();
					List<Meta> listMeta = metaManagerImpl.get(new Meta());
					Dictionary<String ,int> dic = new Dictionary<String, int>();
					foreach(Meta meta in listMeta) {
						dic.Add(meta.Key, meta.Value);
					}
					runningStatus = Common.Messages.Information.Info008;
					Session.Meta = dic;
					runningStatus = Common.Messages.Information.Info009;
					if(Session.Meta["isTrial"] == 1 && Session.Meta["trialLeft"] < 1) {
						runningStatus = Common.Messages.Information.Info010;
						for(int i = 5; i > 0; i--) {
							runningStatus = Common.Messages.Information.Info010 + " " + i;
							Thread.Sleep(1000);
						}
						hasError = true;
					} else if(Session.Meta["isTrial"] == 1) {
						runningStatus = Common.Messages.Information.Info011 + " " + Session.Meta["trialLeft"];
						Thread.Sleep(300);
						ShowMessage.information(Common.Messages.Information.Info011 + " " + Session.Meta["trialLeft"]);
						metaManagerImpl.subtractTrial();
					}
					runningStatus = Common.Messages.Information.Info012;
					PreferenceManagerImpl preferenceImpl = new PreferenceManagerImpl();
					List<Preference> listPreference = preferenceImpl.get(new Preference());
					Dictionary<String, String> dicP = new Dictionary<String, String>();
					foreach(Preference preference in listPreference) {
						dicP.Add(preference.Key, preference.Value);
					}
					runningStatus = Common.Messages.Information.Info008;					
					Session.Preference = dicP;
					DatabaseBackup databaseBackup = new DatabaseBackup();
					databaseBackup.autoBackup();					
					isFinishedThread = true;
				}
			} catch(Exception) {
			}
		}
	}
}
