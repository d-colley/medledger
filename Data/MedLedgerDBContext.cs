using MedLedger.Models;
using Microsoft.EntityFrameworkCore;

namespace MedLedger.Data
{
    public class MedLedgerDBContext : DbContext
    {
        public MedLedgerDBContext(DbContextOptions<MedLedgerDBContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Professional> Professionals { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clinic>().ToTable("Clinic");
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<Professional>().ToTable("Professional");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<User>().ToTable("User");
        }

    }
}
