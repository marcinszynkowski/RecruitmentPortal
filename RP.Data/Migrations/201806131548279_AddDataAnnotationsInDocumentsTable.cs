namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInDocumentsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Documents", "UserId", "dbo.Users");
            DropPrimaryKey("dbo.Documents");
            AddPrimaryKey("dbo.Documents", "UserId");
            AddForeignKey("dbo.Documents", "UserId", "dbo.Users", "Id");
            DropColumn("dbo.Documents", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Documents", "UserId", "dbo.Users");
            DropPrimaryKey("dbo.Documents");
            AddPrimaryKey("dbo.Documents", "Id");
            AddForeignKey("dbo.Documents", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
