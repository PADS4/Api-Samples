using System;

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
    class FolderResponse
    {
        public SubFolder[] SubFolders { get; set; }
        public File[] Files { get; set; }
        public int TotalItems { get; set; }
    }
    class SubFolder
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime DateLastModified { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Hidden { get; set; }
        public string ParentFolder { get; set; }
    }
    class File
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public DateTime DateLastModified { get; set; }
        public DateTime DateCreated { get; set; }
        public int Size { get; set; }
        public string MD5 { get; set; }
        public bool Hidden { get; set; }
        public string ParentFolder { get; set; }
        public string GenericContentType { get; set; }
    }
}
