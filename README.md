# NetCore-ORM-Simple
It's the about net core orm<br>
这是一个关于.net core 的 orm 框架。<br>
orm 框架是一个帮助程序员快速访问数据库的并且完成数据的映射的一个工具<br>
与直接写sql语句执行相比效率肯定要稍微差一点，但是可以提高项目开发的效率.<br>
一些对查询语句的查询效率 (经过执行计划分析之后可以了解sql语句执行的相关信息) 要求不那么高的项目，使用orm框架是一个不错的选择<br>
既然orm框架是为了快速完成数据库表中的数据的添加修改，和查询，那为啥不选择一个语法非常优雅的呢，尤其是查询的写法。<br>
 ISimpleClient simpleClient = new SimpleClient(<br>
            new DataBaseConfiguration(false,<br>
            new ConnectionEntity("链接字符串!") <br>
            { <br>
                IsAutoClose = true,<br>
                DBType=eDBType.Mysql,<br>
                Name="test1",<br>
                ReadWeight=5,<br>
                WriteReadType=eWriteOrReadType.ReadOrWrite<br>
            }));<br>

        var command=simpleClient.Insert(
            new UserEntity() {
                CompanyId=1,
                gIdColumn=Guid.NewGuid(),
                Description="Test",
                Name="Name",Role=10});
<br>
          simpleClient.Update(<br>
          new UserEntity()<br>
          {<br>
              CompanyId = 1,<br>
              gIdColumn = Guid.NewGuid(),<br>
              Description = "Test",<br>
              Name = "Name",<br>
              Role = 10<br>
          });<br>
<br>
        var query = simpleClient.Queryable<UserEntity, RoleEntity, CompanyEntity>(<br>
            (u, r, c) =><br>
                new JoinInfoEntity(<br>
                    new JoinMapEntity(eJoinType.Inner, u.Role == r.Id && u.Role.Equals((int)eConditionType.Sign)),<br>
                    new JoinMapEntity(eJoinType.Inner, u.CompanyId == c.Id)<br>
                )<br>
             )<br>
            .Where((u,r,c)=>u.Id>10&&(r.Id==10||c.Id.Equals((int)eDBType.Mysql)))<br>
            .Select((u,r,c)=>new ViewView<br>
             {<br>
                 UserId=r.Id,<br>
                 DisplayName=u.Name,<br>
                 CompanyName=c.Name,<br>
                 RoleName=r.Name,<br>
             }).Select(v=>new<br>
             {<br>
                 UID=v.UserId,<br>
                 RName=v.RoleName<br>
             });<br>
第一版先实现mysql数据库的相关操作<br>
后续会支持mysql sqlService sqlite 等数据库 <br>
有兴趣的可以了解了解<br>
The authors are currently in development and should will be complete soon.<br>
作者现在正在处于开发中不就的将来应该可以完成。<br>
