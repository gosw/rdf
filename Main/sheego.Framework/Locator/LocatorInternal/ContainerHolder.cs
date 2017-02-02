using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using sheego.Framework.Locator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sheego.Framework.Locator.Internal
{
    public class ContainerHolder
    {
        private static IWindsorContainer _container;
        private static IWindsorContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new WindsorContainer();
                    _container.Install(Configuration.FromAppConfig());
                }
                return _container;
            }
        }

        /**
         * for locator assemblies (decoupling DI-Container usage from locator) 
         */
        public static ILocatedObject<T> Resolve<T>() where T : class
        {
            return new LocatedObject<T>(Container.Resolve<T>());
        }

        public static void Register<T1>(T1 instance, bool isDefault = false) where T1 : class
        {
            if (isDefault)
                Container.Register(Component.For<T1>().Instance(instance).IsDefault());
            else
                Container.Register(Component.For<T1>().Instance(instance));
        }

        public static void Reset()
        {
            _container = null;
        }

        public static void Release(object component)
        {
            Container.Release(component);
        }
    }
}
