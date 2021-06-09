using System;
using System.Collections.Generic;
using System.Text;

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
}
