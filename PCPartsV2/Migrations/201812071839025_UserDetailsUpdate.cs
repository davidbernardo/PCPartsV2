namespace PCPartsV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserDetailsUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "User_DetailsFK", "dbo.User_Details");
            DropIndex("dbo.Orders", new[] { "User_DetailsFK" });
            AddColumn("dbo.Orders", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "PostalCode", c => c.String());
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Orders", "User_DetailsFK");
            DropTable("dbo.User_Details");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.User_Details",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Role = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PostalCode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            AddColumn("dbo.Orders", "User_DetailsFK", c => c.Int(nullable: false));
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropColumn("dbo.AspNetUsers", "PostalCode");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.Orders", "UserId");
            CreateIndex("dbo.Orders", "User_DetailsFK");
            AddForeignKey("dbo.Orders", "User_DetailsFK", "dbo.User_Details", "UserID", cascadeDelete: true);
        }
    }
}
