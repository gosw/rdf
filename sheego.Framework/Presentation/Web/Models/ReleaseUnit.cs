using System.Collections.Generic;

namespace sheego.Framework.Presentation.Web.Models
{
    public class ReleaseUnit
    {
        public string Name { get; set; }

        public List<Stakeholder> StakeholderList { get; set; }

        public List<ReleaseElement> ReleaseElementsList { get; set; }

        public ReleaseUnit()
        {
            Name = "";
            StakeholderList = new List<Stakeholder>();
            ReleaseElementsList = new List<ReleaseElement>();
        }
    }
}