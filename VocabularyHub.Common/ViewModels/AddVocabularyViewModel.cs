using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace VocabularyHub.Common.ViewModels
{
    public class AddVocabularyViewModel
    {
        public int Id { get; set; }

        public int TopicId { get; set; }

        public int SpeechPartId { get; set; }

        public string Name { get; set; }

        public string Meaning { get; set; }

        public string Synonym { get; set; }

        public string Antonym { get; set; }

        public string MarathiMeaning { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public IEnumerable<TopicViewModel> Topics { get; set; }

        public IEnumerable<SelectListItem> TopicListItems { get; set; }

        public IEnumerable<SpeechPartViewModel> SpeechParts { get; set; }
        public IEnumerable<SelectListItem> SpeechPartListItems { get; set; }

        public List<SentenceViewModel> Sentences { get; set; }
    }

    public class TopicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SpeechPartViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SentenceViewModel
    {
        public int Id { get; set; }
        public string SentenceExample { get; set; }
    }
}
