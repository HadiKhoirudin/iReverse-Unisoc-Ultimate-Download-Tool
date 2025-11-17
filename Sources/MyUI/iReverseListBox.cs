using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace iReverse_Unisoc_Ultimate
{
    namespace CustomControls.iReverseControls
    {
        public class iReverseListBox : ListBox
        {
            private Color onItemSelectedColor = Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(138)))), ((int)(((byte)(176)))));
            [Category("iReverse Code Advance")]
            public Color OnItemSelectedColor
            {
                get { return onItemSelectedColor; }
                set
                {
                    onItemSelectedColor = value;
                    this.Invalidate();
                }
            }

            private Color onTextSelectedColor = Color.White;
            [Category("iReverse Code Advance")]
            public Color OnTextSelectedColor
            {
                get { return onTextSelectedColor; }
                set
                {
                    onTextSelectedColor = value;
                    this.Invalidate();
                }
            }

            private Color listTextColor = Color.Black;
            [Category("iReverse Code Advance")]
            public Color ListTextColor
            {
                get { return listTextColor; }
                set
                {
                    listTextColor = value;
                    this.Invalidate();
                }
            }

            private Color itemTextColor = Color.White;
            [Category("iReverse Code Advance")]
            public Color ItemTextColor
            {
                get { return itemTextColor; }
                set
                {
                    itemTextColor = value;
                    this.Invalidate();
                }
            }

            private Color connectionColor = Color.MediumSlateBlue;
            [Category("iReverse Code Advance")]
            public Color ConnectionColor
            {
                get { return connectionColor; }
                set
                {
                    connectionColor = value;
                    this.Invalidate();
                }
            }

            private Color authColor = Color.Crimson;
            [Category("iReverse Code Advance")]
            public Color AuthColor
            {
                get { return authColor; }
                set
                {
                    authColor = value;
                    this.Invalidate();
                }
            }

            private Color newColor = Color.LimeGreen;
            [Category("iReverse Code Advance")]
            public Color NewColor
            {
                get { return newColor; }
                set
                {
                    newColor = value;
                    this.Invalidate();
                }
            }

            private Color borderListColor = Color.Black;
            [Category("iReverse Code Advance")]
            public Color BorderListColor
            {
                get { return borderListColor; }
                set
                {
                    borderListColor = value;
                    this.Invalidate();
                }
            }

            private Font itemFont = new Font("Tahoma", 7, FontStyle.Bold);
            [Category("iReverse Code Advance")]
            public Font ItemFont
            {
                get { return itemFont; }
                set
                {
                    itemFont = value;
                    this.Invalidate();
                }
            }

            private Font descriptionFont = new Font("Tahoma", 6.8F, FontStyle.Regular);
            [Category("iReverse Code Advance")]
            public Font DescriptionFont
            {
                get { return descriptionFont; }
                set
                {
                    descriptionFont = value;
                    this.Invalidate();
                }
            }

            public iReverseListBox()
            {
                this.ItemHeight = 35;
                this.DrawMode = DrawMode.OwnerDrawFixed;
                this.Font = new Font("Consolas", 8.25F, FontStyle.Bold);
                //if (this.DesignMode) this.Items.AddRange(new object[] { "(Devices, Model, Chipsets, Connection, Auth, New)" });
                ListTextColor = this.ForeColor;
            }

            protected override void OnDrawItem(DrawItemEventArgs e)
            {
                if (e.Index < 0 || e.Index >= Items.Count) return;

                string str = this.GetItemText(Items[e.Index]).TrimStart('(').TrimEnd(')');
                if (string.IsNullOrEmpty(str) || !str.Contains(",")) return;

                string[] list_str = str.Split(',');

                //(Devices, Model, Chipsets, Connection, Auth, New)
                string Devices;
                string Model;
                string Chipsets;
                string Connection;
                string Auth;
                string New;

                switch (list_str.Length - 1)
                {
                    case 1:
                        Devices = list_str[0].Trim();
                        Model = list_str[1].Trim();
                        Chipsets = "Qualcomm";
                        Connection = "EDL";
                        Auth = "";
                        New = "";

                        break;

                    case 2:
                        Devices = list_str[0].Trim();
                        Model = list_str[1].Trim();
                        Chipsets = list_str[2].Trim();
                        Connection = "EDL";
                        Auth = "";
                        New = "";

                        break;

                    case 3:
                        Devices = list_str[0].Trim();
                        Model = list_str[1].Trim();
                        Chipsets = list_str[2].Trim();
                        Connection = list_str[3].Trim();
                        Auth = "";
                        New = "";

                        break;

                    case 4:
                        Devices = list_str[0].Trim();
                        Model = list_str[1].Trim();
                        Chipsets = list_str[2].Trim();
                        Connection = list_str[3].Trim();
                        Auth = list_str[4].Trim();
                        New = "";

                        break;

                    case 5:
                        Devices = list_str[0].Trim();
                        Model = list_str[1].Trim();
                        Chipsets = list_str[2].Trim();
                        Connection = list_str[3].Trim();
                        Auth = list_str[4].Trim();
                        New = list_str[5].Trim();

                        break;

                    default:
                        Devices = "Unknown";
                        Model = "";
                        Chipsets = "Qualcomm";
                        Connection = "EDL";
                        Auth = "";
                        New = "";
                        break;
                }

                Color textColor = ListTextColor;
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e = new DrawItemEventArgs(
                        e.Graphics,
                        e.Font,
                        e.Bounds,
                        e.Index,
                        e.State ^ DrawItemState.Selected,
                        e.ForeColor,
                        OnItemSelectedColor
                    );
                    textColor = OnTextSelectedColor;
                }

                var linePen = new Pen(BorderListColor);
                var lineStartPoint = new Point(e.Bounds.Left, e.Bounds.Height + e.Bounds.Top);
                var lineEndPoint = new Point(e.Bounds.Width, e.Bounds.Height + e.Bounds.Top);
                e.Graphics.DrawLine(linePen, lineStartPoint, lineEndPoint);

                e.DrawBackground();

                Rectangle r = new Rectangle(
                    e.Bounds.X,
                    e.Bounds.Y,
                    e.Bounds.Width - 1,
                    e.Bounds.Height
                );

                Pen selPen = new Pen(BorderListColor);
                e.Graphics.DrawRectangle(selPen, r);
                e.DrawFocusRectangle();

                TextRenderer.DrawText(e.Graphics, Devices, ItemFont, new Point(e.Bounds.Left + 3, e.Bounds.Top + 5), textColor);

                int len = Devices.Length;

                if (len > 3 && len < 13)
                {
                    len = 2;
                }
                else if (len > 10 && len < 16)
                {
                    len = 4;
                }
                else if (len > 10 && len < 20)
                {
                    len = 4;
                }
                else if (len > 10 && len < 23)
                {
                    len = 5;
                }
                else if (len > 10 && len < 27)
                {
                    len = 6;
                }
                else if (len > 10 && len < 32)
                {
                    len = 8;
                }
                else
                {
                    len = 10;
                }

                Bitmap _connectionBitmap = new Bitmap(TextRenderer.MeasureText(Connection, ItemFont).Width, TextRenderer.MeasureText(Connection, ItemFont).Height);
                var _connectionrectangle = new Rectangle(new Point(0, 0), TextRenderer.MeasureText(Connection, ItemFont));

                Graphics _connectiong = Graphics.FromImage(_connectionBitmap);

                _connectiong.FillRectangle(new SolidBrush(ConnectionColor), _connectionrectangle);
                _connectiong.DrawString(Connection, ItemFont, new SolidBrush(ItemTextColor), new Point(2, 1));

                e.Graphics.DrawImage(_connectionBitmap, TextRenderer.MeasureText(Devices, ItemFont).Width + 8 - len, e.Bounds.Top + 5, _connectionBitmap.Width, _connectionBitmap.Height);

                if (!string.IsNullOrEmpty(Auth))
                {
                    string expl = Auth;

                    Bitmap _authBitmap = new Bitmap(TextRenderer.MeasureText(expl, ItemFont).Width, TextRenderer.MeasureText(expl, ItemFont).Height);

                    var _authrectangle = new Rectangle(new Point(0, 0), TextRenderer.MeasureText(expl, ItemFont));

                    Graphics _authg = Graphics.FromImage(_authBitmap);

                    _authg.FillRectangle(new SolidBrush(AuthColor), _authrectangle);
                    _authg.DrawString(expl, ItemFont, new SolidBrush(ItemTextColor), new Point(1, 1));

                    e.Graphics.DrawImage(_authBitmap, TextRenderer.MeasureText(Devices, ItemFont).Width + _connectionBitmap.Width + 8 - len + 5, e.Bounds.Top + 5, _authBitmap.Width, _authBitmap.Height);

                    if (!string.IsNullOrEmpty(New))
                    {
                        string nw = New;

                        Bitmap _newBitmap = new Bitmap(TextRenderer.MeasureText(nw, ItemFont).Width, TextRenderer.MeasureText(nw, ItemFont).Height);

                        var _newrectangle = new Rectangle(new Point(0, 0), TextRenderer.MeasureText(nw, ItemFont));

                        Graphics _newg = Graphics.FromImage(_newBitmap);
                        _newg.FillRectangle(new SolidBrush(NewColor), _newrectangle);
                        _newg.DrawString(nw, ItemFont, new SolidBrush(ItemTextColor), new Point(1, 1));

                        e.Graphics.DrawImage(_newBitmap, TextRenderer.MeasureText(Devices, ItemFont).Width + _connectionBitmap.Width + 8 + _authBitmap.Width - len + 8, e.Bounds.Top + 5, _newBitmap.Width, _newBitmap.Height);
                    }
                }

                TextRenderer.DrawText(e.Graphics, Model + " - " + Chipsets, DescriptionFont, new Point(e.Bounds.Left + 3, e.Bounds.Top + 18), textColor);

                e.DrawFocusRectangle();
            }
        }
    }
}