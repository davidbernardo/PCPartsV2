namespace PCPartsV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Discount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Discount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Discount");
        }
    }
}
