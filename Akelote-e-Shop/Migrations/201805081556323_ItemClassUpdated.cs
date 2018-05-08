namespace Akelote_e_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemClassUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Deleted");
        }
    }
}
