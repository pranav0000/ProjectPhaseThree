using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Phase_three_project_final.Models;

namespace Phase_three_project_final.Controllers
{
    public class TblLaptopsController : Controller
    {
        private readonly Project_3DBContext _context;

        public TblLaptopsController(Project_3DBContext context)
        {
            _context = context;
        }

        // GET: TblLaptops
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblLaptops.ToListAsync());
        }

        // GET: TblLaptops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblLaptops = await _context.TblLaptops
                .FirstOrDefaultAsync(m => m.LaptopId == id);
            if (tblLaptops == null)
            {
                return NotFound();
            }

            return View(tblLaptops);
        }

        // GET: TblLaptops/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblLaptops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LaptopId,LaptopName,LaptopImage,LaptopPrice")] TblLaptops tblLaptops)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblLaptops);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblLaptops);
        }

        // GET: TblLaptops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblLaptops = await _context.TblLaptops.FindAsync(id);
            if (tblLaptops == null)
            {
                return NotFound();
            }
            return View(tblLaptops);
        }

        // POST: TblLaptops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LaptopId,LaptopName,LaptopImage,LaptopPrice")] TblLaptops tblLaptops)
        {
            if (id != tblLaptops.LaptopId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblLaptops);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblLaptopsExists(tblLaptops.LaptopId))
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
            return View(tblLaptops);
        }

        // GET: TblLaptops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblLaptops = await _context.TblLaptops
                .FirstOrDefaultAsync(m => m.LaptopId == id);
            if (tblLaptops == null)
            {
                return NotFound();
            }

            return View(tblLaptops);
        }

        // POST: TblLaptops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblLaptops = await _context.TblLaptops.FindAsync(id);
            _context.TblLaptops.Remove(tblLaptops);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblLaptopsExists(int id)
        {
            return _context.TblLaptops.Any(e => e.LaptopId == id);
        }
    }
}
