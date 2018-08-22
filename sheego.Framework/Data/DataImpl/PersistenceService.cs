using Newtonsoft.Json;
using sheego.Framework.Data.Shared;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace sheego.Framework.Data.Impl
{
    class PersistenceService : IPersistenceService
    {
        public void Create<T>(string id, T someObject)
        {
            IConfigurationService configurationService = new ConfigurationService();
            var path = configurationService.GetObjectFilename(typeof(T).Name, id, "json");

            //Workaround
            if (path.EndsWith("MainConfiguration.json") && File.Exists(path)) return;

            using (StreamWriter file = new StreamWriter(path))
            {
                string json = JsonConvert.SerializeObject(someObject);
                file.Write(json);
            }
        }

        public T Read<T>(string id)
        {
            AutofacContractResolver contractResolver = new AutofacContractResolver();
            IConfigurationService configurationService = new ConfigurationService();
            var path = configurationService.GetObjectFilename(typeof(T).Name, id, "json");

            using (StreamReader file = new StreamReader(path))
            {
                string jsonString = file.ReadToEnd();
                T someObject = JsonConvert.DeserializeObject<T>(jsonString, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver
                });
                return someObject;
            }
        }

        public void Delete<T>(string id, T someObject)
        {
            IConfigurationService configurationService = new ConfigurationService();
            var path = configurationService.GetObjectFilename(typeof(T).Name, id, "json");

            File.Delete(path);
        }

        public IEnumerable<string> List<T>()
        {
            var idList = new List<string>();
            IConfigurationService configurationService = new ConfigurationService();
            //var path = configurationService.GetFilesPath(typeof(T).Name);
            string[] list = new string[] { };
            try
            {
                list = Directory.GetFiles((string)configurationService.GetFilesPath(typeof(T).Name), "*.json");
            }
            catch (DirectoryNotFoundException)
            {
                configurationService.GetFilesPath(typeof(T).Name);
            }
            foreach (var id in list)
            {
                idList.Add(Path.GetFileNameWithoutExtension(id));
            }
            return idList;
        }

        public IEnumerable<T> List<T>(string filter)
        {
            var objList = new List<T>();
            IConfigurationService configurationService = new ConfigurationService();
            //var path = configurationService.GetFilesPath(typeof(T).Name);
            var list = Directory.GetFiles((string)configurationService.GetFilesPath(typeof(T).Name), filter + "*.json");
            return list.Select(id => Read<T>(Path.GetFileNameWithoutExtension(id))).ToList();
        }
    }
}
