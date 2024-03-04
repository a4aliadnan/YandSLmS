namespace YandS.UI.Models
{
    using System.Data.Entity.ModelConfiguration;
    public class CaseInvoiceConfiguration : EntityTypeConfiguration<CaseInvoice>
    {
        public CaseInvoiceConfiguration()
        {
            this.HasRequired(s => s.CourtCases)
                .WithMany(g => g.InvoiceId)
                .HasForeignKey<int>(s => s.CaseId);

            this.HasRequired(s => s.Created)
            .WithMany(g => g.CaseInvoiceCreatedby)
            .HasForeignKey<int>(s => s.CreatedBy)
            .WillCascadeOnDelete(false);

            this.HasOptional(s => s.Modified)
                 .WithMany(g => g.CaseInvoiceModifiedby)
                 .HasForeignKey<int?>(s => s.UpdatedBy)
                 .WillCascadeOnDelete(false);

            Property(a => a.HourlyRate).HasPrecision(18, 3);
            Property(a => a.FixedAmount).HasPrecision(18, 3);

        }
    }
}