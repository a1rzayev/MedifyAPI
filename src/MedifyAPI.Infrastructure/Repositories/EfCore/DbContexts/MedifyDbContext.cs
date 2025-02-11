using Microsoft.EntityFrameworkCore;
using MedifyAPI.Core.Models;
using MedifyAPI.Core.Enums;
using System.Text.Json;
using MedifyAPI.Core.Models.Requests;

namespace MedifyAPI.Infrastructure.Repositories.EfCore.DbContexts;
public class MedifyDbContext : DbContext
{
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Log> Logs { get; set; }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Hospital> Hospitals { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserValidation> UserValidations { get; set; }
    public DbSet<UserValidation> DoctorValidations { get; set; }
    public DbSet<VerifyDegreeRequest> VerifyDegreeRequests { get; set; }

    public MedifyDbContext(DbContextOptions<MedifyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Name).IsRequired().HasMaxLength(50);
            entity.Property(a => a.Surname).IsRequired().HasMaxLength(50);
            entity.Property(a => a.Email).IsRequired().HasMaxLength(50);
            entity.Property(a => a.Password).IsRequired().HasMaxLength(100);
            entity.Property(a => a.Phone).HasMaxLength(15);
            entity.Property(a => a.DateJoined);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Name).IsRequired().HasMaxLength(50);
            entity.Property(d => d.Surname).IsRequired().HasMaxLength(50);
            entity.Property(d => d.Email).IsRequired().HasMaxLength(50);
            entity.Property(a => a.Password).IsRequired().HasMaxLength(100);
            entity.Property(d => d.Phone).HasMaxLength(15);
            entity.Property(d => d.DateJoined);
            entity.Property(d => d.Speciality);

        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Surname).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Email).IsRequired().HasMaxLength(50);
            entity.Property(a => a.Password).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Phone).HasMaxLength(15);
            entity.Property(p => p.DateJoined);
        });
        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DoctorId).IsRequired();
            entity.Property(e => e.PatientId).IsRequired();
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Date).IsRequired();
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.HasKey(h => h.Id);
            entity.Property(h => h.Name).IsRequired();
            entity.Property(h => h.Address).IsRequired();
            entity.Property(h => h.Phones).HasColumnType("nvarchar(max)"); // JSON column
            entity.Property(h => h.Email).HasMaxLength(255);
            entity.Property(h => h.Website).HasMaxLength(255);
            entity.Property(h => h.Type).IsRequired();
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(rt => rt.Id);
            entity.Property(rt => rt.UserId).IsRequired();
            entity.Property(rt => rt.Token).IsRequired().HasMaxLength(500);
            entity.Property(rt => rt.ExpirationDate).IsRequired();
            entity.Property(rt => rt.IsRevoked).IsRequired();
        });

        modelBuilder.Entity<UserValidation>(entity =>
        {
            entity.HasKey(uv => uv.UserId);
            entity.Property(uv => uv.IsValidated).IsRequired();
        });

        modelBuilder.Entity<UserValidation>(entity =>
        {
            entity.HasKey(dv => dv.UserId);
            entity.Property(dv => dv.IsValidated).IsRequired();
        });

        modelBuilder.Entity<VerifyDegreeRequest>(entity =>
        {
            entity.HasKey(vdr => vdr.Id);
            entity.Property(vdr => vdr.SenderId).IsRequired();
            entity.Property(vdr => vdr.State).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}
