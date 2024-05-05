using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using YandS.UI.DatabaseInitializer;

namespace YandS.UI.Models
{
    public class ApplicationUserLogin : IdentityUserLogin<int> { }
    public class ApplicationUserClaim : IdentityUserClaim<int> { }

    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public ApplicationUserRole()
            : base()
        { }

        public ApplicationRole Role { get; set; }

        public bool IsPermissionInRole(string _permission)
        {
            bool _retVal = false;
            try
            {
                _retVal = this.Role.IsPermissionInRole(_permission);
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public bool IsSysAdmin { get { return this.Role.IsSysAdmin; } }
    }

    public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public DateTime LastModified { get; set; }

        public bool Inactive { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public ApplicationUser()
        {
            LastModified = DateTime.Now;
            Inactive = false;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public bool IsPermissionInUserRoles(string _permission)
        {
            bool _retVal = false;
            try
            {
                foreach (ApplicationUserRole _role in this.Roles)
                {
                    if (_role.IsPermissionInRole(_permission))
                    {
                        _retVal = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public bool IsSysAdmin()
        {
            bool _retVal = false;
            try
            {
                foreach (ApplicationUserRole _role in this.Roles)
                {
                    if (_role.IsSysAdmin)
                    {
                        _retVal = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        #region ICollections Foreign Key Refernece for Created by and Modified By

        public ICollection<MasterSetups> MasterSetupsCreatedby { get; set; }
        public ICollection<MasterSetups> MasterSetupsModifiedby { get; set; }
        public ICollection<Employees> EmpCreatedby { get; set; }
        public ICollection<Employees> EmpModifiedby { get; set; }
        public ICollection<PayVoucher> PVCreatedby { get; set; }
        public ICollection<PayVoucher> PVModifiedby { get; set; }
        public ICollection<CourtCases> CaseCreatedby { get; set; }
        public ICollection<CourtCases> CaseModifiedby { get; set; }
        public ICollection<CourtCasesDetail> CaseDetailCreatedby { get; set; }
        public ICollection<CourtCasesDetail> CaseDetailModifiedby { get; set; }
        public ICollection<CourtCasesFollowup> CaseFollowupCreatedby { get; set; }
        public ICollection<CourtCasesFollowup> CaseFollowupModifiedby { get; set; }
        public ICollection<CaseInvoice> CaseInvoiceCreatedby { get; set; }
        public ICollection<CaseInvoice> CaseInvoiceModifiedby { get; set; }
        public ICollection<CourtCasesEnforcement> EnforcementCreatedby { get; set; }
        public ICollection<CourtCasesEnforcement> EnforcementModifiedby { get; set; }
        public ICollection<CaseRegistration> CaseRegistrationCreatedby { get; set; }
        public ICollection<CaseRegistration> CaseRegistrationModifiedby { get; set; }
        public ICollection<SessionsRoll> SessionRollCreatedby { get; set; }
        public ICollection<SessionsRoll> SessionRollModifiedby { get; set; }
        public ICollection<DecisionTranslation> TranslationCreatedby { get; set; }
        public ICollection<DecisionTranslation> TranslationModifiedby { get; set; }
        public ICollection<ClosurePartialDetail> ClosurePartialCreatedby { get; set; }
        public ICollection<ClosurePartialDetail> ClosurePartialModifiedby { get; set; }
        public ICollection<DefendentContact> DefendentContactCreatedby { get; set; }
        public ICollection<DefendentContact> DefendentContactModifiedby { get; set; }

        #endregion
    }

    public class ApplicationRole : IdentityRole<int, ApplicationUserRole>
    {
        public ApplicationRole()
        {
            //this.Id = Guid.NewGuid().ToString();
        }
        public ApplicationRole(string name)
            : this()
        {
            this.Name = name;
        }

        public ApplicationRole(string name, string description)
            : this(name)
        {
            this.RoleDescription = description;
        }

        public DateTime LastModified { get; set; }
        public bool IsSysAdmin { get; set; }
        public string RoleDescription { get; set; }

        public virtual ICollection<PERMISSION> PERMISSIONS { get; set; }

        public bool IsPermissionInRole(string _permission)
        {
            bool _retVal = false;
            try
            {
                foreach (PERMISSION _perm in this.PERMISSIONS)
                {
                    if (_perm.PermissionDescription == _permission)
                    {
                        _retVal = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }
    }

    public class RBACDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public DbSet<PERMISSION> PERMISSIONS { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<MasterSetups> MasterSetup { get; set; }
        public DbSet<Link_Bank_Account> LinkBankAccounts { get; set; }
        public DbSet<PayVoucher> PayVouchers { get; set; }
        public DbSet<EmpDoc> EmpDocs { get; set; }
        public DbSet<CourtCases> CourtCase { get; set; }
        public DbSet<CourtCasesDetail> CourtCasesDetail { get; set; }
        public DbSet<CourtCasesFollowup> CourtCasesFollowup { get; set; }
        public DbSet<CaseInvoice> CaseInvoice { get; set; }
        public DbSet<CaseInvoiceFee> CaseInvoiceFee { get; set; }
        public DbSet<CaseInvoiceFeeCalculation> CaseInvoiceFeeCalculation { get; set; }
        public DbSet<CourtCaseStatusDetail> CourtCaseStatusDetail { get; set; }
        public DbSet<CourtCasesEnforcement> CourtCasesEnforcement { get; set; }
        public DbSet<ApplicationError> ApplicationError { get; set; }
        public DbSet<CaseRegistration> CaseRegistration { get; set; }
        public DbSet<SessionsRoll> SessionsRoll { get; set; }
        public DbSet<DefendentTransfer> DefendentTransfer { get; set; }
        public DbSet<ClientAccess> ClientAccess { get; set; }
        public DbSet<DecisionTranslation> DecisionTranslation { get; set; }
        public DbSet<ClosurePartialDetail> ClosurePartialDetail { get; set; }
        public DbSet<DefendentContact> DefendentContact { get; set; }
        public RBACDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer<RBACDbContext>(new RBACDatabaseInitializer());
        }

        public static RBACDbContext Create()
        {
            return new RBACDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("USERS").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<ApplicationRole>().ToTable("ROLES").Property(p => p.Id).HasColumnName("RoleId");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("LNK_USER_ROLE");

            modelBuilder.Entity<ApplicationRole>().
            HasMany(c => c.PERMISSIONS).
            WithMany(p => p.ROLES).
            Map(
                m =>
                {
                    m.MapLeftKey("RoleId");
                    m.MapRightKey("PermissionId");
                    m.ToTable("LNK_ROLE_PERMISSION");
                });

            // Moved all MasterSetups related configuration to MasterSetupConfiguration class
            modelBuilder.Configurations.Add(new MasterSetupConfiguration());

            // Moved all Employees related configuration to EmployeeConfiguration class
            modelBuilder.Configurations.Add(new EmployeesConfiguration());

            // Moved all PayVoucher related configuration to PayVoucherConfiguration class
            modelBuilder.Configurations.Add(new PayVoucherConfiguration());

            // Moved all EmpDoc related configuration to EmpDocConfiguration class
            modelBuilder.Configurations.Add(new EmpDocConfiguration());

            // Moved all CaseCourt related configuration to CourtCasesConfiguration class
            modelBuilder.Configurations.Add(new CourtCasesConfiguration());

            // Moved all CaseCourtDetail related configuration to CourtCasesDetailConfiguration class
            modelBuilder.Configurations.Add(new CourtCasesDetailConfiguration());

            // Moved all CourtCasesFollowup related configuration to CourtCasesFollowupConfiguration class
            modelBuilder.Configurations.Add(new CourtCasesFollowupConfiguration());

            // Moved all CaseInvoice related configuration to CaseInvoiceConfiguration class
            modelBuilder.Configurations.Add(new CaseInvoiceConfiguration());

            // Moved all CaseInvoiceFee related configuration to CaseInvoiceFeeConfiguration class
            modelBuilder.Configurations.Add(new CaseInvoiceFeeConfiguration());

            // Moved all CourtCaseStatusDetail related configuration to CourtCaseStatusDetailConfiguration class
            modelBuilder.Configurations.Add(new CourtCaseStatusDetailConfiguration());

            // Moved all CourtCasesEnforcement related configuration to CourtCasesEnforcementConfiguration class
            modelBuilder.Configurations.Add(new CourtCasesEnforcementConfiguration());

            // Moved all CaseInvoiceFeeCalculation related configuration to CaseInvoiceFeeCalculationConfiguration class
            modelBuilder.Configurations.Add(new CaseInvoiceFeeCalculationConfiguration());

            // Moved all CaseRegistration related configuration to CaseRegistrationConfiguration class
            modelBuilder.Configurations.Add(new CaseRegistrationConfiguration());

            // Moved all SessionsRoll related configuration to SessionsRollConfiguration class
            modelBuilder.Configurations.Add(new SessionsRollConfiguration());

            modelBuilder.Configurations.Add(new DecisionTranslationConfiguration());
            modelBuilder.Configurations.Add(new ClosurePartialDetailConfiguration());
            modelBuilder.Configurations.Add(new DefendentContactConfiguration());
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is Base && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Base)entity.Entity).CreatedOn = DateTime.Now;
                    ((Base)entity.Entity).CreatedBy = HttpContext.Current.User.Identity.GetUserId();
                }
                else if (entity.State == EntityState.Modified)
                {
                    ((Base)entity.Entity).UpdatedOn = DateTime.Now;
                    ((Base)entity.Entity).UpdatedBy = HttpContext.Current.User.Identity.GetUserId();
                }
            }
        }
    }
}