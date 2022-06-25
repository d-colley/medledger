using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedLedger.Models;
namespace MedLedger.Data
{
    public class DbInitializer
    {
        public static void Initialize(MedLedgerDBContext context)
        {
            context.Database.EnsureCreated();

            //Looking for any locations
            if (context.Clinics.Any())
            {
                return; //DB has been seeded
            }

            var clinics = new Clinic[]
            {
                new Clinic{ClinicID = 0000, ClinicName = "TestClinic", ClinicLocation = "Kingston", ClinicType = "Type A" }
            };
            foreach (Clinic e in clinics)
            {
                context.Clinics.Add(e);
            }
            context.SaveChanges();
        }
    }
}
