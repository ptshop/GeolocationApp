using static Geolocation.Implementations.Private.Helpers.LocationIndexHelper;
using static Geolocation.Implementations.Private.Helpers.StringHelper;

namespace Geolocation.Implementations.Private.Finders
{
    unsafe class IndexesByCityFinder
    {
        /// <summary>
        /// Бинарный поиск диапазона индексов записей местоположений по названию города.
        /// </summary>
        public static LocationIndexRange Find(string city, Location* pFirstLocation, uint* pFirstIndex, uint* pLastIndex)
        {
            var finder = new IndexesByCityFinder(city, pFirstLocation, pFirstIndex, pLastIndex);
            return finder.Find();
        }

        private IndexesByCityFinder(string city, Location* pFirstLocation, uint* pFirstIndex, uint* pLastIndex)
        {
            this.city = city;
            this.pFirstLocation = pFirstLocation;
            this.pFirstIndex = pFirstIndex;
            this.pLastIndex = pLastIndex;
        }

        /// <summary>
        /// Бинарный поиск диапазона индексов записей местоположений
        /// </summary>
        /// <returns></returns>
        private LocationIndexRange Find()
        {
            var first = pFirstIndex;
            var last = pLastIndex;

            while (first <= last)
            {
                var mid = first + (last - first) / 2;

                var comparison = CompareCityWithLocationCityAtIndex(*mid); // сравнение искомого названия города с названием города местоположения по текущему индексу mid

                if (comparison == 0) // если названия городов совпали, то нужно найти начало и конец диапазона индексов местоположений с таким же названием городов
                {
                    return new LocationIndexRange()
                    {
                        StartIndex = FindCityEdgeIndex(SearchDirection.ToStart, first, mid, pFirstIndex), // поиск начала диапазона подходящих индексов
                        EndIndex = FindCityEdgeIndex(SearchDirection.ToEnd, mid, last, pLastIndex)        // поиск конца диапазона подходящих индексов
                    };
                }

                if (comparison < 0)
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

        /// <summary>
        /// Бинарный поиск одной из границ диапазона индексов записей местоположений
        /// </summary>
        /// <param name="direction">Направление поиска. В сторону начала или в сторону конца списка индексов</param>
        /// <param name="mostEdge">Самый крайний индекс в выбранном направлении поиска</param>
        /// <returns></returns>
        private uint* FindCityEdgeIndex(SearchDirection direction, uint* first, uint* last, uint* mostEdge)
        {
            while (first <= last)
            {
                var mid = first + (last - first) / 2;

                var comparison = CompareCityWithLocationCityAtIndex(*mid); // сравнение искомого названия города с названием города местоположения по текущему индексу mid

                if (comparison == 0) // если названия городов совпали, то нужно проверить соседний индекс по направлению поиска
                {
                    if (mid == mostEdge ||                                                 // если достигли самого края списка индексов
                        CompareCityWithLocationCityAtIndex(*(mid + (int)direction)) != 0)  // либо название города местоположения по соседнему индексу не совпадает с искомым,
                    {                                                                      // значит граница диапазона найдена
                        return mid;
                    }
                }

                if (direction == SearchDirection.ToStart && comparison <= 0 ||
                    direction == SearchDirection.ToEnd && comparison < 0)
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

        private int CompareCityWithLocationCityAtIndex(uint index)
        {
            var pLocation = pFirstLocation + ConvertByteIndexToLocationIndex(index);

            return Compare(city, pLocation->City, Constants.Location.CitySize);
        }

        private readonly string city;
        private readonly Location* pFirstLocation;
        private readonly uint* pFirstIndex;
        private readonly uint* pLastIndex;

        /// <summary>
        /// Перечисление для задания направления поиска одной из границ диапазона индексов записей местоположений.
        /// Выбраны значения -1 и 1, для того, чтобы их можно было напрямую использовать в качестве инкремента или декремента
        /// для вычисления соседнего индекса во время поиска границы диапазона.
        /// </summary>
        private enum SearchDirection
        {
            ToStart = -1, 
            ToEnd = 1
        }
    }
}
