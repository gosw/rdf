using System.Collections.Generic;

namespace sheego.Framework.Domain.Shared
{
    public interface IVerificationService
    {
        IList<IVerificationMessage> Verify(IDeployment deployment);

        [System.Obsolete("Method for test purpose only. Use Verify(IDeployment) instead")]
        IList<IVerificationMessage> Verify(IDeployment deployment, string option);
    }
}
