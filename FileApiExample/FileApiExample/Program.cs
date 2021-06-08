using FileApiExample.RequestContentModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileApiExample
{
    class FileApiExample
    {
        private readonly HttpClient httpClient = new HttpClient();
        private CancellationTokenSource cancellationTokenSource;
        private string Url = "http://localhost:81/";
        static void Main()
        {
            FileApiExample fai = new FileApiExample();
            var authObject = fai.Authenticate().Result;
            Console.WriteLine(authObject);
            var folderObject = fai.GetFolder("upload_files").Result;
            Console.WriteLine(folderObject);
            var newFolderObject = fai.CreateFolder("TestFolder").Result;
            Console.WriteLine(newFolderObject);
            var moveFolderObject = fai.MoveFolder("/").Result;
            Console.WriteLine(moveFolderObject);
            var deleteFolderObject = fai.DeleteFolder("TestFolder").Result;
            Console.WriteLine(deleteFolderObject);
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

            var authRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(folderUrl),
                Content = new StringContent(JsonConvert.SerializeObject(folderContent), Encoding.UTF8, "application/json")
            };
            return await PostRequest(authRequest);
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

            var authRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(folderUrl),
                Content = new StringContent(JsonConvert.SerializeObject(folderContent), Encoding.UTF8, "application/json")
            };
            return await PostRequest(authRequest);
        }

        public async Task<JObject> MoveFolder(string folderDestination)
        {
            var folderUrl = Url + "rdx/NDS.Services.Content/api/v1/content/moveFolder";

            MoveFolderContent folderContent = new MoveFolderContent
            {
                MoveActions = new MoveAction[1].Select(httpClient => new MoveAction
                {
                    Folder = "preview/TestFolder",
                    Destination = folderDestination,
                    Force = true
                }).ToArray(),
                Async = true
            };

            var authRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(folderUrl),
                Content = new StringContent(JsonConvert.SerializeObject(folderContent), Encoding.UTF8, "application/json")
            };
            return await PostRequest(authRequest);
        }
        public async Task<JObject> DeleteFolder(string folderName)
        {
            var folderUrl = Url + "rdx/NDS.Services.Content/api/v1/content/deleteFolder";

            DeleteFolderContent folderContent = new DeleteFolderContent
            {
                DeleteActions = new DeleteAction[1].Select(h => new DeleteAction
                {
                    Folder = folderName
                }).ToArray(),
                Async = true
            };

            var authRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(folderUrl),
                Content = new StringContent(JsonConvert.SerializeObject(folderContent), Encoding.UTF8, "application/json")
            };
            return await PostRequest(authRequest);
        }

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
