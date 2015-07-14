using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Futbot;

namespace FutbotWeb
{
    public abstract class FutbotScript
    {
        public bool Finished { get; private set; }

        public int StepNumber { get; private set; }

        public int NumSteps { get; private set; }

        public FutbotScript(int maxSteps)
        {
            Finished = false;
            StepNumber = 0;
            NumSteps = maxSteps;
        }

        public bool NextStep(FutbotScriptManager scriptManager)
        {
            try
            {
                DoNextStep(StepNumber, scriptManager);
                StepNumber++;
                Finished = (StepNumber >= NumSteps);
                return true;
            }
            catch (Exception exc)
            {
                LogManager.Error("Script error:" + exc);
                return false;
            }            
        }

        public abstract void DoNextStep(int stepNumber, FutbotScriptManager scriptManager);
    }
}
