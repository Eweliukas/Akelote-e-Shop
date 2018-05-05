namespace Akelote_e_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018050521213 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Orders", name: "IX_User_Id", newName: "IX_UserId");
            DropColumn("dbo.Orders", "ApplicationUserId");
            DropColumn("dbo.Orders", "Username");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Username", c => c.String());
            AddColumn("dbo.Orders", "ApplicationUserId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Orders", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Orders", name: "UserId", newName: "User_Id");
        }
    }
}
