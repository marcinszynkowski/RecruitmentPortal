namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DeleteVirtualCVTableMoveAdressToUserTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VirtualCV", "UserId", "dbo.Users");
            DropIndex("dbo.VirtualCV", new[] { "UserId" });
            AddColumn("dbo.Users", "VirtualCVAdress", c => c.Guid());
            DropTable("dbo.VirtualCV");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VirtualCV",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        Adress = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            DropColumn("dbo.Users", "VirtualCVAdress");
            CreateIndex("dbo.VirtualCV", "UserId");
            AddForeignKey("dbo.VirtualCV", "UserId", "dbo.Users", "Id");
        }
    }
}
