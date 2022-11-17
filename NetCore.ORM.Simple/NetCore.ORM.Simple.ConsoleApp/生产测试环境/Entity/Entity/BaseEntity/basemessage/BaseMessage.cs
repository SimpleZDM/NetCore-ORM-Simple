using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Common
{
    /**
    * 命名空间: MDT.VirtualSoftPlatform.Common
    *
    * 功 能： 返回信息实体
    * 类 名： BaseMessage
    *
    * Ver 变更日期 负责人 变更内容
    * ───────────────────────────────────
    * V0.01 2022/1/22 13:51:34 
    *
    *┌──────────────────────────────────┐
    *│　***************************************************．　           │
    *│　**********************　　　　　　　　　　　　　　                │
    *└──────────────────────────────────┘
    **/
    public class BaseMessage
    {
        public BaseMessage(string Msg, int Code = 200, bool Status = true)
        {
            code = Code;
            status = Status;
            msg = Msg;
        }
        public int code { get; set; }
        public bool status { get; set; }
        public string msg { get; set; }
    }
}
