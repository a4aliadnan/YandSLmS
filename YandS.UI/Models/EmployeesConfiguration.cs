using System.Data.Entity.ModelConfiguration;
using YandS.UI.Models;

public class EmployeesConfiguration : EntityTypeConfiguration<Employees>
{
    public EmployeesConfiguration()
    {
        HasRequired<ApplicationUser>(s => s.Created)
            .WithMany(g => g.EmpCreatedby)
            .HasForeignKey<int>(s => s.CreatedBy)
            .WillCascadeOnDelete(false);

        HasOptional<ApplicationUser>(s => s.Modified)
             .WithMany(g => g.EmpModifiedby)
             .HasForeignKey<int?>(s => s.UpdatedBy)
             .WillCascadeOnDelete(false);

        Property(a => a.BasicSalary).HasPrecision(18, 3);

        Property(a => a.Allowance).HasPrecision(18, 3);

    }
}
