namespace Geolocation.Implementations.Private.Finders
{
    unsafe class IpRangeFinder
    {
        /// <summary>
        /// Бинарный поиск записи интервала IP адресов.
        /// </summary>
        /// <returns></returns>
        public static IpRange* Find(uint ip, IpRange* first, IpRange* last)
        {
            while (first <= last)
            {
                var mid = first + (last - first) / 2;

                if (mid->IpFrom <= ip && ip <= mid->IpTo)
                {
                    return mid;
                }

                if (ip < mid->IpFrom)
                {
                    last = mid - 1;
                }
                else
                {
                    first = mid + 1;
                }
            }

            return null;
        }
    }
}
