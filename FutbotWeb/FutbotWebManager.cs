using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Futbot;
using System.Threading;
using FutbotWeb.Http.Script;

namespace FutbotWeb
{
    public class FutbotWebManager
    {
        #region static

        private static FutbotWebManager _manager;
        
        static FutbotWebManager()
        {
            _manager = new FutbotWebManager();
            _manager.Run();
        }

        public static int ActiveThreads { get { return _manager.NumActiveThreads; } }

        public static void AddScript(FutbotScript script, AuthenticationInfo auth, Fifa fifa)
        {
            FutbotScriptManager manager = new FutbotScriptManager(script, auth, fifa);
        }

        #endregion

        #region instance

        private object _scriptPoolLock = new object();

        private HashSet<FutbotScriptManager> _scriptManagers = new HashSet<FutbotScriptManager>();

        protected int NumActiveThreads 
        { 
            get 
            {
                lock (_scriptPoolLock)
                {
                    return _scriptManagers.Count;
                }
            } 
        }

        protected void CleanScripts()
        {
            lock (_scriptPoolLock)
            {
                _scriptManagers.RemoveWhere(x => !x.IsActive);
            }
        }

        protected void AddScript(FutbotScriptManager scriptManager)
        {
            lock (_scriptPoolLock)
            {
                _scriptManagers.Add(scriptManager);
                scriptManager.RunScript();
            }
        }

        public void Run()
        {
            while (!ApplicationManager.Shutdown)
            {
                Thread.Sleep(1000);
                CleanScripts();
            }
        }

        #endregion
    }
}
