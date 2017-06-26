using System;
using sheego.Framework.Domain.Shared;

namespace sheego.Framework.Domain.Impl
{
    class Deployment : IDeployment
    {
        public string Name { get; set; }

        public string ReleaseVersion { set; get; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public DeploymentStatus Status { set; get; }

        public string Environment { set; get; }

        //public DeploymentStep Step { set; get; } //ToDo: to complete later
    }
}