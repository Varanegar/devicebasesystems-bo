namespace DeviceBaseSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BrandCode = c.String(maxLength: 20),
                        BrandName = c.String(maxLength: 200),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedById = c.Guid(),
                        LastModifiedById = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedById)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.DeviceModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceCode = c.String(maxLength: 20),
                        DeviceName = c.String(maxLength: 200),
                        BrandId = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedById = c.Guid(),
                        LastModifiedById = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedById)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.BrandId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompanyCode = c.Int(nullable: false),
                        CompanyName = c.String(maxLength: 100),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedById = c.Guid(),
                        LastModifiedById = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedById)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.CompanyDevices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompanyId = c.Guid(nullable: false),
                        DeviceModelId = c.Guid(nullable: false),
                        IMEI = c.String(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedById = c.Guid(),
                        LastModifiedById = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedById)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.DeviceModels", t => t.DeviceModelId, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.CompanyId)
                .Index(t => t.DeviceModelId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.CompanyDeviceStatus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ValiityPDate = c.String(),
                        TrackingProductId = c.Guid(nullable: false),
                        LicenceCode = c.String(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedById = c.Guid(),
                        LastModifiedById = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedById)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .ForeignKey("dbo.TrackingProducts", t => t.TrackingProductId, cascadeDelete: true)
                .Index(t => t.TrackingProductId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.TrackingProducts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        ProductDescription = c.String(maxLength: 200),
                        Minute = c.Int(nullable: false),
                        Hour = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Active = c.Boolean(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedById = c.Guid(),
                        LastModifiedById = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedById)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductCode = c.String(maxLength: 20),
                        ProductName = c.String(maxLength: 200),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedById = c.Guid(),
                        LastModifiedById = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedById)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.PurchaseOrderLineItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PurchaseOrderId = c.Guid(nullable: false),
                        CompanyDeviceId = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedById = c.Guid(),
                        LastModifiedById = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedById)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.CompanyDevices", t => t.CompanyDeviceId, cascadeDelete: true)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrderId, cascadeDelete: true)
                .Index(t => t.PurchaseOrderId)
                .Index(t => t.CompanyDeviceId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompanyId = c.Guid(nullable: false),
                        TrackingProductId = c.Guid(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        OrderPDate = c.String(),
                        OrderTime = c.Time(nullable: false, precision: 7),
                        PurchaseOrderStatus = c.Int(nullable: false),
                        PurchaseOrderPaymentId = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedById = c.Guid(),
                        LastModifiedById = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedById)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .ForeignKey("dbo.PurchaseOrderPayments", t => t.PurchaseOrderPaymentId, cascadeDelete: true)
                .ForeignKey("dbo.TrackingProducts", t => t.TrackingProductId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.TrackingProductId)
                .Index(t => t.PurchaseOrderPaymentId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.PurchaseOrderPayments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PayDate = c.DateTime(nullable: false),
                        PayPDate = c.String(maxLength: 10),
                        PayTime = c.Time(nullable: false, precision: 7),
                        PayAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedById = c.Guid(),
                        LastModifiedById = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedById)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.ValidityDurations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DurationCode = c.String(maxLength: 20),
                        DurationTitle = c.String(maxLength: 200),
                        DurationAmount = c.Int(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedById = c.Guid(),
                        LastModifiedById = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedById)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ValidityDurations", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.ValidityDurations", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.ValidityDurations", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.ValidityDurations", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.ValidityDurations", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderLineItems", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrders", "TrackingProductId", "dbo.TrackingProducts");
            DropForeignKey("dbo.PurchaseOrders", "PurchaseOrderPaymentId", "dbo.PurchaseOrderPayments");
            DropForeignKey("dbo.PurchaseOrderPayments", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderPayments", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.PurchaseOrderPayments", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.PurchaseOrderPayments", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PurchaseOrderPayments", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrders", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrders", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.PurchaseOrders", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.PurchaseOrders", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.PurchaseOrders", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PurchaseOrders", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderLineItems", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderLineItems", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.PurchaseOrderLineItems", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.PurchaseOrderLineItems", "CompanyDeviceId", "dbo.CompanyDevices");
            DropForeignKey("dbo.PurchaseOrderLineItems", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PurchaseOrderLineItems", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.CompanyDeviceStatus", "TrackingProductId", "dbo.TrackingProducts");
            DropForeignKey("dbo.TrackingProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.Products", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.Products", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.Products", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Products", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.TrackingProducts", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.TrackingProducts", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.TrackingProducts", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.TrackingProducts", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.TrackingProducts", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.CompanyDeviceStatus", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.CompanyDeviceStatus", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CompanyDeviceStatus", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CompanyDeviceStatus", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CompanyDeviceStatus", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.Companies", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.Companies", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.Companies", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CompanyDevices", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.CompanyDevices", "DeviceModelId", "dbo.DeviceModels");
            DropForeignKey("dbo.CompanyDevices", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CompanyDevices", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CompanyDevices", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.CompanyDevices", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CompanyDevices", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.Companies", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Companies", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.Brands", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.DeviceModels", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.DeviceModels", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.DeviceModels", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.DeviceModels", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.DeviceModels", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.DeviceModels", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.Brands", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.Brands", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.Brands", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Brands", "AddedById", "dbo.Principals");
            DropIndex("dbo.ValidityDurations", new[] { "LastModifiedById" });
            DropIndex("dbo.ValidityDurations", new[] { "AddedById" });
            DropIndex("dbo.ValidityDurations", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.ValidityDurations", new[] { "DataOwnerId" });
            DropIndex("dbo.ValidityDurations", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "LastModifiedById" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "AddedById" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "DataOwnerId" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PurchaseOrders", new[] { "LastModifiedById" });
            DropIndex("dbo.PurchaseOrders", new[] { "AddedById" });
            DropIndex("dbo.PurchaseOrders", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.PurchaseOrders", new[] { "DataOwnerId" });
            DropIndex("dbo.PurchaseOrders", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PurchaseOrders", new[] { "PurchaseOrderPaymentId" });
            DropIndex("dbo.PurchaseOrders", new[] { "TrackingProductId" });
            DropIndex("dbo.PurchaseOrders", new[] { "CompanyId" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "LastModifiedById" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "AddedById" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "DataOwnerId" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "CompanyDeviceId" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "PurchaseOrderId" });
            DropIndex("dbo.Products", new[] { "LastModifiedById" });
            DropIndex("dbo.Products", new[] { "AddedById" });
            DropIndex("dbo.Products", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.Products", new[] { "DataOwnerId" });
            DropIndex("dbo.Products", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.TrackingProducts", new[] { "LastModifiedById" });
            DropIndex("dbo.TrackingProducts", new[] { "AddedById" });
            DropIndex("dbo.TrackingProducts", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.TrackingProducts", new[] { "DataOwnerId" });
            DropIndex("dbo.TrackingProducts", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.TrackingProducts", new[] { "ProductId" });
            DropIndex("dbo.CompanyDeviceStatus", new[] { "LastModifiedById" });
            DropIndex("dbo.CompanyDeviceStatus", new[] { "AddedById" });
            DropIndex("dbo.CompanyDeviceStatus", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CompanyDeviceStatus", new[] { "DataOwnerId" });
            DropIndex("dbo.CompanyDeviceStatus", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CompanyDeviceStatus", new[] { "TrackingProductId" });
            DropIndex("dbo.CompanyDevices", new[] { "LastModifiedById" });
            DropIndex("dbo.CompanyDevices", new[] { "AddedById" });
            DropIndex("dbo.CompanyDevices", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CompanyDevices", new[] { "DataOwnerId" });
            DropIndex("dbo.CompanyDevices", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CompanyDevices", new[] { "DeviceModelId" });
            DropIndex("dbo.CompanyDevices", new[] { "CompanyId" });
            DropIndex("dbo.Companies", new[] { "LastModifiedById" });
            DropIndex("dbo.Companies", new[] { "AddedById" });
            DropIndex("dbo.Companies", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.Companies", new[] { "DataOwnerId" });
            DropIndex("dbo.Companies", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.DeviceModels", new[] { "LastModifiedById" });
            DropIndex("dbo.DeviceModels", new[] { "AddedById" });
            DropIndex("dbo.DeviceModels", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.DeviceModels", new[] { "DataOwnerId" });
            DropIndex("dbo.DeviceModels", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.DeviceModels", new[] { "BrandId" });
            DropIndex("dbo.Brands", new[] { "LastModifiedById" });
            DropIndex("dbo.Brands", new[] { "AddedById" });
            DropIndex("dbo.Brands", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.Brands", new[] { "DataOwnerId" });
            DropIndex("dbo.Brands", new[] { "ApplicationOwnerId" });
            DropTable("dbo.ValidityDurations");
            DropTable("dbo.PurchaseOrderPayments");
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.PurchaseOrderLineItems");
            DropTable("dbo.Products");
            DropTable("dbo.TrackingProducts");
            DropTable("dbo.CompanyDeviceStatus");
            DropTable("dbo.CompanyDevices");
            DropTable("dbo.Companies");
            DropTable("dbo.DeviceModels");
            DropTable("dbo.Brands");
        }
    }
}
