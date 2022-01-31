using System;
using System.Net;

namespace Geolocation.Implementations.Private.Helpers
{
    static class IpAddressHelper
    {
        public static uint ConvertIpToUInt32(string ip)
        {
            var bytes = IPAddress.Parse(ip).GetAddressBytes();

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt32(bytes, 0);
        }
    }
}
