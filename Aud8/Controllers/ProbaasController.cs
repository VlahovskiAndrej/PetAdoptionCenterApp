using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoptionCenter.Domain.Models;
using repository;

namespace PetAdoptionCenter.Web.Controllers
{
    public class ProbaasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProbaasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Probaas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Probaa.ToListAsync());
        }

        // GET: Probaas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probaa = await _context.Probaa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (probaa == null)
            {
                return NotFound();
            }

            return View(probaa);
        }

        // GET: Probaas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Probaas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("field,Id")] Probaa probaa)
        {
            if (ModelState.IsValid)
            {
                probaa.Id = Guid.NewGuid();
                _context.Add(probaa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(probaa);
        }

        // GET: Probaas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probaa = await _context.Probaa.FindAsync(id);
            if (probaa == null)
            {
                return NotFound();
            }
            return View(probaa);
        }

        // POST: Probaas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("field,Id")] Probaa probaa)
        {
            if (id != probaa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(probaa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProbaaExists(probaa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(probaa);
        }

        // GET: Probaas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probaa = await _context.Probaa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (probaa == null)
            {
                return NotFound();
            }

            return View(probaa);
        }

        // POST: Probaas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var probaa = await _context.Probaa.FindAsync(id);
            if (probaa != null)
            {
                _context.Probaa.Remove(probaa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProbaaExists(Guid id)
        {
            return _context.Probaa.Any(e => e.Id == id);
        }
    }
}
