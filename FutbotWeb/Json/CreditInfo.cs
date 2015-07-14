using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FutbotWeb.Json
{
    public class CreditInfo
    {
        public class Currency
        {
            public string name { get; set; }
            public int funds { get; set; }
            public int finalFunds { get; set; }
        }

        public class UnopenedPacks
        {
            public int preOrderPacks { get; set; }
            public int recoveredPacks { get; set; }
        }

        public class RootObject
        {
            public int credits { get; set; }
            public List<Currency> currencies { get; set; }
            public UnopenedPacks unopenedPacks { get; set; }
        }
    }
}
