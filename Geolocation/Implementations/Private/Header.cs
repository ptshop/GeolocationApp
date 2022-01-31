using System.Runtime.InteropServices;

using static Geolocation.Implementations.Private.Constants.Header;

namespace Geolocation.Implementations.Private
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    unsafe struct Header
    {
        public int Version;
        public fixed sbyte Name[NameSize];
        public ulong Timestamp;
        public int RecordsCount;
        public uint IpRangesOffset;
        public uint IndexesOffset;
        public uint LocationsOffset;
    }
}
