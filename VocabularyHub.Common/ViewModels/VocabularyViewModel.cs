using System;
using System.Collections.Generic;
using System.Text;

namespace VocabularyHub.Common.ViewModels
{
    public class VocabularyViewModel
    {
        public int Id { get; set; }

        public int TopicId { get; set; }

        public string TopicName { get; set; }

        public int SpeechPartId { get; set; }

        public string SpeechPartName { get; set; }

        public string Name { get; set; }

        public string Meaning { get; set; }

        public string Synonym { get; set; }

        public string Antonym { get; set; }

        public string MarathiMeaning { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public List<SentenceViewModel> Sentences { get; set; }
    }

    public class VocabulariesViewModel
    {
        public List<VocabularyViewModel> Vocabularies { get; set; }
    }
}
