using Ferreteria.Models;
using Ferreteria.Repositories;

namespace Ferreteria.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly PasswordService _passwordService;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            PasswordService passwordService)
        {
            _usuarioRepository = usuarioRepository;
            _passwordService = passwordService;
        }

        public async Task<Usuario?> ValidarCredencialesAsync(
            string correo,
            string password)
        {
            if (string.IsNullOrWhiteSpace(correo) ||
                string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            var usuario = await _usuarioRepository
                .GetUsuarioPorCorreoAsync(correo.Trim());

            if (usuario == null || !usuario.Activo)
            {
                return null;
            }

            bool passwordValido = _passwordService.VerifyPassword(
                password,
                usuario.PasswordHash);

            if (!passwordValido)
            {
                return null;
            }

            return usuario;
        }
    }
}