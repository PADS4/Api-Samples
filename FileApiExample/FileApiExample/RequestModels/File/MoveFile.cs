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
    class MoveFileResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public MoveFileResult[] Results { get; set; }
    }
    class MoveFileResult
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public string Folder { get; set; }
        public string File { get; set; }
    }
}
