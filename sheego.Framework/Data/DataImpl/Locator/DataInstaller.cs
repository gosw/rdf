using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using sheego.Framework.Data.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sheego.Framework.Data.Impl.Locator
{
    public class DataInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IPersistenceService>()
                .ImplementedBy<PersistenceService>()
                .LifestyleTransient()); 
        }
    }
}
