using Newtonsoft.Json;
using sheego.Framework.Data.Shared;
using sheego.Framework.Domain.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sheego.Framework.Data.Impl
{
    class PersistenceService : IPersistenceService
    {
        public void Create<T>(string id, T someObject)
        {
            IConfigurationService configurationService = new ConfigurationService();
            var path = configurationService.GetObjectFilename(typeof(T).Name, id, "json");
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
            string[] list = Directory.GetFiles((string)configurationService.GetFilesPath(typeof(T).Name),"*.json");
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
            string[] list = Directory.GetFiles((string)configurationService.GetFilesPath(typeof(T).Name), filter + "*.json");
            foreach (var id in list)
            {
                var obj = Read<T>(Path.GetFileNameWithoutExtension(id));
                objList.Add(obj);
            }
            return objList;
        }
    }
}
