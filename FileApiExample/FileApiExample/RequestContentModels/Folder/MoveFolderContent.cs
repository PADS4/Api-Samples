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
}
