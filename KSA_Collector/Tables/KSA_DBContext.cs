using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KSA_Collector.Tables
{
    public partial class KSA_DBContext : DbContext
    {
        public KSA_DBContext()
        {
        }

        public KSA_DBContext(DbContextOptions<KSA_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AoglonassReport> AoglonassReports { get; set; } = null!;
        public virtual DbSet<Composite> Composites { get; set; } = null!;
        public virtual DbSet<Ecu> Ecus { get; set; } = null!;
        public virtual DbSet<EcuIdentidication> EcuIdentidications { get; set; } = null!;
        public virtual DbSet<Identification> Identifications { get; set; } = null!;
        public virtual DbSet<ProcedureReport> ProcedureReports { get; set; } = null!;
        public virtual DbSet<ServiceCenter> ServiceCenters { get; set; } = null!;
        public virtual DbSet<Session> Sessions { get; set; } = null!;
        public virtual DbSet<System> Systems { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
        public virtual DbSet<VehiclesEcu> VehiclesEcus { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=KSA_DB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AoglonassReport>(entity =>
            {
                entity.ToTable("AOGlonassReports");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.DateStart)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_start");

                entity.Property(e => e.IdSession).HasColumnName("id_session");

                entity.Property(e => e.Request).HasColumnType("text");

                entity.Property(e => e.Response).HasColumnType("text");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Composite>(entity =>
            {
                entity.HasIndex(e => e.DesignNumber, "UQ__Composit__37F5005DF44329D9")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.DesignNumber)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("Design_Number");

                entity.Property(e => e.IdEcu).HasColumnName("id_ECU");
            });

            modelBuilder.Entity<Ecu>(entity =>
            {
                entity.ToTable("ECUs");

                entity.HasIndex(e => e.Codifier, "UQ__ECUs__C912939060EC1BD1")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Codifier)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.SystemId).HasColumnName("System_id");
            });

            modelBuilder.Entity<EcuIdentidication>(entity =>
            {
                entity.ToTable("ECU_Identidication");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.IdEcu).HasColumnName("id_ECU");

                entity.Property(e => e.IdIdentifications).HasColumnName("id_Identifications");
            });

            modelBuilder.Entity<Identification>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProcedureReport>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Codifier)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.DataFiles).HasColumnType("text");

                entity.Property(e => e.DateEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_end");

                entity.Property(e => e.DateStart)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_start");

                entity.Property(e => e.IdSession).HasColumnName("id_session");

                entity.Property(e => e.ProcedureName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Procedure_name");

                entity.Property(e => e.Result)
                    .IsRequired()
                    .HasDefaultValueSql("('False')");

                entity.Property(e => e.UsingVin)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("Using_VIN")
                    .HasDefaultValueSql("('NULL')");
            });

            modelBuilder.Entity<ServiceCenter>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DilerTr)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Diler_tr");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Postcode)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Region)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasIndex(e => e.Date, "UQ__Sessions__77387D07C6215104")
                    .IsUnique();

                entity.HasIndex(e => e.SessionsName, "UQ__Sessions__FF7910ADA6B4AAF6")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.HasDtc)
                    .IsRequired()
                    .HasColumnName("Has_DTC")
                    .HasDefaultValueSql("('False')");

                entity.Property(e => e.HasFlash)
                    .IsRequired()
                    .HasColumnName("Has_Flash")
                    .HasDefaultValueSql("('False')");

                entity.Property(e => e.HasIdentifications)
                    .IsRequired()
                    .HasColumnName("Has_Identifications")
                    .HasDefaultValueSql("('False')");

                entity.Property(e => e.HasTests)
                    .IsRequired()
                    .HasColumnName("Has_Tests")
                    .HasDefaultValueSql("('False')");

                entity.Property(e => e.IdServiceCenters).HasColumnName("id_serviceCenters");

                entity.Property(e => e.IdVehicle).HasColumnName("id_Vehicle");

                entity.Property(e => e.SessionsName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Vcisn)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("VCISN");

                entity.Property(e => e.VersionDb)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("VersionDB");

                entity.HasOne(d => d.IdVehicleNavigation)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.IdVehicle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Sessions_fk0");
            });

            modelBuilder.Entity<System>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Systems__737584F666A714C3")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Domain)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.DesignNumber)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("Design_Number");

                entity.Property(e => e.Iccid)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ICCID");

                entity.Property(e => e.Iccidc)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ICCIDC");

                entity.Property(e => e.Imei)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("IMEI");

                entity.Property(e => e.Type)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Vin)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("VIN");
            });

            modelBuilder.Entity<VehiclesEcu>(entity =>
            {
                entity.ToTable("Vehicles_ECUs");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.IdIdentifications).HasColumnName("id_Identifications");

                entity.Property(e => e.IdVehicle).HasColumnName("id_Vehicle");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
