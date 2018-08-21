using System;
using sheego.Framework.Data.Shared;
using System.Configuration;
using System.IO;

namespace sheego.Framework.Data.Impl
{
    class ConfigurationService : IConfigurationService
    {
        public string GetObjectFilename(string objectType, string id = null, string extension = null)
        {
            CreateInitialState(objectType);
            var path = ConfigurationManager.AppSettings["RootPath"];
            Directory.CreateDirectory(string.Format(@"{0}\{1}", path, objectType));
            return string.Format(@"{0}\{1}\{2}.{3}", path, objectType, id, extension);
        }

        public object GetFilesPath(string objectType)
        {
            //CreateInitialState(objectType);
            var path = ConfigurationManager.AppSettings["RootPath"];
            //Directory.CreateDirectory(string.Format(@"{0}\{1}", path, objectType));
            return string.Format(@"{0}\{1}", path, objectType);
        }

        public void CreateInitialState (string objectType)
        {
            if (!File.Exists(ConfigurationManager.AppSettings["MainConfigFilePath"]))
            {
                //ToDO: test Copy (C:\\solutionfolder,C:\\destinatiopath)
                var path = ConfigurationManager.AppSettings["InitialStatePath"];
                Directory.CreateDirectory(string.Format(@"{0}", path));
            }

            var rootPath = ConfigurationManager.AppSettings["RootPath"];
            if (!Directory.Exists(string.Format(@"{0}\{1}", rootPath, objectType)))
            {
                Directory.CreateDirectory(string.Format(@"{0}\{1}", rootPath, objectType));
            }
            return;
        }
    }
}