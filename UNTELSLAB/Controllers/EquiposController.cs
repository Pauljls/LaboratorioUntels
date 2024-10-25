using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using UNTELSLAB.Data;
using UNTELSLAB.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace UNTELSLAB.Controllers
{
    public class EquiposController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EquiposController> _logger;
        private readonly IWebHostEnvironment _env;
        public EquiposController(ApplicationDbContext context, ILogger<EquiposController> logger, IWebHostEnvironment env)
        {
            _context = context;
            _logger = logger;
            _env = env;
        }

        // GET: Equipos
        public async Task<IActionResult> Index()
        {
            var equipos = await _context.EquipoLaboratorio
                .Include(e => e.Laboratorio)
                .ToListAsync();
            return View(equipos);
        }

        // GET: Equipos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Console.WriteLine($"Details method called with id: {id}");

            if (id == null)
            {
                Console.WriteLine("ID is null, returning NotFound()");
                return NotFound();
            }

            try
            {
                Console.WriteLine($"Attempting to fetch EquipoLaboratorio with id: {id}");
                var equipo = await _context.EquipoLaboratorio
                    .Include(e => e.Laboratorio)
                    .Include(e => e.DatosEquipo)
                    .Include(e => e.FichaTecnicaEquipo)
                    .Include(e => e.InformesMantenimiento)
                    .Include(e => e.InformesCalibracion)
                    .Include(e => e.HistoricoFallas)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (equipo == null)
                {
                    Console.WriteLine($"EquipoLaboratorio with id {id} not found");
                    return NotFound();
                }

                Console.WriteLine($"EquipoLaboratorio found: {equipo.Nombre}");
                return View(equipo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw; // Re-throw the exception to be caught by the global exception handler
            }
        }
        // GET: Equipos/Create
        public IActionResult Create()
        {
            ViewBag.Laboratorios = new SelectList(_context.Laboratorios, "Id", "Nombre");
            return View();
        }

        // POST: Equipos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,IdLaboratorio")] EquipoLaboratorio equipo)
        {
            if (ModelState.IsValid)
            {
                // Verificar si ya existe un equipo con el mismo nombre
                var equipoExistente = await _context.EquipoLaboratorio
                    .FirstOrDefaultAsync(e => e.Nombre.ToLower() == equipo.Nombre.ToLower());

                if (equipoExistente != null)
                {
                    ModelState.AddModelError("Nombre", "Ya existe un equipo con este nombre");
                    ViewBag.Laboratorios = new SelectList(_context.Laboratorios, "Id", "Nombre", equipo.IdLaboratorio);
                    return View(equipo);
                }

                _context.Add(equipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(EquiposLaboratorio));
            }
            ViewBag.Laboratorios = new SelectList(_context.Laboratorios, "Id", "Nombre", equipo.IdLaboratorio);
            return View(equipo);
        }

        [HttpGet]
        public async Task<IActionResult> VerificarNombreUnico(string nombre)
        {
            var equipoExiste = await _context.EquipoLaboratorio
                .AnyAsync(e => e.Nombre.ToLower() == nombre.ToLower());

            return Json(!equipoExiste);
        }

        // GET: Equipos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.EquipoLaboratorio.FindAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }
            ViewBag.Laboratorios = new SelectList(_context.Laboratorios, "Id", "Nombre", equipo.IdLaboratorio);
            return View(equipo);
        }

        // POST: Equipos/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Nombre,IdLaboratorio")] EquipoLaboratorio equipo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var equipoToUpdate = await _context.EquipoLaboratorio.FindAsync(equipo.Id);
                    if (equipoToUpdate == null)
                    {
                        return Json(new { success = false, message = "Equipo no encontrado." });
                    }

                    equipoToUpdate.Nombre = equipo.Nombre;
                    equipoToUpdate.IdLaboratorio = equipo.IdLaboratorio;

                    _context.Update(equipoToUpdate);
                    await _context.SaveChangesAsync();

                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al editar equipo");
                    return Json(new { success = false, message = "Ocurrió un error al guardar los cambios." });
                }
            }
            return Json(new { success = false, message = "Datos inválidos." });
        }

        // GET: Equipos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.EquipoLaboratorio
                .Include(e => e.Laboratorio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // POST: Equipos/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var equipo = await _context.EquipoLaboratorio.FindAsync(id);
                if (equipo == null)
                {
                    return NotFound();
                }

                var equipoFolder = Path.Combine(_env.WebRootPath, "Uploads", equipo.Nombre);
                _context.EquipoLaboratorio.Remove(equipo);
                await _context.SaveChangesAsync();

                if (Directory.Exists(equipoFolder)) { 
                    Directory.Delete(equipoFolder, true);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el equipo con ID {Id}", id);
                return StatusCode(500, "Ocurrió un error al eliminar el equipo.");
            }
        }

        // GET: Equipos/EquiposLaboratorio
        public async Task<IActionResult> EquiposLaboratorio()
        {
            var equipos = await _context.EquipoLaboratorio
                .Include(e => e.Laboratorio)
                .ToListAsync();
            ViewBag.Laboratorios = new SelectList(await _context.Laboratorios.ToListAsync(), "Id", "Nombre");
            return View(equipos);
        }

        [HttpPost]
        public IActionResult GuardarFichaTecnica([FromBody] FichaTecnicaEquipo fichaTecnica)
        {
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values.SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();
                return Json(new { success = false, message = "Datos inválidos", errors = errores });
            }

            try
            {
                var fichaTecnicaExistente = _context.FichaTecnicaEquipo
                    .FirstOrDefault(f => f.IdEquipo == fichaTecnica.IdEquipo);

                if (fichaTecnicaExistente != null)
                {
                    _context.Entry(fichaTecnicaExistente).CurrentValues.SetValues(fichaTecnica);
                }
                else
                {
                    _context.FichaTecnicaEquipo.Add(fichaTecnica);
                }

                _context.SaveChanges();

                return Json(new { success = true, message = "Ficha técnica guardada correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult ActualizarDatosEquipo([FromBody] DatosEquipo datos)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Datos inválidos" });
            }

            try
            {
                var equipo = _context.EquipoLaboratorio
                    .Include(e => e.DatosEquipo)
                    .FirstOrDefault(e => e.Id == datos.Id);

                if (equipo == null)
                {
                    return Json(new { success = false, message = "Equipo no encontrado" });
                }

                if (equipo.DatosEquipo == null)
                {
                    equipo.DatosEquipo = new DatosEquipo();
                }

                equipo.DatosEquipo.Procesador = datos.Procesador;
                equipo.DatosEquipo.SistemaOperativo = datos.SistemaOperativo;
                equipo.DatosEquipo.Memoria = datos.Memoria;
                equipo.DatosEquipo.AnoFabricacion = datos.AnoFabricacion;

                _context.SaveChanges();

                return Json(new { success = true, message = "Datos actualizados correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AgregarDatosEquipo([FromForm] DatosEquipoDTO datos)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Datos inválidos" });
            }

            if (datos.Manual != null)
            {
                try
                {
                    var equipoLaboratorio = _context.EquipoLaboratorio.FirstOrDefault(
                        e => e.Id == datos.idEquipo);
                    if (equipoLaboratorio == null)
                    {
                        throw new Exception("No se encontro el equipo especificado");
                    }
                    //Guardar la foto en la carpeta uploads
                    var uploadsRootFolder = Path.Combine(_env.WebRootPath, "Uploads");
                    var equipoFolder = Path.Combine(uploadsRootFolder, equipoLaboratorio.Nombre);
                    Directory.CreateDirectory(equipoFolder);
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(datos.Manual.FileName);
                    var filePath = Path.Combine(equipoFolder, fileName);

                    using (FileStream stream = new(filePath, FileMode.Create))
                    {
                        await datos.Manual.CopyToAsync(stream);
                    }

                    DatosEquipo datosEquipo = new()
                    {
                        Procesador = datos.Procesador,
                        SistemaOperativo = datos.SistemaOperativo,
                        Memoria = datos.Memoria,
                        Manual = fileName,
                        AnoFabricacion = datos.AnoFabricacion,
                        IdEquipo = datos.idEquipo
                    };

                    _context.DatosEquipo.Add(datosEquipo);
                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Datos del equipo agregados correctamente" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
            else {
                try
                {
                    var equipoLaboratorio = _context.EquipoLaboratorio.FirstOrDefault(
                        e => e.Id == datos.idEquipo);
                    if (equipoLaboratorio == null)
                    {
                        throw new Exception("No se encontro el equipo especificado");
                    }
                    DatosEquipo datosEquipo = new()
                    {
                        Procesador = datos.Procesador,
                        SistemaOperativo = datos.SistemaOperativo,
                        Memoria = datos.Memoria,
                        AnoFabricacion = datos.AnoFabricacion,
                        IdEquipo = datos.idEquipo
                    };

                    _context.DatosEquipo.Add(datosEquipo);
                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Datos del equipo agregados correctamente" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }

        [HttpPost]
        public IActionResult ActualizarFichaTecnica([FromBody] FichaTecnicaEquipo fichaTecnica)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Datos inválidos" });
            }

            try
            {
                var fichaTecnicaExistente = _context.FichaTecnicaEquipo
                    .FirstOrDefault(f => f.Id == fichaTecnica.Id);

                if (fichaTecnicaExistente == null)
                {
                    return Json(new { success = false, message = "Ficha técnica no encontrada" });
                }

                _context.Entry(fichaTecnicaExistente).CurrentValues.SetValues(fichaTecnica);
                _context.SaveChanges();

                return Json(new { success = true, message = "Ficha técnica actualizada correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        //GUARDAR INFOMRE MANTENIMIENTO
        [HttpPost]
        public async Task<IActionResult> GuardarInformeMantenimiento([FromBody] InformeMantenimientoEquipo informeMantenimientoEquipo)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Datos inválidos" });
            }
            try
            {
                await _context.InformeMantenimientoEquipo.AddAsync(informeMantenimientoEquipo);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Informe de mantenimiento agregado correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        //GUARDAR INFORMACION CALIBRABRACION
        [HttpPost]
        public async Task<IActionResult> GuardarHistoricoFallaEquipo([FromBody] HistoricoFallasEquipo historicoFallasEquipo)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Datos inválidos" });
            }
            try
            {
                await _context.HistoricoFallasEquipo.AddAsync(historicoFallasEquipo);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Informe de fallas agregado correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        //ACTUALIZAR FICHA TECNICA
        [HttpPatch]
        public async Task<IActionResult> ActualizarInformeMantenimienton([FromBody] InformeMantenimientoEquipo informeMantenimientoEquipo)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Datos inválidos" });
            }

            try
            {
                var informeMantenimientoExistente = await _context.InformeMantenimientoEquipo
                    .FirstOrDefaultAsync(f => f.Id == informeMantenimientoEquipo.Id);

                if (informeMantenimientoExistente == null)
                {
                    return Json(new { success = false, message = "Ficha técnica no encontrada" });
                }

                _context.Entry(informeMantenimientoExistente).CurrentValues.SetValues(informeMantenimientoEquipo);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Ficha técnica actualizada correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        //ACTUALZIAR INFORME CALIBRACION 
        [HttpPatch]
        public async Task<IActionResult> ActualizarInformeFalla([FromBody] HistoricoFallasEquipo historicoFallasEquipo) {
            if (!ModelState.IsValid) {
                return Json(new { success = false, message = "Datos inválidos" });
            }
            try
            {
                var historicoFallaequipoActual = await _context.HistoricoFallasEquipo
                    .FirstOrDefaultAsync(i => i.Id == historicoFallasEquipo.Id);
                if (historicoFallaequipoActual == null) {
                    return Json(new { succes = false, message = "Informe calibracion no exisente" });
                }

                _context.Entry(historicoFallaequipoActual).CurrentValues.SetValues(historicoFallasEquipo);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Ficha técnica actualizada correctamente" });
            }
            catch (Exception ex) {
                return Json(new { success = false, message = ex.Message });
            }
        }



    }
}