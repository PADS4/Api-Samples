using FileApiExample.RequestContentModels;
using FileApiExample.RequestContentModels.File;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FileApiExample
{
    class FileApiExample
    {
        private readonly HttpClient httpClient = new HttpClient();
        private string Url = "http://localhost:81/";
        static void Main()
        {
            FileApiExample fai = new FileApiExample();
            var authObject = fai.Authenticate().Result;
            Console.WriteLine(authObject);
            Console.WriteLine("AUTHENTICATED");
            var folderObject = fai.GetFolder("upload_files").Result;
            Console.WriteLine(folderObject);
            Console.WriteLine("FOLDER GOTTEN");
            var newFolderObject = fai.CreateFolder("TestFolder").Result;
            Console.WriteLine(newFolderObject);
            Console.WriteLine("FOLDER CREATED");
            var moveFolderObject = fai.MoveFolder("\\").Result;
            Console.WriteLine(moveFolderObject);
            Console.WriteLine("FOLDER MOVED");
            var uploadFileObject = fai.UploadFile(".\\Assets\\slipper.png").Result;
            Console.WriteLine(uploadFileObject);
            Console.WriteLine("FILE UPLOADED");
            var moveFileObject = fai.MoveFolder("TestFolder").Result;
            Console.WriteLine(moveFileObject);
            Console.WriteLine("FILE MOVED");
            var deleteFileObject = fai.DeleteFile("slipper.png").Result;
            Console.WriteLine(deleteFileObject);
            Console.WriteLine("FILE DELETED");
            var deleteFolderObject = fai.DeleteFolder("TestFolder").Result;
            Console.WriteLine(deleteFolderObject);
            Console.WriteLine("FOLDER DELETED");
            Console.WriteLine("Done");
        }

        public async Task<JObject> Authenticate()
        {
            string authUrl = Url + "rdx/NDS.Services.Authentication/api/v1/Account/Logon";
            AuthenticationContent authContent = new AuthenticationContent
            {
                Password = "user1",
                Username = "user1",
                Domain = "pads"
            };

            var authRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(authUrl),
                Content = new StringContent(JsonConvert.SerializeObject(authContent), Encoding.UTF8, "application/json")
            };
            return await PostRequest(authRequest);
        }

        #region Folder
        public async Task<JObject> GetFolder(string folderName)
        {
            var folderUrl = Url + "rdx/NDS.Services.Content/api/v1/content/folder";

            FolderContent folderContent = new FolderContent
            {
                Path = folderName,
                IncludeHidden = true,
                SearchPattern = "",
                Paging = new paging
                {
                    Start = 0,
                    Items = 10
                },
                Sorting = new sorting
                {
                    Descending = true
                }
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(folderUrl),
                Content = new StringContent(JsonConvert.SerializeObject(folderContent), Encoding.UTF8, "application/json")
            };
            return await PostRequest(request);
        }

        public async Task<JObject> CreateFolder(string folderName)
        {
            var folderUrl = Url + "rdx/NDS.Services.Content/api/v1/content/createFolder";

            CreateFolderContent folderContent = new CreateFolderContent
            {
                ParentFolder = "preview",
                Name = folderName,
                Hidden = false
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(folderUrl),
                Content = new StringContent(JsonConvert.SerializeObject(folderContent), Encoding.UTF8, "application/json")
            };
            return await PostRequest(request);
        }

        public async Task<JObject> MoveFolder(string folderDestination)
        {
            var folderUrl = Url + "rdx/NDS.Services.Content/api/v1/content/moveFolder";

            MoveFolderContent folderContent = new MoveFolderContent
            {
                MoveActions = new MoveFolderAction[1].Select(httpClient => new MoveFolderAction
                {
                    Folder = "preview/TestFolder",
                    Destination = folderDestination,
                    Force = true
                }).ToArray(),
                Async = true
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(folderUrl),
                Content = new StringContent(JsonConvert.SerializeObject(folderContent), Encoding.UTF8, "application/json")
            };
            return await PostRequest(request);
        }

        public async Task<JObject> DeleteFolder(string folderName)
        {
            var folderUrl = Url + "rdx/NDS.Services.Content/api/v1/content/deleteFolder";

            DeleteFolderContent folderContent = new DeleteFolderContent
            {
                DeleteActions = new DeleteFolderAction[1].Select(h => new DeleteFolderAction
                {
                    Folder = folderName
                }).ToArray(),
                Async = true
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(folderUrl),
                Content = new StringContent(JsonConvert.SerializeObject(folderContent), Encoding.UTF8, "application/json")
            };
            return await PostRequest(request);
        }
        #endregion

        #region File
        public async Task<JObject> UploadFile(string fileName)
        {
            var folderUrl = Url + "rdx/NDS.Services.Content/api/v1/content/uploadFile";
            var fileStream = new FileStream(fileName, FileMode.Open);
            MultipartFormDataContent dataContent = new MultipartFormDataContent();
            HttpContent content = new StreamContent(fileStream);
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "slipper",
                FileName = "slipper.png"
            };
            dataContent.Add(content, "slipper");
            UploadFileContent folderContent = new UploadFileContent
            {
                file = dataContent
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(folderUrl),
                Content = dataContent
            };
            return await PostRequest(request);
        }

        public async Task<JObject> MoveFile(string folderDestination)
        {
            var folderUrl = Url + "rdx/NDS.Services.Content/api/v1/content/moveFolder";

            MoveFileContent folderContent = new MoveFileContent
            {
                MoveActions = new MoveFileAction[1].Select(httpClient => new MoveFileAction
                {
                    Folder = "",
                    File = "slipper.png",
                    DestinationFolder = folderDestination,
                    DestinationFileName = "slipper.png",
                    Force = true
                }).ToArray()
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(folderUrl),
                Content = new StringContent(JsonConvert.SerializeObject(folderContent), Encoding.UTF8, "application/json")
            };
            return await PostRequest(request);
        }

        public async Task<JObject> DeleteFile(string fileName)
        {
            var folderUrl = Url + "rdx/NDS.Services.Content/api/v1/content/deleteFolder";

            DeleteFileContent folderContent = new DeleteFileContent
            {
                DeleteActions = new DeleteFileAction[1].Select(h => new DeleteFileAction
                {
                    File = fileName,
                    Folder = "TestFolder"
                }).ToArray()
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(folderUrl),
                Content = new StringContent(JsonConvert.SerializeObject(folderContent), Encoding.UTF8, "application/json")
            };
            return await PostRequest(request);
        }
        #endregion

        private async Task<JObject> PostRequest(HttpRequestMessage authRequest)
        {
            try
            {
                HttpResponseMessage authResponse = await httpClient.SendAsync(authRequest);
                authResponse.EnsureSuccessStatusCode();
                string responseString = await authResponse.Content.ReadAsStringAsync();
                JObject responseObject = JObject.Parse(responseString);
                return responseObject;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e);
                return new JObject();
            }
        }
    }
}
