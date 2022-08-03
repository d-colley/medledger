using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedLedger.Data;
using MedLedger.Models;

namespace MedLedger.Controllers
{
    public class ServiceScheduleController : Controller
    {
        private readonly MedLedgerDBContext _context;

        public ServiceScheduleController(MedLedgerDBContext context)
        {
            _context = context;
        }

        // GET: ServiceSchedule
        public async Task<IActionResult> Index()
        {
            return View(await _context.ServiceSchedule.ToListAsync());
        }

        // GET: ServiceSchedule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceSchedule = await _context.ServiceSchedule
                .FirstOrDefaultAsync(m => m.ServiceID == id);
            if (serviceSchedule == null)
            {
                return NotFound();
            }

            return View(serviceSchedule);
        }

        // GET: ServiceSchedule/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServiceSchedule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceID,ServiceName,ServiceDays,ServiceStartTime,ServicEndTime")] ServiceSchedule serviceSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceSchedule);
        }

        // GET: ServiceSchedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceSchedule = await _context.ServiceSchedule.FindAsync(id);
            if (serviceSchedule == null)
            {
                return NotFound();
            }
            return View(serviceSchedule);
        }

        // POST: ServiceSchedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceID,ServiceName,ServiceDays,ServiceStartTime,ServicEndTime")] ServiceSchedule serviceSchedule)
        {
            if (id != serviceSchedule.ServiceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceScheduleExists(serviceSchedule.ServiceID))
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
            return View(serviceSchedule);
        }

        // GET: ServiceSchedule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceSchedule = await _context.ServiceSchedule
                .FirstOrDefaultAsync(m => m.ServiceID == id);
            if (serviceSchedule == null)
            {
                return NotFound();
            }

            return View(serviceSchedule);
        }

        // POST: ServiceSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceSchedule = await _context.ServiceSchedule.FindAsync(id);
            _context.ServiceSchedule.Remove(serviceSchedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceScheduleExists(int id)
        {
            return _context.ServiceSchedule.Any(e => e.ServiceID == id);
        }
    }
}
