using iReverse_Unisoc_Ultimate.UniFlash.Worker;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace iReverse_Unisoc_Ultimate
{
    namespace MyUI
    {
        internal static class MyDisplay
        {
            #region Disable Sleep
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

            private enum EXECUTION_STATE : uint
            {
                ES_SYSTEM_REQUIRED = 0x1,
                ES_DISPLAY_REQUIRED = 0x2,
                ES_CONTINUOUS = 0x80000000U
            }

            public static void PreventSleep()
            {
                SetThreadExecutionState(
                    EXECUTION_STATE.ES_CONTINUOUS
                        | EXECUTION_STATE.ES_SYSTEM_REQUIRED
                        | EXECUTION_STATE.ES_DISPLAY_REQUIRED
                );
            }

            public static void AllowSleep()
            {
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            }
            #endregion

            public static string MyOperation = string.Empty;

            public static void GetButtonText(object sender)
            {
                if (sender is Button)
                {
                    Button btn = (Button)sender;
                    MyOperation = btn.Text.ToUpper();
                }
            }

            public static void RichLogs(
                string msg,
                Color colour,
                bool isBold,
                bool NextLine = false
            )
            {

                if (ThemeEngine.isDark && colour == Color.Black) colour = Color.White;
                if (ThemeEngine.isDark && colour == Color.Purple) colour = Color.SkyBlue;
                if (ThemeEngine.isDark && colour == Color.Green) colour = Color.GreenYellow;

                Main.SharedUI.Logs.Invoke(
                    new Action(() =>
                    {
                        Main.SharedUI.Logs.SelectionStart = Main.SharedUI.Logs.Text.Length;
                        Color selectionColor = Main.SharedUI.Logs.SelectionColor;
                        Main.SharedUI.Logs.SelectionColor = colour;
                        if (isBold)
                        {
                            Main.SharedUI.Logs.SelectionFont = new Font(
                                Main.SharedUI.Logs.Font,
                                FontStyle.Bold
                            );
                        }
                        else
                        {
                            Main.SharedUI.Logs.SelectionFont = new Font(
                                Main.SharedUI.Logs.Font,
                                FontStyle.Regular
                            );
                        }
                        Main.SharedUI.Logs.AppendText(msg);
                        Main.SharedUI.Logs.SelectionColor = selectionColor;
                        if (NextLine)
                        {
                            if (Main.SharedUI.Logs.TextLength > 0)
                            {
                                Main.SharedUI.Logs.AppendText("\r\n");
                            }
                        }
                    })
                );
            }

            public static void RtbClear()
            {
                Main.SharedUI.Logs.Invoke(
                    new Action(() =>
                    {
                        Main.SharedUI.Logs.Clear();
                    })
                );
            }

            public static void DataViewClear()
            {
                Main.SharedUI.DataView.Invoke(
                    new Action(() =>
                    {
                        Main.SharedUI.DataView.Rows.Clear();
                    })
                );
            }

            public static bool USBSearchPort()
            {
                bool Flag = false;
                Main.SharedUI.ComboPort.Invoke(
                    new Action(() =>
                    {
                        if (!Main.SharedUI.ComboPort.Text.Contains("SPRD"))
                        {
                            MyProgress.SetWaktu();

                            for (int i = 0; i <= MyProgress.WaktuCari; i++)
                            {
                                MyProgress.Delay(1);

                                if (i > MyProgress.WaktuCari)
                                {
                                    break;
                                }

                                if (Main.SharedUI.ComboPort.Text.ToLower().Contains("u2s"))
                                {
                                    Flag = true;
                                    MyProgress.SetTimer(string.Empty);
                                    break;
                                }

                                MyProgress.SetTimer((MyProgress.WaktuCari - i).ToString());
                            }
                        }
                        else
                        {
                            Flag = true;
                        }

                        if (Flag)
                        {
                            Match match1 = Regex.Match(
                                Main.SharedUI.ComboPort.Text,
                                "\\((COM\\d+)\\)"
                            );
                            if (match1.Success)
                            {
                                WorkerGlobal.PortCom = match1.Groups[1].Value.Replace(
                                    "COM",
                                    string.Empty
                                );
                            }
                        }
                    })
                );
                return Flag;
            }

            public static string GetFileSizes(long TheSize)
            {
                string str = null;
                long num = TheSize;
                if (num >= 1099511627776L)
                {
                    double DoubleBytes = TheSize / 1099511627776.0;
                    str = $"{DoubleBytes:N2} TB";
                }
                else if (num >= 1073741824L && num <= 1099511627775L)
                {
                    double DoubleBytes = TheSize / 1073741824.0;
                    str = $"{DoubleBytes:N2} GB";
                }
                else if (num >= 1048576L && num <= 1073741823L)
                {
                    double DoubleBytes = TheSize / 1048576.0;
                    str = $"{DoubleBytes:N2} MB";
                }
                else if (num >= 1024L && num <= 1048575L)
                {
                    double DoubleBytes = TheSize / 1024.0;
                    str = $"{DoubleBytes:N2} KB";
                }
                else if (num < 0L || num > 1023L)
                {
                    str = "";
                }
                else
                {
                    double DoubleBytes = TheSize;
                    str = $"{DoubleBytes:N2} bytes";
                }

                return str;
            }
        }
    }
}
