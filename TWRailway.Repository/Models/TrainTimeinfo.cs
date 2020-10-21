namespace TWRailway.Repository.Models
{
    public class TrainTimeinfo
    {
        /// <summary>
        /// 路線
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// 車站帶碼
        /// </summary>
        public string Station { get; set; }

        /// <summary>
        /// 停靠順序
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 發車時間 HH:mm:ss
        /// </summary>
        public string DEPTime { get; set; }

        /// <summary>
        /// 到達時間
        /// </summary>
        public string ARRTime { get; set; }
    }
}