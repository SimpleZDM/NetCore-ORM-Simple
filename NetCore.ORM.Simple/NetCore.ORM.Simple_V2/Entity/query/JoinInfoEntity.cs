using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 JoinInfoEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/19 11:38:56
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    /// <summary>
    /// 表链接实体配置
    /// </summary>
    public class JoinInfoEntity
    {
        public JoinMapEntity[] JoinMaps { get { return _joinMaps; } set { _joinMaps=value; } }

        public JoinInfoEntity(eJoinType joinType,bool condition)
        {
            JoinMaps = new JoinMapEntity[1];
            JoinMaps[0] = new JoinMapEntity(joinType,condition);
        }
        public JoinInfoEntity(eJoinType joinType, bool condition,eJoinType joinType1,bool condition1)
        {
            JoinMaps = new JoinMapEntity[2];
            JoinMaps[0] = new JoinMapEntity(joinType, condition);
            JoinMaps[1] = new JoinMapEntity(joinType1, condition1);
        }
        public JoinInfoEntity(eJoinType joinType, bool condition, eJoinType joinType1, 
            bool condition1, eJoinType joinType2, bool condition2)
        {
            JoinMaps = new JoinMapEntity[3];
            JoinMaps[0] = new JoinMapEntity(joinType, condition);
            JoinMaps[1] = new JoinMapEntity(joinType1, condition1);
            JoinMaps[2] = new JoinMapEntity(joinType2, condition2);
        }
        public JoinInfoEntity(eJoinType joinType, bool condition,
            eJoinType joinType1, bool condition1,
            eJoinType joinType2, bool condition2
            ,eJoinType joinType3, bool condition3
            )
        {
            JoinMaps = new JoinMapEntity[4];
            JoinMaps[0] = new JoinMapEntity(joinType, condition);
            JoinMaps[1] = new JoinMapEntity(joinType1, condition1);
            JoinMaps[2] = new JoinMapEntity(joinType2, condition2);
            JoinMaps[3] = new JoinMapEntity(joinType3, condition3);
        }

        public JoinInfoEntity(eJoinType joinType, bool condition,
           eJoinType joinType1, bool condition1,
           eJoinType joinType2, bool condition2
           , eJoinType joinType3, bool condition3
           , eJoinType joinType4, bool condition4
           )
        {
            JoinMaps = new JoinMapEntity[5];
            JoinMaps[0] = new JoinMapEntity(joinType, condition);
            JoinMaps[1] = new JoinMapEntity(joinType1, condition1);
            JoinMaps[2] = new JoinMapEntity(joinType2, condition2);
            JoinMaps[3] = new JoinMapEntity(joinType3, condition3);
            JoinMaps[4] = new JoinMapEntity(joinType4, condition4);
        }
        public JoinInfoEntity(eJoinType joinType, bool condition,
          eJoinType joinType1, bool condition1,
          eJoinType joinType2, bool condition2
          , eJoinType joinType3, bool condition3
          , eJoinType joinType4, bool condition4
          , eJoinType joinType5, bool condition5
          )
        {
            JoinMaps = new JoinMapEntity[6];
            JoinMaps[0] = new JoinMapEntity(joinType, condition);
            JoinMaps[1] = new JoinMapEntity(joinType1, condition1);
            JoinMaps[2] = new JoinMapEntity(joinType2, condition2);
            JoinMaps[3] = new JoinMapEntity(joinType3, condition3);
            JoinMaps[4] = new JoinMapEntity(joinType4, condition4);
            JoinMaps[5] = new JoinMapEntity(joinType5, condition5);
        }
        public JoinInfoEntity(eJoinType joinType, bool condition,
         eJoinType joinType1, bool condition1,
         eJoinType joinType2, bool condition2
         , eJoinType joinType3, bool condition3
         , eJoinType joinType4, bool condition4
         , eJoinType joinType5, bool condition5
         , eJoinType joinType6, bool condition6
         )
        {
            JoinMaps = new JoinMapEntity[7];
            JoinMaps[0] = new JoinMapEntity(joinType, condition);
            JoinMaps[1] = new JoinMapEntity(joinType1, condition1);
            JoinMaps[2] = new JoinMapEntity(joinType2, condition2);
            JoinMaps[3] = new JoinMapEntity(joinType3, condition3);
            JoinMaps[4] = new JoinMapEntity(joinType4, condition4);
            JoinMaps[5] = new JoinMapEntity(joinType5, condition5);
            JoinMaps[6] = new JoinMapEntity(joinType6, condition6);
        }

        public JoinInfoEntity(eJoinType joinType, bool condition,
        eJoinType joinType1, bool condition1,
        eJoinType joinType2, bool condition2
        , eJoinType joinType3, bool condition3
        , eJoinType joinType4, bool condition4
        , eJoinType joinType5, bool condition5
        , eJoinType joinType6, bool condition6
        , eJoinType joinType7, bool condition7
        )
        {
            JoinMaps = new JoinMapEntity[8];
            JoinMaps[0] = new JoinMapEntity(joinType, condition);
            JoinMaps[1] = new JoinMapEntity(joinType1, condition1);
            JoinMaps[2] = new JoinMapEntity(joinType2, condition2);
            JoinMaps[3] = new JoinMapEntity(joinType3, condition3);
            JoinMaps[4] = new JoinMapEntity(joinType4, condition4);
            JoinMaps[5] = new JoinMapEntity(joinType5, condition5);
            JoinMaps[6] = new JoinMapEntity(joinType6, condition6);
            JoinMaps[7] = new JoinMapEntity(joinType7, condition7);
        }

        public JoinInfoEntity(eJoinType joinType, bool condition,
       eJoinType joinType1, bool condition1,
       eJoinType joinType2, bool condition2
       , eJoinType joinType3, bool condition3
       , eJoinType joinType4, bool condition4
       , eJoinType joinType5, bool condition5
       , eJoinType joinType6, bool condition6
       , eJoinType joinType7, bool condition7
       , eJoinType joinType8, bool condition8
       )
        {
            JoinMaps = new JoinMapEntity[9];
            JoinMaps[0] = new JoinMapEntity(joinType, condition);
            JoinMaps[1] = new JoinMapEntity(joinType1, condition1);
            JoinMaps[2] = new JoinMapEntity(joinType2, condition2);
            JoinMaps[3] = new JoinMapEntity(joinType3, condition3);
            JoinMaps[4] = new JoinMapEntity(joinType4, condition4);
            JoinMaps[5] = new JoinMapEntity(joinType5, condition5);
            JoinMaps[6] = new JoinMapEntity(joinType6, condition6);
            JoinMaps[7] = new JoinMapEntity(joinType7, condition7);
            JoinMaps[8] = new JoinMapEntity(joinType8, condition8);
        }

        public JoinInfoEntity(eJoinType joinType, bool condition,
     eJoinType joinType1, bool condition1,
     eJoinType joinType2, bool condition2
     , eJoinType joinType3, bool condition3
     , eJoinType joinType4, bool condition4
     , eJoinType joinType5, bool condition5
     , eJoinType joinType6, bool condition6
     , eJoinType joinType7, bool condition7
     , eJoinType joinType8, bool condition8
     , eJoinType joinType9, bool condition9
     )
        {
            JoinMaps = new JoinMapEntity[10];
            JoinMaps[0] = new JoinMapEntity(joinType, condition);
            JoinMaps[1] = new JoinMapEntity(joinType1, condition1);
            JoinMaps[2] = new JoinMapEntity(joinType2, condition2);
            JoinMaps[3] = new JoinMapEntity(joinType3, condition3);
            JoinMaps[4] = new JoinMapEntity(joinType4, condition4);
            JoinMaps[5] = new JoinMapEntity(joinType5, condition5);
            JoinMaps[6] = new JoinMapEntity(joinType6, condition6);
            JoinMaps[7] = new JoinMapEntity(joinType7, condition7);
            JoinMaps[8] = new JoinMapEntity(joinType8, condition8);
            JoinMaps[9] = new JoinMapEntity(joinType9, condition9);
        }

        public JoinInfoEntity(eJoinType joinType, bool condition,
    eJoinType joinType1, bool condition1,
    eJoinType joinType2, bool condition2
    , eJoinType joinType3, bool condition3
    , eJoinType joinType4, bool condition4
    , eJoinType joinType5, bool condition5
    , eJoinType joinType6, bool condition6
    , eJoinType joinType7, bool condition7
    , eJoinType joinType8, bool condition8
    , eJoinType joinType9, bool condition9
    , eJoinType joinType10, bool condition10
    )
        {
            JoinMaps = new JoinMapEntity[11];
            JoinMaps[0] = new JoinMapEntity(joinType, condition);
            JoinMaps[1] = new JoinMapEntity(joinType1, condition1);
            JoinMaps[2] = new JoinMapEntity(joinType2, condition2);
            JoinMaps[3] = new JoinMapEntity(joinType3, condition3);
            JoinMaps[4] = new JoinMapEntity(joinType4, condition4);
            JoinMaps[5] = new JoinMapEntity(joinType5, condition5);
            JoinMaps[6] = new JoinMapEntity(joinType6, condition6);
            JoinMaps[7] = new JoinMapEntity(joinType7, condition7);
            JoinMaps[8] = new JoinMapEntity(joinType8, condition8);
            JoinMaps[9] = new JoinMapEntity(joinType9, condition9);
            JoinMaps[10] = new JoinMapEntity(joinType10, condition10);
        }

        public JoinInfoEntity(eJoinType joinType, bool condition,
   eJoinType joinType1, bool condition1,
   eJoinType joinType2, bool condition2
   , eJoinType joinType3, bool condition3
   , eJoinType joinType4, bool condition4
   , eJoinType joinType5, bool condition5
   , eJoinType joinType6, bool condition6
   , eJoinType joinType7, bool condition7
   , eJoinType joinType8, bool condition8
   , eJoinType joinType9, bool condition9
   , eJoinType joinType10, bool condition10
   , eJoinType joinType11, bool condition11
   )
        {
            JoinMaps = new JoinMapEntity[12];
            JoinMaps[0] = new JoinMapEntity(joinType, condition);
            JoinMaps[1] = new JoinMapEntity(joinType1, condition1);
            JoinMaps[2] = new JoinMapEntity(joinType2, condition2);
            JoinMaps[3] = new JoinMapEntity(joinType3, condition3);
            JoinMaps[4] = new JoinMapEntity(joinType4, condition4);
            JoinMaps[5] = new JoinMapEntity(joinType5, condition5);
            JoinMaps[6] = new JoinMapEntity(joinType6, condition6);
            JoinMaps[7] = new JoinMapEntity(joinType7, condition7);
            JoinMaps[8] = new JoinMapEntity(joinType8, condition8);
            JoinMaps[9] = new JoinMapEntity(joinType9, condition9);
            JoinMaps[10] = new JoinMapEntity(joinType10, condition10);
            JoinMaps[11] = new JoinMapEntity(joinType11, condition11);
        }

        private JoinMapEntity[] _joinMaps;
    }
}
