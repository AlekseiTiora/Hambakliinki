using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hambakliinki.Models;
using Microsoft.AspNetCore.Authorization;

namespace Hambakliinki.Controllers
{
    public class hambakliiniksController : Controller
    {
        private readonly db _context;

        public hambakliiniksController(db context)
        {
            _context = context;
        }
        [Authorize]

        // GET: hambakliiniks
        public async Task<IActionResult> Index()
        {
            var db = _context.hambakliinik.Include(h => h.hambaarst).Include(h => h.klient).Include(h => h.teenuseid);
            return View(await db.ToListAsync());
        }

        // GET: hambakliiniks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.hambakliinik == null)
            {
                return NotFound();
            }

            var hambakliinik = await _context.hambakliinik
                .Include(h => h.hambaarst)
                .Include(h => h.klient)
                .Include(h => h.teenuseid)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hambakliinik == null)
            {
                return NotFound();
            }

            return View(hambakliinik);
        }

        // GET: hambakliiniks/Create
        public IActionResult Create()
        {
            ViewData["hambaarstId"] = new SelectList(_context.hambaarst, "hambaarstId", "perekonnanimi");
            ViewData["KlientId"] = new SelectList(_context.klient, "Id", "perekonnanimi");
            ViewData["teenuseidId"] = new SelectList(_context.teenuseid, "teenuseidId", "teenuse");
            return View();
        }

        // POST: hambakliiniks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KlientId,kuupaev,hambaarstId,teenuseidId")] hambakliinik hambakliinik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hambakliinik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["hambaarstId"] = new SelectList(_context.hambaarst, "hambaarstId", "perekonnanimi", hambakliinik.hambaarstId);
            ViewData["KlientId"] = new SelectList(_context.klient, "Id", "perekonnanimi", hambakliinik.KlientId);
            ViewData["teenuseidId"] = new SelectList(_context.teenuseid, "teenuseidId", "teenuse", hambakliinik.teenuseidId);
            return View(hambakliinik);
        }

        // GET: hambakliiniks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.hambakliinik == null)
            {
                return NotFound();
            }

            var hambakliinik = await _context.hambakliinik.FindAsync(id);
            if (hambakliinik == null)
            {
                return NotFound();
            }
            ViewData["hambaarstId"] = new SelectList(_context.hambaarst, "hambaarstId", "perekonnanimi", hambakliinik.hambaarstId);
            ViewData["KlientId"] = new SelectList(_context.klient, "Id", "perekonnanimi", hambakliinik.KlientId);
            ViewData["teenuseidId"] = new SelectList(_context.teenuseid, "teenuseidId", "teenuse", hambakliinik.teenuseidId);
            return View(hambakliinik);
        }

        // POST: hambakliiniks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KlientId,kuupaev,hambaarstId,teenuseidId")] hambakliinik hambakliinik)
        {
            if (id != hambakliinik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hambakliinik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!hambakliinikExists(hambakliinik.Id))
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
            ViewData["hambaarstId"] = new SelectList(_context.hambaarst, "hambaarstId", "perekonnanimi", hambakliinik.hambaarstId);
            ViewData["KlientId"] = new SelectList(_context.klient, "Id", "perekonnanimi", hambakliinik.KlientId);
            ViewData["teenuseidId"] = new SelectList(_context.teenuseid, "teenuseidId", "teenuse", hambakliinik.teenuseidId);
            return View(hambakliinik);
        }

        // GET: hambakliiniks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.hambakliinik == null)
            {
                return NotFound();
            }

            var hambakliinik = await _context.hambakliinik
                .Include(h => h.hambaarst)
                .Include(h => h.klient)
                .Include(h => h.teenuseid)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hambakliinik == null)
            {
                return NotFound();
            }

            return View(hambakliinik);
        }

        // POST: hambakliiniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.hambakliinik == null)
            {
                return Problem("Entity set 'db.hambakliinik'  is null.");
            }
            var hambakliinik = await _context.hambakliinik.FindAsync(id);
            if (hambakliinik != null)
            {
                _context.hambakliinik.Remove(hambakliinik);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool hambakliinikExists(int id)
        {
          return _context.hambakliinik.Any(e => e.Id == id);
        }
    }
}
