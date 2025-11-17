using iReverse_Unisoc_Ultimate.UniFlash.Worker;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace iReverse_Unisoc_Ultimate
{
    namespace MyUI
    {
        public class MyListSpdDevice
        {
            #region Create List
            public static List<Tuple<string, string, string, string, string, string>> dataSet =
                new List<Tuple<string, string, string, string, string, string>>();
            public static string Brand { get; set; }
            public static string DevicesName { get; set; }
            public static string ModelName { get; set; }
            public static string Platform { get; set; }

            public static void CreateListDevice()
            {
                try
                {
                    Main.SharedUI.listBoxOneClick.DrawMode = DrawMode.OwnerDrawFixed;
                    Main.SharedUI.listBoxOneClick.ItemHeight = 35;

                    if (!WorkerUnlock.isOneClickServer)
                    {
                        string list_devices = string.Empty;
                        if (File.Exists(Application.StartupPath + "\\Data\\List\\Devices.json"))
                        {
                            list_devices = File.ReadAllText(
                                Application.StartupPath + "\\Data\\List\\Devices.json"
                            );
                            DataSource(list_devices);
                            Main.SharedUI.listBoxOneClick.DataSource = dataSet;
                            Main.SharedUI.labelTotal.Text =
                                dataSet.Count.ToString() + " " + "Devices";
                        }
                    }

                    if (WorkerUnlock.isOneClickServer)
                    {
                        string list_devices = string.Empty;

                        WebRequest webRequest = WebRequest.Create(WorkerUnlock.OneClickList);
                        webRequest.Method = "POST";
                        webRequest.ContentType = "application/x-www-form-urlencoded";
                        webRequest.Timeout = 10000;
                        Stream stream = webRequest.GetRequestStream();
                        stream.Close();
                        WebResponse response = webRequest.GetResponse();
                        stream = response.GetResponseStream();
                        StreamReader streamReader = new StreamReader(stream);
                        HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
                        if (httpWebResponse.StatusCode != HttpStatusCode.OK)
                        {
                            MessageBox.Show(
                                "server Error " + httpWebResponse.StatusCode.ToString(),
                                null,
                                MessageBoxButtons.OK
                            );
                        }
                        else
                        {
                            Main.SharedUI.PanelSPDOneClick.Controls.Clear();
                            while (!streamReader.EndOfStream)
                            {
                                list_devices += streamReader.ReadLine() + Environment.NewLine;
                            }
                        }

                        if (!string.IsNullOrEmpty(list_devices))
                        {
                            DataSource(list_devices);
                            Main.SharedUI.listBoxOneClick.DataSource = dataSet;
                            Main.SharedUI.labelTotal.Text =
                                dataSet.Count.ToString() + " " + "Devices";
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }

            public class Info
            {
                public string Devices { get; set; }
                public string Models { get; set; }
                public string Platform { get; set; }
                public string Conn { get; set; }
                public string Auth { get; set; }
                public string New { get; set; }

                public Info(
                    string Devices,
                    string Models,
                    string Platform,
                    string Conn,
                    string Auth,
                    string New
                )
                {
                    this.Devices = Devices;
                    this.Models = Models;
                    this.Platform = Platform;
                    this.Conn = Conn;
                    this.Auth = Auth;
                    this.New = New;
                }
            }

            public static void DataSource(string path)
            {
                Devicelists = (List<Info>)JsonConvert.DeserializeObject<List<Info>>(path);
                //Devices, Models, Platform, Conn, Auth, [New]
                var Data = new List<Tuple<string, string, string, string, string, string>>();

                foreach (Info inf in Devicelists)
                {
                    Data.Add(
                        new Tuple<string, string, string, string, string, string>(
                            inf.Devices,
                            inf.Models,
                            inf.Platform,
                            inf.Conn,
                            inf.Auth,
                            inf.New
                        )
                    );
                }

                dataSet = Data;
            }

            public static void SearchDevices(string searchText)
            {
                if (Devicelists != null)
                {
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        var searchResults = Devicelists
                            .Where(
                                (user) =>
                                    user.Devices.ToLower().Contains(searchText)
                                    || user.Models.ToLower().Contains(searchText)
                            )
                            .ToList();
                        Main.SharedUI.listBoxOneClick.DataSource = searchResults;
                    }
                    else
                    {
                        Main.SharedUI.listBoxOneClick.DataSource = Devicelists;
                    }
                }
            }

            public static List<Info> Devicelists = new List<Info>();
            #endregion

            #region One Click UI
            public static void txtSearchListBox_GotFocus(object sender, EventArgs e)
            {
                if (Main.SharedUI.txtSearchListBox.Text.Trim(' ') == "Type For Search ...")
                {
                    Main.SharedUI.txtSearchListBox.Text = "";
                }
            }

            public static void txtSearchListBox_LostFocus(object sender, EventArgs e)
            {
                if (string.IsNullOrEmpty(Main.SharedUI.txtSearchListBox.Text.Trim(' ')))
                {
                    Main.SharedUI.txtSearchListBox.Text = "Type For Search ...";
                }
            }

            public static void txtSearchListBox_TextChanged(object sender, EventArgs e)
            {
                if (!string.IsNullOrEmpty(Main.SharedUI.txtSearchListBox.Text))
                {
                    if (Main.SharedUI.txtSearchListBox.Text == "Type For Search ...")
                    {
                        return;
                    }

                    var allStrings = dataSet
                        .SelectMany((tuple) => new string[] { tuple.Item1 + "#" + tuple.Item2 })
                        .ToList();

                    string searchText = Main.SharedUI.txtSearchListBox.Text.ToLower();

                    var searchResults = dataSet
                        .Where(
                            (tuple) =>
                                tuple.Item1.StartsWith(
                                    searchText,
                                    StringComparison.CurrentCultureIgnoreCase
                                )
                                || tuple.Item2.StartsWith(
                                    searchText,
                                    StringComparison.CurrentCultureIgnoreCase
                                )
                                || tuple.Item1.ToLower().Contains(searchText)
                                || tuple.Item2.ToLower().Contains(searchText)
                        )
                        .Select(
                            (tuple) =>
                                new Tuple<string, string, string, string, string, string>(
                                    tuple.Item1,
                                    tuple.Item2,
                                    tuple.Item3,
                                    tuple.Item4,
                                    tuple.Item5,
                                    tuple.Item6
                                )
                        )
                        .ToList();

                    Main.SharedUI.listBoxOneClick.DataSource = searchResults;
                    Main.SharedUI.labelTotal.Text =
                        searchResults.Count.ToString() + " " + "Devices";
                }
                else
                {
                    Main.SharedUI.listBoxOneClick.DataSource = dataSet;
                    Main.SharedUI.labelTotal.Text = dataSet.Count.ToString() + " " + "Devices";
                }
            }

            public static void listBoxOneClick_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (!Main.SharedUI.UnisocWorker.IsBusy)
                {
                    foreach (object item in Main.SharedUI.listBoxOneClick.SelectedItems)
                    {
                        MyDisplay.RtbClear();
                        string str = Main.SharedUI.listBoxOneClick.GetItemText(
                            Main.SharedUI.listBoxOneClick.SelectedItem
                        );

                        if (str.ToLower().Contains("exploit")) { Main.SharedUI.CkAutoRSAExploit.Checked = true; } else { Main.SharedUI.CkAutoRSAExploit.Checked = false; }

                        Console.WriteLine(str);
                        string[] list_str = null;
                        string[] list_brand = null;

                        if (str.Contains(","))
                        {
                            str = str.Substring(1, str.Length - 2);
                            list_str = str.Split(',');

                            if (list_str[0].Contains(" "))
                            {
                                list_brand = list_str[0].Split(' ');
                                Brand = list_brand[0].Replace(" ", string.Empty);
                            }
                            else
                            {
                                Brand = list_str[0].Replace(" ", string.Empty);
                            }

                            DevicesName = list_str[0];
                            ModelName = list_str[1].Replace(" ", string.Empty);
                            Platform = list_str[2].Replace(" ", string.Empty);
                        }

                        MyDisplay.RichLogs(
                            "Device         : " + DevicesName,
                            Color.Black,
                            true,
                            true
                        );
                        MyDisplay.RichLogs(
                            "Model          : " + ModelName,
                            Color.Black,
                            true,
                            true
                        );
                        MyDisplay.RichLogs("Platform       : " + Platform, Color.Black, true, true);

                        WorkerUnlock.SPDOneClickExecModel();
                        break;
                    }
                }
            }

            public static void ListBoxOneClick_DrawItem(object sender, DrawItemEventArgs e)
            {
                Brush _textBrush = Brushes.White;
                Brush _background_dlBrush = Brushes.MediumSlateBlue;
                Brush _background_explBrush = Brushes.Crimson;
                Brush _background_nwBrush = Brushes.LimeGreen;

                Color textColor = ((ListBox)sender).ForeColor;
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e = new DrawItemEventArgs(
                        e.Graphics,
                        e.Font,
                        e.Bounds,
                        e.Index,
                        e.State ^ DrawItemState.Selected,
                        e.ForeColor,
                        Color.SlateBlue
                    );
                    textColor = Color.White;
                }

                var linePen = new Pen(SystemBrushes.Control);
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
                Pen selPen = new Pen(Color.DimGray);
                e.Graphics.DrawRectangle(selPen, r);
                e.DrawFocusRectangle();

                var dataItem = (Tuple<string, string, string, string, string, string>) Main.SharedUI.listBoxOneClick.Items[e.Index] as Tuple<string, string, string, string, string, string>;
                
                var timeFont = new Font("Tahoma", 7, FontStyle.Bold);
                var roomsFont = new Font("Tahoma", 6.8F, FontStyle.Regular);

                //(Gionee F10, F10, Unisoc, Download, Exploit, New Security)
                string Devices = dataItem.Item1;
                string Model = dataItem.Item2;
                string Chipsets = dataItem.Item3;
                string Connection = dataItem.Item4;
                string Auth = dataItem.Item5;
                string New = dataItem.Item6;

                TextRenderer.DrawText(e.Graphics, Devices, timeFont, new Point(e.Bounds.Left + 3, e.Bounds.Top + 5), textColor);

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

                Bitmap _dlBitmap = new Bitmap( TextRenderer.MeasureText(Connection, timeFont).Width, TextRenderer.MeasureText(Connection, timeFont).Height);
                var _dlrectangle = new Rectangle(new Point(0, 0), TextRenderer.MeasureText(Connection, timeFont));

                Graphics _dlg = Graphics.FromImage(_dlBitmap);

                _dlg.FillRectangle(_background_dlBrush, _dlrectangle);
                _dlg.DrawString(Connection, timeFont, _textBrush, new Point(1, 1));

                e.Graphics.DrawImage(_dlBitmap, TextRenderer.MeasureText(Devices, timeFont).Width + 8 - len, e.Bounds.Top + 5, _dlBitmap.Width, _dlBitmap.Height);

                if (!string.IsNullOrEmpty(Auth))
                {
                    string expl = Auth;

                    Bitmap _explBitmap = new Bitmap(TextRenderer.MeasureText(expl, timeFont).Width, TextRenderer.MeasureText(expl, timeFont).Height);
                    
                    var _explrectangle = new Rectangle(new Point(0, 0),TextRenderer.MeasureText(expl, timeFont));
                    
                    Graphics _explg = Graphics.FromImage(_explBitmap);

                    _explg.FillRectangle(_background_explBrush, _explrectangle);
                    _explg.DrawString(expl, timeFont, _textBrush, new Point(1, 1));

                    e.Graphics.DrawImage(_explBitmap, TextRenderer.MeasureText(Devices, timeFont).Width + _dlBitmap.Width + 8 - len + 5, e.Bounds.Top + 5, _explBitmap.Width, _explBitmap.Height );

                    if (!string.IsNullOrEmpty(New))
                    {
                        string nw = New;

                        Bitmap _nwBitmap = new Bitmap(TextRenderer.MeasureText(nw, timeFont).Width,TextRenderer.MeasureText(nw, timeFont).Height);

                        var _nwrectangle = new Rectangle(new Point(0, 0),TextRenderer.MeasureText(nw, timeFont));

                        Graphics _nwg = Graphics.FromImage(_nwBitmap);
                        _nwg.FillRectangle(_background_nwBrush, _nwrectangle);
                        _nwg.DrawString(nw, timeFont, _textBrush, new Point(2, 1));

                        e.Graphics.DrawImage(_nwBitmap, TextRenderer.MeasureText(Devices, timeFont).Width + _dlBitmap.Width + 8 + _explBitmap.Width - len + 8, e.Bounds.Top + 5, _nwBitmap.Width, _nwBitmap.Height );
                    }
                }

                TextRenderer.DrawText(e.Graphics, Model + " - " + Chipsets, roomsFont, new Point(e.Bounds.Left + 3, e.Bounds.Top + 18), textColor);
            }

            #endregion
        }
    }
}
