using Autofac;

namespace VocabularyHub.Application
{
    public class ApplicationAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VocabularyManager>()
                .As<IVocabularyManager>();
        }
    }
}