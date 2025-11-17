using iReverse_Unisoc_Ultimate.MyUI;
using iReverse_Unisoc_Ultimate.Utility.Connection;
using iReverse_Unisoc_Ultimate.Utility.Connection.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using static iReverse_Unisoc_Ultimate.Utility.Connection.PortIO;
using static iReverse_Unisoc_Ultimate.Utility.Connection.USBFastConnect;

namespace iReverse_Unisoc_Ultimate
{
    namespace UniFlash.Worker
    {
        internal static class WorkerDiagChannel
        {
            public static bool busyState = false;
            public static byte[] DiagChannelPayload = uni.StringToByteArray("7E 00 00 00 00 08 00 FE 81 7E");

            public static void UniWorkerDiagChannel(object sender, DoWorkEventArgs e)
            {
                uint bufferLength = 1024;
                IntPtr buffer = Marshal.AllocHGlobal((int)bufferLength);
                byte[] data = new byte[(int)bufferLength];
                byte[] imeiBuffer1 = new byte[PhoneCommandAPI.MAX_IMEI_STR_LENGTH];
                byte[] imeiBuffer2 = new byte[PhoneCommandAPI.MAX_IMEI_STR_LENGTH];

                if (!Main.SharedUI.CkDiagConnected.Checked)
                {
                    if (DiagChannelOpenPort(sender, e))
                    {
                        if (MyDisplay.USBSearchPort())
                        {
                            DiagChannel.DiagConnect(WorkerGlobal.PortCom);
                            Main.SharedUI.CkDiagConnected.Invoke((Action)(() => Main.SharedUI.CkDiagConnected.Checked = true));
                        }
                        else
                        {
                            Main.SharedUI.CkDiagConnected.Invoke((Action)(() => Main.SharedUI.CkDiagConnected.Checked = false));
                            Thread.Sleep(1000);
                            Main.SharedUI.UnisocWorker.CancelAsync();
                            return;
                        }
                    }
                }
                else
                {
                    if (MyDisplay.USBSearchPort())
                    {
                        DiagChannel.DiagConnect(WorkerGlobal.PortCom);
                    }
                    else
                    {
                        Main.SharedUI.CkDiagConnected.Invoke((Action)(() => Main.SharedUI.CkDiagConnected.Checked = false));
                        Thread.Sleep(1000);
                        Main.SharedUI.UnisocWorker.CancelAsync();
                        return;
                    }
                }

                if (Main.SharedUI.UnisocWorker.CancellationPending)
                {
                    e.Cancel = true;
                    DiagChannel.DiagClose();
                    Marshal.Copy(buffer, data, 0, (int)bufferLength);
                    Marshal.FreeHGlobal(buffer);
                    return;
                }

                MyDisplay.RichLogs("Operation " + "\t" + ": ", Color.Black, true, false);
                MyDisplay.RichLogs(MyDisplay.MyOperation, Color.Purple, true, true);

                MyDisplay.RichLogs("Get Information... ", Color.Black, true, false);
                Thread.Sleep(1000);
                MyDisplay.RichLogs("OK", Color.Purple, true, true);

                MyDisplay.RichLogs("SW Info " + "\t" + ": ", Color.Black, true, false);
                int result = PhoneCommandAPI.SP_GetAPVersion(
                    DiagChannel.hDiagPhone,
                    buffer,
                    bufferLength
                );
                if (result == 0)
                {
                    string productInfo = Marshal.PtrToStringAnsi(buffer);
                    MyDisplay.RichLogs(productInfo, Color.Black, true, true);
                }
                else
                {
                    MyDisplay.RichLogs(",", Color.Black, true, true);
                }

                MyDisplay.RichLogs("IMEI 1 " + "\t" + "\t" + ": ", Color.Black, true, false);
                result = PhoneCommandAPI.SP_ReadImei(
                    DiagChannel.hDiagPhone,
                    PhoneCommandAPI.NVID_IMEI1,
                    imeiBuffer1
                );
                if (result == 0)
                {
                    string imeiString1 = Encoding.ASCII.GetString(imeiBuffer1);
                    MyDisplay.RichLogs(imeiString1, Color.Black, true, true);
                }
                else
                {
                    MyDisplay.RichLogs(",", Color.Black, true, true);
                }
                MyProgress.ProcessBar1(50);

                MyDisplay.RichLogs("IMEI 2 " + "\t" + "\t" + ": ", Color.Black, true, false);
                result = PhoneCommandAPI.SP_ReadImei(
                    DiagChannel.hDiagPhone,
                    PhoneCommandAPI.NVID_IMEI2,
                    imeiBuffer2
                );
                if (result == 0)
                {
                    string imeiString2 = Encoding.ASCII.GetString(imeiBuffer2);
                    MyDisplay.RichLogs(imeiString2, Color.Black, true, true);
                }
                else
                {
                    MyDisplay.RichLogs(",", Color.Black, true, true);
                }
                MyProgress.ProcessBar1(100);

                if (WorkerGlobal.WorkerMethod == "Factory Reset")
                {
                    if (Main.SharedUI.UnisocWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        DiagChannel.DiagClose();
                        Marshal.Copy(buffer, data, 0, (int)bufferLength);
                        Marshal.FreeHGlobal(buffer);
                        return;
                    }
                    MyProgress.ProcessBar1(50);
                    string strResponse = string.Empty;
                    SendAT(DiagChannel.hDiagPhone, "AT+SPDIAG=\"AT+ETSRESET\"", ref strResponse);
                    Console.WriteLine("AT Command Resp : " + strResponse);
                    MyProgress.ProcessBar1(100);

                    Main.SharedUI.CkDiagConnected.Invoke(
                        (Action)(() => Main.SharedUI.CkDiagConnected.Checked = false)
                    );
                }
                else if (WorkerGlobal.WorkerMethod == "Power Off")
                {
                    if (Main.SharedUI.UnisocWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        DiagChannel.DiagClose();
                        Marshal.Copy(buffer, data, 0, (int)bufferLength);
                        Marshal.FreeHGlobal(buffer);
                        return;
                    }
                    MyProgress.ProcessBar1(50);
                    PhoneCommandAPI.SP_PowerOff(DiagChannel.hDiagPhone);
                    MyProgress.ProcessBar1(100);

                    Main.SharedUI.CkDiagConnected.Invoke(
                        (Action)(() => Main.SharedUI.CkDiagConnected.Checked = false)
                    );
                }
                else if (WorkerGlobal.WorkerMethod == "Send ATCommand")
                {
                    if (Main.SharedUI.UnisocWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        DiagChannel.DiagClose();
                        Marshal.Copy(buffer, data, 0, (int)bufferLength);
                        Marshal.FreeHGlobal(buffer);
                        return;
                    }
                    MyProgress.ProcessBar1(50);
                    string strResponse = string.Empty;
                    SendAT(
                        DiagChannel.hDiagPhone,
                        Main.SharedUI.TxtATCommand.Text,
                        ref strResponse
                    );
                    Console.WriteLine("AT Command Resp : " + strResponse);
                    MyProgress.ProcessBar1(100);
                }
                else if (WorkerGlobal.WorkerMethod == "Read IMEI")
                {
                    if (Main.SharedUI.UnisocWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        DiagChannel.DiagClose();
                        Marshal.Copy(buffer, data, 0, (int)bufferLength);
                        Marshal.FreeHGlobal(buffer);
                        return;
                    }
                    result = PhoneCommandAPI.SP_ReadImei(
                        DiagChannel.hDiagPhone,
                        PhoneCommandAPI.NVID_IMEI1,
                        imeiBuffer1
                    );
                    if (result == 0)
                    {
                        string imeiString1 = Encoding.ASCII.GetString(imeiBuffer1);
                        Main.SharedUI.TxtIMEI1.Invoke(
                            (Action)(() => Main.SharedUI.TxtIMEI1.Text = imeiString1)
                        );
                    }

                    MyProgress.ProcessBar1(50);

                    result = PhoneCommandAPI.SP_ReadImei(
                        DiagChannel.hDiagPhone,
                        PhoneCommandAPI.NVID_IMEI2,
                        imeiBuffer2
                    );
                    if (result == 0)
                    {
                        string imeiString2 = Encoding.ASCII.GetString(imeiBuffer2);
                        Main.SharedUI.TxtIMEI2.Invoke(
                            (Action)(() => Main.SharedUI.TxtIMEI2.Text = imeiString2)
                        );
                    }

                    MyProgress.ProcessBar1(100);
                }
                else if (WorkerGlobal.WorkerMethod == "Write IMEI 1")
                {
                    if (Main.SharedUI.UnisocWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        DiagChannel.DiagClose();
                        Marshal.Copy(buffer, data, 0, (int)bufferLength);
                        Marshal.FreeHGlobal(buffer);
                        return;
                    }
                    MyDisplay.RichLogs(" ", Color.Black, true, true);
                    MyProgress.ProcessBar1(50);
                    string i1 = Main.SharedUI.TxtIMEI1.Text;
                    RestoreImei(i1, "1", DiagChannel.hDiagPhone);
                    MyProgress.ProcessBar1(100);
                }
                else if (WorkerGlobal.WorkerMethod == "Write IMEI 2")
                {
                    if (Main.SharedUI.UnisocWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        DiagChannel.DiagClose();
                        Marshal.Copy(buffer, data, 0, (int)bufferLength);
                        Marshal.FreeHGlobal(buffer);
                        return;
                    }
                    MyDisplay.RichLogs(" ", Color.Black, true, true);
                    MyProgress.ProcessBar1(50);
                    string i2 = Main.SharedUI.TxtIMEI2.Text;
                    RestoreImei(i2, "2", DiagChannel.hDiagPhone);
                    MyProgress.ProcessBar1(100);
                }

                DiagChannel.DiagClose();
                Marshal.Copy(buffer, data, 0, (int)bufferLength);
                Marshal.FreeHGlobal(buffer);
            }

