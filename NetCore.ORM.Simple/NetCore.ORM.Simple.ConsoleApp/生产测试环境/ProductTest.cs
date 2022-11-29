using MDT.VirtualSoftPlatform.Common;
using MDT.VirtualSoftPlatform.Entity;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.ConsoleApp.生产测试环境
 * 接口名称 ProductTest
 * 开发人员：-nhy
 * 创建时间：2022/11/16 17:41:06
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.ConsoleApp
{
    public class ProductTest
    {
        ISimpleClient client;
        public ProductTest()
        {
            string strLocalhost = "server=localhost;database=virtualsoftplatformdb;user=root;pwd=123456;Allow Zero Datetime=true;Convert Zero Datetime=True;";
            string StrService = "server=49.233.33.36;database=virtualsoftplatformdb;user=root;pwd=[Txy*!14@msql*^];SSL Mode=None";
            client = new SimpleClient(
          new DataBaseConfiguration(false,
          new ConnectionEntity(strLocalhost)
          {
              IsAutoClose = true,
              DBType = eDBType.Mysql,
              Name = "test1",
              ReadWeight = 5,
              WriteReadType = eWriteOrReadType.ReadOrWrite
          }));

            client.SetAOPLog((sql, Params) =>
            {
                Console.WriteLine(sql);
            });
            //client.SetAttr(typeof(MyTableAttrbute), typeof(MyColumnAttrbute));
            // var data=client.Queryable<MatchLog>().Take(10).ToList();
        }
        public void StartTest(MissionDetailParameter Params)
        {
            #region
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.WriteLine($"Join start{watch.ElapsedMilliseconds}");
            var query = client.Queryable<MDT.VirtualSoftPlatform.Entity.MissionDetailEntity, StudentEntity, MDT.VirtualSoftPlatform.Entity.UserEntity, MissionEntity, ProductDeviceEntity>(
              (missionDetail, student, userStudent, mission, productDevice) =>
              new NetCore.ORM.Simple.Entity.JoinInfoEntity(
              NetCore.ORM.Simple.Entity.eJoinType.Inner, student.UserID.Equals(missionDetail.UserId),
              NetCore.ORM.Simple.Entity.eJoinType.Inner, userStudent.ID.Equals(missionDetail.UserId),
              NetCore.ORM.Simple.Entity.eJoinType.Inner, mission.ID.Equals(missionDetail.MissionId),
              NetCore.ORM.Simple.Entity.eJoinType.Left, productDevice.ID.Equals(missionDetail.ProductDeviceId)
              ));
            query.Where(missionDetail=>missionDetail.StatusId==(int)eMissionStatus.Complete);

            Console.WriteLine($"Join end{watch.ElapsedMilliseconds}");
            #endregion
            if (Params.ClassID != Guid.Empty)
            {
                query.Where((missionDetail, student, userStudent, mission
                   , productDevice) => student.ClassID.Equals(Params.ClassID));
            }
            if (Params.Status >= 0)
            {
                query.Where(missionDetail => missionDetail.StatusId.Equals(Params.Status));
            }
            if (Params.ProductID != Guid.Empty)
            {
                query.Where(missionDetail => missionDetail.ProductId.Equals(Params.ProductID));
                

            }
            Console.WriteLine($"any start{watch.ElapsedMilliseconds}");
            if ( query.Any() == false)
            {
                return ;//Ok("无数据!");
            }
            Console.WriteLine($"开始读取!{watch.ElapsedMilliseconds}");
            var data1 = query.Select(
                 (missionDetail, student, userStudent, mission, productDevice) =>
                 new MissionDetailView()
                 {
                     UserId = missionDetail.UserId,
                     Score = missionDetail.Score,
                     StartTime = missionDetail.StartTime,
                     EndTime = missionDetail.EndTime,
                     // RoleName = missionRole.RowName ?? "角色未分配",
                     RoleId = missionDetail.MissionRole,
                     Status = missionDetail.StatusId,
                     // StatusName = status.RowName ?? "",
                     MissionId = mission.ID,
                     StudentName = userStudent.UserName,
                     MissionName = mission.MissionName,
                     ProductDeviceId =  productDevice.ID,
                     ProductDeviceName = productDevice.DisplayName,
                     // MissionModeName = missionMode.RowName ?? "",
                     MissionMode = mission.MissionMode,
                     GroupMode = mission.GroupMode,
                     //  GroupModeName = groupMode.RowName ?? "",
                     Id = missionDetail.ID,
                     //TeacherName = userTeacher.UserName,
                     GroupID = missionDetail.GroupID,
                     ClassID = student.ClassID,
                     Avatar = userStudent.Avatar,
                     //TimeLength = SqlFunc.DateDiff(DateType.Minute, missionDetail.StartTime, missionDetail.EndTime)
                 }
                ).GroupBy(mv=>new { mv.GroupID,mv.RoleId,mv.UserId}).Select(mv=>new GroupEntity
                {
                   Count=mv.Count(),
                   UserName=mv.FirstOrDefault(s=>s.StudentName),
                   RoleId=mv.Key.RoleId,
                   Grade= mv.Average(s=>s.Score)
                }).ToList();

            Console.WriteLine($"读取完成!{watch.ElapsedMilliseconds}");
            //var data = data1.GroupBy(m => m.GroupID).Select(m => new GradeGroupList()
            //{
            //    Roles = new KeyValueView<decimal>[]{
            //        m.Where(m=>m.RoleId.Equals((int)eMissionRole.Admin)).
            //        Select(m=>DetailViewMapKeyValue(m)).FirstOrDefault(),
            //      m.Where(m=>m.RoleId.Equals((int)eMissionRole.ComponentBuilding)).
            //        Select(m=>DetailViewMapKeyValue(m)).FirstOrDefault(),
            //        m.Where(m=>m.RoleId.Equals((int)eMissionRole.ComponentAssemble)).
            //        Select(m=>DetailViewMapKeyValue(m)).FirstOrDefault(),
            //          m.Where(m=>m.RoleId.Equals((int)eMissionRole.ProcessOfConstruction)).
            //        Select(m=>DetailViewMapKeyValue(m)).FirstOrDefault(),
            //    },
            //    Score = Math.Round(m.Average(m => m.Score), 2),
            //    UserTime = m.Where(m => m.RoleId.Equals((int)eMissionRole.Admin)).Select(m => m.TimeLength).FirstOrDefault()
            //}).OrderByDescending(m => m.Score).ToList();
            Console.WriteLine($"分组完成!{watch.ElapsedMilliseconds}");
            //if (Params.UserID != Guid.Empty)
            //{
            //    for (int i = 0; i < data.Count(); i++)
            //    {
            //        if (data[i].Roles.Where(r => r.Ext1.Equals(Params.UserID.ToString())).Any())
            //        {

            //            return;//Ok(new KeyValueView<decimal> { Value = data[i].Score, Name = (i + 1).ToString() });
            //        }
            //    }
            //    return;//Ok(new KeyValueView<decimal> { Value = -1, Name = (-1).ToString() });
            //}
            //var d = data.Take(Params.PageSize).ToList();
            return;
        }

        public void StartTest1( MissionDetailParameter Params)
        {
            #region
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var query = client.Queryable<MDT.VirtualSoftPlatform.Entity.MissionDetailEntity, DictionaryEntity, DictionaryEntity, StudentEntity, MDT.VirtualSoftPlatform.Entity.UserEntity, MissionEntity, DictionaryEntity, DictionaryEntity, ProductDeviceEntity, MDT.VirtualSoftPlatform.Entity.UserEntity>(
              (missionDetail, missionRole, status, student, userStudent, mission, groupMode, missionMode, productDevice, userTeacher) =>
              new NetCore.ORM.Simple.Entity.JoinInfoEntity(
             NetCore.ORM.Simple.Entity.eJoinType.Left, missionDetail.MissionRole.Equals(missionRole.RowID) && missionRole.MainID.Equals((int)eDictionary.missionRole),
              NetCore.ORM.Simple.Entity.eJoinType.Inner, missionDetail.StatusId.Equals(status.RowID) && status.MainID.Equals((int)eDictionary.missionstate),
              NetCore.ORM.Simple.Entity.eJoinType.Inner, student.UserID.Equals(missionDetail.UserId),
              NetCore.ORM.Simple.Entity.eJoinType.Inner, userStudent.ID.Equals(missionDetail.UserId),
              NetCore.ORM.Simple.Entity.eJoinType.Inner, mission.ID.Equals(missionDetail.MissionId),
             NetCore.ORM.Simple.Entity.eJoinType.Inner, groupMode.RowID.Equals(mission.GroupMode) && groupMode.MainID.Equals((int)eDictionary.groupmode),
              NetCore.ORM.Simple.Entity.eJoinType.Inner, missionMode.RowID.Equals(mission.MissionMode) && missionMode.MainID.Equals((int)eDictionary.missionmode),
               NetCore.ORM.Simple.Entity.eJoinType.Left, productDevice.ID.Equals(missionDetail.ProductDeviceId),
             NetCore.ORM.Simple.Entity.eJoinType.Inner, userTeacher.ID.Equals(mission.CreatorID)
              ));


            #endregion
            if (Params.ClassID != Guid.Empty)
            {
                query.Where((missionDetail, missionRole, status, student, userStudent, mission,
                    groupMode, missionMode, productDevice, userTeacher) => student.ClassID.Equals(Params.ClassID));
            }
            if (Params.Status >= 0)
            {
                query.Where(missionDetail => missionDetail.StatusId.Equals(Params.Status));
            }
            if (Params.ProductID != Guid.Empty)
            {
                query.Where(missionDetail => missionDetail.ProductId.Equals(Params.ProductID));
            }

            if ( query.Any() == false)
            {
                return;// Ok("无数据!");
            }
            Console.WriteLine($"开始读取!{watch.ElapsedMilliseconds}");
            var data1 = query.Select(
                 (missionDetail, missionRole, status, student, userStudent, mission, groupMode, missionMode, productDevice, userTeacher) =>
                 new MissionDetailView()
                 {
                     UserId = missionDetail.UserId,
                     Score = missionDetail.Score,
                     StartTime = missionDetail.StartTime,
                     EndTime = missionDetail.EndTime,
                     RoleName = missionRole.RowName,
                     RoleId = missionDetail.MissionRole,
                     Status = missionDetail.StatusId,
                     StatusName = status.RowName ?? "",
                     MissionId = mission.ID,
                     StudentName = userStudent.UserName,
                     MissionName = mission.MissionName,
                     ProductDeviceId = productDevice.ID,
                     ProductDeviceName = productDevice.DisplayName?? "设备未分配",
                     MissionModeName = missionMode.RowName,
                     MissionMode = mission.MissionMode,
                     GroupMode = mission.GroupMode,
                     GroupModeName = groupMode.RowName ,
                     Id = missionDetail.ID,
                     TeacherName = userTeacher.UserName,
                     GroupID = missionDetail.GroupID,
                     ClassID = student.ClassID,
                     Avatar = userStudent.Avatar,
                     //TimeLength = SqlFunc.DateDiff(DateType.Minute, missionDetail.StartTime, missionDetail.EndTime)
                 }
                ).ToList();

            Console.WriteLine($"读取完成!{watch.ElapsedMilliseconds}");
            var data = data1.GroupBy(m => m.GroupID).Select(m => new GradeGroupList()
            {
                Roles = new KeyValueView<decimal>[]{
                    m.Where(m=>m.RoleId.Equals((int)eMissionRole.Admin)).
                    Select(m=>DetailViewMapKeyValue(m)).FirstOrDefault(),
                  m.Where(m=>m.RoleId.Equals((int)eMissionRole.ComponentBuilding)).
                    Select(m=>DetailViewMapKeyValue(m)).FirstOrDefault(),
                    m.Where(m=>m.RoleId.Equals((int)eMissionRole.ComponentAssemble)).
                    Select(m=>DetailViewMapKeyValue(m)).FirstOrDefault(),
                      m.Where(m=>m.RoleId.Equals((int)eMissionRole.ProcessOfConstruction)).
                    Select(m=>DetailViewMapKeyValue(m)).FirstOrDefault(),
                },
                Score = Math.Round(m.Average(m => m.Score), 2),
                UserTime = m.Where(m => m.RoleId.Equals((int)eMissionRole.Admin)).Select(m => m.TimeLength).FirstOrDefault()
            }).OrderByDescending(m => m.Score).ToList();
            Console.WriteLine($"分组完成!{watch.ElapsedMilliseconds}");
            if (Params.UserID != Guid.Empty)
            {
                for (int i = 0; i < data.Count(); i++)
                {
                    if (data[i].Roles.Where(r => r.Ext1.Equals(Params.UserID.ToString())).Any())
                    {

                        return;//Ok(new KeyValueView<decimal> { Value = data[i].Score, Name = (i + 1).ToString() });
                    }
                }
                return;//Ok(new KeyValueView<decimal> { Value = -1, Name = (-1).ToString() });
            }
            var d = data.Take(Params.PageSize).ToList();
            return;// Ok(new { data = d, count = d.Count() });
        }

        private KeyValueView<decimal> DetailViewMapKeyValue(MissionDetailView m)
        {
            return new KeyValueView<decimal>()
            {
                Name = m.StudentName,
                Value = m.Score,
                Description = m.RoleName,
                NickName = m.Avatar,
                Code = m.RoleId,
                Ext1 = m.UserId.ToString()
            };
        }
    }
}
