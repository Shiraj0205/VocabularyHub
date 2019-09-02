using System.Net.Http;

namespace VocabularyHub.Common.Models
{
    public class HttpRequest
    {
        public string Body { get; set; }
        public string Input { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public HttpMethod HttpMethod { get; set; }
    }
}
