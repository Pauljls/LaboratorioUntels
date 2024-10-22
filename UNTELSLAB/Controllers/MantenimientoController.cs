using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UNTELSLAB.Data;

namespace UNTELSLAB.Controllers
{
    public class MantenimientoController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<EquiposController> _logger;
        private readonly IWebHostEnvironment _env;
        public MantenimientoController(ApplicationDbContext context, ILogger<EquiposController> logger, IWebHostEnvironment env)
        {
            _context = context;
            _logger = logger;
            _env = env;
        }

        // GET: MantenimientoController
        public async Task<IActionResult> Index()
        {
            var equipos = await _context.EquipoLaboratorio
                .Include(e => e.Laboratorio)
                .Include(e => e.FichaTecnicaEquipo)
                .Include(e => e.DatosEquipo)
                .ToListAsync();
            return View(equipos);
        }
        // GET: MantenimientoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MantenimientoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MantenimientoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MantenimientoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MantenimientoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MantenimientoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MantenimientoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
