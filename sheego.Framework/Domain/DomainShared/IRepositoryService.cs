using System.Collections.Generic;

namespace sheego.Framework.Domain.Shared
{
    public interface IRepositoryService
    {
        //CRUD
        void CreateRelease(IRelease release);
        void CreateDeployment(IDeployment deployment);
        void CreateDeploymentStep(string deploymentName, IDeploymentStep deploymentStep);
        IList<IRelease> ReadReleases();
        IList<IDeployment> ReadDeployments();
        IDeployment ReadDeployment(string id);
        //ToDo: to complete later
        //ReadRelease(string id) 
        void DeleteRelease(IRelease release);
        void DeleteDeployment(IDeployment deployment);
        IEnumerable<string> ReadReleaseVersions();
        IEnumerable<IDeploymentStep> ReadDeploymentSteps(string name);
    }
}