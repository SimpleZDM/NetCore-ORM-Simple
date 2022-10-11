# NetCore-ORM-Simple
It's the about net core orm<br>
这是一个关于.net core 的 orm 框架。<br>
更加详细的中文介绍地址：https://blog.csdn.net/weixin_45394846/article/details/127154931

orm 框架是一个帮助程序员快速访问数据库的并且完成数据的映射的一个工具<br>
与直接写sql语句执行相比效率肯定要稍微差一点，但是可以提高项目开发的效率.<br>
一些对查询语句的查询效率 (经过执行计划分析之后可以了解sql语句执行的相关信息) 要求不那么高的项目，使用orm框架是一个不错的选择<br>
既然orm框架是为了快速完成数据库表中的数据的添加修改，和查询，那为啥不选择一个语法非常优雅的呢，尤其是查询的写法.
        '''var simpleClient = new SimpleClient(
            new DataBaseConfiguration(false,
            new ConnectionEntity("链接字符串!") 
            { 
                IsAutoClose = true,
                DBType=eDBType.Mysql,
                Name="test1",
                ReadWeight=5,
                WriteReadType=eWriteOrReadType.ReadOrWrite
            }));

        var command=simpleClient.Insert(
            new UserEntity() {
                CompanyId=1,
                gIdColumn=Guid.NewGuid(),
                Description="Test",
                Name="Name",Role=10});

          simpleClient.Update(
          new UserEntity()
          {
              CompanyId = 1,
              gIdColumn = Guid.NewGuid(),
              Description = "Test",
              Name = "Name",
              Role = 10
          });

        var query = simpleClient.Queryable<UserEntity, RoleEntity, CompanyEntity>(
            (u, r, c) =>
                new JoinInfoEntity(
                    new JoinMapEntity(eJoinType.Inner, u.Role == r.Id && u.Role.Equals((int)eConditionType.Sign)),
                    new JoinMapEntity(eJoinType.Inner, u.CompanyId == c.Id)
                )
             )
            .Where((u,r,c)=>u.Id>10&&(r.Id==10||c.Id.Equals((int)eDBType.Mysql)))
            .Select((u,r,c)=>new ViewView
             {
                 UserId=r.Id,
                 DisplayName=u.Name,
                 CompanyName=c.Name,
                 RoleName=r.Name,
             }).Select(v=>new
             {
                 UID=v.UserId,
                 RName=v.RoleName
             });
第一版先实现mysql数据库的相关操作<br>
后续会支持mysql sqlService sqlite 等数据库 <br>
有兴趣的可以了解了解<br>
Basic use is no problem.<br>
现在呢基本使用已经没有问题了。<br>
