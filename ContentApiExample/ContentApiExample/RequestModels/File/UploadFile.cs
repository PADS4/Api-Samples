using System.Net.Http;

namespace ContentApiExample.RequestContentModels.File
{
    class UploadFileContent
    {
        public MultipartFormDataContent file { get; set; }
    }
    class UploadFileResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
    }
}
