using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Futbot
{
    public class ApplicationManager
    {
        public static bool Shutdown { get; set; }

        static ApplicationManager()
        {
            Shutdown = false;
        }
    }
}
