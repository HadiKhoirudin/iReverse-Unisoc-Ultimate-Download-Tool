using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace iReverse_Unisoc_Ultimate
{
    namespace CustomControls.iReverseControls
    {
        public class iReverseToggleButton : CheckBox
        {
            //Fields
            private Color onBackColor = Color.FromArgb(206, 89, 122);
            private Color onToggleColor = Color.WhiteSmoke;
            private Color offBackColor = Color.Gray;
            private Color offToggleColor = Color.Gainsboro;
            private bool solidStyle = true;

            //Properties
            [Category("iReverse Code Advance")]
            public Color OnBackColor
            {
                get { return onBackColor; }
                set
                {
                    onBackColor = value;
                    this.Invalidate();
                }
            }

            [Category("iReverse Code Advance")]
            public Color OnToggleColor
            {
                get { return onToggleColor; }
                set
                {
                    onToggleColor = value;
                    this.Invalidate();
                }
            }

            [Category("iReverse Code Advance")]
            public Color OffBackColor
            {
                get { return offBackColor; }
                set
                {
                    offBackColor = value;
                    this.Invalidate();
                }
            }

            [Category("iReverse Code Advance")]
            public Color OffToggleColor
            {
                get { return offToggleColor; }
                set
                {
                    offToggleColor = value;
                    this.Invalidate();
                }
            }

            [Browsable(true)]
            public override string Text
            {
                get { return base.Text; }
                set { base.Text = value; }
            }

            [Category("iReverse Code Advance")]
            [DefaultValue(true)]
            public bool SolidStyle
            {
                get { return solidStyle; }
                set
                {
                    solidStyle = value;
                    this.Invalidate();
                }
            }

            //Constructor
            public iReverseToggleButton()
            {
                this.MinimumSize = new Size(32, 16);
            }

            //Methods
            private GraphicsPath GetFigurePath(int txtHeight, int txtWidth)
            {
                int arcSize = this.Height - 1;
                Rectangle leftArc = new Rectangle(0, 0, arcSize, arcSize);
                Rectangle rightArc = new Rectangle((this.Width - txtWidth) - arcSize - 2 + txtHeight, 0, arcSize, arcSize);

                GraphicsPath path = new GraphicsPath();
                path.StartFigure();
                path.AddArc(leftArc, 90, 180);
                path.AddArc(rightArc, 270, 180);
                path.CloseFigure();

                return path;
            }

            protected override void OnPaint(PaintEventArgs pevent)
            {
                var txtWidth = TextRenderer.MeasureText(pevent.Graphics, " " + this.Text + "  ", this.Font).Width;
                var txtHeight = TextRenderer.MeasureText(pevent.Graphics, " " + this.Text + "  ", this.Font).Height;
                int toggleSize = this.Height - 5;
                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                pevent.Graphics.Clear(this.Parent.BackColor);

                if (this.Checked) //ON
                {
                    //Draw the control surface
                    if (solidStyle)
                        pevent.Graphics.FillPath(new SolidBrush(onBackColor), GetFigurePath(txtHeight, txtWidth));
                    else
                        pevent.Graphics.DrawPath(new Pen(onBackColor, 2), GetFigurePath(txtHeight, txtWidth));
                    //Draw the toggle
                    pevent.Graphics.FillEllipse(
                        new SolidBrush(onToggleColor),
                        new Rectangle((this.Width - txtWidth) - this.Height + 1 + txtHeight, 2, toggleSize, toggleSize)
                    );
                }
                else //OFF
                {
                    //Draw the control surface
                    if (solidStyle)
                        pevent.Graphics.FillPath(new SolidBrush(offBackColor), GetFigurePath(txtHeight, txtWidth));
                    else
                        pevent.Graphics.DrawPath(new Pen(offBackColor, 2), GetFigurePath(txtHeight, txtWidth));
                    //Draw the toggle
                    pevent.Graphics.FillEllipse(
                        new SolidBrush(offToggleColor),
                        new Rectangle(2, 2, toggleSize, toggleSize)
                    );
                }

                TextRenderer.DrawText(pevent.Graphics, " " + this.Text + "  ", this.Font, new Rectangle((this.Width - txtWidth) - this.Height + 1 + txtHeight + toggleSize, 1, txtWidth, txtHeight), this.ForeColor, TextFormatFlags.TextBoxControl);
            }
        }
    }
}