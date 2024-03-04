namespace YandS.UI.Models
{
    using System.Data.Entity.ModelConfiguration;
    public class ClosurePartialDetailConfiguration : EntityTypeConfiguration<ClosurePartialDetail>
    {
        public ClosurePartialDetailConfiguration()
        {
            this.HasRequired(s => s.CourtCases)
                .WithMany(g => g.PartDetailId)
                .HasForeignKey<int>(s => s.CaseId);

            this.HasRequired(s => s.Created)
            .WithMany(g => g.ClosurePartialCreatedby)
            .HasForeignKey<int>(s => s.CreatedBy)
            .WillCascadeOnDelete(false);

            this.HasOptional(s => s.Modified)
                 .WithMany(g => g.ClosurePartialModifiedby)
                 .HasForeignKey<int?>(s => s.UpdatedBy)
                 .WillCascadeOnDelete(false);
        }
    }
}