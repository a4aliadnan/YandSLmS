namespace YandS.UI.Models
{
    using System.Data.Entity.ModelConfiguration;
    public class CaseInvoiceFeeCalculationConfiguration : EntityTypeConfiguration<CaseInvoiceFeeCalculation>
    {
        public CaseInvoiceFeeCalculationConfiguration()
        {
            HasRequired(s => s.CaseInvoice)
                .WithMany(g => g.FeeCaclId)
                .HasForeignKey(s => s.InvoiceId);

            Property(a => a.ClaimAmountPct).HasPrecision(18, 3);
            Property(a => a.FeeAmountPct).HasPrecision(18, 3);
            Property(a => a.ClaimAmount).HasPrecision(18, 3);
        }
    }
}