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
    public class hambaarstsController : Controller
    {
        private readonly db _context;

        public hambaarstsController(db context)
        {
            _context = context;
        }
        [Authorize]

        // GET: hambaarsts
        public async Task<IActionResult> Index()
        {
              return View(await _context.hambaarst.ToListAsync());
        }

        // GET: hambaarsts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.hambaarst == null)
            {
                return NotFound();
            }

            var hambaarst = await _context.hambaarst
                .FirstOrDefaultAsync(m => m.hambaarstId == id);
            if (hambaarst == null)
            {
                return NotFound();
            }

            return View(hambaarst);
        }

        // GET: hambaarsts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: hambaarsts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("hambaarstId,nimi,perekonnanimi,spetsialiseerumine")] hambaarst hambaarst)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hambaarst);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hambaarst);
        }

        // GET: hambaarsts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.hambaarst == null)
            {
                return NotFound();
            }

            var hambaarst = await _context.hambaarst.FindAsync(id);
            if (hambaarst == null)
            {
                return NotFound();
            }
            return View(hambaarst);
        }

        // POST: hambaarsts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("hambaarstId,nimi,perekonnanimi,spetsialiseerumine")] hambaarst hambaarst)
        {
            if (id != hambaarst.hambaarstId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hambaarst);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!hambaarstExists(hambaarst.hambaarstId))
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
            return View(hambaarst);
        }

        // GET: hambaarsts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.hambaarst == null)
            {
                return NotFound();
            }

            var hambaarst = await _context.hambaarst
                .FirstOrDefaultAsync(m => m.hambaarstId == id);
            if (hambaarst == null)
            {
                return NotFound();
            }

            return View(hambaarst);
        }

        // POST: hambaarsts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.hambaarst == null)
            {
                return Problem("Entity set 'db.hambaarst'  is null.");
            }
            var hambaarst = await _context.hambaarst.FindAsync(id);
            if (hambaarst != null)
            {
                _context.hambaarst.Remove(hambaarst);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool hambaarstExists(int id)
        {
          return _context.hambaarst.Any(e => e.hambaarstId == id);
        }
    }
}
