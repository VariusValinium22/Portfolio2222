namespace Budgeter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        HouseholdId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Households", t => t.HouseholdId, cascadeDelete: true)
                .Index(t => t.HouseholdId);
            
            CreateTable(
                "dbo.Households",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedById = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Total = c.Double(nullable: false),
                        TotalBudget = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HouseholdId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Balance = c.Double(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Households", t => t.HouseholdId, cascadeDelete: true)
                .Index(t => t.HouseholdId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BankAccountId = c.Int(nullable: false),
                        Payee = c.String(),
                        Description = c.String(),
                        Created = c.DateTimeOffset(nullable: false, precision: 7),
                        Amount = c.Double(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        TypeId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        EnteredById = c.Int(nullable: false),
                        EnteredBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.EnteredBy_Id)
                .ForeignKey("dbo.Types", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.BankAccountId)
                .Index(t => t.TypeId)
                .Index(t => t.CategoryId)
                .Index(t => t.EnteredBy_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        Lastname = c.String(),
                        DisplayName = c.String(),
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
                        Household_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Households", t => t.Household_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Household_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        BudgetId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Budgets", t => t.BudgetId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.BudgetId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Items", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Items", "BudgetId", "dbo.Budgets");
            DropForeignKey("dbo.AspNetUsers", "Household_Id", "dbo.Households");
            DropForeignKey("dbo.Budgets", "HouseholdId", "dbo.Households");
            DropForeignKey("dbo.Transactions", "TypeId", "dbo.Types");
            DropForeignKey("dbo.Transactions", "EnteredBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Transactions", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.BankAccounts", "HouseholdId", "dbo.Households");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Items", new[] { "BudgetId" });
            DropIndex("dbo.Items", new[] { "CategoryId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Household_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Transactions", new[] { "EnteredBy_Id" });
            DropIndex("dbo.Transactions", new[] { "CategoryId" });
            DropIndex("dbo.Transactions", new[] { "TypeId" });
            DropIndex("dbo.Transactions", new[] { "BankAccountId" });
            DropIndex("dbo.BankAccounts", new[] { "HouseholdId" });
            DropIndex("dbo.Budgets", new[] { "HouseholdId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Items");
            DropTable("dbo.Types");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Categories");
            DropTable("dbo.Transactions");
            DropTable("dbo.BankAccounts");
            DropTable("dbo.Households");
            DropTable("dbo.Budgets");
        }
    }
}
