using iReverse_Unisoc_Ultimate.MyUI;
using iReverse_Unisoc_Ultimate.UniFlash.Worker;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace iReverse_Unisoc_Ultimate
{
    namespace UniFlash
    {
        public class PACExtractor
        {
            #region PAC
            private static string SwVersion = null;
            private static bool isNeedFixOffsets = false;
            private static bool isContainSuper = false;
            private static bool isShowszPartitionInfo = true;
            private static ulong CurrentFileSizes;
            private static long FwSizes;
            public static string pacfile = null;
            public static string outputDir = null;
            public static bool debug = false;
            private static int partitionCount = 0;
            private static int currentCount = 0;
            public static Dictionary<string, object> PAC_HEADER = new Dictionary<string, object>()
            {
                { "szVersion", string.Empty },
                { "dwHiSize", 0 },
                { "dwLoSize", 0 },
                { "productName", string.Empty },
                { "firmwareName", string.Empty },
                { "partitionCount", 0 },
                { "partitionsListStart", 0 },
                { "dwMode", 0 },
                { "dwFlashType", 0 },
                { "dwNandStrategy", 0 },
                { "dwIsNvBackup", 0 },
                { "dwNandPageType", 0 },
                { "szPrdAlias", string.Empty },
                { "dwOmaDmProductFlag", 0 },
                { "dwIsOmaDM", 0 },
                { "dwIsPreload", 0 },
                { "dwReserved", 0 },
                { "dwMagic", 0 },
                { "wCRC1", 0 },
                { "wCRC2", 0 }
            };

            public static Dictionary<string, object> FILE_HEADER = new Dictionary<string, object>()
            {
                { "length", 0 },
                { "partitionName", string.Empty },
                { "fileName", string.Empty },
                { "szFileName", string.Empty },
                { "hiPartitionSize", 0 },
                { "hiDataOffset", 0 },
                { "loPartitionSize", 0 },
                { "nFileFlag", 0 },
                { "nCheckFlag", 0 },
                { "loDataOffset", 0 },
                { "dwCanOmitFlag", 0 },
                { "dwAddrNum", 0 },
                { "dwAddr", 0 },
                { "dwReserved", 0 }
            };

            public static void Abort(string msg)
            {
                MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Main.SharedUI.TxtPacFirmware.Invoke((Action)(() => Main.SharedUI.TxtPacFirmware.Text = string.Empty));
                WorkerDownload.UniFirmware = string.Empty;
                Console.WriteLine(msg);
                return;
            }

            public static string GetString(byte[] bytes)
            {
                return Encoding.Unicode.GetString(bytes).TrimEnd('\0');
            }

            public static void PrintP(string name, object value)
            {
                Console.WriteLine($"{name,-20} = {value}");
            }

            public static void PrintPacHeader(Dictionary<string, object> pacHeader)
            {
                MyDisplay.RichLogs("Firmware Name 	 : ", Color.Black, true, false);
                MyDisplay.RichLogs($"{pacHeader["firmwareName"]}", Color.Black, true, true);

                MyDisplay.RichLogs("Firmware Product : ", Color.Black, true, false);
                MyDisplay.RichLogs($"{pacHeader["productName"]}", Color.Black, true, true);

                MyDisplay.RichLogs("Firmware Version : ", Color.Black, true, false);
                MyDisplay.RichLogs($"{pacHeader["szVersion"]}", Color.Black, true, true);

                PrintP("Version", pacHeader["szVersion"]);

                ulong dwHiSize = Convert.ToUInt64(pacHeader["dwHiSize"]);
                ulong dwLoSize = Convert.ToUInt64(pacHeader["dwLoSize"]);
                ulong dwSize = Convert.ToUInt64((decimal)(dwHiSize << 32) + dwLoSize);

                MyDisplay.RichLogs("Firmware Size 	 : ", Color.Black, true, false);

                if (Convert.ToUInt64(pacHeader["dwHiSize"]) == 0x0)
                    MyDisplay.RichLogs($"{MyDisplay.GetFileSizes(Convert.ToInt64(pacHeader["dwLoSize"]))}", Color.Black, true, false);
                else
                    MyDisplay.RichLogs($"{MyDisplay.GetFileSizes(Convert.ToInt64(dwSize))}", Color.Black, true, false);

                PrintP("PrdName", pacHeader["productName"]);
                PrintP("FirmwareName", pacHeader["firmwareName"]);
                PrintP("FileCount", pacHeader["partitionCount"]);
                PrintP("FileOffset", pacHeader["partitionsListStart"]);
                PrintP("Mode", pacHeader["dwMode"]);
                PrintP("FlashType", pacHeader["dwFlashType"]);
                PrintP("NandStrategy", pacHeader["dwNandStrategy"]);
                PrintP("IsNvBackup", pacHeader["dwIsNvBackup"]);
                PrintP("NandPageType", pacHeader["dwNandPageType"]);
                PrintP("PrdAlias", pacHeader["szPrdAlias"]);
                PrintP("OmaDmPrdFlag", pacHeader["dwOmaDmProductFlag"]);
                PrintP("IsOmaDM", pacHeader["dwIsOmaDM"]);
                PrintP("IsPreload", pacHeader["dwIsPreload"]);
                PrintP("Magic", "0x" + (Convert.ToUInt64(pacHeader["dwMagic"])).ToString("X").ToUpper());
                PrintP("CRC1", "0x" + (Convert.ToUInt64(pacHeader["wCRC1"])).ToString("X").ToUpper());
                PrintP("CRC2", "0x" + (Convert.ToUInt64(pacHeader["wCRC2"])).ToString("X").ToUpper());

                Console.WriteLine();
            }

            public static Dictionary<string, object> ParsePacHeader(BinaryReader reader, string pacfile, bool debug)
            {
                Dictionary<string, object> pacHeader = new Dictionary<string, object>(PAC_HEADER);

                byte[] pacHeaderBytes = reader.ReadBytes(Marshal.SizeOf(typeof(PacHeaderStruct)));
                GCHandle pacHeaderHandle = GCHandle.Alloc(pacHeaderBytes, GCHandleType.Pinned);
                PacHeaderStruct pacHeaderStruct = (PacHeaderStruct)Marshal.PtrToStructure(pacHeaderHandle.AddrOfPinnedObject(), typeof(PacHeaderStruct));
                pacHeaderHandle.Free();

                pacHeader["szVersion"] = GetString(pacHeaderStruct.szVersion);
                pacHeader["dwHiSize"] = pacHeaderStruct.dwHiSize;
                pacHeader["dwLoSize"] = pacHeaderStruct.dwLoSize;
                pacHeader["productName"] = GetString(pacHeaderStruct.productName);
                pacHeader["firmwareName"] = GetString(pacHeaderStruct.firmwareName);
                pacHeader["partitionCount"] = pacHeaderStruct.partitionCount;
                pacHeader["partitionsListStart"] = pacHeaderStruct.partitionsListStart;
                pacHeader["dwMode"] = pacHeaderStruct.dwMode;
                pacHeader["dwFlashType"] = pacHeaderStruct.dwFlashType;
                pacHeader["dwNandStrategy"] = pacHeaderStruct.dwNandStrategy;
                pacHeader["dwIsNvBackup"] = pacHeaderStruct.dwIsNvBackup;
                pacHeader["dwNandPageType"] = pacHeaderStruct.dwNandPageType;
                pacHeader["szPrdAlias"] = GetString(pacHeaderStruct.szPrdAlias);
                pacHeader["dwOmaDmProductFlag"] = pacHeaderStruct.dwOmaDmProductFlag;
                pacHeader["dwIsOmaDM"] = pacHeaderStruct.dwIsOmaDM;
                pacHeader["dwIsPreload"] = pacHeaderStruct.dwIsPreload;
                pacHeader["dwReserved"] = pacHeaderStruct.dwReserved;
                pacHeader["dwMagic"] = pacHeaderStruct.dwMagic;
                pacHeader["wCRC1"] = pacHeaderStruct.wCRC1;
                pacHeader["wCRC2"] = pacHeaderStruct.wCRC2;

                if (debug)
                {
                    MyDisplay.RichLogs("Firmware Information", Color.Black, true, true);
                    MyDisplay.RichLogs("=====================================================================================================", Color.Black, true, true);

                    PrintPacHeader(pacHeader);
                }

                SwVersion = (string)pacHeader["szVersion"];

                if ((string)pacHeader["szVersion"] != "BP_R1.0.0" && (string)pacHeader["szVersion"] != "BP_R2.0.1")
                {
                    Abort("Unsupported PAC version");
                }

                ulong dwHiSize = Convert.ToUInt64(pacHeader["dwHiSize"]);
                ulong dwLoSize = Convert.ToUInt64(pacHeader["dwLoSize"]);

                ulong dwSize = Convert.ToUInt64((decimal)(dwHiSize << 32) + dwLoSize);
                FileInfo fileInfo = new FileInfo(pacfile);
                FwSizes = fileInfo.Length;

                if (dwSize != (ulong)FwSizes)
                {
                    Abort("Bin packet's size is not correct");
                }

                return pacHeader;
            }

            public static void PrintFileHeader(Dictionary<string, object> fileHeader)
            {
                PrintP("HeaderSize", fileHeader["length"]);
                PrintP("FileID", fileHeader["partitionName"]);
                PrintP("FileName", fileHeader["fileName"]);

                if (Convert.ToUInt64(fileHeader["hiPartitionSize"]) == 0x0 || Convert.ToUInt64(fileHeader["hiPartitionSize"]) == 0x1)
                {
                    PrintP("FileSize [0]", fileHeader["loPartitionSize"]);
                    Console.WriteLine("FileSize Hex         = " + Convert.ToUInt64(fileHeader["loPartitionSize"]).ToString("X").ToUpper());

                    if (CurrentFileSizes == 0)
                        CurrentFileSizes = Convert.ToUInt64(fileHeader["loPartitionSize"]);
                    else
                        CurrentFileSizes += Convert.ToUInt64(fileHeader["loPartitionSize"]);

                    if (CurrentFileSizes > uni.str_to_size("3G"))
                        isNeedFixOffsets = true;
                }
                else
                {
                    Console.WriteLine("FileSize Hex         = " + Convert.ToUInt64(fileHeader["loPartitionSize"]).ToString("X").ToUpper());
                    PrintP("FileSize [1]", Convert.ToUInt64(fileHeader["hiPartitionSize"]) + Convert.ToUInt64(fileHeader["loPartitionSize"]));

                    if (CurrentFileSizes == 0)
                        CurrentFileSizes = Convert.ToUInt64(fileHeader["hiPartitionSize"]);
                    else
                        CurrentFileSizes += Convert.ToUInt64(fileHeader["hiPartitionSize"]);

                    if (CurrentFileSizes > uni.str_to_size("3G"))
                        isNeedFixOffsets = true;
                }

                PrintP("FileFlag", fileHeader["nFileFlag"]);
                PrintP("CheckFlag", fileHeader["nCheckFlag"]);

                if (Convert.ToUInt64(fileHeader["hiDataOffset"]) == 0x0)
                {
                    PrintP("DataOffset", fileHeader["loDataOffset"]);

                    PrintP("DataOffset Hex", "0x" + Convert.ToUInt64(fileHeader["loDataOffset"]).ToString("X").ToUpper());
                    PrintP("DataOffset Size", MyDisplay.GetFileSizes(Convert.ToInt64(fileHeader["loDataOffset"])));
                }
                else
                {
                    ulong hiDataOffset = Convert.ToUInt64(fileHeader["hiDataOffset"]);
                    ulong loDataOffset = Convert.ToUInt64(fileHeader["loDataOffset"]);
                    ulong DataOffset = Convert.ToUInt64(hiDataOffset + loDataOffset);
                    PrintP("DataOffset", Convert.ToUInt64(DataOffset));
                    PrintP("DataOffset Hex", "0x" + Convert.ToUInt64(DataOffset).ToString("X").ToUpper());
                    PrintP("DataOffset Size", MyDisplay.GetFileSizes(Convert.ToInt64(DataOffset)));

                }
                PrintP("CanOmitFlag", fileHeader["dwCanOmitFlag"]);
                PrintP("Current Offset", CurrentFileSizes);
                PrintP("Current Offset Size", MyDisplay.GetFileSizes(Convert.ToInt64(CurrentFileSizes)));
                Console.WriteLine();
            }

            public static void ParseFiles(BinaryReader reader, List<Dictionary<string, object>> fileHeaders, bool debug)
            {
                Dictionary<string, object> fileHeader = new Dictionary<string, object>(FILE_HEADER);
                byte[] fileHeaderBytes = null;
                GCHandle fileHeaderHandle = new GCHandle();

                if (SwVersion == "BP_R1.0.0")
                {
                    fileHeaderBytes = reader.ReadBytes(Marshal.SizeOf(typeof(FileHeaderStruct_BP_R1)));
                    fileHeaderHandle = GCHandle.Alloc(fileHeaderBytes, GCHandleType.Pinned);
                    var fileHeaderStruct = (FileHeaderStruct_BP_R1)
                        Marshal.PtrToStructure(
                            fileHeaderHandle.AddrOfPinnedObject(),
                            typeof(FileHeaderStruct_BP_R1)
                        );
                    fileHeaderHandle.Free();

                    ushort nFileFlag = 0;

                    if (fileHeaderStruct.nFileFlag > 0)
                    {
                        nFileFlag = 1;
                    }

                    ushort nCheckFlag = 0;

                    if (fileHeaderStruct.nCheckFlag > 0)
                    {
                        nCheckFlag = 1;
                    }

                    ulong hiDataOffset = fileHeaderStruct.hiDataOffset;
                    ulong loDataOffset = fileHeaderStruct.loDataOffset;
                    ulong hiPartitionSize = fileHeaderStruct.hiPartitionSize;
                    ulong loPartitionSize = fileHeaderStruct.loPartitionSize;

                    fileHeader["length"] = fileHeaderStruct.length;
                    fileHeader["partitionName"] = GetString(fileHeaderStruct.partitionName);
                    fileHeader["fileName"] = GetString(fileHeaderStruct.fileName);
                    fileHeader["szFileName"] = GetString(fileHeaderStruct.szFileName);
                    fileHeader["hiPartitionSize"] = hiPartitionSize;
                    fileHeader["hiDataOffset"] = hiDataOffset;
                    fileHeader["loPartitionSize"] = loPartitionSize;
                    fileHeader["nFileFlag"] = nFileFlag;
                    fileHeader["nCheckFlag"] = nCheckFlag;
                    fileHeader["loDataOffset"] = loDataOffset;
                    fileHeader["dwCanOmitFlag"] = fileHeaderStruct.dwCanOmitFlag;
                    fileHeader["dwAddrNum"] = fileHeaderStruct.dwAddrNum;
                    fileHeader["dwAddr"] = fileHeaderStruct.dwAddr;

                    if (GetString(fileHeaderStruct.fileName).Contains("super") || GetString(fileHeaderStruct.partitionName).Contains("super")) isContainSuper = true;

                    if (Convert.ToInt32(fileHeader["length"]) != Marshal.SizeOf(typeof(FileHeaderStruct_BP_R1))) Console.WriteLine("Unknown Partition Header format found");

                    PrintFileHeader(fileHeader);

                    fileHeaders.Add(fileHeader);
                }
                else if (SwVersion == "BP_R2.0.1")
                {
                    fileHeaderBytes = reader.ReadBytes(Marshal.SizeOf(typeof(FileHeaderStruct_BP_R2)));
                    fileHeaderHandle = GCHandle.Alloc(fileHeaderBytes, GCHandleType.Pinned);
                    var fileHeaderStruct = (FileHeaderStruct_BP_R2)
                        Marshal.PtrToStructure(
                            fileHeaderHandle.AddrOfPinnedObject(),
                            typeof(FileHeaderStruct_BP_R2)
                        );
                    fileHeaderHandle.Free();

                    ushort nFileFlag = 0;

                    if (fileHeaderStruct.nFileFlag > 0)
                    {
                        nFileFlag = 1;
                    }

                    ushort nCheckFlag = 0;

                    if (fileHeaderStruct.nCheckFlag > 0)
                    {
                        nCheckFlag = 1;
                    }

                    byte[] A = uni.parse_reverse(fileHeaderStruct.szPartitionInfo.Take(4).ToArray());
                    byte[] B = uni.parse_reverse(fileHeaderStruct.szPartitionInfo.Skip(4).Take(4).ToArray());
                    byte[] C = uni.parse_reverse(fileHeaderStruct.szPartitionInfo.Skip(8).Take(4).ToArray());
                    byte[] D = uni.parse_reverse(fileHeaderStruct.szPartitionInfo.Skip(12).Take(4).ToArray());
                    byte[] E = uni.parse_reverse(fileHeaderStruct.szPartitionInfo.Skip(16).Take(4).ToArray());
                    byte[] F = uni.parse_reverse(fileHeaderStruct.szPartitionInfo.Skip(20).Take(4).ToArray());

                    string fixoffset = Convert.ToUInt64(BitConverter.ToString(C).Replace("-", ""), 16).ToString();

                    if (fixoffset.Length == 1)
                        fixoffset = "0" + fixoffset;

                    if ((ulong)FwSizes < uni.str_to_size("8G") && fixoffset == "02")
                        fixoffset = "01";

                    if (isShowszPartitionInfo)
                    {
                        PrintP("Partition Info Hex", BitConverter.ToString(fileHeaderStruct.szPartitionInfo).Replace("-", " "));
                        PrintP("Partition Info Hex A", BitConverter.ToString(A).Replace("-", " "));
                        PrintP("Partition Info Hex B", BitConverter.ToString(B).Replace("-", " "));
                        PrintP("Partition Info Hex C", BitConverter.ToString(C).Replace("-", " "));
                        PrintP("Partition Info Hex D", BitConverter.ToString(D).Replace("-", " "));
                        PrintP("Partition Info Hex E", BitConverter.ToString(E).Replace("-", " "));
                        PrintP("Partition Info Hex F", BitConverter.ToString(F).Replace("-", " "));
                    }


                    ulong hiPartitionSize = Convert.ToUInt64(BitConverter.ToString(A).Replace("-", ""), 16);
                    ulong loPartitionSize = Convert.ToUInt64(BitConverter.ToString(B).Replace("-", ""), 16);
                    ulong hiDataOffset = Convert.ToUInt64(BitConverter.ToString(E).Replace("-", ""), 16);
                    ulong loDataOffset = Convert.ToUInt64(BitConverter.ToString(F).Replace("-", ""), 16);

                    if (isNeedFixOffsets)
                    {
                        if (isShowszPartitionInfo)
                            PrintP("Partition Fix Offset", fixoffset);

                        if (hiDataOffset > 2)
                        {
                            loDataOffset = 0;
                            hiDataOffset = Convert.ToUInt64(fixoffset + Convert.ToUInt64(hiDataOffset).ToString("X8").ToUpper(), 16);
                        }
                        else if (loDataOffset > 2)
                        {
                            hiDataOffset = 0;
                            loDataOffset = Convert.ToUInt64(fixoffset + Convert.ToUInt64(loDataOffset).ToString("X8").ToUpper(), 16);
                        }
                    }

                    if (GetString(fileHeaderStruct.partitionName).ToLower().Contains("super") || GetString(fileHeaderStruct.partitionName).ToLower().Contains(".xml"))
                    {
                        if (isShowszPartitionInfo)
                            PrintP("Partition Fix Offset", fixoffset);

                        if (loPartitionSize > 2 && loPartitionSize < (ulong)FwSizes)
                        {
                            hiPartitionSize = 0;
                            loPartitionSize = Convert.ToUInt64(fixoffset + Convert.ToUInt64(loPartitionSize).ToString("X8").ToUpper(), 16);
                        }
                        else if (hiPartitionSize > 2 && hiPartitionSize < (ulong)FwSizes)
                        {
                            loPartitionSize = 0;
                            hiPartitionSize = Convert.ToUInt64(fixoffset + Convert.ToUInt64(hiPartitionSize).ToString("X8").ToUpper(), 16);
                        }
                    }

                    fileHeader["length"] = fileHeaderStruct.length;
                    fileHeader["partitionName"] = GetString(fileHeaderStruct.partitionName);
                    fileHeader["fileName"] = GetString(fileHeaderStruct.fileName);
                    fileHeader["szFileName"] = GetString(fileHeaderStruct.szFileName);
                    fileHeader["hiPartitionSize"] = hiPartitionSize;
                    fileHeader["hiDataOffset"] = hiDataOffset;
                    fileHeader["loPartitionSize"] = loPartitionSize;
                    fileHeader["nFileFlag"] = nFileFlag;
                    fileHeader["nCheckFlag"] = nCheckFlag;
                    fileHeader["loDataOffset"] = loDataOffset;
                    fileHeader["dwCanOmitFlag"] = fileHeaderStruct.dwCanOmitFlag;
                    fileHeader["dwAddrNum"] = fileHeaderStruct.dwAddrNum;
                    fileHeader["dwAddr"] = fileHeaderStruct.dwAddr;

                    if (GetString(fileHeaderStruct.fileName).Contains("super") || GetString(fileHeaderStruct.partitionName).Contains("super")) isContainSuper = true;

                    if (Convert.ToInt32(fileHeader["length"]) != Marshal.SizeOf(typeof(FileHeaderStruct_BP_R2))) Console.WriteLine("Unknown Partition Header format found");

                    PrintFileHeader(fileHeader);

                    fileHeaders.Add(fileHeader);
                }
                else
                {
                    return;
                }
            }

            public static void UnpackPacFile(string pacfile, string outputDir, bool debug)
            {
                using (BinaryReader reader = new BinaryReader(File.Open(pacfile, FileMode.Open)))
                {
                    Dictionary<string, object> pacHeader = ParsePacHeader(reader, pacfile, debug);

                    partitionCount = Convert.ToInt32(pacHeader["partitionCount"]);
                    int partitionsListStart = Convert.ToInt32(pacHeader["partitionsListStart"]);

                    reader.BaseStream.Seek(partitionsListStart, SeekOrigin.Begin);

                    List<Dictionary<string, object>> fileHeaders = new List<Dictionary<string, object>>();

                    for (int i = 0; i < partitionCount; i++)
                    {
                        ParseFiles(reader, fileHeaders, debug);
                    }

                    foreach (Dictionary<string, object> fileHeader in fileHeaders)
                    {
                        string partitionName = (string)fileHeader["partitionName"];
                        string fileName = (string)fileHeader["fileName"];
                        ulong loDataOffset = Convert.ToUInt64(fileHeader["loDataOffset"]);
                        ulong hiDataOffset = Convert.ToUInt64(fileHeader["hiDataOffset"]);
                        ulong loPartitionSize = Convert.ToUInt64(fileHeader["loPartitionSize"]);
                        ulong hiPartitionSize = Convert.ToUInt64(fileHeader["hiPartitionSize"]);
                        string locations = Path.Combine(outputDir, fileName).Replace("\\\\", "\\");
                        bool skip = fileName.ToLower().Contains(".xml") || fileName.ToLower().Contains(".ini") || fileName.ToLower().Contains(".conf") || fileName.ToLower().Contains(".cfg") || partitionName.ToLower().Contains("fdl");

                        if (hiDataOffset + loDataOffset > 0 && hiPartitionSize + loPartitionSize > 0)
                        {
                            if (!skip)
                            {
                                if (Main.SharedUI.CkKeepNV.Checked)
                                {
                                    if (fileName.ToLower().Contains("nv") || partitionName.ToLower().Contains("nv") || fileName.ToLower().Contains("efs") || partitionName.ToLower().Contains("efs"))
                                    {
                                        Main.SharedUI.DataView.Invoke(
                                            (Action)(
                                                () =>
                                                    Main.SharedUI.DataView.Rows.Add(
                                                        false,
                                                        partitionName,
                                                        GetPartitionNames(partitionName),
                                                        hiDataOffset + loDataOffset,
                                                        hiPartitionSize + loPartitionSize,
                                                        string.Empty,
                                                        locations
                                                    )
                                            )
                                        );
                                    }
                                    else
                                    {
                                        Main.SharedUI.DataView.Invoke(
                                            (Action)(
                                                () =>
                                                    Main.SharedUI.DataView.Rows.Add(
                                                        true,
                                                        partitionName,
                                                        GetPartitionNames(partitionName),
                                                        hiDataOffset + loDataOffset,
                                                        hiPartitionSize + loPartitionSize,
                                                        string.Empty,
                                                        locations
                                                    )
                                            )
                                        );
                                    }
                                }
                                else
                                {
                                    Main.SharedUI.DataView.Invoke(
                                        (Action)(
                                            () =>
                                                Main.SharedUI.DataView.Rows.Add(
                                                    true,
                                                    partitionName,
                                                    GetPartitionNames(partitionName),
                                                    hiDataOffset + loDataOffset,
                                                    hiPartitionSize + loPartitionSize,
                                                    string.Empty,
                                                    locations
                                                )
                                        )
                                    );
                                }
                            }
                        }
                    }
                    ExtractFiles(reader, fileHeaders, outputDir);
                    MyProgress.ProcessBar1(100);
                    MyProgress.ProcessBar2(100);
                    reader.Close();
                    currentCount = 0;
                }
            }

            public static void ExtractFiles(BinaryReader reader, List<Dictionary<string, object>> fileHeaders, string outputDir)
            {
                try
                {
                    MyDisplay.RichLogs(
                        Environment.NewLine
                            + "=====================================================================================================",
                        Color.Black,
                        true,
                        true
                    );
                    MyProgress.ProcessBar2(0);
                    foreach (Dictionary<string, object> fileHeader in fileHeaders)
                    {
                        if (Main.SharedUI.UnisocWorker.CancellationPending)
                            return;

                        string fileName = (string)fileHeader["fileName"];
                        string FileID = (string)fileHeader["partitionName"];
                        ulong loDataOffset = Convert.ToUInt64(fileHeader["loDataOffset"]);
                        ulong hiDataOffset = Convert.ToUInt64(fileHeader["hiDataOffset"]);
                        ulong loPartitionSize = Convert.ToUInt64(fileHeader["loPartitionSize"]);
                        ulong hiPartitionSize = Convert.ToUInt64(fileHeader["hiPartitionSize"]);
                        string outputPath = Path.Combine(outputDir, fileName).Replace("\\\\", "\\");

                        ulong dataOffset = hiDataOffset > 2 ? hiDataOffset : loDataOffset;
                        ulong partitionSize = 0;

                        if (SwVersion == "BP_R2.0.1")
                        {
                            partitionSize = hiPartitionSize > 2 ? hiPartitionSize : loPartitionSize;
                        }
                        else
                        {
                            partitionSize = hiPartitionSize + loPartitionSize;
                        }

                        if (!string.IsNullOrEmpty(fileName))
                        {
                            Console.WriteLine(outputPath + " Data Offset : " + dataOffset + " Partition Size : " + partitionSize);
                            MyDisplay.RichLogs("Extract  File    : ...\\ImageFiles\\" + fileName, Color.Black, true, true);

                            if (File.Exists(outputPath))
                            {
                                File.Delete(outputPath);
                                FilesDoExtract(
                                    reader,
                                    dataOffset,
                                    fileName,
                                    partitionSize,
                                    outputDir
                                );
                            }
                            else
                            {
                                FilesDoExtract(
                                    reader,
                                    dataOffset,
                                    fileName,
                                    partitionSize,
                                    outputDir
                                );
                            }

                            if (fileName.Contains("xml") && Encoding.UTF8.GetString(File.ReadAllBytes(outputPath)).Contains("BMAConfig"))
                            {
                                WorkerDownload.UniFileXML = outputPath;
                                Main.SharedUI.CkRepartition.Invoke((Action)(() => Main.SharedUI.CkRepartition.Checked = true));
                                ScanXMLFile(Encoding.UTF8.GetString(File.ReadAllBytes(outputPath)));
                            }
                            if (FileID.ToLower() == "fdl")
                            {
                                uni.fdl1_location = outputPath;
                                Main.SharedUI.TxtFDL1.Invoke((Action)(() => Main.SharedUI.TxtFDL1.Text = fileName));
                            }
                            if (FileID.ToLower() == "fdl2")
                            {
                                uni.fdl2_location = outputPath;
                                Main.SharedUI.TxtFDL2.Invoke((Action)(() => Main.SharedUI.TxtFDL2.Text = fileName));
                            }
                        }
                        currentCount += 1;
                        MyProgress.ProcessBar2(currentCount, partitionCount - 1);
                    }
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            public static void FilesDoExtract(BinaryReader reader, ulong dataOffset, string fileName, ulong partitionSize, string OutputDir)
            {
                bool isSparse = false;
                bool skip = fileName.ToLower().Contains("userdata");

                using (FileStream fileStream = new FileStream(Path.Combine(OutputDir, fileName), FileMode.Append, FileAccess.Write))
                {
                    reader.BaseStream.Seek((long)dataOffset, SeekOrigin.Begin);

                    int Count = 1;
                    ulong remainingBytes = partitionSize;
                    int bufferSize = 4096;
                    byte[] buffer = new byte[bufferSize];
                    long writenBytes = 0;
                    MyProgress.ProcessBar1(0);
                    do
                    {
                        if (Main.SharedUI.UnisocWorker.CancellationPending) return;
                        if (remainingBytes <= 0) break;

                        int bytesRead = reader.Read(buffer, 0, (int)Math.Min(remainingBytes, (ulong)bufferSize));

                        if (!isSparse)
                        {
                            if (!skip && CekSparse(buffer.ToArray()))
                            {
                                isSparse = true;
                                break;
                            }
                            else if (Count == 1 && SwVersion == "BP_R1.0.0" && !isContainSuper && partitionSize > uni.str_to_size("1M"))
                            {
                                do
                                {
                                    if (uni.str_to_size(Count.ToString() + "K") > partitionSize)
                                    {
                                        remainingBytes = uni.str_to_size(Convert.ToString(Count - 1) + "K"); partitionSize = remainingBytes; break;
                                    }
                                    Count += 1;
                                } while (true);
                            }
                        }

                        fileStream.Write(buffer, 0, bytesRead);
                        remainingBytes -= (ulong)bytesRead;
                        writenBytes += bytesRead;

                        MyProgress.ProcessBar1(writenBytes, (long)partitionSize);
                    } while (true);
                    fileStream.Close();
                }

                if (isSparse)
                {
                    Console.WriteLine($"Filename : {fileName} sparsed");
                    Decompress(reader, dataOffset, fileName, OutputDir);
                }
            }

            public static void ScanXMLFile(string XMLData)
            {
                if (!XMLData.Contains("BMAConfig")) return;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(XMLData);

                XmlNode schemeNode = xmlDoc.SelectSingleNode("/BMAConfig/SchemeList/Scheme");
                string schemeName = schemeNode.Attributes["name"].Value;

                XmlNodeList fileNodes = schemeNode.SelectNodes("File");

                foreach (XmlNode fileNode in fileNodes)
                {
                    string id = fileNode.SelectSingleNode("ID").InnerText;
                    string idAlias = fileNode.SelectSingleNode("IDAlias").InnerText;
                    string fileType = fileNode.SelectSingleNode("Type").InnerText;

                    XmlNode blockNode = fileNode.SelectSingleNode("Block");

                    string blockId = string.Empty;

                    if (blockNode.Attributes["id"] != null)
                    {
                        blockId = blockNode.Attributes["id"].Value;
                    }

                    string baseAddress = blockNode.SelectSingleNode("Base").InnerText;
                    string size = blockNode.SelectSingleNode("Size").InnerText;
                    string flag = fileNode.SelectSingleNode("Flag").InnerText;
                    string checkFlag = fileNode.SelectSingleNode("CheckFlag").InnerText;
                    string description = fileNode.SelectSingleNode("Description").InnerText;

                    Console.WriteLine($"ID: {id}");
                    Console.WriteLine($"IDAlias: {idAlias}");
                    Console.WriteLine($"Type: {fileType}");
                    Console.WriteLine($"Block ID: {blockId}");
                    Console.WriteLine($"Base Address: {baseAddress}");
                    Console.WriteLine($"Size: {size}");
                    Console.WriteLine($"Flag: {flag}");
                    Console.WriteLine($"CheckFlag: {checkFlag}");
                    Console.WriteLine($"Description: {description}");
                    Console.WriteLine();

                    Main.SharedUI.DataView.Invoke(
                        new Action(() =>
                        {
                            foreach (DataGridViewRow item in Main.SharedUI.DataView.Rows)
                            {
                                if (Convert.ToString(item.Cells[Main.SharedUI.DataView.Columns[1].Index].Value) == id)
                                {
                                    item.Cells[Main.SharedUI.DataView.Columns[2].Index].Value = blockId;
                                }
                            }
                        })
                    );

                    if (idAlias == "FDL1")
                    {
                        uni.fdl1_addr = baseAddress;
                        Main.SharedUI.TxtFDL1Address.Invoke((Action)(() => Main.SharedUI.TxtFDL1Address.Text = baseAddress));
                    }
                    else if (idAlias == "FDL2")
                    {
                        uni.fdl2_addr = baseAddress;
                        Main.SharedUI.TxtFDL2Address.Invoke((Action)(() => Main.SharedUI.TxtFDL2Address.Text = baseAddress));
                    }
                }

                XmlTextReader xr1 = new XmlTextReader(new StringReader(XMLData));
                while (xr1.Read())
                {
                    if (xr1.NodeType == XmlNodeType.Element && xr1.Name == "Partition")
                    {
                        string Partition = xr1.GetAttribute("id");
                        string Size = xr1.GetAttribute("size");

                        Main.SharedUI.DataView.Invoke(
                            new Action(() =>
                            {
                                foreach (DataGridViewRow item in Main.SharedUI.DataView.Rows)
                                {
                                    if (Convert.ToString(item.Cells[Main.SharedUI.DataView.Columns[2].Index].Value) == Partition)
                                    {
                                        if (!(Size == "0xFFFFFFFF"))
                                        {
                                            item.Cells[Main.SharedUI.DataView.Columns[5].Index].Value = Size + "MB";
                                        }
                                    }
                                    else if (Convert.ToString(item.Cells[Main.SharedUI.DataView.Columns[2].Index].Value) == "uboot")
                                    {
                                        item.Cells[Main.SharedUI.DataView.Columns[5].Index].Value = "1MB";
                                    }
                                    else if (Convert.ToString(item.Cells[Main.SharedUI.DataView.Columns[2].Index].Value) == "splloader")
                                    {
                                        item.Cells[Main.SharedUI.DataView.Columns[5].Index].Value = "1MB";
                                    }
                                }

                                foreach (DataGridViewRow item in Main.SharedUI.DataView.Rows)
                                {
                                    if (Convert.ToString(item.Cells[Main.SharedUI.DataView.Columns[5].Index].Value) == "0xFFFFFFFF")
                                    {
                                        int Count = 1;
                                        do
                                        {
                                            if (uni.str_to_size(Count.ToString()) > Convert.ToUInt64(item.Cells[Main.SharedUI.DataView.Columns[4].Index].Value))
                                            {
                                                item.Cells[Main.SharedUI.DataView.Columns[5].Index].Value = Count - 1 + "MB";
                                                break;
                                            }
                                            Count += 1;
                                        } while (true);
                                    }
                                }
                            })
                        );

                        Console.WriteLine("Partition Name :" + Partition + " Size : " + Size);
                    }
                }

                Main.SharedUI.DataView.Invoke(
                    new Action(() =>
                    {
                        foreach (DataGridViewRow item in Main.SharedUI.DataView.Rows)
                        {
                            if (string.IsNullOrEmpty(Convert.ToString(item.Cells[Main.SharedUI.DataView.Columns[5].Index].Value)))
                            {
                                int Count = 1;
                                do
                                {
                                    if (uni.str_to_size(Count + "M") > Convert.ToUInt64(item.Cells[Main.SharedUI.DataView.Columns[4].Index].Value))
                                    {
                                        item.Cells[Main.SharedUI.DataView.Columns[5].Index].Value = Count - 1 + "MB";
                                        break;
                                    }
                                    Count += 1;
                                } while (true);
                            }
                        }
                    })
                );

                Main.SharedUI.CkPartition.Invoke((Action)(() => Main.SharedUI.CkPartition.Checked = true));
            }

            public static void LoadPACXMLFile(string XMLData)
            {
                if (!XMLData.Contains("BMAConfig")) return;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(XMLData);

                XmlNode schemeNode = xmlDoc.SelectSingleNode("/BMAConfig/SchemeList/Scheme");
                string schemeName = schemeNode.Attributes["name"].Value;

                XmlNodeList fileNodes = schemeNode.SelectNodes("File");

                foreach (XmlNode fileNode in fileNodes)
                {
                    string id = fileNode.SelectSingleNode("ID").InnerText;
                    string idAlias = fileNode.SelectSingleNode("IDAlias").InnerText;
                    string fileType = fileNode.SelectSingleNode("Type").InnerText;

                    XmlNode blockNode = fileNode.SelectSingleNode("Block");

                    string blockId = string.Empty;

                    if (blockNode.Attributes["id"] != null)
                    {
                        blockId = blockNode.Attributes["id"].Value;
                    }

                    string baseAddress = blockNode.SelectSingleNode("Base").InnerText;
                    string size = blockNode.SelectSingleNode("Size").InnerText;
                    string flag = fileNode.SelectSingleNode("Flag").InnerText;
                    string checkFlag = fileNode.SelectSingleNode("CheckFlag").InnerText;
                    string description = fileNode.SelectSingleNode("Description").InnerText;

                    Console.WriteLine($"ID: {id}");
                    Console.WriteLine($"IDAlias: {idAlias}");
                    Console.WriteLine($"Type: {fileType}");
                    Console.WriteLine($"Block ID: {blockId}");
                    Console.WriteLine($"Base Address: {baseAddress}");
                    Console.WriteLine($"Size: {size}");
                    Console.WriteLine($"Flag: {flag}");
                    Console.WriteLine($"CheckFlag: {checkFlag}");
                    Console.WriteLine($"Description: {description}");
                    Console.WriteLine(" ");

                    if (!(idAlias.ToLower().Contains("fdl")) && !(idAlias.ToLower().Contains("erase")) && !(idAlias.ToLower().Contains("phasecheck")))
                    {
                        if (Main.SharedUI.CkKeepNV.Checked)
                        {
                            if (id.ToLower().Contains("nv") || id.ToLower().Contains("nv") || id.ToLower().Contains("efs") || id.ToLower().Contains("efs"))
                            {
                                Main.SharedUI.DataView.Invoke(
                                    (Action)(
                                        () =>
                                            Main.SharedUI.DataView.Rows.Add(
                                                false,
                                                idAlias,
                                                GetPartitionNames(blockId),
                                                "0x0",
                                                "0x0",
                                                string.Empty,
                                                "Double click for input file..."
                                            )
                                    )
                                );
                            }
                            else
                            {
                                Main.SharedUI.DataView.Invoke(
                                    (Action)(
                                        () =>
                                            Main.SharedUI.DataView.Rows.Add(
                                                true,
                                                idAlias,
                                                GetPartitionNames(blockId),
                                                "0x0",
                                                "0x0",
                                                string.Empty,
                                                "Double click for input file..."
                                            )
                                    )
                                );
                            }
                        }
                        else
                        {
                            Main.SharedUI.DataView.Invoke(
                                (Action)(
                                    () =>
                                        Main.SharedUI.DataView.Rows.Add(
                                            true,
                                            idAlias,
                                            GetPartitionNames(blockId),
                                            "0x0",
                                            "0x0",
                                            string.Empty,
                                            "Double click for input file..."
                                        )
                                )
                            );
                        }
                    }

                    if (idAlias == "FDL1")
                    {
                        uni.fdl1_addr = baseAddress;
                        Main.SharedUI.TxtFDL1Address.Invoke((Action)(() => Main.SharedUI.TxtFDL1Address.Text = baseAddress));
                    }
                    else if (idAlias == "FDL2")
                    {
                        uni.fdl2_addr = baseAddress;
                        Main.SharedUI.TxtFDL2Address.Invoke((Action)(() => Main.SharedUI.TxtFDL2Address.Text = baseAddress));
                    }
                }

                XmlTextReader xr1 = new XmlTextReader(new StringReader(XMLData));
                while (xr1.Read())
                {
                    if (xr1.NodeType == XmlNodeType.Element && xr1.Name == "Partition")
                    {
                        string Partition = xr1.GetAttribute("id");
                        string Size = xr1.GetAttribute("size");

                        Main.SharedUI.DataView.Invoke(
                            new Action(() =>
                            {
                                foreach (DataGridViewRow item in Main.SharedUI.DataView.Rows)
                                {
                                    if (Convert.ToString(item.Cells[Main.SharedUI.DataView.Columns[2].Index].Value) == Partition)
                                    {
                                        if (!(Size == "0xFFFFFFFF"))
                                        {
                                            item.Cells[Main.SharedUI.DataView.Columns[5].Index].Value = Size + "MB";
                                        }
                                        else
                                        {
                                            item.Cells[Main.SharedUI.DataView.Columns[5].Index].Value = Size;
                                        }
                                    }
                                    else if (Convert.ToString(item.Cells[Main.SharedUI.DataView.Columns[2].Index].Value) == "uboot")
                                    {
                                        item.Cells[Main.SharedUI.DataView.Columns[5].Index].Value = "1MB";
                                    }
                                    else if (Convert.ToString(item.Cells[Main.SharedUI.DataView.Columns[2].Index].Value) == "splloader")
                                    {
                                        item.Cells[Main.SharedUI.DataView.Columns[5].Index].Value = "1MB";
                                    }
                                }
                            })
                        );
                        Console.WriteLine("Partition Name :" + Partition + " Size : " + Size);
                    }
                }

                Main.SharedUI.CkPartition.Invoke((Action)(() => Main.SharedUI.CkPartition.Checked = true));
            }

            public static string GetPartitionNames(string Partition)
            {
                return Partition.ToLower();
            }

            public static void StartExtraction(string[] args)
            {
                if (args.Length < 2) return;

                MyDisplay.RichLogs("LOAD PAC FIRMWARE\n", Color.DarkOrange, true, true);

                debug = false;
                SwVersion = null;
                pacfile = args[0];
                outputDir = args[1];
                CurrentFileSizes = 0;
                isContainSuper = false;
                isNeedFixOffsets = false;

                if (args.Length >= 3 && args[2] == "-debug") debug = true;

                UnpackPacFile(pacfile, outputDir, debug);
            }

            #region Sparse
            #region Deklarasi Sparse
#pragma warning disable
            private static CHUNK_HEADER chunkheader;
            private static SPARSE_HEADER sparseheader;
            private const Int64 SPARSE_MAGIC = unchecked((int)0xEED26FF3A);
            private const Int64 SPARSE_RAW_CHUNK = 0xECAC1;
            private const Int64 SPARSE_FILL_CHUNK = 0xECAC2;
            private const Int64 SPARSE_DONT_CARE = 0xECAC3;
            private static long totalchunk;
            private static int blocksize;

            private struct CHUNK_HEADER
            {
                public Int16 wChunkType;
                public Int16 wReserved;
                public Int32 dwChunkSize;
                public Int32 dwTotalSize;
            }

            private struct SPARSE_HEADER
            {
                public Int32 dwMagic; //4
                public Int16 wVerMajor; //2
                public Int16 wVerMinor; //2
                public Int16 wSparseHeaderSize; //2
                public Int16 wChunkHeaderSize; //2
                public Int32 dwBlockSize; //4
                public Int32 dwTotalBlocks; //4
                public Int32 dwTotalChunks;
                public Int32 dwImageChecksum;
            }
            private static SPARSE_HEADER parsingheader(byte[] bytes)
            {
                SPARSE_HEADER stuff = new SPARSE_HEADER();
                GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                try
                {
                    stuff = (SPARSE_HEADER)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(SPARSE_HEADER));
                }
                finally
                {
                    handle.Free();
                }
                return stuff;
            }
            #endregion
#pragma warning enable

            private static bool CekSparse(byte[] DataFiles)
            {
                if (Main.SharedUI.UnisocWorker.CancellationPending) return false;
                if (DataFiles.Length == 0) return false;

                long header_magic = 0;
                Stream stream = new MemoryStream(DataFiles);
                byte[] buffer = new byte[1024];
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    reader.Read(buffer, 0, 28);
                    sparseheader = parsingheader(buffer);
                    var magic = sparseheader.dwMagic;
                    header_magic = Convert.ToInt64(magic);
                    if (header_magic == SPARSE_MAGIC)
                    {
                        totalchunk = sparseheader.dwTotalChunks;
                        blocksize = sparseheader.dwBlockSize;
                        stream.Close();
                        reader.Close();
                        return true;
                    }
                    else
                    {
                        stream.Close();
                        reader.Close();
                        return false;
                    }
                }
            }

            private static void Decompress(
                BinaryReader binaryReader,
                ulong dataOffset,
                string fileName,
                string OutputDir
            )
            {
                binaryReader.BaseStream.Seek((long)dataOffset, SeekOrigin.Begin);
                bool clean_header = true;
                using (var fh = new FileStream(Path.Combine(OutputDir, fileName), FileMode.Create, FileAccess.Write))
                {
                    long i = 0;
                    long besarFile = 0L;
                    long TotalWriten = 0L;
                    long totalwritten = 0L;
                    long bytesWritten = 0L;
                    long bytesTobeWrite = 0L;
                    int sector_size = blocksize; //4096
                    byte[] buffer = new byte[1024];
                    long offsset = (long)dataOffset;
                    chunkheader = new CHUNK_HEADER();
                    do
                    {
                        if (Main.SharedUI.UnisocWorker.CancellationPending) break;

                        double hexchunk;
                        long sectorsizeCunk;
                        if (i == 0)
                        {
                            binaryReader.BaseStream.Seek(offsset + 28L, SeekOrigin.Begin);
                            binaryReader.Read(buffer, 0, 12);
                            chunkheader.wChunkType = BitConverter.ToInt16(buffer.Skip(0).Take(2).ToArray(), 0);
                            chunkheader.dwChunkSize = BitConverter.ToInt32(buffer.Skip(4).Take(4).ToArray(), 0);
                            chunkheader.dwTotalSize = BitConverter.ToInt32(buffer.Skip(8).Take(4).ToArray(), 0);
                            short wChunkType = chunkheader.wChunkType;
                            hexchunk = Dextohex.Hexval("&HE" + Convert.ToString(wChunkType, 16).ToUpper());
                            offsset += chunkheader.dwTotalSize;
                            long sizechunk = chunkheader.dwChunkSize;
                            sectorsizeCunk = sizechunk * sparseheader.dwBlockSize;
                        }
                        else
                        {
                            binaryReader.BaseStream.Seek(offsset + 28L, SeekOrigin.Begin);
                            binaryReader.Read(buffer, 0, 12);
                            chunkheader.wChunkType = BitConverter.ToInt16(buffer.Skip(0).Take(12).ToArray(), 0);
                            chunkheader.dwChunkSize = BitConverter.ToInt32(buffer.Skip(4).Take(4).ToArray(), 0);
                            chunkheader.dwTotalSize = BitConverter.ToInt32(buffer.Skip(8).Take(4).ToArray(), 0);
                            short wChunkType = chunkheader.wChunkType;
                            hexchunk = Dextohex.Hexval("&HE" + Convert.ToString(wChunkType, 16).ToUpper());
                            offsset += chunkheader.dwTotalSize;
                            long sizechunk = chunkheader.dwChunkSize;
                            sectorsizeCunk = sizechunk * sparseheader.dwBlockSize;
                        }
                        if (hexchunk == Convert.ToDouble(SPARSE_RAW_CHUNK))  //SPARSE_RAW_CHUNK
                        {
                            bytesWritten = 0L;
                            do
                            {
                                if (Main.SharedUI.UnisocWorker.CancellationPending) break;
                                if (bytesWritten == sectorsizeCunk) break;
                                byte[] byt = new byte[(int)Math.Min((long)blocksize, sectorsizeCunk)];
                                binaryReader.Read(byt, 0, byt.Length);
                                if (clean_header)
                                {
                                    byt = new byte[0x60].Concat(byt.Skip(0x60).ToArray()).ToArray(); clean_header = false;
                                }
                                fh.Write(byt, 0, byt.Length);
                                bytesTobeWrite += byt.Length;
                                bytesWritten += byt.Length;
                                totalwritten += byt.Length;
                                MyProgress.ProcessBar1(bytesWritten, sectorsizeCunk);
                            } while (true);
                        }
                        else if (hexchunk == Convert.ToDouble(SPARSE_FILL_CHUNK)) //SPARSE_FILL_CHUNK
                        {
                            bytesWritten = 0L;
                            do
                            {
                                if (Main.SharedUI.UnisocWorker.CancellationPending) break;
                                if (bytesWritten == sectorsizeCunk) break;
                                byte[] byt = new byte[(int)Math.Min((long)blocksize, sectorsizeCunk)];
                                binaryReader.Read(byt, 0, byt.Length);
                                if (clean_header)
                                {
                                    byt = new byte[0x60].Concat(byt.Skip(0x60).ToArray()).ToArray(); clean_header = false;
                                }
                                fh.Write(byt, 0, byt.Length);
                                bytesTobeWrite += byt.Length;
                                bytesWritten += byt.Length;
                                totalwritten += byt.Length;
                                MyProgress.ProcessBar1(bytesWritten, sectorsizeCunk);
                            } while (true);
                        }
                        else if (hexchunk == Convert.ToDouble(SPARSE_DONT_CARE)) //SPARSE_DONT_CARE
                        {
                            bytesWritten = 0L;
                            do
                            {
                                if (Main.SharedUI.UnisocWorker.CancellationPending) break;
                                if (bytesWritten == sectorsizeCunk) break;
                                byte[] byt = new byte[(int)Math.Min((long)blocksize, sectorsizeCunk)];
                                binaryReader.Read(byt, 0, byt.Length);
                                if (clean_header)
                                {
                                    byt = new byte[0x60].Concat(byt.Skip(0x60).ToArray()).ToArray(); clean_header = false;
                                }
                                fh.Write(byt, 0, byt.Length);
                                bytesTobeWrite += byt.Length;
                                bytesWritten += byt.Length;
                                totalwritten += byt.Length;
                                MyProgress.ProcessBar1(bytesWritten, sectorsizeCunk);
                            } while (true);
                        }
                        else
                        {
                            Console.WriteLine($"Error Chunk : {hexchunk} blocksize : {sector_size} at offset : {offsset}");
                        }
                        i++;
                        MyProgress.ProcessBar2(i, totalchunk);
                        if (i == totalchunk) break;
                    } while (true);
                    fh.Close();
                }
            }
            #endregion
        }

        #region PAC Structure
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct PacHeaderStruct
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 44)]
            public byte[] szVersion;
            public uint dwHiSize;
            public uint dwLoSize;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] productName;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] firmwareName;
            public uint partitionCount;
            public uint partitionsListStart;
            public uint dwMode;
            public uint dwFlashType;
            public uint dwNandStrategy;
            public uint dwIsNvBackup;
            public uint dwNandPageType;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 996)]
            public byte[] szPrdAlias;
            public uint dwOmaDmProductFlag;
            public uint dwIsOmaDM;
            public uint dwIsPreload;
            public uint dwReserved;
            public uint dwMagic;
            public uint wCRC1;
            public uint wCRC2;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60)]
            public string reservedData;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct FileHeaderStruct_BP_R1
        {
            public uint length;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] partitionName;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] fileName;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 508)]
            public byte[] szFileName;

            public uint hiDataOffset;
            public uint hiPartitionSize;
            public uint dwReserved1;
            public uint dwReserved2;
            public uint loDataOffset;
            public uint loPartitionSize;
            public ushort nFileFlag;
            public ushort nCheckFlag;
            public uint dwReserved3;
            public uint dwCanOmitFlag;
            public uint dwAddrNum;
            public uint dwAddr;
            public uint dwReserved4;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 996)]
            public string reservedData;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct FileHeaderStruct_BP_R2
        {
            public uint length;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] partitionName;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] fileName;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 508)]
            public byte[] szFileName;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] szPartitionInfo;

            public uint dwReserved2;
            public ushort nFileFlag;
            public ushort nCheckFlag;
            public uint dwReserved3;
            public uint dwCanOmitFlag;
            public uint dwAddrNum;
            public uint dwAddr;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 996)]
            public string reservedData;
        }
        #endregion
        #endregion


        public class Dextohex
        {
            public static double Hexval(string x)
            {
                if (x != null)
                {
                    long result2;
                    if (!x.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    {
                        if (
                            x.StartsWith("&H", StringComparison.OrdinalIgnoreCase)
                            && long.TryParse(
                                x.Substring(2),
                                NumberStyles.AllowHexSpecifier,
                                CultureInfo.CurrentCulture,
                                out var result
                            )
                        )
                        {
                            return result;
                        }
                    }
                    else if (
                        long.TryParse(
                            x.Substring(2),
                            NumberStyles.AllowHexSpecifier,
                            CultureInfo.CurrentCulture,
                            out result2
                        )
                    )
                    {
                        return result2;
                    }
                    int num = x.Length;
                    while (num > 0)
                    {
                        if (!double.TryParse(x.Substring(0, num), out var result3))
                        {
                            num--;
                            continue;
                        }
                        return result3;
                    }
                    return 0.0;
                }
                return 0.0;
            }
        }
    }
}
