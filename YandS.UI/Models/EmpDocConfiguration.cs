namespace YandS.UI.Models
{
    using System.Data.Entity.ModelConfiguration;
    public class EmpDocConfiguration : EntityTypeConfiguration<EmpDoc>
    {
        public EmpDocConfiguration()
        {
            this.HasRequired<Employees>(s => s.Employees)
                .WithMany(g => g.DocId)
                .HasForeignKey<int>(s => s.EmployeId);
        }
    }
}