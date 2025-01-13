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
        }

        private EnvDTE.DTE _dte;
    }
}
