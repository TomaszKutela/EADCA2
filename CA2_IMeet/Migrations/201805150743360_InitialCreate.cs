namespace CA2_IMeet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Booking",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        MeetingReference = c.String(nullable: false, maxLength: 55),
                        RoomId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Start_DateTime = c.DateTime(nullable: false),
                        End_DateTime = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.MeetingRoom", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.MeetingRoom",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Size = c.Int(nullable: false),
                        Location = c.String(nullable: false, maxLength: 30),
                        Equipment = c.String(),
                    })
                .PrimaryKey(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Booking", "RoomId", "dbo.MeetingRoom");
            DropIndex("dbo.Booking", new[] { "RoomId" });
            DropTable("dbo.MeetingRoom");
            DropTable("dbo.Booking");
        }
    }
}
