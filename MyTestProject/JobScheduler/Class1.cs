using System.Collections.Generic;
using System.Linq;

namespace JobScheduler
{
    public class Class1
    {
        public static IEnumerable<string> GetConfig()
        {
            var configs = new List<string>()
            {
                "1001",
                "1002"
            };
            return configs;
        }

        public void test()
        {
            var configs = GetConfig();

            var items = new List<Item>
            {
                new Item(1005, "A"),
                new Item(1003, "B"),
                new Item(1004, "C")
            };

            var Ids = items.Select(x => x.Id.ToString()).ToArray();
            var isContain4GService = configs.ContainsAny(Ids);
        }
    }

    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Item(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}