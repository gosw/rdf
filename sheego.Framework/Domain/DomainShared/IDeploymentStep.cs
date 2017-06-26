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
