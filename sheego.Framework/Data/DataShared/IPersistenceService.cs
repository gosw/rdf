using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sheego.Framework.Data.Shared
{
    public interface IPersistenceService
    {
        void Create<T>(string id, T someObject);
        T Read<T>(string id);
        void Delete<T>(string id, T someObject);
        IEnumerable<string> List<T>();
        IEnumerable<T> List<T>(string filter);
    }
}
