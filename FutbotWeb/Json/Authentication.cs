using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FutbotWeb.Json
{
    public class Authentication
    {
        public class Identification
        {
            public string authCode { get; set; }
        }

        public class RootObject
        {
            public bool isReadOnly { get; set; }
            public string sku { get; set; }
            public int clientVersion { get; set; }
            public long nuc { get; set; }
            public int nucleusPersonaId { get; set; }
            public string nucleusPersonaDisplayName { get; set; }
            public string nucleusPersonaPlatform { get; set; }
            public string locale { get; set; }
            public string method { get; set; }
            public int priorityLevel { get; set; }
            public Identification identification { get; set; }
        }
    }
}
