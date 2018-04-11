namespace Akelote_e_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedForeignKeyValues : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsBlocked", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Items", "CategoryId");
            CreateIndex("dbo.Properties", "CategoryId");
            CreateIndex("dbo.Images", "ItemId");
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Items", "CategoryId", "dbo.Categories", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Properties", "CategoryId", "dbo.Categories", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Images", "ItemId", "dbo.Items", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Orders", "UserId", "dbo.Users", "Id", cascadeDelete: false);
            DropColumn("dbo.Users", "IsBloked");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "IsBloked", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Images", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Properties", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Items", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Images", new[] { "ItemId" });
            DropIndex("dbo.Properties", new[] { "CategoryId" });
            DropIndex("dbo.Items", new[] { "CategoryId" });
            DropColumn("dbo.Users", "IsBlocked");
        }
    }
}
