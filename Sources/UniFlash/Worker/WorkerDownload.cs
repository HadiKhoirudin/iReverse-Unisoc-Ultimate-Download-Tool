using iReverse_Unisoc_Ultimate.My.Boot;
using iReverse_Unisoc_Ultimate.MyUI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace iReverse_Unisoc_Ultimate
{
    namespace UniFlash.Worker
    {
        internal static class WorkerDownload
        {
            public static string UniFirmware = string.Empty;
            public static string UniFoldersave = string.Empty;
            public static string UniStringXML = string.Empty;
            public static string UniFileXML = string.Empty;

            public static long TotProgress = 0;
            public static int totalchecked = 0;
            public static int totaldo = 0;

            public static bool isSpdOneClick = false;

            public static void UniworkerDownload(object sender, DoWorkEventArgs e)
            {
                bool isLoaded = false;
                Main.SharedUI.CkFDLLoaded.Invoke((Action)(() => isLoaded = Main.SharedUI.CkFDLLoaded.Checked));

                if (WorkerGlobal.WorkerMethod != "PAC Firmware")
                {
                    if (Download(sender, e))
                    {
                        MyDisplay.RichLogs(" ", Color.Black, true, true);
                        if (Main.SharedUI.CkFDLLoaded.Checked)
                        {
                            ResetDevice(sender, e);
                        }
                    }
                }
                else if (WorkerGlobal.WorkerMethod == "PAC Firmware")
                {
                    if (!Directory.Exists(Path.GetDirectoryName(UniFirmware) + "\\ImageFiles"))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(UniFirmware) + "\\ImageFiles");
                    }
                    string[] input =
                    {
                        UniFirmware, Path.GetDirectoryName(UniFirmware) + "\\ImageFiles", "-debug"
                    };
                    PACExtractor.StartExtraction(input);
                }
            }

            public static bool Download(object sender, DoWorkEventArgs e)
            {

                if (!Directory.Exists("Temp")) Directory.CreateDirectory("Temp");

                MyDisplay.RichLogs("Searching USB SPRD Port Device... ", Color.Black, true, false);
                bool Flag = false;
                if (MyDisplay.USBSearchPort())
                {
                    MyDisplay.RtbClear();
                    MyDisplay.RichLogs("Operation         : ", Color.Black, true, false);
                    MyDisplay.RichLogs(MyDisplay.MyOperation, Color.Purple, true, true);
                    uni.Cleaner();
                    uni.Prepairing((BackgroundWorker)sender, e);
                    Flag = true;
                }
                else
                {
                    uni.CleanerTmp();
                    uni.Cleaner();
                    return Flag;
                }

                uni.uni_cmd((BackgroundWorker)sender, e);

                if (MyDisplay.MyOperation == "IDENTIFY")
                {
                    UniFileXML = Application.StartupPath + "\\Temp\\repartition.xml";
                    if (File.Exists(UniFileXML))
                    {
                        File.Delete(UniFileXML);
                    }
                    StreamWriter xmlrepartition = new StreamWriter(UniFileXML);

                    xmlrepartition.WriteLine("<?xml version=\"1.0\" ?>");
                    xmlrepartition.WriteLine("<!--NOTE: Genererate by HadiK IT **-->");
                    xmlrepartition.WriteLine("<Partitions>");

                    Main.SharedUI.DataView.Invoke(
                        new Action(() =>
                        {
                            foreach (DataGridViewRow item in Main.SharedUI.DataView.Rows)
                            {
                                string Partition = item.Cells[Main.SharedUI.DataView.Columns[2].Index].Value.ToString();
                                string FileSize = item.Cells[Main.SharedUI.DataView.Columns[5].Index].Value.ToString();
                                if (Partition != "splloader" || Partition != "uboot")
                                {
                                    Console.WriteLine("Result : " + Partition + " must be ignored!");
                                }
                                else
                                {
                                    Console.WriteLine("Result : " + Partition);
                                    string Result = "<Partition ";
                                    Result += string.Concat("id=\"" + Partition + "\" ");
                                    if (Partition == "userdata")
                                    {
                                        Result += string.Concat("size=\"" + "0xFFFFFFFF" + "\"");
                                    }
                                    else
                                    {
                                        Result += string.Concat(
                                            "size=\"" + FileSize.Replace("MB", string.Empty) + "\""
                                        );
                                    }
                                    Result += string.Concat("/>");
                                    xmlrepartition.WriteLine(Result);
                                }
                            }
                        })
                    );

                    xmlrepartition.WriteLine("</Partitions>");
                    xmlrepartition.Close();
                    Main.SharedUI.CkRepartition.Invoke((Action)(() => Main.SharedUI.CkRepartition.Checked = false));
                }

                if (MyDisplay.MyOperation == "READ PARTITION")
                {
                    MyDisplay.RichLogs(" ", Color.Black, true, true);
                    MyDisplay.RichLogs("Device Info       : ", Color.Black, true, false);

                    if (File.Exists(UniFoldersave + "\\partitions.xml"))
                    {
                        File.Delete(UniFoldersave + "\\partitions.xml");
                    }

                    StreamWriter xmlpartition = new StreamWriter(UniFoldersave + "\\partitions.xml");
                    xmlpartition.WriteLine("<?xml version=\"1.0\" ?>");
                    xmlpartition.WriteLine("<!--NOTE: Genererate by HadiK IT **-->");
                    xmlpartition.WriteLine("<Partitions>");

                    Main.SharedUI.DataView.Invoke
                    (
                    new Action(() =>
                    {
                        foreach (DataGridViewRow item in Main.SharedUI.DataView.Rows)
                        {
                            string FileIDS = item.Cells[Main.SharedUI.DataView.Columns[1].Index].Value.ToString();
                            string Partition = item.Cells[Main.SharedUI.DataView.Columns[2].Index].Value.ToString();
                            string Sector = item.Cells[Main.SharedUI.DataView.Columns[3].Index].Value.ToString();
                            string Length = item.Cells[Main.SharedUI.DataView.Columns[4].Index].Value.ToString();
                            string FileSize = item.Cells[Main.SharedUI.DataView.Columns[5].Index].Value.ToString();
                            string Location = Partition + ".bin";

                            string Result = "<Partition ";
                            Result += string.Concat("fileids=\"" + FileIDS + "\" ");
                            Result += string.Concat("partition=\"" + Partition + "\" ");
                            Result += string.Concat("sector=\"" + Sector + "\" ");
                            Result += string.Concat("length=\"" + Length + "\" ");
                            Result += string.Concat("filesize=\"" + FileSize + "\" ");
                            Result += string.Concat("location=\"" + Location + "\" ");
                            Result += string.Concat("/>");

                            xmlpartition.WriteLine(Result);
                        }
                    }));

                    xmlpartition.WriteLine("</Partitions>");
                    xmlpartition.Close();

                    if (File.Exists(uni.fdl1_location))
                    {
                        File.WriteAllBytes(UniFoldersave + "\\" + "fdl1-sign.bin", File.ReadAllBytes(uni.fdl1_location));
                        File.WriteAllText(UniFoldersave + "\\" + "fdl1-addr.txt", uni.fdl1_addr);
                    }

                    if (File.Exists(uni.fdl2_location))
                    {
                        File.WriteAllBytes(UniFoldersave + "\\" + "fdl2-sign.bin", File.ReadAllBytes(uni.fdl2_location));
                        File.WriteAllText(UniFoldersave + "\\" + "fdl2-addr.txt", uni.fdl2_addr);
                    }

                    MyDisplay.RichLogs("Saved at " + UniFoldersave + "*", Color.Purple, true, true);
                }
                if (File.Exists(Application.StartupPath + "\\Temp\\boot.img"))
                {
                    byte[] boot_bytes = File.ReadAllBytes(Application.StartupPath + "\\Temp\\boot.img");

                    if (File.Exists(Application.StartupPath + "\\Temp\\boot.img")) File.Delete(Application.StartupPath + "\\Temp\\boot.img");

                    boot.extract(boot_bytes);
                }

                return Flag;
            }

            public static void ResetDevice(object sender, DoWorkEventArgs e)
            {
                if (Main.SharedUI.CkAutoReboot.Checked)
                {
                    if (Main.SharedUI.UnisocWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    uni.uniCommand = "-fdl_skip -timeout 20000 -reset";
                    uni.uni_cmd((BackgroundWorker)sender, e);
                    uni.CleanerTmp();
                    uni.Cleaner();
                }
                else
                {
                    if (Main.SharedUI.UnisocWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    uni.uniCommand = "-fdl_skip -timeout 20000 -power_off";
                    uni.uni_cmd((BackgroundWorker)sender, e);
                    uni.Cleaner();
                }
            }
        }
    }
}
