using sheego.Framework.Domain.Shared;
using System.Collections.Generic;

namespace sheego.Framework.Domain.Impl
{
    class Configuration : IConfiguration
    {
        public IList<IStakeholder> Stakeholders { set; get; }

        public IList<string> DeployEnvironments { set; get; }

        public Configuration()
        {
            Stakeholders = new List<IStakeholder>();
            DeployEnvironments = new List<string>();
        }
    }
}
