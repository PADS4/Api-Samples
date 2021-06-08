namespace FileApiExample
{
    class FolderContent
    {
        public string Path { get; set; }
        public bool IncludeHidden { get; set; }
        public string SearchPattern { get; set; }
        public paging Paging { get; set; }
        public sorting Sorting { get; set; }
    }
    class paging
    {
        public int Start { get; set; }
        public int Items { get; set; }
    }
    class sorting
    {
        public bool Descending { get; set; }
    }
}
