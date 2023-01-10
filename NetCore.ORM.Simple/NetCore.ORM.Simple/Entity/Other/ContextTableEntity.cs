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
    public class ContextTableEntity
    {
        /// <summary>
        /// key-table的别名（有可能重复问题）
        /// value-table的名称
        /// </summary>
        public  Dictionary<string, TableEntity> DicTables { get { return dicTable; } private set { dicTable = value; } }
        /// <summary>
        /// table 中包含表的实际名称
        /// </summary>
        public List<string> TableNames { get { return tableNames; } private set { tableNames = value; } }


        public ContextTableEntity(params Type[] types)
        {
            if (Check.IsNull(types) ||types.Length<=CommonConst.Zero)
            {
                throw new ArgumentException(nameof(types));
            }
            DicTables=new Dictionary<string,TableEntity>();
            TableNames = new List<string>();
            for (int i = 0; i <types.Length; i++)
            {
                AddTableName(types[i],i);
            }
           
        }
        
        private Dictionary<string, TableEntity> dicTable;
        private List<string> tableNames;
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
            var newEity = new TableEntity()
                {
                    DisplayNmae= type.GetTableName(),
                    ClassType=type,
                };
            if (DicTables.ContainsKey(newEity.DisplayNmae))
            {
                var entity=DicTables[newEity.DisplayNmae];
             
                    newEity.AsName=$"{newEity.DisplayNmae}{CommonConst.Letters[entity.Count]}";
                TableNames.Add(newEity.AsName);
                entity.Count++;
            }
            else
            {
                TableNames.Add(newEity.DisplayNmae);
                newEity.AsName = newEity.DisplayNmae;
            }
            DicTables.Add(TableNames[index],newEity);
        }

        /// <summary>
        /// 获取表的真实名称
        /// </summary>
        public TableEntity GetName(int index)
        {
            if (index>=TableNames.Count())
            {
                throw new Exception("超出索引界限!");
            }
            return DicTables[TableNames[index]];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public PropertyInfo GetProperty(int index,string propName)
        {
            if (index >= TableNames.Count())
            {
                throw new Exception("超出索引界限!");
            }
            return DicTables[TableNames[index]].ClassType.GetProperty(propName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="PropName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public  string GetColumnName(int index, string PropName)
        {
            if (TableNames.Count()> index)
            {
                return DicTables[TableNames[index]].ClassType.GetProperty(PropName).GetColName();
            }
            throw new Exception("表不存在!");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public  string GetAsTableName(int index)
        {
            if (TableNames.Count() > index)
            {
                var nameEntity = GetName(index);
                return nameEntity.AsName;
            }
            throw new Exception("表不存在!");
        }

        public  void AppendTable(params Type[] types)
        {
            int index=TableNames.Count();
            foreach (var type in types)
            {
                AddTableName(type,index);
                index++;
            }
        }
    }
}
