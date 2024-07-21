namespace ECommerce.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColoumnsAddedToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}
