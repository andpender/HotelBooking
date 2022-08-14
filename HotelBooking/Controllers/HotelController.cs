using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelBooking.DAL;
using HotelBooking.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNet.Identity;

namespace HotelBooking.Controllers
{
    public class HotelController : Controller
    {
        private HotelBookingContext db = new HotelBookingContext();

        // GET: Hotels
        public async Task<ActionResult> Index()
        {
            return View(await db.Hotels.ToListAsync());
        }

        // GET: Hotels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = await db.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // GET: Hotels/Create
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,PhoneNumber,Address")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                var addressGeocodingResponse = GeocodingAsync(hotel.Address).Result;
                if (addressGeocodingResponse != null)
                {
                    var coordinates = addressGeocodingResponse.features.FirstOrDefault().geometry.coordinates;
                    hotel.Longitude = coordinates[0];
                    hotel.Latitude = coordinates[1];
                }
                db.Hotels.Add(hotel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hotel);
        }

        // GET: Hotels/Edit/5
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = await db.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,PhoneNumber,Address")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = await db.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Hotel hotel = await db.Hotels.FindAsync(id);
            db.Hotels.Remove(hotel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Hotel/AddRating
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRating([Bind(Include = "Score,Content,HotelId")] RatingViewModel ratingViewModel)
        {
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid && userId != null)
            {
                var rating = new Rating();
                rating.UserId = userId;
                rating.Hotel = db.Hotels.Find(ratingViewModel.HotelId);
                rating.Score = ratingViewModel.Score;
                rating.Content = ratingViewModel.Content;

                db.Ratings.Add(rating);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = ratingViewModel.HotelId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private async Task<GeocodingResponse> GeocodingAsync(string address)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"https://api.mapbox.com/geocoding/v5/mapbox.places/\"{address}\".json");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"?access_token={Constants.MapBoxPublicToken}").Result;
            if (response.IsSuccessStatusCode) {
                return await response.Content.ReadFromJsonAsync<GeocodingResponse>();
            }

            return null;
        }
    }
}
