using System.Data.Entity.ModelConfiguration;
using YandS.UI.Models;

public class CourtCasesConfiguration : EntityTypeConfiguration<CourtCases>
{
    public CourtCasesConfiguration()
    {
        this.HasRequired<ApplicationUser>(s => s.Created)
            .WithMany(g => g.CaseCreatedby)
            .HasForeignKey<int>(s => s.CreatedBy)
            .WillCascadeOnDelete(false);

        this.HasOptional<ApplicationUser>(s => s.Modified)
             .WithMany(g => g.CaseModifiedby)
             .HasForeignKey<int?>(s => s.UpdatedBy)
             .WillCascadeOnDelete(false);

        Property(a => a.ClaimAmount).HasPrecision(18, 3);
        Property(a => a.CorporateFee).HasPrecision(18, 3);

    }
}
