using EnvDTE;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace jump_history
{
    internal class ToolWindow1VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
        public ToolWindow1VM(DTE dte)
        {
            this._dte = dte;
        }

        private EnvDTE.DTE _dte;

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
