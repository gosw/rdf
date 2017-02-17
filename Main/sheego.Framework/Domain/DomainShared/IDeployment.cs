using sheego.Framework.Domain.Shared;
using System;

namespace sheego.Framework.Domain.Shared
{
    public interface IDeployment
    {
        string Name { get; set; }
        string ReleaseVersion { set; get; }
        string Description { set; get; }
        DateTime DueDate { set; get; }
        string Environment { set; get; }
        //IDeploymentStep Step { set; get; } //ToDo: to complete later
    }
}