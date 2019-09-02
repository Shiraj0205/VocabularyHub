using Microsoft.EntityFrameworkCore;
using VocabularyHub.Persistence.DatabaseModels;

namespace VocabularyHub.Persistence.DatabaseContext
{
    public class VocabContext : DbContext
    {
        public VocabContext(DbContextOptions<VocabContext> context) : base(context) { }

        public virtual DbSet<Vocabulary> Vocabulary { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<SpeechPart> SpeechPart { get; set; }
        public virtual DbSet<Sentence> Sentence { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vocabulary>(entity =>
            {
                //entity.HasOne<Topic>(s => s.Topic)
                //    .WithMany(ad => ad.Vocabulary)
                //    .HasForeignKey(ad => ad.TopicId)
                //    .HasConstraintName("FK_TopicId");

                //entity.HasOne<SpeechPart>(s => s.SpeechPart)
                //    .WithMany(ad => ad.Vocabulary)
                //    .HasForeignKey(ad => ad.SpeechPartId)
                //    .HasConstraintName("FK_SpeechPartId");
            });

            //modelBuilder.Entity<Sentence>(entity =>
            //{
            //    entity.HasOne<Vocabulary>(s => s.VocabularyId)
            //        .WithMany(ad => ad.Sentences)
            //        .HasForeignKey(ad => ad.VocabularyId)
            //        .HasConstraintName("FK_VocabularyId");
            //});
        }
    }
}
