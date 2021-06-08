using Newtonsoft.Json.Linq;
using System;
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
        private string bearerToken;
        static void Main()
        {            
            FileApiExample fai = new FileApiExample();
            fai.bearerToken = fai.Authenticate().Result;
            Console.WriteLine(fai.bearerToken);
            Console.WriteLine("Done");
        }

        public async void Run()
        {
            await Authenticate();
        }
        public async Task<string> Authenticate()
        {
            string authUrl = Url + "rdx/NDS.Services.Authentication/api/v1/Account/Logon";
            var authRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(authUrl),
                Content = new StringContent("{\"Username\":\"user1\",\"Password\":\"user1\",\"Domain\":\"pads\"}", Encoding.UTF8, "application/json")
            };
            try
            {
                HttpResponseMessage authResponse = await httpClient.SendAsync(authRequest);
                authResponse.EnsureSuccessStatusCode();
                string responseString = await authResponse.Content.ReadAsStringAsync();
                JObject responseObject = JObject.Parse(responseString);
                var token = responseObject["Claims"].First["Value"].ToString();
                return token;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e);
                return "";
            }
        }
    }
}
