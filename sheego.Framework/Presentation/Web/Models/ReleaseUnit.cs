using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sheego.Framework.Presentation.Web.Models
{
    public class ReleaseUnit
    {
        public string Name { get; set; }

        [Display(Name = "Participants")]
        public List<Stakeholder> StakeholderList { get; set; }

        public ReleaseUnit()
        {
            Name = "";
            StakeholderList = new List<Stakeholder>();
        }
    }
}