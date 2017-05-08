using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.EF
{
    public class HotelModelContext : IdentityDbContext
    {
#if DEBUG
        //private static string _name = "name=ProductionConnection";
        private static readonly string _name = "name=HotelModelContext";
#else
        private static string _name = "name=ProductionConnection";
#endif

        public HotelModelContext()
            : base(_name)
        {
            Database.SetInitializer<HotelModelContext>(null);
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static HotelModelContext Create()
        {
            return new HotelModelContext();
        }


        public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<AspNetRole> AspNetUserRoles { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<DailyPrice> DailyPrices { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<RoomOccupancy> RoomOccupancies { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure Asp Net Identity Tables
            modelBuilder.Entity<AspNetUser>().Property(u => u.PasswordHash).HasMaxLength(500);
            modelBuilder.Entity<AspNetUser>().Property(u => u.SecurityStamp).HasMaxLength(500);
            modelBuilder.Entity<AspNetUser>().Property(u => u.PhoneNumber).HasMaxLength(50);
            modelBuilder.Entity<AspNetUserClaim>().Property(u => u.ClaimType).HasMaxLength(150);
            modelBuilder.Entity<AspNetUserClaim>().Property(u => u.ClaimValue).HasMaxLength(500);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Reservations)
                .WithOptional(e => e.AspNetUser);

            modelBuilder.Entity<AspNetUser>()
                .HasRequired(e => e.Profile);

            modelBuilder.Entity<Reservation>()
                .HasMany(e => e.DailyPrices);
        }
    }
}