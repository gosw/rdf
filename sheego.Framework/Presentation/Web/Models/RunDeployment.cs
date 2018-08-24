using System.Collections.Generic;

namespace sheego.Framework.Presentation.Web.Models
{
    public class RunDeployment
    {
        public Deployment Deployment { get; set; }
        public List<DeploymentStep> DeploymentSteps { get; set; }

        public RunDeployment()
        {
            DeploymentSteps = new List<DeploymentStep>();
        }

    }
}