namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PERMISSIONS",
                c => new
                    {
                        PermissionId = c.Int(nullable: false, identity: true),
                        PermissionDescription = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.PermissionId);
            
            CreateTable(
                "dbo.ROLES",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        LastModified = c.DateTime(nullable: false),
                        IsSysAdmin = c.Boolean(nullable: false),
                        RoleDescription = c.String(),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.RoleId)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.LNK_USER_ROLE",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.ROLES", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.USERS", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.USERS",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        LastModified = c.DateTime(nullable: false),
                        Inactive = c.Boolean(nullable: false),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.USERS", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.USERS", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LNK_ROLE_PERMISSION",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.PermissionId })
                .ForeignKey("dbo.ROLES", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.PERMISSIONS", t => t.PermissionId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.PermissionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LNK_USER_ROLE", "UserId", "dbo.USERS");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.USERS");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.USERS");
            DropForeignKey("dbo.LNK_USER_ROLE", "RoleId", "dbo.ROLES");
            DropForeignKey("dbo.LNK_ROLE_PERMISSION", "PermissionId", "dbo.PERMISSIONS");
            DropForeignKey("dbo.LNK_ROLE_PERMISSION", "RoleId", "dbo.ROLES");
            DropIndex("dbo.LNK_ROLE_PERMISSION", new[] { "PermissionId" });
            DropIndex("dbo.LNK_ROLE_PERMISSION", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.USERS", "UserNameIndex");
            DropIndex("dbo.LNK_USER_ROLE", new[] { "RoleId" });
            DropIndex("dbo.LNK_USER_ROLE", new[] { "UserId" });
            DropIndex("dbo.ROLES", "RoleNameIndex");
            DropTable("dbo.LNK_ROLE_PERMISSION");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.USERS");
            DropTable("dbo.LNK_USER_ROLE");
            DropTable("dbo.ROLES");
            DropTable("dbo.PERMISSIONS");
        }
    }
}
