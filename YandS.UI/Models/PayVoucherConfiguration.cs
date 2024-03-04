using System.Data.Entity.ModelConfiguration;
using YandS.UI.Models;

public class PayVoucherConfiguration : EntityTypeConfiguration<PayVoucher>
{
    public PayVoucherConfiguration()
    {
        HasOptional(s => s.CourtCases)
            .WithMany(g => g.Voucher_No)
            .HasForeignKey<int?>(s => s.CaseId)
            .WillCascadeOnDelete(false);

        HasRequired<ApplicationUser>(s => s.Created)
            .WithMany(g => g.PVCreatedby)
            .HasForeignKey<int>(s => s.CreatedBy)
            .WillCascadeOnDelete(false);



        HasOptional<ApplicationUser>(s => s.Modified)
             .WithMany(g => g.PVModifiedby)
             .HasForeignKey<int?>(s => s.UpdatedBy)
             .WillCascadeOnDelete(false);

        Property(a => a.Amount).HasPrecision(18, 3);
        Property(a => a.VatAmount).HasPrecision(18, 3);

    }

}
