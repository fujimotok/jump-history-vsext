using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jump_history
{
    internal class LineInfo
    {


        public string FileName { get; set; } = string.Empty;
        public int LineNumber { get; set; } = 1;
        public int ColumnNumber { get; set; } = 1;
        public bool IsSorce { get; set; } = false;
        public string LineText { get; set; } = "Error";
    }
}
