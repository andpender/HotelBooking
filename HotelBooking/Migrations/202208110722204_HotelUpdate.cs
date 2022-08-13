namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HotelUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Hotels", "Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "Longitude");
            DropColumn("dbo.Hotels", "Latitude");
        }
    }
}
