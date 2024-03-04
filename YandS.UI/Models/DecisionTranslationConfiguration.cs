namespace YandS.UI.Models
{
    using System.Data.Entity.ModelConfiguration;
    public class DecisionTranslationConfiguration : EntityTypeConfiguration<DecisionTranslation>
    {
        public DecisionTranslationConfiguration()
        {
            this.HasRequired(s => s.CourtCases)
                .WithMany(g => g.TranslationId)
                .HasForeignKey<int>(s => s.CaseId);

            this.HasRequired(s => s.Created)
            .WithMany(g => g.TranslationCreatedby)
            .HasForeignKey<int>(s => s.CreatedBy)
            .WillCascadeOnDelete(false);

            this.HasOptional(s => s.Modified)
                 .WithMany(g => g.TranslationModifiedby)
                 .HasForeignKey<int?>(s => s.UpdatedBy)
                 .WillCascadeOnDelete(false);
        }
    }
}