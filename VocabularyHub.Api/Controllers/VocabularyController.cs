using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VocabularyHub.Application;
using Newtonsoft.Json;
using VocabularyHub.Common.ViewModels;

namespace VocabularyHub.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Vocabulary")]
    public class VocabularyController : Controller
    {
        private readonly IVocabularyManager _vocabularyManager;

        
        public VocabularyController(IVocabularyManager vocabularyManager)
        {
            _vocabularyManager = vocabularyManager;
        }

        // GET: api/Vocabulary
        [HttpGet]
        public IActionResult Get()
        {
            var response = _vocabularyManager.GetVocabularies();
            //var tt = JsonConvert.SerializeObject(response);
            return Ok(response);
        }

        // GET: api/Vocabulary/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var response = _vocabularyManager.GetVocabularyById(id);
            return Ok(response);
        }
        
        // POST: api/Vocabulary
        [HttpPost]
        public IActionResult Post([FromBody] AddVocabularyViewModel vocabularyViewModel)
        {
            var vocabulary = _vocabularyManager.AddVocabulary(vocabularyViewModel);
            return Ok(vocabulary);
        }
        
        // PUT: api/Vocabulary/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        [HttpGet("topics", Name = "GetTopics")]
        public IActionResult GetTopics(int id)
        {
            var response = _vocabularyManager.GetTopics();
            return Ok(response);
        }

        [HttpGet("speechParts", Name = "GetSpeechParts")]
        public IActionResult GetSpeechParts(int id)
        {
            var response = _vocabularyManager.GetSpeechParts();
            return Ok(response);
        }

        [HttpGet("add/pageLoad", Name = "GetAddVocabularyPageLoad")]
        public IActionResult GetAddVocabularyPageLoad(int id)
        {
            var response = _vocabularyManager.GetAddVocabularyPageLoad();
            return Ok(response);
        }
    }
}
