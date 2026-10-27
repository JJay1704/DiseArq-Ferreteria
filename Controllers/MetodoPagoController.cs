using Ferreteria.Data;
using Ferreteria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ferreteria.Controllers
{
    public class MetodoPagoController : Controller
    {
        private readonly AppData _appData;
        public MetodoPagoController(AppData appData)
        {
            _appData = appData;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var metodos = await _appData.MetodosPago.AsNoTracking().OrderBy(m => m.Nombre).ToListAsync();
            return View(metodos);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(MetodoPago metodoPago)
        {
            if (!ModelState.IsValid)
            {
                return View(metodoPago);
            }
            _appData.MetodosPago.Add(metodoPago);
            await _appData.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var metodoPago = await _appData.MetodosPago.FindAsync(id.Value);
            if (metodoPago == null)
            {
                return NotFound();
            }
            return View(metodoPago);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, [Bind("IdMetPago,Tipo,Nombre,Descripcion,Comision")] MetodoPago metodoPago)
        {
            if (id != metodoPago.IdMetPago)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(metodoPago);
            }
            var existe = await _appData.MetodosPago.AnyAsync(m => m.IdMetPago == id);
            if (!existe)
            {
                return NotFound();
            }
            _appData.MetodosPago.Update(metodoPago);
            await _appData.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var metodoPago = await _appData.MetodosPago.AsNoTracking().FirstOrDefaultAsync(m => m.IdMetPago == id);
            if (metodoPago == null)
            {
                return NotFound();
            }
            return View(metodoPago);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            var metodoPago = await _appData.MetodosPago.FindAsync(id);
            if (metodoPago == null)
            {
                return NotFound();
            }
            _appData.MetodosPago.Remove(metodoPago);
            await _appData.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
