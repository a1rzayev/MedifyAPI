using Microsoft.EntityFrameworkCore;
using MedifyAPI.Core.Models;
using MedifyAPI.Core.Enums;

namespace MedifyAPI.Infrastructure.Repositories.EfCore.DbContexts;
public class MedifyDbContext : DbContext
{
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Hospital> Hospitals { get; set; }


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
            entity.Property(a => a.Phone).HasMaxLength(15);
            entity.Property(a => a.DateJoined);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Name).IsRequired().HasMaxLength(50);
            entity.Property(d => d.Surname).IsRequired().HasMaxLength(50);
            entity.Property(d => d.Email).IsRequired().HasMaxLength(50);
            entity.Property(d => d.Phone).HasMaxLength(15);
            entity.Property(d => d.DateJoined);
            entity.Property(d => d.Speciality).IsRequired();
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Surname).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Email).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Phone).HasMaxLength(15);
            entity.Property(p => p.DateJoined);
        });


        base.OnModelCreating(modelBuilder);
    }
}
