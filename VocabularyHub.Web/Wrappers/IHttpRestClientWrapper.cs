using System.Threading.Tasks;
using VocabularyHub.Common.Models;

namespace VocabularyHub.Web.Wrappers
{
    public interface IHttpRestClientWrapper
    {
        Task<T> ExecuteGetAsync<T>(string path);
        Task<string> ExecutePostAsync<T>(string path, T postObject);
        //Task<ServiceResponse> ExecuteAsync(SMPRestRequest request);
        Task<string> ExecuteGetAsync(HttpRequest request);
    }
}
