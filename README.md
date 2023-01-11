# NetCore-ORM-Simple
It's the about net core orm<br>
这是一个关于.net core 的 生成sql语句的 框架。<br>

支持数据库：Mysql,Sqlite,SqlService

特点：查询的写法非常优雅

更加详细的中文介绍地址：https://blog.csdn.net/weixin_45394846/article/details/127154931

例子：简单配置只需要配置连接字符串就好了

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
配置需要使用的数据库（它仅仅只是一个生成sql语句的工具）
DataBaseConfiguration.DBDrives.Add(eDBType.Mysql,Tuple.Create(typeof(MySqlConnection),typeof(MySqlParameter)));

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
有兴趣的可以了解了解<br>
这里提供了一简单的实现，尽管代码比较粗糙<br>
Basic use is no problem.<br>
现在呢基本使用已经没有问题了。<br>
