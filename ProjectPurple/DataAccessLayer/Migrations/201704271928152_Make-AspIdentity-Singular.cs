using System.Data.Entity.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class MakeAspIdentitySingular : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Addresses",
                    c => new
                    {
                        Id = c.Guid(false),
                        FirstLine = c.String(false),
                        SecondLine = c.String(),
                        State = c.Int(false),
                        ZipCode = c.String(false),
                        City = c.String(false)
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.AspNetRoles",
                    c => new
                    {
                        Id = c.String(false, 128),
                        Name = c.String(false, 256)
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.AspNetUsers",
                    c => new
                    {
                        Id = c.String(false, 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(false),
                        TwoFactorEnabled = c.Boolean(false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(false),
                        AccessFailedCount = c.Int(false),
                        UserName = c.String(false, 256)
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.AspNetUserClaims",
                    c => new
                    {
                        Id = c.Int(false, true),
                        UserId = c.String(false, 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, true)
                .Index(t => t.UserId);

            CreateTable(
                    "dbo.AspNetUserLogins",
                    c => new
                    {
                        LoginProvider = c.String(false, 128),
                        ProviderKey = c.String(false, 128),
                        UserId = c.String(false, 128)
                    })
                .PrimaryKey(t => new {t.LoginProvider, t.ProviderKey, t.UserId})
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, true)
                .Index(t => t.UserId);

            CreateTable(
                    "dbo.Profiles",
                    c => new
                    {
                        Id = c.Guid(false),
                        FirstName = c.String(false),
                        LastName = c.String(false),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Address_Id = c.Int(false),
                        IdAspNetUsers_Id = c.String(maxLength: 128),
                        Address_Id1 = c.Guid()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id1)
                .ForeignKey("dbo.AspNetUsers", t => t.IdAspNetUsers_Id)
                .Index(t => t.IdAspNetUsers_Id)
                .Index(t => t.Address_Id1);

            CreateTable(
                    "dbo.Reservations",
                    c => new
                    {
                        Id = c.Guid(false),
                        startDate = c.DateTime(false),
                        endDate = c.DateTime(false),
                        isPaid = c.Boolean(false),
                        checkInDate = c.DateTime(),
                        checkOutDate = c.DateTime(),
                        BillingInfo = c.Guid(false),
                        RoomType_Id = c.Guid(false),
                        User_Username = c.String(maxLength: 30),
                        AspNetUsers_Id = c.String(maxLength: 128)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomTypes", t => t.RoomType_Id)
                .ForeignKey("dbo.Users", t => t.User_Username)
                .ForeignKey("dbo.Profiles", t => t.BillingInfo)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUsers_Id)
                .Index(t => t.BillingInfo)
                .Index(t => t.RoomType_Id)
                .Index(t => t.User_Username)
                .Index(t => t.AspNetUsers_Id);

            CreateTable(
                    "dbo.DailyPrices",
                    c => new
                    {
                        Id = c.Guid(false),
                        BillingPrice = c.Int(false),
                        Date = c.DateTime(false),
                        Reservation_Id = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservations", t => t.Reservation_Id)
                .Index(t => t.Reservation_Id);

            CreateTable(
                    "dbo.Guests",
                    c => new
                    {
                        Id = c.Guid(false),
                        FirstName = c.String(false),
                        LastName = c.String(false),
                        Reservation_Id = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservations", t => t.Reservation_Id)
                .Index(t => t.Reservation_Id);

            CreateTable(
                    "dbo.RoomTypes",
                    c => new
                    {
                        Id = c.Guid(false),
                        BaseRate = c.Int(false),
                        Inventory = c.Int(false),
                        Type = c.Int(false),
                        Ameneties = c.String(false),
                        Description = c.String(false),
                        ImageUrl = c.String()
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.RoomOccupancies",
                    c => new
                    {
                        Id = c.Guid(false),
                        Date = c.DateTime(false),
                        Occupancy = c.Int(false),
                        RoomType_Id = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomTypes", t => t.RoomType_Id)
                .Index(t => t.RoomType_Id);

            CreateTable(
                    "dbo.Users",
                    c => new
                    {
                        Username = c.String(false, 30),
                        HashedPassword = c.String(false),
                        isRegistered = c.Boolean(false),
                        LoyalProgramNumber = c.String(),
                        LoyaltyProgress = c.Int(),
                        LoyaltyYear = c.DateTime(),
                        Profile_Id = c.Guid()
                    })
                .PrimaryKey(t => t.Username)
                .ForeignKey("dbo.Profiles", t => t.Profile_Id)
                .Index(t => t.Profile_Id);

            CreateTable(
                    "dbo.Staffs",
                    c => new
                    {
                        Username = c.String(false, 30),
                        HashedPassword = c.String(false)
                    })
                .PrimaryKey(t => t.Username);

            CreateTable(
                    "dbo.AspNetUserRoles",
                    c => new
                    {
                        RoleId = c.String(false, 128),
                        UserId = c.String(false, 128)
                    })
                .PrimaryKey(t => new {t.RoleId, t.UserId})
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reservations", "AspNetUsers_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Profiles", "IdAspNetUsers_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reservations", "BillingInfo", "dbo.Profiles");
            DropForeignKey("dbo.Reservations", "User_Username", "dbo.Users");
            DropForeignKey("dbo.Users", "Profile_Id", "dbo.Profiles");
            DropForeignKey("dbo.RoomOccupancies", "RoomType_Id", "dbo.RoomTypes");
            DropForeignKey("dbo.Reservations", "RoomType_Id", "dbo.RoomTypes");
            DropForeignKey("dbo.Guests", "Reservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.DailyPrices", "Reservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.Profiles", "Address_Id1", "dbo.Addresses");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUserRoles", new[] {"UserId"});
            DropIndex("dbo.AspNetUserRoles", new[] {"RoleId"});
            DropIndex("dbo.Users", new[] {"Profile_Id"});
            DropIndex("dbo.RoomOccupancies", new[] {"RoomType_Id"});
            DropIndex("dbo.Guests", new[] {"Reservation_Id"});
            DropIndex("dbo.DailyPrices", new[] {"Reservation_Id"});
            DropIndex("dbo.Reservations", new[] {"AspNetUsers_Id"});
            DropIndex("dbo.Reservations", new[] {"User_Username"});
            DropIndex("dbo.Reservations", new[] {"RoomType_Id"});
            DropIndex("dbo.Reservations", new[] {"BillingInfo"});
            DropIndex("dbo.Profiles", new[] {"Address_Id1"});
            DropIndex("dbo.Profiles", new[] {"IdAspNetUsers_Id"});
            DropIndex("dbo.AspNetUserLogins", new[] {"UserId"});
            DropIndex("dbo.AspNetUserClaims", new[] {"UserId"});
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Staffs");
            DropTable("dbo.Users");
            DropTable("dbo.RoomOccupancies");
            DropTable("dbo.RoomTypes");
            DropTable("dbo.Guests");
            DropTable("dbo.DailyPrices");
            DropTable("dbo.Reservations");
            DropTable("dbo.Profiles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Addresses");
        }
    }
}