using Autofac;
using VocabularyHub.Persistence.Providers;

namespace VocabularyHub.Persistence
{
    public class PersistenceAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VocabularyDataProvider>()
                .As<IVocabularyDataProvider>()
                .InstancePerDependency();
        }
    }
}
