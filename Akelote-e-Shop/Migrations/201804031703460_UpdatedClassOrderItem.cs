namespace Akelote_e_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedClassOrderItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "Quantity", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderItems", "ItemId");
            AddForeignKey("dbo.OrderItems", "ItemId", "dbo.Items", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "ItemId", "dbo.Items");
            DropIndex("dbo.OrderItems", new[] { "ItemId" });
            DropColumn("dbo.OrderItems", "Quantity");
        }
    }
}
