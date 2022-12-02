using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hambakliinki.Models;
using Microsoft.AspNetCore.Authorization;
using EASendMail;

namespace Hambakliinki.Controllers
{
    public class klientsController : Controller
    {
        private readonly db _context;

        public klientsController(db context)
        {
            _context = context;
        }
        [Authorize(Policy = "writepolicy")]
        // GET: klients
        public async Task<IActionResult> Index()
        {
            var db = _context.klient.Include(k => k.hambaarst).Include(k => k.teenuseid);
            return View(await db.ToListAsync());
        }

        // GET: klients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.klient == null)
            {
                return NotFound();
            }

            var klient = await _context.klient
                .Include(k => k.hambaarst)
                .Include(k => k.teenuseid)
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
            ViewData["hambaarstId"] = new SelectList(_context.hambaarst, "hambaarstId", "perekonnanimi");
            ViewData["teenuseidId"] = new SelectList(_context.teenuseid, "teenuseidId", "teenuse");
            return View();
        }

        // POST: klients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nimi,perekonnanimi,Email,Phone,Data,teenuseidId,hambaarstId")] klient klient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klient);
                await _context.SaveChangesAsync();
                Mail mail = new Mail();
                mail.SendEmailDefault(klient.Data.ToString($"f"), User.Identity?.Name.ToString());
                //Email(klient);
                return RedirectToAction("Broneeri","Home");
            }
            ViewData["hambaarstId"] = new SelectList(_context.hambaarst, "hambaarstId", "perekonnanimi", klient.hambaarstId);
            ViewData["teenuseidId"] = new SelectList(_context.teenuseid, "teenuseidId", "teenuse", klient.teenuseidId);
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
            ViewData["hambaarstId"] = new SelectList(_context.hambaarst, "hambaarstId", "perekonnanimi", klient.hambaarstId);
            ViewData["teenuseidId"] = new SelectList(_context.teenuseid, "teenuseidId", "teenuse", klient.teenuseidId);
            return View(klient);
        }

        // POST: klients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nimi,perekonnanimi,Email,Phone,Data,teenuseidId,hambaarstId")] klient klient)
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
            ViewData["hambaarstId"] = new SelectList(_context.hambaarst, "hambaarstId", "perekonnanimi", klient.hambaarstId);
            ViewData["teenuseidId"] = new SelectList(_context.teenuseid, "teenuseidId", "teenuse", klient.teenuseidId);
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
                .Include(k => k.hambaarst)
                .Include(k => k.teenuseid)
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
