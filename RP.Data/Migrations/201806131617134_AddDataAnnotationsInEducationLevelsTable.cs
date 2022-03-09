namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInEducationLevelsTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.EducationLevels");
            AddColumn("dbo.EducationLevels", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.EducationLevels", "Name", c => c.String(nullable: false));
            AddPrimaryKey("dbo.EducationLevels", "UserId");
            CreateIndex("dbo.EducationLevels", "UserId");
            AddForeignKey("dbo.EducationLevels", "UserId", "dbo.Users", "Id");
            DropColumn("dbo.EducationLevels", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EducationLevels", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.EducationLevels", "UserId", "dbo.Users");
            DropIndex("dbo.EducationLevels", new[] { "UserId" });
            DropPrimaryKey("dbo.EducationLevels");
            AlterColumn("dbo.EducationLevels", "Name", c => c.String());
            DropColumn("dbo.EducationLevels", "UserId");
            AddPrimaryKey("dbo.EducationLevels", "Id");
        }
    }
}
