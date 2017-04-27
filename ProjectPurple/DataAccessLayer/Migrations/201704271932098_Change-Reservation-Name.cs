namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeReservationName : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Profiles", new[] { "Address_Id1" });
            DropColumn("dbo.Profiles", "Address_Id");
            RenameColumn(table: "dbo.Profiles", name: "IdAspNetUsers_Id", newName: "IdAspNetUsersId");
            RenameColumn(table: "dbo.Reservations", name: "AspNetUsers_Id", newName: "AspNetUsersId");
            RenameColumn(table: "dbo.Profiles", name: "Address_Id1", newName: "Address_Id");
            RenameColumn(table: "dbo.DailyPrices", name: "Reservation_Id", newName: "ReservationId");
            RenameColumn(table: "dbo.Guests", name: "Reservation_Id", newName: "ReservationId");
            RenameColumn(table: "dbo.Reservations", name: "RoomType_Id", newName: "RoomTypeId");
            RenameColumn(table: "dbo.Reservations", name: "User_Username", newName: "UserUsername");
            RenameColumn(table: "dbo.RoomOccupancies", name: "RoomType_Id", newName: "RoomTypeId");
            RenameIndex(table: "dbo.Profiles", name: "IX_IdAspNetUsers_Id", newName: "IX_IdAspNetUsersId");
            RenameIndex(table: "dbo.Reservations", name: "IX_RoomType_Id", newName: "IX_RoomTypeId");
            RenameIndex(table: "dbo.Reservations", name: "IX_User_Username", newName: "IX_UserUsername");
            RenameIndex(table: "dbo.Reservations", name: "IX_AspNetUsers_Id", newName: "IX_AspNetUsersId");
            RenameIndex(table: "dbo.DailyPrices", name: "IX_Reservation_Id", newName: "IX_ReservationId");
            RenameIndex(table: "dbo.Guests", name: "IX_Reservation_Id", newName: "IX_ReservationId");
            RenameIndex(table: "dbo.RoomOccupancies", name: "IX_RoomType_Id", newName: "IX_RoomTypeId");
            AddColumn("dbo.Profiles", "AddressId", c => c.Int(nullable: false));
            AlterColumn("dbo.Profiles", "Address_Id", c => c.Guid());
            CreateIndex("dbo.Profiles", "Address_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Profiles", new[] { "Address_Id" });
            AlterColumn("dbo.Profiles", "Address_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Profiles", "AddressId");
            RenameIndex(table: "dbo.RoomOccupancies", name: "IX_RoomTypeId", newName: "IX_RoomType_Id");
            RenameIndex(table: "dbo.Guests", name: "IX_ReservationId", newName: "IX_Reservation_Id");
            RenameIndex(table: "dbo.DailyPrices", name: "IX_ReservationId", newName: "IX_Reservation_Id");
            RenameIndex(table: "dbo.Reservations", name: "IX_AspNetUsersId", newName: "IX_AspNetUsers_Id");
            RenameIndex(table: "dbo.Reservations", name: "IX_UserUsername", newName: "IX_User_Username");
            RenameIndex(table: "dbo.Reservations", name: "IX_RoomTypeId", newName: "IX_RoomType_Id");
            RenameIndex(table: "dbo.Profiles", name: "IX_IdAspNetUsersId", newName: "IX_IdAspNetUsers_Id");
            RenameColumn(table: "dbo.RoomOccupancies", name: "RoomTypeId", newName: "RoomType_Id");
            RenameColumn(table: "dbo.Reservations", name: "UserUsername", newName: "User_Username");
            RenameColumn(table: "dbo.Reservations", name: "RoomTypeId", newName: "RoomType_Id");
            RenameColumn(table: "dbo.Guests", name: "ReservationId", newName: "Reservation_Id");
            RenameColumn(table: "dbo.DailyPrices", name: "ReservationId", newName: "Reservation_Id");
            RenameColumn(table: "dbo.Profiles", name: "Address_Id", newName: "Address_Id1");
            RenameColumn(table: "dbo.Reservations", name: "AspNetUsersId", newName: "AspNetUsers_Id");
            RenameColumn(table: "dbo.Profiles", name: "IdAspNetUsersId", newName: "IdAspNetUsers_Id");
            AddColumn("dbo.Profiles", "Address_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Profiles", "Address_Id1");
        }
    }
}
