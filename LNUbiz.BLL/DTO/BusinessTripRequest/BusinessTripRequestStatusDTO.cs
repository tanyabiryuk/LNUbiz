using System.ComponentModel;

namespace LNUbiz.BLL.DTO.BusinessTripRequest
{
    public enum BusinessTripRequestStatusDTO
    {
        [Description("Непідтверджений")]
        Unconfirmed,

        [Description("Підтверджений")]
        Confirmed,

        [Description("На розгляді")]
        UnderConsideration
    }
}
