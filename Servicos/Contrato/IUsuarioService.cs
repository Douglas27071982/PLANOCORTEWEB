using Otimizador_PC_Web.Models;

namespace Otimizador_PC_Web.Servicos.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string email, string senha, string tipo);
        Task<Usuario> SaveUsuario(Usuario obj);

    }
}
