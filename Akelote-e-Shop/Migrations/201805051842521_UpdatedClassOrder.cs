namespace Akelote_e_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedClassOrder1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Address");
            DropColumn("dbo.Orders", "City");
            DropColumn("dbo.Orders", "State");
            DropColumn("dbo.Orders", "PostalCode");
            DropColumn("dbo.Orders", "Country");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Country", c => c.String());
            AddColumn("dbo.Orders", "PostalCode", c => c.String());
            AddColumn("dbo.Orders", "State", c => c.String());
            AddColumn("dbo.Orders", "City", c => c.String());
            AddColumn("dbo.Orders", "Address", c => c.String());
        }
    }
}
