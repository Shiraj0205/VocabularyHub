using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using VocabularyHub.Application;
using VocabularyHub.Common.Models;
using VocabularyHub.Common.ViewModels;
using VocabularyHub.Web.Models;
using VocabularyHub.Web.Wrappers;

namespace VocabularyHub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpRestClientWrapper _httpRestClientWrapper;
        public HomeController(IHttpRestClientWrapper httpRestClientWrapper)
        {
            _httpRestClientWrapper = httpRestClientWrapper;
        }

        public async Task<IActionResult> Index()
        {
            var restRequest = new HttpRequest
            {
                Url = "http://localhost:61462/api/vocabulary",
                HttpMethod = HttpMethod.Get
            };

            var response = await _httpRestClientWrapper.ExecuteGetAsync(restRequest);
            var vocabulariesViewModel = JsonConvert.DeserializeObject<VocabulariesViewModel>(response);
            return View(vocabulariesViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> AddVocabulary()
        {
            var restRequest = new HttpRequest
            {
                //Url = string.Format(_transactionServicesSettings.Value.EndPoint + StringFormat.GetMonNumber, transactionId),
                Url = "http://localhost:61462/api/vocabulary/add/pageload",
                HttpMethod = HttpMethod.Get
            };

            var response = await _httpRestClientWrapper.ExecuteGetAsync(restRequest);
            if (string.IsNullOrWhiteSpace(response))
                return null;

            var addVocabularyViewModel = JsonConvert.DeserializeObject<AddVocabularyViewModel>(response);

            return View(addVocabularyViewModel);
        }

        public async Task<IActionResult> SaveVocabulary(AddVocabularyViewModel vocabularyViewModel)
        {
            var vocabulary = await _httpRestClientWrapper.ExecutePostAsync("http://localhost:61462/api/vocabulary", vocabularyViewModel);

            /*var restRequest = new HttpRequest
            {
                //Url = string.Format(_transactionServicesSettings.Value.EndPoint + StringFormat.GetMonNumber, transactionId),
                Url = "http://localhost:61462/api/vocabulary/add/pageload",
                HttpMethod = HttpMethod.Get
            };
            var response = await _httpRestClientWrapper.ExecuteGetAsync(restRequest);
            var addVocabularyViewModel = JsonConvert.DeserializeObject<AddVocabularyViewModel>(response);

            return View("AddVocabulary", addVocabularyViewModel);*/

            var restRequest = new HttpRequest
            {
                Url = "http://localhost:61462/api/vocabulary",
                HttpMethod = HttpMethod.Get
            };

            var response = await _httpRestClientWrapper.ExecuteGetAsync(restRequest);
            var vocabulariesViewModel = JsonConvert.DeserializeObject<VocabulariesViewModel>(response);
            return View("Index", vocabulariesViewModel);

        }
    }
}
