using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UNTELSLAB.Data;
using UNTELSLAB.Models;

namespace UNTELSLAB.Controllers
{
    public class LaboratoriosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LaboratoriosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var laboratorios = await _context.Laboratorios.ToListAsync();
            return View(laboratorios);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Laboratorio laboratorio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(laboratorio);
                    await _context.SaveChangesAsync();
                    return Ok(laboratorio);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error al guardar: " + ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var laboratorio = await _context.Laboratorios.FindAsync(id);
            if (laboratorio == null)
            {
                return NotFound();
            }
            return Ok(laboratorio);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Laboratorio laboratorio)
        {
            if (ModelState.IsValid)
            {
                _context.Update(laboratorio);
                await _context.SaveChangesAsync();
                return Ok(laboratorio);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var laboratorio = await _context.Laboratorios.FindAsync(id);
            if (laboratorio == null)
            {
                return NotFound();
            }

            _context.Laboratorios.Remove(laboratorio);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}