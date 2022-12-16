using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NetCore.ORM.Simple.Common;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 TableEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/23 11:55:20
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    /// <summary>
    /// 收集链接查询中涉及到的表
    /// </summary>
    public class TableEntity
    {
        /// <summary>
        /// key-table的别名（有可能重复问题）
        /// value-table的名称
        /// </summary>
        public  Dictionary<string,NameEntity> DicTable { get { return dicTable; } private set { dicTable = value; } }
        /// <summary>
        /// table 中包含表的实际名称
        /// </summary>
        public string[] TableNames { get { return tableNames; } private set { tableNames = value; } }


        public TableEntity(params Type[] types)
        {
            if (Check.IsNull(types) ||types.Length<=CommonConst.Zero)
            {
                throw new ArgumentException(nameof(types));
            }
            DicTable=new Dictionary<string,NameEntity>();
            TableNames=new string[types.Length];
            for (int i = 0; i <types.Length; i++)
            {
                AddTableName(types[i],i);
            }
           
        }
        private Dictionary<string, NameEntity> dicTable;
        private string[] tableNames;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        private void AddTableName(Type type,int index)
        {
            if (Check.IsNull(type))
            {
                throw new Exception("table name can't null!");
            } 
            var newEity = new NameEntity()
                {
                    DisplayNmae= type.GetTableName(),
                    ClassType=type,
                };
            if (DicTable.ContainsKey(newEity.DisplayNmae))
            {
                var entity=DicTable[newEity.DisplayNmae];
             
                    newEity.AsName=$"{newEity.DisplayNmae}{CommonConst.Letters[entity.Count]}";
                    TableNames[index] = newEity.AsName;
                entity.Count++;
            }
            else
            {
                TableNames[index]=newEity.DisplayNmae;
                newEity.AsName = newEity.DisplayNmae;
            }
            DicTable.Add(TableNames[index],newEity);
        }

        /// <summary>
        /// 获取表的真实名称
        /// </summary>
        public NameEntity GetTableName(int index)
        {
            if (index>=TableNames.Length)
            {
                throw new Exception("超出索引界限!");
            }
            return DicTable[TableNames[index]];
        }

        public PropertyInfo GetProperty(int index,string propName)
        {
            if (index >= TableNames.Length)
            {
                throw new Exception("超出索引界限!");
            }
            return DicTable[TableNames[index]].ClassType.GetProperty(propName);
        }
        public  string GetColumnName(int index, string PropName)
        {
            if (TableNames.Length > index)
            {
                return DicTable[TableNames[index]].ClassType.GetProperty(PropName).GetColName();
            }
            throw new Exception("表不存在!");
        }

        public  string GetAsTableName(int index)
        {
            if (TableNames.Length > index)
            {
                var nameEntity = GetTableName(index);
                return nameEntity.AsName;
            }
            throw new Exception("表不存在!");
        }
    }
}
