
using iReverse_Unisoc_Ultimate.CustomControls.iReverseControls;
using System.Windows.Forms;

namespace iReverse_Unisoc_Ultimate
{
	public partial class Main : System.Windows.Forms.Form
	{
		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.ComboPort = new System.Windows.Forms.ComboBox();
            this.UnisocWorker = new System.ComponentModel.BackgroundWorker();
            this.LabelTimer = new System.Windows.Forms.Label();
            this.GroupBoxFlash = new System.Windows.Forms.GroupBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.TxtFDL2Address = new System.Windows.Forms.TextBox();
            this.TxtPacFirmware = new System.Windows.Forms.TextBox();
            this.TxtFDL2 = new System.Windows.Forms.TextBox();
            this.TxtFDL1Address = new System.Windows.Forms.TextBox();
            this.TxtFDL1 = new System.Windows.Forms.TextBox();
            this.Logs = new System.Windows.Forms.RichTextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.ReceiverDataWorker = new System.ComponentModel.BackgroundWorker();
            this.ProgresbarWorker = new System.ComponentModel.BackgroundWorker();
            this.label11 = new System.Windows.Forms.Label();
            this.comboDownloadTimeout = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel_header = new System.Windows.Forms.Panel();
            this.label_title = new System.Windows.Forms.Label();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.iReverseButtonMinimize = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.iReverseToggleButton_SetTheme = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseToggleButton();
            this.iReverseButtonClose = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.BtnStop = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.CkRepartition = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseToggleButton();
            this.CkAutoReboot = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseToggleButton();
            this.CkAutoRSAExploit = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseToggleButton();
            this.BtnDeviceManager = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.BtnInstallDriver = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.CkDiagConnected = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseToggleButton();
            this.CkFDLLoaded = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseToggleButton();
            this.RdDiagChannel = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseRadioButton();
            this.RdDownload = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseRadioButton();
            this.IReverseProgressBar2 = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseProgressBar();
            this.IReverseProgressBar1 = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseProgressBar();
            this.TabControl1 = new System.Windows.Forms.CustomTabControl();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.labelTotal = new System.Windows.Forms.Label();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.PanelSPDOneClick = new System.Windows.Forms.Panel();
            this.txtSearchListBox = new System.Windows.Forms.TextBox();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.listBoxOneClick = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseListBox();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.BtnPowerOff = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.BtnSendATCommand = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.BtnFactoryReset = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnEnterDiagMode = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.RdFactoryTestMode = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseRadioButton();
            this.RdCalibrationMode = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseRadioButton();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnWriteIMEI2 = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.BtnReadIMEI2 = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.BtnWriteIMEI1 = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.BtnReadIMEI1 = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.TxtIMEI2 = new System.Windows.Forms.TextBox();
            this.TxtIMEI1 = new System.Windows.Forms.TextBox();
            this.TxtATCommand = new System.Windows.Forms.TextBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.CkPartition = new System.Windows.Forms.CheckBox();
            this.DataView = new System.Windows.Forms.DataGridView();
            this.Ck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnFlashPartition = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.BtnPACFirmware = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.BtnIdentify = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.CkKeepNV = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseToggleButton();
            this.BtnReadPartition = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.BtnFDL2 = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.BtnErase = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.BtnEraseFRPAccount = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.BtnFDL1 = new iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.iReverseButton();
            this.GroupBoxFlash.SuspendLayout();
            this.panel_header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.TabControl1.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataView)).BeginInit();
            this.SuspendLayout();
            // 
            // ComboPort
            // 
            this.ComboPort.FormattingEnabled = true;
            this.ComboPort.Location = new System.Drawing.Point(841, 46);
            this.ComboPort.Name = "ComboPort";
            this.ComboPort.Size = new System.Drawing.Size(272, 21);
            this.ComboPort.TabIndex = 1;
            this.ComboPort.SelectedIndexChanged += new System.EventHandler(this.ComboPort_SelectedIndexChanged);
            // 
            // UnisocWorker
            // 
            this.UnisocWorker.WorkerReportsProgress = true;
            this.UnisocWorker.WorkerSupportsCancellation = true;
            // 
            // LabelTimer
            // 
            this.LabelTimer.Location = new System.Drawing.Point(1065, 50);
            this.LabelTimer.Name = "LabelTimer";
            this.LabelTimer.Size = new System.Drawing.Size(31, 13);
            this.LabelTimer.TabIndex = 5;
            this.LabelTimer.Text = "[  ]";
            // 
            // GroupBoxFlash
            // 
            this.GroupBoxFlash.Controls.Add(this.BtnFlashPartition);
            this.GroupBoxFlash.Controls.Add(this.BtnPACFirmware);
            this.GroupBoxFlash.Controls.Add(this.BtnIdentify);
            this.GroupBoxFlash.Controls.Add(this.CkKeepNV);
            this.GroupBoxFlash.Controls.Add(this.BtnReadPartition);
            this.GroupBoxFlash.Controls.Add(this.BtnFDL2);
            this.GroupBoxFlash.Controls.Add(this.BtnErase);
            this.GroupBoxFlash.Controls.Add(this.Label18);
            this.GroupBoxFlash.Controls.Add(this.BtnEraseFRPAccount);
            this.GroupBoxFlash.Controls.Add(this.BtnFDL1);
            this.GroupBoxFlash.Controls.Add(this.Label4);
            this.GroupBoxFlash.Controls.Add(this.Label2);
            this.GroupBoxFlash.Controls.Add(this.Label3);
            this.GroupBoxFlash.Controls.Add(this.Label5);
            this.GroupBoxFlash.Controls.Add(this.Label1);
            this.GroupBoxFlash.Controls.Add(this.TxtFDL2Address);
            this.GroupBoxFlash.Controls.Add(this.TxtPacFirmware);
            this.GroupBoxFlash.Controls.Add(this.TxtFDL2);
            this.GroupBoxFlash.Controls.Add(this.TxtFDL1Address);
            this.GroupBoxFlash.Controls.Add(this.TxtFDL1);
            this.GroupBoxFlash.Location = new System.Drawing.Point(12, 562);
            this.GroupBoxFlash.Name = "GroupBoxFlash";
            this.GroupBoxFlash.Size = new System.Drawing.Size(1101, 102);
            this.GroupBoxFlash.TabIndex = 9;
            this.GroupBoxFlash.TabStop = false;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(1012, 21);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(49, 13);
            this.Label18.TabIndex = 38;
            this.Label18.Text = "Keep NV";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(558, 48);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(79, 13);
            this.Label4.TabIndex = 12;
            this.Label4.Text = "FDL2 Address";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(558, 21);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(79, 13);
            this.Label2.TabIndex = 13;
            this.Label2.Text = "FDL1 Address";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(6, 48);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(31, 13);
            this.Label3.TabIndex = 14;
            this.Label3.Text = "FDL2";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(6, 74);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(79, 13);
            this.Label5.TabIndex = 15;
            this.Label5.Text = "PAC Firmware";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(6, 22);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(31, 13);
            this.Label1.TabIndex = 15;
            this.Label1.Text = "FDL1";
            // 
            // TxtFDL2Address
            // 
            this.TxtFDL2Address.Location = new System.Drawing.Point(638, 45);
            this.TxtFDL2Address.Name = "TxtFDL2Address";
            this.TxtFDL2Address.Size = new System.Drawing.Size(453, 20);
            this.TxtFDL2Address.TabIndex = 8;
            this.TxtFDL2Address.Text = "0x00000000";
            this.TxtFDL2Address.TextChanged += new System.EventHandler(this.TxtFDL2Address_TextChanged);
            // 
            // TxtPacFirmware
            // 
            this.TxtPacFirmware.Location = new System.Drawing.Point(88, 71);
            this.TxtPacFirmware.Name = "TxtPacFirmware";
            this.TxtPacFirmware.Size = new System.Drawing.Size(422, 20);
            this.TxtPacFirmware.TabIndex = 9;
            // 
            // TxtFDL2
            // 
            this.TxtFDL2.Location = new System.Drawing.Point(88, 45);
            this.TxtFDL2.Name = "TxtFDL2";
            this.TxtFDL2.Size = new System.Drawing.Size(422, 20);
            this.TxtFDL2.TabIndex = 9;
            // 
            // TxtFDL1Address
            // 
            this.TxtFDL1Address.Location = new System.Drawing.Point(638, 19);
            this.TxtFDL1Address.Name = "TxtFDL1Address";
            this.TxtFDL1Address.Size = new System.Drawing.Size(367, 20);
            this.TxtFDL1Address.TabIndex = 10;
            this.TxtFDL1Address.Text = "0x00000000";
            this.TxtFDL1Address.TextChanged += new System.EventHandler(this.TxtFDL1Address_TextChanged);
            // 
            // TxtFDL1
            // 
            this.TxtFDL1.Location = new System.Drawing.Point(88, 19);
            this.TxtFDL1.Name = "TxtFDL1";
            this.TxtFDL1.Size = new System.Drawing.Size(422, 20);
            this.TxtFDL1.TabIndex = 11;
            // 
            // Logs
            // 
            this.Logs.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Logs.Location = new System.Drawing.Point(12, 98);
            this.Logs.Name = "Logs";
            this.Logs.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.Logs.Size = new System.Drawing.Size(545, 406);
            this.Logs.TabIndex = 0;
            this.Logs.Text = "";
            this.Logs.WordWrap = false;
            this.Logs.TextChanged += new System.EventHandler(this.Logs_TextChanged);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(12, 49);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(67, 13);
            this.Label7.TabIndex = 15;
            this.Label7.Text = "Connection";
            // 
            // ReceiverDataWorker
            // 
            this.ReceiverDataWorker.WorkerReportsProgress = true;
            this.ReceiverDataWorker.WorkerSupportsCancellation = true;
            // 
            // ProgresbarWorker
            // 
            this.ProgresbarWorker.WorkerReportsProgress = true;
            this.ProgresbarWorker.WorkerSupportsCancellation = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(334, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Timeout I/O";
            // 
            // comboDownloadTimeout
            // 
            this.comboDownloadTimeout.FormattingEnabled = true;
            this.comboDownloadTimeout.Items.AddRange(new object[] {
            "5000 ms",
            "10000 ms",
            "15000 ms",
            "20000 ms",
            "50000 ms",
            "100000 ms"});
            this.comboDownloadTimeout.Location = new System.Drawing.Point(443, 46);
            this.comboDownloadTimeout.Name = "comboDownloadTimeout";
            this.comboDownloadTimeout.Size = new System.Drawing.Size(110, 21);
            this.comboDownloadTimeout.TabIndex = 1;
            this.comboDownloadTimeout.Text = "5000 ms";
            this.comboDownloadTimeout.SelectedIndexChanged += new System.EventHandler(this.comboDownloadTimeout_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(760, 49);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "USB Port ";
            // 
            // panel_header
            // 
            this.panel_header.Controls.Add(this.label_title);
            this.panel_header.Controls.Add(this.pictureBoxIcon);
            this.panel_header.Controls.Add(this.iReverseButtonMinimize);
            this.panel_header.Controls.Add(this.iReverseToggleButton_SetTheme);
            this.panel_header.Controls.Add(this.iReverseButtonClose);
            this.panel_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_header.Location = new System.Drawing.Point(0, 0);
            this.panel_header.Name = "panel_header";
            this.panel_header.Size = new System.Drawing.Size(1125, 38);
            this.panel_header.TabIndex = 45;
            this.panel_header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            // 
            // label_title
            // 
            this.label_title.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title.Location = new System.Drawing.Point(39, 8);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(780, 21);
            this.label_title.TabIndex = 57;
            this.label_title.Text = "iReverse Unisoc Ultimate Download Tool - C# Version [11/11/2025] - Hadi Khoirudin" +
    ", S. Kom";
            this.label_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxIcon.Image = global::iReverse_Unisoc_Ultimate.Properties.Resources.logoireverse;
            this.pictureBoxIcon.Location = new System.Drawing.Point(-2, 0);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(36, 38);
            this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxIcon.TabIndex = 1;
            this.pictureBoxIcon.TabStop = false;
            // 
            // iReverseButtonMinimize
            // 
            this.iReverseButtonMinimize.BackColor = System.Drawing.Color.DarkOrange;
            this.iReverseButtonMinimize.BackgroundColor = System.Drawing.Color.DarkOrange;
            this.iReverseButtonMinimize.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.iReverseButtonMinimize.BorderRadius = 7;
            this.iReverseButtonMinimize.BorderSize = 0;
            this.iReverseButtonMinimize.FlatAppearance.BorderSize = 0;
            this.iReverseButtonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iReverseButtonMinimize.ForeColor = System.Drawing.Color.White;
            this.iReverseButtonMinimize.Location = new System.Drawing.Point(1080, 9);
            this.iReverseButtonMinimize.Name = "iReverseButtonMinimize";
            this.iReverseButtonMinimize.Size = new System.Drawing.Size(14, 14);
            this.iReverseButtonMinimize.TabIndex = 0;
            this.iReverseButtonMinimize.TextColor = System.Drawing.Color.White;
            this.iReverseButtonMinimize.UseVisualStyleBackColor = false;
            this.iReverseButtonMinimize.Click += new System.EventHandler(this.iReverseButtonMinimize_Click);
            // 
            // iReverseToggleButton_SetTheme
            // 
            this.iReverseToggleButton_SetTheme.Checked = true;
            this.iReverseToggleButton_SetTheme.CheckState = System.Windows.Forms.CheckState.Checked;
            this.iReverseToggleButton_SetTheme.Location = new System.Drawing.Point(841, 12);
            this.iReverseToggleButton_SetTheme.MinimumSize = new System.Drawing.Size(32, 16);
            this.iReverseToggleButton_SetTheme.Name = "iReverseToggleButton_SetTheme";
            this.iReverseToggleButton_SetTheme.OffBackColor = System.Drawing.Color.Gray;
            this.iReverseToggleButton_SetTheme.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.iReverseToggleButton_SetTheme.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.iReverseToggleButton_SetTheme.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.iReverseToggleButton_SetTheme.Size = new System.Drawing.Size(120, 17);
            this.iReverseToggleButton_SetTheme.TabIndex = 43;
            this.iReverseToggleButton_SetTheme.Text = "Set Dark Theme";
            this.iReverseToggleButton_SetTheme.UseVisualStyleBackColor = true;
            this.iReverseToggleButton_SetTheme.CheckedChanged += new System.EventHandler(this.iReverseToggleButton_SetTheme_CheckedChanged);
            // 
            // iReverseButtonClose
            // 
            this.iReverseButtonClose.BackColor = System.Drawing.Color.Crimson;
            this.iReverseButtonClose.BackgroundColor = System.Drawing.Color.Crimson;
            this.iReverseButtonClose.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.iReverseButtonClose.BorderRadius = 7;
            this.iReverseButtonClose.BorderSize = 0;
            this.iReverseButtonClose.FlatAppearance.BorderSize = 0;
            this.iReverseButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iReverseButtonClose.ForeColor = System.Drawing.Color.White;
            this.iReverseButtonClose.Location = new System.Drawing.Point(1101, 9);
            this.iReverseButtonClose.Name = "iReverseButtonClose";
            this.iReverseButtonClose.Size = new System.Drawing.Size(14, 14);
            this.iReverseButtonClose.TabIndex = 0;
            this.iReverseButtonClose.TextColor = System.Drawing.Color.White;
            this.iReverseButtonClose.UseVisualStyleBackColor = false;
            this.iReverseButtonClose.Click += new System.EventHandler(this.iReverseButtonClose_Click);
            // 
            // BtnStop
            // 
            this.BtnStop.BackColor = System.Drawing.Color.Crimson;
            this.BtnStop.BackgroundColor = System.Drawing.Color.Crimson;
            this.BtnStop.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnStop.BorderRadius = 5;
            this.BtnStop.BorderSize = 0;
            this.BtnStop.FlatAppearance.BorderSize = 0;
            this.BtnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnStop.ForeColor = System.Drawing.Color.White;
            this.BtnStop.Location = new System.Drawing.Point(1043, 516);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(68, 44);
            this.BtnStop.TabIndex = 44;
            this.BtnStop.Text = "STOP";
            this.BtnStop.TextColor = System.Drawing.Color.White;
            this.BtnStop.UseVisualStyleBackColor = false;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // CkRepartition
            // 
            this.CkRepartition.Location = new System.Drawing.Point(746, 74);
            this.CkRepartition.MinimumSize = new System.Drawing.Size(32, 16);
            this.CkRepartition.Name = "CkRepartition";
            this.CkRepartition.OffBackColor = System.Drawing.Color.Gray;
            this.CkRepartition.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.CkRepartition.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.CkRepartition.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.CkRepartition.Size = new System.Drawing.Size(102, 17);
            this.CkRepartition.TabIndex = 43;
            this.CkRepartition.Text = "Repartition";
            this.CkRepartition.UseVisualStyleBackColor = true;
            // 
            // CkAutoReboot
            // 
            this.CkAutoReboot.Checked = true;
            this.CkAutoReboot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CkAutoReboot.Location = new System.Drawing.Point(573, 74);
            this.CkAutoReboot.MinimumSize = new System.Drawing.Size(32, 16);
            this.CkAutoReboot.Name = "CkAutoReboot";
            this.CkAutoReboot.OffBackColor = System.Drawing.Color.Gray;
            this.CkAutoReboot.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.CkAutoReboot.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.CkAutoReboot.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.CkAutoReboot.Size = new System.Drawing.Size(144, 17);
            this.CkAutoReboot.TabIndex = 42;
            this.CkAutoReboot.Text = "Auto Reboot Device";
            this.CkAutoReboot.UseVisualStyleBackColor = true;
            // 
            // CkAutoRSAExploit
            // 
            this.CkAutoRSAExploit.Location = new System.Drawing.Point(573, 48);
            this.CkAutoRSAExploit.MinimumSize = new System.Drawing.Size(32, 16);
            this.CkAutoRSAExploit.Name = "CkAutoRSAExploit";
            this.CkAutoRSAExploit.OffBackColor = System.Drawing.Color.Gray;
            this.CkAutoRSAExploit.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.CkAutoRSAExploit.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.CkAutoRSAExploit.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.CkAutoRSAExploit.Size = new System.Drawing.Size(180, 17);
            this.CkAutoRSAExploit.TabIndex = 41;
            this.CkAutoRSAExploit.Text = "Using RSA Exploit Bypass";
            this.CkAutoRSAExploit.UseVisualStyleBackColor = true;
            this.CkAutoRSAExploit.CheckedChanged += new System.EventHandler(this.CkAutoRSAExploit_CheckedChanged);
            // 
            // BtnDeviceManager
            // 
            this.BtnDeviceManager.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnDeviceManager.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnDeviceManager.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnDeviceManager.BorderRadius = 0;
            this.BtnDeviceManager.BorderSize = 0;
            this.BtnDeviceManager.FlatAppearance.BorderSize = 0;
            this.BtnDeviceManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDeviceManager.ForeColor = System.Drawing.Color.White;
            this.BtnDeviceManager.Location = new System.Drawing.Point(443, 74);
            this.BtnDeviceManager.Name = "BtnDeviceManager";
            this.BtnDeviceManager.Size = new System.Drawing.Size(110, 23);
            this.BtnDeviceManager.TabIndex = 40;
            this.BtnDeviceManager.Text = "Device Manager";
            this.BtnDeviceManager.TextColor = System.Drawing.Color.White;
            this.BtnDeviceManager.UseVisualStyleBackColor = false;
            this.BtnDeviceManager.Click += new System.EventHandler(this.BtnDeviceManager_Click);
            // 
            // BtnInstallDriver
            // 
            this.BtnInstallDriver.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnInstallDriver.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnInstallDriver.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnInstallDriver.BorderRadius = 0;
            this.BtnInstallDriver.BorderSize = 0;
            this.BtnInstallDriver.FlatAppearance.BorderSize = 0;
            this.BtnInstallDriver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnInstallDriver.ForeColor = System.Drawing.Color.White;
            this.BtnInstallDriver.Location = new System.Drawing.Point(336, 74);
            this.BtnInstallDriver.Name = "BtnInstallDriver";
            this.BtnInstallDriver.Size = new System.Drawing.Size(101, 23);
            this.BtnInstallDriver.TabIndex = 39;
            this.BtnInstallDriver.Text = "Install Driver";
            this.BtnInstallDriver.TextColor = System.Drawing.Color.White;
            this.BtnInstallDriver.UseVisualStyleBackColor = false;
            this.BtnInstallDriver.Click += new System.EventHandler(this.BtnInstallDriver_Click);
            // 
            // CkDiagConnected
            // 
            this.CkDiagConnected.Location = new System.Drawing.Point(204, 74);
            this.CkDiagConnected.MinimumSize = new System.Drawing.Size(32, 16);
            this.CkDiagConnected.Name = "CkDiagConnected";
            this.CkDiagConnected.OffBackColor = System.Drawing.Color.Gray;
            this.CkDiagConnected.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.CkDiagConnected.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.CkDiagConnected.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.CkDiagConnected.Size = new System.Drawing.Size(120, 17);
            this.CkDiagConnected.TabIndex = 38;
            this.CkDiagConnected.Text = "Diag Connected";
            this.CkDiagConnected.UseVisualStyleBackColor = true;
            this.CkDiagConnected.CheckedChanged += new System.EventHandler(this.CkDiagConnected_CheckedChanged);
            // 
            // CkFDLLoaded
            // 
            this.CkFDLLoaded.Location = new System.Drawing.Point(203, 48);
            this.CkFDLLoaded.MinimumSize = new System.Drawing.Size(32, 16);
            this.CkFDLLoaded.Name = "CkFDLLoaded";
            this.CkFDLLoaded.OffBackColor = System.Drawing.Color.Gray;
            this.CkFDLLoaded.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.CkFDLLoaded.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.CkFDLLoaded.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.CkFDLLoaded.Size = new System.Drawing.Size(96, 17);
            this.CkFDLLoaded.TabIndex = 36;
            this.CkFDLLoaded.Text = "FDL Loaded";
            this.CkFDLLoaded.UseVisualStyleBackColor = true;
            this.CkFDLLoaded.CheckedChanged += new System.EventHandler(this.CkFDLLoaded_CheckedChanged);
            // 
            // RdDiagChannel
            // 
            this.RdDiagChannel.AutoSize = true;
            this.RdDiagChannel.BackColor = System.Drawing.Color.White;
            this.RdDiagChannel.CheckedColor = System.Drawing.Color.MediumSlateBlue;
            this.RdDiagChannel.Location = new System.Drawing.Point(91, 73);
            this.RdDiagChannel.MinimumSize = new System.Drawing.Size(0, 13);
            this.RdDiagChannel.Name = "RdDiagChannel";
            this.RdDiagChannel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.RdDiagChannel.Size = new System.Drawing.Size(107, 17);
            this.RdDiagChannel.TabIndex = 35;
            this.RdDiagChannel.TabStop = true;
            this.RdDiagChannel.Text = "Diag Channel";
            this.RdDiagChannel.UnCheckedColor = System.Drawing.Color.Gray;
            this.RdDiagChannel.UseVisualStyleBackColor = false;
            this.RdDiagChannel.CheckedChanged += new System.EventHandler(this.RdDiagChannel_CheckedChanged);
            // 
            // RdDownload
            // 
            this.RdDownload.AutoSize = true;
            this.RdDownload.BackColor = System.Drawing.Color.White;
            this.RdDownload.Checked = true;
            this.RdDownload.CheckedColor = System.Drawing.Color.MediumSlateBlue;
            this.RdDownload.Location = new System.Drawing.Point(91, 47);
            this.RdDownload.MinimumSize = new System.Drawing.Size(0, 13);
            this.RdDownload.Name = "RdDownload";
            this.RdDownload.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.RdDownload.Size = new System.Drawing.Size(83, 17);
            this.RdDownload.TabIndex = 34;
            this.RdDownload.TabStop = true;
            this.RdDownload.Text = "Download";
            this.RdDownload.UnCheckedColor = System.Drawing.Color.Gray;
            this.RdDownload.UseVisualStyleBackColor = false;
            this.RdDownload.CheckedChanged += new System.EventHandler(this.RdDownload_CheckedChanged);
            // 
            // IReverseProgressBar2
            // 
            this.IReverseProgressBar2.ChannelColor = System.Drawing.Color.Gray;
            this.IReverseProgressBar2.ChannelHeight = 6;
            this.IReverseProgressBar2.ForeBackColor = System.Drawing.Color.MediumSlateBlue;
            this.IReverseProgressBar2.ForeColor = System.Drawing.Color.White;
            this.IReverseProgressBar2.Location = new System.Drawing.Point(21, 531);
            this.IReverseProgressBar2.Name = "IReverseProgressBar2";
            this.IReverseProgressBar2.ShowMaximun = false;
            this.IReverseProgressBar2.ShowValue = iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.TextPosition.Sliding;
            this.IReverseProgressBar2.Size = new System.Drawing.Size(1012, 23);
            this.IReverseProgressBar2.SliderColor = System.Drawing.Color.MediumSlateBlue;
            this.IReverseProgressBar2.SliderHeight = 10;
            this.IReverseProgressBar2.SymbolAfter = "";
            this.IReverseProgressBar2.SymbolBefore = "";
            this.IReverseProgressBar2.TabIndex = 33;
            // 
            // IReverseProgressBar1
            // 
            this.IReverseProgressBar1.ChannelColor = System.Drawing.Color.Gray;
            this.IReverseProgressBar1.ChannelHeight = 6;
            this.IReverseProgressBar1.ForeBackColor = System.Drawing.Color.MediumSlateBlue;
            this.IReverseProgressBar1.ForeColor = System.Drawing.Color.White;
            this.IReverseProgressBar1.Location = new System.Drawing.Point(21, 509);
            this.IReverseProgressBar1.Name = "IReverseProgressBar1";
            this.IReverseProgressBar1.ShowMaximun = false;
            this.IReverseProgressBar1.ShowValue = iReverse_Unisoc_Ultimate.CustomControls.iReverseControls.TextPosition.Sliding;
            this.IReverseProgressBar1.Size = new System.Drawing.Size(1012, 23);
            this.IReverseProgressBar1.SliderColor = System.Drawing.Color.MediumSlateBlue;
            this.IReverseProgressBar1.SliderHeight = 10;
            this.IReverseProgressBar1.SymbolAfter = "";
            this.IReverseProgressBar1.SymbolBefore = "";
            this.IReverseProgressBar1.TabIndex = 33;
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.TabPage3);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.DisplayStyle = System.Windows.Forms.TabStyle.Dark;
            this.TabControl1.DisplayStyleProvider.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.TabControl1.DisplayStyleProvider.BorderColorHot = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.TabControl1.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.TabControl1.DisplayStyleProvider.CloserColor = System.Drawing.Color.White;
            this.TabControl1.DisplayStyleProvider.CloserColorActive = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(196)))), ((int)(((byte)(232)))));
            this.TabControl1.DisplayStyleProvider.FocusColor = System.Drawing.Color.DarkRed;
            this.TabControl1.DisplayStyleProvider.FocusTrack = false;
            this.TabControl1.DisplayStyleProvider.HotTrack = false;
            this.TabControl1.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TabControl1.DisplayStyleProvider.Opacity = 1F;
            this.TabControl1.DisplayStyleProvider.Overlap = 0;
            this.TabControl1.DisplayStyleProvider.Padding = new System.Drawing.Point(6, 3);
            this.TabControl1.DisplayStyleProvider.Radius = 10;
            this.TabControl1.DisplayStyleProvider.ShowTabCloser = false;
            this.TabControl1.DisplayStyleProvider.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.TabControl1.DisplayStyleProvider.TextColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.TabControl1.DisplayStyleProvider.TextColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(196)))), ((int)(((byte)(232)))));
            this.TabControl1.Location = new System.Drawing.Point(559, 73);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TabControl1.RightToLeftLayout = true;
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(554, 431);
            this.TabControl1.TabIndex = 20;
            this.TabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.TabControl1_Selecting);
            // 
            // TabPage3
            // 
            this.TabPage3.Controls.Add(this.labelTotal);
            this.TabPage3.Controls.Add(this.GroupBox4);
            this.TabPage3.Controls.Add(this.txtSearchListBox);
            this.TabPage3.Controls.Add(this.GroupBox3);
            this.TabPage3.Location = new System.Drawing.Point(4, 23);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TabPage3.Size = new System.Drawing.Size(546, 404);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Unlock Tool";
            this.TabPage3.UseVisualStyleBackColor = true;
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Location = new System.Drawing.Point(342, 6);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(13, 13);
            this.labelTotal.TabIndex = 37;
            this.labelTotal.Text = "[]";
            // 
            // GroupBox4
            // 
            this.GroupBox4.Controls.Add(this.PanelSPDOneClick);
            this.GroupBox4.Location = new System.Drawing.Point(336, 32);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(204, 373);
            this.GroupBox4.TabIndex = 6;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "Operation";
            // 
            // PanelSPDOneClick
            // 
            this.PanelSPDOneClick.Location = new System.Drawing.Point(3, 13);
            this.PanelSPDOneClick.Name = "PanelSPDOneClick";
            this.PanelSPDOneClick.Size = new System.Drawing.Size(195, 351);
            this.PanelSPDOneClick.TabIndex = 0;
            // 
            // txtSearchListBox
            // 
            this.txtSearchListBox.Location = new System.Drawing.Point(10, 6);
            this.txtSearchListBox.Name = "txtSearchListBox";
            this.txtSearchListBox.Size = new System.Drawing.Size(326, 20);
            this.txtSearchListBox.TabIndex = 6;
            this.txtSearchListBox.Text = "Type For Search ...";
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.listBoxOneClick);
            this.GroupBox3.Location = new System.Drawing.Point(6, 32);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(330, 370);
            this.GroupBox3.TabIndex = 5;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Devices";
            // 
            // listBoxOneClick
            // 
            this.listBoxOneClick.AuthColor = System.Drawing.Color.Crimson;
            this.listBoxOneClick.BorderListColor = System.Drawing.Color.Transparent;
            this.listBoxOneClick.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxOneClick.ConnectionColor = System.Drawing.Color.MediumSlateBlue;
            this.listBoxOneClick.DescriptionFont = new System.Drawing.Font("Tahoma", 6.8F);
            this.listBoxOneClick.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxOneClick.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold);
            this.listBoxOneClick.FormattingEnabled = true;
            this.listBoxOneClick.ItemFont = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.listBoxOneClick.ItemHeight = 35;
            this.listBoxOneClick.Items.AddRange(new object[] {
            "(Devices, Model, Chipsets, Connection, Auth, New)"});
            this.listBoxOneClick.ItemTextColor = System.Drawing.Color.White;
            this.listBoxOneClick.ListTextColor = System.Drawing.SystemColors.WindowText;
            this.listBoxOneClick.Location = new System.Drawing.Point(3, 13);
            this.listBoxOneClick.Name = "listBoxOneClick";
            this.listBoxOneClick.NewColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.listBoxOneClick.OnItemSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.listBoxOneClick.OnTextSelectedColor = System.Drawing.Color.White;
            this.listBoxOneClick.Size = new System.Drawing.Size(321, 350);
            this.listBoxOneClick.TabIndex = 7;
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.BtnPowerOff);
            this.TabPage2.Controls.Add(this.BtnSendATCommand);
            this.TabPage2.Controls.Add(this.BtnFactoryReset);
            this.TabPage2.Controls.Add(this.GroupBox2);
            this.TabPage2.Controls.Add(this.GroupBox1);
            this.TabPage2.Controls.Add(this.TxtATCommand);
            this.TabPage2.Controls.Add(this.Label15);
            this.TabPage2.Location = new System.Drawing.Point(4, 23);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TabPage2.Size = new System.Drawing.Size(546, 404);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Diag Tool";
            this.TabPage2.UseVisualStyleBackColor = true;
            // 
            // BtnPowerOff
            // 
            this.BtnPowerOff.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnPowerOff.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnPowerOff.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnPowerOff.BorderRadius = 0;
            this.BtnPowerOff.BorderSize = 0;
            this.BtnPowerOff.FlatAppearance.BorderSize = 0;
            this.BtnPowerOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPowerOff.ForeColor = System.Drawing.Color.White;
            this.BtnPowerOff.Location = new System.Drawing.Point(290, 167);
            this.BtnPowerOff.Name = "BtnPowerOff";
            this.BtnPowerOff.Size = new System.Drawing.Size(186, 23);
            this.BtnPowerOff.TabIndex = 36;
            this.BtnPowerOff.Text = "Power Off";
            this.BtnPowerOff.TextColor = System.Drawing.Color.White;
            this.BtnPowerOff.UseVisualStyleBackColor = false;
            this.BtnPowerOff.Click += new System.EventHandler(this.BtnBtnPowerOff_Click);
            // 
            // BtnSendATCommand
            // 
            this.BtnSendATCommand.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnSendATCommand.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnSendATCommand.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnSendATCommand.BorderRadius = 0;
            this.BtnSendATCommand.BorderSize = 0;
            this.BtnSendATCommand.FlatAppearance.BorderSize = 0;
            this.BtnSendATCommand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSendATCommand.ForeColor = System.Drawing.Color.White;
            this.BtnSendATCommand.Location = new System.Drawing.Point(402, 228);
            this.BtnSendATCommand.Name = "BtnSendATCommand";
            this.BtnSendATCommand.Size = new System.Drawing.Size(80, 23);
            this.BtnSendATCommand.TabIndex = 36;
            this.BtnSendATCommand.Text = "Send";
            this.BtnSendATCommand.TextColor = System.Drawing.Color.White;
            this.BtnSendATCommand.UseVisualStyleBackColor = false;
            this.BtnSendATCommand.Click += new System.EventHandler(this.BtnSendATCommand_Click);
            // 
            // BtnFactoryReset
            // 
            this.BtnFactoryReset.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnFactoryReset.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnFactoryReset.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnFactoryReset.BorderRadius = 0;
            this.BtnFactoryReset.BorderSize = 0;
            this.BtnFactoryReset.FlatAppearance.BorderSize = 0;
            this.BtnFactoryReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFactoryReset.ForeColor = System.Drawing.Color.White;
            this.BtnFactoryReset.Location = new System.Drawing.Point(62, 167);
            this.BtnFactoryReset.Name = "BtnFactoryReset";
            this.BtnFactoryReset.Size = new System.Drawing.Size(186, 23);
            this.BtnFactoryReset.TabIndex = 36;
            this.BtnFactoryReset.Text = "Factory Reset";
            this.BtnFactoryReset.TextColor = System.Drawing.Color.White;
            this.BtnFactoryReset.UseVisualStyleBackColor = false;
            this.BtnFactoryReset.Click += new System.EventHandler(this.BtnFactoryReset_Click);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.BtnEnterDiagMode);
            this.GroupBox2.Controls.Add(this.RdFactoryTestMode);
            this.GroupBox2.Controls.Add(this.RdCalibrationMode);
            this.GroupBox2.Location = new System.Drawing.Point(7, 6);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(533, 128);
            this.GroupBox2.TabIndex = 34;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Connection Mode";
            // 
            // BtnEnterDiagMode
            // 
            this.BtnEnterDiagMode.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnEnterDiagMode.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnEnterDiagMode.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnEnterDiagMode.BorderRadius = 0;
            this.BtnEnterDiagMode.BorderSize = 0;
            this.BtnEnterDiagMode.FlatAppearance.BorderSize = 0;
            this.BtnEnterDiagMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEnterDiagMode.ForeColor = System.Drawing.Color.White;
            this.BtnEnterDiagMode.Location = new System.Drawing.Point(164, 66);
            this.BtnEnterDiagMode.Name = "BtnEnterDiagMode";
            this.BtnEnterDiagMode.Size = new System.Drawing.Size(186, 23);
            this.BtnEnterDiagMode.TabIndex = 39;
            this.BtnEnterDiagMode.Text = "Enter Diag Mode";
            this.BtnEnterDiagMode.TextColor = System.Drawing.Color.White;
            this.BtnEnterDiagMode.UseVisualStyleBackColor = false;
            this.BtnEnterDiagMode.Click += new System.EventHandler(this.BtnEnterDiagMode_Click);
            // 
            // RdFactoryTestMode
            // 
            this.RdFactoryTestMode.AutoSize = true;
            this.RdFactoryTestMode.BackColor = System.Drawing.Color.White;
            this.RdFactoryTestMode.CheckedColor = System.Drawing.Color.MediumSlateBlue;
            this.RdFactoryTestMode.Location = new System.Drawing.Point(390, 23);
            this.RdFactoryTestMode.MinimumSize = new System.Drawing.Size(0, 13);
            this.RdFactoryTestMode.Name = "RdFactoryTestMode";
            this.RdFactoryTestMode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.RdFactoryTestMode.Size = new System.Drawing.Size(124, 17);
            this.RdFactoryTestMode.TabIndex = 38;
            this.RdFactoryTestMode.Text = "Factory Test Mode";
            this.RdFactoryTestMode.UnCheckedColor = System.Drawing.Color.Gray;
            this.RdFactoryTestMode.UseVisualStyleBackColor = false;
            this.RdFactoryTestMode.CheckedChanged += new System.EventHandler(this.RdFactoryTestMode_CheckedChanged);
            // 
            // RdCalibrationMode
            // 
            this.RdCalibrationMode.AutoSize = true;
            this.RdCalibrationMode.BackColor = System.Drawing.Color.White;
            this.RdCalibrationMode.Checked = true;
            this.RdCalibrationMode.CheckedColor = System.Drawing.Color.MediumSlateBlue;
            this.RdCalibrationMode.Location = new System.Drawing.Point(14, 23);
            this.RdCalibrationMode.MinimumSize = new System.Drawing.Size(0, 13);
            this.RdCalibrationMode.Name = "RdCalibrationMode";
            this.RdCalibrationMode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.RdCalibrationMode.Size = new System.Drawing.Size(114, 17);
            this.RdCalibrationMode.TabIndex = 37;
            this.RdCalibrationMode.TabStop = true;
            this.RdCalibrationMode.Text = "Calibration Mode";
            this.RdCalibrationMode.UnCheckedColor = System.Drawing.Color.Gray;
            this.RdCalibrationMode.UseVisualStyleBackColor = false;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.BtnWriteIMEI2);
            this.GroupBox1.Controls.Add(this.BtnReadIMEI2);
            this.GroupBox1.Controls.Add(this.BtnWriteIMEI1);
            this.GroupBox1.Controls.Add(this.BtnReadIMEI1);
            this.GroupBox1.Controls.Add(this.Label10);
            this.GroupBox1.Controls.Add(this.Label9);
            this.GroupBox1.Controls.Add(this.TxtIMEI2);
            this.GroupBox1.Controls.Add(this.TxtIMEI1);
            this.GroupBox1.Location = new System.Drawing.Point(6, 307);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(534, 100);
            this.GroupBox1.TabIndex = 33;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Read / Write";
            // 
            // BtnWriteIMEI2
            // 
            this.BtnWriteIMEI2.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnWriteIMEI2.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnWriteIMEI2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnWriteIMEI2.BorderRadius = 0;
            this.BtnWriteIMEI2.BorderSize = 0;
            this.BtnWriteIMEI2.FlatAppearance.BorderSize = 0;
            this.BtnWriteIMEI2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnWriteIMEI2.ForeColor = System.Drawing.Color.White;
            this.BtnWriteIMEI2.Location = new System.Drawing.Point(443, 51);
            this.BtnWriteIMEI2.Name = "BtnWriteIMEI2";
            this.BtnWriteIMEI2.Size = new System.Drawing.Size(80, 23);
            this.BtnWriteIMEI2.TabIndex = 37;
            this.BtnWriteIMEI2.Text = "Write";
            this.BtnWriteIMEI2.TextColor = System.Drawing.Color.White;
            this.BtnWriteIMEI2.UseVisualStyleBackColor = false;
            this.BtnWriteIMEI2.Click += new System.EventHandler(this.BtnWriteIMEI2_Click);
            // 
            // BtnReadIMEI2
            // 
            this.BtnReadIMEI2.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnReadIMEI2.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnReadIMEI2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnReadIMEI2.BorderRadius = 0;
            this.BtnReadIMEI2.BorderSize = 0;
            this.BtnReadIMEI2.FlatAppearance.BorderSize = 0;
            this.BtnReadIMEI2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnReadIMEI2.ForeColor = System.Drawing.Color.White;
            this.BtnReadIMEI2.Location = new System.Drawing.Point(357, 51);
            this.BtnReadIMEI2.Name = "BtnReadIMEI2";
            this.BtnReadIMEI2.Size = new System.Drawing.Size(80, 23);
            this.BtnReadIMEI2.TabIndex = 37;
            this.BtnReadIMEI2.Text = "Read";
            this.BtnReadIMEI2.TextColor = System.Drawing.Color.White;
            this.BtnReadIMEI2.UseVisualStyleBackColor = false;
            this.BtnReadIMEI2.Click += new System.EventHandler(this.BtnReadIMEI_Click);
            // 
            // BtnWriteIMEI1
            // 
            this.BtnWriteIMEI1.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnWriteIMEI1.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnWriteIMEI1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnWriteIMEI1.BorderRadius = 0;
            this.BtnWriteIMEI1.BorderSize = 0;
            this.BtnWriteIMEI1.FlatAppearance.BorderSize = 0;
            this.BtnWriteIMEI1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnWriteIMEI1.ForeColor = System.Drawing.Color.White;
            this.BtnWriteIMEI1.Location = new System.Drawing.Point(443, 25);
            this.BtnWriteIMEI1.Name = "BtnWriteIMEI1";
            this.BtnWriteIMEI1.Size = new System.Drawing.Size(80, 23);
            this.BtnWriteIMEI1.TabIndex = 37;
            this.BtnWriteIMEI1.Text = "Write";
            this.BtnWriteIMEI1.TextColor = System.Drawing.Color.White;
            this.BtnWriteIMEI1.UseVisualStyleBackColor = false;
            this.BtnWriteIMEI1.Click += new System.EventHandler(this.BtnWriteIMEI1_Click);
            // 
            // BtnReadIMEI1
            // 
            this.BtnReadIMEI1.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnReadIMEI1.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnReadIMEI1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnReadIMEI1.BorderRadius = 0;
            this.BtnReadIMEI1.BorderSize = 0;
            this.BtnReadIMEI1.FlatAppearance.BorderSize = 0;
            this.BtnReadIMEI1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnReadIMEI1.ForeColor = System.Drawing.Color.White;
            this.BtnReadIMEI1.Location = new System.Drawing.Point(357, 25);
            this.BtnReadIMEI1.Name = "BtnReadIMEI1";
            this.BtnReadIMEI1.Size = new System.Drawing.Size(80, 23);
            this.BtnReadIMEI1.TabIndex = 37;
            this.BtnReadIMEI1.Text = "Read";
            this.BtnReadIMEI1.TextColor = System.Drawing.Color.White;
            this.BtnReadIMEI1.UseVisualStyleBackColor = false;
            this.BtnReadIMEI1.Click += new System.EventHandler(this.BtnReadIMEI_Click);
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(12, 56);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(38, 13);
            this.Label10.TabIndex = 29;
            this.Label10.Text = "IMEI 2";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(12, 30);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(38, 13);
            this.Label9.TabIndex = 30;
            this.Label9.Text = "IMEI 1";
            // 
            // TxtIMEI2
            // 
            this.TxtIMEI2.Location = new System.Drawing.Point(92, 53);
            this.TxtIMEI2.Name = "TxtIMEI2";
            this.TxtIMEI2.Size = new System.Drawing.Size(259, 20);
            this.TxtIMEI2.TabIndex = 27;
            // 
            // TxtIMEI1
            // 
            this.TxtIMEI1.Location = new System.Drawing.Point(92, 27);
            this.TxtIMEI1.Name = "TxtIMEI1";
            this.TxtIMEI1.Size = new System.Drawing.Size(259, 20);
            this.TxtIMEI1.TabIndex = 28;
            // 
            // TxtATCommand
            // 
            this.TxtATCommand.Location = new System.Drawing.Point(137, 230);
            this.TxtATCommand.Name = "TxtATCommand";
            this.TxtATCommand.Size = new System.Drawing.Size(259, 20);
            this.TxtATCommand.TabIndex = 28;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(57, 233);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(71, 13);
            this.Label15.TabIndex = 30;
            this.Label15.Text = "AT Command";
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.CkPartition);
            this.TabPage1.Controls.Add(this.DataView);
            this.TabPage1.Location = new System.Drawing.Point(4, 23);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TabPage1.Size = new System.Drawing.Size(546, 404);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Download Tool";
            this.TabPage1.UseVisualStyleBackColor = true;
            // 
            // CkPartition
            // 
            this.CkPartition.AutoSize = true;
            this.CkPartition.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.CkPartition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CkPartition.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.CkPartition.Location = new System.Drawing.Point(6, 5);
            this.CkPartition.Name = "CkPartition";
            this.CkPartition.Size = new System.Drawing.Size(12, 11);
            this.CkPartition.TabIndex = 21;
            this.CkPartition.UseVisualStyleBackColor = false;
            this.CkPartition.CheckedChanged += new System.EventHandler(this.CkPartition_CheckedChanged);
            // 
            // DataView
            // 
            this.DataView.AllowUserToAddRows = false;
            this.DataView.AllowUserToDeleteRows = false;
            this.DataView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DataView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DataView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MediumSlateBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SlateBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ck,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.MediumSlateBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataView.DefaultCellStyle = dataGridViewCellStyle4;
            this.DataView.EnableHeadersVisualStyles = false;
            this.DataView.Location = new System.Drawing.Point(3, 3);
            this.DataView.Name = "DataView";
            this.DataView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.MediumSlateBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.DataView.RowHeadersVisible = false;
            this.DataView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataView.Size = new System.Drawing.Size(540, 396);
            this.DataView.TabIndex = 20;
            this.DataView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataView_CellDoubleClick);
            // 
            // Ck
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.MediumSlateBlue;
            dataGridViewCellStyle2.NullValue = false;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.MediumSlateBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.Ck.DefaultCellStyle = dataGridViewCellStyle2;
            this.Ck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Ck.HeaderText = "";
            this.Ck.Name = "Ck";
            this.Ck.Width = 20;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "File IDs";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Partitions";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Sectors";
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Lengths";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 70;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "File Sizes";
            this.Column5.Name = "Column5";
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 80;
            // 
            // Column6
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column6.HeaderText = "Locations";
            this.Column6.Name = "Column6";
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 105;
            // 
            // BtnFlashPartition
            // 
            this.BtnFlashPartition.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnFlashPartition.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnFlashPartition.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnFlashPartition.BorderRadius = 0;
            this.BtnFlashPartition.BorderSize = 0;
            this.BtnFlashPartition.FlatAppearance.BorderSize = 0;
            this.BtnFlashPartition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFlashPartition.ForeColor = System.Drawing.Color.White;
            this.BtnFlashPartition.Location = new System.Drawing.Point(1020, 69);
            this.BtnFlashPartition.Name = "BtnFlashPartition";
            this.BtnFlashPartition.Size = new System.Drawing.Size(71, 23);
            this.BtnFlashPartition.TabIndex = 44;
            this.BtnFlashPartition.Text = "Flash";
            this.BtnFlashPartition.TextColor = System.Drawing.Color.White;
            this.BtnFlashPartition.UseVisualStyleBackColor = false;
            this.BtnFlashPartition.Click += new System.EventHandler(this.BtnFlashPartition_Click);
            // 
            // BtnPACFirmware
            // 
            this.BtnPACFirmware.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnPACFirmware.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnPACFirmware.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnPACFirmware.BorderRadius = 0;
            this.BtnPACFirmware.BorderSize = 0;
            this.BtnPACFirmware.FlatAppearance.BorderSize = 0;
            this.BtnPACFirmware.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPACFirmware.ForeColor = System.Drawing.Color.White;
            this.BtnPACFirmware.Location = new System.Drawing.Point(516, 69);
            this.BtnPACFirmware.Name = "BtnPACFirmware";
            this.BtnPACFirmware.Size = new System.Drawing.Size(31, 23);
            this.BtnPACFirmware.TabIndex = 44;
            this.BtnPACFirmware.Text = "+";
            this.BtnPACFirmware.TextColor = System.Drawing.Color.White;
            this.BtnPACFirmware.UseVisualStyleBackColor = false;
            this.BtnPACFirmware.Click += new System.EventHandler(this.BtnPACFirmware_Click);
            // 
            // BtnIdentify
            // 
            this.BtnIdentify.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnIdentify.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnIdentify.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnIdentify.BorderRadius = 0;
            this.BtnIdentify.BorderSize = 0;
            this.BtnIdentify.FlatAppearance.BorderSize = 0;
            this.BtnIdentify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnIdentify.ForeColor = System.Drawing.Color.White;
            this.BtnIdentify.Location = new System.Drawing.Point(940, 69);
            this.BtnIdentify.Name = "BtnIdentify";
            this.BtnIdentify.Size = new System.Drawing.Size(74, 23);
            this.BtnIdentify.TabIndex = 44;
            this.BtnIdentify.Text = "Identify";
            this.BtnIdentify.TextColor = System.Drawing.Color.White;
            this.BtnIdentify.UseVisualStyleBackColor = false;
            this.BtnIdentify.Click += new System.EventHandler(this.BtnIdentify_Click);
            // 
            // CkKeepNV
            // 
            this.CkKeepNV.AutoSize = true;
            this.CkKeepNV.Checked = true;
            this.CkKeepNV.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CkKeepNV.Location = new System.Drawing.Point(1065, 19);
            this.CkKeepNV.MinimumSize = new System.Drawing.Size(32, 16);
            this.CkKeepNV.Name = "CkKeepNV";
            this.CkKeepNV.OffBackColor = System.Drawing.Color.Gray;
            this.CkKeepNV.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.CkKeepNV.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.CkKeepNV.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.CkKeepNV.Size = new System.Drawing.Size(32, 16);
            this.CkKeepNV.TabIndex = 39;
            this.CkKeepNV.UseVisualStyleBackColor = true;
            // 
            // BtnReadPartition
            // 
            this.BtnReadPartition.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnReadPartition.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnReadPartition.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnReadPartition.BorderRadius = 0;
            this.BtnReadPartition.BorderSize = 0;
            this.BtnReadPartition.FlatAppearance.BorderSize = 0;
            this.BtnReadPartition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnReadPartition.ForeColor = System.Drawing.Color.White;
            this.BtnReadPartition.Location = new System.Drawing.Point(811, 69);
            this.BtnReadPartition.Name = "BtnReadPartition";
            this.BtnReadPartition.Size = new System.Drawing.Size(123, 23);
            this.BtnReadPartition.TabIndex = 44;
            this.BtnReadPartition.Text = "Read Partition";
            this.BtnReadPartition.TextColor = System.Drawing.Color.White;
            this.BtnReadPartition.UseVisualStyleBackColor = false;
            this.BtnReadPartition.Click += new System.EventHandler(this.BtnReadPartition_Click);
            // 
            // BtnFDL2
            // 
            this.BtnFDL2.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnFDL2.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnFDL2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnFDL2.BorderRadius = 0;
            this.BtnFDL2.BorderSize = 0;
            this.BtnFDL2.FlatAppearance.BorderSize = 0;
            this.BtnFDL2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFDL2.ForeColor = System.Drawing.Color.White;
            this.BtnFDL2.Location = new System.Drawing.Point(516, 43);
            this.BtnFDL2.Name = "BtnFDL2";
            this.BtnFDL2.Size = new System.Drawing.Size(31, 23);
            this.BtnFDL2.TabIndex = 44;
            this.BtnFDL2.Text = "+";
            this.BtnFDL2.TextColor = System.Drawing.Color.White;
            this.BtnFDL2.UseVisualStyleBackColor = false;
            this.BtnFDL2.Click += new System.EventHandler(this.BtnFDL2_Click);
            // 
            // BtnErase
            // 
            this.BtnErase.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnErase.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnErase.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnErase.BorderRadius = 0;
            this.BtnErase.BorderSize = 0;
            this.BtnErase.FlatAppearance.BorderSize = 0;
            this.BtnErase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnErase.ForeColor = System.Drawing.Color.White;
            this.BtnErase.Location = new System.Drawing.Point(682, 69);
            this.BtnErase.Name = "BtnErase";
            this.BtnErase.Size = new System.Drawing.Size(123, 23);
            this.BtnErase.TabIndex = 44;
            this.BtnErase.Text = "Erase Partition";
            this.BtnErase.TextColor = System.Drawing.Color.White;
            this.BtnErase.UseVisualStyleBackColor = false;
            this.BtnErase.Click += new System.EventHandler(this.BtnErase_Click);
            // 
            // BtnEraseFRPAccount
            // 
            this.BtnEraseFRPAccount.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnEraseFRPAccount.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnEraseFRPAccount.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnEraseFRPAccount.BorderRadius = 0;
            this.BtnEraseFRPAccount.BorderSize = 0;
            this.BtnEraseFRPAccount.FlatAppearance.BorderSize = 0;
            this.BtnEraseFRPAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEraseFRPAccount.ForeColor = System.Drawing.Color.White;
            this.BtnEraseFRPAccount.Location = new System.Drawing.Point(553, 69);
            this.BtnEraseFRPAccount.Name = "BtnEraseFRPAccount";
            this.BtnEraseFRPAccount.Size = new System.Drawing.Size(123, 23);
            this.BtnEraseFRPAccount.TabIndex = 44;
            this.BtnEraseFRPAccount.Text = "Erase FRP Account";
            this.BtnEraseFRPAccount.TextColor = System.Drawing.Color.White;
            this.BtnEraseFRPAccount.UseVisualStyleBackColor = false;
            this.BtnEraseFRPAccount.Click += new System.EventHandler(this.BtnEraseFRPAccount_Click);
            // 
            // BtnFDL1
            // 
            this.BtnFDL1.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnFDL1.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnFDL1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BtnFDL1.BorderRadius = 0;
            this.BtnFDL1.BorderSize = 0;
            this.BtnFDL1.FlatAppearance.BorderSize = 0;
            this.BtnFDL1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFDL1.ForeColor = System.Drawing.Color.White;
            this.BtnFDL1.Location = new System.Drawing.Point(516, 17);
            this.BtnFDL1.Name = "BtnFDL1";
            this.BtnFDL1.Size = new System.Drawing.Size(31, 23);
            this.BtnFDL1.TabIndex = 44;
            this.BtnFDL1.Text = "+";
            this.BtnFDL1.TextColor = System.Drawing.Color.White;
            this.BtnFDL1.UseVisualStyleBackColor = false;
            this.BtnFDL1.Click += new System.EventHandler(this.BtnFDL1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1125, 673);
            this.Controls.Add(this.panel_header);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.Logs);
            this.Controls.Add(this.CkRepartition);
            this.Controls.Add(this.CkAutoReboot);
            this.Controls.Add(this.CkAutoRSAExploit);
            this.Controls.Add(this.BtnDeviceManager);
            this.Controls.Add(this.BtnInstallDriver);
            this.Controls.Add(this.CkDiagConnected);
            this.Controls.Add(this.CkFDLLoaded);
            this.Controls.Add(this.RdDiagChannel);
            this.Controls.Add(this.RdDownload);
            this.Controls.Add(this.IReverseProgressBar2);
            this.Controls.Add(this.IReverseProgressBar1);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.GroupBoxFlash);
            this.Controls.Add(this.LabelTimer);
            this.Controls.Add(this.comboDownloadTimeout);
            this.Controls.Add(this.ComboPort);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.Label7);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Opacity = 0.96D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "iReverse Unisoc Ultimate Download Tool [x86] - C# - Version [22/02/2024] - Hadi K" +
    "hoirudin, S. Kom - [HadiK-IT] - Final";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_Closing);
            this.GroupBoxFlash.ResumeLayout(false);
            this.GroupBoxFlash.PerformLayout();
            this.panel_header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.TabControl1.ResumeLayout(false);
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            this.GroupBox4.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		public ComboBox ComboPort;
		public System.ComponentModel.BackgroundWorker UnisocWorker;
		public Label LabelTimer;
		internal GroupBox GroupBoxFlash;
		internal Label Label4;
		internal Label Label2;
		internal Label Label3;
		internal Label Label5;
		internal Label Label1;
		public TextBox TxtFDL2Address;
		public TextBox TxtPacFirmware;
		public TextBox TxtFDL2;
		public TextBox TxtFDL1Address;
		public TextBox TxtFDL1;
		internal TabPage TabPage1;
		public CheckBox CkPartition;
		internal DataGridView DataView;
		public RichTextBox Logs;
		internal Label Label7;
		public System.ComponentModel.BackgroundWorker ReceiverDataWorker;
		public System.ComponentModel.BackgroundWorker ProgresbarWorker;
		internal TabPage TabPage2;
		internal GroupBox GroupBox2;
		internal GroupBox GroupBox1;
		internal Label Label10;
		internal Label Label9;
		public TextBox TxtIMEI2;
		public TextBox TxtIMEI1;
		public TextBox TxtATCommand;
		internal Label Label15;
		internal TabPage TabPage3;
		internal GroupBox GroupBox4;
		internal GroupBox GroupBox3;
		internal Panel PanelSPDOneClick;
		public iReverseListBox listBoxOneClick;
		internal Label label11;
		public ComboBox comboDownloadTimeout;
		internal Label label12;
		public CustomTabControl TabControl1;
		internal CustomControls.iReverseControls.iReverseProgressBar IReverseProgressBar1;
		internal CustomControls.iReverseControls.iReverseProgressBar IReverseProgressBar2;
		internal CustomControls.iReverseControls.iReverseRadioButton RdCalibrationMode;
		internal CustomControls.iReverseControls.iReverseRadioButton RdFactoryTestMode;
		internal CustomControls.iReverseControls.iReverseButton BtnEnterDiagMode;
		internal CustomControls.iReverseControls.iReverseButton BtnFactoryReset;
		internal CustomControls.iReverseControls.iReverseButton BtnPowerOff;
		internal CustomControls.iReverseControls.iReverseButton BtnSendATCommand;
		internal CustomControls.iReverseControls.iReverseButton BtnReadIMEI1;
		internal CustomControls.iReverseControls.iReverseButton BtnReadIMEI2;
		internal CustomControls.iReverseControls.iReverseButton BtnWriteIMEI2;
		internal CustomControls.iReverseControls.iReverseButton BtnWriteIMEI1;
		internal CustomControls.iReverseControls.iReverseRadioButton RdDownload;
		internal CustomControls.iReverseControls.iReverseRadioButton RdDiagChannel;
		internal CustomControls.iReverseControls.iReverseToggleButton CkFDLLoaded;
		internal CustomControls.iReverseControls.iReverseToggleButton CkDiagConnected;
		internal CustomControls.iReverseControls.iReverseButton BtnInstallDriver;
		internal CustomControls.iReverseControls.iReverseButton BtnDeviceManager;
		internal CustomControls.iReverseControls.iReverseToggleButton CkAutoRSAExploit;
		internal CustomControls.iReverseControls.iReverseToggleButton CkAutoReboot;
		internal CustomControls.iReverseControls.iReverseToggleButton CkRepartition;
		internal CustomControls.iReverseControls.iReverseToggleButton CkKeepNV;
		internal Label Label18;
		internal CustomControls.iReverseControls.iReverseButton BtnFDL1;
		internal CustomControls.iReverseControls.iReverseButton BtnFDL2;
		internal CustomControls.iReverseControls.iReverseButton BtnPACFirmware;
		internal CustomControls.iReverseControls.iReverseButton BtnEraseFRPAccount;
		internal CustomControls.iReverseControls.iReverseButton BtnErase;
		internal CustomControls.iReverseControls.iReverseButton BtnReadPartition;
		internal CustomControls.iReverseControls.iReverseButton BtnIdentify;
		internal CustomControls.iReverseControls.iReverseButton BtnFlashPartition;
		internal CustomControls.iReverseControls.iReverseButton BtnStop;
		internal Panel panel_header;
		internal PictureBox pictureBoxIcon;
		internal CustomControls.iReverseControls.iReverseButton iReverseButtonMinimize;
		internal CustomControls.iReverseControls.iReverseButton iReverseButtonClose;
		private Label label_title;
		public TextBox txtSearchListBox;
		public Label labelTotal;
        internal CustomControls.iReverseControls.iReverseToggleButton iReverseToggleButton_SetTheme;
        private DataGridViewCheckBoxColumn Ck;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
    }

}