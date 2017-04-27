namespace DataAccessLayer.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CodeFirstHotelModel : DbContext
    {
        public CodeFirstHotelModel()
            : base("name=CodeFirstHotelModel")
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<DailyPrice> DailyPrices { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<RoomOccupancy> RoomOccupancies { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasMany(e => e.Profiles)
                .WithRequired(e => e.Address)
                .HasForeignKey(e => e.Address_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Profiles)
                .WithOptional(e => e.AspNetUsers)
                .HasForeignKey(e => e.IdAspNetUsers_Id);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Reservations)
                .WithOptional(e => e.AspNetUsers)
                .HasForeignKey(e => e.AspNetUsers_Id);

            modelBuilder.Entity<Email>()
                .HasMany(e => e.Profiles)
                .WithRequired(e => e.Email)
                .HasForeignKey(e => e.Email_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhoneNumber>()
                .HasMany(e => e.Profiles)
                .WithRequired(e => e.PhoneNumber)
                .HasForeignKey(e => e.PhoneNumber_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Profile>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Profile)
                .HasForeignKey(e => e.BillingInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reservation>()
                .HasMany(e => e.DailyPrices)
                .WithRequired(e => e.Reservation)
                .HasForeignKey(e => e.Reservation_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reservation>()
                .HasMany(e => e.Guests)
                .WithRequired(e => e.Reservation)
                .HasForeignKey(e => e.Reservation_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoomType>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.RoomType)
                .HasForeignKey(e => e.RoomType_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoomType>()
                .HasMany(e => e.RoomOccupancies)
                .WithRequired(e => e.RoomType)
                .HasForeignKey(e => e.RoomType_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Reservations)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.User_Username);
        }
    }
}
