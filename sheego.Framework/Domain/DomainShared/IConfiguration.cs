using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sheego.Framework.Domain.Shared
{
    public interface IConfiguration
    {
        IList<IStakeholder> Stakeholders { set; get; }
    }
}
