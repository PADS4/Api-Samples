namespace FileApiExample.RequestContentModels
{
    class DeleteFileContent
    {
        public DeleteFileAction[] DeleteActions { get; set; }
    }
    class DeleteFileAction
    {
        public string File { get; set; }
        public string Folder { get; set; }
    }
    class DeleteFileResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public DeleteResult[] Results { get; set; }
    }
    class DeleteResult
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public string Folder { get; set; }
    }
}
