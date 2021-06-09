namespace FileApiExample.RequestContentModels
{
    class MoveFileContent
    {
        public MoveFileAction[] MoveActions { get; set; }
    }
    class MoveFileAction
    {
        public string Folder { get; set; }
        public string File { get; set; }
        public string DestinationFolder { get; set; }
        public string DestinationFileName { get; set; }
        public bool Force { get; set; }
    }
}
