using System.Data.Entity.ModelConfiguration;
using YandS.UI.Models;

public class MasterSetupConfiguration : EntityTypeConfiguration<MasterSetups>
{
    public MasterSetupConfiguration()
    {
        this.HasRequired<ApplicationUser>(s => s.Created)
            .WithMany(g => g.MasterSetupsCreatedby)
            .HasForeignKey<int>(s => s.CreatedBy)
            .WillCascadeOnDelete(false);



        this.HasOptional<ApplicationUser>(s => s.Modified)
             .WithMany(g => g.MasterSetupsModifiedby)
             .HasForeignKey<int?>(s => s.UpdatedBy)
             .WillCascadeOnDelete(false);

    }

}
