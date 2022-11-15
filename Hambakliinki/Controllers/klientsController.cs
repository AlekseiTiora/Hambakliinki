using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hambakliinki.Models;

namespace Hambakliinki.Controllers
{
    public class klientsController : Controller
    {
        private readonly db _context;

        public klientsController(db context)
        {
            _context = context;
        }

        // GET: klients
        public async Task<IActionResult> Index()
        {
              return View(await _context.klient.ToListAsync());
        }

        // GET: klients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.klient == null)
            {
                return NotFound();
            }

            var klient = await _context.klient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (klient == null)
            {
                return NotFound();
            }

            return View(klient);
        }

        // GET: klients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: klients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nimi,perekonnanimi,Phone,vanus")] klient klient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(klient);
        }

        // GET: klients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.klient == null)
            {
                return NotFound();
            }

            var klient = await _context.klient.FindAsync(id);
            if (klient == null)
            {
                return NotFound();
            }
            return View(klient);
        }

        // POST: klients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nimi,perekonnanimi,Phone,vanus")] klient klient)
        {
            if (id != klient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!klientExists(klient.Id))
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
            return View(klient);
        }

        // GET: klients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.klient == null)
            {
                return NotFound();
            }

            var klient = await _context.klient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (klient == null)
            {
                return NotFound();
            }

            return View(klient);
        }

        // POST: klients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.klient == null)
            {
                return Problem("Entity set 'db.klient'  is null.");
            }
            var klient = await _context.klient.FindAsync(id);
            if (klient != null)
            {
                _context.klient.Remove(klient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool klientExists(int id)
        {
          return _context.klient.Any(e => e.Id == id);
        }
    }
}
