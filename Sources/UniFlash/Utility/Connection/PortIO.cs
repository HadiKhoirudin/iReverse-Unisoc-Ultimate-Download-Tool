using System;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace iReverse_Unisoc_Ultimate
{
    namespace Utility.Connection
    {
        public class PortIO
        {
            public static SerialPort serialPort;
            public static string PortCOMDiag = string.Empty;
            public static byte[] resp = new byte[1];
            public static byte[] DataReadFlash = new byte[1];

            public static void PortOpen(string PortCOMDiag)
            {
                serialPort = new SerialPort("COM" + PortCOMDiag);
                serialPort.BaudRate = 115200;
                serialPort.Parity = Parity.None;
                serialPort.Handshake = Handshake.None;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                serialPort.Open();
            }

            public static void PortClose()
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    serialPort.Dispose();
                }
            }

            public static byte[] PortRead()
            {
                if (!serialPort.IsOpen)
                {
                    return new byte[1];
                }
                int numBytes = serialPort.BytesToRead;
                byte[] buffer = new byte[numBytes];
                serialPort.BaseStream.ReadAsync(buffer, 0, numBytes);
                return buffer;
            }

            private static void raiseAppSerialDataEvent(byte[] received)
            {
                throw new NotImplementedException();
            }

            public static void PortWrite(byte[] request)
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Write(request, 0, request.Length);
                    Thread.Sleep(15);
                }
            }

            public static byte[] MergeBytes(string First, byte[] Data, string Last)
            {
                byte[] b1 = StringToByteArray(First);
                byte[] b2 = Data;
                byte[] b3 = StringToByteArray(Last);
                var s = new MemoryStream();
                s.Write(b1, 0, b1.Length);
                s.Write(b2, 0, b2.Length);
                s.Write(b3, 0, b3.Length);
                var b4 = s.ToArray();
                return b4;
            }

            public static byte[] StringToByteArray(string hex)
            {
                hex = hex.Replace(" ", string.Empty).Replace("-", string.Empty);

                byte[] bytes = new byte[hex.Length / 2];

                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                }

                return bytes;
            }

            public static byte[] TakeByte(byte[] source, int start, int length)
            {
                return ((from element in source select element).Skip(start).Take(length)).ToArray();
            }

            public static string HexStringToAscii(string hex)
            {
                hex = hex.Replace(" ", string.Empty);
                byte[] bytes = new byte[(hex.Length / 2)];
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                }
                string asciiString = Encoding.ASCII.GetString(bytes);
                return asciiString;
            }

            public static string ReverseBytes(string value)
            {
                string reversed = string.Empty;
                for (int i = value.Length - 2; i >= 0; i -= 2)
                {
                    reversed += value.Substring(i, 2);
                }
                return reversed;
            }
        }
    }
}
