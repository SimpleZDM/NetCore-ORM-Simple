using MDT.VirtualSoftPlatform.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDT.VirtualSoftPlatform.Entity
{
    /// <summary>
    /// 通知
    /// </summary>
    [Table("noticeTable")]
    public class NoticeEntity: RecordEntity<Guid>
    {
        public NoticeEntity()
        {
            ID= Guid.NewGuid();
            IsRead = false;
        }
        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(50)]
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
        public Guid ReceiveUserId { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool IsRead { get; set; }
    }
}
