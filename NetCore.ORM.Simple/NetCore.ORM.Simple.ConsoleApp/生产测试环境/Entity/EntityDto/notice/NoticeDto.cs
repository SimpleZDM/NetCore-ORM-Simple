using System;
using System.IO;
using MDT.VirtualSoftPlatform.Common;


namespace MDT.VirtualSoftPlatform.Entity
{
    public class NoticeDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 消息类型，info,notice,warning
        /// </summary>
        public int NoticeLevel { get; set; }
        /// <summary>
        /// 接收者ids,逗号隔开
        /// </summary>
        public string ReceiveUserId { get; set; }
    }
}
