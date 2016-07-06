using Anatoli.DataAccess.Models.Identity;

namespace DeviceBaseSystem.DataAccess.Migrations
{
    using Anatoli.DataAccess.Models;
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
           // if (true)
            {

                #region Permission Info
                context.Applications.AddOrUpdate(item => item.Id,
                    new Application { Id = Guid.Parse("8A074FD5-9311-4F8E-AF47-0572DE1A7B6A"), Name = "Anatoli Market place" },
                    new Application { Id = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), Name = "VN Cloud" }
                    );

                context.ApplicationModules.AddOrUpdate(item => item.Id,
                    new ApplicationModule { Id = Guid.Parse("1539E462-467A-48F3-9697-597EDED27523"), Name = "SCM Module", ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC") },
                    new ApplicationModule { Id = Guid.Parse("56E3B436-06C9-4A43-AF16-DF0B122B1747"), Name = "VN GRS", ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC") }
                    );

                context.ApplicationModuleResources.AddOrUpdate(item => item.Id,
                    new ApplicationModuleResource { Id = Guid.Parse("214336BD-FB51-48E0-8DC5-74A1396DE916"), Name = "Route Planning", ApplicationModuleId = Guid.Parse("56E3B436-06C9-4A43-AF16-DF0B122B1747") },
                    new ApplicationModuleResource { Id = Guid.Parse("33C98AA3-CB90-4724-8EAD-45959C3C6551"), Name = "Personnel Tracking", ApplicationModuleId = Guid.Parse("56E3B436-06C9-4A43-AF16-DF0B122B1747") },
                    new ApplicationModuleResource { Id = Guid.Parse("A515CCD7-CFBE-4407-9959-DF4CEAFBD444"), Name = "GRS Reporting", ApplicationModuleId = Guid.Parse("56E3B436-06C9-4A43-AF16-DF0B122B1747") },
                    new ApplicationModuleResource { Id = Guid.Parse("68F751E3-8744-4775-8942-39501F56452F"), Name = "Stock", ApplicationModuleId = Guid.Parse("1539E462-467A-48F3-9697-597EDED27523") },
                    new ApplicationModuleResource { Id = Guid.Parse("12BA035E-B336-41B5-8DBF-1C7DE2162139"), Name = "StockProduct", ApplicationModuleId = Guid.Parse("1539E462-467A-48F3-9697-597EDED27523") },
                    new ApplicationModuleResource { Id = Guid.Parse("E62CF7C0-869C-46DC-8380-70623A35F642"), Name = "StockProductRequest", ApplicationModuleId = Guid.Parse("1539E462-467A-48F3-9697-597EDED27523") },
                    new ApplicationModuleResource { Id = Guid.Parse("D18144DB-DAAC-4BF8-8FB9-B852DABC1F0C"), Name = "StockProductRequestHistory", ApplicationModuleId = Guid.Parse("1539E462-467A-48F3-9697-597EDED27523") },
                    new ApplicationModuleResource { Id = Guid.Parse("0B6CE4DD-DCFE-436F-B995-18F9842A2DA5"), Name = "StockRequest", ApplicationModuleId = Guid.Parse("1539E462-467A-48F3-9697-597EDED27523") },
                    new ApplicationModuleResource { Id = Guid.Parse("BE2B20D7-D1B7-44EE-80DA-55C07C9782A9"), Name = "ProductRequestRules", ApplicationModuleId = Guid.Parse("1539E462-467A-48F3-9697-597EDED27523") },
                    new ApplicationModuleResource { Id = Guid.Parse("01AC3D14-7ED5-4AE5-AF23-4180F15F9677"), Name = "ProductGroup", ApplicationModuleId = Guid.Parse("1539E462-467A-48F3-9697-597EDED27523") },
                    new ApplicationModuleResource { Id = Guid.Parse("D9AC9D01-A00C-4201-9C2D-4F92D29358E9"), Name = "Product", ApplicationModuleId = Guid.Parse("1539E462-467A-48F3-9697-597EDED27523") },
                    new ApplicationModuleResource { Id = Guid.Parse("7BA8CC86-CB32-403E-A7D1-E53710BBDC02"), Name = "Permission", ApplicationModuleId = Guid.Parse("1539E462-467A-48F3-9697-597EDED27523") },
                    new ApplicationModuleResource { Id = Guid.Parse("DD0A28C1-F0B4-4513-8B03-622AA9F66E09"), Name = "UserStock", ApplicationModuleId = Guid.Parse("1539E462-467A-48F3-9697-597EDED27523") },
                    new ApplicationModuleResource { Id = Guid.Parse("1FD9D17F-5AAC-4B27-A8A9-0DCCF84FE8A2"), Name = "UserManagement", ApplicationModuleId = Guid.Parse("1539E462-467A-48F3-9697-597EDED27523") }
                    );

                context.PermissionActions.AddOrUpdate(item => item.Id,
                    new PermissionAction { Id = Guid.Parse("66A925B5-0F61-41D0-86B6-B7E867D4E208"), Name = "List" },
                    new PermissionAction { Id = Guid.Parse("1145EF8D-1BAA-404A-80DA-B7847F0A4763"), Name = "Save" },
                    new PermissionAction { Id = Guid.Parse("EF7F974F-0E0F-45A1-B454-F94E8AC16FE7"), Name = "View" },
                    new PermissionAction { Id = Guid.Parse("0B9A0BF2-7A19-4AAF-8C91-3AC0322D7B74"), Name = "Delete" },
                    new PermissionAction { Id = Guid.Parse("8B31C770-CD0D-440F-83E4-B7A9469B5BCE"), Name = "Page" },
                    new PermissionAction { Id = Guid.Parse("F25A6068-4BD3-44C8-9035-EEAF30C64084"), Name = "Custom" }
                    );


                context.Permissions.AddOrUpdate(item => item.Id,
                    new Permission { Id = Guid.Parse("2AAA4A5B-5550-43EB-BDF4-41908F002EA3"), ApplicationModuleResourceId = Guid.Parse("214336BD-FB51-48E0-8DC5-74A1396DE916"), PermissionActionId = Guid.Parse("8B31C770-CD0D-440F-83E4-B7A9469B5BCE"), Name = "Route Planning" },
                    new Permission { Id = Guid.Parse("9747552B-575F-4E0F-AF39-668B28EBB537"), ApplicationModuleResourceId = Guid.Parse("33C98AA3-CB90-4724-8EAD-45959C3C6551"), PermissionActionId = Guid.Parse("8B31C770-CD0D-440F-83E4-B7A9469B5BCE"), Name = "Personnel Tracking" },
                    new Permission { Id = Guid.Parse("A573A0B8-0B92-4D4E-8396-F3A87EF41F69"), ApplicationModuleResourceId = Guid.Parse("33C98AA3-CB90-4724-8EAD-45959C3C6551"), PermissionActionId = Guid.Parse("8B31C770-CD0D-440F-83E4-B7A9469B5BCE"), Name = "Personnel Latest Status" },
                    new Permission { Id = Guid.Parse("EF265174-3703-497D-A10D-870FCDDC31EF"), ApplicationModuleResourceId = Guid.Parse("A515CCD7-CFBE-4407-9959-DF4CEAFBD444"), PermissionActionId = Guid.Parse("8B31C770-CD0D-440F-83E4-B7A9469B5BCE"), Name = "Sales Report" },
                    new Permission { Id = Guid.Parse("91EBFC80-A8CA-4D82-A7BE-303F73B78961"), ApplicationModuleResourceId = Guid.Parse("A515CCD7-CFBE-4407-9959-DF4CEAFBD444"), PermissionActionId = Guid.Parse("8B31C770-CD0D-440F-83E4-B7A9469B5BCE"), Name = "Financial Report" },
                    new Permission { Id = Guid.Parse("EB1DB303-AF51-4BD6-A69A-24F995ED7D74"), ApplicationModuleResourceId = Guid.Parse("A515CCD7-CFBE-4407-9959-DF4CEAFBD444"), PermissionActionId = Guid.Parse("8B31C770-CD0D-440F-83E4-B7A9469B5BCE"), Name = "Delivery Report" },
                    new Permission { Id = Guid.Parse("35A62A10-451F-4FC7-B3E5-2E98EDCB6BCD"), ApplicationModuleResourceId = Guid.Parse("68F751E3-8744-4775-8942-39501F56452F"), PermissionActionId = Guid.Parse("8B31C770-CD0D-440F-83E4-B7A9469B5BCE"), Name = "Stock Page" },
                    new Permission { Id = Guid.Parse("6E6C61BC-7B18-4757-AE2F-3D411B7B5E0F"), ApplicationModuleResourceId = Guid.Parse("D18144DB-DAAC-4BF8-8FB9-B852DABC1F0C"), PermissionActionId = Guid.Parse("8B31C770-CD0D-440F-83E4-B7A9469B5BCE"), Name = "Stock Request History Page" },
                    new Permission { Id = Guid.Parse("A9D36EAF-8C6F-42D6-B496-880ED5F2E442"), ApplicationModuleResourceId = Guid.Parse("7BA8CC86-CB32-403E-A7D1-E53710BBDC02"), PermissionActionId = Guid.Parse("8B31C770-CD0D-440F-83E4-B7A9469B5BCE"), Name = "Permision Page" },
                    new Permission { Id = Guid.Parse("05321E5D-3FFF-41AB-821A-AF47FBE8B53E"), ApplicationModuleResourceId = Guid.Parse("12BA035E-B336-41B5-8DBF-1C7DE2162139"), PermissionActionId = Guid.Parse("8B31C770-CD0D-440F-83E4-B7A9469B5BCE"), Name = "Stock Product Page" },
                    new Permission { Id = Guid.Parse("40AF7559-8D25-4523-849C-BB71D9A49C32"), ApplicationModuleResourceId = Guid.Parse("1FD9D17F-5AAC-4B27-A8A9-0DCCF84FE8A2"), PermissionActionId = Guid.Parse("8B31C770-CD0D-440F-83E4-B7A9469B5BCE"), Name = "User Management Page" },
                    new Permission { Id = Guid.Parse("87FE4232-11C3-4885-B3C2-EBEEB7F45C1A"), ApplicationModuleResourceId = Guid.Parse("BE2B20D7-D1B7-44EE-80DA-55C07C9782A9"), PermissionActionId = Guid.Parse("8B31C770-CD0D-440F-83E4-B7A9469B5BCE"), Name = "Product Request Rule Page" },
                    new Permission { Id = Guid.Parse("9373E348-E5E4-4521-BD17-8AE597DACA58"), ApplicationModuleResourceId = Guid.Parse("DD0A28C1-F0B4-4513-8B03-622AA9F66E09"), PermissionActionId = Guid.Parse("8B31C770-CD0D-440F-83E4-B7A9469B5BCE"), Name = "User Stocks Page" },
                    new Permission { Id = Guid.Parse("ECD68A39-8F5F-43C6-9B58-FA978057374D"), ApplicationModuleResourceId = Guid.Parse("D9AC9D01-A00C-4201-9C2D-4F92D29358E9"), PermissionActionId = Guid.Parse("8B31C770-CD0D-440F-83E4-B7A9469B5BCE"), Name = "Product Page" }
                    );

                context.PermissionCatalogs.AddOrUpdate(item => item.Id,
                    new PermissionCatalog { Id = Guid.Parse("017664FA-676A-43AD-B32B-E8E4CE84C609"), Name = "سامانه راهبری مبتنی بر نقشه", PermissionCatalougeParentId = null },
                    new PermissionCatalog { Id = Guid.Parse("863F50DF-1191-43FA-916E-034A5565C3B9"), Name = "مدیریت کاربران", PermissionCatalougeParentId = Guid.Parse("017664FA-676A-43AD-B32B-E8E4CE84C609") },
                    new PermissionCatalog { Id = Guid.Parse("E88D621C-876F-4BC1-B9EB-075191E23F5E"), Name = "مدیریت دسترسی", PermissionCatalougeParentId = Guid.Parse("017664FA-676A-43AD-B32B-E8E4CE84C609") },
                    new PermissionCatalog { Id = Guid.Parse("7096ED44-B450-4885-9A0C-2D64E848AE93"), Name = "مسیر بندی", PermissionCatalougeParentId = Guid.Parse("017664FA-676A-43AD-B32B-E8E4CE84C609") },
                    new PermissionCatalog { Id = Guid.Parse("3DA3BE95-E92B-4A5F-B37E-41A668E1DCF3"), Name = "ردیابی", PermissionCatalougeParentId = Guid.Parse("017664FA-676A-43AD-B32B-E8E4CE84C609") },
                    new PermissionCatalog { Id = Guid.Parse("924698F0-034B-422F-A23D-5879C7E42828"), Name = "آخرین وضعیت ", PermissionCatalougeParentId = Guid.Parse("017664FA-676A-43AD-B32B-E8E4CE84C609") },
                    new PermissionCatalog { Id = Guid.Parse("1D7B9A8A-A20E-4224-994E-5E9F3B57809D"), Name = "گزارش فروش", PermissionCatalougeParentId = Guid.Parse("017664FA-676A-43AD-B32B-E8E4CE84C609") },
                    new PermissionCatalog { Id = Guid.Parse("09553C3D-7CB4-42D9-A351-9E94264BAD69"), Name = "گزارش مالی", PermissionCatalougeParentId = Guid.Parse("017664FA-676A-43AD-B32B-E8E4CE84C609") },
                    new PermissionCatalog { Id = Guid.Parse("1923CC4A-DD7C-4E29-A5F3-A1C749506B36"), Name = "گزارش توزیع", PermissionCatalougeParentId = Guid.Parse("017664FA-676A-43AD-B32B-E8E4CE84C609") },
                    new PermissionCatalog { Id = Guid.Parse("BEEBAE84-6690-421C-8E94-9BAD1EFC6856"), Name = "سامانه تامین مرکزی", PermissionCatalougeParentId = null },
                    new PermissionCatalog { Id = Guid.Parse("C9126FD9-6B3B-4F8D-8C41-C4220C5ABEAB"), Name = "مدیریت کاربران", PermissionCatalougeParentId = Guid.Parse("BEEBAE84-6690-421C-8E94-9BAD1EFC6856") },
                    new PermissionCatalog { Id = Guid.Parse("1156B463-1E62-4AA2-9035-AA3BEF5AEB62"), Name = "مدیریت دسترسی", PermissionCatalougeParentId = Guid.Parse("BEEBAE84-6690-421C-8E94-9BAD1EFC6856") },
                    new PermissionCatalog { Id = Guid.Parse("493D293A-A4B8-4778-8257-55403FF6A9DA"), Name = "مدیریت انبار های کاربر", PermissionCatalougeParentId = Guid.Parse("BEEBAE84-6690-421C-8E94-9BAD1EFC6856") },
                    new PermissionCatalog { Id = Guid.Parse("ED720A26-9643-42FA-8B4B-F5DC889ACC4A"), Name = "بازبینی درخواست ها", PermissionCatalougeParentId = Guid.Parse("BEEBAE84-6690-421C-8E94-9BAD1EFC6856") },
                    new PermissionCatalog { Id = Guid.Parse("F222A442-DA75-4A34-9EF7-D86B408AF0A9"), Name = "سوابق درخواست ها", PermissionCatalougeParentId = Guid.Parse("BEEBAE84-6690-421C-8E94-9BAD1EFC6856") },
                    new PermissionCatalog { Id = Guid.Parse("12BBABB4-E9B7-4091-8F21-E462156FFDA7"), Name = "مدیریت انبارها", PermissionCatalougeParentId = Guid.Parse("BEEBAE84-6690-421C-8E94-9BAD1EFC6856") },
                    new PermissionCatalog { Id = Guid.Parse("0F1605B6-0EF1-4CE6-98EF-E172BF234973"), Name = "مدیریت کالاهای انبار", PermissionCatalougeParentId = Guid.Parse("BEEBAE84-6690-421C-8E94-9BAD1EFC6856") },
                    new PermissionCatalog { Id = Guid.Parse("4292B492-8195-47B7-B684-E2D222B8754C"), Name = "قوانین", PermissionCatalougeParentId = Guid.Parse("BEEBAE84-6690-421C-8E94-9BAD1EFC6856") },
                    new PermissionCatalog { Id = Guid.Parse("56D12CA0-5F50-47CD-92A4-D88D4F024FCB"), Name = "مدیریت کالا", PermissionCatalougeParentId = Guid.Parse("BEEBAE84-6690-421C-8E94-9BAD1EFC6856") }
                    );

                #endregion

                #region Add Users
                #region Anatoli VN Users
                var userId = "02D3C1AA-6149-4810-9F83-DF3928BFDF16";
                var applicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240");
                var dataOwnerId = applicationOwnerId;

                context.Principals.AddOrUpdate(item => item.Id,
                    new Principal
                    {
                        Id = Guid.Parse(userId),
                        ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"),
                    });

                if (!context.Users.Any(item => item.Id == userId && item.DataOwnerId == dataOwnerId && item.ApplicationOwnerId == applicationOwnerId))
                {
                    context.Users.AddOrUpdate(item => item.Id,
                        new User
                        {
                            Id = userId,
                            PrincipalId = Guid.Parse(userId),
                            PhoneNumber = "87135000",
                            UserName = "anatoli",
                            UserNameStr = "anatoli",
                            PasswordHash = "ALcPM/XAJNHiJUxe/vMfqY4eCRmDOGnJsjd2lCC8fSPdnh19Phs6+4EkTZxXz7xBqA==",
                            Email = "anatoli@varanegar.com",
                            EmailConfirmed = true,
                            AnatoliContactId = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16"),
                            PhoneNumberConfirmed = true,
                            CreatedDate = DateTime.Now,
                            LastUpdate = DateTime.Now,
                            LastEntry = DateTime.Now,
                            DataOwnerId = dataOwnerId,
                            ApplicationOwnerId = applicationOwnerId,
                            SecurityStamp = "d752d1f2-2fe5-4fb4-b47c-580329de70d5",
                        });

                }

                if (context.Users.Any(item => item.Id == userId))
                {
                    context.UserRoles.AddOrUpdate(item => new { item.RoleId, item.UserId },
                        new IdentityUserRole { RoleId = "507b6966-17f1-4116-a497-02242c052961", UserId = "02d3c1aa-6149-4810-9f83-df3928bfdf16" },
                        new IdentityUserRole { RoleId = "5a61344b-b1b5-4157-8861-7bed15c0bdc2", UserId = "02d3c1aa-6149-4810-9f83-df3928bfdf16" },
                        new IdentityUserRole { RoleId = "4d10bd96-7f25-477a-a544-75e54b619a1f", UserId = "02d3c1aa-6149-4810-9f83-df3928bfdf16" },
                        new IdentityUserRole { RoleId = "C0614C05-855F-45C6-A93C-EB3B8A8B2D94", UserId = "02d3c1aa-6149-4810-9f83-df3928bfdf16" },
                        new IdentityUserRole { RoleId = "AE4AF236-E229-45A8-B1C0-CBE6CB104721", UserId = "02d3c1aa-6149-4810-9f83-df3928bfdf16" }
                        
                    );
                }

                userId = "E8724E69-0A81-4DC6-87FE-FDA91D1D2EC2";
                context.Principals.AddOrUpdate(item => item.Id,
                    new Principal
                    {
                        Id = Guid.Parse(userId),
                        ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"),
                    });


                applicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240");
                dataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240");
                if (!context.Users.Any(item => item.Id == userId && item.DataOwnerId == dataOwnerId && item.ApplicationOwnerId == applicationOwnerId))
                {
                    context.Users.AddOrUpdate(item => item.Id,
                        new User
                        {
                            Id = userId,
                            UserName = "AnatoliMobileApp",
                            PrincipalId = Guid.Parse(userId),
                            PhoneNumber = "87135000",
                            UserNameStr = "AnatoliMobileApp",
                            PasswordHash = "AA7XiPMTyUfecJ0H6MYalhVvkX7JnNaNXt+OCy8bQYm5tkvzPfZFVFDIoLbwYWzQsA==",
                            Email = "anatolimobileapp@varanegar.com",
                            EmailConfirmed = true,
                            //AnatoliContactId = Guid.Parse("E8724E69-0A81-4DC6-87FE-FDA91D1D2EC2"),
                            PhoneNumberConfirmed = true,
                            CreatedDate = DateTime.Now,
                            LastUpdate = DateTime.Now,
                            LastEntry = DateTime.Now,
                            DataOwnerId = dataOwnerId,
                            ApplicationOwnerId = applicationOwnerId,
                            SecurityStamp = "4e3b2471-3700-405b-be71-53be82205fa5",
                        });

                }

                if (context.Users.Any(item => item.Id == userId))
                {
                    context.UserRoles.AddOrUpdate(item => new { item.RoleId, item.UserId },
                        new IdentityUserRole { RoleId = "507b6966-17f1-4116-a497-02242c052961", UserId = "E8724E69-0A81-4DC6-87FE-FDA91D1D2EC2" },
                        new IdentityUserRole { RoleId = "4447853b-e19f-42ce-bb29-f5aa1943b542", UserId = "E8724E69-0A81-4DC6-87FE-FDA91D1D2EC2" },
                        new IdentityUserRole { RoleId = "5a61344b-b1b5-4157-8861-7bed15c0bdc2", UserId = "E8724E69-0A81-4DC6-87FE-FDA91D1D2EC2" },
                        new IdentityUserRole { RoleId = "4d10bd96-7f25-477a-a544-75e54b619a1f", UserId = "E8724E69-0A81-4DC6-87FE-FDA91D1D2EC2" },
                        new IdentityUserRole { RoleId = "C0614C05-855F-45C6-A93C-EB3B8A8B2D94", UserId = "E8724E69-0A81-4DC6-87FE-FDA91D1D2EC2" },
                        new IdentityUserRole { RoleId = "AE4AF236-E229-45A8-B1C0-CBE6CB104721", UserId = "E8724E69-0A81-4DC6-87FE-FDA91D1D2EC2" }
                    );
                }
                #endregion
                #region Petropay Users
                applicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240");
                dataOwnerId = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");

                userId = "b65e8981-88c5-4391-84ad-36416345681d";

                context.Principals.AddOrUpdate(item => item.Id,
                    new Principal
                    {
                        Id = Guid.Parse(userId),
                        ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"),
                    });

                if (!context.Users.Any(item => item.Id == userId && item.DataOwnerId == dataOwnerId && item.ApplicationOwnerId == applicationOwnerId))
                {
                    context.Users.AddOrUpdate(item => item.Id,
                        new User
                        {
                            Id = userId,
                            PrincipalId = Guid.Parse(userId),
                            UserName = userId,
                            PhoneNumber = "87135000",
                            UserNameStr = "AnatoliMobileApp",
                            PasswordHash = "AA7XiPMTyUfecJ0H6MYalhVvkX7JnNaNXt+OCy8bQYm5tkvzPfZFVFDIoLbwYWzQsA==",
                            Email = "anatolimobileapp@varanegar.com",
                            EmailConfirmed = true,
                            //AnatoliContactId = Guid.Parse("E8724E69-0A81-4DC6-87FE-FDA91D1D2EC2"),
                            PhoneNumberConfirmed = true,
                            CreatedDate = DateTime.Now,
                            LastUpdate = DateTime.Now,
                            LastEntry = DateTime.Now,
                            DataOwnerId = dataOwnerId,
                            ApplicationOwnerId = applicationOwnerId,
                            SecurityStamp = "4e3b2471-3700-405b-be71-53be82205fa5",
                        });

                }

                if (context.Users.Any(item => item.Id == userId))
                {
                    context.UserRoles.AddOrUpdate(item => new { item.RoleId, item.UserId },
                        new IdentityUserRole { RoleId = "507b6966-17f1-4116-a497-02242c052961", UserId = userId },
                        new IdentityUserRole { RoleId = "5a61344b-b1b5-4157-8861-7bed15c0bdc2", UserId = userId },
                        new IdentityUserRole { RoleId = "4d10bd96-7f25-477a-a544-75e54b619a1f", UserId = userId },
                        new IdentityUserRole { RoleId = "C0614C05-855F-45C6-A93C-EB3B8A8B2D94", UserId = userId },
                        new IdentityUserRole { RoleId = "AE4AF236-E229-45A8-B1C0-CBE6CB104721", UserId = userId }
                    );
                }

                userId = "c7ed66ed-d02b-4dd8-9490-eaf5804ff8ef";

                context.Principals.AddOrUpdate(item => item.Id,
                    new Principal
                    {
                        Id = Guid.Parse(userId),
                        ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"),
                    });

                if (!context.Users.Any(item => item.Id == userId && item.DataOwnerId == dataOwnerId && item.ApplicationOwnerId == applicationOwnerId))
                {
                    context.Users.AddOrUpdate(item => item.Id,
                        new User
                        {
                            Id = userId,
                            PrincipalId = Guid.Parse(userId),
                            UserName = userId,
                            PhoneNumber = "87135000",
                            UserNameStr = "petropay",
                            PasswordHash = "AOw6dMvdSydP0geii72BK6vtgL+omhMNHlMhNMUoGgH4eF7hlmVdCF7E9v1c+uahCA==",
                            Email = "petropay@varanegar.com",
                            EmailConfirmed = true,
                            //AnatoliContactId = Guid.Parse("E8724E69-0A81-4DC6-87FE-FDA91D1D2EC2"),
                            PhoneNumberConfirmed = true,
                            CreatedDate = DateTime.Now,
                            LastUpdate = DateTime.Now,
                            LastEntry = DateTime.Now,
                            DataOwnerId = dataOwnerId,
                            ApplicationOwnerId = applicationOwnerId,
                            SecurityStamp = "81434395-72c2-4def-8c09-853b08f233d9",
                        });

                }

                if (context.Users.Any(item => item.Id == userId))
                {
                    context.UserRoles.AddOrUpdate(item => new { item.RoleId, item.UserId },
                        new IdentityUserRole { RoleId = "4d10bd96-7f25-477a-a544-75e54b619a1f", UserId = userId },
                        new IdentityUserRole { RoleId = "C0614C05-855F-45C6-A93C-EB3B8A8B2D94", UserId = userId },
                        new IdentityUserRole { RoleId = "AE4AF236-E229-45A8-B1C0-CBE6CB104721", UserId = userId }
                    );
                }
                #endregion

                #region Base Data
                context.AnatoliContactTypes.AddOrUpdate(item => item.Id,
                    new AnatoliContactType { Id = Guid.Parse("0B8F7429-B33C-4209-ABED-1192E7B36657"), Name = "حقیقی" },
                    new AnatoliContactType { Id = Guid.Parse("8E1DB1BC-52FD-456B-95FB-F2BBCA07C87D"), Name = "حقوقی" },
                    new AnatoliContactType { Id = Guid.Parse("ED1220EF-5BD2-4BFE-B5C8-7D3713143A6A"), Name = "فروشگاه" }
                );

                context.AnatoliContacts.AddOrUpdate(
                    new AnatoliContact { Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), AnatoliContactTypeId = Guid.Parse("8E1DB1BC-52FD-456B-95FB-F2BBCA07C87D"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, IsRemoved = false, ContactName = "نیک توشه زیست", Phone = "+98-21-", Email = "info@eiggstores.com", Website = "http://www.eiggstores.com/" },
                    new AnatoliContact { Id = Guid.Parse("DD86E785-7171-498E-A9BB-82E1DBE334EE"), AnatoliContactTypeId = Guid.Parse("8E1DB1BC-52FD-456B-95FB-F2BBCA07C87D"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, IsRemoved = false, ContactName = "شرکت دومینو", Phone = "+98-21-", Email = "info@", Website = "" },
                    new AnatoliContact { Id = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16"), AnatoliContactTypeId = Guid.Parse("8E1DB1BC-52FD-456B-95FB-F2BBCA07C87D"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, IsRemoved = false, ContactName = "داده کاوان پیشرو ایده ورانگر", Phone = "+98-21-87134", Email = "info@varanegar.com", Website = "http://www.varanegar.com/" }
                    );


                context.ApplicationOwners.AddOrUpdate(item => item.Id,
                    new ApplicationOwner { Id = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), AnatoliContactId = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16"), Title = "نرم افزارهای ابری داده کاوان پیشرو ایده ورانگر", WebHookUsername = "anatoli-inter-com@varanegar.com" },
                    new ApplicationOwner { Id = Guid.Parse("E0468F3E-CA81-4867-8768-52F18887BEF8"), ApplicationId = Guid.Parse("8A074FD5-9311-4F8E-AF47-0572DE1A7B6A"), AnatoliContactId = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16"), Title = "Parastoo Market Place", WebHookUsername = "anatoli-inter-com@varanegar.com" }
                    //new ApplicationOwner { Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), AnatoliContactId = Guid.Parse("D186DFBC-611F-42ED-B3D8-ACF92A8DE3C9"), Title = "نرم افزار فروشگاهی نیک توشه زیست", WebHookUsername = "anatoli-inter-com@varanegar.com" },
                    //new ApplicationOwner { Id = Guid.Parse("897DAD91-EB44-407A-9C63-9132132E1F99"), ApplicationId = Guid.Parse("61B8646F-77D4-49D1-8949-909EA771DEED"), AnatoliContactId = Guid.Parse("D186DFBC-611F-42ED-B3D8-ACF92A8DE3C9"), Title = "نرم افزار تامین داخلی نیک توشه زیست", WebHookUsername = "anatoli-inter-com@varanegar.com" }
                );


                context.DataOwners.AddOrUpdate(item => item.Id,
                    new DataOwner { Id = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), AnatoliContactId = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16"), Title = "نرم افزارهای ابری داده کاوان پیشرو ایده ورانگر", WebHookUsername = "anatoli-inter-com@varanegar.com" },
                    new DataOwner { Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), AnatoliContactId = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), Title = "نرم افزار فروش اینترنتی نیک توشه زیست", WebHookUsername = "info@eiggstores.com" },
                    new DataOwner { Id = Guid.Parse("DD86E785-7171-498E-A9BB-82E1DBE334EE"), ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), AnatoliContactId = Guid.Parse("DD86E785-7171-498E-A9BB-82E1DBE334EE"), Title = "نرم افزار ردیابی شرکت دومینو", WebHookUsername = "" }
                );

                context.DataOwnerCenters.AddOrUpdate(item => item.Id,
                    new DataOwnerCenter { Id = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), AnatoliContactId = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16"), Title = "نرم افزارهای ابری داده کاوان پیشرو ایده ورانگر", WebHookUsername = "anatoli-inter-com@varanegar.com" },
                    new DataOwnerCenter { Id = Guid.Parse("02313882-9767-446D-B4CE-54004EF0AAC4"), DataOwnerId = Guid.Parse("DD86E785-7171-498E-A9BB-82E1DBE334EE"), AnatoliContactId = Guid.Parse("DD86E785-7171-498E-A9BB-82E1DBE334EE"), Title = "دومینو دفتر تهران", WebHookUsername = "" },
                    new DataOwnerCenter { Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), DataOwnerId = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), AnatoliContactId = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), Title = "نرم افزار فروش اینترنتی نیک توشه زیست", WebHookUsername = "info@eiggstores.com" }
                );

                context.Roles.AddOrUpdate(item => item.Id,
                    new Role { Id = "4447853b-e19f-42ce-bb29-f5aa1943b542", ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), Name = "User" },
                    new Role { Id = "4d10bd96-7f25-477a-a544-75e54b619a1f", ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), Name = "AuthorizedApp" },
                    new Role { Id = "C0614C05-855F-45C6-A93C-EB3B8A8B2D94", ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), Name = "DataSync" },
                    new Role { Id = "507b6966-17f1-4116-a497-02242c052961", ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), Name = "SuperAdmin" },
                    new Role { Id = "5a61344b-b1b5-4157-8861-7bed15c0bdc2", ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), Name = "Admin" },
                    new Role { Id = "AE4AF236-E229-45A8-B1C0-CBE6CB104721", ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), Name = "InternalCommunication" }
                );


                #endregion

                #region Domino Users
                applicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240");
                dataOwnerId = Guid.Parse("DD86E785-7171-498E-A9BB-82E1DBE334EE");

                userId = "D0BBD3F2-CF76-4C5B-9336-0D38F1B6DAAB";

                context.Principals.AddOrUpdate(item => item.Id,
                    new Principal
                    {
                        Id = Guid.Parse(userId),
                        ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"),
                    });

                if (!context.Users.Any(item => item.Id == userId && item.DataOwnerId == dataOwnerId && item.ApplicationOwnerId == applicationOwnerId))
                {
                    context.Users.AddOrUpdate(item => item.Id,
                        new User
                        {
                            Id = userId,
                            PrincipalId = Guid.Parse(userId),
                            UserName = userId,
                            PhoneNumber = "87135000",
                            UserNameStr = "AnatoliMobileApp",
                            PasswordHash = "AEeZFHD0JoCSW5J3+p7qq983Qp72sWRd1LEiLKSJQicxnQUNBijUM3KSw1kyGSa0KA==",
                            Email = "anatolimobileapp@varanegar.com",
                            EmailConfirmed = true,
                            //AnatoliContactId = Guid.Parse("E8724E69-0A81-4DC6-87FE-FDA91D1D2EC2"),
                            PhoneNumberConfirmed = true,
                            CreatedDate = DateTime.Now,
                            LastUpdate = DateTime.Now,
                            LastEntry = DateTime.Now,
                            DataOwnerId = dataOwnerId,
                            ApplicationOwnerId = applicationOwnerId,
                            SecurityStamp = "843957f1-640c-4201-96af-d39ce581efb5",
                        });

                }

                if (context.Users.Any(item => item.Id == userId))
                {
                    context.UserRoles.AddOrUpdate(item => new { item.RoleId, item.UserId },
                        new IdentityUserRole { RoleId = "4d10bd96-7f25-477a-a544-75e54b619a1f", UserId = userId }
                    );
                }

                userId = "7EDE6F2E-653B-43DF-AF84-650D533A9571";

                context.Principals.AddOrUpdate(item => item.Id,
                    new Principal
                    {
                        Id = Guid.Parse(userId),
                        ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"),
                    });

                if (!context.Users.Any(item => item.Id == userId && item.DataOwnerId == dataOwnerId && item.ApplicationOwnerId == applicationOwnerId))
                {
                    context.Users.AddOrUpdate(item => item.Id,
                        new User
                        {
                            Id = userId,
                            PrincipalId = Guid.Parse(userId),
                            UserName = userId,
                            PhoneNumber = "87135000",
                            UserNameStr = "domino",
                            PasswordHash = "AJQcFArf7wTB89V3jt2XBCLUzipw1vLmvW4FtlZdw9m4u02WoNzpcTqta6ee+ejJiQ==",
                            Email = "domino@varanegar.com",
                            EmailConfirmed = true,
                            //AnatoliContactId = Guid.Parse("E8724E69-0A81-4DC6-87FE-FDA91D1D2EC2"),
                            PhoneNumberConfirmed = true,
                            CreatedDate = DateTime.Now,
                            LastUpdate = DateTime.Now,
                            LastEntry = DateTime.Now,
                            DataOwnerId = dataOwnerId,
                            ApplicationOwnerId = applicationOwnerId,
                            SecurityStamp = "14658655-8eec-4d9f-895f-c436244b81cb",
                        });

                }

                if (context.Users.Any(item => item.Id == userId))
                {
                    context.UserRoles.AddOrUpdate(item => new { item.RoleId, item.UserId },
                        new IdentityUserRole { RoleId = "4d10bd96-7f25-477a-a544-75e54b619a1f", UserId = userId },
                        new IdentityUserRole { RoleId = "C0614C05-855F-45C6-A93C-EB3B8A8B2D94", UserId = userId },
                        new IdentityUserRole { RoleId = "AE4AF236-E229-45A8-B1C0-CBE6CB104721", UserId = userId },
                        new IdentityUserRole { RoleId = "4447853b-e19f-42ce-bb29-f5aa1943b542", UserId = userId }
                    );
                }
                #endregion

                #endregion

            }
        }
    }
}
