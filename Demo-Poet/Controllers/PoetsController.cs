using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demo_Poet.Data;
using Demo_Poet.Models;

namespace Demo_Poet.Controllers
{
    public class PoetsController : Controller
    {
        private readonly Demo_PoetContext _context;

        public PoetsController(Demo_PoetContext context)
        {
            _context = context;
        }

        // GET: Poets
        public async Task<IActionResult> Index()
        {
              return _context.Poet != null ? 
                          View(await _context.Poet.ToListAsync()) :
                          Problem("Entity set 'Demo_PoetContext.Poet'  is null.");
        }

        // GET: Poets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Poet == null)
            {
                return NotFound();
            }

            var poet = await _context.Poet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poet == null)
            {
                return NotFound();
            }

            return View(poet);
        }

        // GET: Poets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Poets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PoetVerse1,PoetVerse2")] Poet poet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(poet);
        }

        // GET: Poets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Poet == null)
            {
                return NotFound();
            }

            var poet = await _context.Poet.FindAsync(id);
            if (poet == null)
            {
                return NotFound();
            }
            return View(poet);
        }

        // POST: Poets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PoetVerse1,PoetVerse2")] Poet poet)
        {
            if (id != poet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoetExists(poet.Id))
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
            return View(poet);
        }

        // GET: Poets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Poet == null)
            {
                return NotFound();
            }

            var poet = await _context.Poet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poet == null)
            {
                return NotFound();
            }

            return View(poet);
        }

        // POST: Poets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Poet == null)
            {
                return Problem("Entity set 'Demo_PoetContext.Poet'  is null.");
            }
            var poet = await _context.Poet.FindAsync(id);
            if (poet != null)
            {
                _context.Poet.Remove(poet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoetExists(int id)
        {
          return (_context.Poet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
