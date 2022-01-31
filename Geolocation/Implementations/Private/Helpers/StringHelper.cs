using System;
using System.Text;

namespace Geolocation.Implementations.Private.Helpers
{
    static class StringHelper
    {
        unsafe public static string CreateString(sbyte* pBytes, int maxLength)
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < maxLength; i++)
            {
                if (pBytes[i] != '\0')
                {
                    stringBuilder.Append(Convert.ToChar(pBytes[i]));
                }
            }

            return stringBuilder.ToString();
        }

        unsafe public static int Compare(string city1, sbyte* pCity2, int city2MaxLength)
        {
            // TODO: для увеличения производительности можно применить другой способ сравнения строк string и sbyte*, не требующий создания вспомогательной строки,
            // стоит смортеть в сторону готовых реализаций либо на WinAPI, либо вызов подходящей C/С++ функции из семейства strcmp()

            var city2 = CreateString(pCity2, city2MaxLength);

            return string.CompareOrdinal(city1, city2);
        }
    }
}
