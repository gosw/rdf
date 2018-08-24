using System.Collections.Generic;

namespace sheego.Framework.Presentation.Web.Models
{
    public class ReleaseCombined
    {
        public Release Release { get; set; }

        public string NewReleaseUnit { set; get; }

        public List<Stakeholder> StakeholdersHeadline { set; get; }
    }
}