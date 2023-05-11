using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repositories.GeneratedModels;

public partial class MyDBContext : DbContext
{
    public MyDBContext()
    {
    }

    public MyDBContext(DbContextOptions<MyDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vaccination> Vaccinations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("MyDBConnectionString");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Codepatient).HasName("patients_pkey");

            entity.ToTable("patients");

            entity.Property(e => e.Codepatient)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(100L, null, null, null, null, null)
                .HasColumnName("codepatient");
            entity.Property(e => e.Datenegative)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datenegative");
            entity.Property(e => e.Datepositive)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datepositive");
            entity.Property(e => e.Userid)
                .HasMaxLength(9)
                .HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Patients)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patients_userid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Userid)
                .HasMaxLength(9)
                .HasColumnName("userid");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Dateofbirth)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateofbirth");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Housenumber).HasColumnName("housenumber");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Mobile)
                .HasMaxLength(11)
                .HasColumnName("mobile");
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .HasColumnName("phone");
            entity.Property(e => e.Street)
                .HasMaxLength(100)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Vaccination>(entity =>
        {
            entity.HasKey(e => e.Codevaccination).HasName("vaccinations_pkey");

            entity.ToTable("vaccinations");

            entity.Property(e => e.Codevaccination)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(100L, null, null, null, null, null)
                .HasColumnName("codevaccination");
            entity.Property(e => e.Datevaccination)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datevaccination");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(50)
                .HasColumnName("manufacturer");
            entity.Property(e => e.Userid)
                .HasMaxLength(9)
                .HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vaccinations_userid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
