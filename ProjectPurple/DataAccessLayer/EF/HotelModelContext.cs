using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.EF
{
    public class HotelModelContext : IdentityDbContext
    {
#if DEBUG2
        private static string name = "name=HotelModelContext";
#else
        private static string name = "name=ProductionConnection";
#endif
        public HotelModelContext()
            : base(name)
        {
            Database.SetInitializer<HotelModelContext>(null);// Remove default initializer
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

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Configure Asp Net Identity Tables
            modelBuilder.Entity<AspNetUser>().ToTable("AspNetUsers");
            modelBuilder.Entity<AspNetUser>().Property(u => u.PasswordHash).HasMaxLength(500);
            modelBuilder.Entity<AspNetUser>().Property(u => u.SecurityStamp).HasMaxLength(500);
            modelBuilder.Entity<AspNetUser>().Property(u => u.PhoneNumber).HasMaxLength(50);

            modelBuilder.Entity<AspNetRole>().ToTable("AspNetUserRoles");
            modelBuilder.Entity<AspNetUserLogin>().ToTable("AspNetUserLogins");
            modelBuilder.Entity<AspNetUserClaim>().ToTable("AspNetUserClaims");
            modelBuilder.Entity<AspNetUserClaim>().Property(u => u.ClaimType).HasMaxLength(150);
            modelBuilder.Entity<AspNetUserClaim>().Property(u => u.ClaimValue).HasMaxLength(500);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Reservations)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.AspNetUsersId);

            modelBuilder.Entity<AspNetUser>()
                .HasRequired(e => e.Profile);

            modelBuilder.Entity<Profile>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Profile)
                .HasForeignKey(e => e.BillingInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reservation>()
                .HasMany(e => e.DailyPrices)
                .WithRequired(e => e.Reservation)
                .HasForeignKey(e => e.ReservationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoomType>()
                .HasMany(e => e.RoomOccupancies)
                .WithRequired(e => e.RoomType)
                .HasForeignKey(e => e.RoomTypeId)
                .WillCascadeOnDelete(false);
        }
    }
}