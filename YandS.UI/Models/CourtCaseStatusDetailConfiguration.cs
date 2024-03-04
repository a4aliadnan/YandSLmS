namespace YandS.UI.Models
{
    using System.Data.Entity.ModelConfiguration;
    public class CourtCaseStatusDetailConfiguration : EntityTypeConfiguration<CourtCaseStatusDetail>
    {
        public CourtCaseStatusDetailConfiguration()
        {
            this.HasRequired(s => s.CourtCases)
                .WithMany(g => g.StatusDetailId)
                .HasForeignKey<int>(s => s.CaseId);
        }
    }
}