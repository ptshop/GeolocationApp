namespace Geolocation.Implementations.Private.Helpers
{
    static class LocationIndexHelper
    {
        /// <summary>
        /// Приводит индекс, заданный в номерах байтов в индекс, заданный в номерах записей местоположений
        /// </summary>
        public static uint ConvertByteIndexToLocationIndex(uint index)
        {
            return index / Constants.Location.Size;
        }
    }
}
