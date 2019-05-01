namespace KenakataApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 15),
                        UserRoles = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShopkeeperId = c.Int(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        DiscountAmount = c.Int(nullable: false),
                        FromDate = c.Long(nullable: false),
                        ToDate = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shopkeepers", t => t.ShopkeeperId, cascadeDelete: true)
                .Index(t => t.ShopkeeperId);
            
            CreateTable(
                "dbo.Shopkeepers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShopName = c.String(nullable: false),
                        ShopType = c.String(nullable: false),
                        FullAddress = c.String(nullable: false),
                        OwnerName = c.String(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        ImageURL = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Discounts", "ShopkeeperId", "dbo.Shopkeepers");
            DropIndex("dbo.Discounts", new[] { "ShopkeeperId" });
            DropTable("dbo.Shopkeepers");
            DropTable("dbo.Discounts");
            DropTable("dbo.AdminLogins");
        }
    }
}
