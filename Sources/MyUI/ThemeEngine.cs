using iReverse_Unisoc_Ultimate.CustomControls.iReverseControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace iReverse_Unisoc_Ultimate.MyUI
{
    public static class ThemeEngine
    {
        public static bool isDebbugging = true;
        public static bool isDark = false;

        public enum Styles
        {
            Dark = 0,
            Light = 1
        }

        public static IEnumerable<Control> GetAllComponents(Control container)
        {
            foreach (Control c in container.Controls)
            {
                foreach (Control child in GetAllComponents(c))
                {
                    yield return child;
                }
                yield return c;
            }
        }

        public static void ThemeSet(object sender, Styles styles)
        {
            if (isDebbugging) Console.WriteLine($"Set Style to : {styles}");

            if (sender is Form form)
            {
                ApplyTheme(form, styles);
            }
            else if (sender is UserControl userControl)
            {
                ApplyTheme(userControl, styles);
            }
        }

        private static void ApplyTheme(Control container, Styles styles)
        {
            Color BackColor_Dark = Color.FromArgb(26, 23, 64);
            Color ForeColor_Dark = Color.Azure;

            Color BackColor_Light = SystemColors.Window;
            Color ForeColor_Light = SystemColors.ControlText;

            Color Btn_BackColor_Dark = Color.FromArgb(48, 48, 48);
            Color Btn_ForeColor_Dark = Color.FromArgb(152, 196, 232);

            Color Btn_BackColor_Light = Color.FromArgb(206, 89, 122);
            Color Btn_ForeColor_Light = SystemColors.ControlText;

            switch (styles)
            {
                case Styles.Dark:
                    container.BackColor = BackColor_Dark;
                    container.ForeColor = ForeColor_Dark;
                    isDark = true;
                    break;
                case Styles.Light:
                    container.BackColor = BackColor_Light;
                    container.ForeColor = ForeColor_Light;
                    isDark = false;
                    break;
                default:
                    return;
            }

            var allComponents = GetAllComponents(container).ToList();

            if (isDebbugging) Console.WriteLine($"Found Component : {allComponents.Count}");

            foreach (var item in allComponents)
            {
                var Type = $"{item.GetType()}"; Type = Type.Substring(Type.LastIndexOf(".")).Replace(".", "");

                if (isDebbugging) Console.WriteLine($"Found : {Type}");

                switch (Type)
                {
                    case "CustomTabControl":

                        var tab = (CustomTabControl)item;
                        if (isDark)
                        {
                            tab.DisplayStyle = TabStyle.Dark;
                        }
                        else
                        {
                            tab.DisplayStyle = TabStyle.iReverse;
                        }
                        break;

                    case "Button":

                        var btn = (Button)item;
                        if (isDark)
                        {
                            btn.FlatStyle = FlatStyle.Flat;
                            btn.BackColor = Btn_BackColor_Dark;
                            btn.ForeColor = Btn_ForeColor_Dark;
                        }
                        else
                        {
                            btn.FlatStyle = FlatStyle.Flat;
                            btn.BackColor = Btn_BackColor_Light;
                            btn.ForeColor = Color.Azure;
                        }
                        break;
                        
                    case "iReverseListBox":

                        var listBox = (iReverseListBox)item;
                        if (isDark)
                        {
                            listBox.ListTextColor = Color.White;
                            listBox.BackColor = container.BackColor;
                            listBox.ForeColor = Btn_ForeColor_Dark;
                        }
                        else
                        {
                            listBox.ListTextColor = Color.Black;
                            listBox.BackColor = Color.White;
                            listBox.ForeColor = Color.Azure;
                        }
                        break;

                    case "ComboBox":

                        var combobox = (ComboBox)item;
                        if (isDark)
                        {
                            combobox.FlatStyle = FlatStyle.Flat;
                            item.BackColor = Btn_BackColor_Dark;
                            item.ForeColor = Btn_ForeColor_Dark;
                        }
                        else
                        {
                            combobox.FlatStyle = FlatStyle.Standard;
                            item.BackColor = container.BackColor;
                            item.ForeColor = container.ForeColor;
                        }
                        break;

                    case "TextBox":

                        var txtbox = (TextBox)item;
                        txtbox.BackColor = container.BackColor;
                        txtbox.ForeColor = container.ForeColor;
                        if (isDark)
                        {
                            txtbox.BorderStyle = BorderStyle.FixedSingle;
                        }
                        else
                        {
                            txtbox.BorderStyle = BorderStyle.Fixed3D;
                        }
                        break;

                    case "DataGridView":

                        var gridView = (DataGridView)item;
                        gridView.BackgroundColor = container.BackColor;
                        gridView.ForeColor = container.ForeColor;
                        DataGridViewCellStyle dataGridViewCellStyles = new DataGridViewCellStyle();
                        dataGridViewCellStyles.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridViewCellStyles.BackColor = container.BackColor;
                        dataGridViewCellStyles.Font = new Font("Nirmala UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                        if (isDark)
                        {
                            dataGridViewCellStyles.ForeColor = Color.Azure;
                            dataGridViewCellStyles.SelectionBackColor = Btn_ForeColor_Dark;
                            dataGridViewCellStyles.SelectionForeColor = Color.Azure;
                        }
                        else
                        {
                            dataGridViewCellStyles.ForeColor = Color.Black;
                            dataGridViewCellStyles.SelectionBackColor = Color.FromArgb(206, 89, 122);
                            dataGridViewCellStyles.SelectionForeColor = Color.Azure;
                        }
                        dataGridViewCellStyles.WrapMode = DataGridViewTriState.True;
                        gridView.RowsDefaultCellStyle = dataGridViewCellStyles;
                        gridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyles;
                        break;

                    case "ListView":

                        var listview = (ListView)item;

                        if (isDark)
                        {
                            ListView_ColumnHeaderTextColor = Color.Azure;
                        }
                        else
                        {
                            ListView_ColumnHeaderTextColor = Color.Black;
                        }

                        ListView_ColumnHeaderColor = container.BackColor;

                        listview.BackColor = container.BackColor;
                        listview.ForeColor = container.ForeColor;

                        listview.DrawColumnHeader -= ListView_DrawColumnHeader;
                        listview.DrawColumnHeader += ListView_DrawColumnHeader;

                        listview.DrawItem -= ListView_DrawItem;
                        listview.DrawItem += ListView_DrawItem;

                        break;

                    case "RichTextBox":

                        var rchtxtbox = (RichTextBox)item;
                        rchtxtbox.BackColor = container.BackColor;
                        rchtxtbox.ForeColor = container.ForeColor;
                        break;

                    case "Panel":

                        var pnl = (Panel)item;
                        if (!pnl.Name.Contains("panel_top"))
                        {
                            item.BackColor = container.BackColor;
                            item.ForeColor = container.ForeColor;
                        }
                        else if (pnl.Name.Contains("panel_top") && isDark)
                        {
                            item.BackColor = Btn_ForeColor_Dark;
                        }
                        else if (pnl.Name.Contains("panel_top") && !isDark)
                        {
                            item.BackColor = Color.Crimson;
                        }
                        break;

                    case "iReverseProgressBar":

                        var prg = item as CustomControls.iReverseControls.iReverseProgressBar;

                        if (isDark)
                        {
                            prg.BackColor = container.BackColor;
                            prg.ForeBackColor = container.ForeColor;
                            prg.ForeColor = ForeColor_Light;
                            prg.SliderColor = Btn_ForeColor_Dark;
                        }
                        else
                        {
                            prg.BackColor = container.BackColor;
                            prg.ForeBackColor = Color.FromArgb(206, 89, 122);
                            prg.ForeColor = ForeColor_Dark;
                            prg.SliderColor = Color.Crimson;
                        }

                        break;

                    case "iReverseButton":

                        var ibtn = (iReverseButton)item;
                        if (ibtn.Name.Contains("Close"))
                        {
                            // Do Nothing
                        }
                        else if (ibtn.Name.Contains("Minimize")) 
                        { 
                            // Do Nothing
                        }
                        else
                        {
                            if (isDark)
                            {
                                ibtn.FlatStyle = FlatStyle.Flat;
                                ibtn.BackColor = Btn_BackColor_Dark;
                                ibtn.ForeColor = Btn_ForeColor_Dark;
                            }
                            else
                            {
                                ibtn.FlatStyle = FlatStyle.Flat;
                                ibtn.BackColor = Btn_BackColor_Light;
                                ibtn.ForeColor = Color.Azure;
                            }
                        }
                        break;

                    default:
                        item.BackColor = container.BackColor;
                        item.ForeColor = container.ForeColor;
                        break;
                }
            }

            if (Main.SharedUI != null)
            {
                Main.SharedUI.IReverseProgressBar1.Value = 1;
                Main.SharedUI.IReverseProgressBar1.Value = 0;
                Main.SharedUI.IReverseProgressBar2.Value = 1;
                Main.SharedUI.IReverseProgressBar2.Value = 0;
            }

            if (isDebbugging) Console.WriteLine(" ");
        }

        private static Color ListView_ColumnHeaderColor;
        private static Color ListView_ColumnHeaderTextColor;

        private static void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            var brush = new SolidBrush(ListView_ColumnHeaderColor);
            e.Graphics.FillRectangle(brush, e.Bounds);
            var textBounds = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 5, e.Bounds.Width, e.Bounds.Height);
            e.Graphics.DrawString(e.Header.Text, e.Font, new SolidBrush(ListView_ColumnHeaderTextColor), textBounds);
        }

        private static void ListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }
    }
}
