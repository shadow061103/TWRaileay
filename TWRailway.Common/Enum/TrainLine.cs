using System.ComponentModel;

namespace TWRailway.Common.Enum
{
    public enum TrainLine
    {
        [Description("不經山海線")]
        None = 0,

        [Description("山線")]
        Mountain = 1,

        [Description("海線")]
        Sea = 2,

        [Description("成追線")]
        Chenzhui = 3,

        [Description("山海")]
        MountainandSea = 4
    }
}