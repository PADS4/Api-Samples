using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScheduleApiExample.RequestModels.Schedule;
using ScheduleApiExample.RequestModels.Viewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

// SWAGGER URLS
// http://<PADS4 IP>:<PADS4 PORT>/rdx/NDS.Services.Authentication/swagger/index.html#/
// http://<PADS4 IP>:<PADS4 PORT>/rdx/NDS.Services.Content/swagger/index.html#/

namespace ScheduleApiExample
{
    class ScheduleApiExample
    {
        private readonly HttpClient httpClient = new HttpClient();
        private string Url = "";
        private IList<Viewer> Viewers;
        private IList<Schedule> Schedules;

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
            JObject jResponse = await HttpRequest(authRequest);
            AuthenticationResponse response = jResponse.ToObject<AuthenticationResponse>();
            if (response.Succeeded)
            {
                return response.Message;
            }
            else
            {
                return "unable to Authenticate: " + response.Message;
            }
        }

        #region Schedule
        public async Task<string> GetSchedules(int startPage, int items, string searchPattern)
        {
            var scheduleUrl = Url + "rdx/NDS.Services.Viewer/api/v1/Viewer/Schedule";

            ScheduleFilter scheduleFilter = new ScheduleFilter
            {
                Filter = new filter
                {
                    SearchString = searchPattern,
                    //these can be filled with IDs to filter your search:
                    //Destinations = new List<string> { "" }, 
                    //ScheduleIds = new List<string> { "" },
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddDays(7),
                    ScheduleTypeFilter = new List<int> { 0, 1 }
                },
                Sorting = new sorting
                {
                    SortBy = 0,
                    Descending = true
                },
                Paging = new paging
                {
                    Start = startPage,
                    Items = items
                }
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(scheduleUrl),
                Content = new StringContent(JsonConvert.SerializeObject(scheduleFilter), Encoding.UTF8, "application/json")
            };
            JObject jResponse = await HttpRequest(request);
            SchedulesResponse response = jResponse.ToObject<SchedulesResponse>();
            Schedules = response.Schedules;
            return response.TotalItems + " schedules already planned";
        }

        public async Task<string> CreateSchedule()
        {
            var scheduleUrl = Url + "rdx/NDS.Services.Viewer/api/v1/Viewer/Schedule/create";
            //byte[] data = File.ReadAllBytes("../Assets/andrea.padsx");

            CreateSchedulesRequest scheduleRequest = new CreateSchedulesRequest
            {
                Name = "new schedule",
                Presentations = new List<presentation>
                {
                    new presentation
                    {
                        Name = "new presentation",
                        FileName = "andrea.padsx",
                        //Data = data
                    }
                },
                Items = new List<Schedule> {
                    new Schedule
                    {
                        DestinationId = Viewers[0].Id,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(1),
                        Presentations = new List<presentation>
                        {
                            new presentation
                            {
                                Name = "new presentation",
                                FileName = "andrea.padsx",
                                //Data = data
                            }
                        },
                        IsMessage = false
                    }
                },

            };


            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(scheduleUrl),
                Content = new StringContent(JsonConvert.SerializeObject(scheduleRequest), Encoding.UTF8, "application/json")
            };
            JObject jResponse = await HttpRequest(request);
            return jResponse.ToString();
        }

        public async Task<string> DeleteSchedules()
        {
            if (Schedules.Count > 0)
            {
                var scheduleUrl = Url + "rdx/NDS.Services.Viewer/api/v1/Viewer/Schedule/delete";
                List<Guid> scheduleIds = new List<Guid>();
                scheduleIds.Add(Schedules[0].Id);

                DeleteSchedulesRequest scheduleRequest = new DeleteSchedulesRequest
                {
                    ScheduleIds = scheduleIds,
                };

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(scheduleUrl),
                    Content = new StringContent(JsonConvert.SerializeObject(scheduleRequest), Encoding.UTF8, "application/json")
                };
                JObject jResponse = await HttpRequest(request);
                BasicResponse response = jResponse.ToObject<BasicResponse>();
                return response.Message;
            }
            return "No schedules found to delete";
        }

        public async Task<string> DeleteContent(string contentId)
        {
            var scheduleUrl = Url + "rdx/NDS.Services.Viewer/api/v1/Viewer/Schedule/content/delete";

            DeleteContentRequest scheduleRequest = new DeleteContentRequest
            {
                ContentId = contentId,
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(scheduleUrl),
                Content = new StringContent(JsonConvert.SerializeObject(scheduleRequest), Encoding.UTF8, "application/json")
            };
            JObject jResponse = await HttpRequest(request);
            BasicResponse response = jResponse.ToObject<BasicResponse>();
            return response.Message;
        }
        #endregion

        #region Viewer
        public async Task<string> GetViewers()
        {
            var scheduleUrl = Url + "rdx/NDS.Services.Viewer/api/v1/Viewer";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(scheduleUrl)
            };
            JObject jResponse = await HttpRequest(request);
            ViewersResponse response = jResponse.ToObject<ViewersResponse>();
            return response.TotalItems + " viewers found";
        }

        public async Task<string> GetViewerNames(int startItem, int items, IList<string> viewerTypes)
        {
            var scheduleUrl = Url + "rdx/NDS.Services.Viewer/api/v1/Viewer/names";

            var namesRequest = new ViewerNamesRequest
            {
                Paging = new paging
                {
                    Start = startItem,
                    Items = items
                },
                ViewerTypes = viewerTypes
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(scheduleUrl),
                Content = new StringContent(JsonConvert.SerializeObject(namesRequest), Encoding.UTF8, "application/json")
            };

            JObject jResponse = await HttpRequest(request);
            ViewerNamesResponse response = jResponse.ToObject<ViewerNamesResponse>();
            Viewers = response.Viewers;
            return response.TotalItems + " viewers found";
        }
        #endregion

        private async Task<JObject> HttpRequest(HttpRequestMessage authRequest)
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
