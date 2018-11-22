using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task4.Models;
using Microsoft.EntityFrameworkCore;

namespace Task4.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieContext _context;

        public HomeController(MovieContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/movies")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Movies(string searchString)
        {
            var movies = from movie in _context.Movies select movie;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(m => m.Name.ToLower().Contains(searchString));
            }

            return Json(movies);
        }

        [HttpPost("/movies/edit")]
        public ActionResult EditMovie(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return Content("Success");
        }
    }
}
