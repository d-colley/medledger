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
    public class ProfessionalsController : Controller
    {
        private readonly MedLedgerDBContext _context;

        public ProfessionalsController(MedLedgerDBContext context)
        {
            _context = context;
        }

        // GET: Professionals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Professionals.ToListAsync());
        }

        // GET: Professionals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professional = await _context.Professionals
                .FirstOrDefaultAsync(m => m.ProfessionalID == id);
            if (professional == null)
            {
                return NotFound();
            }

            return View(professional);
        }

        // GET: Professionals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professionals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfessionalID,ProfessionalName,ProfessionalEmail,ProfessionalSpecialty,ProfessionalExpYears,UserID,ClinicID,TeamID")] Professional professional)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(professional);
        }

        // GET: Professionals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professional = await _context.Professionals.FindAsync(id);
            if (professional == null)
            {
                return NotFound();
            }
            return View(professional);
        }

        // POST: Professionals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfessionalID,ProfessionalName,ProfessionalEmail,ProfessionalSpecialty,ProfessionalExpYears,UserID,ClinicID,TeamID")] Professional professional)
        {
            if (id != professional.ProfessionalID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professional);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessionalExists(professional.ProfessionalID))
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
            return View(professional);
        }

        // GET: Professionals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professional = await _context.Professionals
                .FirstOrDefaultAsync(m => m.ProfessionalID == id);
            if (professional == null)
            {
                return NotFound();
            }

            return View(professional);
        }

        // POST: Professionals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professional = await _context.Professionals.FindAsync(id);
            _context.Professionals.Remove(professional);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessionalExists(int id)
        {
            return _context.Professionals.Any(e => e.ProfessionalID == id);
        }

        private void ProfessionalSchedulingEngine()
        {
            //get professional based on ProfessionalID
            //var schedule = _context.ServiceSchedules.Where(t => t.ServiceID == serviceID).FirstOrDefault();
            var professionals = _context.Professionals.ToList();
            var clinics = _context.Clinics.ToList();
            var serviceSchedulesInNeed = _context.ServiceSchedules.Where(t=>t.ActualResources < t.EfficientResources).ToList();
            var serviceSchedulesOverProvision = _context.ServiceSchedules.Where(t => t.ActualResources > t.EfficientResources).ToList();
            var currentClinicID = 0;

            foreach(var clinic in clinics)
            {
                currentClinicID = clinic.ClinicID;
                //get clinic services ofr clinic id
                var clinicServices = _context.ServiceSchedules.Where(t => t.ClinicID == currentClinicID).ToList();
                //get docs
                var clinicDoctors = _context.Professionals.Where(t => t.ClinicID == currentClinicID).ToList();

                var clinicDocCount = clinicDoctors.Count();

                //check for deficiency in all services

            }
            //check their clinicID
            //check # specialists for clinic, check efficient resources
            //if clinic prof # is over efficient #
            //check the professional exp for likelihood of leaving (<5 yrs )
            //get best candidates and pair with underserved clinics
            //return list of suggestions (at least 5)
        }
    }
}
