using Ferreteria.Models;

namespace Ferreteria.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetUsuarioPorCorreoAsync(string correo);
    }
}