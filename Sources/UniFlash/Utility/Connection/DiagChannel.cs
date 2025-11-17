using iReverse_Unisoc_Ultimate.Utility.Connection.API;
using System;
using System.Threading;

namespace iReverse_Unisoc_Ultimate
{
    namespace Utility.Connection
    {
        internal static class DiagChannel
        {
            public static PhoneCommandAPI.SP_HANDLE hDiagPhone;
            public static byte[] ChannelBuffer = new byte[1024];

            public static void DiagConnect(string PortCom)
            {
                IntPtr logUtilPtr = IntPtr.Zero;
                hDiagPhone = PhoneCommandAPI.SP_CreatePhone(logUtilPtr);

                PhoneCommandAPI.CHANNEL_ATTRIBUTE openArgument = new PhoneCommandAPI.CHANNEL_ATTRIBUTE();
                openArgument.ChannelType = PhoneCommandAPI.CHANNEL_TYPE.CHANNEL_TYPE_COM;
                openArgument.Com.dwPortNum = uint.Parse(PortCom);
                openArgument.Com.dwBaudRate = 115200; //115200
                Console.WriteLine(
                    "Begin Diag Channel  From Channel : "
                        + PhoneCommandAPI.SP_BeginPhoneTest(hDiagPhone, ref openArgument)
                        + " USB Port COM"
                        + PortCom
                );
                Thread.Sleep(500);
            }

            public static void DiagClose()
            {
                Thread.Sleep(500);
                PhoneCommandAPI.SP_ReleasePhone(hDiagPhone);
                Thread.Sleep(1000);
            }

            public static void WriteDiag(byte[] lpvalue)
            {
                Thread.Sleep(15);
                PhoneCommandAPI.SP_Write(hDiagPhone, lpvalue, (ulong)lpvalue.Length);
            }
        }
    }
}
