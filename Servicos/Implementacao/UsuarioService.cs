using Microsoft.EntityFrameworkCore;
using Otimizador_PC_Web.Models;
using Otimizador_PC_Web.Servicos.Contrato;

namespace Otimizador_PC_Web.Servicos.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DbpruebaContext _dbContext;
        public UsuarioService(DbpruebaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> GetUsuario(string nome, string senha, string tipo)
        {
            Usuario usuario_encontrado = await _dbContext.Usuarios.Where(u => u.Nome == nome && u.Senha == senha)
                 .FirstOrDefaultAsync();

            return usuario_encontrado;
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _dbContext.Usuarios.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }
    }
}
