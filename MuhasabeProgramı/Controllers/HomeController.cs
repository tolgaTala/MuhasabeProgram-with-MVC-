using Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using MuhasabeProgramı.Models;
using System.Diagnostics;

namespace MuhasabeProgramı.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISalerService salerService;

        public HomeController(ILogger<HomeController> logger, ISalerService salerService)
        {
            _logger = logger;
            this.salerService = salerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var salers = await salerService.GetAllNonDeletedSaler();
            return View(salers);
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
