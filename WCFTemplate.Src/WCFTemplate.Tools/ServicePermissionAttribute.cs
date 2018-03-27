using System;
using System.Security;
using System.Security.Permissions;

namespace WCFTemplate.Tools
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ServicePermissionAttribute : CodeAccessSecurityAttribute
    {
        private readonly bool _authenticated;

        public ServicePermissionAttribute(SecurityAction action) : base(action)
        {
            _authenticated = true;
        }

        public override IPermission CreatePermission()
        {
            return string.IsNullOrEmpty(Utils.AuthorizedGroup) ? null : new PrincipalPermission(null, Utils.AuthorizedGroup, _authenticated);
        }
    }
}
