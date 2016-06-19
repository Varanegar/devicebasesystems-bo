namespace DeviceBaseSystem.DataAccess.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<DeviceBaseSystem.DataAccess.AnatoliDbContext>
    {
        private bool _pendingMigrations;
        public Configuration()
        {
            var migrator = new DbMigrator(this);
            _pendingMigrations = migrator.GetPendingMigrations().Any();
        }

        protected override void Seed(DeviceBaseSystem.DataAccess.AnatoliDbContext context)
        {
            if (_pendingMigrations)
            {
            }
        }
    }
}
