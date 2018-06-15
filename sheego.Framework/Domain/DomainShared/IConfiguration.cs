using System.Collections.Generic;

namespace sheego.Framework.Domain.Shared
{
    public interface IConfiguration
    {
        IList<IStakeholder> Stakeholders { set; get; }
        IList<string> DeployEnvironments { set; get; }
        IList<string> InitFolders { get; set; }
    }
}
