namespace DeviceBaseSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DevceCompany_AddDesc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyDevices", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.CompanyDevices", "IMEI", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CompanyDevices", "IMEI", c => c.String());
            DropColumn("dbo.CompanyDevices", "Description");
        }
    }
}
