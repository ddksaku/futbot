using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FutbotWeb.Json
{
    public class DiscardInfo
    {
        public class Item
        {
            public long id { get; set; }
        }

        public class RootObject
        {
            public List<Item> items { get; set; }
            public int totalCredits { get; set; }
        }
    }
}
