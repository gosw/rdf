using sheego.Framework.Locator.Internal;
using sheego.Framework.Locator.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sheego.Framework.Data.Shared.Locator
{
    public class DataLocator
    {
        public static ILocatedObject<IPersistenceService> GetPersistenceService()
        {
            return ContainerHolder.Resolve<IPersistenceService>();
        }
     
        public static object GetObject(Type objectType)
        {
            return ContainerHolder.Resolve(objectType);
        }
    }

}
