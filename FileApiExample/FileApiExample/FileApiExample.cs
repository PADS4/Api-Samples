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

// SWAGGER URLS
// http://<PADS4 IP>:<PADS4 PORT>/rdx/NDS.Services.Authentication/swagger/index.html#/
// http://<PADS4 IP>:<PADS4 PORT>/rdx/NDS.Services.Content/swagger/index.html#/

namespace FileApiExample
{
    class FileApiExample
    {
        private readonly HttpClient httpClient = new HttpClient();
        private string Url = "http://localhost:81/";

        public async Task<string> Authenticate(string user, string password, string domain, string url)
        {
            Url = url;
            string authUrl = Url + "rdx/NDS.Services.Authentication/api/v1/Account/Logon";
            AuthenticationContent authContent = new AuthenticationContent
            {
                Username = user,
                Password = password,
                Domain = domain
            };

            var authRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(authUrl),
                Content = new StringContent(JsonConvert.SerializeObject(authContent), Encoding.UTF8, "application/json")
            };
            JObject jResponse = await PostRequest(authRequest);
            AuthenticationResponse response = jResponse.ToObject<AuthenticationResponse>();
            if (response.Succeeded)
            {
                return response.Message;
            }
            else
            {
                return "unable to create Folder";
            }
        }

        #region Folder
        public async Task<string> GetFolder(string folderName, int startPage, int items, string searchPattern)
        {
            var folderUrl = Url + "rdx/NDS.Services.Content/api/v1/content/folder";

            FolderContent folderContent = new FolderContent
            {
                Path = folderName,
                IncludeHidden = true,
                SearchPattern = searchPattern,
                Paging = new paging
                {
                    Start = startPage,
                    Items = items
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
            JObject jResponse = await PostRequest(request);
            FolderResponse response = jResponse.ToObject<FolderResponse>();
            return response.TotalItems.ToString() + " items in the chosen folder";
        }

        public async Task<string> CreateFolder(string folderName, string parentFolder, bool isHidden)
        {
            var folderUrl = Url + "rdx/NDS.Services.Content/api/v1/content/createFolder";

            CreateFolderContent folderContent = new CreateFolderContent
            {
                ParentFolder = parentFolder,
                Name = folderName,
                Hidden = isHidden
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(folderUrl),
                Content = new StringContent(JsonConvert.SerializeObject(folderContent), Encoding.UTF8, "application/json")
            };
            JObject jResponse = await PostRequest(request);
            CreateFolderResponse response = jResponse.ToObject<CreateFolderResponse>();
            if (response.Succeeded)
            {
                return response.Code;
            }
            else
            {
                return "unable to create Folder";
            }
        }

        public async Task<string> MoveFolder(string folder, string folderDestination)
        {
            var folderUrl = Url + "rdx/NDS.Services.Content/api/v1/content/moveFolder";

            MoveFolderContent folderContent = new MoveFolderContent
            {
                MoveActions = new MoveFolderAction[1].Select(httpClient => new MoveFolderAction
                {
                    Folder = folder,
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
            JObject jResponse = await PostRequest(request);
            MoveFolderResponse response = jResponse.ToObject<MoveFolderResponse>();
            if (response.Succeeded)
            {
                return response.Code;
            }
            else
            {
                return "unable to move Folder";
            }
        }

        public async Task<string> DeleteFolder(string folderName)
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
            JObject jResponse = await PostRequest(request);
            DeleteFolderResponse response = jResponse.ToObject<DeleteFolderResponse>();
            if (response.Succeeded)
            {
                return response.Code;
            }
            else
            {
                return "unable to delete Folder";
            }
        }
        #endregion

        #region File
        public async Task<string> UploadFile(string file, string name, string fileName)
        {
            var folderUrl = Url + "rdx/NDS.Services.Content/api/v1/content/uploadFile";
            var fileStream = new FileStream(file, FileMode.Open);
            MultipartFormDataContent dataContent = new MultipartFormDataContent();
            HttpContent content = new StreamContent(fileStream);
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = name,
                FileName = fileName
            };
            dataContent.Add(content, name);
            UploadFileContent folderContent = new UploadFileContent
            {
                file = dataContent
            };
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(folderUrl),
                Content = folderContent.file
            };
            JObject jResponse = await PostRequest(request);
            UploadFileResponse response = jResponse.ToObject<UploadFileResponse>();
            if (response.Succeeded)
            {
                return response.Message;
            }
            else
            {
                return "unable to upload File";
            }
        }

        public async Task<string> MoveFile(string fileName, string folder, string folderDestination, string destinationFileName)
        {
            var folderUrl = Url + "rdx/NDS.Services.Content/api/v1/content/moveFolder";

            MoveFileContent folderContent = new MoveFileContent
            {
                MoveActions = new MoveFileAction[1].Select(httpClient => new MoveFileAction
                {
                    Folder = folder,
                    File = fileName,
                    DestinationFolder = folderDestination,
                    DestinationFileName = destinationFileName,
                    Force = true
                }).ToArray()
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(folderUrl),
                Content = new StringContent(JsonConvert.SerializeObject(folderContent), Encoding.UTF8, "application/json")
            };
            JObject jResponse = await PostRequest(request);
            MoveFileResponse response = jResponse.ToObject<MoveFileResponse>();
            if (response.Succeeded)
            {
                return response.Message;
            }
            else
            {
                return "unable to move File";
            }
        }

        public async Task<string> DeleteFile(string fileName, string folder)
        {
            var folderUrl = Url + "rdx/NDS.Services.Content/api/v1/content/deleteFolder";

            DeleteFileContent folderContent = new DeleteFileContent
            {
                DeleteActions = new DeleteFileAction[1].Select(h => new DeleteFileAction
                {
                    File = fileName,
                    Folder = folder
                }).ToArray()
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(folderUrl),
                Content = new StringContent(JsonConvert.SerializeObject(folderContent), Encoding.UTF8, "application/json")
            };
            JObject jResponse = await PostRequest(request);
            DeleteFileResponse response = jResponse.ToObject<DeleteFileResponse>();
            if (response.Succeeded)
            {
                return response.Code;
            }
            else
            {
                return "unable to delete File";
            }
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
