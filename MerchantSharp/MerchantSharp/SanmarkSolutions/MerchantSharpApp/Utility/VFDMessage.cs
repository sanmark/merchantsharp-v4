using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.Main;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility {
	class VFDMessage {

		private static SerialPort serialPort = null;
		private static byte[] byteStore;

		public static Thread threadTextChange = new Thread(whenTextChange);
		private static String currentString1;
		private static String currentString2;

		private static Thread threadLine1 = null;
		private static int count_1;
		public static String string_vfd_fl;
		private static bool canRunLine1 = true;
		private static bool isNewLine1 = true;

		private static Thread threadLine2 = null;
		private static int count_2;
		public static String string_vfd_sl;
		private static bool canRunLine2 = true;
		private static bool isNewLine2 = true;

		private static void openPort() {
			try {
				if ( serialPort == null ) {
					serialPort = new SerialPort();
					serialPort.PortName = Session.Preference["portVFD"];
					serialPort.BaudRate = 9600;
					serialPort.Parity = Parity.None;
					serialPort.DataBits = 8;
					serialPort.StopBits = StopBits.One;
				}
				serialPort.Open();
			} catch ( Exception ) {
			}
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		private static void write( String message, int lineNumber ) {
			try {
				openPort();
				if ( lineNumber == 1 ) {
					byteStore = new byte[1] { 0x0B };
					serialPort.Write(byteStore, 0, 1);

					if ( isNewLine1 ) {
						byteStore = new byte[1] { 0x18 };
						serialPort.Write(byteStore, 0, 1);
					}

					if ( message != null ) { serialPort.Write(message); }
				} else if ( lineNumber == 2 ) {
					byteStore = new byte[1] { 0x0B };
					serialPort.Write(byteStore, 0, 1);

					byteStore = new byte[1] { 0x0A };
					serialPort.Write(byteStore, 0, 1);

					if ( isNewLine2 ) {
						byteStore = new byte[1] { 0x18 };
						serialPort.Write(byteStore, 0, 1);
					}
					if ( message != null ) { serialPort.Write(message); }
				}
				serialPort.Close();
			} catch ( Exception ) {
			}
		}

		private static void startFlowLine1( String text ) {
			try {
				stopFlowLine1();
				count_1 = -1;
				string_vfd_fl = text;
				if ( threadLine1 != null && threadLine1.IsAlive ) {
					threadLine1.Join();
				}
				canRunLine1 = true;
				threadLine1 = new Thread(flowText1);
				threadLine1.Start();
			} catch ( Exception ) {
			}
		}

		private static void startFlowLine2( String text ) {
			try {
				stopFlowLine2();
				count_2 = -1;
				string_vfd_sl = text;
				if ( threadLine2 != null && threadLine2.IsAlive ) {
					threadLine2.Join();
				}
				canRunLine2 = true;
				threadLine2 = new Thread(flowText2);
				threadLine2.Start();
			} catch ( Exception ) {
			}
		}

		private static void stopFlowLine1() {
			try {
				canRunLine1 = false;
				if ( threadLine1 != null && threadLine1.IsAlive ) {
					threadLine1.Join();
					isNewLine1 = true;
				}
			} catch ( Exception ) {
			}
		}

		private static void stopFlowLine2() {
			try {
				canRunLine2 = false;
				if ( threadLine2 != null && threadLine2.IsAlive ) {
					threadLine2.Join();
					isNewLine2 = true;
				}
			} catch ( Exception ) {
			}
		}

		private static void flowText1() {
			try {
				while ( canRunLine1 ) {
					if ( isNewLine1 ) {
						isNewLine1 = false;
					}
					Thread.Sleep(100);
					count_1++;
					if ( string_vfd_fl.Length - count_1 >= 20 ) {
						write(string_vfd_fl.Substring(count_1, 20), 1);
					} else if ( string_vfd_fl.Length - count_1 == 19 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " ", 1);
					} else if ( string_vfd_fl.Length - count_1 == 18 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " *", 1);
					} else if ( string_vfd_fl.Length - count_1 == 17 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " **", 1);
					} else if ( string_vfd_fl.Length - count_1 == 16 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " ***", 1);
					} else if ( string_vfd_fl.Length - count_1 == 15 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " *** ", 1);
					} else if ( string_vfd_fl.Length - count_1 == 14 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " *** " + string_vfd_fl.Substring(0, 1), 1);
					} else if ( string_vfd_fl.Length - count_1 == 13 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " *** " + string_vfd_fl.Substring(0, 2), 1);
					} else if ( string_vfd_fl.Length - count_1 == 12 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " *** " + string_vfd_fl.Substring(0, 3), 1);
					} else if ( string_vfd_fl.Length - count_1 == 11 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " *** " + string_vfd_fl.Substring(0, 4), 1);
					} else if ( string_vfd_fl.Length - count_1 == 10 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " *** " + string_vfd_fl.Substring(0, 5), 1);
					} else if ( string_vfd_fl.Length - count_1 == 9 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " *** " + string_vfd_fl.Substring(0, 6), 1);
					} else if ( string_vfd_fl.Length - count_1 == 8 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " *** " + string_vfd_fl.Substring(0, 7), 1);
					} else if ( string_vfd_fl.Length - count_1 == 7 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " *** " + string_vfd_fl.Substring(0, 8), 1);
					} else if ( string_vfd_fl.Length - count_1 == 6 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " *** " + string_vfd_fl.Substring(0, 9), 1);
					} else if ( string_vfd_fl.Length - count_1 == 5 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " *** " + string_vfd_fl.Substring(0, 10), 1);
					} else if ( string_vfd_fl.Length - count_1 == 4 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " *** " + string_vfd_fl.Substring(0, 11), 1);
					} else if ( string_vfd_fl.Length - count_1 == 3 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " *** " + string_vfd_fl.Substring(0, 12), 1);
					} else if ( string_vfd_fl.Length - count_1 == 2 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " *** " + string_vfd_fl.Substring(0, 13), 1);
					} else if ( string_vfd_fl.Length - count_1 == 1 ) {
						write(string_vfd_fl.Substring(count_1, ( string_vfd_fl.Length - count_1 )) + " *** " + string_vfd_fl.Substring(0, 14), 1);
					} else if ( string_vfd_fl.Length - count_1 == 0 ) {
						write(" *** " + string_vfd_fl.Substring(0, 15), 1);
					} else if ( string_vfd_fl.Length - count_1 == -1 ) {
						write("*** " + string_vfd_fl.Substring(0, 16), 1);
					} else if ( string_vfd_fl.Length - count_1 == -2 ) {
						write("** " + string_vfd_fl.Substring(0, 17), 1);
					} else if ( string_vfd_fl.Length - count_1 == -3 ) {
						write("* " + string_vfd_fl.Substring(0, 18), 1);
					} else if ( string_vfd_fl.Length - count_1 == -4 ) {
						write(" " + string_vfd_fl.Substring(0, 19), 1);
					} else if ( string_vfd_fl.Length - count_1 == -5 ) {
						write(string_vfd_fl.Substring(0, 20), 1);
						count_1 = -1;
					}
				}
			} catch ( Exception ) {
			}
		}

		private static void flowText2() {
			try {
				while ( canRunLine2 ) {
					if ( isNewLine2 ) {
						isNewLine2 = false;
					}
					Thread.Sleep(100);
					count_2++;
					if ( string_vfd_sl.Length - count_2 >= 20 ) {
						write(string_vfd_sl.Substring(count_2, 20), 2);
					} else if ( string_vfd_sl.Length - count_2 == 19 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " ", 2);
					} else if ( string_vfd_sl.Length - count_2 == 18 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " *", 2);
					} else if ( string_vfd_sl.Length - count_2 == 17 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " **", 2);
					} else if ( string_vfd_sl.Length - count_2 == 16 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " ***", 2);
					} else if ( string_vfd_sl.Length - count_2 == 15 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " *** ", 2);
					} else if ( string_vfd_sl.Length - count_2 == 14 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " *** " + string_vfd_sl.Substring(0, 1), 2);
					} else if ( string_vfd_sl.Length - count_2 == 13 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " *** " + string_vfd_sl.Substring(0, 2), 2);
					} else if ( string_vfd_sl.Length - count_2 == 12 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " *** " + string_vfd_sl.Substring(0, 3), 2);
					} else if ( string_vfd_sl.Length - count_2 == 11 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " *** " + string_vfd_sl.Substring(0, 4), 2);
					} else if ( string_vfd_sl.Length - count_2 == 10 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " *** " + string_vfd_sl.Substring(0, 5), 2);
					} else if ( string_vfd_sl.Length - count_2 == 9 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " *** " + string_vfd_sl.Substring(0, 6), 2);
					} else if ( string_vfd_sl.Length - count_2 == 8 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " *** " + string_vfd_sl.Substring(0, 7), 2);
					} else if ( string_vfd_sl.Length - count_2 == 7 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " *** " + string_vfd_sl.Substring(0, 8), 2);
					} else if ( string_vfd_sl.Length - count_2 == 6 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " *** " + string_vfd_sl.Substring(0, 9), 2);
					} else if ( string_vfd_sl.Length - count_2 == 5 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " *** " + string_vfd_sl.Substring(0, 10), 2);
					} else if ( string_vfd_sl.Length - count_2 == 4 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " *** " + string_vfd_sl.Substring(0, 11), 2);
					} else if ( string_vfd_sl.Length - count_2 == 3 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " *** " + string_vfd_sl.Substring(0, 12), 2);
					} else if ( string_vfd_sl.Length - count_2 == 2 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " *** " + string_vfd_sl.Substring(0, 13), 2);
					} else if ( string_vfd_sl.Length - count_2 == 1 ) {
						write(string_vfd_sl.Substring(count_2, ( string_vfd_sl.Length - count_2 )) + " *** " + string_vfd_sl.Substring(0, 14), 2);
					} else if ( string_vfd_sl.Length - count_2 == 0 ) {
						write(" *** " + string_vfd_sl.Substring(0, 15), 2);
					} else if ( string_vfd_sl.Length - count_2 == -1 ) {
						write("*** " + string_vfd_sl.Substring(0, 16), 2);
					} else if ( string_vfd_sl.Length - count_2 == -2 ) {
						write("** " + string_vfd_sl.Substring(0, 17), 2);
					} else if ( string_vfd_sl.Length - count_2 == -3 ) {
						write("* " + string_vfd_sl.Substring(0, 18), 2);
					} else if ( string_vfd_sl.Length - count_2 == -4 ) {
						write(" " + string_vfd_sl.Substring(0, 19), 2);
					} else if ( string_vfd_sl.Length - count_2 == -5 ) {
						write(string_vfd_sl.Substring(0, 20), 2);
						count_2 = -1;
					}
				}
			} catch ( Exception ) {
			}
		}

		private static void printLine1(/*String text*/) {
			try {
				stopFlowLine1();
				if ( threadLine1 != null && threadLine1.IsAlive ) {
					threadLine1.Join();
				}
				if ( string_vfd_fl.Length <= 20 ) {
					write(string_vfd_fl, 1);
				} else {
					startFlowLine1(string_vfd_fl);
				}
			} catch ( Exception ) {
			}
		}

		private static void printLine2() {
			try {
				stopFlowLine2();
				if ( threadLine2 != null && threadLine2.IsAlive ) {
					threadLine2.Join();
				}
				if ( string_vfd_sl.Length <= 20 ) {
					write(string_vfd_sl, 2);
				} else {
					startFlowLine2(string_vfd_sl);
				}
			} catch ( Exception ) {
			}
		}

		private static void whenTextChange() {
			try {
				while ( true ) {
					Thread.Sleep(100);
					if ( currentString1 != string_vfd_fl ) {
						if ( !isNewLine1 ) {
							isNewLine1 = true;
						}
						currentString1 = string_vfd_fl;
						printLine1();
					}
					if ( currentString2 != string_vfd_sl ) {
						if ( !isNewLine2 ) {
							isNewLine2 = true;
						}
						currentString2 = string_vfd_sl;
						printLine2();
					}
				}
			} catch ( Exception ) {
			}
		}

		public static void terminateAllThreads() {
			try {
				stopFlowLine1();
				stopFlowLine2();
				threadTextChange.Abort();
				write(null, 1);
				write(null, 2);
			} catch ( Exception ) {
			}
		}

	}
}
