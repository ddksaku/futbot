using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FutbotWeb.Json
{
    public class QuestionInfo
    {
        public class RootObject
        {
            public int question { get; set; }
            public int attempts { get; set; }
            public int recoverAttempts { get; set; }
        }
    }
}
