namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUselessFields2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Profiles", "IdAspNetUsersId", "dbo.AspNetUsers");
            DropIndex("dbo.Profiles", new[] { "IdAspNetUsersId" });
            DropIndex("dbo.Reservations", new[] { "User_Id" });
            AddColumn("dbo.AspNetUsers", "ProfileGuid", c => c.Guid(nullable: false));
            AddColumn("dbo.AspNetUsers", "Profile_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Profile_Id");
            AddForeignKey("dbo.AspNetUsers", "Profile_Id", "dbo.Profiles", "Id", cascadeDelete: true);
            DropColumn("dbo.Profiles", "IdAspNetUsersId");
            DropColumn("dbo.Reservations", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Profiles", "IdAspNetUsersId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AspNetUsers", "Profile_Id", "dbo.Profiles");
            DropIndex("dbo.AspNetUsers", new[] { "Profile_Id" });
            DropColumn("dbo.AspNetUsers", "Profile_Id");
            DropColumn("dbo.AspNetUsers", "ProfileGuid");
            CreateIndex("dbo.Reservations", "User_Id");
            CreateIndex("dbo.Profiles", "IdAspNetUsersId");
            AddForeignKey("dbo.Profiles", "IdAspNetUsersId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Reservations", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
