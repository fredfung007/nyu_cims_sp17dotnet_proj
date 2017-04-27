using System.Data.Entity;

namespace DataAccessLayer.EF
{
    public class CodeFirstHotelModel : DbContext
    {
#if DEBUG
        private static string name = "name=ProductionConnection";
#else
        private static string name = "name=CodeFirstHotelModel";
#endif
        public CodeFirstHotelModel()
            : base(name)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<DailyPrice> DailyPrices { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<RoomOccupancy> RoomOccupancies { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Profiles)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.IdAspNetUsersId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Reservations)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.AspNetUsersId);

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

            modelBuilder.Entity<Reservation>()
                .HasMany(e => e.Guests)
                .WithRequired(e => e.Reservation)
                .HasForeignKey(e => e.ReservationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoomType>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.RoomType)
                .HasForeignKey(e => e.RoomTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoomType>()
                .HasMany(e => e.RoomOccupancies)
                .WithRequired(e => e.RoomType)
                .HasForeignKey(e => e.RoomTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Reservations)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.UserUsername);
        }
    }
}