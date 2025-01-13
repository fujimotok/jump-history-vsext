using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace jump_history
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("5b4be84a-b890-406f-9a09-684f7abb7831")]
    public class ToolWindow1 : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindow1"/> class.
        /// </summary>
        public ToolWindow1() : base(null)
        {
            this.Caption = "Jump History";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new ToolWindow1Control();
        }
    }
}
