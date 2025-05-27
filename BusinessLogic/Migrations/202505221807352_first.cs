namespace BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VacationBaseId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        CheckInDate = c.DateTime(nullable: false),
                        CheckOutDate = c.DateTime(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.VacationBases", t => t.VacationBaseId, cascadeDelete: true)
                .Index(t => t.VacationBaseId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Role = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        PasswordHash = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VacationBases",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        AccommodationType = c.String(),
                        PricePerNight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Capacity = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Amenities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VacationBaseAmenities",
                c => new
                    {
                        VacationBaseId = c.Guid(nullable: false),
                        AmenityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VacationBaseId, t.AmenityId })
                .ForeignKey("dbo.VacationBases", t => t.VacationBaseId, cascadeDelete: true)
                .ForeignKey("dbo.Amenities", t => t.AmenityId, cascadeDelete: true)
                .Index(t => t.VacationBaseId)
                .Index(t => t.AmenityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "VacationBaseId", "dbo.VacationBases");
            DropForeignKey("dbo.VacationBaseAmenities", "AmenityId", "dbo.Amenities");
            DropForeignKey("dbo.VacationBaseAmenities", "VacationBaseId", "dbo.VacationBases");
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropIndex("dbo.VacationBaseAmenities", new[] { "AmenityId" });
            DropIndex("dbo.VacationBaseAmenities", new[] { "VacationBaseId" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropIndex("dbo.Bookings", new[] { "VacationBaseId" });
            DropTable("dbo.VacationBaseAmenities");
            DropTable("dbo.Amenities");
            DropTable("dbo.VacationBases");
            DropTable("dbo.Users");
            DropTable("dbo.Bookings");
        }
    }
}
