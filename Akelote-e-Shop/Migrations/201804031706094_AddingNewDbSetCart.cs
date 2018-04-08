namespace Akelote_e_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingNewDbSetCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        AlbumId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .Index(t => t.Item_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "Item_Id", "dbo.Items");
            DropIndex("dbo.Carts", new[] { "Item_Id" });
            DropTable("dbo.Carts");
        }
    }
}
