namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddVirtualCVTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VirtualCVs",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        Adress = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VirtualCVs", "UserId", "dbo.Users");
            DropIndex("dbo.VirtualCVs", new[] { "UserId" });
            DropTable("dbo.VirtualCVs");
        }
    }
}