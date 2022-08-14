namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomsUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rooms", "NumBeds", c => c.Int(nullable: false));
            DropColumn("dbo.Rooms", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Rooms", "NumBeds", c => c.String(nullable: false));
        }
    }
}
