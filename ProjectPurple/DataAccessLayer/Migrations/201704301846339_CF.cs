namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CF : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstLine = c.String(nullable: false),
                        SecondLine = c.String(),
                        State = c.Int(nullable: false),
                        ZipCode = c.String(nullable: false),
                        City = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        LoyaltyYear = c.DateTime(),
                        LoyaltyProgress = c.Int(nullable: false),
                        ProfileGuid = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Profile_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.Profile_Id, cascadeDelete: true)
                .Index(t => t.Profile_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        AddressId = c.Int(nullable: false),
                        Address_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .Index(t => t.Address_Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        CheckInDate = c.DateTime(),
                        CheckOutDate = c.DateTime(),
                        BillingInfo = c.Guid(nullable: false),
                        RoomTypeId = c.Guid(nullable: false),
                        AspNetUsersId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomTypes", t => t.RoomTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Profiles", t => t.BillingInfo)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUsersId)
                .Index(t => t.BillingInfo)
                .Index(t => t.RoomTypeId)
                .Index(t => t.AspNetUsersId);
            
            CreateTable(
                "dbo.DailyPrices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BillingPrice = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ReservationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservations", t => t.ReservationId)
                .Index(t => t.ReservationId);
            
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Reservation_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservations", t => t.Reservation_Id)
                .Index(t => t.Reservation_Id);
            
            CreateTable(
                "dbo.RoomTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BaseRate = c.Int(nullable: false),
                        Inventory = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Ameneties = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomOccupancies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Occupancy = c.Int(nullable: false),
                        RoomTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomTypes", t => t.RoomTypeId)
                .Index(t => t.RoomTypeId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reservations", "AspNetUsersId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Profile_Id", "dbo.Profiles");
            DropForeignKey("dbo.Reservations", "BillingInfo", "dbo.Profiles");
            DropForeignKey("dbo.Reservations", "RoomTypeId", "dbo.RoomTypes");
            DropForeignKey("dbo.RoomOccupancies", "RoomTypeId", "dbo.RoomTypes");
            DropForeignKey("dbo.Guests", "Reservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.DailyPrices", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.Profiles", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.RoomOccupancies", new[] { "RoomTypeId" });
            DropIndex("dbo.Guests", new[] { "Reservation_Id" });
            DropIndex("dbo.DailyPrices", new[] { "ReservationId" });
            DropIndex("dbo.Reservations", new[] { "AspNetUsersId" });
            DropIndex("dbo.Reservations", new[] { "RoomTypeId" });
            DropIndex("dbo.Reservations", new[] { "BillingInfo" });
            DropIndex("dbo.Profiles", new[] { "Address_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Profile_Id" });
            DropTable("dbo.AspNetUserRoles");
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
