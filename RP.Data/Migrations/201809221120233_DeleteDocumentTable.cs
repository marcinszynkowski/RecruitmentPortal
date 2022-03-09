namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DeleteDocumentTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Documents", "UserId", "dbo.Users");
            DropIndex("dbo.Documents", new[] { "UserId" });
            AlterColumn("dbo.Courses", "DateFrom", c => c.DateTime());
            AlterColumn("dbo.Courses", "DateTo", c => c.DateTime());
            AlterColumn("dbo.Skills", "Description", c => c.String());
            AlterColumn("dbo.WorkExperiences", "Responsibilities", c => c.String());
            DropTable("dbo.Documents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        VirtualCvVisibility = c.Boolean(nullable: false),
                        Cv = c.String(),
                        LetterOfMotivation = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            AlterColumn("dbo.WorkExperiences", "Responsibilities", c => c.String(nullable: false));
            AlterColumn("dbo.Skills", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "DateTo", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Courses", "DateFrom", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Documents", "UserId");
            AddForeignKey("dbo.Documents", "UserId", "dbo.Users", "Id");
        }
    }
}
