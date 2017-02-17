using System;
using sheego.Framework.Data.Shared;
using System.Configuration;
using System.IO;

namespace sheego.Framework.Data.Impl
{
    class ConfigurationService : IConfigurationService
    {
        public string GetObjectFilename(string objectType, string id, string extension)
        {
            var path = ConfigurationManager.AppSettings["RootPath"];
            Directory.CreateDirectory(string.Format(@"{0}\{1}", path, objectType));
            return string.Format(@"{0}\{1}\{2}.{3}", path, objectType, id, extension);
        }

        public object GetFilesPath(string objectType)
        {
            var path = ConfigurationManager.AppSettings["RootPath"];
            return string.Format(@"{0}\{1}", path, objectType);
        }
    }
}