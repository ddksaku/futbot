using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FutbotWeb.Http.Script
{
    public class Context
    {
        public AuthenticationInfo Authentication { get; set; }

        public Fifa Fifa { get; set; }

        public Context(AuthenticationInfo auth, Fifa fifa)
        {
            this.Authentication = auth;
            this.Fifa = fifa;
        }
    }
}
