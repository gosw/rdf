using System;
using System.Collections.Generic;

namespace sheego.Framework.Domain.Shared
{
    public interface IReleaseUnit
    {
        String Name { set; get; }
        IList<IStakeholder> StakeholderList { get; set; }
        IList<IReleaseElement> ReleaseElementList { get; set; }
    }
}
