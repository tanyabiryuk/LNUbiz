
using LNUbiz.BLL.Interfaces.Resources;
using LNUbiz.Resources;
using Microsoft.Extensions.Localization;

namespace LNUbiz.BLL.Services.Resources
{
    public class Resources: IResources
    {
        public Resources(IStringLocalizer<AuthenticationErrors> resourceForErrors)
        {
            ResourceForErrors = resourceForErrors;
        }

        public IStringLocalizer<AuthenticationErrors> ResourceForErrors { get; }
    }
}
