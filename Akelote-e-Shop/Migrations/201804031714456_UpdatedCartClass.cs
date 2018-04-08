namespace Akelote_e_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedCartClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "Item_Id", "dbo.Items");
            DropIndex("dbo.Carts", new[] { "Item_Id" });
            RenameColumn(table: "dbo.Carts", name: "Item_Id", newName: "ItemId");
            AlterColumn("dbo.Carts", "ItemId", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "ItemId");
            AddForeignKey("dbo.Carts", "ItemId", "dbo.Items", "Id", cascadeDelete: true);
            DropColumn("dbo.Carts", "AlbumId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "AlbumId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Carts", "ItemId", "dbo.Items");
            DropIndex("dbo.Carts", new[] { "ItemId" });
            AlterColumn("dbo.Carts", "ItemId", c => c.Int());
            RenameColumn(table: "dbo.Carts", name: "ItemId", newName: "Item_Id");
            CreateIndex("dbo.Carts", "Item_Id");
            AddForeignKey("dbo.Carts", "Item_Id", "dbo.Items", "Id");
        }
    }
}
