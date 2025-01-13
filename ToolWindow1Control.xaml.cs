using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace jump_history
{
    /// <summary>
    /// Interaction logic for ToolWindow1Control.
    /// </summary>
    public partial class ToolWindow1Control : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindow1Control"/> class.
        /// </summary>
        public ToolWindow1Control()
        {
            this.InitializeComponent();
        }

        private void list_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var vm = this.DataContext as ToolWindow1VM;
            if (vm != null)
            {
                var item = (sender as ListView).SelectedItem as LineInfo;
                if (item != null)
                {
                    vm.JumpTo(item);
                }
            }
        }
    }
}
