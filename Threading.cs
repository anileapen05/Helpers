using System;
using System.Threading;

namespace Threading_1_0
{


	public delegate void thread_callback(int num, string text);

	public class Threading_Globals
	{
			public static ManualResetEvent man_res_evnt = new ManualResetEvent(false);
			public static AutoResetEvent auto_res_evnt = new AutoResetEvent(false);
			public static ReaderWriterLockSlim rw_lock = new ReaderWriterLockSlim();
			public static Semaphore sem;
			public static Thread[] threads;
			public static object[] objects;

			public static void sleep(int msecs)
			{
				Thread.Sleep(msecs);
			}
	}




}