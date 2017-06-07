using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sheego.Framework.Presentation.Web.Models
{
    public class ReleasePrepared
    {
        public Release Release { get; set; }
        public HttpPostedFileBase newReleaseElement { set; get; }
    }
}