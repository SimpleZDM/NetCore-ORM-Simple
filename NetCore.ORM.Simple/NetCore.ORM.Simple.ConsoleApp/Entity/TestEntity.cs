using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.ORM.Simple.ConsoleApp.Entity
{
    public class TestEntity
    {
        public TestEntity()
        {
            dictionary = new Dictionary<string, int>();
            list = new List<TestEntity>();
            dictionaryEntity = new Dictionary<string, TestEntity>();
            dictionaryKeyEntity = new Dictionary<TestEntity, TestEntity>();
        }
        public TestEntity Test { get; set; }
        public int Age { get; set; }
        public Dictionary<string, int> dictionary { get; set; }
        public Dictionary<string, TestEntity> dictionaryEntity { get; set; }
        public Dictionary<TestEntity, TestEntity> dictionaryKeyEntity { get; set; }
        public List<TestEntity> list { get; set; }
        public int[] array { get; set; }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 7fa562bd3062f87f02ed1cd3306129ee312242d4
