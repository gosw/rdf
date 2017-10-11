namespace sheego.Framework.Domain.Shared
{
    public interface IDeploymentStep
    {
        int Id { get; set; }
        string Description { get; set; }
        DeploymentStepState StepState { get; set; }
        string Comment { get; set; }
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
