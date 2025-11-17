using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace iReverse_Unisoc_Ultimate
{
    namespace MyUI
    {
        internal static class MyProgress
        {
            public static Stopwatch Watch = new Stopwatch();
            public static int WaktuCari = 0;
            private static double DoubleBytes;

            public static void SetWaktu()
            {
                WaktuCari = 60;
                if (Main.SharedUI.LabelTimer.InvokeRequired)
                {
                    Main.SharedUI.LabelTimer.Invoke(
                        (Action)(() => Main.SharedUI.LabelTimer.Visible = true)
                    );
                }
                else
                {
                    Main.SharedUI.LabelTimer.Visible = true;
                }
            }

            public static void SetTimer(string Val)
            {
                if (Main.SharedUI.LabelTimer.InvokeRequired)
                {
                    Main.SharedUI.LabelTimer.Invoke(
                        (Action)(() => Main.SharedUI.LabelTimer.Text = Val)
                    );
                }
                else
                {
                    Main.SharedUI.LabelTimer.Text = Val;
                }
            }

            public static void DGVClear()
            {
                if (Main.SharedUI.DataView.InvokeRequired)
                {
                    Main.SharedUI.DataView.Invoke(
                        (Action)(() => Main.SharedUI.DataView.Rows.Clear())
                    );
                }
                else
                {
                    Main.SharedUI.DataView.Rows.Clear();
                }
            }

            public static void Delay(double dblSecs)
            {
                DateTime.Now.AddSeconds(0.0000115740740740741);
                DateTime dateTime = DateTime.Now.AddSeconds(0.0000115740740740741);
                DateTime dateTime1 = dateTime.AddSeconds(dblSecs);
                while (DateTime.Compare(DateTime.Now, dateTime1) <= 0)
                {
                    Application.DoEvents();
                }
            }

            public static void ProcessBar1(long Process, long total)
            {
                int val = Convert.ToInt32(Math.Round(Process * 100L / (double)total));
                if (val > 99)
                {
                    val = 100;
                }
                Main.SharedUI.IReverseProgressBar1.Invoke(
                    (Action)(() => Main.SharedUI.IReverseProgressBar1.Value = val)
                );
            }

            public static void ProcessBar2(long Process, long total)
            {
                int val = Convert.ToInt32(Math.Round(Process * 100L / (double)total));
                if (val > 99)
                {
                    val = 100;
                }
                Main.SharedUI.IReverseProgressBar2.Invoke(
                    (Action)(() => Main.SharedUI.IReverseProgressBar2.Value = val)
                );
            }

            public static void ProcessBar1(long Process)
            {
                long val = Process;
                if (val > 99)
                {
                    val = 100;
                }
                Main.SharedUI.IReverseProgressBar1.Invoke(
                    (Action)(() => Main.SharedUI.IReverseProgressBar1.Value = (int)val)
                );
            }

            public static void ProcessBar2(long Process)
            {
                long val = Process;
                if (val > 99)
                {
                    val = 100;
                }
                Main.SharedUI.IReverseProgressBar2.Invoke(
                    (Action)(() => Main.SharedUI.IReverseProgressBar2.Value = (int)val)
                );
            }

            public static string GetFileSizes(long TheSize)
            {
                string str = string.Empty;
                try
                {
                    long num = TheSize;
                    if (num >= 1099511627776L)
                    {
                        DoubleBytes = TheSize / 1099511627776.0;
                        str = string.Concat(
                            Microsoft.VisualBasic.Strings.FormatNumber(
                                DoubleBytes,
                                2,
                                Microsoft.VisualBasic.TriState.UseDefault,
                                Microsoft.VisualBasic.TriState.UseDefault,
                                Microsoft.VisualBasic.TriState.UseDefault
                            ),
                            " TB"
                        );
                    }
                    else if (num >= 1073741824L && num <= 1099511627775L)
                    {
                        DoubleBytes = TheSize / 1073741824.0;
                        str = string.Concat(
                            Microsoft.VisualBasic.Strings.FormatNumber(
                                DoubleBytes,
                                2,
                                Microsoft.VisualBasic.TriState.UseDefault,
                                Microsoft.VisualBasic.TriState.UseDefault,
                                Microsoft.VisualBasic.TriState.UseDefault
                            ),
                            " GB"
                        );
                    }
                    else if (num >= 1048576L && num <= 1073741823L)
                    {
                        DoubleBytes = TheSize / 1048576.0;
                        str = string.Concat(
                            Microsoft.VisualBasic.Strings.FormatNumber(
                                DoubleBytes,
                                2,
                                Microsoft.VisualBasic.TriState.UseDefault,
                                Microsoft.VisualBasic.TriState.UseDefault,
                                Microsoft.VisualBasic.TriState.UseDefault
                            ),
                            " MB"
                        );
                    }
                    else if (num >= 1024L && num <= 1048575L)
                    {
                        DoubleBytes = TheSize / 1024.0;
                        str = string.Concat(
                            Microsoft.VisualBasic.Strings.FormatNumber(
                                DoubleBytes,
                                2,
                                Microsoft.VisualBasic.TriState.UseDefault,
                                Microsoft.VisualBasic.TriState.UseDefault,
                                Microsoft.VisualBasic.TriState.UseDefault
                            ),
                            " KB"
                        );
                    }
                    else if (num < 0L || num > 1023L)
                    {
                        str = string.Empty;
                    }
                    else
                    {
                        DoubleBytes = TheSize;
                        str = string.Concat(
                            Microsoft.VisualBasic.Strings.FormatNumber(
                                DoubleBytes,
                                2,
                                Microsoft.VisualBasic.TriState.UseDefault,
                                Microsoft.VisualBasic.TriState.UseDefault,
                                Microsoft.VisualBasic.TriState.UseDefault
                            ),
                            " bytes"
                        );
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                return str;
            }
        }
    }
}
