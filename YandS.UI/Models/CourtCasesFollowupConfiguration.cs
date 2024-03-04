namespace YandS.UI.Models
{
    using System.Data.Entity.ModelConfiguration;
    public class CourtCasesFollowupConfiguration : EntityTypeConfiguration<CourtCasesFollowup>
    {
        public CourtCasesFollowupConfiguration()
        {
            this.HasRequired(s => s.CourtCasesDetail)
                .WithMany(g => g.FollowupId)
                .HasForeignKey<int>(s => s.DetailId);

            this.HasRequired(s => s.Created)
            .WithMany(g => g.CaseFollowupCreatedby)
            .HasForeignKey<int>(s => s.CreatedBy)
            .WillCascadeOnDelete(false);

            this.HasOptional(s => s.Modified)
                 .WithMany(g => g.CaseFollowupModifiedby)
                 .HasForeignKey<int?>(s => s.UpdatedBy)
                 .WillCascadeOnDelete(false);
        }
    }
}