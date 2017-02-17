using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sheego.Framework.Presentation.Web.Models
{
    public enum DeploymentStepState
    {
        Init,
        Active,
        Successful,
        Skipped,
        Failed
    }
}