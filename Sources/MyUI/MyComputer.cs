using System;

using System.Management;
using System.Runtime.InteropServices;

namespace iReverse_Unisoc_Ultimate
{
    namespace MyUI
    {
        internal static class MyComputers
        {
            public static string Win;
            public static string Major;
            public static string Minor;
            public static string Build;

            private struct OSVERSIONINFOW
            {
                public int dwOSVersionInfoSize;
                public int dwMajorVersion;
                public int dwMinorVersion;
                public int dwBuildNumber;
                public int dwPlatformId;

                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
                public string szCSDVersion;
            }

            [DllImport(
                "ntdll",
                EntryPoint = "RtlGetVersion",
                ExactSpelling = true,
                CharSet = CharSet.Ansi,
                SetLastError = true
            )]
            private static extern int RtlGetVersion(ref OSVERSIONINFOW lpVersionInformation);

            private static void GetWinVer()
            {
                OSVERSIONINFOW VersionInfo = new OSVERSIONINFOW();
                VersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(VersionInfo);
                if (RtlGetVersion(ref VersionInfo) == 0)
                {
                    Major = VersionInfo.dwMajorVersion.ToString();
                    Minor = VersionInfo.dwMinorVersion.ToString();
                    Build = VersionInfo.dwBuildNumber.ToString();
                }
                string tempVar = Major + "." + Minor;
                VersionToName(ref tempVar);
            }

            private static string VersionToName(ref string sVersion)
            {
                if (Major == "10" && Convert.ToInt32(Build) > 19044)
                {
                    sVersion = 11.ToString() + "." + Minor;
                }
                switch (sVersion)
                {
                    case "6.0":
                        Win = "Windows Vista";
                        break;
                    case "6.1":
                        Win = "Windows 7";
                        break;
                    case "6.2":
                        Win = "Windows 8";
                        break;
                    case "6.3":
                        Win = "Windows 8.1";
                        break;
                    case "10.0":
                        Win = "Windows 10";
                        break;
                    case "11.0":
                        Win = "Windows 11";
                        break;
                    default:
                        Win = "Unknown";
                        break;
                }
                return Win;
            }

            public static void GetWindowsVersion()
            {
                GetWinVer();
                Console.WriteLine(Win + "-" + Major + "." + Minor + "." + Build);
            }

            public static void SetOSInstallDate()
            {
                try
                {
                    string myAlias = "Win32_OperatingSystem";
                    string strOut = string.Empty;

                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                        "SELECT * FROM " + myAlias
                    );
                    ManagementObjectCollection colItems = searcher.Get();

                    string dateString = string.Empty;
                    foreach (ManagementObject objComputer in colItems)
                    {
                        dateString = objComputer["InstallDate"].ToString();
                    }

                    string year = dateString.Substring(0, 4);
                    string mth = dateString.Substring(4, 2);
                    string mdate = dateString.Substring(6, 2);
                    string mHr = dateString.Substring(8, 2);
                    string mMin = dateString.Substring(10, 2);
                    string mSS = dateString.Substring(12, 2);

                    string firstDate = $"{mdate}-{mth}-{year} {mHr}:{mMin}:{mSS}";

                    Console.WriteLine(firstDate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
