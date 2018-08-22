using System.Collections.Generic;

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
