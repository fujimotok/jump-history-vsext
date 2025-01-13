using EnvDTE;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jump_history
{
    /// <summary>
    /// singleton
    /// </summary>
    internal class GotoLogger
    {
        private static GotoLogger _instance = new GotoLogger();
        private EnvDTE.DTE _dte;
        private CommandEvents _commandEvents;

        private GotoLogger() { }

        public static GotoLogger Instance
        {
            get
            {
                return _instance;
            }
        }

        public void InitializeCommandEvents(Package package)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            EnvDTE.DTE dte = ServiceProvider.GlobalProvider.GetService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;

            if (dte == null)
            {
                return;
            }

            // CommandEventsを保持しておかないとGCで解放されてしまうため、メンバー変数に保持
            this._commandEvents = dte.Events.CommandEvents;
            this._commandEvents.BeforeExecute += this.CommandEventsBeforeExecute;
            this._commandEvents.AfterExecute += this.CommandEventsAfterExecute;
        }

        private void CommandEventsBeforeExecute(string guid, int id, object objIn, object objOut, ref bool cancel)
        {
            Debug.WriteLine($"before execute {guid} {id}");
        }

        private void CommandEventsAfterExecute(string guid, int id, object objIn, object objOut)
        {
            Debug.WriteLine($"after execute {guid} {id}");
        }
    }
}
