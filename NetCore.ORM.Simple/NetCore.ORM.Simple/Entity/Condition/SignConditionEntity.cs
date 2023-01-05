using NetCore.ORM.Simple.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Text;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple_V2.Entity.Condition
 * 接口名称 SignConditionEntity
 * 开发人员：11920
 * 创建时间：2022/12/29 13:49:43
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity.Condition
{
    public class SignConditionEntity : BaseCondition, ICondition
    {

        public eSignType SignType { get { return signType; } set { signType = value; } }
        public SignConditionEntity(eSignType signType) :base(eConditionType.Sign)
        {
            SignType=signType;
        }
        public string GetConditionName()
        {
            if (MysqlConst.cStrSign.Length<=(int)SignType)
            {
                throw new ArgumentOutOfRangeException(nameof(SignType));
            }
            return MysqlConst.cStrSign[(int)SignType];
        }

        private eSignType signType;
    }
}
