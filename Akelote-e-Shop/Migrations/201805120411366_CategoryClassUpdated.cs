namespace Akelote_e_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryClassUpdated : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Categories", "ParentId");
            AddForeignKey("dbo.Categories", "ParentId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "ParentId" });
        }
    }
}
