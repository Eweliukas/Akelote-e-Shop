namespace Akelote_e_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'133adb5a-58e8-4d01-b849-e91567674c01', N'guest@guest.com', 0, N'AAlGXKBNei8tReOpz6BCf/dZlfYEmNdhEhEV3qKiJiXS6sA5SPRQ35KJ/yGEj3u7rA==', N'72a28131-3026-43ce-92a6-524c60b1475b', NULL, 0, 0, NULL, 1, 0, N'guest@guest.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'fad3b400-9cbf-4430-9562-ae96c722730b', N'admin@admin.com', 0, N'ANVOox619b6teTiE816vW2gxNceOdhDHWWCED15ZYNzkQNF0bI71xh5b5PAwGMQcow==', N'1184ca04-a3df-4161-9efd-ec5f839c6a59', NULL, 0, 0, NULL, 1, 0, N'admin@admin.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'53ed95d2-c1ac-444a-90aa-bcb1e529b4c8', N'CanUseAdminAccess')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fad3b400-9cbf-4430-9562-ae96c722730b', N'53ed95d2-c1ac-444a-90aa-bcb1e529b4c8')

");
        }
        
        public override void Down()
        {
        }
    }
}
