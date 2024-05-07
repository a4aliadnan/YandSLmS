namespace YandS.UI.Models
{
    using System.Data.Entity.ModelConfiguration;
    public class DefendentContactConfiguration : EntityTypeConfiguration<DefendentContact>
    {
        public DefendentContactConfiguration()
        {
            this.HasRequired(s => s.CourtCases)
                .WithMany(g => g.DefendentContactId)
                .HasForeignKey<int>(s => s.CaseId);

            this.HasRequired(s => s.Created)
            .WithMany(g => g.DefendentContactCreatedby)
            .HasForeignKey<int>(s => s.CreatedBy)
            .WillCascadeOnDelete(false);

            this.HasOptional(s => s.Modified)
                 .WithMany(g => g.DefendentContactModifiedby)
                 .HasForeignKey<int?>(s => s.UpdatedBy)
                 .WillCascadeOnDelete(false);
        }
    }
}