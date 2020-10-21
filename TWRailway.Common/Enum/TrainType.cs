using System.ComponentModel;

namespace TWRailway.Common.Enum
{
    public enum TrainType
    {
        [Description("定期")]
        Regular = 1,

        [Description("加開")]
        Additional = 2,

        [Description("郵輪")]
        Ship = 3,

        [Description("專列")]
        Special = 4
    }
}