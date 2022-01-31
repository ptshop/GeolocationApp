using System.IO;

namespace Geolocation.Implementations
{
    public class FileDatabaseLoader : IDatabaseLoader
    {
        public FileDatabaseLoader(string fileName)
        {
            this.fileName = fileName;
        }

        public byte[] LoadDatabase()
        {
             return File.ReadAllBytes(fileName);
        }

        private readonly string fileName;
    }
}
