using Geolocation;
using Geolocation.Implementations;
using Moq;
using NUnit.Framework;

namespace GeolocationTests
{
    [TestFixture]
    class LocationServiceTests
    {
        // Тест приведен для демонстрации возможности тестирования класса LocationService при помощи заглушечной реализации интерфейса IDatabaseLoader 
        [Test]
        public void DatabaseLoad_Test()
        {
            byte[] database = new byte[]
            {
                5, 0, 0, 0,
                (byte)'F', (byte)'o', (byte)'o', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                1, 2, 3, 4, 5, 6, 7, 8,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0
            };

            var databaseLoader = Mock.Of<IDatabaseLoader>(l => l.LoadDatabase() == database);

            var locationService = new LocationService(databaseLoader);

            Assert.AreEqual(5, locationService.DatabaseVersion);
            Assert.AreEqual("Foo", locationService.DatabaseName);
            Assert.AreEqual(0x0807060504030201, locationService.DatabaseTimestamp);
            Assert.IsNull(locationService.FindLocationByIp("1.1.1.1"));
            Assert.IsNull(locationService.FindLocationsByCity("Bar"));
        }
    }
}