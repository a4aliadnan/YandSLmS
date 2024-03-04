namespace YandS.UI.Models
{
    using System.Data.Entity.ModelConfiguration;
    public class CaseInvoiceFeeConfiguration : EntityTypeConfiguration<CaseInvoiceFee>
    {
        public CaseInvoiceFeeConfiguration()
        {
            HasRequired(s => s.CaseInvoice)
                .WithMany(g => g.FeeId)
                .HasForeignKey<int>(s => s.InvoiceId);

            Property(a => a.FeeAmount).HasPrecision(18, 3);
        }
    }
}