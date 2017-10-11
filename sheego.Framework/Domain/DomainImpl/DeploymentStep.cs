﻿using sheego.Framework.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
