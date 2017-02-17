﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using sheego.Framework.Domain.Shared;

namespace sheego.Framework.Domain.Impl.Locator
{
    public class DomainInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IAuthorizationService>() 
                .ImplementedBy<AuthorizationService>()
                .LifestyleTransient());

            container.Register(
                Component.For<IRelease>() 
                .ImplementedBy<Release>() 
                .LifestyleTransient());

            container.Register(
                Component.For<IReleaseUnit>()
                .ImplementedBy<ReleaseUnit>()
                .LifestyleTransient());

            container.Register(
                Component.For<IRepositoryService>()
                .ImplementedBy<RepositoryService>()
                .LifestyleTransient());

            container.Register(
                Component.For<IDeployment>()
                .ImplementedBy<Deployment>()
                .LifestyleTransient());

            container.Register(
                Component.For<IDeploymentStep>()
                .ImplementedBy<DeploymentStep>()
                .LifestyleTransient());
        }
    }
}