using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 MethodEntity
 * 开发人员：11920
 * 创建时间：2022/12/13 17:51:11
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    public class MethodEntity
    {
        public MethodEntity() 
        {
            TreeConditions = new List<TreeConditionEntity>();
            Conditions = new List<ConditionEntity>();
            Parameters = new List<ConditionEntity>();
            
        }
        /// <summary>
        /// 方法名称集合
        /// </summary>
        public string Name { get { return name; } set { name = value; } }

        /// <summary>
        /// 方法类型
        /// </summary>
        public eMethodType MethodType { get { return methodType; } set { methodType = value; } }
        /// <summary>
        /// 条件表达式集合
        /// </summary>
        public List<TreeConditionEntity> TreeConditions { get { return treeConditions; } set { treeConditions = value; } }
        /// <summary>
        ///等式中间连接符 数量等于等式数量减去一
        /// </summary>
        public List<ConditionEntity> Conditions { get { return conditions; } set { conditions = value; } }

        /// <summary>
        /// 方法中的参数
        /// </summary>
        /// 
        public List<ConditionEntity>Parameters { get { return parameters; } set { parameters = value; } }
        public bool IsNot { get { return isNot; } set { isNot = value; } }

        public MethodInfo Method { get; set; }


        private string name;
        private eMethodType methodType;
        private List<TreeConditionEntity> treeConditions;
        private List<ConditionEntity> conditions;
        private List<ConditionEntity> parameters;
        public bool isNot;
    }
}
