using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class PlayerLoginStatusModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public int MissionRole { get; set; }
        /// <summary>
        /// 准备状态
        /// </summary>
        public bool IsReady { get; set; }
        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline { get; set; }
        /// <summary>
        /// 头像
        /// </summary>

        public string ImagePath { get; set; }

        public string MissionDetailId { get; set; }

    }
}
