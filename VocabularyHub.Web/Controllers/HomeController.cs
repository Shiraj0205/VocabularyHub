using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VocabularyHub.Common.Models;
using VocabularyHub.Common.ViewModels;
using VocabularyHub.Web.Models;
using VocabularyHub.Web.Wrappers;

namespace VocabularyHub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpRestClientWrapper _httpRestClientWrapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        public HomeController(IHttpRestClientWrapper httpRestClientWrapper, IHostingEnvironment hostingEnvironment)
        {
            _httpRestClientWrapper = httpRestClientWrapper;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var restRequest = new Common.Models.HttpRequest
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
            var restRequest = new Common.Models.HttpRequest
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

            var restRequest = new Common.Models.HttpRequest
            {
                Url = "http://localhost:61462/api/vocabulary",
                HttpMethod = HttpMethod.Get
            };

            var response = await _httpRestClientWrapper.ExecuteGetAsync(restRequest);
            var vocabulariesViewModel = JsonConvert.DeserializeObject<VocabulariesViewModel>(response);
            return View("Index", vocabulariesViewModel);

        }

        public IActionResult LoadImportVocabulary(ImportVocabularyViewModel importVocabularyViewModel)
        {
            var tt = importVocabularyViewModel.TempleteFile;
            return View("ImportVocabulary");
        }

        public async Task<IActionResult> ImportVocabulary(ImportVocabularyViewModel importVocabularyViewModel)
        {
            var vocabularies = GetVocabularyRows(importVocabularyViewModel.TempleteFile);
            foreach(var vocabulary in vocabularies)
            {
               await _httpRestClientWrapper.ExecutePostAsync("http://localhost:61462/api/vocabulary", vocabulary);
            }
            return View();
        }

        private List<AddVocabularyViewModel> GetVocabularyRows(IFormFile file)
        {
            var vocabularyRows = new List<AddVocabularyViewModel>();
            const string folderName = "Upload";
            var newPath = Path.Combine(_hostingEnvironment.WebRootPath, folderName);

            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);

            var sFileExtension = Path.GetExtension(file.FileName).ToLower();
            var fileName = "VocabularyTemplate_" + DateTime.Now.ToFileTime() + sFileExtension;
            var fullPath = Path.Combine(newPath, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
                stream.Position = 0;
                ISheet sheet;
                if (sFileExtension == ".xls")
                {
                    var hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                    sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                }
                else
                {
                    var hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                    sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                }

                var headerRow = sheet.GetRow(0); //Get Header Row
                int cellCount = headerRow.LastCellNum;
                for (var i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                {
                    var row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                    var sentences = new List<SentenceViewModel>();

                    if (!string.IsNullOrWhiteSpace(row.GetCell(6).ToString()))
                    {
                        sentences.Add(new SentenceViewModel
                        {
                            SentenceExample = row.GetCell(6).ToString(),
                        });
                    }
                    if (!string.IsNullOrWhiteSpace(row.GetCell(7).ToString()))
                    {
                        sentences.Add(new SentenceViewModel
                        {
                            SentenceExample = row.GetCell(7).ToString(),
                        });
                    }
                    if (!string.IsNullOrWhiteSpace(row.GetCell(8).ToString()))
                    {
                        sentences.Add(new SentenceViewModel
                        {
                            SentenceExample = row.GetCell(8).ToString(),
                        });
                    }

                    var vocabularyRowDto = new AddVocabularyViewModel
                    {
                        Name = row.GetCell(0).ToString(),
                        Meaning = row.GetCell(1).ToString(),
                        MarathiMeaning = row.GetCell(2).ToString(),
                        Synonym = row.GetCell(3).ToString(),
                        Antonym = row.GetCell(4).ToString(),
                        Sentences = sentences,
                        SpeechPartId = 1,
                        TopicId = 1
                    };

                    vocabularyRows.Add(vocabularyRowDto);
                }
            }
            return vocabularyRows;
        }
    }
}
