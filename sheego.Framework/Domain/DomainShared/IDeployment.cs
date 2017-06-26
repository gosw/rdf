using System;

namespace sheego.Framework.Domain.Shared
{
    public interface IDeployment
    {
        string Name { get; set; }
        string ReleaseVersion { set; get; }
        string Description { set; get; }
        DateTime DueDate { set; get; }
        DeploymentStatus Status { set; get; }
        string Environment { set; get; }
        //IDeploymentStep Step { set; get; } //ToDo: to complete later
    }

    public enum DeploymentStatus
    {
        Init,
        Verified,
        Active,
        Successful,
        Failed
    }
}