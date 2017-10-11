using BO = sheego.Framework.Domain.Shared;
using WEB = sheego.Framework.Presentation.Web.Models;
using sheego.Framework.Domain.Shared;
using sheego.Framework.Domain.Shared.Locator;
using sheego.Framework.Presentation.Web.Models;
using System.Collections.Generic;

namespace sheego.Framework.Presentation.Web.Util
{
    public class Converter
    {
        #region Release

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
                foreach (var stakeholderBO in releaseunitBO.StakeholderList)
                {
                    convertedReleaseUnit.StakeholderList.Add(Convert(stakeholderBO));
                }
                convertedRelease.UnitList.Add(convertedReleaseUnit);
            }
            return convertedRelease;
        }

        #endregion Release

        #region Deployment

        public IDeployment Convert(Deployment deployment)
        {
            using (var convertedDeployment = DomainLocator.GetDeployment())
            {
                convertedDeployment.Object.Name = deployment.Name;
                convertedDeployment.Object.Description = deployment.Description;
                convertedDeployment.Object.DueDate = deployment.DueDate;
                convertedDeployment.Object.ReleaseVersion = deployment.ReleaseVersion;
                convertedDeployment.Object.Environment = deployment.Environment;
                convertedDeployment.Object.Status = (BO.DeploymentStatus)deployment.Status;
                return convertedDeployment.Object;
            }
        }

        public Deployment Convert(IDeployment deploymentBO)
        {
            var convertedDeployment = new Deployment();
            convertedDeployment.Name = deploymentBO.Name;
            convertedDeployment.Description = deploymentBO.Description;
            convertedDeployment.DueDate = deploymentBO.DueDate;
            convertedDeployment.ReleaseVersion = deploymentBO.ReleaseVersion;
            convertedDeployment.Environment = deploymentBO.Environment;
            convertedDeployment.Status = (WEB.DeploymentStatus)deploymentBO.Status;
            return convertedDeployment;
        }

        #endregion Deployment

        #region DeploymentStep

        public IDeploymentStep Convert(DeploymentStep deploymentStep)
        {
            using (var convertedDeploymentStep = DomainLocator.GetDeploymentStep())
            {
                convertedDeploymentStep.Object.Id = deploymentStep.Id;
                convertedDeploymentStep.Object.Description = deploymentStep.Description;
                convertedDeploymentStep.Object.StepState = (BO.DeploymentStepState)deploymentStep.StepState;
                convertedDeploymentStep.Object.Comment = deploymentStep.Comment;
                return convertedDeploymentStep.Object;
            }
        }

        public DeploymentStep Convert(IDeploymentStep deploymentStepBO)
        {
            var convertedDeploymentStep = new DeploymentStep();
            convertedDeploymentStep.Id = deploymentStepBO.Id;
            convertedDeploymentStep.Description = deploymentStepBO.Description;
            convertedDeploymentStep.StepState = (WEB.DeploymentStepState)deploymentStepBO.StepState;
            convertedDeploymentStep.Comment = deploymentStepBO.Comment;
            return convertedDeploymentStep;
        }

        #endregion DeploymentStep

        #region Configuration

        public IConfiguration Convert(Configuration configuration)
        {
            using (var convertedConfiguration = DomainLocator.GetConfiguration())
            {
                foreach (var stakeholder in configuration.Stakeholders)
                {
                    convertedConfiguration.Object.Stakeholders.Add(Convert(stakeholder));
                }

                foreach (var environment in configuration.DeployEnvironments)
                {
                    convertedConfiguration.Object.DeployEnvironments.Add(environment);
                }
                return convertedConfiguration.Object;
            }
        }

        public Configuration Convert(IConfiguration configurationBO)
        {
            var convertedConfiguration = new Configuration();
            foreach (var stakeholderBO in configurationBO.Stakeholders)
            {
                convertedConfiguration.Stakeholders.Add(Convert(stakeholderBO));
            }

            foreach (var environment in configurationBO.DeployEnvironments)
            {
                convertedConfiguration.DeployEnvironments.Add(environment);
            }
            return convertedConfiguration;
        }

        #endregion Configuration

        #region Stakeholder

        public IStakeholder Convert(Stakeholder stakeholder)
        {
            using (var convertedStakeholder = DomainLocator.GetStakeholder())
            {
                convertedStakeholder.Object.Name = stakeholder.Name;
                convertedStakeholder.Object.isParticipating = stakeholder.isParticipating;
                return convertedStakeholder.Object;
            }
        }

        public Stakeholder Convert(IStakeholder stakeholderBO)
        {
            var convertedStakeholder = new Stakeholder();
            convertedStakeholder.Name = stakeholderBO.Name;
            convertedStakeholder.isParticipating = stakeholderBO.isParticipating;
            return convertedStakeholder;
        }

        #endregion Stakeholder

        #region VerificationMessage

        public IVerificationMessage Convert(VerificationMessage verificationMessage)
        {
            using (var convertedMessage = DomainLocator.GetVerificationMessage())
            {
                convertedMessage.Object.MessageContent = verificationMessage.MessageContent;
                convertedMessage.Object.Status = (BO.VerificationStatus)verificationMessage.Status;
                return convertedMessage.Object;
            }
        }

        public VerificationMessage Convert(IVerificationMessage verificationMessageBO)
        {
            var verificationMessage = new VerificationMessage();
            verificationMessage.MessageContent = verificationMessageBO.MessageContent;
            verificationMessage.Status =  (WEB.VerificationStatus)verificationMessageBO.Status;
            return verificationMessage;
        }

        #endregion VerificationMessage

        public string ConvertListToString<T>(IList<T> list)
        {
            return string.Join("|", list);
        }
    }
}