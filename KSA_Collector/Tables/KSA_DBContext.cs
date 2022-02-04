﻿using System;
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
        public virtual DbSet<EcuIdentification> EcuIdentifications { get; set; } = null!;
        public virtual DbSet<Identification> Identifications { get; set; } = null!;
        public virtual DbSet<ProcedureReport> ProcedureReports { get; set; } = null!;
        public virtual DbSet<ServiceCenter> ServiceCenters { get; set; } = null!;
        public virtual DbSet<Session> Sessions { get; set; } = null!;
        public virtual DbSet<SessionEcuidentification> SessionEcuidentifications { get; set; } = null!;
        public virtual DbSet<System> Systems { get; set; } = null!;
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
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DateStart)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_start");

                entity.Property(e => e.IdSession).HasColumnName("id_session");

                entity.Property(e => e.ParamText)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSessionNavigation)
                    .WithMany(p => p.AoglonassReports)
                    .HasForeignKey(d => d.IdSession)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AOGlonassReports_fk0");
            });

            modelBuilder.Entity<Composite>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DesignNumber)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("Design_Number");

                entity.Property(e => e.IdEcu).HasColumnName("id_ECU");

                entity.HasOne(d => d.IdEcuNavigation)
                    .WithMany(p => p.Composites)
                    .HasForeignKey(d => d.IdEcu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Composites_fk0");
            });

            modelBuilder.Entity<Ecu>(entity =>
            {
                entity.ToTable("ECUs");

                entity.HasIndex(e => e.Codifier, "UQ__ECUs__C9129390D853AD7B")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codifier)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.SystemId).HasColumnName("System_id");

                entity.HasOne(d => d.System)
                    .WithMany(p => p.Ecus)
                    .HasForeignKey(d => d.SystemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ECUs_fk0");
            });

            modelBuilder.Entity<EcuIdentification>(entity =>
            {
                entity.ToTable("ECU_Identification");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdEcu).HasColumnName("id_ECU");

                entity.Property(e => e.IdIdentifications).HasColumnName("id_Identifications");

                entity.HasOne(d => d.IdEcuNavigation)
                    .WithMany(p => p.EcuIdentifications)
                    .HasForeignKey(d => d.IdEcu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ECU_Identification_fk0");

                entity.HasOne(d => d.IdIdentificationsNavigation)
                    .WithMany(p => p.EcuIdentifications)
                    .HasForeignKey(d => d.IdIdentifications)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ECU_Identification_fk1");
            });

            modelBuilder.Entity<Identification>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .IsUnicode(false);
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

                entity.Property(e => e.Name).HasMaxLength(255);

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

                entity.HasOne(d => d.IdEcuNavigation)
                    .WithMany(p => p.ProcedureReports)
                    .HasForeignKey(d => d.IdEcu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProcedureReports_fk0");

                entity.HasOne(d => d.IdSessionNavigation)
                    .WithMany(p => p.ProcedureReports)
                    .HasForeignKey(d => d.IdSession)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProcedureReports_fk1");
            });

            modelBuilder.Entity<ServiceCenter>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(255);

                entity.Property(e => e.Country).HasMaxLength(255);

                entity.Property(e => e.DilerTr)
                    .HasMaxLength(255)
                    .HasColumnName("Diler_tr");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Postcode).HasMaxLength(255);

                entity.Property(e => e.Region).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(255);

                entity.Property(e => e.Username).HasMaxLength(255);
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasIndex(e => e.SessionsName, "UQ__Sessions__FF7910ADFC7B529B")
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

                entity.HasOne(d => d.IdServiceCentersNavigation)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.IdServiceCenters)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Sessions_fk1");

                entity.HasOne(d => d.IdVehicleNavigation)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.IdVehicle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Sessions_fk0");
            });

            modelBuilder.Entity<SessionEcuidentification>(entity =>
            {
                entity.ToTable("Session_ECUIdentification");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdEcuidentifications).HasColumnName("id_ECUIdentifications");

                entity.Property(e => e.IdSession).HasColumnName("id_Session");

                entity.HasOne(d => d.IdEcuidentificationsNavigation)
                    .WithMany(p => p.SessionEcuidentifications)
                    .HasForeignKey(d => d.IdEcuidentifications)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Session_ECUIdentification_fk0");

                entity.HasOne(d => d.IdSessionNavigation)
                    .WithMany(p => p.SessionEcuidentifications)
                    .HasForeignKey(d => d.IdSession)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Session_ECUIdentification_fk1");
            });

            modelBuilder.Entity<System>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Systems__737584F6222DDC69")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Domain)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
