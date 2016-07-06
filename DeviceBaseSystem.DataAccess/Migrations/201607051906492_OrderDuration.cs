namespace DeviceBaseSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderDuration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DayPerMonths",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
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
                "dbo.HourPerDays",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
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
                "dbo.Minutes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
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
            
            AddColumn("dbo.CompanyDeviceStatus", "CompanyDeviceId", c => c.Guid(nullable: false));
            AddColumn("dbo.TrackingProducts", "MinuteId", c => c.Guid(nullable: false));
            AddColumn("dbo.TrackingProducts", "HourPerDayId", c => c.Guid(nullable: false));
            AddColumn("dbo.TrackingProducts", "DayPerMonthId", c => c.Guid(nullable: false));
            AddColumn("dbo.PurchaseOrders", "ValidityDurationId", c => c.Guid(nullable: false));
            CreateIndex("dbo.CompanyDeviceStatus", "CompanyDeviceId");
            CreateIndex("dbo.TrackingProducts", "MinuteId");
            CreateIndex("dbo.TrackingProducts", "HourPerDayId");
            CreateIndex("dbo.TrackingProducts", "DayPerMonthId");
            CreateIndex("dbo.PurchaseOrders", "ValidityDurationId");
            AddForeignKey("dbo.CompanyDeviceStatus", "CompanyDeviceId", "dbo.CompanyDevices", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TrackingProducts", "DayPerMonthId", "dbo.DayPerMonths", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TrackingProducts", "HourPerDayId", "dbo.HourPerDays", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TrackingProducts", "MinuteId", "dbo.Minutes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PurchaseOrders", "ValidityDurationId", "dbo.ValidityDurations", "Id", cascadeDelete: true);
            DropColumn("dbo.TrackingProducts", "Minute");
            DropColumn("dbo.TrackingProducts", "Hour");
            DropColumn("dbo.TrackingProducts", "Day");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TrackingProducts", "Day", c => c.Int(nullable: false));
            AddColumn("dbo.TrackingProducts", "Hour", c => c.Int(nullable: false));
            AddColumn("dbo.TrackingProducts", "Minute", c => c.Int(nullable: false));
            DropForeignKey("dbo.PurchaseOrders", "ValidityDurationId", "dbo.ValidityDurations");
            DropForeignKey("dbo.TrackingProducts", "MinuteId", "dbo.Minutes");
            DropForeignKey("dbo.Minutes", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.Minutes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.Minutes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.Minutes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Minutes", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.TrackingProducts", "HourPerDayId", "dbo.HourPerDays");
            DropForeignKey("dbo.HourPerDays", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.HourPerDays", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.HourPerDays", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.HourPerDays", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.HourPerDays", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.TrackingProducts", "DayPerMonthId", "dbo.DayPerMonths");
            DropForeignKey("dbo.DayPerMonths", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.DayPerMonths", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.DayPerMonths", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.DayPerMonths", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.DayPerMonths", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.CompanyDeviceStatus", "CompanyDeviceId", "dbo.CompanyDevices");
            DropIndex("dbo.PurchaseOrders", new[] { "ValidityDurationId" });
            DropIndex("dbo.Minutes", new[] { "LastModifiedById" });
            DropIndex("dbo.Minutes", new[] { "AddedById" });
            DropIndex("dbo.Minutes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.Minutes", new[] { "DataOwnerId" });
            DropIndex("dbo.Minutes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.HourPerDays", new[] { "LastModifiedById" });
            DropIndex("dbo.HourPerDays", new[] { "AddedById" });
            DropIndex("dbo.HourPerDays", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.HourPerDays", new[] { "DataOwnerId" });
            DropIndex("dbo.HourPerDays", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.DayPerMonths", new[] { "LastModifiedById" });
            DropIndex("dbo.DayPerMonths", new[] { "AddedById" });
            DropIndex("dbo.DayPerMonths", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.DayPerMonths", new[] { "DataOwnerId" });
            DropIndex("dbo.DayPerMonths", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.TrackingProducts", new[] { "DayPerMonthId" });
            DropIndex("dbo.TrackingProducts", new[] { "HourPerDayId" });
            DropIndex("dbo.TrackingProducts", new[] { "MinuteId" });
            DropIndex("dbo.CompanyDeviceStatus", new[] { "CompanyDeviceId" });
            DropColumn("dbo.PurchaseOrders", "ValidityDurationId");
            DropColumn("dbo.TrackingProducts", "DayPerMonthId");
            DropColumn("dbo.TrackingProducts", "HourPerDayId");
            DropColumn("dbo.TrackingProducts", "MinuteId");
            DropColumn("dbo.CompanyDeviceStatus", "CompanyDeviceId");
            DropTable("dbo.Minutes");
            DropTable("dbo.HourPerDays");
            DropTable("dbo.DayPerMonths");
        }
    }
}