            public static bool DiagChannelOpenPort(object sender, DoWorkEventArgs e)
            {
                bool iscontinue = true;
                MyDisplay.RichLogs(
                    "Please connect 'usb' cable w/o pressing any boot button!",
                    Color.Black,
                    true,
                    true
                );
                MyDisplay.RichLogs("Waiting for U2S connection... ", Color.Black, true, false);

                busyState = true;
                List<comInfo> deviceList = listDevices;
                comInfo selectedDevice = FindNewDevice(deviceList);

                if (selectedDevice == null)
                {
                    MyDisplay.RichLogs("Not Found!", Color.Red, true, true);
                    busyState = false;
                    iscontinue = false;
                    return false;
                }
                else
                {
                    MyDisplay.RichLogs("OK", Color.Purple, true, true);
                    string[] usb = VID_PID(selectedDevice.hwid);
                    MyDisplay.RichLogs(
                        "Port Number " + "\t" + "\t" + ": COM" + selectedDevice.comport,
                        Color.Black,
                        true,
                        true
                    );
                    MyDisplay.RichLogs(
                        "Vendor ID " + "\t" + "\t" + ": " + usb[0],
                        Color.Black,
                        true,
                        true
                    );
                    MyDisplay.RichLogs(
                        "Product ID " + "\t" + "\t" + ": " + usb[1],
                        Color.Black,
                        true,
                        true
                    );
                }

                MyDisplay.RichLogs("Handshaking... ", Color.Black, true, false);
                PortOpen(selectedDevice.comport);

                if (serialPort.IsOpen)
                {
                    if (Main.SharedUI.UnisocWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        iscontinue = false;
                        return false;
                    }
                    MyDisplay.RichLogs("OK", Color.Purple, true, true);
                    MyDisplay.RichLogs("Execute command... ", Color.Black, true, false);
                    byte[] datameta = DiagChannelPayload;
                    PortWrite(datameta);
                    MyDisplay.RichLogs("OK", Color.Purple, true, true);
                }
                else
                {
                    MyDisplay.RichLogs("Fail", Color.Red, true, true);
                    busyState = false;
                    iscontinue = false;
                    return false;
                }
                MyDisplay.RichLogs(" ", Color.Purple, true, true);
                MyDisplay.RichLogs(" ", Color.Purple, true, true);
                busyState = false;

                Main.SharedUI.ComboPort.Invoke(
                    new Action(() =>
                    {
                        do
                        {
                            if (Main.SharedUI.UnisocWorker.CancellationPending)
                            {
                                e.Cancel = true;
                                iscontinue = false;
                                return;
                            }
                            if (string.IsNullOrEmpty(Main.SharedUI.ComboPort.Text))
                            {
                                break;
                            }
                            MyProgress.Delay(1);
                        } while (true);
                    })
                );

                if (Main.SharedUI.UnisocWorker.CancellationPending)
                {
                    e.Cancel = true;
                    iscontinue = false;
                    return false;
                }
                return iscontinue;
            }

