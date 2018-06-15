using System.Collections.Generic;

namespace sheego.Framework.Domain.Shared
{
    public interface IRepositoryService
    {
        //CRUD
        void CreateRelease(IRelease release);
        void CreateDeployment(IDeployment deployment);
        void CreateDeploymentStep(string deploymentName, IDeploymentStep deploymentStep);
        void CreateConfiguration(string id, IConfiguration configuration);
        IEnumerable<string> ReadReleaseVersions();
        IList<IRelease> ReadReleases();
        IList<IDeployment> ReadDeployments();
        IRelease ReadRelease(string id);
        IDeployment ReadDeployment(string id); 
        IEnumerable<IDeploymentStep> ReadDeploymentSteps(string name);
        IConfiguration ReadConfiguration(string id);
        void DeleteRelease(IRelease release);
        void DeleteDeployment(IDeployment deployment);
    }
}