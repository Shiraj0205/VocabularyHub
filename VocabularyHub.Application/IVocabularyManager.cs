using System.Collections.Generic;
using VocabularyHub.Common.ViewModels;
using VocabularyHub.Persistence.DatabaseModels;

namespace VocabularyHub.Application
{
    public interface IVocabularyManager
    {
        Vocabulary GetVocabularyById(int id);
        VocabulariesViewModel GetVocabularies();
        IEnumerable<Topic> GetTopics();
        IEnumerable<SpeechPart> GetSpeechParts();
        AddVocabularyViewModel GetAddVocabularyPageLoad();
        Vocabulary AddVocabulary(AddVocabularyViewModel vocabularyViewModel);
    }
}
