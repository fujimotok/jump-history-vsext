using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

        public ObservableCollection<LineInfo> LineInfos { get; } = new ObservableCollection<LineInfo>();

        public void InitializeCommandEvents(AsyncPackage package)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            this._dte = ServiceProvider.GlobalProvider.GetService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;

            if (this._dte == null)
            {
                return;
            }

            // CommandEventsを保持しておかないとGCで解放されてしまうため、メンバー変数に保持
            this._commandEvents = this._dte.Events.CommandEvents;
            this._commandEvents.BeforeExecute += this.CommandEventsBeforeExecute;
            this._commandEvents.AfterExecute += this.CommandEventsAfterExecute;
        }

        private void CommandEventsBeforeExecute(string guid, int id, object objIn, object objOut, ref bool cancel)
        {
            // Debug.WriteLine($"before execute {guid} {id}");

            if (IsLoggingCommand(guid, id))
            {
                LogCommand(true);
            }
        }

        private void CommandEventsAfterExecute(string guid, int id, object objIn, object objOut)
        {
            // Debug.WriteLine($"after execute {guid} {id}");

            if (IsLoggingCommand(guid, id))
            {
                LogCommand(false);
            }
        }

        private bool IsLoggingCommand(string guid, int id)
        {
            // VSStd97CmdID の GUID のみを対象
            if (guid != "{5EFC7975-14BC-11CF-9B2B-00AA00573819}")
            {
                return false;
            }

            VSConstants.VSStd97CmdID cmdId = (VSConstants.VSStd97CmdID)id;

            switch (cmdId)
            {
                case VSConstants.VSStd97CmdID.GotoDefn:
                case VSConstants.VSStd97CmdID.GotoRef:
                case VSConstants.VSStd97CmdID.GotoDecl:
                    return true;
                default:
                    return false;
            }
        }

        private void LogCommand(bool isSrc)
        {
            var info = GetLineInfo(isSrc);
            this.LineInfos.Add(info);
        }

        private LineInfo GetLineInfo(bool isSrc)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var document = this._dte?.ActiveDocument;
            var selection = this._dte?.ActiveDocument?.Selection as TextSelection;
            if (document == null || selection == null)
            {
                return new LineInfo();
            }

            var info = new LineInfo
            {
                FileName = document.FullName ?? string.Empty,
                LineNumber = selection.CurrentLine,
                ColumnNumber = selection.CurrentColumn,
                IsSorce = isSrc,
                LineText = this.GetCurrentLineText(selection) ?? string.Empty,
            };

            return info;
        }

        private string GetCurrentLineText(TextSelection selection)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            int originalLine = selection.CurrentLine;
            int originalColumn = selection.CurrentColumn;

            // 現在の行全体を選択
            selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstColumn, false);
            selection.EndOfLine(true);
            
            // テキストを取得
            string lineText = selection.Text;

            // 選択を元に戻す
            selection.MoveToLineAndOffset(originalLine, originalColumn);
            
            return lineText;
        }
    }
}
