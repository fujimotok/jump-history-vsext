namespace jump_history
{
    internal class LineInfo
    {
        public string FileName { get; set; } = string.Empty;
        public int LineNumber { get; set; } = 1;
        public int ColumnNumber { get; set; } = 1;
        public bool IsSorce { get; set; } = false;
        public string Direction => this.IsSorce ? "<-" : "->";
        public string LineText { get; set; } = "Error";
        public string Name => $"{this.Direction}    {this.LineText}";
    }
}
