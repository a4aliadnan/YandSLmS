namespace YandS.UI.Models
{
    using System.Data.Entity.ModelConfiguration;
    public class SessionsRollConfiguration : EntityTypeConfiguration<SessionsRoll>
    {
        public SessionsRollConfiguration()
        {
            this.HasRequired(s => s.CourtCases)
                .WithMany(g => g.SessionRollId)
                .HasForeignKey<int>(s => s.CaseId);

            this.HasRequired(s => s.Created)
            .WithMany(g => g.SessionRollCreatedby)
            .HasForeignKey<int>(s => s.CreatedBy)
            .WillCascadeOnDelete(false);

            this.HasOptional(s => s.Modified)
                 .WithMany(g => g.SessionRollModifiedby)
                 .HasForeignKey<int?>(s => s.UpdatedBy)
                 .WillCascadeOnDelete(false);

            Property(a => a.PrimaryFinalJDAmount).HasPrecision(18, 3);
            Property(a => a.AppealFinalJDAmount).HasPrecision(18, 3);
            Property(a => a.SupremeFinalJDAmount).HasPrecision(18, 3);
        }
    }
}