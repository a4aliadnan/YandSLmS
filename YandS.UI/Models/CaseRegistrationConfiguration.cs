namespace YandS.UI.Models
{
    using System.Data.Entity.ModelConfiguration;
    public class CaseRegistrationConfiguration : EntityTypeConfiguration<CaseRegistration>
    {
        public CaseRegistrationConfiguration()
        {
            this.HasRequired(s => s.CourtCases)
                .WithMany(g => g.CaseRegistrationId)
                .HasForeignKey<int>(s => s.CaseId);

            this.HasRequired(s => s.Created)
            .WithMany(g => g.CaseRegistrationCreatedby)
            .HasForeignKey<int>(s => s.CreatedBy)
            .WillCascadeOnDelete(false);

            this.HasOptional(s => s.Modified)
                 .WithMany(g => g.CaseRegistrationModifiedby)
                 .HasForeignKey<int?>(s => s.UpdatedBy)
                 .WillCascadeOnDelete(false);
        }
    }
}