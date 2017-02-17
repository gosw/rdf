using System.Collections.Generic;

namespace sheego.Framework.Domain.Shared
{
    public interface IRepositoryService
    {
        //CRUD
        void CreateRelease(IRelease release);
        void CreateDeployment(IDeployment deployment);
        IList<IRelease> ReadReleases();
        IList<IDeployment> ReadDeployments();
        IDeployment ReadDeployment(string id);
        //ReadRelease(string id) //ToDo: to complete later
        void DeleteRelease(IRelease release);
        void DeleteDeployment(IDeployment deployment);
        IEnumerable<string> ReadReleaseVersions();
    }
}