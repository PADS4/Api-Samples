using System.Net.Http;

namespace FileApiExample.RequestContentModels.File
{
    class UploadFileContent
    {
        public MultipartFormDataContent file { get; set; }
    }
}
