using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FutbotWeb.Http.Script;

namespace FutbotSandbox
{
    public class TestService
    {
        public static AuthenticationInfo[] TestAccounts = new AuthenticationInfo[]
        {
            new AuthenticationInfo() { UserName = "", Password = "", Platform = "", SecretAnswer = "" },
            new AuthenticationInfo() { UserName = "", Password = "", Platform = "", SecretAnswer = "" },
            new AuthenticationInfo() { UserName = "", Password = "", Platform = "", SecretAnswer = "" },
            new AuthenticationInfo() { UserName = "", Password = "", Platform = "", SecretAnswer = "" },
            new AuthenticationInfo() { UserName = "", Password = "", Platform = "", SecretAnswer = "" },
            new AuthenticationInfo() { UserName = "", Password = "", Platform = "", SecretAnswer = "" },
            new AuthenticationInfo() { UserName = "", Password = "", Platform = "", SecretAnswer = "" },
            new AuthenticationInfo() { UserName = "", Password = "", Platform = "", SecretAnswer = "" }
        };

        public static Fifa[] TestFifa = new Fifa[]
        {
            new Fifa(),
            new Fifa()
        };
    }
}
