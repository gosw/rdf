using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sheego.Framework.Locator.Shared
{
    public interface ILocatedObject<T> : IDisposable where T : class
    {
        T Object { get; }
    }
}
