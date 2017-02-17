using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sheego.Framework.Presentation.Web.Models
{
    public class DeploymentStep
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DeploymentStepState StepState { get; set; }
    }
}