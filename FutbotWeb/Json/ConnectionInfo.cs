using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FutbotWeb.Json
{
    public class ConnectionInfo
    {
        public class RootObject
        {
            public string protocol { get; set; }
            public string ipPort { get; set; }
            public string serverTime { get; set; }
            public string lastOnlineTime { get; set; }
            public string sid { get; set; }
        }
    }
}
