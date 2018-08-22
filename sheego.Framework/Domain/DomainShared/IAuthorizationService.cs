using System.Security.Principal;

namespace sheego.Framework.Domain.Shared
{
    public interface IAuthorizationService
    {
        bool IsAuthorized(IPrincipal user);
    }
}
