using System;
using System.Collections.Generic;

namespace VocabularyHub.Persistence.DatabaseModels
{
    public class Vocabulary
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

        public Topic Topic { get; set; }

        public SpeechPart SpeechPart { get; set; }

        public List<Sentence> Sentences { get; set; }
    }
}
