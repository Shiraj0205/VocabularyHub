using System;
using System.Collections.Generic;

namespace VocabularyHub.Persistence.DatabaseModels
{
    public class SpeechPart
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        //public ICollection<Vocabulary> Vocabulary { get; set; }
    }
}
