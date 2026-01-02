using Microsoft.AspNetCore.Mvc;
using ChallengeIntegra.Services;
using ChallengeIntegra.Models;
using System.Threading.Tasks;

namespace ChallengeIntegra.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: Cliente
        public async Task<IActionResult> Index(bool showInactive = false)
        {
            ViewData["ShowInactive"] = showInactive; // Pasamos el estado del filtro a la vista
            var clientes = await _clienteService.GetAllClientes(showInactive);
            return View(clientes);
        }

        // GET: Cliente/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var cliente = await _clienteService.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CUIT,RazonSocial,Telefono,Direccion")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                await _clienteService.CreateCliente(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _clienteService.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CUIT,RazonSocial,Telefono,Direccion,Activo")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _clienteService.UpdateCliente(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _clienteService.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clienteService.DeleteCliente(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _clienteService.GetClienteById(id) != null;
        }
    }
}

