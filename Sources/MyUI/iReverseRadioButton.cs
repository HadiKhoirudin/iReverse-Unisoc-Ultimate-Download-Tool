using System;

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace iReverse_Unisoc_Ultimate
{
    namespace CustomControls.iReverseControls
    {
        public class iReverseRadioButton : RadioButton
        {
            private Color checkedColorField = Color.MediumSlateBlue;
            private Color unCheckedColorField = Color.Gray;

            //Properties
            [Category("iReverse Code Advance")]
            public Color CheckedColor
            {
                get { return checkedColorField; }
                set
                {
                    checkedColorField = value;
                    Invalidate();
                }
            }

            [Category("iReverse Code Advance")]
            public Color UnCheckedColor
            {
                get { return unCheckedColorField; }
                set
                {
                    unCheckedColorField = value;
                    Invalidate();
                }
            }

            //Constructor
            public iReverseRadioButton()
            {
                MinimumSize = new Size(0, 13);
                //Add a padding of 10 to the left to have a considerable distance between the text and the RadioButton.
                Padding = new Padding(10, 0, 0, 0);

                this.BackColor = Color.White;
            }

            //Overridden methods
            protected override void OnPaint(PaintEventArgs pevent)
            {
                //Fields
                var graphics = pevent.Graphics;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rbBorderSize = 13.0F;
                var rbCheckSize = 8.0F;

                RectangleF rectRbBorder = new RectangleF()
                {
                    X = 0.5F,
                    Y = (Height - rbBorderSize) / 2,
                    Width = Convert.ToInt32(rbBorderSize),
                    Height = Convert.ToInt32(rbBorderSize)
                };

                RectangleF rectRbCheck = new RectangleF()
                {
                    X = rectRbBorder.X + (rectRbBorder.Width - rbCheckSize) / 2,
                    Y = (Height - rbCheckSize) / 2,
                    Width = Convert.ToInt32(rbCheckSize),
                    Height = Convert.ToInt32(rbCheckSize)
                };

                //Drawing
                using (Pen penBorder = new Pen(checkedColorField, 1.6F))
                {
                    using (SolidBrush brushRbCheck = new SolidBrush(checkedColorField))
                    {
                        using (SolidBrush brushText = new SolidBrush(ForeColor))
                        {
                            //Draw surface
                            graphics.Clear(BackColor);
                            //Draw Radio Button
                            if (Checked)
                            {
                                graphics.DrawEllipse(penBorder, rectRbBorder); //Circle border
                                graphics.FillEllipse(brushRbCheck, rectRbCheck); //Circle Radio Check
                            }
                            else
                            {
                                penBorder.Color = unCheckedColorField;
                                graphics.DrawEllipse(penBorder, rectRbBorder); //Circle border
                            }
                            //Draw text
                            graphics.DrawString(
                                Text,
                                base.Font,
                                brushText,
                                rbBorderSize + 8,
                                Height - TextRenderer.MeasureText(Text, base.Font).Height - 2
                            ); //Y=Center
                        }
                    }
                }
            }
        }
    }
}
