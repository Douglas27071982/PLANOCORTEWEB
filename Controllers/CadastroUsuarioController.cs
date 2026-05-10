using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otimizador_PC_Web.Controllers;
using Otimizador_PC_Web.Dados;
using Otimizador_PC_Web.Models;
using Otimizador_PC_Web.Servicos.Contrato;

namespace Indicador_OEE_Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class CadastroUsuarioController : Controller
    {

        Da_Usuario _dausuario = new Da_Usuario();
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnv;


        public Da_Usuario dausuario = new Da_Usuario();

        private readonly IUsuarioService _usuarioServicio;
        public CadastroUsuarioController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CadastroUsuario()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListaUsuarios()
        {
            List<Usuario> oLista = new List<Usuario>();
            oLista = _dausuario.ListarUsuario();
            return Json(new { data = oLista });




        }

        //função para salvar novos Usuarioa ou editar
        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario([FromBody] Usuario obj)
        {
            string operacion = Request.Headers["operacion"];
            bool respuesta;

            if (operacion == "crear")

            {
                obj.Senha = Utilidades.EncriptarSenha(obj.Senha);
                respuesta = true;
                Usuario usuario_creado = await _usuarioServicio.SaveUsuario(obj);

            }
            else
            {
                Usuario usuario_creado = await _usuarioServicio.SaveUsuario(obj);
                respuesta = false;
            }

            return Json(new { respuesta = respuesta });

        }


        [HttpDelete]
        public JsonResult Exluir(int idusuario)
        {
            bool respuesta;
            respuesta = _dausuario.ExcluirUsuario(idusuario);
            return Json(new { respuesta = respuesta });
        }




    }
}
