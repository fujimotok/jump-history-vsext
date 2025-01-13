using EnvDTE;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace jump_history
{
    internal class ToolWindow1VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private EnvDTE.DTE _dte;

        public ToolWindow1VM(DTE dte)
        {
            this._dte = dte;
        }

        public void JumpTo(LineInfo item)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            this._dte.ItemOperations.OpenFile(item.FileName);
            var selection = (TextSelection)this._dte.ActiveDocument.Selection;
            selection.MoveToLineAndOffset(item.LineNumber, item.ColumnNumber);
        }

        private LineInfo _selectedItem = null;
        public LineInfo SelectedItem
        {
            get => _selectedItem;
            set
            {
                this._selectedItem = value;
                this.RaisePropertyChanged();
            }
        }

        public ObservableCollection<LineInfo> LineInfos => GotoLogger.Instance.LineInfos;
    }
}
