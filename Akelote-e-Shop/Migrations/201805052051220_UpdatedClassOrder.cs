namespace Akelote_e_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedClassOrder2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Total", c => c.Int(nullable: false));
        }
    }
}
