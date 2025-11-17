using iReverse_Unisoc_Ultimate.MyUI;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace iReverse_Unisoc_Ultimate
{
    namespace UniFlash.Worker
    {
        internal static class WorkerUnlock
        {
            public static bool isOneClickServer = false;
            public static string OneClickServer = "http://localhost/UniFlash";
            public static string OneClickAPIs = OneClickServer + "/api";
            public static string OneClickDataTool = OneClickServer + "/datatool";
            public static string OneClickModels = OneClickDataTool + "/models";
            public static string OneClickList = OneClickDataTool + "/List/Devices.json";
            public static string OneClickDownload = OneClickAPIs + "/spddownload.php";

            public static void SPDOneClickExecModel()
            {
                try
                {
                    if (!string.IsNullOrEmpty(MyListSpdDevice.Brand))
                    {
                        int num = 0;
                        Main.SharedUI.PanelSPDOneClick.Controls.Clear();

                        string str = string.Empty;

                        if (!isOneClickServer)
                        {
                            str = File.ReadAllText(
                                Application.StartupPath
                                    + "\\Data\\Models\\"
                                    + MyListSpdDevice.Brand.ToUpper()
                                    + "\\"
                                    + MyListSpdDevice.ModelName.ToUpper()
                                    + ".txt"
                            );
                        }

                        if (isOneClickServer)
                        {
                            WebRequest webRequest = WebRequest.Create(
                                string.Concat(
                                    new string[]
                                    {
                                        OneClickModels,
                                        "/",
                                        MyListSpdDevice.Brand.ToUpper(),
                                        "/",
                                        MyListSpdDevice.ModelName,
                                        ".txt"
                                    }
                                )
                            );
                            webRequest.Method = "POST";
                            webRequest.ContentType = "application/x-www-form-urlencoded";
                            webRequest.Timeout = 10000;
                            Stream stream = webRequest.GetRequestStream();
                            stream.Close();
                            WebResponse response = webRequest.GetResponse();
                            stream = response.GetResponseStream();
                            StreamReader streamReader = new StreamReader(stream);
                            HttpWebResponse httpWebResponse = (HttpWebResponse)
                                webRequest.GetResponse();
                            if (httpWebResponse.StatusCode != HttpStatusCode.OK)
                            {
                                MessageBox.Show(
                                    "server Error " + httpWebResponse.StatusCode.ToString(),
                                    null,
                                    MessageBoxButtons.OK
                                );
                            }
                            else
                            {
                                while (!streamReader.EndOfStream)
                                {
                                    str += streamReader.ReadLine() + Environment.NewLine;
                                }
                            }
                        }

                        using (StringReader stringReader = new StringReader(str))
                        {
                            while (stringReader.Peek() != -1)
                            {
                                string text = stringReader.ReadLine();
                                if (text.Contains("FDL1Address"))
                                {
                                    uni.Prepare_Exploit(
                                        text.Replace(" ", string.Empty)
                                            .Replace("FDL1Address:", string.Empty)
                                    );
                                }
                                else if (text.Contains("FDL2Address"))
                                {
                                    uni.fdl2_addr = text.Replace(" ", string.Empty)
                                        .Replace("FDL2Address:", string.Empty);
                                }
                                else
                                {
                                    CustomControls.iReverseControls.iReverseButton BtnSPDOneClick =
                                        new CustomControls.iReverseControls.iReverseButton
                                        {
                                            Anchor =
                                                AnchorStyles.Top
                                                | AnchorStyles.Left
                                                | AnchorStyles.Right
                                        };
                                    BtnSPDOneClick.ForeColor = Color.White;
                                    BtnSPDOneClick.Location = new Point(2, num);
                                    BtnSPDOneClick.Size = new Size(194, 23);
                                    BtnSPDOneClick.TabIndex = 36;
                                    BtnSPDOneClick.Text = text;
                                    BtnSPDOneClick.TextAlign = ContentAlignment.MiddleLeft;
                                    BtnSPDOneClick.BorderRadius = 0;
                                    Main.SharedUI.PanelSPDOneClick.Controls.Add(BtnSPDOneClick);
                                    num += 27;
                                    BtnSPDOneClick.Click += SPDDoExecOneClick;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), null, MessageBoxButtons.OK);
                }
            }

            public static void SPDDoExecOneClick(object sender, EventArgs e)
            {
                if (!Main.SharedUI.UnisocWorker.IsBusy)
                {
                    Main.SharedUI.CkFDLLoaded.Checked = false;
                    uni.CleanerTmp();
                    MyDisplay.RtbClear();
                    MyDisplay.GetButtonText(sender);
                    MyProgress.ProcessBar1(0);
                    MyProgress.ProcessBar2(0);
                    WorkerDownload.totaldo = 0;
                    WorkerDownload.totalchecked = 0;

                    MyDisplay.RichLogs("Operation" + "\t" + ": ", Color.Black, true, false);
                    MyDisplay.RichLogs(MyDisplay.MyOperation, Color.Orange, true, true);
                    MyDisplay.RichLogs(" Brand" + "\t" + "\t" + ": ", Color.Black, true, false);
                    MyDisplay.RichLogs(MyListSpdDevice.Brand, Color.Purple, true, true);
                    string dev = MyListSpdDevice.DevicesName.Replace(
                        MyListSpdDevice.Brand,
                        string.Empty
                    );
                    if (!string.IsNullOrEmpty(dev))
                    {
                        MyDisplay.RichLogs(" Devices" + "\t" + ":", Color.Black, true, false);
                        MyDisplay.RichLogs(dev, Color.Purple, true, true);
                    }
                    MyDisplay.RichLogs(" Model" + "\t" + "\t" + ": ", Color.Black, true, false);
                    MyDisplay.RichLogs(MyListSpdDevice.ModelName, Color.Purple, true, true);
                    MyDisplay.RichLogs(" Platform" + "\t" + ": ", Color.Black, true, false);
                    MyDisplay.RichLogs("Spreadtrum", Color.Purple, true, true);
                    MyDisplay.RichLogs(" Connect" + "\t" + ": ", Color.Black, true, false);
                    MyDisplay.RichLogs("Download", Color.Purple, true, true);
                    MyDisplay.RichLogs(" Loader Data" + "\t" + ": ", Color.Black, true, false);

                    uni.fdl1_location = GetSPDFile("fdl1-sign.bin", false);
                    Thread.Sleep(200);
                    uni.fdl2_location = GetSPDFile("fdl2-sign.bin", false);
                    Thread.Sleep(200);

                    MyDisplay.RichLogs("Done  ✓ ", Color.Purple, true, true);
                    MyDisplay.RichLogs("Support Data" + "\t" + ": ", Color.Black, true, false);
                    MyDisplay.RichLogs("Done  ✓ ", Color.Purple, true, true);
                    MyDisplay.RichLogs(" ", Color.Purple, true, true);
                    Thread.Sleep(200);

                    GenerateUniCommand();

                    Main.SharedUI.UnisocWorker.RunWorkerAsync();
                    Main.SharedUI.UnisocWorker.Dispose();
                }
            }

            public static string GetSPDFile(string namafile, bool pbar)
            {
                string result = string.Empty;
                if (!isOneClickServer)
                {
                    try
                    {
                        result =
                            Application.StartupPath
                            + "\\Data\\Models\\"
                            + MyListSpdDevice.Brand.ToUpper()
                            + "\\"
                            + MyListSpdDevice.ModelName.ToUpper()
                            + "\\"
                            + namafile;
                    }
                    catch (Exception e1)
                    {
                        result = string.Empty;
                    }
                }

                if (isOneClickServer)
                {
                    if (!Directory.Exists(uni.UniTmp))
                    {
                        Directory.CreateDirectory(uni.UniTmp);
                    }

                    string s = string.Concat(
                        new string[]
                        {
                            "&merk=",
                            MyListSpdDevice.Brand.ToUpper(),
                            "&type=",
                            MyListSpdDevice.ModelName,
                            "&file=",
                            namafile
                        }
                    );
                    string requestUriString = OneClickDownload;
                    try
                    {
                        HttpWebRequest httpWebRequest = (HttpWebRequest)
                            WebRequest.Create(requestUriString);
                        byte[] bytes = Encoding.UTF8.GetBytes(s);
                        httpWebRequest.Method = "POST";
                        httpWebRequest.Timeout = 600000;
                        httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                        httpWebRequest.ContentLength = bytes.Length;
                        using (Stream requestStream = httpWebRequest.GetRequestStream())
                        {
                            requestStream.Write(bytes, 0, bytes.Length);
                        }
                        HttpWebResponse httpWebResponse = (HttpWebResponse)
                            httpWebRequest.GetResponse();
                        double num = httpWebResponse.ContentLength;
                        byte[] array = new byte[Convert.ToInt32(Math.Round(num - 1.0)) + 1];
                        byte[] buffer = new byte[4096];
                        using (MemoryStream memoryStream = new MemoryStream(array))
                        {
                            int num2 = httpWebResponse.GetResponseStream().Read(buffer, 0, 4096);
                            memoryStream.Write(buffer, 0, num2);
                            int num3 = 0;
                            while (num2 != 0)
                            {
                                num3 += num2;
                                num2 = httpWebResponse.GetResponseStream().Read(buffer, 0, 4096);
                                memoryStream.Write(buffer, 0, num2);
                                if (pbar)
                                {
                                    MyProgress.ProcessBar1(num3, Convert.ToInt64(Math.Round(num)));
                                }
                            }
                        }
                        httpWebResponse.GetResponseStream().Close();

                        if (File.Exists(uni.UniTmp + "\\" + namafile))
                        {
                            File.Delete(uni.UniTmp + "\\" + namafile);
                        }

                        File.WriteAllBytes(uni.UniTmp + "\\" + namafile, array);
                        result = uni.UniTmp + "\\" + namafile;
                    }
                    catch (Exception ex)
                    {
                        result = string.Empty;
                    }
                }
                return result;
            }

            public static void GenerateUniCommand()
            {
                string method = MyDisplay.MyOperation;
                string files = null;
                uni.uniCommand = string.Empty;
                uni.uniCommand = string.Concat(
                    uni.uniCommand,
                    "-progress -wait 5 -timeout " + uni.Timeout
                );

                if (uni.isRSAExploit && !string.IsNullOrEmpty(uni.exploit))
                {
                    uni.uniCommand = string.Concat(uni.uniCommand, "-exploit " + uni.exploit + " ");
                    WorkerDownload.totalchecked += 1;
                }

                if (File.Exists(uni.fdl2_location))
                {
                    uni.uniCommand = string.Concat(
                        uni.uniCommand,
                        "-fdl"
                            + " "
                            + "\""
                            + uni.fdl1_location
                            + "\""
                            + " "
                            + uni.fdl1_addr
                            + " "
                            + "-fdl"
                            + " "
                            + "\""
                            + uni.fdl2_location
                            + "\""
                            + " "
                            + uni.fdl2_addr
                            + " "
                            + "-exec"
                            + " "
                    );
                    WorkerDownload.totalchecked += 2;
                }
                else
                {
                    uni.uniCommand = string.Concat(
                        uni.uniCommand,
                        "-fdl"
                            + " "
                            + "\""
                            + uni.fdl1_location
                            + "\""
                            + " "
                            + uni.fdl1_addr
                            + " "
                            + "-exec"
                            + " "
                    );
                    WorkerDownload.totalchecked += 1;
                }
                Console.WriteLine("Doing " + method);
                if (method == "READ DEVICE INFO - IDENTIFY")
                {
                    WorkerDownload.totalchecked += 2;
                    uni.uniCommand = string.Concat(uni.uniCommand, "-get_deviceinfo " + uni.Temp + "\\boot.img");
                }
                else if (method == "FLASH MIUI RECOVERY - INSTALL")
                {
                    WorkerDownload.totalchecked += 2;
                    uni.uniCommand = string.Concat(uni.uniCommand, "-w boot " + GetSPDFile("MIUI-Recovery.img", false));
                }
                else if (method == "RECOVERY WIPE DATA I + FRP")
                {
                    WorkerDownload.totalchecked += 3;
                    uni.uniCommand = string.Concat(uni.uniCommand, "-erase_frp -e userdata");
                }
                else if (method == "RECOVERY WIPE DATA II + FRP")
                {
                    WorkerDownload.totalchecked += 3;
                    uni.uniCommand = string.Concat(uni.uniCommand, "-erase_frp -e userdata");
                }
                else if (method == "RECOVERY FORMAT DATA + FRP")
                {
                    WorkerDownload.totalchecked += 3;
                    uni.uniCommand = string.Concat(uni.uniCommand, "-erase_frp -e userdata");
                }
                else if (method == "RECOVERY WIPE APP DATA + FRP")
                {
                    WorkerDownload.totalchecked += 3;
                    files = "\"" + Application.StartupPath + "\\Data\\Misc\\4" + "\"";
                    uni.uniCommand = string.Concat(uni.uniCommand, "-erase_frp -w misc " + files);
                }
                else if (method == "RECOVERY WIPE DATA ONLY + FRP")
                {
                    WorkerDownload.totalchecked += 3;
                    files = "\"" + Application.StartupPath + "\\Data\\Misc\\5" + "\"";
                    uni.uniCommand = string.Concat(uni.uniCommand, "-erase_frp -w misc " + files);
                }
                else if (method == "ERASE DATA + FRP")
                {
                    WorkerDownload.totalchecked += 3;
                    uni.uniCommand = string.Concat(uni.uniCommand, "-erase_frp -e userdata");
                }
                else if (method == "ERASE FRP ONLY")
                {
                    WorkerDownload.totalchecked += 2;
                    uni.uniCommand = string.Concat(uni.uniCommand, "-erase_frp");
                }
            }
        }
    }
}
