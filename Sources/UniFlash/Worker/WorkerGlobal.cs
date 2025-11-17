using iReverse_Unisoc_Ultimate.MyUI;
using System;
using System.ComponentModel;
using System.Drawing;

namespace iReverse_Unisoc_Ultimate
{
    namespace UniFlash.Worker
    {
        internal static class WorkerGlobal
        {
            public static string PortCom = string.Empty;
            public static string WorkerMethod = string.Empty;
            public static object USBMethod = "Download";

            public static void UnisocWorker_DoWork(object sender, DoWorkEventArgs e)
            {
                if (Convert.ToString(USBMethod) == "Download")
                {
                    WorkerDownload.UniworkerDownload(sender, e);
                }
                else if (Convert.ToString(USBMethod) == "Diag Channel")
                {
                    WorkerDiagChannel.UniWorkerDiagChannel(sender, e);
                }
            }

            public static void UnisocWorker_RunWorkerCompleted(
                object sender,
                RunWorkerCompletedEventArgs e
            )
            {
                MyDisplay.RichLogs(Environment.NewLine, Color.Lime, false, false);
                MyDisplay.RichLogs("iReverse Unisoc Ultimate Download Tool - [", Color.Azure, false, false);
                MyDisplay.RichLogs(
                    DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss"),
                    Color.DarkOrange,
                    false,
                    false
                );
                MyDisplay.RichLogs("]", Color.Azure, false, true);
                MyDisplay.RichLogs("All Tasks Is Completed - ", Color.Azure, false, false);

                MyDisplay.RichLogs(
                    "Brought to you by Hadi Khoirudin, S. Kom.",
                    Color.SkyBlue,
                    false,
                    true
                );
                
                MyDisplay.RichLogs(
                    "\n*For educational purposes...",
                    Color.Azure,
                    true,
                    true
                );

                MyProgress.ProcessBar2(100);
                WorkerMethod = string.Empty;
                WorkerDownload.totalchecked = 0;
                WorkerDownload.totaldo = 0;
            }

        }
    }
}
