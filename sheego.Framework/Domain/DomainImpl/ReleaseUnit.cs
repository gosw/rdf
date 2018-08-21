using sheego.Framework.Domain.Shared;
using System.Collections.Generic;
using System;

namespace sheego.Framework.Domain.Impl
{
    class ReleaseUnit : IReleaseUnit
    {
        public string Name { get; set; }

        public IList<IStakeholder> StakeholderList { get; set; }

        public IList<IReleaseElement> ReleaseElementList { get; set; }

        public ReleaseUnit()
        {
            StakeholderList = new List<IStakeholder>();
            ReleaseElementList = new List<IReleaseElement>();
        }
    }
}
