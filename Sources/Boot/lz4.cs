using System.Runtime.InteropServices;

namespace iReverse_Unisoc_Ultimate.My.Boot
{
    public static class Lz4Decompressor
    {
        [DllImport(@"Data\DeviceApi\LZ4.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int LZ4_hadikit_iReverse_decompress(byte[] src, byte[] dst, int isize);

        public static int lz4_hadikit_decompress(byte[] src, ref byte[] dst)
        {
            return LZ4_hadikit_iReverse_decompress(src: src, dst: dst, isize: src.Length);
        }
    }
}
