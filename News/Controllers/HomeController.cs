using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using News.Models;
using News.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace News.Controllers
{
    public class HomeController : Controller
    { 
        private readonly ILogger<HomeController> _logger;
        private IPageRepository _pageRepository;
        public HomeController(IPageRepository pageRepository,ILogger<HomeController> logger)
        {
            _logger = logger;
            _pageRepository = pageRepository;
        }

        public IActionResult Index()
        {
            ViewData["Slider"] = _pageRepository.GetPagesInSlider();
            return View(_pageRepository.GetLatesPage());
        }
        [Route("/error/404")]
        public IActionResult Error404(){
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
