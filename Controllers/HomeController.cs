using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ContactForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;

namespace ContactForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _filePath;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _filePath = configuration["FileConfiguration:Filepath"];
        }

        public IActionResult Index()
        {
            return View();
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

        [HttpPost]
        public ActionResult Index(PersonModel person)
        {         
           
            PushPersonDetailsToFile(person);
            return View();
        }

        private void PushPersonDetailsToFile(PersonModel person)
        {
            var path = string.Format(_filePath, $"{DateTime.Today:MM-dd-yy}-{DateTime.Now:%H}");
            using (StreamWriter sw = System.IO.File.AppendText(path))
            {
                    sw.WriteLine(person.FirstName);
                    sw.WriteLine(person.LastName);
                    sw.WriteLine(person.City);
            }
           
        }
    }
}
