using System.Collections.Generic;

namespace TWRailway.Repository.Models
{
    public class TRATrainInfos
    {
        /// <summary>
        /// 車次資料
        /// </summary>
        public IEnumerable<Traininfo> TrainInfos { get; set; }

        /// <summary>
        /// 更新時間
        /// </summary>
        public TRAUpdatetime updateTime { get; set; }
    }
}