using iReverse_Unisoc_Ultimate.MyUI;
using iReverse_Unisoc_Ultimate.UniFlash;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using static iReverse_Unisoc_Ultimate.UniFlash.Worker.WorkerDiagChannel;
using static iReverse_Unisoc_Ultimate.UniFlash.Worker.WorkerDownload;
using static iReverse_Unisoc_Ultimate.UniFlash.Worker.WorkerGlobal;
using static iReverse_Unisoc_Ultimate.Utility.Connection.USBFastConnect;

namespace iReverse_Unisoc_Ultimate
{
    public partial class Main
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        internal static Main SharedUI;

        public Main()
        {
            InitializeComponent();
            SharedUI = this;
            this.ClientSize = new Size(1125, 566);
            this.CenterToScreen();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 13, 13));

            ThemeEngine.ThemeSet(this, ThemeEngine.Styles.Dark);

            getcomInfo();

            UnisocWorker.DoWork += UnisocWorker_DoWork;
            UnisocWorker.RunWorkerCompleted += UnisocWorker_RunWorkerCompleted;

            listBoxOneClick.DrawItem += MyListSpdDevice.ListBoxOneClick_DrawItem;
            txtSearchListBox.GotFocus += MyListSpdDevice.txtSearchListBox_GotFocus;
            txtSearchListBox.LostFocus += MyListSpdDevice.txtSearchListBox_LostFocus;
            txtSearchListBox.TextChanged += MyListSpdDevice.txtSearchListBox_TextChanged;
            listBoxOneClick.SelectedIndexChanged += MyListSpdDevice.listBoxOneClick_SelectedIndexChanged;

            uni.Temp = Application.StartupPath + "\\Temp";
            if (!Directory.Exists(uni.Temp)) Directory.CreateDirectory(uni.Temp);

            MyComputers.GetWindowsVersion();
            MyComputers.SetOSInstallDate();
            MyListSpdDevice.CreateListDevice();

        }

        private void iReverseToggleButton_SetTheme_CheckedChanged(object sender, EventArgs e)
        {
            if (iReverseToggleButton_SetTheme.CheckState == CheckState.Checked)
                ThemeEngine.ThemeSet(this, ThemeEngine.Styles.Dark);
            else
                ThemeEngine.ThemeSet(this, ThemeEngine.Styles.Light);
        }

        public void Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public static void Alert(string msg, iReverseCustomUI.Alerts.enmType type)
        {
            iReverseCustomUI.Alerts frm = new iReverseCustomUI.Alerts();
            frm.showAlert(msg, type);
        }

        private void iReverseButtonMinimize_Click(object sender, EventArgs e)
        {
            MyProgress.ProcessBar1(0);
            MyProgress.ProcessBar2(0);
            this.WindowState = FormWindowState.Minimized;
        }

        private void iReverseButtonClose_Click(object sender, EventArgs e)
        {
            var Result = MessageBox.Show
            (
                "Sure wanna exit from this application?",
                "iReverse Unisoc Ultimate Download Tool - C# Version",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Main_Closing(object sender, FormClosingEventArgs e)
        {
            MyDisplay.AllowSleep();
        }

        private void Logs_TextChanged(object sender, EventArgs e)
        {
            if (Logs.InvokeRequired)
            {
                Logs.Invoke
                (new Action(() =>
                   {
                       Logs.SelectionStart = Logs.TextLength;
                       Logs.ScrollToCaret();
                   }
                ));
            }
            else
            {
                Logs.SelectionStart = Logs.TextLength;
                Logs.ScrollToCaret();
            }
        }

        private void ComboPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ComboPort.Text))
            {
                CkFDLLoaded.Checked = false;

                if (CkDiagConnected.CheckState == CheckState.Checked)
                {
                    CkDiagConnected.Checked = false;
                }
            }
        }

        private void comboDownloadTimeout_SelectedIndexChanged(object sender, EventArgs e)
        {
            uni.Timeout = comboDownloadTimeout.Text.Replace("ms", " ");
        }

        private void CkAutoRSAExploit_CheckedChanged(object sender, EventArgs e)
        {
            if (CkAutoRSAExploit.Checked)
            {
                uni.isRSAExploit = true;
            }
            else
            {
                uni.exploit = string.Empty;
                uni.isRSAExploit = false;
            }
        }

        private void CkFDLLoaded_CheckedChanged(object sender, EventArgs e)
        {
            if (CkFDLLoaded.Checked)
            {
                if (string.IsNullOrEmpty(ComboPort.Text))
                {
                    CkFDLLoaded.Checked = false;
                }

                MyDisplay.PreventSleep();
            }
            else
            {
                MyDisplay.AllowSleep();
            }
        }

        private void CkPartition_CheckedChanged(object sender, EventArgs e)
        {
            if (!UnisocWorker.IsBusy)
            {
                if (CkPartition.CheckState == CheckState.Checked)
                {
                    foreach (DataGridViewRow item in DataView.Rows)
                    {
                        for (int i = 0; i < item.Cells.Count; i++)
                        {
                            item.Cells[0].Value = true;
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow item in DataView.Rows)
                    {
                        for (int i = 0; i < item.Cells.Count; i++)
                        {
                            item.Cells[0].Value = false;
                        }
                    }
                }
            }
        }

        private void DataView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!UnisocWorker.IsBusy)
            {
                if (e.ColumnIndex == 6 && DataView.Rows.Count > 0)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog
                    {
                        Title = string.Format("Choice {0}  file !", DataView.CurrentRow.Cells[2].Value),
                        Filter = string.Format("{0}  |*.*|Other|*.*", DataView.CurrentRow.Cells[2].Value)
                    };

                    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        DataView.CurrentRow.Cells[6].Value = openFileDialog.FileName;
                        DataView.CurrentRow.Cells[0].Value = true;
                    }
                }
            }
        }

        private void RdDownload_CheckedChanged(object sender, EventArgs e)
        {
            if (!UnisocWorker.IsBusy)
            {
                if (RdDownload.Checked)
                {
                    USBMethod = "Download";
                }
                else
                {
                    RdDownload.Checked = false;
                    RdDiagChannel.Checked = true;
                }
            }
        }

        private void RdDiagChannel_CheckedChanged(object sender, EventArgs e)
        {
            if (!UnisocWorker.IsBusy)
            {
                if (RdDiagChannel.Checked)
                {
                    USBMethod = "Diag Channel";
                }
                else
                {
                    RdDownload.Checked = true;
                    RdDiagChannel.Checked = false;
                }
            }
        }

        private void RdFactoryTestMode_CheckedChanged(object sender, EventArgs e)
        {
            if (!UnisocWorker.IsBusy)
            {
                if (RdFactoryTestMode.Checked)
                {
                    DiagChannelPayload = uni.StringToByteArray("7E 00 00 00 00 08 00 FE 95 7E");
                }
                else
                {
                    DiagChannelPayload = uni.StringToByteArray("7E 00 00 00 00 08 00 FE 81 7E");
                }
            }
        }

        private void TabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (UnisocWorker.IsBusy)
            {
                MessageBox.Show
                (
                    "Worker is running.",
                    "iReverse Unisoc Ultimate Download Tool - [HadiK-IT]",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                e.Cancel = true;
            }
            else
            {
                if (e.TabPageIndex == 2)
                {
                    MyDisplay.RtbClear();
                    this.ClientSize = new Size(1125, 673);
                    this.CenterToScreen();
                    SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                    Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 13, 13));
                    RdDownload.Checked = true;
                    GroupBoxFlash.Enabled = true;
                    isSpdOneClick = false;
                }
                else if (e.TabPageIndex == 1)
                {
                    MyDisplay.RtbClear();
                    this.ClientSize = new Size(1125, 566);
                    this.CenterToScreen();
                    SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                    Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 13, 13));
                    RdDiagChannel.Checked = true;
                    GroupBoxFlash.Enabled = false;
                    isSpdOneClick = false;
                }
                else if (e.TabPageIndex == 0)
                {
                    MyDisplay.RtbClear();
                    this.ClientSize = new Size(1125, 566);
                    this.CenterToScreen();
                    SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                    Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 13, 13));
                    RdDiagChannel.Checked = false;
                    GroupBoxFlash.Enabled = false;
                    isSpdOneClick = true;
                    MyListSpdDevice.CreateListDevice();
                }
            }
        }

        private void BtnDeviceManager_Click(object sender, EventArgs e)
        {
            Process.Start("devmgmt.msc");
        }

        private void BtnInstallDriver_Click(object sender, EventArgs e)
        {
            if (Environment.Is64BitOperatingSystem)
            {
                Process.Start("Drivers\\Spreadtrum\\" + MyComputers.Win + "\\DPInst64.exe");
            }
            else
            {
                Process.Start("Drivers\\Spreadtrum\\" + MyComputers.Win + "\\DPInst32.exe");
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            if (UnisocWorker.IsBusy)
            {
                MyProgress.WaktuCari = 2;
                UnisocWorker.CancelAsync();
                UnisocWorker.Dispose();
                uni.ProcessKill();
                uni.Cleaner();
                MyDisplay.RichLogs(" ", Color.Black, true, true);
                MyDisplay.RichLogs(" ", Color.Black, true, true);
                MyDisplay.RichLogs(" ", Color.Black, true, true);
                MyDisplay.RichLogs("Process Stoped!", Color.Red, true, true);
                MyDisplay.RichLogs(" ", Color.Black, true, true);
            }
        }

        #region Tab Download


        private void TxtFDL1Address_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtFDL1Address.Text))
            {
                uni.Prepare_Exploit(TxtFDL1Address.Text);
            }
        }

        private void TxtFDL2Address_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtFDL2Address.Text))
            {
                uni.fdl2_addr = TxtFDL2Address.Text;
            }
        }

        private void BtnFlashPartition_Click(object sender, EventArgs e)
        {
            if (!UnisocWorker.IsBusy)
            {
                bool flag = false;

                foreach (DataGridViewRow item in DataView.Rows)
                {
                    if (CkKeepNV.Checked)
                    {
                        if (
                            item.Cells[1].Value.ToString().ToLower().Contains("nv")
                            || item.Cells[2].Value.ToString().ToLower().Contains("nv")
                            || item.Cells[1].Value.ToString().ToLower().Contains("modem")
                            || item.Cells[2].Value.ToString().ToLower().Contains("modem")
                            || item.Cells[1].Value.ToString().ToLower().Contains("dsp")
                            || item.Cells[2].Value.ToString().ToLower().Contains("dsp")
                            || item.Cells[1].Value.ToString().ToLower().Contains("wcn")
                            || item.Cells[2].Value.ToString().ToLower().Contains("wcn")
                            || item.Cells[1].Value.ToString().ToLower().Contains("lte")
                            || item.Cells[2].Value.ToString().ToLower().Contains("lte")
                            || item.Cells[1].Value.ToString().ToLower().Contains("pm_")
                            || item.Cells[2].Value.ToString().ToLower().Contains("pm_")
                            || item.Cells[1].Value.ToString().ToLower().Contains("g_")
                            || item.Cells[2].Value.ToString().ToLower().Contains("g_")
                            || item.Cells[1].Value.ToString().ToLower().Contains("l_")
                            || item.Cells[2].Value.ToString().ToLower().Contains("l_")
                            || item.Cells[1].Value.ToString().ToLower().Contains("w_")
                            || item.Cells[2].Value.ToString().ToLower().Contains("w_")
                            || item.Cells[1].Value.ToString().ToLower().Contains("t_")
                            || item.Cells[2].Value.ToString().ToLower().Contains("t_")
                            || item.Cells[1].Value.ToString().ToLower().Contains("miscdata")
                            || item.Cells[2].Value.ToString().ToLower().Contains("miscdata")
                        )
                        {
                            item.Cells[0].Value = false;
                        }
                    }
                }

                foreach (DataGridViewRow item in DataView.Rows)
                {
                    if (Convert.ToBoolean(item.Cells[0].Value) == true && File.Exists(item.Cells[6].Value.ToString()))
                    {
                        flag = true;
                    }
                }

                if (flag)
                {
                    MyDisplay.RtbClear();
                    MyDisplay.GetButtonText(sender);
                    MyProgress.ProcessBar1(0);
                    MyProgress.ProcessBar2(0);
                    totalchecked = 0;
                    totaldo = 0;
                    uni.uniCommand = string.Empty;
                    uni.uniCommand = string.Concat(uni.uniCommand, "-progress -wait 5 -timeout ", uni.Timeout.Replace(" ", "") + " ");

                    if (CkAutoRSAExploit.Checked)
                    {
                        if (!string.IsNullOrEmpty(TxtFDL1Address.Text))
                        {
                            if (uni.Prepare_Exploit(TxtFDL1Address.Text))
                            {
                                uni.uniCommand = string.Concat(
                                    uni.uniCommand,
                                    "-exploit " + uni.exploit + " "
                                );
                                totalchecked += 1;
                            }
                        }
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
                        totalchecked += 2;
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
                        totalchecked += 1;
                    }

                    if (CkRepartition.Checked)
                    {
                        uni.uniCommand = string.Concat(
                            uni.uniCommand,
                            "-repartition " + "\"" + UniFileXML + "\"" + " "
                        );
                        totalchecked += 1;
                    }

                    foreach (DataGridViewRow item in DataView.Rows)
                    {
                        if (Convert.ToBoolean(item.Cells[DataView.Columns[0].Index].Value) == true && File.Exists(item.Cells[DataView.Columns[6].Index].Value.ToString()))
                        {
                            FileInfo myInfo = new FileInfo(item.Cells[DataView.Columns[6].Index].Value.ToString());
                            if (myInfo.Length > 512)
                            {
                                uni.uniCommand = string.Concat(
                                    uni.uniCommand,
                                    "-w"
                                        + " "
                                        + item.Cells[DataView.Columns[2].Index].Value.ToString()
                                        + " "
                                        + "\""
                                        + item.Cells[DataView.Columns[6].Index].Value.ToString()
                                        + "\""
                                        + " "
                                );
                                totalchecked += 1;
                            }
                        }
                    }

                    totalchecked += 1;
                    uni.uniCommand = uni.uniCommand.Substring(0, uni.uniCommand.Length - 1);

                    UnisocWorker.RunWorkerAsync();
                    UnisocWorker.Dispose();
                }
                else
                {
                    MessageBox.Show(
                        "Flash files doesn't exist!",
                        "iReverse Unisoc Ultimate Download Tool - [HadiK-IT]",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            else
            {
                MessageBox.Show(
                    "Worker is running.",
                    "iReverse Unisoc Ultimate Download Tool - [HadiK-IT]",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void BtnReadPartition_Click(object sender, EventArgs e)
        {
            if (!UnisocWorker.IsBusy && !CkFDLLoaded.Checked)
            {
                bool flag = false;
                var readfromlist = false;

                foreach (DataGridViewRow item in DataView.Rows)
                {
                    if (CkKeepNV.Checked)
                    {
                        if (
                            item.Cells[1].Value.ToString().ToLower().Contains("nv")
                            || item.Cells[2].Value.ToString().ToLower().Contains("nv")
                            || item.Cells[1].Value.ToString().ToLower().Contains("modem")
                            || item.Cells[2].Value.ToString().ToLower().Contains("modem")
                            || item.Cells[1].Value.ToString().ToLower().Contains("dsp")
                            || item.Cells[2].Value.ToString().ToLower().Contains("dsp")
                            || item.Cells[1].Value.ToString().ToLower().Contains("wcn")
                            || item.Cells[2].Value.ToString().ToLower().Contains("wcn")
                            || item.Cells[1].Value.ToString().ToLower().Contains("lte")
                            || item.Cells[2].Value.ToString().ToLower().Contains("lte")
                            || item.Cells[1].Value.ToString().ToLower().Contains("pm_")
                            || item.Cells[2].Value.ToString().ToLower().Contains("pm_")
                            || item.Cells[1].Value.ToString().ToLower().Contains("g_")
                            || item.Cells[2].Value.ToString().ToLower().Contains("g_")
                            || item.Cells[1].Value.ToString().ToLower().Contains("l_")
                            || item.Cells[2].Value.ToString().ToLower().Contains("l_")
                            || item.Cells[1].Value.ToString().ToLower().Contains("w_")
                            || item.Cells[2].Value.ToString().ToLower().Contains("w_")
                            || item.Cells[1].Value.ToString().ToLower().Contains("t_")
                            || item.Cells[2].Value.ToString().ToLower().Contains("t_")
                            || item.Cells[1].Value.ToString().ToLower().Contains("miscdata")
                            || item.Cells[2].Value.ToString().ToLower().Contains("miscdata")
                        )
                        {
                            item.Cells[0].Value = true;
                        }
                    }
                }

                foreach (DataGridViewRow item in DataView.Rows)
                {
                    if (Convert.ToBoolean(item.Cells[0].Value) == true)
                    {
                        flag = true;
                    }
                }

                if (flag)
                {
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
                    {
                        ShowNewFolderButton = true
                    };

                    if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        MyDisplay.RtbClear();
                        MyDisplay.GetButtonText(sender);
                        MyProgress.ProcessBar1(0);
                        MyProgress.ProcessBar2(0);
                        totalchecked = 0;
                        totaldo = 0;

                        uni.uniCommand = string.Empty;
                        uni.uniCommand = string.Concat(uni.uniCommand, "-progress -wait 5 -timeout ", uni.Timeout.Replace(" ", "") + " ");

                        if (CkAutoRSAExploit.Checked)
                        {
                            if (!string.IsNullOrEmpty(TxtFDL1Address.Text))
                            {
                                if (uni.Prepare_Exploit(TxtFDL1Address.Text))
                                {
                                    uni.uniCommand = string.Concat(
                                        uni.uniCommand,
                                        "-exploit " + uni.exploit + " "
                                    );
                                    totalchecked += 1;
                                }
                            }
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
                            totalchecked += 2;
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
                            totalchecked += 1;
                        }

                        UniFoldersave = folderBrowserDialog.SelectedPath;

                        foreach (DataGridViewRow item in DataView.Rows)
                        {
                            if (Convert.ToBoolean(item.Cells[DataView.Columns[0].Index].Value) == true)
                            {
                                if (readfromlist)
                                {
                                    uni.uniCommand = string.Concat(
                                        uni.uniCommand,
                                        "-rsize"
                                            + " "
                                            + item.Cells[DataView.Columns[2].Index].Value.ToString()
                                            + " "
                                            + "0"
                                            + " "
                                            + item.Cells[DataView.Columns[5].Index].Value.ToString().Replace("B", string.Empty)
                                            + " "
                                            + "\""
                                            + UniFoldersave
                                            + "\\"
                                            + item.Cells[DataView.Columns[2].Index].Value.ToString()
                                            + ".bin"
                                            + "\""
                                            + " "
                                    );
                                }
                                else
                                {
                                    uni.uniCommand = string.Concat(
                                        uni.uniCommand,
                                        "-r"
                                            + " "
                                            + item.Cells[DataView.Columns[2].Index].Value.ToString()
                                            + " "
                                            + "\""
                                            + UniFoldersave
                                            + "\\"
                                            + item.Cells[DataView.Columns[2].Index].Value.ToString()
                                            + ".bin"
                                            + "\""
                                            + " "
                                    );
                                }
                                totalchecked += 1;
                            }
                        }

                        totalchecked += 1;
                        uni.uniCommand = uni.uniCommand.Substring(0, uni.uniCommand.Length - 1);

                        UnisocWorker.RunWorkerAsync();
                        UnisocWorker.Dispose();
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "Worker is running or FDL is loaded."
                        + "\n"
                        + "Please stop worker & reconnect device to download mode before continue!",
                    "iReverse Unisoc Ultimate Download Tool - [HadiK-IT]",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void BtnErase_Click(object sender, EventArgs e)
        {
            if (!UnisocWorker.IsBusy)
            {
                bool flag = false;

                foreach (DataGridViewRow item in DataView.Rows)
                {
                    if (CkKeepNV.Checked)
                    {
                        if (
                            item.Cells[1].Value.ToString().ToLower().Contains("nv")
                            || item.Cells[2].Value.ToString().ToLower().Contains("nv")
                            || item.Cells[1].Value.ToString().ToLower().Contains("modem")
                            || item.Cells[2].Value.ToString().ToLower().Contains("modem")
                            || item.Cells[1].Value.ToString().ToLower().Contains("dsp")
                            || item.Cells[2].Value.ToString().ToLower().Contains("dsp")
                            || item.Cells[1].Value.ToString().ToLower().Contains("wcn")
                            || item.Cells[2].Value.ToString().ToLower().Contains("wcn")
                            || item.Cells[1].Value.ToString().ToLower().Contains("lte")
                            || item.Cells[2].Value.ToString().ToLower().Contains("lte")
                            || item.Cells[1].Value.ToString().ToLower().Contains("pm_")
                            || item.Cells[2].Value.ToString().ToLower().Contains("pm_")
                            || item.Cells[1].Value.ToString().ToLower().Contains("g_")
                            || item.Cells[2].Value.ToString().ToLower().Contains("g_")
                            || item.Cells[1].Value.ToString().ToLower().Contains("l_")
                            || item.Cells[2].Value.ToString().ToLower().Contains("l_")
                            || item.Cells[1].Value.ToString().ToLower().Contains("w_")
                            || item.Cells[2].Value.ToString().ToLower().Contains("w_")
                            || item.Cells[1].Value.ToString().ToLower().Contains("t_")
                            || item.Cells[2].Value.ToString().ToLower().Contains("t_")
                            || item.Cells[1].Value.ToString().ToLower().Contains("miscdata")
                            || item.Cells[2].Value.ToString().ToLower().Contains("miscdata")
                        )
                        {
                            item.Cells[0].Value = false;
                        }
                    }
                }

                foreach (DataGridViewRow item in DataView.Rows)
                {
                    if (Convert.ToBoolean(item.Cells[0].Value) == true)
                    {
                        flag = true;
                    }
                }

                if (flag)
                {
                    MyDisplay.RtbClear();
                    MyDisplay.GetButtonText(sender);
                    MyProgress.ProcessBar1(0);
                    MyProgress.ProcessBar2(0);
                    totalchecked = 0;
                    totaldo = 0;

                    uni.uniCommand = string.Empty;
                    uni.uniCommand = string.Concat(uni.uniCommand, "-progress -wait 5 -timeout ", uni.Timeout.Replace(" ", "") + " ");

                    if (CkAutoRSAExploit.Checked)
                    {
                        if (!string.IsNullOrEmpty(TxtFDL1Address.Text))
                        {
                            if (uni.Prepare_Exploit(TxtFDL1Address.Text))
                            {
                                uni.uniCommand = string.Concat(
                                    uni.uniCommand,
                                    "-exploit " + uni.exploit + " "
                                );
                                totalchecked += 1;
                            }
                        }
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
                        totalchecked += 2;
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
                        totalchecked += 1;
                    }

                    if (CkRepartition.Checked)
                    {
                        uni.uniCommand = string.Concat(uni.uniCommand, "-repartition " + "\"" + UniFileXML + "\"" + " ");
                        totalchecked += 1;
                    }

                    foreach (DataGridViewRow item in DataView.Rows)
                    {
                        if (Convert.ToBoolean(item.Cells[DataView.Columns[0].Index].Value) == true)
                        {
                            uni.uniCommand = string.Concat(uni.uniCommand, "-e" + " " + item.Cells[DataView.Columns[2].Index].Value.ToString() + " ");
                            totalchecked += 1;
                        }
                    }

                    totalchecked += 1;
                    uni.uniCommand = uni.uniCommand.Substring(0, uni.uniCommand.Length - 1);

                    UnisocWorker.RunWorkerAsync();
                    UnisocWorker.Dispose();
                }
            }
            else
            {
                MessageBox.Show(
                    "Worker is running.",
                    "iReverse Unisoc Ultimate Download Tool - [HadiK-IT]",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void BtnEraseFRPAccount_Click(object sender, EventArgs e)
        {
            if (!UnisocWorker.IsBusy)
            {
                if (File.Exists(uni.fdl1_location) && !string.IsNullOrEmpty(uni.fdl1_addr))
                {
                    MyDisplay.RtbClear();
                    MyDisplay.GetButtonText(sender);
                    MyProgress.ProcessBar1(0);
                    MyProgress.ProcessBar2(0);
                    totalchecked = 0;
                    totaldo = 0;

                    uni.uniCommand = string.Empty;
                    uni.uniCommand = string.Concat(uni.uniCommand, "-progress -wait 5 -timeout ", uni.Timeout.Replace(" ", "") + " ");

                    if (CkAutoRSAExploit.Checked)
                    {
                        if (!string.IsNullOrEmpty(TxtFDL1Address.Text))
                        {
                            if (uni.Prepare_Exploit(TxtFDL1Address.Text))
                            {
                                uni.uniCommand = string.Concat(uni.uniCommand, "-exploit " + uni.exploit + " ");
                                totalchecked += 1;
                            }
                        }
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
                        totalchecked += 2;
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
                        totalchecked += 1;
                    }

                    totalchecked += 2;
                    uni.uniCommand = string.Concat(uni.uniCommand, "-erase_frp");
                    UnisocWorker.RunWorkerAsync();
                    UnisocWorker.Dispose();
                }
            }
            else
            {
                MessageBox.Show(
                    "Worker is running.",
                    "iReverse Unisoc Ultimate Download Tool - [HadiK-IT]",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void BtnIdentify_Click(object sender, EventArgs e)
        {
            if (!UnisocWorker.IsBusy)
            {
                if (File.Exists(uni.fdl1_location) && !string.IsNullOrEmpty(uni.fdl1_addr))
                {
                    MyDisplay.RtbClear();
                    DataView.Rows.Clear();
                    MyDisplay.GetButtonText(sender);
                    MyProgress.ProcessBar1(0);
                    MyProgress.ProcessBar2(0);
                    totalchecked = 0;
                    totaldo = 0;

                    uni.uniCommand = string.Empty;
                    uni.uniCommand = string.Concat(uni.uniCommand, "-progress -wait 5 -timeout ", uni.Timeout.Replace(" ", "") + " ");

                    if (CkAutoRSAExploit.Checked)
                    {
                        if (!string.IsNullOrEmpty(TxtFDL1Address.Text))
                        {
                            if (uni.Prepare_Exploit(TxtFDL1Address.Text))
                            {
                                uni.uniCommand = string.Concat(uni.uniCommand, "-exploit " + uni.exploit + " ");
                                totalchecked += 1;
                            }
                        }
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
                        totalchecked += 2;
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
                        totalchecked += 1;
                    }

                    totalchecked += 2;

                    uni.uniCommand = string.Concat(uni.uniCommand, "-gpt -get_deviceinfo " + "\"" + uni.Temp + "\\boot.img" + "\"");

                    UnisocWorker.RunWorkerAsync();
                    UnisocWorker.Dispose();
                }
                else
                {
                    Console.WriteLine("Pleace check fdl1 location : " + uni.fdl1_location);
                    Console.WriteLine("Pleace check fdl1 address  : " + uni.fdl1_addr);
                }
            }
            else
            {
                MessageBox.Show(
                    "Worker is running.",
                    "iReverse Unisoc Ultimate Download Tool - [HadiK-IT]",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void BtnFDL1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Select FDL 1 File",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
                FileName = "*.bin*",
                Filter = "FDL 1 |*.bin* ",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MyDisplay.RtbClear();
                string[] filenames = Directory.GetFiles(
                    Path.GetDirectoryName(openFileDialog.FileName),
                    "*.*",
                    SearchOption.TopDirectoryOnly
                );
                foreach (string filename in filenames)
                {
                    string ext = Path.GetExtension(filename);
                    string tmp = null;
                    if (ext == ".txt")
                    {
                        if (filename.Contains("fdl1-addr.txt"))
                        {
                            tmp = File.ReadAllText(filename);
                            if (tmp.Contains("0x"))
                            {
                                uni.fdl1_addr = tmp;
                                TxtFDL1Address.Text = tmp;
                            }
                        }
                        else if (filename.Contains("fdl2-addr.txt"))
                        {
                            tmp = File.ReadAllText(filename);
                            if (tmp.Contains("0x"))
                            {
                                uni.fdl2_addr = tmp;
                                TxtFDL2Address.Text = tmp;
                            }
                        }
                    }
                    else if (ext == ".bin")
                    {
                        if (filename.Contains("fdl2.bin") || filename.Contains("fdl2-sign.bin"))
                        {
                            string fdl2 = filename;
                            int position = fdl2.LastIndexOf("\\");

                            if (position > -1)
                            {
                                fdl2 = fdl2.Substring(position + 1);
                                fdl2 = fdl2.Replace(" ", "");
                            }

                            TxtFDL2.Text = fdl2;
                            uni.fdl2_location = filename;
                        }
                    }
                    else if (ext == ".xml")
                    {
                        if (filename.Contains("partitions.xml"))
                        {
                            if (DataView.Rows.Count > 0)
                            {
                                MyProgress.DGVClear();
                            }
                            XmlReader xmlReader = XmlReader.Create(filename);
                            string text = null;
                            bool chkd = false;

                            UniFileXML = Application.StartupPath + "\\Temp\\repartition.xml";
                            if (File.Exists(UniFileXML))
                            {
                                File.Delete(UniFileXML);
                            }
                            StreamWriter files = new StreamWriter(UniFileXML);

                            files.WriteLine("<?xml version=\"1.0\" ?>");
                            files.WriteLine("<!--NOTE: Genererate by HadiK IT **-->");
                            files.WriteLine("<Partitions>");

                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType != XmlNodeType.Element|| xmlReader.Name != "Partition")
                                {
                                    continue;
                                }
                                if (File.Exists(Path.GetDirectoryName(openFileDialog.FileName) + "\\" + xmlReader.GetAttribute("location"))
                                )
                                {
                                    text = Path.GetDirectoryName(openFileDialog.FileName) + "\\" + xmlReader.GetAttribute("location");
                                    chkd = true;
                                }
                                else
                                {
                                    text = "none";
                                    chkd = false;
                                }

                                DataView.Rows.Add(
                                    chkd,
                                    xmlReader.GetAttribute("fileids"),
                                    xmlReader.GetAttribute("partition"),
                                    xmlReader.GetAttribute("sector"),
                                    xmlReader.GetAttribute("length"),
                                    xmlReader.GetAttribute("filesize"),
                                    text
                                );

                                if (xmlReader.GetAttribute("partition") == "splloader" || xmlReader.GetAttribute("partition") == "uboot")
                                {
                                    Console.WriteLine("Result : " + xmlReader.GetAttribute("partition") + " must be ignored!");
                                }
                                else
                                {
                                    Console.WriteLine("Result : " + xmlReader.GetAttribute("partition"));
                                    string Result = "<Partition ";
                                    Result += string.Concat("id=\"" + xmlReader.GetAttribute("partition") + "\" ");
                                    if (xmlReader.GetAttribute("partition") == "userdata")
                                    {
                                        Result += string.Concat("size=\"" + "0xFFFFFFFF" + "\"");
                                    }
                                    else
                                    {
                                        Result += string.Concat(
                                            "size=\""
                                                + xmlReader
                                                    .GetAttribute("filesize")
                                                    .Replace("MB", string.Empty)
                                                + "\""
                                        );
                                    }
                                    Result += string.Concat("/>");
                                    files.WriteLine(Result);
                                }
                            }

                            files.WriteLine("</Partitions>");
                            files.Close();
                            CkRepartition.Checked = false;
                        }
                    }
                }

                TxtFDL1.Text = openFileDialog.SafeFileName;
                uni.fdl1_location = openFileDialog.FileName;
            }
        }

        private void BtnFDL2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Select FDL 2 File",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
                FileName = "*.bin*",
                Filter = "FDL 2 |*.bin* ",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MyDisplay.RtbClear();
                string[] filenames = Directory.GetFiles(
                    Path.GetDirectoryName(openFileDialog.FileName),
                    "*.*",
                    SearchOption.TopDirectoryOnly
                );
                foreach (string filename in filenames)
                {
                    string ext = Path.GetExtension(filename);
                    string tmp = null;
                    if (ext == ".txt")
                    {
                        if (filename.Contains("fdl1-addr.txt"))
                        {
                            tmp = File.ReadAllText(filename);
                            if (tmp.Contains("0x"))
                            {
                                uni.fdl1_addr = tmp;
                                TxtFDL1Address.Text = tmp;
                            }
                        }
                        else if (filename.Contains("fdl2-addr.txt"))
                        {
                            tmp = File.ReadAllText(filename);
                            if (tmp.Contains("0x"))
                            {
                                uni.fdl2_addr = tmp;
                                TxtFDL2Address.Text = tmp;
                            }
                        }
                    }
                    else if (ext == ".bin")
                    {
                        if (filename.Contains("fdl1.bin") || filename.Contains("fdl1-sign.bin"))
                        {
                            string fdl1 = filename;
                            int position = fdl1.LastIndexOf("\\");

                            if (position > -1)
                            {
                                fdl1 = fdl1.Substring(position + 1);
                                fdl1 = fdl1.Replace(" ", "");
                            }

                            TxtFDL1.Text = fdl1;
                            uni.fdl1_location = filename;
                        }
                    }
                    else if (ext == ".xml")
                    {
                        if (filename.Contains("partitions.xml"))
                        {
                            if (DataView.Rows.Count > 0)
                            {
                                MyProgress.DGVClear();
                            }
                            XmlReader xmlReader = XmlReader.Create(filename);
                            string text = null;
                            bool chkd = false;

                            UniFileXML = Application.StartupPath + "\\Temp\\repartition.xml";
                            if (File.Exists(UniFileXML))
                            {
                                File.Delete(UniFileXML);
                            }
                            StreamWriter files = new StreamWriter(UniFileXML);

                            files.WriteLine("<?xml version=\"1.0\" ?>");
                            files.WriteLine("<!--NOTE: Genererate by HadiK IT **-->");
                            files.WriteLine("<Partitions>");

                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType != XmlNodeType.Element || xmlReader.Name != "Partition")
                                {
                                    continue;
                                }
                                if (File.Exists( Path.GetDirectoryName(openFileDialog.FileName) + "\\" + xmlReader.GetAttribute("location")))
                                {
                                    text = Path.GetDirectoryName(openFileDialog.FileName) + "\\" + xmlReader.GetAttribute("location");
                                    chkd = true;
                                }
                                else
                                {
                                    text = "none";
                                    chkd = false;
                                }

                                DataView.Rows.Add
                                (
                                    chkd,
                                    xmlReader.GetAttribute("fileids"),
                                    xmlReader.GetAttribute("partition"),
                                    xmlReader.GetAttribute("sector"),
                                    xmlReader.GetAttribute("length"),
                                    xmlReader.GetAttribute("filesize"),
                                    text
                                );

                                if (xmlReader.GetAttribute("partition") == "splloader" || xmlReader.GetAttribute("partition") == "uboot")
                                {
                                    Console.WriteLine("Result : " + xmlReader.GetAttribute("partition") + " must be ignored!");
                                }
                                else
                                {
                                    Console.WriteLine("Result : " + xmlReader.GetAttribute("partition"));
                                    string Result = "<Partition ";
                                    Result += string.Concat("id=\"" + xmlReader.GetAttribute("partition") + "\" ");
                                    if (xmlReader.GetAttribute("partition") == "userdata")
                                    {
                                        Result += string.Concat("size=\"" + "0xFFFFFFFF" + "\"");
                                    }
                                    else
                                    {
                                        Result += string.Concat(
                                            "size=\""
                                                + xmlReader
                                                    .GetAttribute("filesize")
                                                    .Replace("MB", string.Empty)
                                                + "\""
                                        );
                                    }
                                    Result += string.Concat("/>");
                                    files.WriteLine(Result);
                                }
                            }

                            files.WriteLine("</Partitions>");
                            files.Close();
                            CkRepartition.Checked = false;
                        }
                    }
                }

                TxtFDL2.Text = openFileDialog.SafeFileName;
                uni.fdl2_location = openFileDialog.FileName;
            }
        }

        private void BtnPACFirmware_Click(object sender, EventArgs e)
        {
            if (!UnisocWorker.IsBusy)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Title = "Select PAC Firmware",
                    InitialDirectory = Environment.GetFolderPath(
                        Environment.SpecialFolder.MyComputer
                    ),
                    FileName = "*.*",
                    Filter = "PAC Firmware |*.pac* ",
                    FilterIndex = 2,
                    RestoreDirectory = true
                };
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    MyProgress.DGVClear();
                    MyDisplay.RtbClear();
                    MyDisplay.GetButtonText(sender);
                    MyProgress.ProcessBar1(0);
                    WorkerMethod = "PAC Firmware";
                    TxtPacFirmware.Text = openFileDialog.SafeFileName;
                    UniFirmware = openFileDialog.FileName;
                    UnisocWorker.RunWorkerAsync();
                    UnisocWorker.Dispose();
                }
            }
        }

        #endregion

        #region Tab Diag Tool

        private void CkDiagConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (CkDiagConnected.CheckState == CheckState.Checked)
            {
                if (string.IsNullOrEmpty(ComboPort.Text))
                {
                    CkDiagConnected.Checked = false;
                }
                MyDisplay.PreventSleep();
            }
            else
            {
                MyDisplay.AllowSleep();
            }
        }

        private void BtnFactoryReset_Click(object sender, EventArgs e)
        {
            if (!UnisocWorker.IsBusy)
            {
                MyDisplay.RtbClear();
                MyDisplay.GetButtonText(sender);
                MyProgress.ProcessBar1(0);
                MyProgress.ProcessBar2(0);
                WorkerMethod = "Factory Reset";
                UnisocWorker.RunWorkerAsync();
                UnisocWorker.Dispose();
            }
        }

        private void BtnBtnPowerOff_Click(object sender, EventArgs e)
        {
            if (!UnisocWorker.IsBusy)
            {
                MyDisplay.RtbClear();
                MyDisplay.GetButtonText(sender);
                MyProgress.ProcessBar1(0);
                MyProgress.ProcessBar2(0);
                WorkerMethod = "Power Off";
                UnisocWorker.RunWorkerAsync();
                UnisocWorker.Dispose();
            }
        }

        private void BtnSendATCommand_Click(object sender, EventArgs e)
        {
            if (!UnisocWorker.IsBusy && !(string.IsNullOrEmpty(TxtATCommand.Text)))
            {
                MyDisplay.RtbClear();
                MyDisplay.GetButtonText(sender);
                MyProgress.ProcessBar1(0);
                MyProgress.ProcessBar2(0);
                WorkerMethod = "Send ATCommand";
                UnisocWorker.RunWorkerAsync();
                UnisocWorker.Dispose();
                CkDiagConnected.Checked = false;
            }
        }

        private void BtnReadIMEI_Click(object sender, EventArgs e)
        {
            if (!UnisocWorker.IsBusy)
            {
                MyDisplay.RtbClear();
                MyDisplay.GetButtonText(sender);
                TxtIMEI1.Text = string.Empty;
                TxtIMEI2.Text = string.Empty;
                MyProgress.ProcessBar1(0);
                MyProgress.ProcessBar2(0);
                WorkerMethod = "Read IMEI";
                UnisocWorker.RunWorkerAsync();
                UnisocWorker.Dispose();
            }
        }

        private void BtnWriteIMEI1_Click(object sender, EventArgs e)
        {
            if (!UnisocWorker.IsBusy)
            {
                MyDisplay.RtbClear();
                MyDisplay.GetButtonText(sender);
                MyProgress.ProcessBar1(0);
                MyProgress.ProcessBar2(0);
                WorkerMethod = "Write IMEI 1";
                UnisocWorker.RunWorkerAsync();
                UnisocWorker.Dispose();
            }
        }

        private void BtnWriteIMEI2_Click(object sender, EventArgs e)
        {
            if (!UnisocWorker.IsBusy)
            {
                MyDisplay.RtbClear();
                MyDisplay.GetButtonText(sender);
                MyProgress.ProcessBar1(0);
                MyProgress.ProcessBar2(0);
                WorkerMethod = "Write IMEI 2";
                UnisocWorker.RunWorkerAsync();
                UnisocWorker.Dispose();
            }
        }

        private void BtnEnterDiagMode_Click(object sender, EventArgs e)
        {
            if (!UnisocWorker.IsBusy)
            {
                MyDisplay.RtbClear();
                MyDisplay.GetButtonText(sender);
                MyProgress.ProcessBar1(0);
                MyProgress.ProcessBar2(0);
                WorkerMethod = "Enter Diag Mode";
                UnisocWorker.RunWorkerAsync();
                UnisocWorker.Dispose();
            }
        }

        #endregion
    }
}
