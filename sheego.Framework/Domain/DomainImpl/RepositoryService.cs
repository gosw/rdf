using System;
using System.Collections.Generic;
using sheego.Framework.Domain.Shared;
using sheego.Framework.Domain.Shared.Locator;
using sheego.Framework.Data.Shared.Locator;
using System.Linq;

namespace sheego.Framework.Domain.Impl
{
    class RepositoryService : IRepositoryService
    {
        #region Create

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

        public void CreateDeploymentStep(string deploymentName, IDeploymentStep deploymentStep)
        {
            using (var service = DataLocator.GetPersistenceService())
            {
                service.Object.Create(deploymentName + " Step" + deploymentStep.Id, deploymentStep);
            }
        }

        #endregion Create

        #region Read

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

        //ToDo: ReadRelease(id)

        public IDeployment ReadDeployment(string id)
        {
            using (var service = DataLocator.GetPersistenceService())
            {
                return service.Object.Read<IDeployment>(id);
            }
        }

        public IEnumerable<IDeploymentStep> ReadDeploymentSteps (string deploymentName)
        {
            using (var service = DataLocator.GetPersistenceService())
            {
                var list = service.Object.List<IDeploymentStep>(deploymentName).ToList();
                //Activate next Step
                //ToDo: create separate service for StepState control
                for(var i = 0;i < list.Count;i++)
                {
                    if (list[i].StepState == DeploymentStepState.Active)
                        break;

                    if (list[i].StepState == DeploymentStepState.Init)
                    {
                        list[i].StepState = DeploymentStepState.Active;
                        break;
                    }
                }
                return list;
            }
        }

        public IConfiguration ReadConfiguration(string id)
        {
            using (var service = DataLocator.GetPersistenceService())
            {
                return service.Object.Read<IConfiguration>(id);
            }
        }

        #endregion Read

        #region Update
        #endregion Update

        #region Delete

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

        #endregion Delete

    }
}