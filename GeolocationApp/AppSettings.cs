using System;
using System.Configuration;
using System.IO;
using System.Web.Hosting;

namespace GeolocationApp
{
    public static class AppSettings
    {
        private const string DataBaseFileSettingName = "DatabaseFile";

        public static string DatabaseFile
        {
            get
            {
                var databaseFileName = ConfigurationManager.AppSettings[DataBaseFileSettingName];
                if (databaseFileName == null)
                {
                    throw new Exception($"Не указан файл базы данных в \"Web.config\". Укажите его в разделе \"appSettings\" с ключом \"{DataBaseFileSettingName}\".");
                }

                var databaseFileFullName = HostingEnvironment.MapPath(databaseFileName);
                if (!File.Exists(databaseFileFullName))
                {
                    throw new Exception($"Файл базы данных \"{databaseFileFullName}\" не найден.");
                }

                return databaseFileFullName;
            }
        }
    }
}