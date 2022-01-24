using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace KSA_Collector.TablesOld
{
    class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=KSD_DB;Trusted_Connection=True;");
        }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ProcedureReport> ProcedureReports { get; set; }
        public DbSet<AOGlonassReport> AOGlonassReports { get; set; }
        public DbSet<ECU> ECUs { get; set; }
        public DbSet<Identification> Identifications { get; set; }
        public DbSet<Composite> Composites { get; set; }
        public DbSet<System> Systems { get; set; }
        public DbSet<ECU_Identidication> ECU_Identidication { get; set; }
        public DbSet<Vehicles_ECUs> Vehicles_ECUs { get; set; }
        public DbSet<ServiceCenter> ServiceCenters { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<System>().HasIndex(u => u.Name).IsUnique();
        }
    }
}
