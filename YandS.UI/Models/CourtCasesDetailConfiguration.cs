namespace YandS.UI.Models
{
    using System.Data.Entity.ModelConfiguration;
    public class CourtCasesDetailConfiguration : EntityTypeConfiguration<CourtCasesDetail>
    {
        public CourtCasesDetailConfiguration()
        {
            this.HasRequired(s => s.CourtCases)
                .WithMany(g => g.DetailId)
                .HasForeignKey<int>(s => s.CaseId);

            this.HasRequired(s => s.Created)
            .WithMany(g => g.CaseDetailCreatedby)
            .HasForeignKey<int>(s => s.CreatedBy)
            .WillCascadeOnDelete(false);

            this.HasOptional(s => s.Modified)
                 .WithMany(g => g.CaseDetailModifiedby)
                 .HasForeignKey<int?>(s => s.UpdatedBy)
                 .WillCascadeOnDelete(false);
        }
    }
}