using sheego.Framework.Domain.Shared;
using sheego.Framework.Domain.Shared.Locator;
using sheego.Framework.Presentation.Web.Models;

namespace sheego.Framework.Presentation.Web.Util
{
    public class Converter
    {
        #region Convert from Web-type to Domain-type

        public IRelease Convert(Release release)
        {
            using (var convertedRelease = DomainLocator.GetRelease())
            {
                convertedRelease.Object.Version = release.Version;
                convertedRelease.Object.Description = release.Description;
                convertedRelease.Object.DueDate = release.DueDate;
                foreach (var releaseUnit in release.UnitList)
                {
                    using (var convertedReleaseunit = DomainLocator.GetReleaseUnit())
                    {
                        convertedReleaseunit.Object.Name = releaseUnit.Name;
                        foreach(var stakeholder in releaseUnit.StakeholderList)
                        {
                            using(var convertedStakeholder = DomainLocator.GetStakeholder())
                            {
                                convertedReleaseunit.Object.StakeholderList.Add(Convert(stakeholder));
                            }
                        }
                        convertedRelease.Object.UnitList.Add(convertedReleaseunit.Object);
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

        public IConfiguration Convert(Configuration configuration)
        {
            using (var convertedConfiguration = DomainLocator.GetConfiguration())
            {
                foreach (var stakeholder in configuration.Stakeholders)
                {
                    convertedConfiguration.Object.Stakeholders.Add(Convert(stakeholder));
                }
                return convertedConfiguration.Object;
            }
        }

        public IStakeholder Convert (Stakeholder stakeholder)
        {
            using (var convertedStakeholder = DomainLocator.GetStakeholder())
            {
                convertedStakeholder.Object.Name = stakeholder.Name;
                convertedStakeholder.Object.isParticipating = stakeholder.isParticipating;
                return convertedStakeholder.Object;
            }
        }

        #endregion Convert from Web-type to Domain-type

        #region Convert from Domain-type to Web-type

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
                foreach(var stakeholderBO in releaseunitBO.StakeholderList)
                {
                    convertedReleaseUnit.StakeholderList.Add(Convert(stakeholderBO));
                }
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

        public Configuration Convert(IConfiguration configurationBO)
        {
            var convertedConfiguration = new Configuration();
            foreach(var stakeholderBO in configurationBO.Stakeholders)
            {
                convertedConfiguration.Stakeholders.Add(Convert(stakeholderBO));
            }
            return convertedConfiguration;
        }

        public Stakeholder Convert(IStakeholder stakeholderBO)
        {
            var convertedStakeholder = new Stakeholder();
            convertedStakeholder.Name = stakeholderBO.Name;
            convertedStakeholder.isParticipating = stakeholderBO.isParticipating;
            return convertedStakeholder;
        }

        #endregion Convert from Domain-type to Web-type
    }
}