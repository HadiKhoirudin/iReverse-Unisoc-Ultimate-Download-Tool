using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace iReverse_Unisoc_Ultimate
{
    namespace CustomControls.iReverseControls
    {
        // HadiK-IT Custom
        public enum TextPosition
        {
            Left,
            Right,
            Center,
            Sliding,
            None
        }

        public class iReverseProgressBar : ProgressBar
        {
            //Fields
            //-> Appearance
            private Color channelColor_Conflict = Color.LightSteelBlue;
            private Color sliderColor_Conflict = Color.RoyalBlue;
            private Color foreBackColor_Conflict = Color.RoyalBlue;
            private int channelHeight_Conflict = 6;
            private int sliderHeight_Conflict = 6;
            private TextPosition showValue_Conflict = TextPosition.Right;
            private string symbolBefore_Conflict = "";
            private string symbolAfter_Conflict = "";
            private bool showMaximun_Conflict = false;

            //-> Others
            private bool paintedBack = false;
            private bool stopPainting = false;

            //Constructor
            public iReverseProgressBar()
            {
                this.SetStyle(ControlStyles.UserPaint, true);
                this.ForeColor = Color.White;
            }

            //Properties
            [Category("iReverse Code Advance")]
            public Color ChannelColor
            {
                get { return channelColor_Conflict; }
                set
                {
                    channelColor_Conflict = value;
                    this.Invalidate();
                }
            }

            [Category("iReverse Code Advance")]
            public Color SliderColor
            {
                get { return sliderColor_Conflict; }
                set
                {
                    sliderColor_Conflict = value;
                    this.Invalidate();
                }
            }

            [Category("iReverse Code Advance")]
            public Color ForeBackColor
            {
                get { return foreBackColor_Conflict; }
                set
                {
                    foreBackColor_Conflict = value;
                    this.Invalidate();
                }
            }

            [Category("iReverse Code Advance")]
            public int ChannelHeight
            {
                get { return channelHeight_Conflict; }
                set
                {
                    channelHeight_Conflict = value;
                    this.Invalidate();
                }
            }

            [Category("iReverse Code Advance")]
            public int SliderHeight
            {
                get { return sliderHeight_Conflict; }
                set
                {
                    sliderHeight_Conflict = value;
                    this.Invalidate();
                }
            }

            [Category("iReverse Code Advance")]
            public TextPosition ShowValue
            {
                get { return showValue_Conflict; }
                set
                {
                    showValue_Conflict = value;
                    this.Invalidate();
                }
            }

            [Category("iReverse Code Advance")]
            public string SymbolBefore
            {
                get { return symbolBefore_Conflict; }
                set
                {
                    symbolBefore_Conflict = value;
                    this.Invalidate();
                }
            }

            [Category("iReverse Code Advance")]
            public string SymbolAfter
            {
                get { return symbolAfter_Conflict; }
                set
                {
                    symbolAfter_Conflict = value;
                    this.Invalidate();
                }
            }

            [Category("iReverse Code Advance")]
            public bool ShowMaximun
            {
                get { return showMaximun_Conflict; }
                set
                {
                    showMaximun_Conflict = value;
                    this.Invalidate();
                }
            }

            [Category("iReverse Code Advance")]
            [Browsable(true)]
            [EditorBrowsable(EditorBrowsableState.Always)]
            public override Font Font
            {
                get { return base.Font; }
                set { base.Font = value; }
            }

            [Category("iReverse Code Advance")]
            public override Color ForeColor
            {
                get { return base.ForeColor; }
                set { base.ForeColor = value; }
            }

            //-> Paint the background & channel
            protected override void OnPaintBackground(PaintEventArgs pevent)
            {
                if (stopPainting == false)
                {
                    if (paintedBack == false)
                    {
                        //Fields
                        Graphics graph = pevent.Graphics;
                        Rectangle rectChannel = new Rectangle(0, 0, this.Width, ChannelHeight);
                        using (var brushChannel = new SolidBrush(channelColor_Conflict))
                        {
                            if (channelHeight_Conflict >= sliderHeight_Conflict)
                            {
                                rectChannel.Y = this.Height - channelHeight_Conflict;
                            }
                            else
                            {
                                rectChannel.Y =
                                    this.Height
                                    - ((channelHeight_Conflict + sliderHeight_Conflict) / 2);
                            }

                            //Painting
                            graph.Clear(this.Parent.BackColor); //Surface
                            graph.FillRectangle(brushChannel, rectChannel); //Channel

                            //Stop painting the back & Channel
                            if (this.DesignMode == false)
                            {
                                paintedBack = true;
                            }
                        }
                    }
                    //Reset painting the back & channel
                    if (this.Value == this.Maximum || this.Value == this.Minimum)
                    {
                        paintedBack = false;
                    }
                }
            }

            //-> Paint slider
            protected override void OnPaint(PaintEventArgs e)
            {
                if (stopPainting == false)
                {
                    //Fields
                    Graphics graph = e.Graphics;
                    double scaleFactor = (
                        ((double)this.Value - this.Minimum) / ((double)this.Maximum - this.Minimum)
                    );
                    int sliderWidth = Convert.ToInt32(Math.Truncate(this.Width * scaleFactor));
                    Rectangle rectSlider = new Rectangle(0, 0, sliderWidth, sliderHeight_Conflict);
                    using (var brushSlider = new SolidBrush(sliderColor_Conflict))
                    {
                        if (sliderHeight_Conflict >= channelHeight_Conflict)
                        {
                            rectSlider.Y = this.Height - sliderHeight_Conflict;
                        }
                        else
                        {
                            rectSlider.Y =
                                this.Height
                                - ((sliderHeight_Conflict + channelHeight_Conflict) / 2);
                        }

                        //Painting
                        if (sliderWidth > 1) //Slider
                        {
                            graph.FillRectangle(brushSlider, rectSlider);
                        }
                        if (showValue_Conflict != TextPosition.None) //Text
                        {
                            DrawValueText(graph, sliderWidth, rectSlider);
                        }
                    }
                }
                if (this.Value == this.Maximum)
                {
                    stopPainting = true; //Stop painting
                }
                else
                {
                    stopPainting = false; //Keep painting
                }
            }

            //-> Paint value text
            private void DrawValueText(Graphics graph, int sliderWidth, Rectangle rectSlider)
            {
                //Fields
                string text_Conflict =
                    symbolBefore_Conflict + this.Value.ToString() + symbolAfter_Conflict;
                if (showMaximun_Conflict)
                {
                    text_Conflict =
                        text_Conflict
                        + "/"
                        + symbolBefore_Conflict
                        + this.Maximum.ToString()
                        + symbolAfter_Conflict;
                }
                var textSize = TextRenderer.MeasureText(text_Conflict, this.Font);
                var rectText = new Rectangle(0, 0, textSize.Width, textSize.Height + 2);
                using (var brushText = new SolidBrush(this.ForeColor))
                {
                    using (var brushTextBack = new SolidBrush(foreBackColor_Conflict))
                    {
                        using (var textFormat = new StringFormat())
                        {
                            switch (showValue_Conflict)
                            {
                                case TextPosition.Left:
                                    rectText.X = 0;
                                    textFormat.Alignment = StringAlignment.Near;

                                    break;
                                case TextPosition.Right:
                                    rectText.X = this.Width - textSize.Width;
                                    textFormat.Alignment = StringAlignment.Far;

                                    break;
                                case TextPosition.Center:
                                    rectText.X = (this.Width - textSize.Width) / 2;
                                    textFormat.Alignment = StringAlignment.Center;

                                    break;
                                case TextPosition.Sliding:
                                    rectText.X = sliderWidth - textSize.Width;
                                    textFormat.Alignment = StringAlignment.Center;
                                    //Clean previous text surface
                                    using (var brushClear = new SolidBrush(this.Parent.BackColor))
                                    {
                                        var rect = rectSlider;
                                        rect.Y = rectText.Y;
                                        rect.Height = rectText.Height;
                                        graph.FillRectangle(brushClear, rect);
                                    }
                                    break;
                            }
                            //Painting
                            graph.FillRectangle(brushTextBack, rectText);
                            graph.DrawString(
                                text_Conflict,
                                this.Font,
                                brushText,
                                rectText,
                                textFormat
                            );
                        }
                    }
                }
            }
        }
    }
}
