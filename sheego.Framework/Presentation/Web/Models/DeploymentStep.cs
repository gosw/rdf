namespace sheego.Framework.Presentation.Web.Models
{
    public class DeploymentStep
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DeploymentStepState StepState { get; set; }
        public string Comment { get; set; }
    }
}