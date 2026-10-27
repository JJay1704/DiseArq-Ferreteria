using Ferreteria.Data;
using Ferreteria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ferreteria.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly AppData _appData;
        public CategoriaController(AppData appData)
        {
            _appData = appData;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categorias = await _appData.Categorias.AsNoTracking().OrderBy(c => c.Nombre).ToListAsync();
            return View(categorias);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }
            _appData.Categorias.Add(categoria);
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
            var categoria = await _appData.Categorias.FindAsync(id.Value);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, [Bind("IdCat,Nombre,Descr")] Categoria categoria)
        {
            if (id != categoria.IdCat)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }
            var existe = await _appData.Categorias.AnyAsync(c => c.IdCat == id);
            if (!existe)
            {
                return NotFound();
            }
            _appData.Categorias.Update(categoria);
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
            var categoria = await _appData.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.IdCat == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            var categoria = await _appData.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            _appData.Categorias.Remove(categoria);
            await _appData.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
