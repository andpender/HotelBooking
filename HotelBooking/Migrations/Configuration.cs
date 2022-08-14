namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HotelBooking.DAL.HotelBookingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "HotelBooking.DAL.HotelBookingContext";
        }

        protected override void Seed(HotelBooking.DAL.HotelBookingContext context)
        {
            context.Ratings.AddOrUpdate(x => x.Id,
                new Models.Rating() { Id = 1, Score = 1, UserId = "a", Content = "This was great", Hotel = context.Hotels.Single(h => h.Id == 13) },
                new Models.Rating() { Id = 2, Score = 1, UserId = "a", Content = "This was great", Hotel = context.Hotels.Single(h => h.Id == 13) },
                new Models.Rating() { Id = 3, Score = 3, UserId = "a", Content = "This was great", Hotel = context.Hotels.Single(h => h.Id == 13) },
                new Models.Rating() { Id = 4, Score = 3, UserId = "a", Content = "This was great", Hotel = context.Hotels.Single(h => h.Id == 13) },
                new Models.Rating() { Id = 5, Score = 4, UserId = "a", Content = "This was great", Hotel = context.Hotels.Single(h => h.Id == 13) },
                new Models.Rating() { Id = 6, Score = 5, UserId = "a", Content = "This was great", Hotel = context.Hotels.Single(h => h.Id == 13) }
                );
        }
    }
}
