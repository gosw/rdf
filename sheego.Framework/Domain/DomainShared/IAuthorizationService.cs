using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace sheego.Framework.Domain.Shared
{
    public interface IAuthorizationService
    {
        bool IsAuthorized(IPrincipal user);
    }
}
