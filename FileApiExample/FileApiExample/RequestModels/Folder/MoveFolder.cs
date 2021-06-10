namespace FileApiExample.RequestContentModels
{
    class MoveFolderContent
    {
        public MoveFolderAction[] MoveActions { get; set; }
        public bool Async { get; set; }
    }
    class MoveFolderAction
    {
        public string Folder { get; set; }
        public string Destination { get; set; }
        public bool Force { get; set; }
    }
    class MoveFolderResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public MoveFolderResult[] Results { get; set; }
    }
    class MoveFolderResult
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public string Folder { get; set; }
    }
}
