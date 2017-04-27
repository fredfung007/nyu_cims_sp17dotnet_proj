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

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Addresses> Addresses { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<DailyPrices> DailyPrices { get; set; }
        public virtual DbSet<Emails> Emails { get; set; }
        public virtual DbSet<Guests> Guests { get; set; }
        public virtual DbSet<PhoneNumbers> PhoneNumbers { get; set; }
        public virtual DbSet<Profiles> Profiles { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<RoomOccupancies> RoomOccupancies { get; set; }
        public virtual DbSet<RoomTypes> RoomTypes { get; set; }
        public virtual DbSet<Staffs> Staffs { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Addresses>()
                .HasMany(e => e.Profiles)
                .WithRequired(e => e.Addresses)
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

            modelBuilder.Entity<Emails>()
                .HasMany(e => e.Profiles)
                .WithRequired(e => e.Emails)
                .HasForeignKey(e => e.Email_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhoneNumbers>()
                .HasMany(e => e.Profiles)
                .WithRequired(e => e.PhoneNumbers)
                .HasForeignKey(e => e.PhoneNumber_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Profiles>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Profiles)
                .HasForeignKey(e => e.BillingInfo_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reservations>()
                .HasMany(e => e.DailyPrices)
                .WithRequired(e => e.Reservations)
                .HasForeignKey(e => e.Reservation_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reservations>()
                .HasMany(e => e.Guests)
                .WithRequired(e => e.Reservations)
                .HasForeignKey(e => e.Reservation_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoomTypes>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.RoomTypes)
                .HasForeignKey(e => e.RoomType_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoomTypes>()
                .HasMany(e => e.RoomOccupancies)
                .WithRequired(e => e.RoomTypes)
                .HasForeignKey(e => e.RoomType_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Reservations)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.User_Username);
        }
    }
}
