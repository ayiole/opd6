using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using sha7p.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace sha7p.Controllers
{
    public class HomeController : Controller
    {
        private CitiesDBContext db = new CitiesDBContext();
        public HomeController(CitiesDBContext cities)
        {
            db = cities;
        }

        public IActionResult Index()
        {
        ViewData["Title"] = "Города";

            //List<Country> Countries = new List<Country>()
            //{
            //    new Country { Name = "Россия", Population = 2000, Square = 122 },
            //    new Country { Name = "Украина", Population = 2000, Square = 122 },
            //    new Country { Name = "Швеция", Population = 2000, Square = 122 },
            //    new Country { Name = "Швейцария", Population = 2000, Square = 122 },

            //};


            //List<City> Cities = new List<City>()
            //{
            //    new City { Name = "Санкт-Петербург", FoundingDate = 1703, Population = 5000, Square = 400, Country = Countries.First() },
            //    new City { Name = "Киев", FoundingDate = 1703, Population = 5000, Square = 400, Country = Countries[1] },
            //    new City { Name = "Москва", FoundingDate = 1703, Population = 5000, Square = 400, Country = Countries[2] },
            //    new City { Name = "Минск", FoundingDate = 1703, Population = 5000, Square = 400, Country = Countries[3] }

            //};


            //db.Countries.AddRange(Countries);
            //db.SaveChanges();


            //db.Cities.AddRange(Cities);
            //db.SaveChanges();



            var cities = db.Cities.Include(a => a.Country).ToList();

            //var phones = MemoryDb.Phones;
            return View(cities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Cntr = new SelectList(db.Countries, "Id", "Name");
            City city = new City();
            return View(city);
        }

        [HttpPost]
        public IActionResult Create(City city)
        {
            if (ModelState.IsValid)
            {
                db.Cities.Add(city);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            City cty = new City();
            ViewBag.Cntr= new SelectList(db.Countries, "Id", "Name");
            ViewBag.Message = String.Format("Ошибка!");
            return View(cty);
        }

        [HttpGet]
        public IActionResult EditCity(int Id)
        {
            var city = db.Cities.FirstOrDefault(p => p.Id == Id);
            ViewBag.Cntr = new SelectList(db.Countries, "Id", "Name");
            return View(city);
        }

        [HttpPost]
        public IActionResult EditCity(City city)
        {
            if (ModelState.IsValid)
            {
                db.Cities.Update(city);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var cty = db.Cities.FirstOrDefault(p => p.Id == city.Id);
            ViewBag.Cntr = new SelectList(db.Countries, "Id", "Name");
            ViewBag.Message = String.Format("Ошибка!");
            return View(cty);

        }

        public IActionResult Delete(City city)
        {
            db.Cities.Remove(city);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult CountryInfo(int Id)
        {
            var cntr = db.Countries.Where(c => c.Cities.Where(p => p.Id == Id).FirstOrDefault().Country.Name == c.Name).FirstOrDefault();
            ViewData["Cities"] = db.Cities.Where(p => p.CountryId == cntr.Id);
            return View(cntr);
        }

        [HttpGet]
        public IActionResult EditCountry(int Id)
        {
            var cntr = db.Countries.FirstOrDefault(p => p.Id == Id);
            ViewData["Cities"] = db.Cities.Where(p => p.CountryId == cntr.Id);
            return View(cntr);
        }

        [HttpPost]
        public IActionResult EditCountry(Country country)
        {
            try
            {
                db.Countries.Update(country);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                var cntr = db.Countries.Where(c => country.Id == c.Id).FirstOrDefault();
                ViewData["Cities"] = db.Cities.Where(p => p.Country.Id == cntr.Id);
                ViewBag.Message = String.Format("Ошибка!");
                return View(cntr);
            }
        }

        public IActionResult Stats(int Id)
        {

            ViewData["Countries"] = db.Countries;

            ViewData["Cities"] = db.Cities;

            var cntr = db.Countries.Where(p => p.Id == Id).FirstOrDefault();

            var cities = db.Cities;

            List<int> CountsCities = new List<int>();
            List<int> SumPopulation = new List<int>();
            List<int> AvgSquare = new List<int>();

            foreach (var item in db.Countries.Include(c => c.Cities))
            {
                int count = item.Cities.Count;
                CountsCities.Add(count);

                int sumP = 0;
                int sumS = 0;

                foreach (var c in item.Cities)
                {
                    sumS += c.Square;
                    sumP += c.Population;
                }
                SumPopulation.Add(sumP);
                AvgSquare.Add(sumS / count);
            }

            ViewData["CountsCities"] = CountsCities;
            ViewData["AvgSquare"] = AvgSquare;
            ViewData["SumPopulation"] = SumPopulation;

            return View(cntr);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
