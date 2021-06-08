namespace FileApiExample.RequestContentModels
{
    class MoveFolderContent
    {
        public MoveAction[] MoveActions { get; set; }
        public bool Async { get; set; }
    }
    class MoveAction
    {
        public string Folder { get; set; }
        public string Destination { get; set; }
        public bool Force { get; set; }
    }
}
