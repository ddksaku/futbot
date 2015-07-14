using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FutbotWeb
{
    public class LoginScript : FutbotScript
    {
        public LoginScript()
            : base(1)
        {

        }

        public override void DoNextStep(int stepNumber, FutbotScriptManager scriptManager)
        {
            switch (stepNumber)
            {
                case 1:
                    scriptManager.Authenticate();
                    break;
                default:
                    break;
            }
        }
    }
}
