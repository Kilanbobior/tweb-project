using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class TWebDbContext : DbContext
    {
        public TWebDbContext() : base("Server=localhost;port=5432;Database=Vacation;User Id=postgres;Password=5656;")
        {
        }

        public DbSet<VacationBase> VacationBases { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure relationships and constraints
            modelBuilder.Entity<Booking>()
                .HasRequired(b => b.VacationBase)
                .WithMany(v => v.Bookings)
                .HasForeignKey(b => b.VacationBaseId);

            modelBuilder.Entity<Booking>()
                .HasRequired(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);

            // Many-to-many relationship between VacationBase and Amenity
            modelBuilder.Entity<VacationBase>()
                .HasMany(v => v.Amenities)
                .WithMany(a => a.VacationBases)
                .Map(m =>
                {
                    m.ToTable("VacationBaseAmenities");
                    m.MapLeftKey("VacationBaseId");
                    m.MapRightKey("AmenityId");
                });
        }
    }
}
