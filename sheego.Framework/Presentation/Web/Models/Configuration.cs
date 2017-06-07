using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sheego.Framework.Presentation.Web.Models
{
    public class Configuration
    {
        public List<Stakeholder> Stakeholders { set; get; }

        public Configuration()
        {
            Stakeholders = new List<Stakeholder>();
        }
    }
}