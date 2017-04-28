using System.Data.Entity.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class ChangeReservationName : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Profiles", new[] {"Address_Id1"});
            DropColumn("dbo.Profiles", "Address_Id");
            RenameColumn("dbo.Profiles", "IdAspNetUsers_Id", "IdAspNetUsersId");
            RenameColumn("dbo.Reservations", "AspNetUsers_Id", "AspNetUsersId");
            RenameColumn("dbo.Profiles", "Address_Id1", "Address_Id");
            RenameColumn("dbo.DailyPrices", "Reservation_Id", "ReservationId");
            RenameColumn("dbo.Guests", "Reservation_Id", "ReservationId");
            RenameColumn("dbo.Reservations", "RoomType_Id", "RoomTypeId");
            RenameColumn("dbo.Reservations", "User_Username", "UserUsername");
            RenameColumn("dbo.RoomOccupancies", "RoomType_Id", "RoomTypeId");
            RenameIndex("dbo.Profiles", "IX_IdAspNetUsers_Id", "IX_IdAspNetUsersId");
            RenameIndex("dbo.Reservations", "IX_RoomType_Id", "IX_RoomTypeId");
            RenameIndex("dbo.Reservations", "IX_User_Username", "IX_UserUsername");
            RenameIndex("dbo.Reservations", "IX_AspNetUsers_Id", "IX_AspNetUsersId");
            RenameIndex("dbo.DailyPrices", "IX_Reservation_Id", "IX_ReservationId");
            RenameIndex("dbo.Guests", "IX_Reservation_Id", "IX_ReservationId");
            RenameIndex("dbo.RoomOccupancies", "IX_RoomType_Id", "IX_RoomTypeId");
            AddColumn("dbo.Profiles", "AddressId", c => c.Int(false));
            AlterColumn("dbo.Profiles", "Address_Id", c => c.Guid());
            CreateIndex("dbo.Profiles", "Address_Id");
        }

        public override void Down()
        {
            DropIndex("dbo.Profiles", new[] {"Address_Id"});
            AlterColumn("dbo.Profiles", "Address_Id", c => c.Int(false));
            DropColumn("dbo.Profiles", "AddressId");
            RenameIndex("dbo.RoomOccupancies", "IX_RoomTypeId", "IX_RoomType_Id");
            RenameIndex("dbo.Guests", "IX_ReservationId", "IX_Reservation_Id");
            RenameIndex("dbo.DailyPrices", "IX_ReservationId", "IX_Reservation_Id");
            RenameIndex("dbo.Reservations", "IX_AspNetUsersId", "IX_AspNetUsers_Id");
            RenameIndex("dbo.Reservations", "IX_UserUsername", "IX_User_Username");
            RenameIndex("dbo.Reservations", "IX_RoomTypeId", "IX_RoomType_Id");
            RenameIndex("dbo.Profiles", "IX_IdAspNetUsersId", "IX_IdAspNetUsers_Id");
            RenameColumn("dbo.RoomOccupancies", "RoomTypeId", "RoomType_Id");
            RenameColumn("dbo.Reservations", "UserUsername", "User_Username");
            RenameColumn("dbo.Reservations", "RoomTypeId", "RoomType_Id");
            RenameColumn("dbo.Guests", "ReservationId", "Reservation_Id");
            RenameColumn("dbo.DailyPrices", "ReservationId", "Reservation_Id");
            RenameColumn("dbo.Profiles", "Address_Id", "Address_Id1");
            RenameColumn("dbo.Reservations", "AspNetUsersId", "AspNetUsers_Id");
            RenameColumn("dbo.Profiles", "IdAspNetUsersId", "IdAspNetUsers_Id");
            AddColumn("dbo.Profiles", "Address_Id", c => c.Int(false));
            CreateIndex("dbo.Profiles", "Address_Id1");
        }
    }
}