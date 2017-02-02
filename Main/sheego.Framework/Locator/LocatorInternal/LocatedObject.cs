using sheego.Framework.Locator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sheego.Framework.Locator.Internal
{
    class LocatedObject<T> : ILocatedObject<T> where T : class
    {
        private bool _disposed = false;
        private T _object;

        public LocatedObject(T obj)
        {
            _object = obj;
        }

        #region ILocatedObject<T> Members

        public T Object
        {
            get { return _object; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                ContainerHolder.Release(_object);
                _object = null;
                GC.SuppressFinalize(this);
                _disposed = true;
            }
        }

        #endregion
    }
}