            private static string ImeiToHex(string imei)
            {
                if (string.IsNullOrEmpty(imei))
                {
                    return string.Empty;
                }
                imei = imei.Trim();
                if (imei.Length != 15)
                {
                    return string.Empty;
                }
                string res = imei.Substring(0, 1) + "A ";

                for (int i = 1; i < imei.Length; i += 2)
                {
                    res += imei.Substring(i + 1, 1) + imei.Substring(i, 1) + " ";
                }

                Console.WriteLine("Imei To Hex : " + res);
                return res;
            }

            private static string HexToImei(string hex)
            {
                if (string.IsNullOrEmpty(hex))
                {
                    return string.Empty;
                }
                hex = hex.Replace(" ", string.Empty).Replace("-", string.Empty);
                if (hex.Length != 16)
                {
                    return string.Empty;
                }
                string res = hex.Substring(0, 1) + "A ";

                for (int i = 0; i < hex.Length; i += 2)
                {
                    res += hex.Substring(i + 1, 1) + hex.Substring(i, 1) + " ";
                }

                res = res.Substring(4).Replace(" ", string.Empty);
                Console.WriteLine("Hex To Imei : " + res);
                return res;
            }

            private static void RestoreImei(
                string Imei,
                string num,
                PhoneCommandAPI.SP_HANDLE hDiagPhone
            )
            {
                //WRITE IMEI 1	:
                MyDisplay.RichLogs(
                    "WRITE IMEI " + num + "	: " + Imei + "... ",
                    Color.Black,
                    true,
                    false
                );
                ushort NVID = PhoneCommandAPI.NVID_IMEI1;
                if (num == "1")
                {
                    NVID = PhoneCommandAPI.NVID_IMEI1;
                }
                else if (num == "2")
                {
                    NVID = PhoneCommandAPI.NVID_IMEI2;
                }
                else
                {
                    MyDisplay.RichLogs("Error", Color.Red, true, true);
                    return;
                }
                Console.WriteLine("SP_WriteImei " + "NVID : " + num + " IMEI : " + Imei);
                int result = PhoneCommandAPI.SP_WriteImei(hDiagPhone, NVID, Imei);
                Thread.Sleep(1000);
                if (result == 0)
                {
                    MyDisplay.RichLogs("OK", Color.Purple, true, true);
                }
                else
                {
                    MyDisplay.RichLogs("FAIL, Error " + result, Color.Red, true, true);
                }
            }

            private static int SendAT(
                PhoneCommandAPI.SP_HANDLE hDiagPhone,
                string command,
                ref string response
            )
            {
                string atCommand = command;
                byte[] atCommandBytes = Encoding.ASCII.GetBytes(atCommand);
                bool wantReply = true;
                int replyCapacity = 1024;
                byte[] replyStringBytes = new byte[replyCapacity];
                uint replyStringLength = 0;
                uint timeout = 5000;

                var result = PhoneCommandAPI.SP_SendATCommand(
                    hDiagPhone,
                    atCommandBytes,
                    wantReply,
                    replyStringBytes,
                    (uint)replyCapacity,
                    ref replyStringLength,
                    timeout
                );
                string replyString = Encoding.ASCII.GetString(
                    replyStringBytes,
                    0,
                    (int)replyStringLength
                );
                response = Regex.Replace(replyString, "[\\r\\n]|OK", string.Empty);

                return result;
            }
        }
    }
}
