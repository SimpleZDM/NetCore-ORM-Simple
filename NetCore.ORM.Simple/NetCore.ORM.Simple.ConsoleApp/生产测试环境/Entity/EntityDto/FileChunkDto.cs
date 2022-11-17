using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    /// <summary>
    /// 提供终止信号确保文件全部传输完成
    /// 如果不提供完成信号可能导致(后面的较小的文件先完成提交)导致提前合并文件导致文件损坏
    /// </summary>
    public class FileChunkDto
    {
        public FileChunkDto()
        {
            IsUnZip = false;
        }
        public string FileName { get; set; }
        /// <summary>
        /// 当前分片
        /// </summary>
        public int PartNumber { get; set; }
        /// <summary>
        /// 缓冲区大小
        /// </summary>
        public int ChunkSize { get; set; }
        /// <summary>
        /// 分片总数
        /// </summary>
        public int Chunks { get; set; }
        /// <summary>
        /// 文件读取起始位置
        /// </summary>
        public int Start { get; set; }
        /// <summary>
        /// 文件读取结束位置
        /// </summary>
        public int End { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public int FileSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int FileType { get; set; }
        /// <summary>
        /// code key
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 老文件路径
        /// </summary>
        public string OldPath { get; set; }
        /// <summary>
        /// 是否需要解压
        /// </summary>
        public bool IsUnZip { get; set; }
    }
}
