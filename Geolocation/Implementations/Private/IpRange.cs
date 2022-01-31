using System.Runtime.InteropServices;

namespace Geolocation.Implementations.Private
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct IpRange
    {
        public uint IpFrom;
        public uint IpTo;
        public uint LocationIndex;
    }
}
