using System.Collections.Generic;
using System.Web.Mvc;

namespace sheego.Framework.Presentation.Web.Models
{
    public class ReleasePrepared
    {
        public Release Release { get; set; }

        public string releaseElementOption { set; get; }

        public SelectList releaseElementOptions { set; get; }

        public string newReleaseElement { set; get; }
        //public HttpPostedFileBase newReleaseElement { set; get; }
        public List<Stakeholder> StakeholdersHeadline { set; get; }
    }
}