using System.Collections.Generic;
using VocabularyHub.Common.ViewModels;
using VocabularyHub.Persistence.DatabaseModels;
using VocabularyHub.Persistence.Providers;

namespace VocabularyHub.Application
{
    public class VocabularyManager : IVocabularyManager
    {
        private readonly IVocabularyDataProvider _dataProvider;

        public VocabularyManager(IVocabularyDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public Vocabulary AddVocabulary(AddVocabularyViewModel vocabularyViewModel)
        {
            return _dataProvider.AddVocabulary(vocabularyViewModel);
        }

        public AddVocabularyViewModel GetAddVocabularyPageLoad()
        {
            return _dataProvider.GetAddVocabularyPageLoad();
        }

        public IEnumerable<SpeechPart> GetSpeechParts()
        {
            return _dataProvider.GetSpeechParts();
        }

        public IEnumerable<Topic> GetTopics()
        {
            return _dataProvider.GetTopics();
        }

        public VocabulariesViewModel GetVocabularies()
        {
            return _dataProvider.GetVocabularies();
        }

        public Vocabulary GetVocabularyById(int id)
        {
            return _dataProvider.GetVocabularyById(id);
        }
    }
}
