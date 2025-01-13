using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System.Diagnostics;

namespace jump_history
{
    internal class ToolWindow1VM
    {
        public ToolWindow1VM(DTE dte)
        {
            this._dte = dte;

            this.InitializeCommandEvents(this._dte);
        }

        private EnvDTE.DTE _dte;
        private CommandEvents _commandEvents;

        private void InitializeCommandEvents(EnvDTE.DTE dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (dte == null)
            {
                return;
            }

            // CommandEventsを保持しておかないとGCで解放されてしまうため、メンバー変数に保持
            this._commandEvents = dte.Events.CommandEvents;
            this._commandEvents.BeforeExecute += this.CommandEventsBeforeExecute;
            this._commandEvents.AfterExecute += this.CommandEventsAfterExecute;
        }

        private void CommandEventsBeforeExecute (string guid, int id, object objIn, object objOut, ref bool cancel)
        {
            Debug.WriteLine($"before execute {guid} {id}");
        }

        private void CommandEventsAfterExecute (string guid, int id, object objIn, object objOut)
        {
            Debug.WriteLine($"after execute {guid} {id}");
        }
    }
}
