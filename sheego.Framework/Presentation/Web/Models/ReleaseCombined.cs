﻿using System.Collections.Generic;

namespace sheego.Framework.Presentation.Web.Models
{
    public class ReleaseCombined
    {
        public Release Release { get; set; }
        public string newReleaseUnit { set; get; }
        public List<Stakeholder> Stakeholders { set; get; }
    }
}