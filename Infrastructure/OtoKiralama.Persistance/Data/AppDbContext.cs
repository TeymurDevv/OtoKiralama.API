﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using System.Reflection;

namespace OtoKiralama.Persistance.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

            // When a Brand is deleted, all related Models will be deleted.
            modelBuilder.Entity<Model>()
                .HasOne(m => m.Brand)
                .WithMany(b => b.Models)
                .HasForeignKey(m => m.BrandId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete Models when a Brand is deleted

            // Disable cascade delete from Brand to Cars
            //modelBuilder.Entity<Car>()
            //    .HasOne(c => c.Brand)
            //    .WithMany(b => b.Cars)
            //    .HasForeignKey(c => c.BrandId)
            //    .OnDelete(DeleteBehavior.Restrict); // No cascade delete from Brand to Cars directly

            // When a Model is deleted, all related Cars will be deleted.
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Model)
                .WithMany(m => m.Cars)
                .HasForeignKey(c => c.ModelId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete Cars when a Model is delete

            modelBuilder.Entity<Reservation>()
                .HasOne<AppUser>(r => (AppUser)r.AppUser)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.AppUserId);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // TPT (Table-per-Type) Inheritance istifadə edirik
            modelBuilder.Entity<IndividualInvoice>().ToTable("IndividualInvoices");
            modelBuilder.Entity<CorporateInvoice>().ToTable("CorporateInvoices");
            modelBuilder.Entity<IndividualCompanyInvoice>().ToTable("IndividualCompanyInvoices");

            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Invoice)
                .WithOne(f => f.AppUser)
                .HasForeignKey<Invoice>(f => f.AppUserId);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Model> Models { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Gear> Gears { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Body> Bodies { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<CarPhoto> CarPhotos { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<DeliveryType> DeliveryTypes { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<FilterRange> FilterRanges { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
