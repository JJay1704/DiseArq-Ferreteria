using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ferreteria.Data;
using Ferreteria.Models;
using Ferreteria.States;
using StateMachine.Core;

namespace Ferreteria.Controllers
{
    public class VentaController : Controller
    {
        private readonly AppData _context;
        private readonly StateMachine<EstadoVenta> _stateMachine = new VentaStateMachineConfig().Build();

        public VentaController(AppData context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appData = _context.Ventas.Include(v => v.Cliente).Include(v => v.Empleado).Include(v => v.MetodoPago);
            return View(await appData.ToListAsync());
        }

        public async Task<IActionResult> Detalles(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Empleado)
                .Include(v => v.MetodoPago)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        public IActionResult Crear()
        {
            var venta = new Venta { Estado = (int)EstadoVenta.Pendiente };
            PopulateLookups(venta, incluirTransiciones: false);
            return View(venta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("IdVenta,Fecha,IdCliente,IdEmpleado,IdMetodoPago,Total,Estado")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                venta.Estado = (int)EstadoVenta.Pendiente;
                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateLookups(venta, incluirTransiciones: false);
            return View(venta);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            PopulateLookups(venta);
            return View(venta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("IdVenta,Fecha,IdCliente,IdEmpleado,IdMetodoPago,Total,Estado")] Venta venta)
        {
            if (id != venta.IdVenta)
            {
                return NotFound();
            }

            var ventaGuardada = await _context.Ventas.FindAsync(id);
            if (ventaGuardada == null)
            {
                return NotFound();
            }

            var estadoActual = (EstadoVenta)ventaGuardada.Estado;
            var estadoSolicitadoValido = Enum.IsDefined(typeof(EstadoVenta), venta.Estado);
            if (!estadoSolicitadoValido || !_stateMachine.CanTransition(estadoActual, (EstadoVenta)venta.Estado))
            {
                ModelState.AddModelError(nameof(venta.Estado), "La transición al estado seleccionado no está permitida.");
            }

            if (ModelState.IsValid)
            {
                ventaGuardada.Fecha = venta.Fecha;
                ventaGuardada.IdCliente = venta.IdCliente;
                ventaGuardada.IdEmpleado = venta.IdEmpleado;
                ventaGuardada.IdMetodoPago = venta.IdMetodoPago;
                ventaGuardada.Total = venta.Total;
                ventaGuardada.Estado = venta.Estado;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            venta.Estado = Enum.IsDefined(typeof(EstadoVenta), ventaGuardada.Estado)
                ? (int)estadoActual
                : ventaGuardada.Estado;
            PopulateLookups(venta);
            return View(venta);
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Empleado)
                .Include(v => v.MetodoPago)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateLookups(Venta venta, bool incluirTransiciones = true)
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombres", venta.IdCliente);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "Nombres", venta.IdEmpleado);
            ViewData["IdMetodoPago"] = new SelectList(_context.MetodosPago, "IdMetPago", "Nombre", venta.IdMetodoPago);

            var estadoActual = (EstadoVenta)venta.Estado;
            var estados = !incluirTransiciones
                ? new[] { EstadoVenta.Pendiente }
                : Enum.IsDefined(typeof(EstadoVenta), venta.Estado)
                    ? _stateMachine.AvailableStates(estadoActual)
                    : Enum.GetValues<EstadoVenta>();
            ViewData["Estados"] = new SelectList(
                estados.Select(estado => new { Value = (int)estado, Text = estado.ToString() }),
                "Value",
                "Text",
                venta.Estado);
        }
    }
}
