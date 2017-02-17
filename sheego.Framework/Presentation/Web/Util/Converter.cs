using sheego.Framework.Domain.Shared;
using sheego.Framework.Domain.Shared.Locator;
using sheego.Framework.Presentation.Web.Models;
using System.Collections.Generic;
using System;
using System.Web.Mvc;

namespace sheego.Framework.Presentation.Web.Util
{
    public class Converter
    {
        public IRelease Convert(Release release)
        {
            using (var convertedRelease = DomainLocator.GetRelease())
            {
                convertedRelease.Object.Version = release.Version;
                convertedRelease.Object.Description = release.Description;
                convertedRelease.Object.DueDate = release.DueDate;
                foreach (var releaseunitCombined in release.UnitList)
                {
                    using (var releaseunit = DomainLocator.GetReleaseUnit())
                    {
                        releaseunit.Object.Name = releaseunitCombined.Name;
                        convertedRelease.Object.UnitList.Add(releaseunit.Object);
                    }
                }
                return convertedRelease.Object;
            }
        }

        public IDeployment Convert(Deployment deployment)
        {
            using (var convertedDeployment = DomainLocator.GetDeployment())
            {
                convertedDeployment.Object.Name = deployment.Name;
                convertedDeployment.Object.Description = deployment.Description;
                convertedDeployment.Object.DueDate = deployment.DueDate;
                convertedDeployment.Object.ReleaseVersion = deployment.ReleaseVersion;
                convertedDeployment.Object.Environment = deployment.Environment;
                return convertedDeployment.Object;
            }
        }

        public IDeploymentStep Convert(DeploymentStep deploymentStep)
        {
            using (var convertedDeploymentStep = DomainLocator.GetDeploymentStep())
            {
                convertedDeploymentStep.Object.Id = deploymentStep.Id;
                convertedDeploymentStep.Object.Description = deploymentStep.Description;
                convertedDeploymentStep.Object.StepState = (Domain.Shared.DeploymentStepState)deploymentStep.StepState;
                return convertedDeploymentStep.Object;
            }
        }

        public Release Convert(IRelease releaseBO)
        {
            var convertedRelease = new Release();
            convertedRelease.Version = releaseBO.Version;
            convertedRelease.Description = releaseBO.Description;
            convertedRelease.DueDate = releaseBO.DueDate;
            foreach (var releaseunitBO in releaseBO.UnitList)
            {
                var convertedReleaseUnit = new ReleaseUnit();
                convertedReleaseUnit.Name = releaseunitBO.Name;
                convertedRelease.UnitList.Add(convertedReleaseUnit);
            }
            return convertedRelease;
        }

        public Deployment Convert(IDeployment deploymentBO)
        {
            var convertedDeployment = new Deployment();
            convertedDeployment.Name = deploymentBO.Name;
            convertedDeployment.ReleaseVersion = deploymentBO.ReleaseVersion;
            convertedDeployment.Description = deploymentBO.Description;
            convertedDeployment.DueDate = deploymentBO.DueDate;
            convertedDeployment.Environment = deploymentBO.Environment; 
            return convertedDeployment;
        }

        public DeploymentStep Convert (IDeploymentStep deploymentStepBO)
        {
            var convertedDeploymentStep = new DeploymentStep();
            convertedDeploymentStep.Id = deploymentStepBO.Id;
            convertedDeploymentStep.Description = deploymentStepBO.Description;
            convertedDeploymentStep.StepState = (Models.DeploymentStepState)deploymentStepBO.StepState;
            return convertedDeploymentStep;
        }
    }
}