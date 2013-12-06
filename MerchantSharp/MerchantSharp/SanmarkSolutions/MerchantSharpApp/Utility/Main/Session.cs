using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.MainWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main {
	class Session {

		private static Dictionary<String, Object> sessionMap = new Dictionary<String, Object>();

		/// <summary>
		/// Will add attributes in the Session, if exists replace it.
		/// </summary>
		/// <param name="name">The Name(Key, Id or Identifier) of the Object.</param>
		/// <param name="value">The Object(Value) of the Name.</param>
		public static void setAttribute(String name, Object value){
			try {
				try {
					if(sessionMap[name] == null) {
						sessionMap.Add(name, value);
					} else {
						sessionMap.Remove(name);
						sessionMap.Add(name, value);
					}
				} catch(Exception) {
					sessionMap.Add(name, value);
				}				
			} catch(Exception) {
			}
		}

		/// <summary>
		/// Can be get the value of name.
		/// </summary>
		/// <param name="name">The Name(Key, Id or Identifier) of Object entered.</param>
		/// <returns>Object.</returns>
		public static Object getAttribute(String name) {
			try {
				return sessionMap[name];
			} catch(Exception) {
				return null;
			}
		}

		/// <summary>
		/// GET and SET Property of current logged User.
		/// </summary>
		public static User User {
			get { return (User)getAttribute("USER"); }
			set { setAttribute("USER", value); }
		}

		/// <summary>
		/// GET and SET Property of MainWindow
		/// </summary>
		public static MainWindow MainWindow {
			get { return (MainWindow)getAttribute("MAIN_WINDOW"); }
			set { setAttribute("MAIN_WINDOW", value); }
		}

		/// <summary>
		/// GET and SET Permissions of current User
		/// </summary>
		public static Dictionary<String, int> Permission {
			get { return (Dictionary<String, int>)getAttribute("PERMISSION"); }
			set { setAttribute("PERMISSION", value); }
		}

		/// <summary>
		/// GET and SET Preferences of System
		/// </summary>
		public static Dictionary<String, String> Preference {
			get { return (Dictionary<String, String>)getAttribute("PREFERENCE"); }
			set { setAttribute("PREFERENCE", value); }
		}

		/// <summary>
		/// GET and SET Meta of System
		/// </summary>
		public static Dictionary<String, int> Meta {
			get { return (Dictionary<String, int>)getAttribute("META"); }
			set { setAttribute("META", value); }
		}
		
	}
}
