using System;
using System.Collections.Generic;
using sheego.Framework.Domain.Shared;
using sheego.Framework.Domain.Shared.Locator;
using sheego.Framework.Data.Shared.Locator;

namespace sheego.Framework.Domain.Impl
{
    class RepositoryService : IRepositoryService
    {
        public void CreateRelease(IRelease release)
        {
            using (var service = DataLocator.GetPersistenceService())
            {
                service.Object.Create(release.Version, release);
            }
        }

        public void CreateDeployment(IDeployment deployment)
        {
            using (var service = DataLocator.GetPersistenceService())
            {
                service.Object.Create(deployment.Name, deployment); 
            }
        }

        public IList<IRelease> ReadReleases()
        {
            var list = new List<IRelease>();
            using (var service = DataLocator.GetPersistenceService())
            {
                foreach (var id in service.Object.List<IRelease>())
                {
                    var release = service.Object.Read<IRelease>(id);
                    list.Add(release);
                }
            }
            return list;
        }

        public IList<IDeployment> ReadDeployments()
        {
            var list = new List<IDeployment>();
            using (var service = DataLocator.GetPersistenceService())
            {
                foreach (var id in service.Object.List<IDeployment>())
                {
                    var deployment = service.Object.Read<IDeployment>(id);
                    list.Add(deployment);
                }
            }
            return list;
        }

        public IDeployment ReadDeployment(string id)
        {
            using (var service = DataLocator.GetPersistenceService())
            {
                return service.Object.Read<IDeployment>(id);
            }
        }

        //ToDo: ReadRelease(id)

        public void DeleteRelease(IRelease release)
        {
            using (var service = DataLocator.GetPersistenceService())
            {
                service.Object.Delete(release.Version, release);
            }
        }

        public void DeleteDeployment(IDeployment deployment)
        {
            using (var service = DataLocator.GetPersistenceService())
            {
                service.Object.Delete(deployment.Name, deployment); 
            }
        }

        public IEnumerable<string> ReadReleaseVersions()
        {
            var list = new List<string>();
            using (var service = DataLocator.GetPersistenceService())
            {
                foreach (var id in service.Object.List<IRelease>())
                {
                    list.Add(id);
                }
            }
            return list;
        }
    }
}