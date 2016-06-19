namespace DeviceBaseSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnatoliAccounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        AnatoliPlaceId = c.Guid(),
                        AnatoliContactId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnatoliContacts", t => t.AnatoliContactId)
                .ForeignKey("dbo.AnatoliPlaces", t => t.AnatoliPlaceId)
                .Index(t => t.AnatoliPlaceId)
                .Index(t => t.AnatoliContactId);
            
            CreateTable(
                "dbo.AnatoliContacts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ContactName = c.String(maxLength: 200),
                        FirstName = c.String(maxLength: 200),
                        LastName = c.String(maxLength: 200),
                        BirthDay = c.DateTime(),
                        Phone = c.String(maxLength: 20),
                        Mobile = c.String(maxLength: 20),
                        Email = c.String(maxLength: 100),
                        Website = c.String(maxLength: 100),
                        NationalCode = c.String(maxLength: 20),
                        AnatoliContactTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnatoliContactTypes", t => t.AnatoliContactTypeId, cascadeDelete: true)
                .Index(t => t.AnatoliContactTypeId);
            
            CreateTable(
                "dbo.AnatoliContactTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 20),
                        Description = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        FullName = c.String(maxLength: 200),
                        UserName = c.String(),
                        UserNameStr = c.String(maxLength: 50),
                        Password = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        LastEntry = c.DateTime(nullable: false),
                        LastEntryIp = c.String(),
                        ResetSMSCode = c.String(maxLength: 200),
                        ResetSMSPass = c.String(maxLength: 200),
                        ResetSMSRequestTime = c.DateTime(),
                        AnatoliContactId = c.Guid(),
                        ApplicationOwnerId = c.Guid(),
                        DataOwnerId = c.Guid(nullable: false),
                        PrincipalId = c.Guid(nullable: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        Application_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnatoliContacts", t => t.AnatoliContactId)
                .ForeignKey("dbo.Applications", t => t.Application_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.PrincipalId, cascadeDelete: true)
                .Index(t => t.AnatoliContactId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.PrincipalId)
                .Index(t => t.Application_Id);
            
            CreateTable(
                "dbo.ApplicationOwners",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        WebHookURI = c.String(maxLength: 200),
                        WebHookUsername = c.String(maxLength: 200),
                        WebHookPassword = c.String(maxLength: 200),
                        AnatoliContactId = c.Guid(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnatoliContacts", t => t.AnatoliContactId, cascadeDelete: true)
                .ForeignKey("dbo.Applications", t => t.ApplicationId, cascadeDelete: true)
                .Index(t => t.AnatoliContactId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationModules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        ApplicationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.ApplicationId, cascadeDelete: true)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.ApplicationModuleResources",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        ApplicationModuleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationModules", t => t.ApplicationModuleId, cascadeDelete: true)
                .Index(t => t.ApplicationModuleId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        ApplicationModuleResourceId = c.Guid(nullable: false),
                        PermissionActionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationModuleResources", t => t.ApplicationModuleResourceId, cascadeDelete: true)
                .ForeignKey("dbo.PermissionActions", t => t.PermissionActionId, cascadeDelete: true)
                .Index(t => t.ApplicationModuleResourceId)
                .Index(t => t.PermissionActionId);
            
            CreateTable(
                "dbo.PermissionActions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PermissionCatalogPermissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Permission_Id = c.Guid(),
                        PermissionCatalog_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permissions", t => t.Permission_Id)
                .ForeignKey("dbo.PermissionCatalogs", t => t.PermissionCatalog_Id)
                .Index(t => t.Permission_Id)
                .Index(t => t.PermissionCatalog_Id);
            
            CreateTable(
                "dbo.PermissionCatalogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        PermissionCatalougeParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PermissionCatalogs", t => t.PermissionCatalougeParentId)
                .Index(t => t.PermissionCatalougeParentId);
            
            CreateTable(
                "dbo.PrincipalPermissionCatalogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Grant = c.Int(nullable: false),
                        PermissionCatalog_Id = c.Guid(nullable: false),
                        PrincipalId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PermissionCatalogs", t => t.PermissionCatalog_Id, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.PrincipalId, cascadeDelete: true)
                .Index(t => t.PermissionCatalog_Id)
                .Index(t => t.PrincipalId);
            
            CreateTable(
                "dbo.Principals",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        Group_Id = c.Guid(),
                        Group_Id1 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id1)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.Group_Id)
                .Index(t => t.Group_Id1);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 200),
                        ParentId = c.Guid(),
                        NodeId = c.Int(nullable: false),
                        NLeft = c.Int(nullable: false),
                        NRight = c.Int(nullable: false),
                        NLevel = c.Int(nullable: false),
                        Priority = c.Int(),
                        Manager_Id = c.Guid(),
                        Principal_Id = c.Guid(),
                        Principal_Id1 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.ParentId)
                .ForeignKey("dbo.Principals", t => t.Manager_Id)
                .ForeignKey("dbo.Principals", t => t.Principal_Id)
                .ForeignKey("dbo.Principals", t => t.Principal_Id1)
                .Index(t => t.ParentId)
                .Index(t => t.Manager_Id)
                .Index(t => t.Principal_Id)
                .Index(t => t.Principal_Id1);
            
            CreateTable(
                "dbo.PrincipalPermissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Grant = c.Int(nullable: false),
                        Permission_Id = c.Guid(nullable: false),
                        PrincipalId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permissions", t => t.Permission_Id, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.PrincipalId, cascadeDelete: true)
                .Index(t => t.Permission_Id)
                .Index(t => t.PrincipalId);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        ApplicationId = c.Guid(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.ApplicationId, cascadeDelete: true)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.UserId)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.DataOwners",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        WebHookURI = c.String(maxLength: 200),
                        WebHookUsername = c.String(maxLength: 200),
                        WebHookPassword = c.String(maxLength: 200),
                        AnatoliContactId = c.Guid(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnatoliContacts", t => t.AnatoliContactId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .Index(t => t.AnatoliContactId)
                .Index(t => t.ApplicationOwnerId);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AnatoliPlaces",
                c => new
                    {
                        RegionInfoId = c.Guid(),
                        RegionLevel1Id = c.Guid(),
                        RegionLevel2Id = c.Guid(),
                        RegionLevel3Id = c.Guid(),
                        RegionLevel4Id = c.Guid(),
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        Phone = c.String(),
                        Mobile = c.String(maxLength: 20),
                        Email = c.String(maxLength: 500),
                        MainStreet = c.String(maxLength: 500),
                        OtherStreet = c.String(maxLength: 500),
                        PostalCode = c.String(maxLength: 20),
                        NationalCode = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnatoliRegions", t => t.RegionInfoId)
                .ForeignKey("dbo.AnatoliRegions", t => t.RegionLevel1Id)
                .ForeignKey("dbo.AnatoliRegions", t => t.RegionLevel2Id)
                .ForeignKey("dbo.AnatoliRegions", t => t.RegionLevel3Id)
                .ForeignKey("dbo.AnatoliRegions", t => t.RegionLevel4Id)
                .Index(t => t.RegionInfoId)
                .Index(t => t.RegionLevel1Id)
                .Index(t => t.RegionLevel2Id)
                .Index(t => t.RegionLevel3Id)
                .Index(t => t.RegionLevel4Id);
            
            CreateTable(
                "dbo.AnatoliRegions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        GroupName = c.String(),
                        NLeft = c.Int(nullable: false),
                        NRight = c.Int(nullable: false),
                        NLevel = c.Int(nullable: false),
                        Priority = c.Int(),
                        AnatoliRegion2Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnatoliRegions", t => t.AnatoliRegion2Id)
                .Index(t => t.AnatoliRegion2Id);
            
            CreateTable(
                "dbo.DataOwnerCenters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        WebHookURI = c.String(maxLength: 200),
                        WebHookUsername = c.String(maxLength: 200),
                        WebHookPassword = c.String(maxLength: 200),
                        AnatoliContactId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnatoliContacts", t => t.AnatoliContactId, cascadeDelete: true)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .Index(t => t.AnatoliContactId)
                .Index(t => t.DataOwnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.DataOwnerCenters", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.DataOwnerCenters", "AnatoliContactId", "dbo.AnatoliContacts");
            DropForeignKey("dbo.AnatoliAccounts", "AnatoliPlaceId", "dbo.AnatoliPlaces");
            DropForeignKey("dbo.AnatoliPlaces", "RegionLevel4Id", "dbo.AnatoliRegions");
            DropForeignKey("dbo.AnatoliPlaces", "RegionLevel3Id", "dbo.AnatoliRegions");
            DropForeignKey("dbo.AnatoliPlaces", "RegionLevel2Id", "dbo.AnatoliRegions");
            DropForeignKey("dbo.AnatoliPlaces", "RegionLevel1Id", "dbo.AnatoliRegions");
            DropForeignKey("dbo.AnatoliPlaces", "RegionInfoId", "dbo.AnatoliRegions");
            DropForeignKey("dbo.AnatoliRegions", "AnatoliRegion2Id", "dbo.AnatoliRegions");
            DropForeignKey("dbo.AnatoliAccounts", "AnatoliContactId", "dbo.AnatoliContacts");
            DropForeignKey("dbo.IdentityUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "PrincipalId", "dbo.Principals");
            DropForeignKey("dbo.IdentityUserLogins", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.DataOwners", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.DataOwners", "AnatoliContactId", "dbo.AnatoliContacts");
            DropForeignKey("dbo.IdentityUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.ApplicationOwners", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Users", "Application_Id", "dbo.Applications");
            DropForeignKey("dbo.IdentityRoles", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.PrincipalPermissionCatalogs", "PrincipalId", "dbo.Principals");
            DropForeignKey("dbo.PrincipalPermissions", "PrincipalId", "dbo.Principals");
            DropForeignKey("dbo.PrincipalPermissions", "Permission_Id", "dbo.Permissions");
            DropForeignKey("dbo.Groups", "Principal_Id1", "dbo.Principals");
            DropForeignKey("dbo.Principals", "Group_Id1", "dbo.Groups");
            DropForeignKey("dbo.Principals", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Groups", "Principal_Id", "dbo.Principals");
            DropForeignKey("dbo.Groups", "Manager_Id", "dbo.Principals");
            DropForeignKey("dbo.Groups", "ParentId", "dbo.Groups");
            DropForeignKey("dbo.Principals", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PrincipalPermissionCatalogs", "PermissionCatalog_Id", "dbo.PermissionCatalogs");
            DropForeignKey("dbo.PermissionCatalogs", "PermissionCatalougeParentId", "dbo.PermissionCatalogs");
            DropForeignKey("dbo.PermissionCatalogPermissions", "PermissionCatalog_Id", "dbo.PermissionCatalogs");
            DropForeignKey("dbo.PermissionCatalogPermissions", "Permission_Id", "dbo.Permissions");
            DropForeignKey("dbo.Permissions", "PermissionActionId", "dbo.PermissionActions");
            DropForeignKey("dbo.Permissions", "ApplicationModuleResourceId", "dbo.ApplicationModuleResources");
            DropForeignKey("dbo.ApplicationModuleResources", "ApplicationModuleId", "dbo.ApplicationModules");
            DropForeignKey("dbo.ApplicationModules", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.ApplicationOwners", "AnatoliContactId", "dbo.AnatoliContacts");
            DropForeignKey("dbo.Users", "AnatoliContactId", "dbo.AnatoliContacts");
            DropForeignKey("dbo.AnatoliContacts", "AnatoliContactTypeId", "dbo.AnatoliContactTypes");
            DropIndex("dbo.DataOwnerCenters", new[] { "DataOwnerId" });
            DropIndex("dbo.DataOwnerCenters", new[] { "AnatoliContactId" });
            DropIndex("dbo.AnatoliRegions", new[] { "AnatoliRegion2Id" });
            DropIndex("dbo.AnatoliPlaces", new[] { "RegionLevel4Id" });
            DropIndex("dbo.AnatoliPlaces", new[] { "RegionLevel3Id" });
            DropIndex("dbo.AnatoliPlaces", new[] { "RegionLevel2Id" });
            DropIndex("dbo.AnatoliPlaces", new[] { "RegionLevel1Id" });
            DropIndex("dbo.AnatoliPlaces", new[] { "RegionInfoId" });
            DropIndex("dbo.IdentityUserLogins", new[] { "User_Id" });
            DropIndex("dbo.DataOwners", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.DataOwners", new[] { "AnatoliContactId" });
            DropIndex("dbo.IdentityUserClaims", new[] { "UserId" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "UserId" });
            DropIndex("dbo.IdentityRoles", new[] { "ApplicationId" });
            DropIndex("dbo.PrincipalPermissions", new[] { "PrincipalId" });
            DropIndex("dbo.PrincipalPermissions", new[] { "Permission_Id" });
            DropIndex("dbo.Groups", new[] { "Principal_Id1" });
            DropIndex("dbo.Groups", new[] { "Principal_Id" });
            DropIndex("dbo.Groups", new[] { "Manager_Id" });
            DropIndex("dbo.Groups", new[] { "ParentId" });
            DropIndex("dbo.Principals", new[] { "Group_Id1" });
            DropIndex("dbo.Principals", new[] { "Group_Id" });
            DropIndex("dbo.Principals", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PrincipalPermissionCatalogs", new[] { "PrincipalId" });
            DropIndex("dbo.PrincipalPermissionCatalogs", new[] { "PermissionCatalog_Id" });
            DropIndex("dbo.PermissionCatalogs", new[] { "PermissionCatalougeParentId" });
            DropIndex("dbo.PermissionCatalogPermissions", new[] { "PermissionCatalog_Id" });
            DropIndex("dbo.PermissionCatalogPermissions", new[] { "Permission_Id" });
            DropIndex("dbo.Permissions", new[] { "PermissionActionId" });
            DropIndex("dbo.Permissions", new[] { "ApplicationModuleResourceId" });
            DropIndex("dbo.ApplicationModuleResources", new[] { "ApplicationModuleId" });
            DropIndex("dbo.ApplicationModules", new[] { "ApplicationId" });
            DropIndex("dbo.ApplicationOwners", new[] { "ApplicationId" });
            DropIndex("dbo.ApplicationOwners", new[] { "AnatoliContactId" });
            DropIndex("dbo.Users", new[] { "Application_Id" });
            DropIndex("dbo.Users", new[] { "PrincipalId" });
            DropIndex("dbo.Users", new[] { "DataOwnerId" });
            DropIndex("dbo.Users", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.Users", new[] { "AnatoliContactId" });
            DropIndex("dbo.AnatoliContacts", new[] { "AnatoliContactTypeId" });
            DropIndex("dbo.AnatoliAccounts", new[] { "AnatoliContactId" });
            DropIndex("dbo.AnatoliAccounts", new[] { "AnatoliPlaceId" });
            DropTable("dbo.DataOwnerCenters");
            DropTable("dbo.AnatoliRegions");
            DropTable("dbo.AnatoliPlaces");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.DataOwners");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.PrincipalPermissions");
            DropTable("dbo.Groups");
            DropTable("dbo.Principals");
            DropTable("dbo.PrincipalPermissionCatalogs");
            DropTable("dbo.PermissionCatalogs");
            DropTable("dbo.PermissionCatalogPermissions");
            DropTable("dbo.PermissionActions");
            DropTable("dbo.Permissions");
            DropTable("dbo.ApplicationModuleResources");
            DropTable("dbo.ApplicationModules");
            DropTable("dbo.Applications");
            DropTable("dbo.ApplicationOwners");
            DropTable("dbo.Users");
            DropTable("dbo.AnatoliContactTypes");
            DropTable("dbo.AnatoliContacts");
            DropTable("dbo.AnatoliAccounts");
        }
    }
}
