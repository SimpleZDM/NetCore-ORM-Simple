using System;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class NoticeView
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 消息类型，info,notice,warning
        /// </summary>
        public string NoticeLevel { get; set; }
     
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool IsRead { get; set; }
        /// <summary>
        ///接收者ID
        /// </summary>
        public Guid ? ReceiveUserId { get; set; }
        /// <summary>
        /// 创建者id
        /// </summary>
        public Guid ? CreateUserID { get; set; }
    }
}
