using iReverse_Unisoc_Ultimate.MyUI;
using iReverse_Unisoc_Ultimate.UniFlash.Worker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace iReverse_Unisoc_Ultimate
{
    namespace UniFlash
    {
        internal static class uni
        {
            public static bool isRSAExploit = false;
            public static string Temp = string.Empty;
            public static byte[] fdl1;
            public static string fdl1_location = string.Empty;
            public static string fdl1_addr = string.Empty;
            public static string exploit = string.Empty;
            public static byte[] fdl2;
            public static string fdl2_location = string.Empty;
            public static string fdl2_addr = string.Empty;
            public static string Timeout = "5000 ";
            public static string UniDir =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts))
                + "\\"
                + "UniDir";
            public static string UniTmp =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts))
                + "\\"
                + "UniTmp";
            public static string uniCommand = string.Empty;
            public static bool isPartitionOperation = false;

            public static void uni_cmd(BackgroundWorker worker, DoWorkEventArgs e)
            {
                if (worker.CancellationPending)
                {
                    worker.CancelAsync();
                    e.Cancel = true;
                    uniCommand = string.Empty;
                    return;
                }
                else
                {
                    try
                    {
                        bool enter_space_partition = true;
                        ProcessStartInfo startInfo = new ProcessStartInfo(UniDir + "\\chiller.tft",uniCommand)
                        {
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            UseShellExecute = false,
                            Verb = "runas",
                            WorkingDirectory = UniDir,
                            RedirectStandardError = true
                        };
                        Console.WriteLine(" ");
                        Console.WriteLine("uni.exe " + uniCommand);
                        Console.WriteLine(" ");
                        MyDisplay.RichLogs(" ", Color.Azure, true, true);
                        using (Process process = Process.Start(startInfo))
                        {
                            bool mute = false;
                            while (!process.StandardError.EndOfStream)
                            {
                                string text = process.StandardError.ReadLine() ?? string.Empty;

                                if (text.Contains("[+]"))
                                {
                                    mute = true;
                                    string val = text.Replace("Found Partition   : [+] ", string.Empty);
                                    string[] tmp = val.Split(' ');

                                    //New
                                    Main.SharedUI.DataView.Invoke((Action)(() => Main.SharedUI.DataView.Rows.Add(false, tmp[0].Replace("_a", string.Empty).Replace("_b", string.Empty).Replace("my_", string.Empty).Replace("_rs1", string.Empty).Replace("_rs2", string.Empty).Replace("_rs", string.Empty).Replace("l_", string.Empty).Replace("w_", string.Empty).ToUpper(), tmp[0], BitConverter.ToInt32(Encoding.Unicode.GetBytes(tmp[0]), 2), str_to_size(tmp[1].Replace("B", string.Empty)), tmp[1], "Double click for input file...")));
                                }

                                if (
                                    (
                                        text.Contains("Writing Partition") ||
                                        text.Contains("Erasing Partition") ||
                                        text.Contains("Reading Partition")
                                    )
                                    && enter_space_partition)
                                {
                                    text = $"\n{text}";

                                    enter_space_partition = false;
                                }

                                if (text.Contains("Found!")) mute = false;

                                if (text.Contains("Done") || text.Contains("Failed"))
                                {
                                    WorkerDownload.totaldo += 1;
                                    MyProgress.ProcessBar2(WorkerDownload.totaldo, WorkerDownload.totalchecked);
                                }

                                if (worker.CancellationPending)
                                {
                                    worker.CancelAsync();
                                    e.Cancel = true;
                                    process.Close();
                                }

                                if (text.Contains("Kicked"))
                                {
                                    Main.SharedUI.CkFDLLoaded.Invoke((Action)(() => Main.SharedUI.CkFDLLoaded.Checked = true));
                                }

                                if (text.Contains("%"))
                                {
                                    string s = text;
                                    int position = s.LastIndexOf('[');

                                    if (position > -1)
                                    {
                                        s = s.Substring(position + 1).Replace("%]", string.Empty);
                                        if (!string.IsNullOrEmpty(s))
                                        {
                                            MyProgress.ProcessBar1(int.Parse(s));
                                        }
                                    }
                                    if (position > 11)
                                    {
                                        MyDisplay.RichLogs(text.Substring(0, position - 11), Color.Black, false, false);
                                        Console.Write(text.Substring(0, position - 11));
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(text);
                                    if (text.Contains("FDL 1"))
                                    {
                                        MyDisplay.RichLogs("Sending FDL 1     : ", Color.Black, false, false);
                                    }
                                    else if (text.Contains("FDL 2"))
                                    {
                                        MyDisplay.RichLogs("Sending FDL 2     : ", Color.Black, false, false);
                                    }
                                    else if (text.Contains("Download Info     :"))
                                    {
                                        MyDisplay.RichLogs(string.Empty, Color.Black, true, false);
                                    }
                                    else if (text.Contains("Setup             :"))
                                    {
                                        MyDisplay.RichLogs(string.Empty, Color.Black, true, false);
                                    }
                                    else if (text.Contains("State             :"))
                                    {
                                        MyDisplay.RichLogs(string.Empty, Color.Black, true, false);
                                    }
                                    else if (text.Contains("Config            :"))
                                    {
                                        MyDisplay.RichLogs(string.Empty, Color.Black, true, false);
                                    }
                                    else if (text.Contains("Reading GPT Table"))
                                    {
                                        MyDisplay.RichLogs("Reading GPT Table : ", Color.Black, false, false);
                                        MyDisplay.RichLogs("OK", Color.Green, true, true);
                                    }
                                    else if (text == "Repartition       : Start")
                                    {
                                        MyDisplay.RichLogs("Repartition       : ", Color.Black, false, false);
                                        mute = true;
                                    }
                                    else if (text.Contains("Reset Device"))
                                    {
                                        MyDisplay.RichLogs("Rebooting : ", Color.Black, false, false);
                                        Main.SharedUI.CkFDLLoaded.Invoke((Action)(() => Main.SharedUI.CkFDLLoaded.Checked = false));
                                    }
                                    else if (text.Contains("Power Off Device"))
                                    {
                                        MyDisplay.RichLogs("Power Off : ", Color.Black, false, false);
                                        Main.SharedUI.CkFDLLoaded.Invoke((Action)(() => Main.SharedUI.CkFDLLoaded.Checked = false));
                                    }
                                    else if (text.Contains("Skiped"))
                                    {
                                        MyDisplay.RichLogs("Skiped!", Color.Orange, true, true);
                                    }
                                    else if (text.Contains("Done") || text.Contains("Done\n"))
                                    {
                                        mute = false;
                                        MyDisplay.RichLogs("OK", Color.Green, true, true);
                                    }
                                    else if (text.Contains("Failed"))
                                        MyDisplay.RichLogs("Failed", Color.Red, true, true);
                                    else
                                    {
                                        if (!mute)
                                        {
                                            var log = filter_logs(text);
                                            {
                                                if (log.Contains("SPRD"))
                                                {
                                                    MyDisplay.RichLogs("Protocol          : ", Color.Black, false, false);
                                                    MyDisplay.RichLogs(log, Color.MediumSlateBlue, true, true);
                                                }
                                                else if (log.Contains("Done"))
                                                {
                                                    MyDisplay.RichLogs("OK", Color.Green, true, true);
                                                }
                                                else if (log.Contains("Boot ver"))
                                                {
                                                    var ver = log.Split(':');
                                                    MyDisplay.RichLogs("Boot ver          : ", Color.Black, false, false);
                                                    MyDisplay.RichLogs(ver[1].Substring(1, ver[1].Length - 1), Color.MediumSlateBlue, true, true);
                                                }
                                                else if (string.IsNullOrEmpty(log))
                                                {
                                                    MyDisplay.RichLogs(string.Empty, Color.Green, true, false);
                                                }
                                                else
                                                {
                                                    MyDisplay.RichLogs(log, Color.Black, false, true);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (text.Contains("usb_send failed"))
                                            {
                                                MyDisplay.RichLogs("Failed", Color.Red, true, true);
                                            }
                                        }
                                    }
                                }
                            }
                            process.WaitForExit();
                        }
                        uniCommand = string.Empty;
                    }
                    catch (Exception ex)
                    {
                        uniCommand = string.Empty;
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            public static string filter_logs(string logs)
            {
                if (string.IsNullOrEmpty(logs))
                    return string.Empty;
                else
                    return logs
                        .Replace("Boot Version      : ", string.Empty)
                        .Replace("Boot Information  : ", "Boot ver : ")
                        .Replace("FDL2 State        : ", string.Empty)
                        .Replace("Ok Kicked By [Hadi Khoirudin, S.Kom]", string.Empty);
            }

            public static bool Prepare_Exploit(string val)
            {
                fdl1_addr = val;
                if (val == "0x5000")
                {
                    exploit = "0x4ee8";
                    return true;
                }
                else if (val == "0x00005000")
                {
                    exploit = "0x4ee8";
                    return true;
                }
                else if (val == "0x65000800")
                {
                    exploit = "0x65015f08";
                    return true;
                }
                else
                {
                    exploit = string.Empty;
                    return false;
                }
            }

            //New 8-4-2024
            public static ulong str_to_size(string str)
            {
                int shl = 0;
                ulong n = Convert.ToUInt64(
                    str.Replace("B", string.Empty)
                        .Replace("K", string.Empty)
                        .Replace("k", string.Empty)
                        .Replace("M", string.Empty)
                        .Replace("m", string.Empty)
                        .Replace("G", string.Empty)
                        .Replace("g", string.Empty)
                );

                if (str.EndsWith("K") || str.EndsWith("k"))
                {
                    shl = 10;
                }
                else if (str.EndsWith("M") || str.EndsWith("m"))
                {
                    shl = 20;
                }
                else if (str.EndsWith("G") || str.EndsWith("g"))
                {
                    shl = 30;
                }
                else
                {
                    throw new Exception("unknown size suffix");
                }

                if (shl != 0)
                {
                    long tmp = (long)n;
                    tmp >>= 63 - shl;
                    if (tmp != 0 && tmp != -1)
                    {
                        throw new Exception("size overflow on multiply");
                    }
                }

                return (ulong)(n << shl);
            }

            public static byte[] TakeByte(byte[] source, int start, long length)
            {
                return (
                    (from element in source select element).Skip(start).Take((int)length)
                ).ToArray();
            }

            public static byte[][] Split(this byte[] input, byte[] separator)
            {
                List<byte[]> list = new List<byte[]>();
                using (MemoryStream memoryStream = new MemoryStream(input))
                {
                    while (memoryStream.Position + 1L < input.Length)
                    {
                        int num = input.Find(separator, (int)memoryStream.Position);
                        byte[] array = (
                            (num != -1)
                                ? new byte[(int)(num - memoryStream.Position)]
                                : new byte[(int)(input.Length - memoryStream.Position)]
                        );
                        if (array.Length != 0)
                        {
                            memoryStream.Read(array, 0, array.Length);
                            list.Add(array);
                        }
                        memoryStream.Seek(separator.Length, SeekOrigin.Current);
                    }
                }
                return list.ToArray();
            }

            public static int Find(this byte[] haystack, byte[] needle, int start = 0)
            {
                int num = needle.Length;
                int num2 = haystack.Length - num;
                for (int i = start; i <= num2; i++)
                {
                    int j = 0;
                    while (j < num && needle[j] == haystack[i + j])
                    {
                        j += 1;
                    }
                    if (j == num)
                    {
                        return i;
                    }
                }
                return -1;
            }

            public static int FindBinary(byte[] data, byte[] needle, int pos = 0)
            {
                byte[][] array = needle.Split(new byte[1] { 46 });
                int num = 0;
                List<int> list = new List<int>();
                do
                {
                    if (num != -1)
                    {
                        num = data.Find(array[0], pos + num);
                        if (num == -1)
                        {
                            if (list.Count <= 0)
                            {
                                break;
                            }
                            foreach (int item in list)
                            {
                                bool flag = false;
                                int num2 = item + array[0].Length;
                                for (int i = 1; i < array.Length; i++)
                                {
                                    num2 += 1;
                                    if (data.Find(array[i], num2) == 0)
                                    {
                                        num2 += array[i].Length;
                                        continue;
                                    }
                                    flag = true;
                                    break;
                                }
                                if (!flag)
                                {
                                    return item + pos;
                                }
                            }
                        }
                        else
                        {
                            list.Add(num);
                            num += 1;
                        }
                        continue;
                    }
                    return 0;
                } while (true);
                return 0;
            }

            public static byte[] parse_reverse(byte[] data)
            {
                string a = BitConverter.ToString(data).Replace("-", " ");
                byte[] b = StringToByteArray(ReverseBytes(a));
                return b;
            }

            public static string ReverseBytes(string value)
            {
                string reversed = string.Empty;
                string val = value.Replace(" ", string.Empty).Replace("-", string.Empty);
                for (int i = val.Length - 2; i >= 0; i -= 2)
                {
                    reversed += val.Substring(i, 2);
                }
                return reversed;
            }

            public static byte[] StringToByteArray(string hex)
            {
                hex = hex.Replace(" ", string.Empty).Replace("-", string.Empty);

                byte[] bytes = new byte[hex.Length / 2];

                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                }

                return bytes;
            }

            public static void Prepairing(BackgroundWorker worker, DoWorkEventArgs e)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                ProcessKill();

                if (!Directory.Exists(UniDir))
                {
                    Directory.CreateDirectory(UniDir);

                    if (isRSAExploit)
                    {
                        if (exploit == "0x4ee8")
                        {
                            File.WriteAllBytes(
                                UniDir + "\\exploit_4ee8.bin",
                                Properties.Resources.exploit_4ee8
                            );
                        }
                        else if (exploit == "0x65015f48")
                        {
                            File.WriteAllBytes(
                                UniDir + "\\exploit_65015f48.bin",
                                Properties.Resources.exploit_65015f48
                            );
                        }
                        else if (exploit == "0x65015f08")
                        {
                            File.WriteAllBytes(
                                UniDir + "\\exploit_65015f08.bin",
                                Properties.Resources.exploit_65015f08
                            );
                        }
                    }

                    File.WriteAllBytes(UniDir + "\\chiller.tft", Properties.Resources.sleep);
                    File.WriteAllBytes(UniDir + "\\Channel9.dll", Properties.Resources.Channel9);
                    File.WriteAllBytes(UniDir + "\\msvcp140d.dll", Properties.Resources.msvcp140d);
                    File.WriteAllBytes(UniDir + "\\ucrtbased.dll", Properties.Resources.ucrtbased);
                    File.WriteAllBytes(
                        UniDir + "\\vcruntime140d.dll",
                        Properties.Resources.vcruntime140d
                    );
                }
                else
                {
                    MyProgress.Delay(1);
                    Cleaner();
                    MyProgress.Delay(1);

                    Directory.CreateDirectory(UniDir);

                    if (isRSAExploit)
                    {
                        if (exploit == "0x4ee8")
                        {
                            File.WriteAllBytes(
                                UniDir + "\\exploit_4ee8.bin",
                                Properties.Resources.exploit_4ee8
                            );
                        }
                        else if (exploit == "0x65015f48")
                        {
                            File.WriteAllBytes(
                                UniDir + "\\exploit_65015f48.bin",
                                Properties.Resources.exploit_65015f48
                            );
                        }
                        else if (exploit == "0x65015f08")
                        {
                            File.WriteAllBytes(
                                UniDir + "\\exploit_65015f08.bin",
                                Properties.Resources.exploit_65015f08
                            );
                        }
                    }
                    File.WriteAllBytes(UniDir + "\\chiller.tft", Properties.Resources.sleep);
                    File.WriteAllBytes(UniDir + "\\Channel9.dll", Properties.Resources.Channel9);
                    File.WriteAllBytes(UniDir + "\\msvcp140d.dll", Properties.Resources.msvcp140d);
                    File.WriteAllBytes(UniDir + "\\ucrtbased.dll", Properties.Resources.ucrtbased);
                    File.WriteAllBytes(
                        UniDir + "\\vcruntime140d.dll",
                        Properties.Resources.vcruntime140d
                    );
                }
            }

            public static void ProcessKill()
            {
                string[] array = { "uni", "uni.exe", "chiller", "chiller.tft", "c4", "c4.exe" };

                foreach (string text in array)
                {
                    Process[] processes = Process.GetProcesses();

                    foreach (Process process in processes)
                    {
                        if (
                            (process.ProcessName.ToLower() ?? string.Empty)
                            == (text.ToLower() ?? string.Empty)
                        )
                        {
                            process.Kill();
                            process.WaitForExit();
                            process.Dispose();
                        }
                    }
                }
            }

            public static void Cleaner()
            {
                if (Directory.Exists(UniDir))
                {
                    DirectoryInfo directory = new DirectoryInfo(UniDir);
                    foreach (FileInfo File in directory.EnumerateFiles())
                    {
                        File.Delete();
                    }
                    foreach (DirectoryInfo subDirectory in directory.EnumerateDirectories())
                    {
                        subDirectory.Delete(true);
                    }
                    directory.Delete(true);
                }
            }

            public static void CleanerTmp()
            {
                if (Directory.Exists(UniTmp))
                {
                    DirectoryInfo directory = new DirectoryInfo(UniTmp);
                    foreach (FileInfo File in directory.EnumerateFiles())
                    {
                        File.Delete();
                    }
                    foreach (DirectoryInfo subDirectory in directory.EnumerateDirectories())
                    {
                        subDirectory.Delete(true);
                    }
                    directory.Delete(true);
                }
            }
        }
    }
}
