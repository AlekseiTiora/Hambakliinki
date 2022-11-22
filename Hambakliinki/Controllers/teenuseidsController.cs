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
    public class teenuseidsController : Controller
    {
        private readonly db _context;

        public teenuseidsController(db context)
        {
            _context = context;
        }
        [Authorize(Policy = "writepolicy")]

        // GET: teenuseids
        public async Task<IActionResult> Index()
        {
              return View(await _context.teenuseid.ToListAsync());
        }

        // GET: teenuseids/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.teenuseid == null)
            {
                return NotFound();
            }

            var teenuseid = await _context.teenuseid
                .FirstOrDefaultAsync(m => m.teenuseidId == id);
            if (teenuseid == null)
            {
                return NotFound();
            }

            return View(teenuseid);
        }

        // GET: teenuseids/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: teenuseids/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("teenuseidId,teenuse,hind")] teenuseid teenuseid)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teenuseid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teenuseid);
        }

        // GET: teenuseids/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.teenuseid == null)
            {
                return NotFound();
            }

            var teenuseid = await _context.teenuseid.FindAsync(id);
            if (teenuseid == null)
            {
                return NotFound();
            }
            return View(teenuseid);
        }

        // POST: teenuseids/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("teenuseidId,teenuse,hind")] teenuseid teenuseid)
        {
            if (id != teenuseid.teenuseidId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teenuseid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!teenuseidExists(teenuseid.teenuseidId))
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
            return View(teenuseid);
        }

        // GET: teenuseids/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.teenuseid == null)
            {
                return NotFound();
            }

            var teenuseid = await _context.teenuseid
                .FirstOrDefaultAsync(m => m.teenuseidId == id);
            if (teenuseid == null)
            {
                return NotFound();
            }

            return View(teenuseid);
        }

        // POST: teenuseids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.teenuseid == null)
            {
                return Problem("Entity set 'db.teenuseid'  is null.");
            }
            var teenuseid = await _context.teenuseid.FindAsync(id);
            if (teenuseid != null)
            {
                _context.teenuseid.Remove(teenuseid);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool teenuseidExists(int id)
        {
          return _context.teenuseid.Any(e => e.teenuseidId == id);
        }
    }
}
