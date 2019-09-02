using System.Collections.Generic;
using VocabularyHub.Common.ViewModels;
using VocabularyHub.Persistence.DatabaseModels;

namespace VocabularyHub.Persistence.Providers
{
    public interface IVocabularyDataProvider
    {
        Vocabulary GetVocabularyById(int id);
        VocabulariesViewModel GetVocabularies();
        IEnumerable<Topic> GetTopics();
        IEnumerable<SpeechPart> GetSpeechParts();
        AddVocabularyViewModel GetAddVocabularyPageLoad();
        Vocabulary AddVocabulary(AddVocabularyViewModel vocabularyViewModel);
    }
}
