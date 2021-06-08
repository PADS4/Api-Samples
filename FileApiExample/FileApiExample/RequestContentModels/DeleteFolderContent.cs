using System;
using System.Collections.Generic;
using System.Text;

namespace FileApiExample.RequestContentModels
{
    class DeleteFolderContent
    {
        public DeleteAction[] DeleteActions { get; set; }
        public bool Async { get; set; }
    }
    class DeleteAction
    {
        public string Folder { get; set; }
    }
}
