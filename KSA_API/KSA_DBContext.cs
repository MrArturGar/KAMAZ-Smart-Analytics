using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TableModelLibrary.Models;

namespace KSA_API
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
        public virtual DbSet<ControlSystem> ControlSystems { get; set; } = null!;
        public virtual DbSet<Ecu> Ecus { get; set; } = null!;
        public virtual DbSet<EcuIdentification> EcuIdentifications { get; set; } = null!;
        public virtual DbSet<Identification> Identifications { get; set; } = null!;
        public virtual DbSet<ProcedureReport> ProcedureReports { get; set; } = null!;
        public virtual DbSet<ServiceCenter> ServiceCenters { get; set; } = null!;
        public virtual DbSet<Session> Sessions { get; set; } = null!;
        public virtual DbSet<SessionEcuidentification> SessionEcuidentifications { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;

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

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Action)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.DateStart)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_start");

                entity.Property(e => e.IdSession).HasColumnName("id_session");

                entity.Property(e => e.ParamText)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(32)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<Composite>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DesignNumber)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("Design_Number");

                entity.Property(e => e.IdEcu).HasColumnName("id_ECU");

            });

            modelBuilder.Entity<ControlSystem>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.Domain }, "ControlSystems_fk0")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Domain)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ecu>(entity =>
            {
                entity.ToTable("ECUs");

                entity.HasIndex(e => e.Codifier, "UQ__ECUs__C9129390709E3DDB")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codifier)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.IdControlSystem).HasColumnName("id_ControlSystem");

            });

            modelBuilder.Entity<EcuIdentification>(entity =>
            {
                entity.ToTable("ECU_Identification");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdEcu).HasColumnName("id_ECU");

                entity.Property(e => e.IdIdentifications).HasColumnName("id_Identifications");

            });

            modelBuilder.Entity<Identification>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.Value }, "Identifications_fk0")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Value).HasMaxLength(512);
            });

            modelBuilder.Entity<ProcedureReport>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DataFiles).HasColumnType("text");

                entity.Property(e => e.DateEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_end");

                entity.Property(e => e.DateStart)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_start");

                entity.Property(e => e.IdEcu).HasColumnName("id_ECU");

                entity.Property(e => e.IdSession).HasColumnName("id_session");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Result)
                    .IsRequired()
                    .HasDefaultValueSql("('False')");

                entity.Property(e => e.Type)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.UsingVin)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("Using_VIN")
                    .HasDefaultValueSql("('NULL')");

            });

            modelBuilder.Entity<ServiceCenter>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasMaxLength(256);

                entity.Property(e => e.City).HasMaxLength(256);

                entity.Property(e => e.Country).HasMaxLength(256);

                entity.Property(e => e.DilerTr)
                    .HasMaxLength(256)
                    .HasColumnName("Diler_tr");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Postcode).HasMaxLength(256);

                entity.Property(e => e.Region).HasMaxLength(256);

                entity.Property(e => e.Status)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Username).HasMaxLength(256);
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasIndex(e => e.SessionsName, "UQ__Sessions__FF7910AD51D19F91")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

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
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Vcisn)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("VCISN");

                entity.Property(e => e.VersionDb)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("VersionDB");

            });

            modelBuilder.Entity<SessionEcuidentification>(entity =>
            {
                entity.ToTable("Session_ECUIdentification");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdEcuidentifications).HasColumnName("id_ECUIdentifications");

                entity.Property(e => e.IdSession).HasColumnName("id_Session");

            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasIndex(e => new { e.Vin, e.DesignNumber, e.Iccid, e.Iccidc, e.Imei, e.Type }, "Vehicles_fk0")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DesignNumber)
                    .HasMaxLength(256)
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
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Vin)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("VIN");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
