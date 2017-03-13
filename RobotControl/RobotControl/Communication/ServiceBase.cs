using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsManager.Services
{
    public class ServiceBase
    {
       /* #region singleton

        private static ServiceBase instance = null;

        public static T GetInstance<T>() where T : ServiceBase, new()
        {
            if (instance == null)
                instance = new T();

            return (T)instance;
        }

        public ServiceBase()
        {
        }

        #endregion*/

        #region [ Log ] **************************************************
        public event EventHandler<LogEventArgs> OnLog;
        protected void Log(string message, params object[] arg)
        {
            Log(String.Format(message, arg));
        }

        protected void Log(string message)
        {
            if (OnLog != null)
                OnLog(this, new LogEventArgs(message));
        }
        #endregion *******************************************************

    }
}
