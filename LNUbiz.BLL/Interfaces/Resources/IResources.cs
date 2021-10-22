using LNUbiz.Resources;
using Microsoft.Extensions.Localization;

namespace LNUbiz.BLL.Interfaces.Resources
{
    public interface IResources
    {
        IStringLocalizer<AuthenticationErrors> ResourceForErrors { get; }
    }
}
