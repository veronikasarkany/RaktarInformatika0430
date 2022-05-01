using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RaktarInformatika0430.Data;
using RaktarInformatika0430.Models;

namespace RaktarInformatika0430.Controllers
{
    public class RaktarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RaktarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Raktar
        public async Task<IActionResult> Index(string Megnevezes, string Beszallito)
        {
            var model = new AruKeres();
            var aruk = _context.Aru.Select(x => x);
            if(!string.IsNullOrEmpty(Megnevezes))
            {
                aruk = aruk.Where(x => x.Megnevezes.Contains(Megnevezes));
                model.Megnevezes = Megnevezes;
            }

            if (!string.IsNullOrEmpty(Beszallito))
            {
                aruk = aruk.Where(x => x.Beszallito==Beszallito);
                model.Beszallito = Beszallito;
            }

            model.AruLista = await aruk.OrderBy(x => x.Megnevezes).ToListAsync();
            model.BeszallitoLista = new SelectList(await _context.Aru.Select(x => x.Beszallito).Distinct().OrderBy(x => x).ToListAsync());

            return View(model);
        }

        // GET: Raktar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aru = await _context.Aru
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aru == null)
            {
                return NotFound();
            }

            return View(aru);
        }

        // GET: Raktar/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Raktar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Megnevezes,Beszallito,Ar")] Aru aru)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aru);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aru);
        }

        // GET: Raktar/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aru = await _context.Aru.FindAsync(id);
            if (aru == null)
            {
                return NotFound();
            }
            return View(aru);
        }

        // POST: Raktar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Megnevezes,Beszallito,Ar")] Aru aru)
        {
            if (id != aru.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aru);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AruExists(aru.Id))
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
            return View(aru);
        }

        // GET: Raktar/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aru = await _context.Aru
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aru == null)
            {
                return NotFound();
            }

            return View(aru);
        }

        // POST: Raktar/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aru = await _context.Aru.FindAsync(id);
            _context.Aru.Remove(aru);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AruExists(int id)
        {
            return _context.Aru.Any(e => e.Id == id);
        }
    }
}
