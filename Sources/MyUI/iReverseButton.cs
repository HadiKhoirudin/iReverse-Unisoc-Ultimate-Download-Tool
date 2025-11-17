using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace iReverse_Unisoc_Ultimate
{
    namespace CustomControls.iReverseControls
    {
        public class iReverseButton : Button
        {
            //Fields
            private int borderSize_Conflict = 0;
            private int borderRadius_Conflict = 0;
            private Color borderColor_Conflict = Color.PaleVioletRed;

            //Properties
            [Category("iReverse Code Advance")]
            public int BorderSize
            {
                get { return borderSize_Conflict; }
                set
                {
                    borderSize_Conflict = value;
                    this.Invalidate();
                }
            }

            [Category("iReverse Code Advance")]
            public int BorderRadius
            {
                get { return borderRadius_Conflict; }
                set
                {
                    borderRadius_Conflict = value;
                    this.Invalidate();
                }
            }

            [Category("iReverse Code Advance")]
            public Color BorderColor
            {
                get { return borderColor_Conflict; }
                set
                {
                    borderColor_Conflict = value;
                    this.Invalidate();
                }
            }

            [Category("iReverse Code Advance")]
            public Color BackgroundColor
            {
                get { return this.BackColor; }
                set { this.BackColor = value; }
            }

            [Category("iReverse Code Advance")]
            public Color TextColor
            {
                get { return this.ForeColor; }
                set { this.ForeColor = value; }
            }

            //Constructor
            public iReverseButton()
            {
                this.borderSize_Conflict = 0;
                this.borderRadius_Conflict = 5;
                this.Size = new Size(105, 24);
                this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.FlatAppearance.BorderSize = 0;
                this.BackColor = Color.MediumSlateBlue;
                this.ForeColor = Color.White;
                this.Resize += Button_Resize;
            }

            //Methods
            private GraphicsPath GetFigurePath(Rectangle rect, int radius)
            {
                GraphicsPath path = new GraphicsPath();
                float curveSize = radius * 2F;

                path.StartFigure();
                path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
                path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
                path.AddArc(
                    rect.Right - curveSize,
                    rect.Bottom - curveSize,
                    curveSize,
                    curveSize,
                    0,
                    90
                );
                path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
                path.CloseFigure();
                return path;
            }

            protected override void OnPaint(PaintEventArgs pevent)
            {
                base.OnPaint(pevent);

                Rectangle rectSurface = this.ClientRectangle;
                Rectangle rectBorder = Rectangle.Inflate(
                    rectSurface,
                    -borderRadius_Conflict,
                    -borderSize_Conflict
                );
                int smoothSize = 2;
                if (borderSize_Conflict > 0)
                {
                    smoothSize = borderSize_Conflict;
                }

                if (borderRadius_Conflict > 2) //Rounded button
                {
                    using (
                        GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius_Conflict)
                    )
                    {
                        using (
                            GraphicsPath pathBorder = GetFigurePath(
                                rectBorder,
                                borderRadius_Conflict - borderSize_Conflict
                            )
                        )
                        {
                            using (Pen penSurface = new Pen(this.Parent.BackColor, smoothSize))
                            {
                                using (
                                    Pen penBorder = new Pen(
                                        borderColor_Conflict,
                                        borderSize_Conflict
                                    )
                                )
                                {
                                    pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                                    //Button surface
                                    this.Region = new Region(pathSurface);
                                    //Draw surface border for HD result
                                    pevent.Graphics.DrawPath(penSurface, pathSurface);

                                    //Button border
                                    if (borderSize_Conflict >= 1)
                                    {
                                        //Draw control border
                                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                                    }
                                }
                            }
                        }
                    }
                }
                else //Normal button
                {
                    pevent.Graphics.SmoothingMode = SmoothingMode.None;
                    //Button surface
                    this.Region = new Region(rectSurface);
                    //Button border
                    if (borderSize_Conflict >= 1)
                    {
                        using (Pen penBorder = new Pen(borderColor_Conflict, borderSize_Conflict))
                        {
                            penBorder.Alignment = PenAlignment.Inset;
                            pevent.Graphics.DrawRectangle(
                                penBorder,
                                0,
                                0,
                                this.Width - 1,
                                this.Height - 1
                            );
                        }
                    }
                }
            }

            protected override void OnHandleCreated(EventArgs e)
            {
                base.OnHandleCreated(e);
                Parent.BackColorChanged += Container_BackColorChanged;
            }

            private void Container_BackColorChanged(object sender, EventArgs e)
            {
                this.Invalidate();
            }

            private void Button_Resize(object sender, EventArgs e)
            {
                if (borderRadius_Conflict > this.Height)
                {
                    borderRadius_Conflict = this.Height;
                }
            }
        }
    }
}
