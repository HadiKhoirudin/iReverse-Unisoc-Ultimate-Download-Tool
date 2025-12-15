using iReverse_Unisoc_Ultimate.MyUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace iReverse_Unisoc_Ultimate.My.Boot
{

    public class boot
    {

        #region structure

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct bootheader
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] magic;
            public int kernel_size;
            public uint kernel_addr;
            public int ramdisk_size;
            public uint ramdisk_addr;
            public uint second_size;
            public uint second_addr;
            public uint tags_addr;
            public int page_size;
            public uint dt_size;
            public int os_version;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] name;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] cmdline;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] id4x8;
        }
        #endregion

        public static Dictionary<string, List<string>> build = new Dictionary<string, List<string>>
        {
            { "Brand ", new List<string> { "ro.product.brand", "ro.product.product.brand", "ro.product.odm.brand", "ro.product.vendor.brand", "ro.product.system_ext.brand", "ro.dolby.brand", "ro.product.bootimage.brand" } },
            { "OEM ", new List<string> { "ro.product.manufacturer", "ro.product.vendor.manufacturer", "ro.product.odm.manufacturer", "ro.product.product.manufacturer", "ro.product.system_ext.manufacturer", "ro.product.system.manufacturer", "ro.dolby.manufacturer", "ro.product.bootimage.manufacturer" } },
            { "Model ", new List<string> { "ro.product.model", "ro.product.product.model", "ro.product.system_ext.model", "ro.product.system.model", "ro.product.vendor.model", "ro.product.odm.model", "ro.vivo.product.release.model", "ro.product.bootimage.model" } },
            { "Name ", new List<string> { "ro.product.product.name", "ro.product.system_ext.name", "ro.product.system.name", "ro.product.name", "ro.vivo.product.release.name", "ro.product.bootimage.tran.device.name.default", "ro.product.bootimage.name" } },
            { "Product ", new List<string> { "ro.product.device", "ro.product.product.device", "ro.product.system_ext.device", "ro.product.system.device", "ro.product.mod_device", "ro.miui.cust_device", "ro.product.vendor.device", "ro.product.board", "ro.product.bootimage.device" } },
            { "Market name ", new List<string> { "ro.oppo.market.name", "ro.product.product.marketname", "ro.product.system_ext.marketname", "ro.product.system.marketname" } },
            { "SDK ver ", new List<string> { "ro.build.version.sdk", "ro.vendor.build.version.sdk", "ro.product.build.version.sdk", "ro.bootimage.build.version.sdk" } },
            { "Code name ", new List<string> { "ro.build.version.codename", "ro.system.build.version.release_or_codename" } },
            { "Incremental ", new List<string> { "ro.system.build.version.incremental", "ro.build.version.incremental", "ro.vendor.build.version.incremental" } },
            { "OTA ver ", new List<string> { "ro.build.version.ota" } },
            { "Build ID ", new List<string> { "ro.product.build.id", "ro.build.id", "ro.bootimage.build.id" } },
            { "OS ver ", new List<string> { "ro.vivo.os.build.display.id" } },
            { "Android ver ", new List<string> { "ro.odm.build.version.release", "ro.product.build.version.release", "ro.build.version.release", "ro.vivo.os.version", "ro.bootimage.build.version.release", "ro.bootimage.build.version.release_or_codename" } },
            { "MIUI ver ", new List<string> { "ro.miui.ui.version.code", "ro.miui.ui.version.name" } },
            { "Security patch ", new List<string> { "ro.build.version.security_patch", "ro.vendor.build.security_patch", "ro.bootimage.build.security_patch" } },
            { "Region ", new List<string> { "ro.miui.build.region", "persist.sys.oppo.region" } },
            { "Timezone ", new List<string> { "persist.sys.timezone" } },
            { "Platform ", new List<string> { "ro.vivo.product.platform", "ro.vendor.mediatek.platform", "ro.board.platform", "ro.mediatek.platform", "ro.vivo.product.platform" } },
            { "Kernel ID ", new List<string> { "ro.build.kernel.id" } },
            { "CPU ABI ", new List<string> { "ro.vendor.product.cpu.abilist", "ro.product.cpu.abi" } },
            { "SW ver ", new List<string> { "ro.vendor.build.software.version", "ro.build.software.version" } },
            { "Build Date ", new List<string> { "ro.product.build.date", "ro.system.build.date", "ro.build.date", "ro.bootimage.build.date", "ro.bootimage.build.date" } },
            { "Fingerprint ", new List<string> { "ro.build.description", "ro.bootimage.build.fingerprint" } }
        };

        public static void extract(byte[] boot)
        {
            string check_boot = Encoding.UTF8.GetString(boot.Take(15).ToArray());

            if (check_boot.Contains("SPRD-SECUREFLAG"))
            {
                int start = find_binary(boot, StringToByteArray("41 4E 44 52 4F 49 44 21 60"), 0);
                boot = boot.Skip(start).Take(boot.Length - start).ToArray();
            }
            try
            {
                MemoryStream bootstream = new MemoryStream(boot);
                using (bootstream)
                {
                    byte[] buffer = new byte[8];
                    bootstream.Read(buffer, 0, 8);
                    string boot_magic = Encoding.ASCII.GetString(buffer);

                    if (boot_magic != "ANDROID!")
                    {
                        MyDisplay.RichLogs("Cant read device information.", Color.Red, true, true);
                        return;
                    }

                    BinaryReader reader = new BinaryReader(new MemoryStream(boot));
                    bootheader header = unpack_reader<bootheader>(reader);
                    if (header.page_size <= 0)
                    {
                        header.page_size = 4096;
                        header.ramdisk_size = (int)header.kernel_addr;
                    }

                    uint baseAddr = (uint)(header.kernel_addr - 0x8000);

                    bootstream.Seek(header.page_size - Marshal.SizeOf(typeof(bootheader)), SeekOrigin.Current);
                    long size = 0;
                    while (true)
                    {
                        byte[] pageBuffer = reader.ReadBytes(header.page_size);
                        bootstream.Read(pageBuffer, 0, pageBuffer.Length);
                        if (pageBuffer.SequenceEqual(new byte[header.page_size]))
                        {
                            continue;
                        }
                        reader.BaseStream.Seek(-header.page_size, SeekOrigin.Current);
                        size = bootstream.Position;
                        break;
                    }

                    long num_header_pages = 1;
                    var num_kernel_pages = get_number_of_pages(header.kernel_size, header.page_size);
                    var ramdisk_offset = header.page_size * (num_header_pages + num_kernel_pages);

                    bootstream.Seek(ramdisk_offset, SeekOrigin.Begin);
                    byte[] ramdisk = new byte[header.ramdisk_size];
                    bootstream.Read(ramdisk, 0, header.ramdisk_size);
                    var tsmigic = ramdisk.Take(3).ToArray();
                    var listsprop = new List<string>();
                    if (ramdisk[0] != 0x1F && ramdisk[1] != 0x8B && ramdisk[2] != 0x8)
                    {
                        byte[] dst = new byte[50000000];
                        var block = Lz4Decompressor.lz4_hadikit_decompress(src: ramdisk, dst: ref dst);
                        
                        // Fix info not readed ... #16-12-2025
                        //if (block > 0)
                        //{
                            var cpio = new cpio_stream(dst);
                            listsprop = cpio.parsecpio();
                        //}
                    }
                    else
                    {
                        using (GZipStream inflater = new GZipStream(new MemoryStream(ramdisk), CompressionMode.Decompress))
                        {
                            int decompressed_bytes_read = 0;
                            var decompressed_buffer = new byte[ramdisk.Length];
                            using (var stream = new MemoryStream())
                            {
                                while (true)
                                {
                                    decompressed_bytes_read = inflater.Read(decompressed_buffer, 0, decompressed_buffer.Length);
                                    if (decompressed_bytes_read <= 0)
                                    {
                                        break;
                                    }
                                    stream.Write(decompressed_buffer, 0, decompressed_bytes_read);
                                }
                                var cpio = new cpio_stream(stream.ToArray());
                                listsprop = cpio.parsecpio();
                            }
                        }
                    }
                    if (listsprop.Count > 0)
                    {
                        MyDisplay.RichLogs(" ", Color.Black, true, true);

                        MyDisplay.RichLogs(
                            "Read Android Info! Please Wait...",
                            Color.Black,
                            false,
                            true
                        );

                        foreach (var kvp in build)
                        {
                            string mainCmd = kvp.Key;
                            foreach (string command in kvp.Value)
                            {
                                var info = infolist(listsprop, command);
                                if (!string.IsNullOrEmpty(info))
                                {
                                    MyDisplay.RichLogs($" {mainCmd}: ", Color.Black, false, false);
                                    MyDisplay.RichLogs(info, Color.MediumSlateBlue, true, true);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDisplay.RichLogs("Recovery / Boot IMG file not supported!", Color.Orange, true, true);
                Console.WriteLine(ex.Message);
            }
        }

        public static long get_number_of_pages(long imageSize, long pageSize)
        {
            return (imageSize + pageSize - 1) / pageSize;
        }

        public static T unpack_reader<T>(BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(Marshal.SizeOf(typeof(T)));
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T theStructure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return theStructure;
        }

        public static string infolist(List<string> allinfolist, string search)
        {
            foreach (string infos in allinfolist)
            {
                if (infos.Contains("=") && infos.StartsWith(search))
                {
                    return infos.Split('=')[1];
                }
            }
            return null;
        }

        public static int find_binary(byte[] array, object sequence, int start = 0)
        {
            byte[] bytef = new byte[] { };
            try
            {
                if (sequence is string)
                    bytef = StringToByteArray((string)sequence);
                else if (sequence is byte[])
                    bytef = (byte[])sequence;

                if (bytef.Length <= 0)
                    return -1;

                int end = array.Length - bytef.Length;
                byte firstByte = bytef[0];

                while (start <= end)
                {
                    if (array[start] == firstByte)
                    {
                        for (int offset = 1; ; ++offset)
                        {
                            if (offset == bytef.Length)
                            {
                                return start;
                            }
                            else if (array[start + offset] != bytef[offset])
                            {
                                break;
                            }
                        }
                    }
                    ++start;
                }
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nfindSequence report : {ex.Message}\nfindSequence data : {BitConverter.ToString(bytef).Replace("-", " ")}\n");
                return -1;
            }
        }

        public static byte[] StringToByteArray(string hex)
        {
            string val = hex.Replace(" ", string.Empty).Replace("-", string.Empty).ToLower().Replace("0x", string.Empty);

            if (val.Length % 2 > 0)
            {
                string su = "0" + val.Substring(val.Length - 1);
                val = val.Substring(0, val.Length - 1) + su;
            }

            return Enumerable
                .Range(0, val.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(val.Substring(x, 2), 16))
                .ToArray();
        }
    }
}

