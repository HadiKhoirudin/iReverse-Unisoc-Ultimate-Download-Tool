using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace iReverse_Unisoc_Ultimate
{
    namespace iReverseCustomUI
    {
        public partial class Alerts : Form
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

            public Alerts()
            {
                InitializeComponent();
                SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                Region = System.Drawing.Region.FromHrgn(
                    CreateRoundRectRgn(0, 0, Width, Height, 13, 13)
                );
                timer1.Tick += timer1_Tick;
            }

            public enum enmAction
            {
                wait,
                start,
                close
            }

            public enum enmType
            {
                Success,
                Warning,
                Error,
                Info
            }

            private Alerts.enmAction action;

            private int x;
            private int y;

            private void button1_Click(object sender, EventArgs e) { }

            private void timer1_Tick(object sender, EventArgs e)
            {
                switch (this.action)
                {
                    case enmAction.wait:
                        timer1.Interval = 5000;
                        action = enmAction.close;
                        break;
                    case enmAction.start:
                        this.timer1.Interval = 1;
                        this.Opacity += 0.1;
                        if (this.x < this.Location.X)
                        {
                            this.Left -= 1;
                        }
                        else
                        {
                            if (this.Opacity == 1.0)
                            {
                                action = enmAction.wait;
                            }
                        }
                        break;
                    case enmAction.close:
                        timer1.Interval = 1;
                        this.Opacity -= 0.1;

                        this.Left -= 3;
                        if (Opacity == 0.0)
                        {
                            Close();
                        }
                        break;
                }
            }

            private void pictureBox2_Click(object sender, EventArgs e)
            {
                timer1.Interval = 1;
                action = enmAction.close;
            }

            public void showAlert(string msg, enmType type)
            {
                this.Opacity = 0.0;
                this.StartPosition = FormStartPosition.Manual;
                string fname = null;

                for (int i = 1; i <= 9; i++)
                {
                    fname = "alert" + i.ToString();
                    Alerts frm = (Alerts)Application.OpenForms[fname];

                    if (frm == null)
                    {
                        this.Name = fname;
                        this.x = Screen.PrimaryScreen.WorkingArea.Width - this.Width + 15;
                        this.y = Screen.PrimaryScreen.WorkingArea.Height - this.Height * i - 5 * i;
                        this.Location = new Point(this.x, this.y);
                        break;
                    }
                }
                this.x = Screen.PrimaryScreen.WorkingArea.Width - Width - 5;

                switch (type)
                {
                    case enmType.Success:
                        this.pictureBox1.Image = Properties.Resources.success;
                        this.BackColor = Color.SeaGreen;
                        break;
                    case enmType.Error:
                        this.pictureBox1.Image = Properties.Resources._error;
                        this.BackColor = Color.DarkRed;
                        break;
                    case enmType.Info:
                        this.pictureBox1.Image = Properties.Resources.info;
                        this.BackColor = Color.RoyalBlue;
                        break;
                    case enmType.Warning:
                        this.pictureBox1.Image = Properties.Resources.warning;
                        this.BackColor = Color.DarkOrange;
                        break;
                }

                this.lblMsg.Text = msg;

                this.Show();
                this.action = enmAction.start;
                this.timer1.Interval = 1;
                this.timer1.Start();
            }
        }
    }
}
