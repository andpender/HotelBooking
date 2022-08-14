namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Hotel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.Hotel_Id)
                .Index(t => t.Hotel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "Hotel_Id", "dbo.Hotels");
            DropIndex("dbo.Ratings", new[] { "Hotel_Id" });
            DropTable("dbo.Ratings");
        }
    }
}
