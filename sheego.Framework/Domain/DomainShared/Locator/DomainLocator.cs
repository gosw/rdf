using sheego.Framework.Locator.Internal;
using sheego.Framework.Locator.Shared;

namespace sheego.Framework.Domain.Shared.Locator
{
    public class DomainLocator
    {
        public static ILocatedObject<IAuthorizationService> GetAuthorizationService()
        {
            return ContainerHolder.Resolve<IAuthorizationService>();
        }

        public static ILocatedObject<IRelease> GetRelease()
        {
            return ContainerHolder.Resolve<IRelease>();
        }

        public static ILocatedObject<IReleaseUnit> GetReleaseUnit()
        {
            return ContainerHolder.Resolve<IReleaseUnit>();
        }

        public static ILocatedObject<IRepositoryService> GetRepositoryService()
        {
            return ContainerHolder.Resolve<IRepositoryService>();
        }

        public static ILocatedObject<IDeployment> GetDeployment()
        {
            return ContainerHolder.Resolve<IDeployment>();
        }

        public static ILocatedObject<IDeploymentStep> GetDeploymentStep()
        {
            return ContainerHolder.Resolve<IDeploymentStep>();
        }

        public static ILocatedObject<IConfiguration> GetConfiguration()
        {
            return ContainerHolder.Resolve<IConfiguration>();
        }

        public static ILocatedObject<IStakeholder> GetStakeholder()
        {
            return ContainerHolder.Resolve<IStakeholder>();
        }

        public static ILocatedObject<IVerificationService> GetVerificationService()
        {
            return ContainerHolder.Resolve<IVerificationService>();
        }

        public static ILocatedObject<IVerificationMessage> GetVerificationMessage()
        {
            return ContainerHolder.Resolve<IVerificationMessage>();
        }
    }
}
