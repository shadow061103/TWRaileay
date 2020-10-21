using System.Collections.Generic;
using Nest;

namespace TWRailway.Repository.Models
{
    public class Traininfo
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 車次
        /// </summary>
        [Keyword(Name = "train")]
        public string Train { get; set; }

        /// <summary>
        /// 哺集乳室車廂 Y/N
        /// </summary>
        public string BreastFeed { get; set; }

        /// <summary>
        /// 路線
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// 行李託運服務 Y/N
        /// </summary>
        public string Package { get; set; }

        /// <summary>
        /// 跨日車站代碼 空值表示無跨日
        /// </summary>
        public string OverNightStn { get; set; }

        /// <summary>
        ///方向 1順2逆
        /// </summary>
        public string LineDir { get; set; }

        /// <summary>
        /// 經由路線山/海
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// 是否有餐車 Y/N
        /// </summary>
        public string Dinning { get; set; }

        /// <summary>
        /// 是否提供訂餐(便當)服務 Y/N
        /// </summary>
        public string FoodSrv { get; set; }

        /// <summary>
        /// 是否有輪椅座 Y/N
        /// </summary>
        public string Cripple { get; set; }

        /// <summary>
        /// 列車種類
        /// </summary>
        public string CarClass { get; set; }

        /// <summary>
        /// 是否含腳踏車 Y/N
        /// </summary>
        public string Bike { get; set; }

        /// <summary>
        /// 加班車 Y/N
        /// </summary>
        public string ExtraTrain { get; set; }

        /// <summary>
        /// 是否每日行駛 Y/N
        /// </summary>
        public string Everyday { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 英文備註
        /// </summary>
        public string NoteEng { get; set; }

        /// <summary>
        /// 停靠資訊
        /// </summary>
        public IEnumerable<TrainTimeinfo> TimeInfos { get; set; }
    }
}