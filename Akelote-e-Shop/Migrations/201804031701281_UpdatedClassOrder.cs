namespace Akelote_e_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedClassOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Username", c => c.String());
            AddColumn("dbo.Orders", "FirstName", c => c.String());
            AddColumn("dbo.Orders", "LastName", c => c.String());
            AddColumn("dbo.Orders", "Address", c => c.String());
            AddColumn("dbo.Orders", "City", c => c.String());
            AddColumn("dbo.Orders", "State", c => c.String());
            AddColumn("dbo.Orders", "PostalCode", c => c.String());
            AddColumn("dbo.Orders", "Country", c => c.String());
            AddColumn("dbo.Orders", "Phone", c => c.String());
            AddColumn("dbo.Orders", "Email", c => c.String());
            CreateIndex("dbo.OrderItems", "OrderId");
            AddForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropColumn("dbo.Orders", "Email");
            DropColumn("dbo.Orders", "Phone");
            DropColumn("dbo.Orders", "Country");
            DropColumn("dbo.Orders", "PostalCode");
            DropColumn("dbo.Orders", "State");
            DropColumn("dbo.Orders", "City");
            DropColumn("dbo.Orders", "Address");
            DropColumn("dbo.Orders", "LastName");
            DropColumn("dbo.Orders", "FirstName");
            DropColumn("dbo.Orders", "Username");
        }
    }
}
