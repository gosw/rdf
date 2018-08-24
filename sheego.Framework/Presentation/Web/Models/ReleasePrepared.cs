using System.Collections.Generic;
using System.Web.Mvc;

namespace sheego.Framework.Presentation.Web.Models
{
    public class ReleasePrepared
    {
        public Release Release { get; set; }

        public string ReleaseElementOption { set; get; }

        public SelectList ReleaseElementOptions { set; get; }

        public string NewReleaseElement { set; get; }
        //public HttpPostedFileBase newReleaseElement { set; get; }
        public List<Stakeholder> StakeholdersHeadline { set; get; }
    }
}