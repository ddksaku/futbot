using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FutbotWeb.Json
{
    public class ValidateInfo
    {
        public class RootObject
        {
            public string debug { get; set; }
            public string @string { get; set; }
            public string reason { get; set; }
            public string token { get; set; }
            public string code { get; set; }
        }
    }
}
