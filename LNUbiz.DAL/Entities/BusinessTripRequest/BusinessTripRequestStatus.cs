using System.ComponentModel;

namespace LNUbiz.DAL.Entities
{
    public enum BusinessTripRequestStatus
    {
        [Description("Непідтверджений")]
        Unconfirmed,

        [Description("Підтверджений")]
        Confirmed,

        [Description("На розгляді")]
        UnderConsideration
    }
}
