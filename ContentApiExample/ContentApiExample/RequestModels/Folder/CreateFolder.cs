namespace ContentApiExample
{
    class CreateFolderContent
    {
        public string ParentFolder { get; set; }
        public string Name { get; set; }
        public bool Hidden { get; set; }
    }
    class CreateFolderResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public string FolderPath { get; set; }
    }
}
