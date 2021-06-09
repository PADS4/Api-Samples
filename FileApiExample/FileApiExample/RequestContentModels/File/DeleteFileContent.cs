using System;
using System.Collections.Generic;
using System.Text;

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
}
