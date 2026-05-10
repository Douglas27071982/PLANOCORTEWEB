using Microsoft.AspNetCore.Mvc;
using Otimizador_PC_Web.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Otimizador_PC_Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            string Nome = "";
            string Tipo = "";
            if (claimuser.Identity.IsAuthenticated)
            {
                Nome = claimuser.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();
            }

            if (claimuser.Identity.IsAuthenticated)
            {
                Tipo = claimuser.Claims.Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value).SingleOrDefault();
            }

            ViewData["Nome"] = Nome;
            ViewData["Tipo"] = Tipo;
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
    }
}