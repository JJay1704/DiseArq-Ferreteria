using Ferreteria.Data;
using Ferreteria.Models;
using Microsoft.EntityFrameworkCore;

namespace Ferreteria.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppData _appData;

        public UsuarioRepository(AppData appData)
        {
            _appData = appData;
        }

        public async Task<Usuario?> GetUsuarioPorCorreoAsync(string correo)
        {
            return await _appData.Usuarios
                .AsNoTracking()
                .Include(u => u.Rol)
                .Include(u => u.Empleado)
                .FirstOrDefaultAsync(u => u.Correo == correo);
        }
    }
}