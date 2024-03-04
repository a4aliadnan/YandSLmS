namespace YandS.UI.Models
{
    using System.Data.Entity.ModelConfiguration;
    public class CourtCasesEnforcementConfiguration : EntityTypeConfiguration<CourtCasesEnforcement>
    {
        public CourtCasesEnforcementConfiguration()
        {
            HasRequired(s => s.CourtCases)
                .WithMany(g => g.EnforcementId)
                .HasForeignKey(s => s.CaseId);

            HasRequired(s => s.Created)
            .WithMany(g => g.EnforcementCreatedby)
            .HasForeignKey(s => s.CreatedBy)
            .WillCascadeOnDelete(false);

            HasOptional(s => s.Modified)
                .WithMany(g => g.EnforcementModifiedby)
                .HasForeignKey<int?>(s => s.UpdatedBy)
                .WillCascadeOnDelete(false);
        }
    }
}