using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Otimizador_PC_Web.Dados;
using Otimizador_PC_Web.Models;
using Otimizador_PC_Web.Servicos.Contrato;
using System.Security.Claims;

namespace Otimizador_PC_Web.Controllers
{

    public class LoginController : Controller
    {

      
        
        public IActionResult Registrarse()
        {
            return View();
        }

       
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Login1()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha, string tipo)
        {
            // Usuário e senha fixos
            if (email != "Fabiano" || senha != "atualle")
            {
                ViewData["Mensaje"] = "Usuário ou senha inválidos";
                return View();
            }

            // Cria autenticação
            List<Claim> claims = new List<Claim>()
    {
        new Claim(ClaimTypes.Name, "Fabiano"),
        new Claim(ClaimTypes.Role, "Administrador")
    };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );

            return RedirectToAction("Index", "Home");
        }
    }
}
