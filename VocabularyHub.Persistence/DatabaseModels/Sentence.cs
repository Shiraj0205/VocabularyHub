using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VocabularyHub.Persistence.DatabaseModels
{
    public class Sentence
    {
        public int Id { get; set; }

        public int VocabularyId { get; set; }

        public string SentenceExample { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        //public Vocabulary Vocabulary { get; set; }
    }
}
