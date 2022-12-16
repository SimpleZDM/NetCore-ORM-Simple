using NetCore.ORM.Simple.Entity;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple
 * 接口名称 ConnectionEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/15 17:28:43
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple
{
    public class ConnectionEntity
    {
        public ConnectionEntity(string connectStr)
        {
            IsAutoClose = true;
            WriteReadType = eWriteOrReadType.ReadOrWrite;
            readWeight = 1;
            ConnectStr = connectStr;
        }

        public string Name { get { return name; } set { name = value; } }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectStr { get { return connectStr; } private set { connectStr = value; } }
        /// <summary>
        /// 自动释放连接
        /// </summary>
        public bool IsAutoClose { get { return isAutoClose; } set { isAutoClose = value; } }
        /// <summary>
        /// 数据库的权重
        /// </summary>
        public short ReadWeight { get { return readWeight; } set { readWeight = value; } }
        /// <summary>
        /// 配置读与写
        /// </summary>
        public eWriteOrReadType WriteReadType { get { return ewritereadType; } set { ewritereadType = value; } }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public eDBType DBType { get { return dbType; } set { dbType = value; } }
        /// <summary>
        /// 用于计算占空比
        /// </summary>
        public short Start { get { return start; } set { start = value; } }
        /// <summary>
        /// 用于计算占空比
        /// </summary>
        public short End { get { return end; } set { end = value; } }


        private eDBType dbType;
        private string name;
        private string connectStr;
        private eWriteOrReadType ewritereadType;
        private bool isAutoClose;
        private short readWeight;
        private short start;
        private short end;

    }
}
