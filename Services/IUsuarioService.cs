using Ferreteria.Models;

namespace Ferreteria.Services
{
    public interface IUsuarioService
    {
        Task<Usuario?> ValidarCredencialesAsync(
            string correo,
            string password);
    }
}