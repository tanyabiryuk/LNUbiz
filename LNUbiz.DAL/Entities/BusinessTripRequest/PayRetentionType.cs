using System.ComponentModel;

namespace LNUbiz.DAL.Entities
{
    public enum PayRetentionType
    {
        [Description("зі збереженням середньої зарплати за основним місце праці")]
        ByFullTimePosition,

        [Description("зі збереженням середньої зарплати за основним місцем праці та за сумісництвом")]
        ByFullAndPartTimePositions,

        [Description("без збереження заробітної плати (тривалість відрядження більше 10-ти днів)")]
        NoPayRetention
    }
}
