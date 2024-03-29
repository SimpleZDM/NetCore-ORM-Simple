﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 JoinTableEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/19 16:33:16
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    /// <summary>
    /// 收集配置过的表链接信息
    /// </summary>
    internal class JoinTableEntity
    {
        public JoinTableEntity()
        {
            TreeConditions = new List<TreeConditionEntity>();
            Conditions = new List<ConditionEntity>();
        }

        /// <summary>
        /// 表名称
        /// </summary>
        public string DisplayName { get { return displayName; } set { displayName = value; } }
        /// <summary>
        /// 表类型
        /// </summary>
        public eTableType TableType { get { return tableType; } set { tableType = value; } }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public eJoinType JoinType { get { return joinType; } set { joinType = value; } }

        /// <summary>
        /// 表的别称
        /// </summary>
        public string AsName { get { return asName; } set { asName = value; } }
        /// <summary>
        /// 记录一个等式
        /// </summary>
        public List<TreeConditionEntity> TreeConditions { get { return treeConditions; } set { treeConditions = value; } }
        /// <summary>
        /// 连接等式的条件
        /// </summary>
        public List<ConditionEntity> Conditions { get { return conditions; } set { conditions = value; } }
        
        private string displayName;
        private string asName;
        private eTableType tableType;
        private eJoinType joinType;
        private List<TreeConditionEntity> treeConditions;
        private List<ConditionEntity> conditions;
    }
}
