using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VocabularyHub.Common.Models;

namespace VocabularyHub.Web.Wrappers
{
    public class HttpRestClientWrapper : IHttpRestClientWrapper
    {
        private readonly HttpClient _client;
        public HttpRestClientWrapper()
        {
            _client = new HttpClient();
        }

        public async Task<T> ExecuteGetAsync<T>(string path)
        {
            try
            {
                var response = await _client.GetStringAsync(GetUrl(path));

                return JsonConvert.DeserializeObject<T>(response, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore
                });
            }
            catch
            {
                return default(T);
            }
        }

        public async Task<string> ExecuteGetAsync(HttpRequest request)
        {
            string responseMessage = string.Empty;
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _client.DefaultRequestHeaders.Add("cache-control", "no-cache");
                //if (!_client.DefaultRequestHeaders.Contains(request.OssAuthToken))
                //    _client.DefaultRequestHeaders.Add(request.OssAuthToken, request.OssAuthTokenValue);
                responseMessage = await _client.GetStringAsync(request.Url);
            }
            catch
            { }

            return responseMessage;
        }

        public async Task<string> ExecutePostAsync<T>(string path, T postObject)
        {
            try
            {
                var jsonObject = JsonConvert.SerializeObject(postObject);

                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                //var result = await _client.PostAsync(GetUrl(path), content);
                var result = await _client.PostAsync(path, content);
                var response = result.Content.ReadAsStringAsync();

                return response.Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when calling post method: {path}", ex);
            }
        }

        private string GetUrl(string path)
        {
            return $"{_client.BaseAddress}/{path}";
        }
    }
}
