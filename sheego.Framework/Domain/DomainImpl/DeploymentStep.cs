using sheego.Framework.Domain.Shared;

namespace sheego.Framework.Domain.Impl
{
    class DeploymentStep : IDeploymentStep
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DeploymentStepState StepState { get; set; }
        public string Comment { get; set; }
    }
}
