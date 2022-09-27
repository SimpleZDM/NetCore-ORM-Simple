using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TableEntity
    {
        /// <summary>
        /// key-table的别名（有可能重复问题）
        /// value-table的名称
        /// </summary>
        public Dictionary<string,NameEntity> DicTable { get { return dicTable; } private set { dicTable = value; } }
        public string[] TableNames { get { return tableNames; } private set { tableNames = value; } }

        public TableEntity(params string[] tableNames)
        {
            if (Check.IsNull(tableNames)||tableNames.Length<=CommonConst.ZeroOrNull)
            {
                throw new ArgumentException(nameof(tableNames));
            }
            DicTable=new Dictionary<string,NameEntity>();
            TableNames=new string[tableNames.Length];
            for (int i = 0; i < tableNames.Length; i++)
            {
                AddTableName(tableNames[i],i);
            }
        }
        private Dictionary<string, NameEntity> dicTable;
        private string[] tableNames;
        private int count = 0;
        private string currentName;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        private void AddTableName(string Name,int index)
        {
            if (Check.IsNullOrEmpty(Name))
            {
                throw new Exception("table name can't null!");
            } 
            var newEity = new NameEntity()
                {
                    DisplayNmae=Name
             };
            if (DicTable.ContainsKey(Name))
            {
                var entity=DicTable[Name];
               
                TableNames[index] = $"{Name}{CommonConst.Letters[entity.Count]}";
                entity.Count++;
            }
            else
            {
                TableNames[index]=Name;
            }DicTable.Add(TableNames[index],newEity);
        }
    }
}
