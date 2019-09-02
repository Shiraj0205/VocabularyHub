using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VocabularyHub.Common.ViewModels;
using VocabularyHub.Persistence.DatabaseContext;
using VocabularyHub.Persistence.DatabaseModels;

namespace VocabularyHub.Persistence.Providers
{
    public class VocabularyDataProvider : IVocabularyDataProvider
    {
        private readonly VocabContext _vocabContext;
        private readonly string _createdBy = "Admin";

        public VocabularyDataProvider(VocabContext vocabContext)
        {
            _vocabContext = vocabContext;
        }

        public Vocabulary GetVocabularyById(int id)
        {
            return _vocabContext.Vocabulary.Where(v => v.Id == id)
                .Include(t => t.Topic)
                .Include(s => s.Sentences)
                .FirstOrDefault();
        }

        public VocabulariesViewModel GetVocabularies()
        {
           var vocabularies = (from vocabulary in _vocabContext.Vocabulary
                .Include(t => t.Topic)
                .Include(s => s.Sentences)
                    select new VocabularyViewModel
                    {
                        Id = vocabulary.Id,
                        Name = vocabulary.Name,
                        SpeechPartId = vocabulary.SpeechPartId,
                        SpeechPartName = vocabulary.SpeechPart.Name,
                        TopicId = vocabulary.TopicId,
                        TopicName = vocabulary.Topic.Name,
                        Synonym = vocabulary.Synonym,
                        Antonym = vocabulary.Antonym,
                        MarathiMeaning = vocabulary.MarathiMeaning,
                        Meaning = vocabulary.Meaning,
                        Sentences = (from sentence in vocabulary.Sentences
                                     where !string.IsNullOrWhiteSpace(sentence.SentenceExample)
                                     select new SentenceViewModel()
                                     {
                                         Id = sentence.Id,
                                         SentenceExample = sentence.SentenceExample
                                     }).ToList(),
                        CreatedBy = _createdBy,
                        CreatedOn = DateTime.Now,
                        UpdatedBy = _createdBy,
                        UpdatedOn = DateTime.Now
                    }).ToList();
            return new VocabulariesViewModel
            {
                Vocabularies = vocabularies
            };
        }

        public IEnumerable<Topic> GetTopics()
        {
            return _vocabContext.Topic;
        }

        public IEnumerable<SpeechPart> GetSpeechParts()
        {
            return _vocabContext.SpeechPart;
        }

        public AddVocabularyViewModel GetAddVocabularyPageLoad()
        {
            return new AddVocabularyViewModel
            {
                Id = 0,
                Name = string.Empty,
                Meaning = string.Empty,
                SpeechPartId = 1,
                TopicId = 1,
                Synonym = string.Empty,
                Antonym = string.Empty,
                MarathiMeaning = string.Empty,
                Topics = (from topic in _vocabContext.Topic
                          select new TopicViewModel
                          {
                              Id = topic.Id,
                              Name = topic.Name
                          }),
                TopicListItems = (from topic in _vocabContext.Topic
                                  select new SelectListItem
                                  {
                                      Value = topic.Id.ToString(),
                                      Text = topic.Name
                                  }),
                SpeechParts = (from sppechPart in _vocabContext.SpeechPart
                               select new SpeechPartViewModel
                               {
                                   Id = sppechPart.Id,
                                   Name = sppechPart.Name
                               }),
                SpeechPartListItems = (from speechPart in _vocabContext.SpeechPart
                                       select new SelectListItem
                                       {
                                           Value = speechPart.Id.ToString(),
                                           Text = speechPart.Name
                                       }),
                Sentences = new List<SentenceViewModel>()
                {
                    new SentenceViewModel
                    {
                        Id = 1,
                        SentenceExample = ""
                    },
                    new SentenceViewModel
                    {
                        Id = 2,
                        SentenceExample = ""
                    },
                    new SentenceViewModel
                    {
                        Id = 3,
                        SentenceExample = ""
                    },
                    new SentenceViewModel
                    {
                        Id = 4,
                        SentenceExample = ""
                    }
                }
            };
        }

        public Vocabulary AddVocabulary(AddVocabularyViewModel vocabularyViewModel)
        {
            var vocabulary = new Vocabulary
            {
                Name = vocabularyViewModel.Name,
                SpeechPartId = vocabularyViewModel.SpeechPartId,
                TopicId = vocabularyViewModel.TopicId,
                Synonym = vocabularyViewModel.Synonym,
                Antonym = vocabularyViewModel.Antonym,
                MarathiMeaning = vocabularyViewModel.MarathiMeaning,
                Meaning = vocabularyViewModel.Meaning,
                Sentences = (from sentence in vocabularyViewModel.Sentences
                             where !string.IsNullOrWhiteSpace(sentence.SentenceExample)
                             select new Sentence()
                             {
                                 SentenceExample = sentence.SentenceExample,
                                 CreatedBy = _createdBy,
                                 CreatedOn = DateTime.Now,
                                 UpdatedBy = _createdBy,
                                 UpdatedOn = DateTime.Now
                             }).ToList(),
                CreatedBy = _createdBy,
                CreatedOn = DateTime.Now,
                UpdatedBy = _createdBy,
                UpdatedOn = DateTime.Now
            };
            _vocabContext.Vocabulary.Add(vocabulary);
            _vocabContext.SaveChanges();
            return vocabulary;

        }
    }
}
