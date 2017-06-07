using sheego.Framework.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sheego.Framework.Domain.Impl
{
    class Configuration : IConfiguration
    {
        public IList<IStakeholder> Stakeholders { set; get; }

        public Configuration()
        {
            Stakeholders = new List<IStakeholder>();
        }
    }
}
