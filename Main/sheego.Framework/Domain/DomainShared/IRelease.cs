using System;
using System.Collections.Generic;

namespace sheego.Framework.Domain.Shared
{
    public interface IRelease
    {
        string Version { get; set; }
        string Description { set; get; }
        DateTime DueDate { set; get; }
        IList<IReleaseUnit> UnitList { set; get; }
    }
}
