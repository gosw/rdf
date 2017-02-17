using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sheego.Framework.Domain.Shared
{
    public interface IDeploymentStep
    {
        string Id { get; set; }
        string Description { get; set; }
        DeploymentStepState StepState { get; set; }
    }

    public enum DeploymentStepState
    {
        Init,
        Active,
        Successful,
        Skipped,
        Failed
    }
}
