using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sheego.Framework.Data.Shared
{
    public interface IConfigurationService
    {
        string GetObjectFilename(string objectType, string id, string extension);
        object GetFilesPath(string objectType);
    }
}
