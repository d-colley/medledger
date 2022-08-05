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
    public class AppointmentsController : Controller
    {
        private readonly MedLedgerDBContext _context;

        public AppointmentsController(MedLedgerDBContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Appointments.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentID,AppointmentDate,AppointmentService,AppointmentDescription,ProfessionalID,PatientID,ClinicID,ServiceID")] Appointment appointment)
        {
            //resource scheduling algortihm - https://journals.sagepub.com/doi/pdf/10.1177/1460458220905380
            //using Takt time to manage resources, from there we  can manage appointments
            //Low Takt time = low demand, high Takt = high demand

            //Takt time = eff avail. time in day/# of patients serviced in day
            //int availableTime = 0;
            //int patientsToBeServiced = 0;

            //int TaktTime = 0;

            //TaktTime = availableTime / patientsToBeServiced;

            int initialTaktTime = TaktTimeEngine(appointment.ServiceID);

            int initialEfficientResources = ResourcesEngine(appointment.ServiceID, initialTaktTime);

             

            int bestTaktTime = initialTaktTime;
            int bestTaktTimeClinicId = appointment.ServiceID;
            int actualResources = InventoryCounter(appointment.ClinicID, appointment.ServiceID);
            //if two clinics are the same Takt time, we use location

            //service time (standard service time per health region)(based on procedure)

            //r - units of resources required
            //r = service time/Takt time (approx to nearest whole number)

            //Run this function for the various health centres
            //for (int i=0; i< _context.Clinics.Count();i++)
            var schedule = _context.ServiceSchedules.Where(t=> t.ServiceName == appointment.AppointmentService);
            foreach(var item in _context.ServiceSchedules)
            {
                Console.WriteLine(item.ServiceID);
                if (bestTaktTime < TaktTimeEngine(item.ServiceID)) //new item has higher processing time
                {
                    bestTaktTimeClinicId = item.ClinicID;
                    bestTaktTime = TaktTimeEngine(item.ServiceID);
                }

                else if (bestTaktTime == TaktTimeEngine(item.ServiceID))
                {
                    //resource count check
                    int itemEfficientResources = ResourcesEngine(item.ServiceID, TaktTimeEngine(item.ServiceID));
                    int itemActualResources = InventoryCounter(item.ClinicID,item.ServiceID);
                    if (initialEfficientResources < actualResources && itemEfficientResources < itemActualResources)
                    {
                        Console.WriteLine("preferred location");
                        //possibly go with less
                    }
                    else if(initialEfficientResources == actualResources && itemEfficientResources == itemActualResources)
                    {
                        Console.WriteLine("--Use location here--");
                    }
                    else if (initialEfficientResources > actualResources && itemEfficientResources <= itemActualResources)
                    {
                        Console.WriteLine("--choose item--");
                    }
                    else if (initialEfficientResources <= actualResources && itemEfficientResources > itemActualResources)
                    {
                        Console.WriteLine("--choose initial--");
                    }
                }
            }
            //choose the one with the units of resources or higher with service needed(could throw in location) - ignore

            //**return best clinic along with best time to modal. if yes, replace appointmentschedule and clinic. if no, keep the appointment**
            

            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentID,AppointmentDate,AppointmentService,AppointmentDescription,ProfessionalID,PatientID,ClinicID,ServiceID")] Appointment appointment)
        {
            if (id != appointment.AppointmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentID))
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
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentID == id);
        }

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
            

            return TaktTime;
        }

        private int ResourcesEngine(int serviceID, int takTIme)
        {
            int serviceTime = 0;

            var clinicService = _context.ServiceSchedule.Find(serviceID);
            serviceTime = clinicService.ServiceTime;

            //in order to reach Takt time, we need r units of resources
            int r = serviceTime / takTIme;
            //check resources
            //int actualResources = InventoryCounter(clinicService.ClinicID,serviceID);
            
            //if resources <= r
            //if (actualResources >= r)
            //{
            //    Console.WriteLine("actual greater than r");
            //}
            //if(actualResources < r)
            //{
            //    Console.WriteLine("actual less than effecient resource");
            //}
            //recalculate for time based on resources
            //show actual time 

            //if resources >= r
            //use current time

            //store in DB(serviceschedule)

            
            return r;
        }

        private void InventoryEngine()
        {

        }

        private int InventoryCounter(int clinicID, int serviceID)
        {
            var schedule = _context.ServiceSchedules.Where(t => t.ServiceID == serviceID).FirstOrDefault();

            var resourceList = schedule.ResourceList;

            var totalResourceCount = 0;
            var totalResourceElements = 0;

            String[] resourceArray = resourceList.Split(",");
            foreach(var item in resourceArray)
            {
                totalResourceCount += _context.Inventories.Where(t => t.ClinicID == clinicID && t.InventoryName == item).Count();

                totalResourceElements++;
            }
            
            int trueResources = totalResourceCount/totalResourceElements;
            return trueResources;
        }
    }
}
