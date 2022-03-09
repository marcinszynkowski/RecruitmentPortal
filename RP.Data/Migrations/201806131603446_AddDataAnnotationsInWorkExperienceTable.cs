namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInWorkExperienceTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkExperiences", "Company", c => c.String(nullable: false));
            AlterColumn("dbo.WorkExperiences", "Position", c => c.String(nullable: false));
            AlterColumn("dbo.WorkExperiences", "DateFrom", c => c.DateTime(nullable: false));
            AlterColumn("dbo.WorkExperiences", "DateTo", c => c.DateTime(nullable: false));
            AlterColumn("dbo.WorkExperiences", "Responsibilities", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkExperiences", "Responsibilities", c => c.String());
            AlterColumn("dbo.WorkExperiences", "DateTo", c => c.DateTime());
            AlterColumn("dbo.WorkExperiences", "DateFrom", c => c.DateTime());
            AlterColumn("dbo.WorkExperiences", "Position", c => c.String());
            AlterColumn("dbo.WorkExperiences", "Company", c => c.String());
        }
    }
}
