using KitchenEquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenEquipmentManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<RegisteredEquipment> RegisteredEquipments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.UserType)
                .HasConversion<int>();

            modelBuilder.Entity<Equipment>()
                .Property(e => e.Condition)
                .HasConversion<string>();

            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Site>().HasKey(s => s.SiteId);
            modelBuilder.Entity<Equipment>().HasKey(e => e.EquipmentId);
            modelBuilder.Entity<RegisteredEquipment>().HasKey(r => r.Id);

            modelBuilder.Entity<Site>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sites)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.User)
                .WithMany(u => u.Equipments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisteredEquipment>()
                .HasOne(r => r.Equipment)
                .WithMany(e => e.RegisteredEquipments)
                .HasForeignKey(r => r.EquipmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RegisteredEquipment>()
                .HasOne(r => r.Site)
                .WithMany(s => s.RegisteredEquipments)
                .HasForeignKey(r => r.SiteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
