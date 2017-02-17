using sheego.Framework.Domain.Shared;
using System.Security.Principal;

namespace sheego.Framework.Domain.Impl
{
    class AuthorizationService : IAuthorizationService
    {
        public bool IsAuthorized(IPrincipal user)
        {
            return true;
        }
    }
}
