using sheego.Framework.Domain.Shared;
using System;
using System.Collections.Generic;

namespace sheego.Framework.Domain.Impl
{
    class Release : IRelease
    {
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public IList<IReleaseUnit> UnitList { get; set; }
        public string Version { get; set; }

        public Release()
        {
            UnitList = new List<IReleaseUnit>();
        }
    }
}
