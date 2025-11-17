using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace iReverse_Unisoc_Ultimate.My.Boot
{
    public class cpio_stream
    {
        private MemoryStream cpioStream;

        public cpio_stream(byte[] cpio)
        {
            cpioStream = new MemoryStream(cpio);
        }

        private const int S_IFLNK = 0xA000;
        private const int S_IFREG = 0x8000;
        private const int S_IFMT = 0xF000;

        public List<string> parsecpio()
        {
            MemoryStream cpio = cpioStream;
            long totalsize = 0;
            List<string> listsprop = new List<string>();
            Func<int, int> padding = (x) => (-x + 1 - 1) & 3;
            while (cpio.Length > totalsize)
            {
                try
                {
                    var header = read_cpio_header(cpio);
                    string name = header.Item1;
                    int mode = header.Item2;
                    int filesize = header.Item3;
                    totalsize += filesize;
                    if (name != "invalid" && (name.Contains("build.prop") || name.Contains("prop.default") || name.Contains("default.prop")))
                    {
                        byte[] buffer = new byte[filesize];
                        cpio.Read(buffer, 0, buffer.Length);
                        listsprop.AddRange(Encoding.UTF8.GetString(buffer).Split("\n"[0]));
                        cpio.Seek(padding(filesize), SeekOrigin.Current);
                    }
                    if (name == "TRAILER!!!")
                    {
                        break;
                    }
                    if ((mode & S_IFMT) == S_IFLNK)
                    {
                        byte[] location = new byte[filesize];
                        cpio.Read(location, 0, filesize);
                        cpio.Seek(padding(filesize), SeekOrigin.Current);
                    }
                    else if ((mode & S_IFMT) == S_IFREG)
                    {
                        byte[] data = new byte[filesize];
                        cpio.Read(data, 0, filesize);
                        cpio.Seek(padding(filesize), SeekOrigin.Current);
                    }
                    else
                    {
                        cpio.Seek(filesize, SeekOrigin.Current);
                        cpio.Seek(padding(filesize), SeekOrigin.Current);
                    }
                }
                catch
                {
                    return new List<string>();
                }
            }
            return listsprop;
        }

        public Tuple<string, int, int> read_cpio_header(Stream cpio)
        {
            Func<int, int> padding = (x) => (-x + 1 - 1) & 3;
            byte[] header = new byte[6];
            cpio.Read(header, 0, 6);
            string magic = Encoding.ASCII.GetString(header);
            if (magic != "070701")
            {
                return Tuple.Create("invalid", 0, header.Length);
            }
            cpio.Seek(8, SeekOrigin.Current);
            header = new byte[8];
            cpio.Read(header, 0, 8);
            int mode = Convert.ToInt32(Encoding.ASCII.GetString(header), 16);
            cpio.Seek(32, SeekOrigin.Current);
            cpio.Read(header, 0, 8);
            int filesize = Convert.ToInt32(Encoding.ASCII.GetString(header), 16);
            cpio.Seek(32, SeekOrigin.Current);
            cpio.Read(header, 0, 8);
            int namesize = Convert.ToInt32(Encoding.ASCII.GetString(header), 16);
            cpio.Seek(8, SeekOrigin.Current);
            header = new byte[namesize - 1];
            cpio.Read(header, 0, namesize - 1);
            string name = Encoding.UTF8.GetString(header);
            cpio.Seek(1, SeekOrigin.Current);
            cpio.Seek(padding(namesize + 110), SeekOrigin.Current);
            return Tuple.Create(name, mode, filesize);
        }
    }
}
