namespace FileApiExample.RequestContentModels
{
    class DeleteFolderContent
    {
        public DeleteFolderAction[] DeleteActions { get; set; }
        public bool Async { get; set; }
    }
    class DeleteFolderAction
    {
        public string Folder { get; set; }
    }
    class DeleteFolderResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public DeleteFolderResult[] Results { get; set; }
    }
    class DeleteFolderResult
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public string Folder { get; set; }
    }
}
