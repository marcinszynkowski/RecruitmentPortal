namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInEducationTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Educations", "EducationLevelId", "dbo.EducationLevels");
            DropIndex("dbo.Educations", new[] { "EducationLevelId" });
            AlterColumn("dbo.Educations", "NameOfSchool", c => c.String(nullable: false));
            DropColumn("dbo.Educations", "EducationLevelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Educations", "EducationLevelId", c => c.Int(nullable: false));
            AlterColumn("dbo.Educations", "NameOfSchool", c => c.String());
            CreateIndex("dbo.Educations", "EducationLevelId");
            AddForeignKey("dbo.Educations", "EducationLevelId", "dbo.EducationLevels", "Id", cascadeDelete: true);
        }
    }
}
