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
            //DocScheduleSuggestion();
            //get professional based on ProfessionalID
            //var schedule = _context.ServiceSchedules.Where(t => t.ServiceID == serviceID).FirstOrDefault();
            var professionals = _context.Professionals.ToList();
            var clinics = _context.Clinics.ToList();
            var serviceSchedulesInNeed = _context.ServiceSchedules.Where(t => t.ActualResources < t.EfficientResources).ToList();
            var serviceSchedulesOverProvision = _context.ServiceSchedules.Where(t => t.ActualResources > t.EfficientResources).ToList();
            var currentClinicID = 0;

            var docArrayForEmptyClinics = new List<Tuple<string,int, int>>(){ };

            foreach (var clinic in clinics)
            {
                currentClinicID = clinic.ClinicID;
                //get clinic services ofr clinic id
                var clinicServices = _context.ServiceSchedules.Where(t => t.ClinicID == currentClinicID).ToList();
                //get docs
                var clinicDoctors = _context.Professionals.Where(t => t.ClinicID == currentClinicID).ToList();

                var clinicDocCount = clinicDoctors.Count();

                //updating taktime and resource scores
                foreach (var item in clinicServices)
                {
                    var takt = TaktTimeEngine(item.ServiceID);
                    ResourcesEngine(item.ServiceID, takt);

                }

                    //check for deficiency in all services for the clinic
                    foreach (var item in clinicServices)
                {
                    var serviceName = item.ServiceName;
                    var serviceClinicID = item.ClinicID;
                    var docCount = clinicDoctors.Where(t => t.ClinicID == item.ClinicID && t.ProfessionalSpecialty == serviceName).Count();

                    //if (docCount > 0 && docCount > item.EfficientResources) -> add to tuple
                   
                    if (docCount == 0)
                    {
                        //find doc from an overprovisioned clinic
                        //foreach(var s in serviceSchedulesOverProvision)
                        //{
                        //serviceSchedulesOverProvision = _context.ServiceSchedules.Where(t => t.ActualResources > t.EfficientResources).ToList();

                        //}
                        //find docs from overprovisioned with same specialty and low exp
                        var movableDoctors = doctorExpEngine(item);
                        //var random = new Random();
                        //int index = random.Next(movableDoctors.Count);

                        if (movableDoctors.Count != 0)
                        {
                            var randomDocSelection = movableDoctors.FirstOrDefault();
                            docArrayForEmptyClinics.Add(new Tuple<string, int, int>(serviceName, serviceClinicID, randomDocSelection.ProfessionalID));
                        }
                        
                        docArrayForEmptyClinics.Add(new Tuple<string, int, int>(serviceName, serviceClinicID, 0));
                    }
                }
                Console.WriteLine(docArrayForEmptyClinics.ToString());
            }
            //find serviceschedules with actual more than the efficient resources needed -> serviceSchedulesOverProvision
            //check docs for that clinic that fit the service -> 
            //pair the docs with empty clinics and return to view

            //var serviceSchedulesOverProvision = _context.ServiceSchedules.Where(t => t.ActualResources > t.EfficientResources).ToList();
            Console.WriteLine(docArrayForEmptyClinics);
            //----------- start here ------------------------
            //foreach (var item in docArrayForEmptyClinics)
            //{
            //    ViewBag.ClinicsWithNoDoctor = ClinicsWithNoDoctor.Concat(item.Item1); //item.Item1 + " " + item.Item2;
            //}


             ViewBag.ClinicsWithNoDoctor = docArrayForEmptyClinics;
            //return View(professional);
            //return RedirectToAction(nameof(Index));
            //return Task.FromResult(RedirectToAction(nameof(Index)));
            //check their clinicID
            //check # specialists for clinic, check efficient resources
            //if clinic prof # is over efficient #
            //check the professional exp for likelihood of leaving (<5 yrs )
            //get best candidates and pair with underserved clinics
            //return list of suggestions (at least 5)

            //---

            //find serviceschedules with more than the efficient resources needed
            //check docs for that clinic that fit the service
            //pair the docs with empty clinics and return to view


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

        private async Task<IActionResult> Test()
        {
            
            return View();
            //return RedirectToAction(nameof(Index));
            //return Task.FromResult(RedirectToAction(nameof(Index)));
            //check their clinicID
            //check # specialists for clinic, check efficient resources
            //if clinic prof # is over efficient #
            //check the professional exp for likelihood of leaving (<5 yrs )
            //get best candidates and pair with underserved clinics
            //return list of suggestions (at least 5)
        }

        //[HttpPost]
        //private async Task<IActionResult> ProfessionalSchedulingEngine(Professional professional)
        //{
        //    //get professional based on ProfessionalID
        //    //var schedule = _context.ServiceSchedules.Where(t => t.ServiceID == serviceID).FirstOrDefault();
        //    var professionals = _context.Professionals.ToList();
        //    var clinics = _context.Clinics.ToList();
        //    var serviceSchedulesInNeed = _context.ServiceSchedules.Where(t => t.ActualResources < t.EfficientResources).ToList();
        //    var serviceSchedulesOverProvision = _context.ServiceSchedules.Where(t => t.ActualResources > t.EfficientResources).ToList();
        //    var currentClinicID = 0;

        //    int[] docArrayForEmptyClinics = new int[] { };

        //    foreach (var clinic in clinics)
        //    {
        //        currentClinicID = clinic.ClinicID;
        //        //get clinic services ofr clinic id
        //        var clinicServices = _context.ServiceSchedules.Where(t => t.ClinicID == currentClinicID).ToList();
        //        //get docs
        //        var clinicDoctors = _context.Professionals.Where(t => t.ClinicID == currentClinicID).ToList();

        //        var clinicDocCount = clinicDoctors.Count();



        //        //check for deficiency in all services for the clinic
        //        foreach (var item in clinicServices)
        //        {
        //            var serviceName = item.ServiceName;
        //            var docCount = clinicDoctors.Where(t => t.ClinicID == item.ClinicID && t.ProfessionalSpecialty == serviceName).Count();
        //            if (docCount == 0)
        //            {
        //                docArrayForEmptyClinics.Append(item.ClinicID);
        //            }
        //        }
        //        Console.WriteLine(docArrayForEmptyClinics);
        //        //find serviceschedules with more than the efficient resources needed
        //        //check docs for that clinic that fit the service
        //        //pair the docs with empty clinics and return to view
        //    }

        //    Console.WriteLine(docArrayForEmptyClinics);
        //    return View(professional);
        //    //return RedirectToAction(nameof(Index));
        //    //return Task.FromResult(RedirectToAction(nameof(Index)));
        //    //check their clinicID
        //    //check # specialists for clinic, check efficient resources
        //    //if clinic prof # is over efficient #
        //    //check the professional exp for likelihood of leaving (<5 yrs )
        //    //get best candidates and pair with underserved clinics
        //    //return list of suggestions (at least 5)
        //}

        //helpers
        private int TaktTimeEngine(int serviceID)
        {
            int availableTime = 0;
            int patientsToBeServiced = 0;

            var clinicService = _context.ServiceSchedule.Find(serviceID);

            availableTime = clinicService.MaxTimeAvailable;
            patientsToBeServiced = clinicService.MaxAppointments;


            int TaktTime = 0;

            if (patientsToBeServiced == 0)
            {
                TaktTime = availableTime;
            }

            else
            {
                TaktTime = availableTime / patientsToBeServiced;
            }

            clinicService.ActualTaktTime = TaktTime;
            _context.Update(clinicService);
            _context.SaveChanges();

            return TaktTime;
        }

        private int ResourcesEngine(int serviceID, int takTIme)
        {
            int serviceTime = 0;

            var clinicService = _context.ServiceSchedule.Find(serviceID);
            serviceTime = clinicService.ServiceTime;

            int r = serviceTime / takTIme;
            
            clinicService.EfficientResources = r;
            _context.Update(clinicService);
            _context.SaveChanges();

            return r;
        }

        private int docCounter(int clinicId)
        {
            var docCount = _context.Professionals.Where(t => t.ClinicID == clinicId).Count();
            return docCount;
        }
        private List<Professional> doctorExpEngine(ServiceSchedule item)
        {
            //var docs = _context.Professionals.Where(t => t.ClinicID != item.ClinicID && t.ProfessionalSpecialty == item.ServiceName && t.ProfessionalExpYears < 3 && docCounter(t.ClinicID) > item.EfficientResources).ToList();
            var docs = _context.Professionals.Where(t => t.ClinicID != item.ClinicID && t.ProfessionalSpecialty == item.ServiceName && t.ProfessionalExpYears < 5).ToList();
            //find doc from an overprovisioned clinic
            //if (docCount > 0 && docCount > item.EfficientResources) -> add to tuple
            return docs;
        }
    }
}
